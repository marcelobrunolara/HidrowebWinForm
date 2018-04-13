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

        public static _Workbook CriarNovaPlanilhaPluviometrico(string filename)
        {

            // creating Excel Application
            _Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            _Workbook workbook = app.Workbooks.Open(path + "/template_plu.xlsx");

            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;


            xlSheets = workbook.Sheets as Sheets;

            // see the excel sheet behind the program
            app.Visible = true;

            return workbook;
        }

        #region [Aba Estação]
        public static _Workbook CriarAbaEstacao(_Workbook workbook, EstacaoData estacao)
        {
            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[1];

            object[,] dadosEstacao = new object[16, 1];

            dadosEstacao[0, 0] = estacao.Codigo.ToString();
            dadosEstacao[1, 0] = estacao.Nome;
            dadosEstacao[2, 0] = estacao.CodigoAdicional;
            dadosEstacao[3, 0] = estacao.NomeBacia;
            dadosEstacao[4, 0] = estacao.NomeSubBacia;
            dadosEstacao[5, 0] = estacao.NomeRio;
            dadosEstacao[6, 0] = estacao.Estado;
            dadosEstacao[7, 0] = estacao.Municipio;
            dadosEstacao[8, 0] = estacao.Responsavel;
            dadosEstacao[9, 0] = estacao.Operadora;
            dadosEstacao[10, 0] = estacao.Latitude;
            dadosEstacao[11, 0] = estacao.Longitude;
            dadosEstacao[12, 0] = estacao.Altitude;
            dadosEstacao[13, 0] = estacao.AreaDrenagem;
            dadosEstacao[14, 0] = DateTime.Now;
            dadosEstacao[15, 0] = $"De {estacao.Inicio.Date} a {estacao.Fim.Date}";


            Range range = worksheet.Cells[2, 3];
            range = range.Resize[16, 1];

            range.Value = dadosEstacao;

            return workbook;
        }
        #endregion

        #region [Aba Chuvas]
        public static _Workbook CriarAbaChuvas(_Workbook workbook, IList<SerieHistorica> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            SerieHistorica linhaEstacao;
            DateTime dataIt = estacao.Inicio;

            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[2];

            int monthQuantity = (((estacao.Fim.Year - estacao.Inicio.Year) * 12) + estacao.Fim.Month - estacao.Inicio.Month)+1;

            //array de dados
            object[,] dados = new object[monthQuantity, 37];
            int i = 0;
            while (dataIt <= estacao.Fim)//Cria todas as linhas até a data fim.
            {
                //Tenta buscar dados consistidos, senão busca os dados Brutos
                linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                if (linhaEstacao == null)
                    linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                dados[i, 1] = dataIt.Date;

                if (linhaEstacao == null) //linha de dados não existente na base da Ana
                {
                    dados[i, 0] = "1";
                    dados[i, 34] = "Não";
                    dados[i, 35] = "i";
                }
                else
                {

                    for (int j = 0; j < 31; j++)
                    {
                        dados[i, 2 + j] = linhaEstacao.ChuvasArray[j + 1];
                    }
                    dados[i, 0] = linhaEstacao.NivelConsistencia;
                    dados[i, 33] = linhaEstacao.Maxima;
                    dados[i, 34] = linhaEstacao.NivelConsistencia == "2" ? "Sim" : "Não";
                    dados[i, 35] = linhaEstacao.NivelConsistencia == "2" ? "n" : "b";
                    dados[i, 36] = Convert.ToInt32(linhaEstacao.MaximaStatus);
                }

                i++;
                dataIt = dataIt.AddMonths(1);
            }

            Range range = worksheet.Cells[2, 1];
            range = range.Resize[monthQuantity, 37];

            range.Value = dados;

            return workbook;
        }

        #endregion

        #region [Aba Diária]
        public static _Workbook CriarAbaDiaria(_Workbook workbook, IList<SerieHistorica> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[3];
            SerieHistorica linhaEstacao;
            DateTime dataIt = estacao.Inicio;

            long daysQuantity = Convert.ToInt64((estacao.Fim - estacao.Inicio).TotalDays) + DateTime.DaysInMonth(estacao.Fim.Year, estacao.Fim.Month);

            object[,] dados = new object[daysQuantity, 3];

            int i = 0;
            int ultimaLinha = 0;

            while (dataIt <= estacao.Fim)//Cria todas as linhas até a data fim.
            {

                //Tenta buscar dados consistidos, senão busca os dados Brutos
                linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                if (linhaEstacao == null)
                    linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                var totalDias = DateTime.DaysInMonth(dataIt.Year, dataIt.Month);

                for (int j = 0; j < totalDias; j++)
                {
                    dados[ultimaLinha, 0] = dataIt.Date;
                    if (linhaEstacao == null)
                    {
                        dados[ultimaLinha, 2] = "i";
                    }
                    else
                    {
                        dados[ultimaLinha, 1] = linhaEstacao.ChuvasArray[j + 1];
                        dados[ultimaLinha, 2] = linhaEstacao.NivelConsistencia;
                    }
                    ultimaLinha++;
                    dataIt = dataIt.AddDays(1);
                }
            }

            Range range = worksheet.Cells[3, 2];
            range = range.Resize[ultimaLinha, 3];

            range.Value = dados;

            return workbook;
        }
        #endregion

        #region Aba Resumo Mes

        public static _Workbook CriarAbaResumo(_Workbook workbook, IList<SerieHistorica> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[4];
            SerieHistorica linhaEstacao = new SerieHistorica();

            DateTime dataIt = new DateTime(estacao.Inicio.Year,1, 1);

            long yearsQuantity = estacao.Fim.Year - estacao.Inicio.Year+1; //Adiciona ano final 

            object[,] dados = new object[yearsQuantity, 28];

            int i = 0;
            while (dataIt <= estacao.Fim)//Cria todas as linhas até a data fim.
            {
                dados[i, 0] = dataIt.Date.Year;

                for (int j=1; j<=12; j++)
                {
                    //Tenta buscar dados consistidos, senão busca os dados Brutos
                    linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                    if (linhaEstacao == null)
                        linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                    if(linhaEstacao == null)
                    {
                        dados[i, 14] = "a";
                        dados[i, j+14] = "i";
                    }
                    else
                    {
                        dados[i, j] = linhaEstacao.Total;
                        dados[i, j + 14] = string.IsNullOrWhiteSpace(linhaEstacao.TotalStatus)?"b": linhaEstacao.TotalStatus;
                        dados[i, 14] = linhaEstacao.NivelConsistencia!="2"?"a":string.Empty;
                        dados[i, 13] = linhaEstacao.TotalAnual;
                        dados[i, 27] = linhaEstacao.TotalAnualStatus;
                    }
                    dataIt = dataIt.AddMonths(1);
                }

                i++;
            }

            Range range = worksheet.Cells[3, 2];
            range = range.Resize[yearsQuantity, 28];
            range.Value = dados;

            Range range2 = worksheet.Cells[3, 2];
            range = range.Resize[yearsQuantity, 14];
            range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous; 
            return workbook;
        }

        #endregion



    }
}
