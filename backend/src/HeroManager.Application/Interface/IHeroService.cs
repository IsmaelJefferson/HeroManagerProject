using HeroManager.Application.DTOs;

namespace HeroManager.Application.Interface;

public interface IHeroService
{
    Task<List<HeroDTO>> GetAllAsync();
    Task<HeroDTO> GetByIdAsync(int id);
    Task<HeroDTO> CreateAsync(CreateHeroDTO dto);
    Task <HeroDTO> UpdateAsync(int id, UpdateHeroDTO dto);
    Task DeleteAsync(int id);
}