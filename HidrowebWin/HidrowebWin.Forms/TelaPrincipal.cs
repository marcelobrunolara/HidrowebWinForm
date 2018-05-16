using HidrowebWin.Forms.Data;
using HidrowebWin.Forms.Data.Models;
using HidrowebWin.Forms.ExcelManager;
using HidrowebWin.Forms.Services;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HidrowebWin.Forms
{
    public partial class TelaPrincipal : Form
    {
        private ConfiguracoesProxy telaProxy;
        public TelaPrincipal()
        {
            InitializeComponent();
            Atividade.Text = string.Empty;

    }

        #region Eventos
        private async void button1_Click(object sender, EventArgs e)
        {
            int codigoEstacao;
            button1.Enabled = false;

            bool isValid = Int32.TryParse(codigoEstacaoTxtBox.Text, out codigoEstacao);
            if (isValid && (string)tipoEstacaoCombo.SelectedItem == "Pluviométrica")
            {
                Atividade.Text = "Buscando estação...";
                var estacao = await BuscaDadosHelper.BuscarEstacaoPluviometrica(codigoEstacao);

                if (string.IsNullOrEmpty(estacao.Nome))
                    MessageBox.Show("Estação não encontrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                else
                {
                    ListaEstacoesCache.Estacoes.Add(estacao);
                    preListBox.Items.Add($"{estacao.Codigo}-{estacao.Nome}");
                    codigoEstacaoTxtBox.Text = string.Empty;
                }
                Atividade.Text = string.Empty;
            }
            else if (isValid && (string)tipoEstacaoCombo.SelectedItem == "Fluviométrica")
            {
                Atividade.Text = "Buscando estação...";
                var estacao = await BuscaDadosHelper.BuscarEstacaoFluviometrica(codigoEstacao);

                if (string.IsNullOrEmpty(estacao.Nome))
                    MessageBox.Show("Estação não encontrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                else
                {
                    ListaEstacoesCache.Estacoes.Add(estacao);
                    preListBox.Items.Add($"{estacao.Codigo}-{estacao.Nome}");
                    codigoEstacaoTxtBox.Text = string.Empty;
                }
                Atividade.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Código de estação inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            button1.Enabled = true;
        }

        private void addToSelectBtn_Click(object sender, EventArgs e)
        {
            if (preListBox.Items.Count == 0 || preListBox.SelectedItem == null)
                return;

            selectLstBox.Items.Add(preListBox.SelectedItem);
            preListBox.Items.Remove(preListBox.SelectedItem);
        }

        private void remOfSelect_Click(object sender, EventArgs e)
        {
            if (selectLstBox.Items.Count == 0 || selectLstBox.SelectedItem == null)
                return;

            preListBox.Items.Add(selectLstBox.SelectedItem);
            selectLstBox.Items.Remove(selectLstBox.SelectedItem);
        }

        private async void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            Atividade.Text = $"Processando, aguarde...";
            escolherDiretorio.ShowDialog();

            if (selectLstBox.Items.Count == 0)
            {
                MessageBox.Show("Insira pelo menos uma estação para gerar o relatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            boxBusca.Enabled = false;
            boxSelecao.Enabled = false;

            if ((string)tipoEstacaoCombo.SelectedItem == "Fluviométrica")
            {
                await GerarRelatorioFluviometrico();
            }
            else
            {
                await GerarRelatorioPluviometrico();
            }

            Atividade.Text = string.Empty;
            boxBusca.Enabled = true;
            boxSelecao.Enabled = true;
        }


        private void ApagarEstacoes(object sender, EventArgs e)
        {
            button1.Enabled = true;
            codigoEstacaoTxtBox.Text = string.Empty;
            preListBox.Items.Clear();
            selectLstBox.Items.Clear();
        }
        #endregion


        #region Métodos auxiliares

        private async Task GerarRelatorioPluviometrico()
        {
            foreach (var item in selectLstBox.Items)
            {

                string codigo = item.ToString().Split('-')[0];

                Atividade.Text = $"Buscando dados estação {codigo} no Hidroweb-ANA";

                var dadosEstacao = await ServiceANAHelper.DadosPluviometricosEstacao(Convert.ToInt32(codigo));

                try
                {
                    if (dadosEstacao.EhValido)
                    {
                        Atividade.Text = $"Gerando planilha para estação: {codigo}";

                        var dadosSerieHistorica = DataTableParaSerieHistoricaChuvas(dadosEstacao.Dados);
                        var estacao = ListaEstacoesCache.Estacoes.First(c => c.Codigo == Convert.ToInt32(codigo));

                        _Workbook planilha = ExcelInteropHelper.CriarNovaPlanilhaPluviometrico("item");
                        planilha = ExcelInteropHelper.CriarAbaEstacao(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaChuvas(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaDiaria(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumo(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumoDia(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumoDiasChuva(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumoDiasFalha(planilha, dadosSerieHistorica, estacao);
                        Atividade.Text = $"Salvando planilha.";

                        planilha.SaveAs(escolherDiretorio.SelectedPath + $"/{codigo}", Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, null,
                        null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                        null, null, null);

                        ExcelInteropHelper.FecharAplicacao(planilha);
                    }
                    else
                    {
                        MessageBox.Show($"Não foi possível encontrar dados para {Convert.ToInt32(codigo)}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                catch (Exception ex)
                {
                    boxBusca.Enabled = true;
                    boxSelecao.Enabled = true;
                    Atividade.Text = string.Empty;
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private async Task GerarRelatorioFluviometrico()
        {
            foreach (var item in selectLstBox.Items)
            {

                string codigo = item.ToString().Split('-')[0];

                Atividade.Text = $"Buscando dados estação {codigo} no Hidroweb-ANA";

                var dadosVazaoEstacao = await ServiceANAHelper.DadosFluviometricosVazaoEstacao(Convert.ToInt32(codigo));

                var dadosCotasEstacao = await ServiceANAHelper.DadosFluviometricosCotaEstacao(Convert.ToInt32(codigo));

                try
                {
                    if (dadosVazaoEstacao.EhValido && dadosCotasEstacao.EhValido)
                    {
                        Atividade.Text = $"Gerando planilha para estação: {codigo}";

                        var dadosSerieHistoricaCotas = DataTableParaSerieHistoricaCota(dadosCotasEstacao.Dados);
                        var dadosSerieHistoricaVazao = DataTableParaSerieHistoricaVazao(dadosVazaoEstacao.Dados);

                        var estacao = ListaEstacoesCache.Estacoes.First(c => c.Codigo == Convert.ToInt32(codigo));

                        _Workbook planilha = ExcelInteropHelper.CriarNovaPlanilhaFluviometrico ("item");
                        planilha = ExcelInteropHelper.CriarAbaEstacaoFluviometrica(planilha, dadosSerieHistoricaVazao, estacao);
                        planilha = ExcelInteropHelper.CriarAbaCotas(planilha, dadosSerieHistoricaCotas, estacao);
                        planilha = ExcelInteropHelper.CriarAbaVazao(planilha, dadosSerieHistoricaVazao, estacao);
                        planilha = ExcelInteropHelper.CriarCotaVazaoDiaria(planilha, dadosSerieHistoricaCotas, dadosSerieHistoricaVazao, estacao);
                        planilha = ExcelInteropHelper.CriarGraficoCotaTempo(planilha, estacao);
                        planilha = ExcelInteropHelper.CriarGraficoCotaVazao(planilha, estacao);


                        Atividade.Text = $"Salvando planilha.";

                        planilha.SaveAs(escolherDiretorio.SelectedPath + $"/{codigo}", Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, null,
                        null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                        null, null, null);

                        ExcelInteropHelper.FecharAplicacao(planilha);
                    }
                    else
                    {
                        MessageBox.Show($"Não foi possível encontrar dados para {Convert.ToInt32(codigo)}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                catch (Exception ex)
                {
                    boxBusca.Enabled = true;
                    boxSelecao.Enabled = true;
                    Atividade.Text = string.Empty;
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private IList<SerieHistoricaChuvas> DataTableParaSerieHistoricaChuvas(System.Data.DataTable dataTable)
        {
            if(dataTable!=null)
                return dataTable.DataTableToList<SerieHistoricaChuvas>().ToList();

            return null;
        }

        private IList<SerieHistoricaVazao> DataTableParaSerieHistoricaVazao(System.Data.DataTable dataTable)
        {
            if (dataTable != null)
                return dataTable.DataTableToList<SerieHistoricaVazao>().ToList();

            return null;
        }

        private IList<SerieHistoricaCotas> DataTableParaSerieHistoricaCota(System.Data.DataTable dataTable)
        {
            if (dataTable != null)
                return dataTable.DataTableToList<SerieHistoricaCotas>().ToList();

            return null;
        }

        #endregion

        //Quando mudar a seleção ativa o botão de busca


    }
}
