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
        if (await _heroRepository.HeroNameExistsAsync(createHeroDTO.HeroName))
        {
            throw new InvalidOperationException("Já existe um heroi com esse HeroName");
        }

        var hero = new Hero(createHeroDTO.Name, createHeroDTO.HeroName, createHeroDTO.BirthDate, createHeroDTO.Height, createHeroDTO.Weight);

        var superPowerlist = await _superPowerRepository.GetListByIdsAsync(createHeroDTO.SuperPowersId);

        if (superPowerlist == null || !superPowerlist.Any())
            throw new KeyNotFoundException("Não foi encontrado nenhum super poder para os Ids informados");

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
            SuperPowers = superPowerlist.Select(sp => sp.Name).ToList()
        };
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0)
            throw new Exception("O ID fornecido é inválido. Deve ser um número inteiro positivo.");

        var hero = await _heroRepository.GetByIdAsync(id);

        if (hero == null) throw new KeyNotFoundException("Não existe nenhum super-heroi com o id informado");

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
        if (id <= 0)
            throw new Exception("O ID fornecido é inválido. Deve ser um número inteiro positivo.");
        
        var hero = await _heroRepository.GetByIdAsync(id);

        if (hero == null)
            throw new KeyNotFoundException("Não existe nenhum heroi com o id informado");

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

    public async Task<HeroDTO> UpdateAsync(int id, UpdateHeroDTO updateHeroDTO)
    {
        if (await _heroRepository.IsHeroNameUniqueExceptCurrentId(id, updateHeroDTO.HeroName))
        {
            throw new InvalidOperationException("Já existe um Heroi com esse HeroName");
        }

        if (id <= 0)
            throw new Exception("O ID fornecido é inválido. Deve ser um número inteiro positivo.");

        var hero = await _heroRepository.GetByIdAsync(id);

        if (hero == null) return null;

        hero.UpdateDate(updateHeroDTO.Name, updateHeroDTO.HeroName, updateHeroDTO.BirthDate, updateHeroDTO.Height, updateHeroDTO.Weight);

        var superPowerList = await _superPowerRepository.GetListByIdsAsync(updateHeroDTO.SuperPowersId);

        if (superPowerList == null || !superPowerList.Any())
            throw new KeyNotFoundException("Não foi encontrado nenhum super poder para os Ids informados");

        hero.AssignSuperPower(superPowerList);

        await _heroRepository.UpdateAsync(hero);

        return new HeroDTO
        {
            Id = hero.Id,
            Name = hero.Name,
            HeroName = hero.HeroName,
            BirthDate = hero.BirthDate,
            Height = hero.Height,
            Weight = hero.Weight,
            SuperPowers = superPowerList.Select(sp => sp.Name).ToList()
        };
    }
}