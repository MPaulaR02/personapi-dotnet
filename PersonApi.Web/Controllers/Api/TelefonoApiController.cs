using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class TelefonoApiController : ControllerBase
{
    private readonly ITelefonoRepository _telefonos;

    public TelefonoApiController(ITelefonoRepository telefonos)
    {
        _telefonos = telefonos;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Telefono>>> GetAll()
    {
        return Ok(await _telefonos.GetAllAsync());
    }

    [HttpGet("{num}")]
    public async Task<ActionResult<Telefono>> GetByNum(string num)
    {
        var item = await _telefonos.GetByNumAsync(num);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Telefono>> Create(Telefono entity)
    {
        await _telefonos.AddAsync(entity);
        return CreatedAtAction(nameof(GetByNum), new { num = entity.Num }, entity);
    }

    [HttpPut("{num}")]
    public async Task<IActionResult> Update(string num, Telefono entity)
    {
        if (num != entity.Num) return BadRequest();
        if (!await _telefonos.ExistsAsync(num)) return NotFound();
        await _telefonos.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{num}")]
    public async Task<IActionResult> Delete(string num)
    {
        if (!await _telefonos.ExistsAsync(num)) return NotFound();
        await _telefonos.DeleteAsync(num);
        return NoContent();
    }
}


