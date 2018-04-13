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

            object[,] dadosEstacao = new object[16,1];

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
            dadosEstacao[15, 0] = $"De {estacao.Inicio.ToShortDateString()} a {estacao.Fim.ToShortDateString()}";


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

            int monthQuantity = ((estacao.Fim.Year - estacao.Inicio.Year) * 12) + estacao.Fim.Month - estacao.Inicio.Month;

            //array de dados
            object[,] dados = new object[monthQuantity, 37];
            int i = 0;
            while (dataIt <= estacao.Fim)//Cria todas as linhas até a data fim.
            {
                //Tenta buscar dados consistidos, senão busca os dados Brutos
                linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia=="2");
                if (linhaEstacao == null)
                    serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                dados[i, 1] = dataIt.ToShortDateString();

                if (linhaEstacao == null) //linha de dados não existente na base da Ana
                {
                    dados[i, 0] = "1";
                    dados[i, 34] = "Não";
                    dados[i, 35] = "i";
                    break;
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

                if (dataIt == estacao.Fim)
                {
                    string yes = "yes it is";
                }
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

            return workbook;
        }
        #endregion

    }
}
