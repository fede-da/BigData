using Microsoft.Extensions.Logging;
using RagApp.DAL.PostgresModels;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RagApp.DAL.Repositories.Postgres.PostgresDipendenteService;
using RagApp.DAL.Utils;

namespace RagApp.Services.PostgresService
{
    public class CsvService
    {
        private readonly ILogger<CsvService> _logger;
        private readonly IPostgresDipendenteService _postgresDipendenteService;

        public CsvService(ILogger<CsvService> logger, IPostgresDipendenteService postgresDipendenteService)
        {
            _logger = logger;
            _postgresDipendenteService = postgresDipendenteService;
        }

        public async Task ImportCsvAsync(Stream csvStream)
        {
            try
            {
                using (var reader = new StreamReader(csvStream))
                {
                    string headerLine = reader.ReadLine(); // Leggi l'intestazione
                    if (headerLine == null)
                    {
                        _logger.LogWarning("Il file CSV è vuoto.");
                        return;
                    }

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var dipendente = DipendenteParser.Parse(line);

                        var existingDipendente = await _postgresDipendenteService.GetByIdAsync(dipendente.Id.ToString());
                        if (existingDipendente == null)
                        {
                            await _postgresDipendenteService.CreateAsync(dipendente);
                        }
                        else
                        {
                            await _postgresDipendenteService.UpdateAsync(existingDipendente.Id.ToString(), dipendente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'importazione del CSV.");
                throw;
            }
        }
    }
}
