using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class PersonaApiController : ControllerBase
{
    private readonly IPersonaRepository _personas;

    public PersonaApiController(IPersonaRepository personas)
    {
        _personas = personas;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
    {
        var list = await _personas.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Persona>> GetById(int id)
    {
        var persona = await _personas.GetByIdWithRelationsAsync(id);
        if (persona == null) return NotFound();
        return Ok(persona);
    }

    [HttpPost]
    public async Task<ActionResult<Persona>> Create(Persona entity)
    {
        await _personas.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Cc }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Persona entity)
    {
        if (id != entity.Cc) return BadRequest();
        var exists = await _personas.ExistsAsync(id);
        if (!exists) return NotFound();
        await _personas.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var exists = await _personas.ExistsAsync(id);
        if (!exists) return NotFound();
        await _personas.DeleteAsync(id);
        return NoContent();
    }
}


