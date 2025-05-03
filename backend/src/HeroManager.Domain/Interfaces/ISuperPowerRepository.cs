using HeroManager.Domain.Entities;

namespace HeroManager.Domain.Interfaces;

public interface ISuperPowerRepository
{
    Task<List<SuperPower?>> GetListByIdsAsync(List<int> id);

    Task<List<SuperPower>> GetAllAsync();

    Task<SuperPower?> GetByIdAsync(int id);
}