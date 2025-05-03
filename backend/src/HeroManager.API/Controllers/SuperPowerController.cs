using HeroManager.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeroManager.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento dos superpoderes no sistema
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SuperPowerController : ControllerBase
{
    private readonly ISuperPowerRepository _superPowerRepository;

    public SuperPowerController(ISuperPowerRepository superPowerRepository)
    {
        _superPowerRepository = superPowerRepository;
    }
    
    /// <summary>
    /// Retorna a lista de todos os superpoderes disponiveis.
    /// </summary>
    /// <returns>Uma lista de super poderes.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var superPowerList = await _superPowerRepository.GetAllAsync();
        return Ok(superPowerList);
    }
}