using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Interfaces;

public interface ITelefonoRepository
{
    Task<List<Telefono>> GetAllAsync();
    Task<Telefono?> GetByNumAsync(string num);
    Task AddAsync(Telefono entity);
    Task UpdateAsync(Telefono entity);
    Task DeleteAsync(string num);
    Task<bool> ExistsAsync(string num);
}


