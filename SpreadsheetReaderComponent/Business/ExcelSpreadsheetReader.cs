using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreedsheetReader.Adapters;
using SpreedsheetReader.Interfaces;
using SpreedsheetReader.Models;

namespace SpreedsheetReader
{
    public class ExcelSpreadsheetReader : ISpreadsheetReader
    {
        public Spreadsheet Read(string fileAsPathOrStream)
        {
            return new ExcelSpreadsheetAdapter(fileAsPathOrStream);
        }
    }
}
