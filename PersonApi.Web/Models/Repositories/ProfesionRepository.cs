using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Models.Repositories;

public class ProfesionRepository : IProfesionRepository
{
    private readonly PersonaDbContext _context;

    public ProfesionRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Profesion>> GetAllAsync()
    {
        return await _context.Profesions.AsNoTracking().ToListAsync();
    }

    public async Task<Profesion?> GetByIdAsync(int id)
    {
        return await _context.Profesions.FindAsync(id);
    }

    public async Task AddAsync(Profesion entity)
    {
        _context.Profesions.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Profesion entity)
    {
        _context.Profesions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Profesions.FindAsync(id);
        if (entity != null)
        {
            _context.Profesions.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Profesions.AnyAsync(e => e.Id == id);
    }
}


