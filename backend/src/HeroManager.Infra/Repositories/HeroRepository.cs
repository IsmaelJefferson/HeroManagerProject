using HeroManager.Domain.Entities;
using HeroManager.Domain.Interfaces;
using HeroManager.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HeroManager.Infra.Repositories;

public class HeroRepository : IHeroRepository
{
    private readonly ApplicationDbContext  _context;
    public HeroRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Hero hero)
    {
        await _context.Heros.AddAsync(hero);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Hero hero)
    {
        _context.Heros.Remove(hero);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Hero>> GetAllAsync()
    {
        return await _context.Heros.Include(h => h.SuperPowers).ThenInclude(sp => sp.SuperPower).ToListAsync();
    }

    public async Task<Hero?> GetByHeroNameAsync(string heroName)
    {
        return await _context.Heros.AsNoTracking().FirstOrDefaultAsync(h => h.HeroName.ToLower() == heroName.ToLower());
    }

    public async Task<Hero?> GetByIdAsync(int id)
    {
        return await _context.Heros.Include(h => h.SuperPowers).ThenInclude(sp => sp.SuperPower).FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<bool> HeroNameExistsAsync(string heroName)
    {
        return await _context.Heros.AnyAsync(h => h.HeroName.ToLower() == heroName.ToLower());
    }

    public async Task<bool> IsHeroNameUniqueExceptCurrentId(int id, string heroName)
    {
        return await _context.Heros.AnyAsync(h => h.HeroName.ToLower() == heroName.ToLower() && h.Id != id);
    }

    public async Task UpdateAsync(Hero hero)
    {
        _context.Heros.Update(hero);
        await _context.SaveChangesAsync();
    }
}