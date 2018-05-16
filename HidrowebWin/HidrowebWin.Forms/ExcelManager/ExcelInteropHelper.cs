using HidrowebWin.Forms.Data.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using DataTables = System.Data.DataTable;

namespace HidrowebWin.Forms.ExcelManager
{
    public class ExcelInteropHelper
    {
        private static Application app;

        #region Pluviométrico
        public static _Workbook CriarNovaPlanilhaPluviometrico(string filename)
        {

            // creating Excel Application
            app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application

            _Workbook workbook = app.Workbooks.Open(Environment.CurrentDirectory + "/template_plu.xlsx");

            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;


            xlSheets = workbook.Sheets as Sheets;

            // see the excel sheet behind the program
            app.Visible = false;

            return workbook;
        }

        #region [Aba Estação]
        public static _Workbook CriarAbaEstacao(_Workbook workbook, IList<SerieHistoricaChuvas> serieHistorica, EstacaoData estacao)
        {
            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[1];

            DateTime dataIt = estacao.Inicio.HasValue ? estacao.Inicio.Value : serieHistorica.OrderBy(c => c.Data).First().Data;
            DateTime dataFim = estacao.Fim.HasValue ? estacao.Fim.Value : serieHistorica.OrderByDescending(c => c.Data).First().Data;

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
            dadosEstacao[15, 0] = string.Format("De {0} a {1}", new object[] { estacao.Inicio.HasValue?estacao.Inicio.Value.ToString("dd/MM/yyyy"):dataIt.ToString("dd/MM/yyyy")
                                                                ,estacao.Fim.HasValue?estacao.Fim.Value.ToString("dd/MM/yyyy"):dataFim.ToString("dd/MM/yyyy")});


            Range range = worksheet.Cells[2, 3];
            range = range.Resize[16, 1];

            range.Value = dadosEstacao;

            return workbook;
        }
        #endregion

        #region [Aba Chuvas]
        public static _Workbook CriarAbaChuvas(_Workbook workbook, IList<SerieHistoricaChuvas> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            SerieHistoricaChuvas linhaEstacao;

            DateTime dataInicio = estacao.Inicio.HasValue ? estacao.Inicio.Value : serieHistorica.OrderBy(c => c.Data).First().Data;
            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);
            DateTime dataFim = estacao.Fim.HasValue ? estacao.Fim.Value : serieHistorica.OrderByDescending(c => c.Data).First().Data;

            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[2];

            int monthQuantity = (((dataFim.Year - dataIt.Year) * 12) + dataFim.Month - dataIt.Month) + 1;

            //array de dados
            object[,] dados = new object[monthQuantity, 37];
            int i = 0;
            while (dataIt <= dataFim)//Cria todas as linhas até a data fim.
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
        public static _Workbook CriarAbaDiaria(_Workbook workbook, IList<SerieHistoricaChuvas> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[3];
            SerieHistoricaChuvas linhaEstacao;

            DateTime dataInicio = estacao.Inicio.HasValue ? estacao.Inicio.Value : serieHistorica.OrderBy(c => c.Data).First().Data;
            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);
            DateTime dataFim = estacao.Fim.HasValue ? estacao.Fim.Value : serieHistorica.OrderByDescending(c => c.Data).First().Data;

            long daysQuantity = Convert.ToInt64((dataFim - dataIt).TotalDays) + DateTime.DaysInMonth(dataFim.Year, dataFim.Month);

            object[,] dados = new object[daysQuantity, 3];

            int i = 0;
            int ultimaLinha = 0;

            while (dataIt <= dataFim)//Cria todas as linhas até a data fim.
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

        public static _Workbook CriarAbaResumo(_Workbook workbook, IList<SerieHistoricaChuvas> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[4];
            SerieHistoricaChuvas linhaEstacao = new SerieHistoricaChuvas();

            DateTime dataInicio = estacao.Inicio.HasValue ? estacao.Inicio.Value : serieHistorica.OrderBy(c => c.Data).First().Data;
            DateTime dataFim = estacao.Fim.HasValue ? estacao.Fim.Value : serieHistorica.OrderByDescending(c => c.Data).First().Data;

            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);

            long yearsQuantity = dataFim.Year - dataInicio.Year + 1; //Adiciona ano final 

            object[,] dados = new object[yearsQuantity, 28];

