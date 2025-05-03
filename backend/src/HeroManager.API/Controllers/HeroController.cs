using HeroManager.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using HeroManager.Application.Interface;

namespace HeroManager.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de heróis no sistema.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HeroController : ControllerBase
{
    private readonly IHeroService _heroService;

    public HeroController(IHeroService heroService)
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
        var heroes = await _heroService.GetAllAsync();

        if (heroes == null || !heroes.Any())
        {
            return NotFound("Não existe nenhum super-heroi cadastrado");
        }

        return Ok(heroes);
    }

    /// <summary>
    /// Retorna um herói específico por ID.
    /// </summary>
    /// <param name="id">ID do herói.</param>
    /// <returns>O herói correspondente ao ID informado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<HeroDTO>> GetById(int id)
    {
        try
        {
            var hero = await _heroService.GetByIdAsync(id);
            return Ok(hero);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Cadastra um novo herói.
    /// </summary>
    /// <param name="createHeroDTO">Objeto com os dados do novo herói.</param>
    /// <returns>O herói criado.</returns>
    [HttpPost]
    public async Task<ActionResult<HeroDTO>> Create([FromBody] CreateHeroDTO createHeroDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var hero = await _heroService.CreateAsync(createHeroDTO);

            return Ok(new
            {
                message = "Super-herói cadastrado com sucesso.",
                date = hero
            }
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza um herói existente.
    /// </summary>
    /// <param name="id">ID do herói a ser atualizado.</param>
    /// <param name="updateHeroDTO">Objeto com os dados atualizados.</param>
    /// <returns>Nenhum conteúdo.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<HeroDTO>> Update(int id, [FromBody] UpdateHeroDTO updateHeroDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var hero = await _heroService.UpdateAsync(id, updateHeroDTO);

            if (hero == null) return NotFound($"Não existe nenhum super-heroi com o id informado");

            return Ok(new
            {
                message = "Super-herói atualizado com sucesso.",
                date = hero
            }
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
        try
        {
            await _heroService.DeleteAsync(id);

            return Ok(new
            {
                message = "Herói deletado com sucesso"
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}