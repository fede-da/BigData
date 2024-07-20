using System;
using System.Globalization;

namespace RagApp.DAL.Utils
{
    public class CustomDateTimeConverter
    {
        private static readonly string[] Formats = { "M/d/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" }; // Aggiungi altri formati se necessario

        public static DateTime ConvertFromString(string text)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(text, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }

            throw new FormatException($"Cannot convert '{text}' to DateTime.");
        }
    }
}
