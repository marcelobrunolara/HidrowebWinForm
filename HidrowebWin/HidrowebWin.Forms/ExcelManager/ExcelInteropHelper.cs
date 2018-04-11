using HidrowebWin.Forms.Data.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using DataTables = System.Data.DataTable;

namespace HidrowebWin.Forms.ExcelManager
{
    public class ExcelInteropHelper
    {
        #region [Abas pluviométrico]

        static string[] _nomeAbasPluviometrico = { "Estação", "Chuva", "Diária", "Resumo mês", "Resumo dias", "Resumo dias chuva", "Resumo dias falha" };

        public static _Workbook CriarNovaPlanilhaPluviometrico(string filename)
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

            for (int i = 0; i < _nomeAbasPluviometrico.Length; i++)
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

        public static _Workbook CriarAbaEstacao(_Workbook workbook, EstacaoData estacao)
        {
            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[1];

            #region [Dados da estação]
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

            worksheet.Cells[2, "C"] = estacao.Codigo.ToString();
            worksheet.Cells[3, "C"] = estacao.Nome;
            worksheet.Cells[4, "C"] = estacao.CodigoAdicional;
            worksheet.Cells[5, "C"] = estacao.NomeBacia;
            worksheet.Cells[6, "C"] = estacao.NomeSubBacia;
            worksheet.Cells[7, "C"] = estacao.NomeRio;
            worksheet.Cells[8, "C"] = estacao.Estado;
            worksheet.Cells[9, "C"] = estacao.Municipio;
            worksheet.Cells[10, "C"] = estacao.Responsavel;
            worksheet.Cells[11, "C"] = estacao.Operadora;
            worksheet.Cells[12, "C"] = estacao.Latitude;
            worksheet.Cells[13, "C"] = estacao.Longitude;
            worksheet.Cells[14, "C"] = estacao.Altitude;
            worksheet.Cells[15, "C"] = estacao.AreaDrenagem;
            worksheet.Cells[16, "C"] = DateTime.Now;
            worksheet.Cells[17, "C"] = $"De {estacao.Inicio.ToShortDateString()} a {estacao.Fim.ToShortDateString()}";

            ((Range)worksheet.Range[worksheet.Cells[2, "B"], worksheet.Cells[17, "C"]]).ColumnWidth = 25;
            ((Range)worksheet.Range[worksheet.Cells[2, "B"], worksheet.Cells[17, "C"]]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)worksheet.Range[worksheet.Cells[2, "B"], worksheet.Cells[17, "C"]]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            #endregion

            #region Legendas

            worksheet.Cells[2, "F"] = "Legenda";
            ((Range)worksheet.Cells[2, "F"]).Font.Bold = true;

            worksheet.Cells[3, "F"] = "Planilha Estação";
            ((Range)worksheet.Cells[3, "F"]).Font.Bold = true;

            worksheet.Cells[4, "F"] = "Fonte Preta";
            worksheet.Cells[4, "G"] = "Dados Não Fornecidos pela Ana";


            worksheet.Cells[5, "F"] = "Fonte Azul";
            ((Range)worksheet.Cells[5, "F"]).Font.Color = XlRgbColor.rgbBlue;
            worksheet.Cells[5, "G"] = @"Meses não existentes na parte de dados consistidos da ANA. 
                                        Valores preenchidos com dados não consistidos ou em branco, caso não exista.
                                        Falhas dentro do intervalo de dados consistidos não serão preenchidas com dados brutos.";
            ((Range)worksheet.Range[worksheet.Cells[5, "G"], worksheet.Cells[5, "N"]]).Merge();
            ((Range)worksheet.Rows[5]).RowHeight = 15;

            worksheet.Cells[8, "F"] = "Planilhas de Resumo";
            ((Range)worksheet.Cells[8, "F"]).Font.Bold = true;

            worksheet.Cells[9, "F"] = "Cor da Célula";
            ((Range)worksheet.Cells[9, "F"]).Interior.Color = XlRgbColor.rgbRed;
            worksheet.Cells[9, "G"] = "Informação não contida nos dados da ANA";

            worksheet.Cells[10, "F"] = "Cor da Célula";
            ((Range)worksheet.Cells[10, "F"]).Interior.Color = XlRgbColor.rgbWhite;
            worksheet.Cells[10, "G"] = "Meses com dados incompletos (falhas)";

            worksheet.Cells[11, "F"] = "Cor da Célula";
            ((Range)worksheet.Cells[11, "F"]).Interior.Color = XlRgbColor.rgbOrange;
            worksheet.Cells[11, "G"] = "Em branco - ANA";

            worksheet.Cells[12, "F"] = "Cor da Fonte";
            ((Range)worksheet.Cells[12, "F"]).Font.Color = XlRgbColor.rgbRed;
            worksheet.Cells[12, "G"] = "Estimado - ANA";

            worksheet.Cells[13, "F"] = "Cor da Fonte";
            ((Range)worksheet.Cells[13, "F"]).Font.Color = XlRgbColor.rgbPink;
            worksheet.Cells[13, "G"] = "Duvidoso - ANA";

            worksheet.Cells[14, "F"] = "Cor da Fonte";
            ((Range)worksheet.Cells[14, "F"]).Interior.Color = XlRgbColor.rgbDarkGreen;
            worksheet.Cells[14, "G"] = "Acumulado - ANA";

            ((Range)worksheet.Columns["F"]).ColumnWidth = 20;
            ((Range)worksheet.Range[worksheet.Cells[2, "F"], worksheet.Cells[14, "F"]]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)worksheet.Range[worksheet.Cells[2, "F"], worksheet.Cells[17, "F"]]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;


            #endregion

            return workbook;
        }

