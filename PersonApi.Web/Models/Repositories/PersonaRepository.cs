using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Models.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly PersonaDbContext _context;

    public PersonaRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Persona>> GetAllAsync()
    {
        return await _context.Personas.AsNoTracking().ToListAsync();
    }

    public async Task<Persona?> GetByIdAsync(int id)
    {
        return await _context.Personas.FindAsync(id);
    }

    public async Task<Persona?> GetByIdWithRelationsAsync(int id)
    {
        return await _context.Personas
            .Include(p => p.Telefonos)
            .Include(p => p.Estudios)
            .FirstOrDefaultAsync(p => p.Cc == id);
    }

    public async Task AddAsync(Persona entity)
    {
        _context.Personas.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Persona entity)
    {
        _context.Personas.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Personas.FindAsync(id);
        if (entity != null)
        {
            _context.Personas.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Personas.AnyAsync(e => e.Cc == id);
    }
}


