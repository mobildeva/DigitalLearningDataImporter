using System.Collections.Generic;
using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;
using System;

namespace DigitalLearningIntegration.Application.Utils
{
    public class ReadWriteExcel
    {
        public static DataTable ReadExcelSheet(string fname, bool firstRowIsHeader)
        {

            var cellAdress = new Dictionary<int, string>()
            {
                { 0, "A" },{1, "B" },{2, "C" },{3,"D" },{4,"E" },{5,"F" },{6,"G" },{7,"H" },{8,"I" },{9,"J" },{10, "K" },{11,"L" },{12,"M" },{13,"N" },{14,"O" },{15,"P" },{16, "Q" },{17, "R" },{18, "S" },{19, "T" },{20,"U" },{21,"V" },{22, "W" },{23,"X" },{24,"Y" },{25, "Z" },
                {26, "AA" },{27, "AB" },{28,"AC" }, {29 , "AD" }, {30, "AE" }, {31, "AF" }, {32, "AG" }, {33, "AH" }, {34, "AI" }, {35, "AJ" }, {36, "AK" }, {37, "AL" }, {38, "AM" }, {39, "AN" }, {40, "AO" }, {41, "AP" }, {42, "AQ" }, {43, "AR" }, {44, "AS" }, {45, "AT" }, {46, "AU" }, {47, "AV" }, {48, "AW" }, {49, "AX" }, {50, "AY" }, {51, "AZ" },
                { 52, "BA" }, {53, "BB" }, {54, "BC" }, {55, "BD" }
            };

            List<string> Headers = new List<string>();
            DataTable dt = new DataTable();
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fname, false))
            {
                //Read the first Sheets 
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                int counter = 0;
                foreach (Row row in rows)
                {
                    counter += 1;
                    Console.Write("r: " + counter + "; ");

                    //Read the first row as header
                    if (counter == 1)
                    {
                        var j = 1;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            var colunmName = firstRowIsHeader ? GetCellValue(doc, cell) : "Field" + j++;
                            Headers.Add(colunmName);
                            dt.Columns.Add(colunmName);
                        }
                    }
                    else
                    {
                        var values = new List<object>();

                        int i = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            values.Add(GetCellValue(doc, cell));
                            i++;
                        }

                        if (values.Count == 56)
                        {
                            dt.Rows.Add(values.ToArray());
                        }
                        else
                        {
                            var invalids = dt.ToString();
                        }
                    }

                }

            }
            return dt;
        }

        static string GetCellValue(SpreadsheetDocument doc, Cell cell)
        {
            if (cell != null && cell.CellValue != null)
            {
                string value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }

                return value;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetCellValue(string fileName, string sheetName, string addressName)
        {
            string value = null; // Open the spreadsheet document for read-only access. 
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
            {
                // Retrieve a reference to the workbook part. 
                WorkbookPart wbPart = document.WorkbookPart;
                // Find the sheet with the supplied name, and then use that 
                // Sheet object to retrieve a reference to the first worksheet. 
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();
                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }
                // Retrieve a reference to the worksheet part. 
                WorksheetPart wsPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
                // Use its Worksheet property to get a reference to the cell 
                // whose address matches the address you supplied. 
                Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == addressName).FirstOrDefault();
                // If the cell does not exist, return an empty string. 
                if (theCell != null)
                {
                    value = theCell.InnerText; // If the cell represents an integer number, you are done. // For dates, this code returns the serialized value that // represents the date. The code handles strings and // Booleans individually. For shared strings, the code // looks up the corresponding value in the shared string // table. For Booleans, the code converts the value into // the words TRUE or FALSE. 
                    if (theCell.DataType != null)
                    {
                        switch (theCell.DataType.Value)
                        {
                            case CellValues.SharedString: // For shared strings, look up the value in the // shared strings table. 
                                var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault(); // If the shared string table is missing, something // is wrong. Return the index that is in // the cell. Otherwise, look up the correct text in // the table. 
                                if (stringTable != null) { value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText; }
                                break;
                            case CellValues.Boolean: switch (value) { case "0": value = "FALSE"; break; default: value = "TRUE"; break; } break;
                        }
                    }
                }
            }
            return value;
        }
    }
}