        public static _Workbook CriarAbaChuvas(_Workbook workbook, IList<SerieHistorica> serieHistorica, EstacaoData estacao)
        {

            _Worksheet worksheet = workbook.Worksheets[2];

            //Criando primeira linha (títulos)
            worksheet.Cells[1, 2] = "Data";
            for (int i=1; i<=31; i++)
            {
                worksheet.Cells[1, 2+i] = "Chuva"+i.ToString("D2");
            }
            worksheet.Cells[1, 34] = "Máxima";
            worksheet.Cells[1, 35] = "Consistido";

            ((Range)worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 35]]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 35]]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            ((Range)worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 35]]).Font.Bold = true;

            var dataIt = estacao.Inicio;
            int lineIndex = 2;

            while (dataIt != estacao.Fim)//Cria todas as linhas até a data fim.
            {
                SerieHistorica linhaDados;

                linhaDados = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia=="2"); //Buscar a priori, dados consistentes

                if (linhaDados == null) 
                    linhaDados = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1"); //Buscar por dados brutos

                if (linhaDados != null)
                {
                    var consistencia = linhaDados.NivelConsistencia == "2";
                    MontarLinhaDadosChuvaDiaria(workbook, linhaDados, consistencia, lineIndex);
                }
                else 
                    CriarLinhaInexistenteAbaChuvas(workbook,lineIndex, dataIt.ToShortDateString());

                dataIt = dataIt.AddMonths(1);
                lineIndex++;
            }

            return workbook;
        }

        private static void CriarLinhaInexistenteAbaChuvas(_Workbook workbook, int lineIndex, string dataString)
        {
            _Worksheet worksheet = workbook.Worksheets[2];
            //Status consistencia
            worksheet.Cells[lineIndex, 1] = "1";
            worksheet.Cells[lineIndex, 2] = dataString;
            for(int i=1; i<=31; i++)
            {
                ((Range)worksheet.Cells[lineIndex, 2 + i]).Interior.Color = XlRgbColor.rgbRed;
            }
            worksheet.Cells[lineIndex, 35] = "Não";

            ((Range)worksheet.Range[worksheet.Cells[lineIndex, 1], worksheet.Cells[lineIndex, 35]]).Font.Color = XlRgbColor.rgbBlue;
            ((Range)worksheet.Range[worksheet.Cells[lineIndex, 1], worksheet.Cells[lineIndex, 35]]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)worksheet.Range[worksheet.Cells[lineIndex, 1], worksheet.Cells[lineIndex, 35]]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
        }

        private static void MontarLinhaDadosChuvaDiaria(_Workbook workbook, SerieHistorica linhaDados, bool valid, int lineIndex)
        {
            _Worksheet worksheet = workbook.Worksheets[2];

            //Status consistencia
            worksheet.Cells[lineIndex, 1] = linhaDados.NivelConsistencia;
            //Data chuva
            worksheet.Cells[lineIndex, 2] = linhaDados.Data.ToShortDateString(); ;
            ((Range)worksheet.Cells[lineIndex,2]).Font.Bold = true;
            ((Range)worksheet.Cells[lineIndex, 2]).ColumnWidth = 15;

            int i = 1;
            while (i <= 31) { 
                try
                {

                    PropertyInfo propertyInfo = linhaDados.GetType().GetProperty("Chuva"+i.ToString("D2"));
                    string medidaChuva = propertyInfo.GetValue(linhaDados).ToString();

                    worksheet.Cells[lineIndex, 2 + i] = medidaChuva;

                    if (string.IsNullOrEmpty(medidaChuva) && valid)
                        ((Range)worksheet.Cells[lineIndex, 2 + i]).Interior.Color = XlRgbColor.rgbOrange;//Se os dados estiverem em branco
                        
                    i++;
                }
                catch
                {
                }
            }

            //Status consistencia
            worksheet.Cells[lineIndex, 1] = linhaDados.NivelConsistencia;
            //Data chuva
            worksheet.Cells[lineIndex, 2] = linhaDados.Data.ToShortDateString();

            //Maxima
            worksheet.Cells[lineIndex,34] = linhaDados.Maxima;
            switch (linhaDados.MaximaStatus)
            {
                case "1":  break; //Estimado
                case "2": ((Range)worksheet.Cells[lineIndex, 34]).Font.Color = XlRgbColor.rgbRed; break; //Estimado
                case "3": ((Range)worksheet.Cells[lineIndex, 34]).Font.Color = XlRgbColor.rgbPink; break; //Duvidoso
                case "4": ((Range)worksheet.Cells[lineIndex, 34]).Interior.Color = XlRgbColor.rgbDarkGreen; break; //Acumulado
                default: ((Range)worksheet.Cells[lineIndex, 34]).Interior.Color = XlRgbColor.rgbOrange; break; //Em branco
            }

            //Consistido

            worksheet.Cells[lineIndex, 35] = valid ? "Sim" : "Não";
            ((Range)worksheet.Cells[lineIndex, 35]).Font.Color = valid ? XlRgbColor.rgbBlack : XlRgbColor.rgbBlue;

            if (!valid) //dados brutos ou invalidos
                ((Range)worksheet.Range[worksheet.Cells[lineIndex, 1], worksheet.Cells[lineIndex, 33]]).Font.Color = XlRgbColor.rgbBlue;

            ((Range)worksheet.Range[worksheet.Cells[lineIndex, 1], worksheet.Cells[lineIndex, 35]]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)worksheet.Range[worksheet.Cells[lineIndex, 1], worksheet.Cells[lineIndex, 35]]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
        }

        #endregion
    }
}
