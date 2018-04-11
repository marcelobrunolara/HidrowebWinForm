﻿using HidrowebWin.Forms.Data;
using HidrowebWin.Forms.Data.Models;
using HidrowebWin.Forms.ExcelManager;
using HidrowebWin.Forms.Services;
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
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int codigoEstacao;
            button1.Enabled = false;

            DefinirTextStatus(null, true,0);

            bool isValid = Int32.TryParse(codigoEstacaoTxtBox.Text, out codigoEstacao);
            if (isValid)
            {
                var estacao = await BuscaDadosHelper.BuscarEstacaoPluviometrica(codigoEstacao);

                ListaEstacoesCache.Estacoes.Add(estacao);
                preListBox.Items.Add($"{estacao.Codigo} {estacao.Nome}");

                codigoEstacaoTxtBox.Text = string.Empty;
                DefinirTextStatus(string.Empty, true, preListBox.Items.Count);
            }
            else
            {
                DefinirTextStatus(null, false, 0);
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
            string item = selectLstBox.Items[0].ToString();
            var dadosEstacao = await ServiceANAHelper.DadosPluviometricosEstacao(2043003);

            if (dadosEstacao.EhValido) {

                var dadosSerieHistorica = DataTableParaSerieHistorica(dadosEstacao.Dados);
                var estacao = ListaEstacoesCache.Estacoes.First(c => c.Codigo == 2043003);

                var planilha = ExcelInteropHelper.CriarNovaPlanilhaPluviometrico("item");
                planilha = ExcelInteropHelper.CriarAbaEstacao(planilha, estacao);
                planilha = ExcelInteropHelper.CriarAbaChuvas(planilha, dadosSerieHistorica, estacao);
            }
        }

        #region Métodos auxiliares

        private void DefinirTextStatus(string texto, bool isValid, int regCount)
        {
            if (!isValid)
                statusText.Text = "Entrada inválida.";
            else if (texto == null)
                statusText.Text = "Buscando...";
            else if (regCount == 0)
                statusText.Text = "Não encontrada.";
            else
                statusText.Text = string.Empty;
        }

        private IList<SerieHistorica> DataTableParaSerieHistorica(DataTable dataTable)
        {
            if(dataTable!=null)
                return dataTable.DataTableToList<SerieHistorica>().ToList();

            return null;
        }

        #endregion

    }
}
