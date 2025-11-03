using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ProfesionApiController : ControllerBase
{
    private readonly IProfesionRepository _profesiones;

    public ProfesionApiController(IProfesionRepository profesiones)
    {
        _profesiones = profesiones;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profesion>>> GetAll()
    {
        return Ok(await _profesiones.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Profesion>> GetById(int id)
    {
        var item = await _profesiones.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Profesion>> Create(Profesion entity)
    {
        await _profesiones.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Profesion entity)
    {
        if (id != entity.Id) return BadRequest();
        if (!await _profesiones.ExistsAsync(id)) return NotFound();
        await _profesiones.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _profesiones.ExistsAsync(id)) return NotFound();
        await _profesiones.DeleteAsync(id);
        return NoContent();
    }
}


