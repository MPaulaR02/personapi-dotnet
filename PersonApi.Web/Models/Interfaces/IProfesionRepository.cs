using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Interfaces;

public interface IProfesionRepository
{
    Task<List<Profesion>> GetAllAsync();
    Task<Profesion?> GetByIdAsync(int id);
    Task AddAsync(Profesion entity);
    Task UpdateAsync(Profesion entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}


