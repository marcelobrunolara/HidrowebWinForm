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

        #region Aba resumo dia

        public static _Workbook CriarAbaResumoDia(_Workbook workbook, IList<SerieHistorica> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[5];

            DateTime dataIt = estacao.Inicio;

            int linhasRange = 32;
            int anoIteracao = 0;
            int linhaInicio = 3;

            SerieHistorica linhaEstacao = new SerieHistorica();

            while (dataIt <= estacao.Fim)//Cria todas as linhas até a data fim.
            {
                object[,] dados = new object[32, 25];

                for (int i = 0; i <= 12; i++) //iteração por mes
                {
                    var diasNoMes = DateTime.DaysInMonth(dataIt.Year, dataIt.Month); // Dias no mes mais linha referente ao ano.

                    linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                    if (linhaEstacao == null)
                        linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                    //preenche a coluna
                    for (int index = 0; index <= 31; index++) // iteracao por dia
                    {
                        if (i == 0 && index == 0) //Escreve o Ano
                            dados[index, i] = dataIt.Year;
                        if (i != 0 && index == 0) //Escreve o "-"
                            dados[index, i] = "-";
                        if (i == 0 && index != 0) //Escreve o dia do mes
                            dados[index, i] = index;

                        if (i != 0 && index != 0) // Escreve dados da chuva para o dia
                        {
                            if (linhaEstacao == null) // nao existe dados para este mes
                                dados[index, i + 12] = "i";
                            else if (string.IsNullOrEmpty(linhaEstacao.ChuvasArray[index]) && index > diasNoMes)
                                dados[index, i] = "-";
                            else if (string.IsNullOrEmpty(linhaEstacao.ChuvasArray[index]))
                                dados[index, i + 12] = "b";
                            else
                                dados[index, i] = linhaEstacao.ChuvasArray[index];
                        }

                    }
                    if(i!=0)
                        dataIt = dataIt.AddMonths(1);
                }

                linhaInicio = 3 + (linhasRange * anoIteracao);

                //Imprime os dados
                Range range = worksheet.Cells[linhaInicio, 2];
                range = range.Resize[linhasRange, 25];
                range.Value = dados;

                //Desenha as linhas de borda
                range = range.Resize[linhasRange, 13];
                range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

                anoIteracao++;
            }
            return workbook;
        }

        #endregion

        #region Aba resumo dias chuva
        public static _Workbook CriarAbaResumoDiasChuva(_Workbook workbook, IList<SerieHistorica> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[6];

            DateTime dataIt = estacao.Inicio;
            SerieHistorica linhaEstacao = new SerieHistorica();

            int totalAnos = estacao.Fim.Year - dataIt.Year; // Dias no mes mais linha referente ao ano.
            int somatorioDias;
            bool linhaInvalida;

            object[,] dados = new object[totalAnos+2, 27];

            int totalLinhas = totalAnos + 2;

            for (int linha=0; linha< totalLinhas-1; linha++ )
            {
                somatorioDias = 0;
                linhaInvalida = false;

                for (int coluna=0; coluna<14; coluna++)
                {
                    //Somatorio Total
                    if (coluna == 13)
                    {
                        dados[linha, coluna] = linhaInvalida ? string.Empty : somatorioDias.ToString();
                        dados[linha, coluna + 13] = linhaInvalida ? "i" : string.Empty;
                    }
                    else
                    {

                        linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                        if (linhaEstacao == null)
                            linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt);

                        if (coluna == 0)
                        {
                            dados[linha, coluna] = dataIt.Year;
                        }
                        else if (linhaEstacao == null)
                        {
                            dados[linha, coluna + 13] = "i";
                            linhaInvalida = true;
                        }
                        else if (string.IsNullOrEmpty(linhaEstacao.NumDiasDeChuva) && string.IsNullOrEmpty(linhaEstacao.Total))
                        {
                            dados[linha, coluna + 13] = "a";
                            linhaInvalida = true;
                        }
                        else if (string.IsNullOrEmpty(linhaEstacao.NumDiasDeChuva))
                        {
                            dados[linha, coluna + 13] = "b";
                            linhaInvalida = true;
                        }
                        else
                        {
                            if(linhaEstacao.NivelConsistencia=="1")
                                dados[linha, coluna + 13] = "fa";

                            dados[linha, coluna] = linhaEstacao.NumDiasDeChuva;
                            somatorioDias += Convert.ToInt32(linhaEstacao.NumDiasDeChuva);
                        }
                        if (coluna != 0)
                            dataIt = dataIt.AddMonths(1);
                    }
                }
            }

            dados[totalLinhas-1, 0] = "Médias";

            for(int linhaMedia =1; linhaMedia <= 13; linhaMedia++)
            {
                int valor = 0;
                int quantidadeValores = 0;
                double somatorio = 0.0;
                double media = 0.0;

                for (int colunaSomatorio=0; colunaSomatorio< totalLinhas-1; colunaSomatorio++)
                {
                    valor = !(dados[colunaSomatorio, linhaMedia] == string.Empty) ? Convert.ToInt32(dados[colunaSomatorio, linhaMedia]) : 0;
                    somatorio += valor;
                    if(valor!=0)
                        quantidadeValores++;
                }
                media = somatorio / quantidadeValores;
                dados[totalLinhas -1, linhaMedia] = media;
            }


            //Imprime os dados
            Range range = worksheet.Cells[3, 2];
            range = range.Resize[totalLinhas, 27];
            range.Value = dados;

            range = worksheet.Cells[3, 2];
            range = range.Resize[totalLinhas, 14];
            range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;


            return workbook;
        }

        #endregion

        #region Aba resumo dias chuva
        public static _Workbook CriarAbaResumoDiasFalha(_Workbook workbook, IList<SerieHistorica> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[7];

            DateTime dataIt = estacao.Inicio;
            SerieHistorica linhaEstacao = new SerieHistorica();

            int totalAnos = estacao.Fim.Year - dataIt.Year; // Dias no mes mais linha referente ao ano.
            int somatorioDias;

            object[,] dados = new object[totalAnos + 2, 27];

            int totalLinhas = totalAnos + 2;

            for (int linha = 0; linha < totalLinhas - 1; linha++)
            {
                somatorioDias = 0;

                for (int coluna = 0; coluna < 14; coluna++)
                {
                    //Somatorio Total
                    if (coluna == 13)
                    {
                        dados[linha, coluna] = somatorioDias;
                    }
                    else
                    {
                        int diasMes = DateTime.DaysInMonth(dataIt.Year, dataIt.Month);

                        linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                        if (linhaEstacao == null)
                            linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt);

                        if (coluna == 0)
                        {
                            dados[linha, coluna] = dataIt.Year;
                        }
                        else if (linhaEstacao == null)
                        {
                            dados[linha, coluna] = diasMes;
                            dados[linha, coluna + 13] = "i";
                        }
                        else 
                        {
                            int diasFalha = linhaEstacao.StatusChuvasArray.Where(c => c == "0").Count();
                            int diasChuva = linhaEstacao.StatusChuvasArray.Where(c => c == "1").Count();

                            if (diasMes == diasChuva)
                                dados[linha, coluna] = 0;
                            else if (diasMes == diasFalha)
                            {
                                dados[linha, coluna] = diasFalha;
                                dados[linha, coluna + 13] = "i";
                            }
                            else if(diasMes!=diasFalha)
                            {
                                if (diasMes > diasFalha)
                                {
                                    dados[linha, coluna] = diasMes - diasChuva;
                                    dados[linha, coluna + 13] = "a";
                                }else
                                {
                                    dados[linha, coluna] = diasMes;
                                    dados[linha, coluna + 13] = "i";
                                }
                          }

                            somatorioDias += diasFalha;
                        }
                        if (coluna != 0)
                            dataIt = dataIt.AddMonths(1);
                    }
                }
            }

            dados[totalLinhas - 1, 0] = "Médias";

            for (int linhaMedia = 1; linhaMedia <= 13; linhaMedia++)
            {
                int valor = 0;
                int quantidadeValores = 0;
                double somatorio = 0.0;
                double media = 0.0;

                for (int colunaSomatorio = 0; colunaSomatorio < totalLinhas - 1; colunaSomatorio++)
                {
                    valor = !(dados[colunaSomatorio, linhaMedia] == string.Empty) ? Convert.ToInt32(dados[colunaSomatorio, linhaMedia]) : 0;
                    somatorio += valor;
                    if (valor != 0)
                        quantidadeValores++;
                }
                media = somatorio / quantidadeValores;
                dados[totalLinhas - 1, linhaMedia] = media;
            }


            //Imprime os dados
            Range range = worksheet.Cells[3, 2];
            range = range.Resize[totalLinhas, 27];
            range.Value = dados;

            range = worksheet.Cells[3, 2];
            range = range.Resize[totalLinhas, 14];
            range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;


            return workbook;
        }

        #endregion

    }
}
