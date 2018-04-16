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
        public TelaPrincipal()
        {
            InitializeComponent();
            Atividade.Text = string.Empty;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int codigoEstacao;
            button1.Enabled = false;

            bool isValid = Int32.TryParse(codigoEstacaoTxtBox.Text, out codigoEstacao);
            if (isValid)
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
            else
            {
                MessageBox.Show("Código de estação inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            button1.Enabled = true;
        }

        private void addToSelectBtn_Click(object sender, EventArgs e)
        {
            if (preListBox.Items.Count == 0)
                return;

            selectLstBox.Items.Add(preListBox.SelectedItem);
            preListBox.Items.Remove(preListBox.SelectedItem);
        }

        private void remOfSelect_Click(object sender, EventArgs e)
        {
            if (selectLstBox.Items.Count == 0)
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
                MessageBox.Show("Código pelo menos uma estação para gerar o relatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            boxBusca.Enabled = false;
            boxSelecao.Enabled = false;

            foreach (var item in selectLstBox.Items) {

                string codigo = item.ToString().Split('-')[0];

                Atividade.Text = $"Buscando dados estação {codigo} no Hidroweb-ANA";

                var dadosEstacao = await ServiceANAHelper.DadosPluviometricosEstacao(Convert.ToInt32(codigo));

                try
                {
                    if (dadosEstacao.EhValido)
                    {
                        Atividade.Text = $"Gerando planilha para estação: {codigo}";

                        var dadosSerieHistorica = DataTableParaSerieHistorica(dadosEstacao.Dados);
                        var estacao = ListaEstacoesCache.Estacoes.First(c => c.Codigo == Convert.ToInt32(codigo));

                        var planilha = ExcelInteropHelper.CriarNovaPlanilhaPluviometrico("item");
                        planilha = ExcelInteropHelper.CriarAbaEstacao(planilha, estacao);
                        planilha = ExcelInteropHelper.CriarAbaChuvas(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumo(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumoDia(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumoDiasChuva(planilha, dadosSerieHistorica, estacao);
                        planilha = ExcelInteropHelper.CriarAbaResumoDiasFalha(planilha, dadosSerieHistorica, estacao);
                        ExcelInteropHelper.FinalizarPlanilha();

                        Atividade.Text = $"Salvando planilha.";

                        planilha.SaveAs(escolherDiretorio.SelectedPath+$"/{codigo}", Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, null,
                        null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                        null, null, null);

                        ExcelInteropHelper.FecharAplicacao();
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

            Atividade.Text = string.Empty;
            boxBusca.Enabled = true;
            boxSelecao.Enabled = true;
        }

        #region Métodos auxiliares

        private IList<SerieHistorica> DataTableParaSerieHistorica(System.Data.DataTable dataTable)
        {
            if(dataTable!=null)
                return dataTable.DataTableToList<SerieHistorica>().ToList();

            return null;
        }

        #endregion

        //Quando mudar a selação ativa o botão de busca
        private void ApagarEstacoes(object sender, EventArgs e)
        {
            button1.Enabled = true;
            codigoEstacaoTxtBox.Text = string.Empty;
            preListBox.Items.Clear();
            selectLstBox.Items.Clear();
        }

        private void escolherDiretorio_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}
