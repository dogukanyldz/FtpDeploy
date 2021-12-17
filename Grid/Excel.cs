using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace Grid
{
    class Excel
    {
        internal static void FillExcelWithData(Grid.Xml.Parameter oParameter, Data.Database oDatabase, bool exitApp)
        {
            try
            {
                Trace.WriteLine("TO-EXCEL START " + DateTime.Now);

                // Execute SQL and write it into a CSV file
                //
                string tempFileName = Path.GetTempFileName();
                FileStream csf = File.OpenWrite(tempFileName);

                WriteColumnsToCSV(oParameter, csf);
                
                SqlCommand cmd = GetData(oDatabase, oParameter.Rs.SqlQuery);
                var rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    string line = "";
                    foreach (Grid.Xml.Column col in oParameter.Columns)
                    {
                        var value = "";
                        try
                        {
                            var index = rs.GetOrdinal(col.Name);
                            if (index != -1)
                            {
                                var obj = rs[index];

                                if (obj != null)
                                {
                                    value = rs[col.Name].ToString();

                                    if (obj is decimal)
                                    {
                                        if (CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.Equals("."))
                                        {
                                            value = value.Replace(",", ".");
                                        }
                                    }

                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        { 
                        }

                        // Escape double quotes
                        value = value.Replace("\"", "\"\"");
                        value = value.Replace("\r", " ");
                        value = value.Replace("\n", " ");
                        value = value.Replace("\t", " "); // Muammer

                        line += "\"" + value + "\",";
                    }
                    WriteLineToCSV(csf, line);
                }

                
                cmd.Connection.Close();
                csf.Close();
                Trace.WriteLine("TO-EXCEL GOT DATA FROM SQL " + DateTime.Now);

                string alltext = File.ReadAllText(tempFileName, Encoding.Unicode);
                Clipboard.SetText(alltext, TextDataFormat.UnicodeText);

                var xlapp = new Microsoft.Office.Interop.Excel.Application();
                var wkbk1 = xlapp.Workbooks.Add();

                Microsoft.Office.Interop.Excel.Worksheet shit1;
                if (wkbk1.Sheets.Count < 1)
                {
                    shit1 = wkbk1.Sheets.Add() as Microsoft.Office.Interop.Excel.Worksheet;
                }
                else
                {
                    shit1 = wkbk1.Sheets.Item[1] as Microsoft.Office.Interop.Excel.Worksheet;
                }

                Range renc;
                var xltypes = new object[oParameter.Columns.Count];
                int i = 0;
                foreach (Grid.Xml.Column col in oParameter.Columns)
                {
                    switch (col.ColumnType)
                    {
                        case Grid.Xml.DataType.DateTime:
                        case Grid.Xml.DataType.Numeric:
                            xltypes[i] = new object[2] { i + 1, XlColumnDataType.xlGeneralFormat};
                            break;
                        default:
                            xltypes[i] = new object[2] { i + 1, XlColumnDataType.xlTextFormat };
                            break;
                    }
                    i++;
                }                        
                
                shit1.Paste();
                xlapp.Visible = true;

                renc = shit1.Cells.Columns[1] as Range;
                renc.TextToColumns(renc, XlTextParsingType.xlDelimited,
                    XlTextQualifier.xlTextQualifierDoubleQuote, false, false, false, true, false, false, ""
                    ,xltypes, ",", ".");

                File.Delete(tempFileName);

                
                // Format it
                //
                var columns = shit1.Cells.Rows[1] as Range;
                columns.Font.Bold = true;
                columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                i = 0;
                foreach (Grid.Xml.Column col in oParameter.Columns)
                {
                    i++;
                    renc = shit1.Cells.Columns[i] as Range;

                    Trace.WriteLine("FORMAT " + col.Name + " " + col.FormatString);
                    switch (col.FormatString)
                    { 
                        case "n;0":
                            renc.NumberFormat = "0";
                            break;
                        case "n;1":
                            renc.NumberFormat = "0.0";
                            break;
                        case "n;2":
                            renc.NumberFormat = "0.00";
                            break;
                        case "d":
                            renc.NumberFormat = "d/m/yyyy";
                            break;

                    }

                    renc.AutoFit();
                }

                
                Trace.WriteLine("TO-EXCEL FORMAT DONE " + DateTime.Now);
                if (exitApp)
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                MessageBox.Show(e.Message, "HATA");
            }
        }

        internal static void FillExcelWithData2(Grid.Xml.Parameter oParameter, Data.Database oDatabase, bool exitApp)
        {
            try
            {
                //string tempFileName = "c:\\always\\savas.txt";
                string tempFileName = @"c:\always\tmpCD74.txt";
                string alltext = File.ReadAllText(tempFileName, Encoding.Unicode);
                Clipboard.SetText(alltext, TextDataFormat.UnicodeText);
                
                var xlapp = new Microsoft.Office.Interop.Excel.Application();
                xlapp.Visible = true;
                var wkbk1 = xlapp.Workbooks.Add();
                //var shit1 = wkbk1.Sheets.Add() as Microsoft.Office.Interop.Excel.Worksheet;
                Microsoft.Office.Interop.Excel.Worksheet shit1;
                if (wkbk1.Sheets.Count < 1)
                {
                    shit1 = wkbk1.Sheets.Add() as Microsoft.Office.Interop.Excel.Worksheet;
                }
                else
                {
                    shit1 = wkbk1.Sheets.Item[1] as Microsoft.Office.Interop.Excel.Worksheet;
                }
                
                
                
                var types = new object[3];
                types[0] = new object[2] { 8, XlColumnDataType.xlGeneralFormat};
                types[1] = new object[2] { 2, XlColumnDataType.xlDMYFormat};
                types[2] = new object[2] { 3, XlColumnDataType.xlGeneralFormat};

                shit1.Paste();

                var renc = shit1.Cells.Columns[1] as Range;
                renc.TextToColumns(renc, XlTextParsingType.xlDelimited,
                    XlTextQualifier.xlTextQualifierDoubleQuote, false, false, false, true, false, false, "",
                    types, ",", ".");

                renc = shit1.Cells.Columns[8] as Range;
                renc.NumberFormat = "0.0";
                
                Trace.WriteLine("TO-EXCEL FORMAT DONE " + DateTime.Now);
                if (exitApp)
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                MessageBox.Show(e.Message, "HATA");
            }
        }

        private static void WriteLineToCSV(FileStream csf, string line)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(line + "\r\n");
            csf.Write(bytes, 0, bytes.Length);
            //byte[] bytes = Grid.Utils.GetStringBytes(line + "\n");
            //csf.Write(bytes, 0, bytes.Length);
        }

        private static void WriteColumnsToCSV(Grid.Xml.Parameter oParameter, FileStream csf)
        {
            string line = "";
            foreach (Grid.Xml.Column col in oParameter.Columns)
            {
                line += "\"" + col.Caption + "\",";
            }
            WriteLineToCSV(csf, line);
        }

        private static SqlCommand GetData(Data.Database oDatabase, string sql)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            conn = Data.Database.SqlConnection(oDatabase.ServerName, oDatabase.DbName, oDatabase.DbUserName, oDatabase.DbPassword);
            conn.Open();

            if (!string.IsNullOrEmpty(oDatabase.ContextInfo))
            {
                string sql2 = "DECLARE @BinVar binary(128);" +
                  "SET @BinVar = cast(" + oDatabase.ContextInfo + " as binary(128));" +
                  "SET context_info @BinVar";

                cmd = new SqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();
            }

            cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 9999; //0 = no limit
            return cmd;
        }
    }
}
