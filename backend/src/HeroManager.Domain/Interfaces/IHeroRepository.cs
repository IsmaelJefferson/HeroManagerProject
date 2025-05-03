using HeroManager.Domain.Entities;

namespace HeroManager.Domain.Interfaces;

public interface IHeroRepository
{
    Task<IEnumerable<Hero>> GetAllAsync();

    Task<Hero?> GetByIdAsync(int id);

    Task<Hero?> GetByHeroNameAsync(string heroName);

    Task CreateAsync(Hero hero);

    Task UpdateAsync(Hero hero);

    Task DeleteAsync(Hero hero);

    Task<bool> HeroNameExistsAsync(string heroName);
    
    Task<bool> IsHeroNameUniqueExceptCurrentId(int id, string heroName);
}