using System;
using System.Globalization;
using RagApp.DAL.PostgresModels;

namespace RagApp.DAL.Utils
{
    public static class DipendenteParser
    {
        public static Dipendente Parse(string csvLine)
        {
            var values = csvLine.Split(',');

            if (values.Length < 6)
            {
                throw new FormatException("La riga CSV non contiene un numero sufficiente di valori.");
            }

            return new Dipendente
            {
                Nome = values[0],
                Posizione = values[1],
                Dipartimento = values[2],
                Eta = int.Parse(values[3], CultureInfo.InvariantCulture),
                DataAssunzione = CustomDateTimeConverter.ConvertFromString(values[4]), // Usa la funzione CustomDateTimeConverter
                Email = values[5]
            };
        }
    }
}
