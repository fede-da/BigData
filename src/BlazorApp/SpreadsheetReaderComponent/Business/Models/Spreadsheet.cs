using System;
using System.Collections.Generic;
using System.Text;

namespace SpreedsheetReader.Models
{
    public abstract class Spreadsheet
    {
        public abstract void AddRow(string row);
        public abstract ICollection<string> GetRows();
    }
}
