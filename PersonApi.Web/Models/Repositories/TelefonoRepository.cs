using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Models.Repositories;

public class TelefonoRepository : ITelefonoRepository
{
    private readonly PersonaDbContext _context;

    public TelefonoRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Telefono>> GetAllAsync()
    {
        return await _context.Telefonos
            .Include(t => t.DuenioNavigation)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Telefono?> GetByNumAsync(string num)
    {
        return await _context.Telefonos
            .Include(t => t.DuenioNavigation)
            .FirstOrDefaultAsync(t => t.Num == num);
    }

    public async Task AddAsync(Telefono entity)
    {
        _context.Telefonos.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Telefono entity)
    {
        _context.Telefonos.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string num)
    {
        var entity = await _context.Telefonos.FindAsync(num);
        if (entity != null)
        {
            _context.Telefonos.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string num)
    {
        return await _context.Telefonos.AnyAsync(e => e.Num == num);
    }
}


