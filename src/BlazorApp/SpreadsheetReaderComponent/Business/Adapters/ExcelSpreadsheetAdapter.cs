using ClosedXML.Excel;
using SpreedsheetReader.Interfaces;
using SpreedsheetReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreedsheetReader.Adapters
{
    public class ExcelSpreadsheetAdapter : Spreadsheet
    {
        private List<string> columns;
        private List<string> rows;

        // Existing constructor that takes a file path
        public ExcelSpreadsheetAdapter(string filePath)
        {
            columns = new List<string>();
            rows = new List<string>();

            Initialize(new XLWorkbook(filePath));
        }

        // New constructor that takes a stream
        public ExcelSpreadsheetAdapter(Stream stream)
        {
            columns = new List<string>();
            rows = new List<string>();
            var workbook = new XLWorkbook(stream);

            Initialize(workbook);
        }

        private void Initialize(IXLWorkbook workbook)
        {
            var worksheet = workbook.Worksheet(1);
            var range = worksheet.RangeUsed();

            // Read column headers
            foreach (var cell in range.Row(1).Cells())
            {
                columns.Add(cell.Value.ToString());
            }

            // Read rows
            for (int rowIndex = 2; rowIndex <= range.RowCount(); rowIndex++)
            {
                var rowCells = range.Row(rowIndex).Cells();
                string row = string.Join(",", rowCells.Select(cell => cell.Value.ToString()));
                rows.Add(row);
            }
        }

        public override void AddRow(string row)
        {
            // 'row' is expected to be a comma-separated string of values
            rows.Add(row);
        }

        public override ICollection<string> GetRows()
        {
            return rows;
        }
        public void PrintRows()
        {
            foreach (var cell in rows)
            {
                Console.WriteLine(cell.ToString());
            }
        }
    }
}
