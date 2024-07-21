using System;
using System.Globalization;

public static class CustomDateTimeConverter
{
    public static DateTime ConvertFromString(string dateTimeString)
    {
        // Specifica il formato della data che ti aspetti
        string[] formats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy", "M/d/yyyy"};

        if (DateTime.TryParseExact(dateTimeString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate.ToUniversalTime(); // Assicurati che sia in UTC
        }

        throw new FormatException($"Formato della data non valido per il valore: {dateTimeString}");
    }
}
