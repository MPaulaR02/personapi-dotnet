using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Models.Repositories;

public class EstudioRepository : IEstudioRepository
{
    private readonly PersonaDbContext _context;

    public EstudioRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Estudio>> GetAllAsync()
    {
        return await _context.Estudios
            .Include(e => e.CcPerNavigation)
            .Include(e => e.IdProfNavigation)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Estudio?> GetByKeyAsync(int idProf, int ccPer)
    {
        return await _context.Estudios.FindAsync(idProf, ccPer);
    }

    public async Task<Estudio?> GetByKeyWithRelationsAsync(int idProf, int ccPer)
    {
        return await _context.Estudios
            .Include(e => e.CcPerNavigation)
            .Include(e => e.IdProfNavigation)
            .FirstOrDefaultAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
    }

    public async Task AddAsync(Estudio entity)
    {
        _context.Estudios.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Estudio entity)
    {
        _context.Estudios.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int idProf, int ccPer)
    {
        var entity = await _context.Estudios.FindAsync(idProf, ccPer);
        if (entity != null)
        {
            _context.Estudios.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int idProf, int ccPer)
    {
        return await _context.Estudios.AnyAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
    }
}


