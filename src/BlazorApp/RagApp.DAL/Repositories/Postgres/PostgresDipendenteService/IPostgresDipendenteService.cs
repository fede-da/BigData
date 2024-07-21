using System.Collections.Generic;
using System.Threading.Tasks;
using RagApp.DAL.PostgresModels;

namespace RagApp.DAL.Repositories.Postgres.PostgresDipendenteService
{
    public interface IPostgresDipendenteService
    {
        Task<List<Dipendente>> GetAllAsync();
        Task<Dipendente> GetByIdAsync(int id);
        Task<Dipendente> CreateAsync(Dipendente dipendente);
        Task UpdateAsync(int id, Dipendente dipendenteIn);
        Task DeleteAsync(int id);
        Task<long> UpsertDipendentiAsync(IEnumerable<Dipendente> dipendenti);
    }
}
