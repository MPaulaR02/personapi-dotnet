using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Interfaces;

public interface IPersonaRepository
{
    Task<List<Persona>> GetAllAsync();
    Task<Persona?> GetByIdAsync(int id);
    Task<Persona?> GetByIdWithRelationsAsync(int id);
    Task AddAsync(Persona entity);
    Task UpdateAsync(Persona entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}


