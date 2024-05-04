# TODO
Separate creation from classes
I don't want to instantiate a specific implementation like the following line:

var excelSpreadsheet = new ExcelSpreadsheetAdapter(stream);

It should be something more generic like:
excelSpreadsheet = new SpreedSheetReader(stream);