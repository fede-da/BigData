using System.Collections.Generic;
using System.Threading.Tasks;
using RagApp.DAL.PostgresModels;

namespace RagApp.DAL.Repositories.Postgres.PostgresDipendenteService
{
    public interface IPostgresDipendenteService
    {
        Task<List<Dipendente>> GetAllAsync();
        Task<Dipendente> GetByIdAsync(string id);
        Task<Dipendente> CreateAsync(Dipendente dipendente);
        Task UpdateAsync(string id, Dipendente dipendenteIn);
        Task DeleteAsync(string id);
        Task<long> UpsertDipendentiAsync(IEnumerable<Dipendente> dipendenti);
    }
}
