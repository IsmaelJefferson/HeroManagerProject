using HeroManager.Application.DTOs;
using HeroManager.Application.Interface;
using HeroManager.Domain.Entities;
using HeroManager.Domain.Interfaces;

namespace HeroManager.Application.Services;

public class HeroService : IHeroService
{
    private readonly IHeroRepository _heroRepository;
    private readonly ISuperPowerRepository _superPowerRepository;

    public HeroService(IHeroRepository heroRepository, ISuperPowerRepository superPowerRepository)
    {
        _heroRepository = heroRepository;
        _superPowerRepository = superPowerRepository;
    }

    public async Task<HeroDTO> CreateAsync(CreateHeroDTO createHeroDTO)
    {
        if(await _heroRepository.HeroNameExistsAsync(createHeroDTO.HeroName))
        {
            throw new InvalidOperationException("Já existe um heroi com esse HeroName");
        }

        var hero = new Hero(createHeroDTO.Name, createHeroDTO.HeroName, createHeroDTO.BirthDate, createHeroDTO.Height, createHeroDTO.Weight);

        var superPowerlist = await _superPowerRepository.GetListByIdsAsync(createHeroDTO.SuperPowersId);

        hero.AssignSuperPower(superPowerlist);

        await _heroRepository.CreateAsync(hero);

        return new HeroDTO
        {
            Id = hero.Id,
            Name = hero.Name,
            HeroName = hero.HeroName,
            BirthDate = hero.BirthDate,
            Height = hero.Height,
            Weight = hero.Weight,
            SuperPowers	= superPowerlist.Select(sp => sp.Name).ToList()
        };

    }

    public async Task DeleteAsync(int id)
    {
        var hero = await _heroRepository.GetByIdAsync(id);

        if(hero == null) return;

        await _heroRepository.DeleteAsync(hero);
    }

    public async Task<List<HeroDTO>> GetAllAsync()
    {
        var heros = await _heroRepository.GetAllAsync();

        return heros.Select(h => new HeroDTO
            {
                Id = h.Id,
                Name = h.Name,
                HeroName = h.HeroName,
                BirthDate = h.BirthDate,
                Height = h.Height,
                Weight = h.Weight,
                SuperPowers = h.SuperPowers.Select(sp => sp.SuperPower?.Name).ToList()
            }).ToList();
    }

    public async Task<HeroDTO> GetByIdAsync(int id)
    {
        var hero = await _heroRepository.GetByIdAsync(id);

        if(hero == null) return null;

        return new HeroDTO
        {
            Id = hero.Id,
            Name = hero.Name,
            HeroName = hero.HeroName,
            BirthDate = hero.BirthDate,
            Height = hero.Height,
            Weight = hero.Weight,
            SuperPowers = hero.SuperPowers.Select(sp => sp.SuperPower?.Name).ToList()
        };
    }

    public async Task UpdateAsync(int id, UpdateHeroDTO updateHeroDTO)
    {
        if (await _heroRepository.IsHeroNameUniqueExceptCurrentId(id, updateHeroDTO.HeroName))
        {
            throw new InvalidOperationException("Já existe um Heroi com esse HeroName");
        }
        
        var hero = await _heroRepository.GetByIdAsync(id);

        if(hero == null) return;

        hero.UpdateDate(updateHeroDTO.Name, updateHeroDTO.HeroName, updateHeroDTO.BirthDate, updateHeroDTO.Height, updateHeroDTO.Weight);

        var superPowerList = await _superPowerRepository.GetListByIdsAsync(updateHeroDTO.SuperPowersId);

        hero.AssignSuperPower(superPowerList);
        
        await _heroRepository.UpdateAsync(hero);
    }
}