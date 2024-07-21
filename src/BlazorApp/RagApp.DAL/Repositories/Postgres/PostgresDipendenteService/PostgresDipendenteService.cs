using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RagApp.DAL.PostgresModels;

namespace RagApp.DAL.Repositories.Postgres.PostgresDipendenteService
{
    public class PostgresDipendenteService : IPostgresDipendenteService
    {
        private readonly PostgresDbContext _context;

        public PostgresDipendenteService(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dipendente>> GetAllAsync()
        {
            return await _context.Dipendenti.ToListAsync();
        }

        public async Task<Dipendente> GetByIdAsync(int id)
        {
            return await _context.Dipendenti.FindAsync(id);
        }

        public async Task<Dipendente> CreateAsync(Dipendente dipendente)
        {
            _context.Dipendenti.Add(dipendente);
            await _context.SaveChangesAsync();
            return dipendente;
        }

        public async Task UpdateAsync(int id, Dipendente dipendenteIn)
        {
            var existingDipendente = await _context.Dipendenti.FindAsync(id);
            if (existingDipendente != null)
            {
                existingDipendente.Nome = dipendenteIn.Nome;
                existingDipendente.Posizione = dipendenteIn.Posizione;
                existingDipendente.Dipartimento = dipendenteIn.Dipartimento;
                existingDipendente.Eta = dipendenteIn.Eta;
                existingDipendente.DataAssunzione = dipendenteIn.DataAssunzione;
                existingDipendente.Email = dipendenteIn.Email;

                _context.Dipendenti.Update(existingDipendente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var dipendente = await _context.Dipendenti.FindAsync(id);
            if (dipendente != null)
            {
                _context.Dipendenti.Remove(dipendente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<long> UpsertDipendentiAsync(IEnumerable<Dipendente> dipendenti)
        {
            foreach (var dipendente in dipendenti)
            {
                var existingDipendente = await _context.Dipendenti.FindAsync(dipendente.Id);
                if (existingDipendente == null)
                {
                    _context.Dipendenti.Add(dipendente);
                }
                else
                {
                    existingDipendente.Nome = dipendente.Nome;
                    existingDipendente.Posizione = dipendente.Posizione;
                    existingDipendente.Dipartimento = dipendente.Dipartimento;
                    existingDipendente.Eta = dipendente.Eta;
                    existingDipendente.DataAssunzione = dipendente.DataAssunzione;
                    existingDipendente.Email = dipendente.Email;

                    _context.Dipendenti.Update(existingDipendente);
                }
            }

            return await _context.SaveChangesAsync();
        }
    }
}
