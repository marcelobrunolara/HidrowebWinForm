using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using static HidrowebWin.Forms.Data.HIDRODataSet;

namespace HidrowebWin.Forms.ExcelManager
{
    public class ExcelInteropHelper
    {

        static string[] _nomeAbasPluviometrico = {"Estação","Chuva","Diária", "Resumo mês", "Resumo dias", "Resumo dias chuva", "Resumo dias falha" };

        public static _Workbook CriarNovaPlanilha(string filename, TipoPlanilha tipoPlanilha)
        {

            // creating Excel Application
            _Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application
            _Workbook workbook = app.Workbooks.Add(Type.Missing);

            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;


            xlSheets = workbook.Sheets as Sheets;

            // see the excel sheet behind the program
            app.Visible = true;

            for (int i=0; i<_nomeAbasPluviometrico.Length; i++)
            {
                try
                {
                    worksheet = workbook.Worksheets[i + 1];
                }
                catch { }
                finally
                {
                    if (worksheet == null)
                        worksheet = (Worksheet)xlSheets.Add(Type.Missing, xlSheets[xlSheets.Count], Type.Missing, Type.Missing);

                    worksheet.Name = _nomeAbasPluviometrico[i];
                    worksheet = null;
                }
            }
            return workbook;
        }

        public static _Workbook CriarAbaEstacao(_Workbook workbook, EstacaoRow estacaoRow)
        {
            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[1];

            worksheet.Cells[2, "B"] = "Código";
            worksheet.Cells[3, "B"] = "Nome";
            worksheet.Cells[4, "B"] = "Código adicional";
            worksheet.Cells[5, "B"] = "Bacia";
            worksheet.Cells[6, "B"] = "Sub-bacia";
            worksheet.Cells[7, "B"] = "Rio";
            worksheet.Cells[8, "B"] = "Estado";
            worksheet.Cells[9, "B"] = "Município";
            worksheet.Cells[10, "B"] = "Responsável";
            worksheet.Cells[11, "B"] = "Operadora";
            worksheet.Cells[12, "B"] = "Latitude";
            worksheet.Cells[13, "B"] = "Longitude";
            worksheet.Cells[14, "B"] = "Altitude";
            worksheet.Cells[15, "B"] = "Area de drenagem (KM²)";
            worksheet.Cells[16, "B"] = "Data de geração da planilha";
            worksheet.Cells[17, "B"] = "Disponibilidade de dados";

            ((Range)worksheet.Range[worksheet.Cells[2,"B"], worksheet.Cells[16, "B"]]).ColumnWidth=25;
            ((Range)worksheet.Range[worksheet.Cells[2, "B"], worksheet.Cells[16, "B"]]).VerticalAlignment = XlHAlign.xlHAlignCenter;

            return workbook;
        }

        #region [Abas pluviométrico]

        #endregion
    }
}
