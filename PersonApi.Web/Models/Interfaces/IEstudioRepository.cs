using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Interfaces;

public interface IEstudioRepository
{
    Task<List<Estudio>> GetAllAsync();
    Task<Estudio?> GetByKeyAsync(int idProf, int ccPer);
    Task<Estudio?> GetByKeyWithRelationsAsync(int idProf, int ccPer);
    Task AddAsync(Estudio entity);
    Task UpdateAsync(Estudio entity);
    Task DeleteAsync(int idProf, int ccPer);
    Task<bool> ExistsAsync(int idProf, int ccPer);
}


