using HeroManager.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using HeroManager.Application.Interface;

namespace HeroManager.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de heróis no sistema.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HeroContoller : ControllerBase
{
    private readonly IHeroService _heroService;

    public HeroContoller(IHeroService heroService)
    {
        _heroService = heroService;
    }

    /// <summary>
    /// Retorna a lista de todos os heróis cadastrados.
    /// </summary>
    /// <returns>Uma lista de heróis.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HeroDTO>>> Get()
    {
        var hero = await _heroService.GetAllAsync();
        return Ok(hero);
    }

    /// <summary>
    /// Retorna um herói específico por ID.
    /// </summary>
    /// <param name="id">ID do herói.</param>
    /// <returns>O herói correspondente ao ID informado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<HeroDTO>> GetById(int id)
    {
        var hero = await _heroService.GetByIdAsync(id);
        if (hero == null) return NotFound();

        return Ok(hero);

    }

    /// <summary>
    /// Cadastra um novo herói.
    /// </summary>
    /// <param name="createHeroDTO">Objeto com os dados do novo herói.</param>
    /// <returns>O herói criado.</returns>
    [HttpPost]
    public async Task<ActionResult<HeroDTO>> Create([FromBody] CreateHeroDTO createHeroDTO)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var hero = await _heroService.CreateAsync(createHeroDTO);

            return CreatedAtAction(nameof(GetById), new {id = hero.Id}, hero);
        }
        catch(InvalidOperationException ex)
        {
            return Conflict(new {message = ex.Message});
        }

        

    }

    /// <summary>
    /// Atualiza um herói existente.
    /// </summary>
    /// <param name="id">ID do herói a ser atualizado.</param>
    /// <param name="updateHeroDTO">Objeto com os dados atualizados.</param>
    /// <returns>Nenhum conteúdo.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody] UpdateHeroDTO updateHeroDTO)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            await _heroService.UpdateAsync(id, updateHeroDTO);
            return NoContent();
        }
        catch(InvalidOperationException ex)
        {
            return Conflict(new {message = ex.Message});
        }
        

    }

    /// <summary>
    /// Remove um herói pelo ID.
    /// </summary>
    /// <param name="id">ID do herói a ser removido.</param>
    /// <returns>Nenhum conteúdo.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _heroService.DeleteAsync(id);
        return NoContent();

    }

}