            int i = 0;
            while (dataIt <= dataFim)//Cria todas as linhas até a data fim.
            {
                dados[i, 0] = dataIt.Date.Year;

                for (int j = 1; j <= 12; j++)
                {
                    //Tenta buscar dados consistidos, senão busca os dados Brutos
                    linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                    if (linhaEstacao == null)
                        linhaEstacao = serieHistorica.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                    if (linhaEstacao == null)
                    {
                        dados[i, 14] = "a";
                        dados[i, j + 14] = "i";
                    }
                    else
                    {
                        dados[i, j] = linhaEstacao.Total;
                        dados[i, j + 14] = string.IsNullOrWhiteSpace(linhaEstacao.TotalStatus) ? "b" : linhaEstacao.TotalStatus;
                        dados[i, 14] = linhaEstacao.NivelConsistencia != "2" ? "a" : string.Empty;
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

        public static _Workbook CriarAbaResumoDia(_Workbook workbook, IList<SerieHistoricaChuvas> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[5];


            DateTime dataInicio = estacao.Inicio.HasValue ? estacao.Inicio.Value : serieHistorica.OrderBy(c => c.Data).First().Data;
            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);
            DateTime dataFim = estacao.Fim.HasValue ? estacao.Fim.Value : serieHistorica.OrderByDescending(c => c.Data).First().Data;

            int linhasRange = 32;
            int anoIteracao = 0;
            int linhaInicio = 3;

            SerieHistoricaChuvas linhaEstacao = new SerieHistoricaChuvas();

            while (dataIt <= dataFim)//Cria todas as linhas até a data fim.
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
                    if (i != 0)
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
        public static _Workbook CriarAbaResumoDiasChuva(_Workbook workbook, IList<SerieHistoricaChuvas> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[6];

            DateTime dataInicio = estacao.Inicio.HasValue ? estacao.Inicio.Value : serieHistorica.OrderBy(c => c.Data).First().Data;
            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);
            DateTime dataFim = estacao.Fim.HasValue ? estacao.Fim.Value : serieHistorica.OrderByDescending(c => c.Data).First().Data;

            SerieHistoricaChuvas linhaEstacao = new SerieHistoricaChuvas();

            int totalAnos = dataFim.Year - dataIt.Year; // Dias no mes mais linha referente ao ano.
            int somatorioDias;
            bool linhaInvalida;

            object[,] dados = new object[totalAnos + 2, 27];

            int totalLinhas = totalAnos + 2;

            for (int linha = 0; linha < totalLinhas - 1; linha++)
            {
                somatorioDias = 0;
                linhaInvalida = false;

                for (int coluna = 0; coluna < 14; coluna++)
                {
                    //Somatorio Total
                    if (coluna == 13)
                    {
                        dados[linha, coluna] = linhaInvalida ? string.Empty : somatorioDias.ToString();
                        dados[linha, coluna + 13] = linhaInvalida ? "i" : string.Empty;
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
                            dados[linha, coluna + 13] = "i";
                            linhaInvalida = true;
                        }
                        else
                        {
                            int diasFalha = linhaEstacao.StatusChuvasArray.Where(c => c == "0").Count();
                            int diasCaptacao = linhaEstacao.StatusChuvasArray.Where(c => c == "1").Count();

                            if (string.IsNullOrEmpty(linhaEstacao.NumDiasDeChuva)) //Informação não exibida na planilha do Hidroweb
                            {
                                if (diasCaptacao == diasMes)
                                {
                                    dados[linha, coluna] = 0;
                                }
                                else if (diasCaptacao == 0)
                                {
                                    dados[linha, coluna + 13] = "i";
                                    linhaInvalida = true;
                                }
                                else if (diasMes > diasCaptacao)
                                {
                                    dados[linha, coluna + 13] = "a";
                                    linhaInvalida = true;
                                }
                            }
                            else
                            {
                                dados[linha, coluna] = linhaEstacao.NumDiasDeChuva;
                                somatorioDias += Convert.ToInt32(linhaEstacao.NumDiasDeChuva);
                            }

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

        #region Aba resumo dias falha
        public static _Workbook CriarAbaResumoDiasFalha(_Workbook workbook, IList<SerieHistoricaChuvas> serieHistorica, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[7];

            DateTime dataInicio = estacao.Inicio.HasValue ? estacao.Inicio.Value : serieHistorica.OrderBy(c => c.Data).First().Data;
            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);
            DateTime dataFim = estacao.Fim.HasValue ? estacao.Fim.Value : serieHistorica.OrderByDescending(c => c.Data).First().Data;

            SerieHistoricaChuvas linhaEstacao = new SerieHistoricaChuvas();

            int totalAnos = dataFim.Year - dataIt.Year; // Dias no mes mais linha referente ao ano.
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
                                somatorioDias += diasFalha;
                            }
                            else if (diasMes != diasFalha)
                            {
                                if (diasMes > diasFalha)
                                {
                                    dados[linha, coluna] = diasMes - diasChuva;
                                    dados[linha, coluna + 13] = "a";
                                    somatorioDias += diasMes - diasChuva;
                                }
                                else
                                {
                                    somatorioDias += diasMes;
                                    dados[linha, coluna] = diasMes;
                                    dados[linha, coluna + 13] = "i";
                                }
                            }

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
                media = somatorio / (dataFim.Year - dataInicio.Year + 1);
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

        #endregion

        #region Fluviométrico

        //CotaStatus: 0 = Branco, 1 = Real, 2 = Estimado, 3 = Duvidoso, 4 = Régua Seca

        static object[,] dadosBackup;

        public static _Workbook CriarNovaPlanilhaFluviometrico(string filename)
        {

            // creating Excel Application
            app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application

            _Workbook workbook = app.Workbooks.Open(Environment.CurrentDirectory + "/template_flu.xlsx");

            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;


            xlSheets = workbook.Sheets as Sheets;


            // see the excel sheet behind the program
            app.Visible = false;


            return workbook;
        }

        #region [Aba Estação Fluviometrico]
        public static _Workbook CriarAbaEstacaoFluviometrica(_Workbook workbook, IList<SerieHistoricaVazao> serieHistoricaVazao, EstacaoData estacao)
        {
            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[1];

            DateTime dataIt = serieHistoricaVazao.OrderBy(c => c.Data).First().Data;
            DateTime dataFim = serieHistoricaVazao.OrderByDescending(c => c.Data).First().Data;

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
            dadosEstacao[15, 0] = string.Format("De {0} a {1}", new object[] { dataIt.ToString("dd/MM/yyyy")
                                                                ,dataFim.ToString("dd/MM/yyyy")});


            Range range = worksheet.Cells[2, 3];
            range = range.Resize[16, 1];

            range.Value = dadosEstacao;

            return workbook;
        }
        #endregion

        #region [Aba Cotas]
        public static _Workbook CriarAbaCotas(_Workbook workbook, IList<SerieHistoricaCotas> serieHistoricaCotas, EstacaoData estacao)
        {
            GC.Collect();
            SerieHistoricaCotas linhaEstacao;

            DateTime dataInicio = serieHistoricaCotas.OrderBy(c => c.Data).First().Data;
            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);
            DateTime dataFim = serieHistoricaCotas.OrderByDescending(c => c.Data).First().Data;

            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[2];

            int monthQuantity = (((dataFim.Year - dataIt.Year) * 12) + dataFim.Month - dataIt.Month) + 1;

            //array de dados
            object[,] dados = new object[monthQuantity, 39];
            int i = 0;
            dados[0, 0] = estacao.Codigo;
            while (dataIt <= dataFim)//Cria todas as linhas até a data fim.
            {
                //Tenta buscar dados consistidos, senão busca os dados Brutos
                linhaEstacao = serieHistoricaCotas.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                if (linhaEstacao == null)
                {
                    linhaEstacao = serieHistoricaCotas.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");
                    dados[i, 38] = 1;
                }

                dados[i, 1] = dataIt.Date;

                if (linhaEstacao == null) //linha de dados não existente na base da Ana
                {
                    dados[i, 38] = 5;
                }
                else
                {

                    for (int j = 0; j < 31; j++)
                    {
                        dados[i, 2 + j] = linhaEstacao.CotasArray[j + 1];
                    }
                    dados[i, 33] = linhaEstacao.Maxima;
                    dados[i, 34] = linhaEstacao.Minima;
                    dados[i, 35] = linhaEstacao.Media;
                    dados[i, 36] = linhaEstacao.DiaMaxima;
                    dados[i, 37] = linhaEstacao.DiaMinima;
                }

                i++;
                dataIt = dataIt.AddMonths(1);
            }

            Range range = worksheet.Cells[2, 1];
            range = range.Resize[monthQuantity, 39];

            range.Value = dados;

            return workbook;
        }

        #endregion

        #region [Aba Vazão]
        public static _Workbook CriarAbaVazao(_Workbook workbook, IList<SerieHistoricaVazao> serieHistoricaVazao, EstacaoData estacao)
        {
            GC.Collect();
            SerieHistoricaVazao linhaEstacao;

            DateTime dataInicio = serieHistoricaVazao.OrderBy(c => c.Data).First().Data;
            DateTime dataIt = new DateTime(dataInicio.Year, 1, 1);
            DateTime dataFim = serieHistoricaVazao.OrderByDescending(c => c.Data).First().Data;

            //Select the sheet
            _Worksheet worksheet = workbook.Worksheets[3];

            int monthQuantity = (((dataFim.Year - dataIt.Year) * 12) + dataFim.Month - dataIt.Month) + 1;

            //array de dados
            object[,] dados = new object[monthQuantity, 39];
            int i = 0;
            dados[0, 0] = estacao.Codigo;
            while (dataIt <= dataFim)//Cria todas as linhas até a data fim.
            {
                //Tenta buscar dados consistidos, senão busca os dados Brutos
                linhaEstacao = serieHistoricaVazao.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                if (linhaEstacao == null)
                {
                    linhaEstacao = serieHistoricaVazao.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");
                    dados[i, 38] = 1;
                }
                dados[i, 1] = dataIt.Date;

                if (linhaEstacao == null) //linha de dados não existente na base da Ana
                {
                    dados[i, 38] = 5;
                }
                else
                {

                    for (int j = 0; j < 31; j++)
                    {
                        dados[i, 2 + j] = linhaEstacao.VazaoArray[j + 1];
                    }
                    dados[i, 33] = linhaEstacao.Maxima;
                    dados[i, 34] = linhaEstacao.Minima;
                    dados[i, 35] = linhaEstacao.Media;
                    dados[i, 36] = linhaEstacao.DiaMaxima;
                    dados[i, 37] = linhaEstacao.DiaMinima;
                }

                i++;
                dataIt = dataIt.AddMonths(1);
            }

            Range range = worksheet.Cells[2, 1];
            range = range.Resize[monthQuantity, 39];

            range.Value = dados;

            return workbook;
        }

        #endregion

        #region [Aba Resumo Cotas Vazão]
        public static _Workbook CriarCotaVazaoDiaria(_Workbook workbook, IList<SerieHistoricaCotas> serieHistoricaCotas, IList<SerieHistoricaVazao> serieHistoricaVazao, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[4];

            SerieHistoricaVazao linhaVazao;
            SerieHistoricaCotas linhaCotas;

            DateTime dataInicioVazao = serieHistoricaVazao.OrderBy(c => c.Data).First().Data;
            DateTime dataFimVazao = serieHistoricaVazao.OrderByDescending(c => c.Data).First().Data;

            DateTime dataInicioCotas = serieHistoricaCotas.OrderBy(c => c.Data).First().Data;
            DateTime dataFimCotas= serieHistoricaCotas.OrderByDescending(c => c.Data).First().Data;

            var anoInicio = dataInicioVazao.Year > dataInicioCotas.Year ? dataInicioVazao.Year : dataInicioCotas.Year;
            DateTime dataFim = dataFimVazao > dataFimCotas ? dataFimVazao : dataFimCotas;

            DateTime dataIt = new DateTime(anoInicio, 1, 1);

            long daysQuantity = Convert.ToInt64((dataFim - dataIt).TotalDays) + DateTime.DaysInMonth(dataFim.Year, dataFim.Month);

            object[,] dados = new object[daysQuantity, 5];

            int i = 0;
            int ultimaLinha = 0;

            while (dataIt <= dataFim)//Cria todas as linhas até a data fim.
            {
                //Tenta buscar dados consistidos de COTAS, senão busca os dados Brutos
                linhaCotas = serieHistoricaCotas.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                if (linhaCotas == null)
                    linhaCotas = serieHistoricaCotas.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                //Tenta buscar dados consistidos de VAZAO, senão busca os dados Brutos
                linhaVazao = serieHistoricaVazao.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "2");
                if (linhaVazao == null)
                    linhaVazao = serieHistoricaVazao.FirstOrDefault(c => c.Data == dataIt && c.NivelConsistencia == "1");

                var totalDias = DateTime.DaysInMonth(dataIt.Year, dataIt.Month);

                for (int j = 0; j < totalDias; j++)
                {
                    dados[ultimaLinha, 0] = dataIt.Date;

                    //COTAS
                    if (linhaCotas == null)
                    {
                        dados[ultimaLinha, 3] = 5;
                    }
                    else
                    {
                        dados[ultimaLinha, 1] = linhaCotas.CotasArray[j + 1];
                        dados[ultimaLinha, 3] = linhaCotas.NivelConsistencia;
                    }
                    //VAZÃO
                    if (linhaVazao == null)
                    {
                        dados[ultimaLinha, 3] = 5;
                    }
                    else
                    {
                        dados[ultimaLinha, 2] = linhaVazao.VazaoArray[j + 1];
                        dados[ultimaLinha, 3] = linhaVazao.NivelConsistencia;
                    }
                    ultimaLinha++;
                    dataIt = dataIt.AddDays(1);
                }
            }

            Range range = worksheet.Cells[3, 2];
            range = range.Resize[ultimaLinha, 5];

            dadosBackup = new object[ultimaLinha,5];
            dadosBackup = dados;

            range.Value = dados;

            return workbook;
        }


        #endregion

        #region Grafico CT

        public static _Workbook CriarGraficoCotaTempo(_Workbook workbook, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[5];

            // Add chart.
            var charts = worksheet.ChartObjects() as
                Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add(10, 10, 800, 600) as
                Microsoft.Office.Interop.Excel.ChartObject;
            var chart = chartObject.Chart;

            var dadosGrafico = new object[dadosBackup.GetUpperBound(0) + 1, 2];

            for (int i = 0; i < dadosBackup.GetUpperBound(0); i++)
            {
                if (dadosBackup[i, 1]!=null && !string.IsNullOrEmpty(dadosBackup[i,1].ToString()))
                {
                    dadosGrafico[i, 0] = dadosBackup[i, 0]; //Atribui vazão
                    dadosGrafico[i, 1] = dadosBackup[i, 1]; //Atribui cota
                }
            }

            Range range = worksheet.Cells[3, 2];
            range = range.Resize[dadosGrafico.GetUpperBound(0), 2];
            range.Value = dadosGrafico;

            chart.SetSourceData(range);

            // Set chart properties.
            chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlXYScatterSmooth;
            chart.ChartWizard(Source: range,
                Title: "Cota X Tempo",
                CategoryTitle: "Tempo",
                ValueTitle: "Cota");


            return workbook;
        }

        #endregion

        #region Grafico CV

        public static _Workbook CriarGraficoCotaVazao(_Workbook workbook, EstacaoData estacao)
        {
            GC.Collect();
            _Worksheet worksheet = workbook.Worksheets[6];

            // Add chart.
            var charts = worksheet.ChartObjects() as
                Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add(10, 10, 800, 600) as
                Microsoft.Office.Interop.Excel.ChartObject;
            var chart = chartObject.Chart;

            var dadosGrafico = new object[dadosBackup.GetUpperBound(0) + 1, 2];

            for (int i = 0; i < dadosBackup.GetUpperBound(0); i++)
            {
                dadosGrafico[i, 0] = dadosBackup[i, 2]; //Atribui vazão
                dadosGrafico[i, 1] = dadosBackup[i, 1]; //Atribui cota
            }

            Range range = worksheet.Cells[3, 2];
            range = range.Resize[dadosGrafico.GetUpperBound(0), 2];
            range.Value = dadosGrafico;

            chart.SetSourceData(range);

            // Set chart properties.
            chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlXYScatter;
            chart.ChartWizard(Source: range,
                Title: "Cota X Vazão",
                CategoryTitle: "Vazão",
                ValueTitle: "Cotas");


            return workbook;
        }

        #endregion

        #endregion

        #region Geral

        public static void FecharAplicacao(_Workbook workbook)
        {
            object misValue = System.Reflection.Missing.Value;
            workbook.Close(true, misValue, misValue);
            app.Quit();

            //DesalocarObjeto(workbook.Worksheets);
            DesalocarObjeto(workbook);
            DesalocarObjeto(app);
        }

        private static void DesalocarObjeto(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal
                   .ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine(@"Exception Occurred while releasing
    
                   object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }
}
