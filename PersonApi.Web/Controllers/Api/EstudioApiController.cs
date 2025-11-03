using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class EstudioApiController : ControllerBase
{
    private readonly IEstudioRepository _estudios;

    public EstudioApiController(IEstudioRepository estudios)
    {
        _estudios = estudios;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudio>>> GetAll()
    {
        return Ok(await _estudios.GetAllAsync());
    }

    [HttpGet("{idProf:int}/{ccPer:int}")]
    public async Task<ActionResult<Estudio>> GetByKey(int idProf, int ccPer)
    {
        var item = await _estudios.GetByKeyWithRelationsAsync(idProf, ccPer);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Estudio>> Create(Estudio entity)
    {
        await _estudios.AddAsync(entity);
        return CreatedAtAction(nameof(GetByKey), new { idProf = entity.IdProf, ccPer = entity.CcPer }, entity);
    }

    [HttpPut("{idProf:int}/{ccPer:int}")]
    public async Task<IActionResult> Update(int idProf, int ccPer, Estudio entity)
    {
        if (idProf != entity.IdProf || ccPer != entity.CcPer) return BadRequest();
        if (!await _estudios.ExistsAsync(idProf, ccPer)) return NotFound();
        await _estudios.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{idProf:int}/{ccPer:int}")]
    public async Task<IActionResult> Delete(int idProf, int ccPer)
    {
        if (!await _estudios.ExistsAsync(idProf, ccPer)) return NotFound();
        await _estudios.DeleteAsync(idProf, ccPer);
        return NoContent();
    }
}


