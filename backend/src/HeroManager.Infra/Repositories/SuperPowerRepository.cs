using HeroManager.Domain.Entities;
using HeroManager.Domain.Interfaces;
using HeroManager.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HeroManager.Infra.Repositories;

public class SuperPowerRepository : ISuperPowerRepository
{
    private readonly ApplicationDbContext _context;

    public SuperPowerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SuperPower>> GetAllAsync()
    {
        return await _context.SuperPowers.AsNoTracking().ToListAsync(); 
    }

    public async Task<SuperPower?> GetByIdAsync(int id)
    {
        return await _context.SuperPowers.FindAsync(id);
    }

    public async Task<List<SuperPower?>> GetListByIdsAsync(List<int> ids)
    {
        return await _context.SuperPowers.Where(sp => ids.Contains(sp.Id)).ToListAsync();
    }
}