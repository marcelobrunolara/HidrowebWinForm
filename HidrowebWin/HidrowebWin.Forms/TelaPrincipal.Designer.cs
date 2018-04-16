namespace HidrowebWin.Forms
{
    partial class TelaPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.boxSelecao = new System.Windows.Forms.GroupBox();
            this.remOfSelect = new System.Windows.Forms.Button();
            this.addToSelectBtn = new System.Windows.Forms.Button();
            this.selectLstBox = new System.Windows.Forms.ListBox();
            this.preListBox = new System.Windows.Forms.ListBox();
            this.btnGerarRelatorio = new System.Windows.Forms.Button();
            this.boxBusca = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tipoEstacaoCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.codigoEstacaoTxtBox = new System.Windows.Forms.TextBox();
            this.escolherDiretorio = new System.Windows.Forms.FolderBrowserDialog();
            this.Atividade = new System.Windows.Forms.Label();
            this.boxSelecao.SuspendLayout();
            this.boxBusca.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxSelecao
            // 
            this.boxSelecao.Controls.Add(this.remOfSelect);
            this.boxSelecao.Controls.Add(this.addToSelectBtn);
            this.boxSelecao.Controls.Add(this.selectLstBox);
            this.boxSelecao.Controls.Add(this.preListBox);
            this.boxSelecao.Location = new System.Drawing.Point(26, 69);
            this.boxSelecao.Name = "boxSelecao";
            this.boxSelecao.Size = new System.Drawing.Size(543, 186);
            this.boxSelecao.TabIndex = 0;
            this.boxSelecao.TabStop = false;
            this.boxSelecao.Text = "Estações";
            // 
            // remOfSelect
            // 
            this.remOfSelect.Location = new System.Drawing.Point(239, 116);
            this.remOfSelect.Name = "remOfSelect";
            this.remOfSelect.Size = new System.Drawing.Size(70, 23);
            this.remOfSelect.TabIndex = 3;
            this.remOfSelect.Text = "<";
            this.remOfSelect.UseVisualStyleBackColor = true;
            this.remOfSelect.Click += new System.EventHandler(this.remOfSelect_Click);
            // 
            // addToSelectBtn
            // 
            this.addToSelectBtn.Location = new System.Drawing.Point(238, 43);
            this.addToSelectBtn.Name = "addToSelectBtn";
            this.addToSelectBtn.Size = new System.Drawing.Size(70, 23);
            this.addToSelectBtn.TabIndex = 2;
            this.addToSelectBtn.Text = ">";
            this.addToSelectBtn.UseVisualStyleBackColor = true;
            this.addToSelectBtn.Click += new System.EventHandler(this.addToSelectBtn_Click);
            // 
            // selectLstBox
            // 
            this.selectLstBox.FormattingEnabled = true;
            this.selectLstBox.Location = new System.Drawing.Point(331, 19);
            this.selectLstBox.Name = "selectLstBox";
            this.selectLstBox.ScrollAlwaysVisible = true;
            this.selectLstBox.Size = new System.Drawing.Size(206, 147);
            this.selectLstBox.TabIndex = 1;
            // 
            // preListBox
            // 
            this.preListBox.FormattingEnabled = true;
            this.preListBox.Location = new System.Drawing.Point(7, 20);
            this.preListBox.Name = "preListBox";
            this.preListBox.ScrollAlwaysVisible = true;
            this.preListBox.Size = new System.Drawing.Size(206, 147);
            this.preListBox.TabIndex = 0;
            // 
            // btnGerarRelatorio
            // 
            this.btnGerarRelatorio.Location = new System.Drawing.Point(464, 264);
            this.btnGerarRelatorio.Name = "btnGerarRelatorio";
            this.btnGerarRelatorio.Size = new System.Drawing.Size(105, 23);
            this.btnGerarRelatorio.TabIndex = 1;
            this.btnGerarRelatorio.Text = "Gerar Relatório";
            this.btnGerarRelatorio.UseVisualStyleBackColor = true;
            this.btnGerarRelatorio.Click += new System.EventHandler(this.btnGerarRelatorio_Click);
            // 
            // boxBusca
            // 
            this.boxBusca.Controls.Add(this.label2);
            this.boxBusca.Controls.Add(this.button1);
            this.boxBusca.Controls.Add(this.tipoEstacaoCombo);
            this.boxBusca.Controls.Add(this.label1);
            this.boxBusca.Controls.Add(this.codigoEstacaoTxtBox);
            this.boxBusca.Location = new System.Drawing.Point(26, 14);
            this.boxBusca.Name = "boxBusca";
            this.boxBusca.Size = new System.Drawing.Size(543, 49);
            this.boxBusca.TabIndex = 2;
            this.boxBusca.TabStop = false;
            this.boxBusca.Text = "Buscar Estação";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tipo de Estação:";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(430, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tipoEstacaoCombo
            // 
            this.tipoEstacaoCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoEstacaoCombo.FormattingEnabled = true;
            this.tipoEstacaoCombo.Items.AddRange(new object[] {
            "Pluviométrica"});
            this.tipoEstacaoCombo.Location = new System.Drawing.Point(101, 20);
            this.tipoEstacaoCombo.Name = "tipoEstacaoCombo";
            this.tipoEstacaoCombo.Size = new System.Drawing.Size(121, 21);
            this.tipoEstacaoCombo.TabIndex = 4;
            this.tipoEstacaoCombo.SelectedIndexChanged += new System.EventHandler(this.ApagarEstacoes);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código:";
            // 
            // codigoEstacaoTxtBox
            // 
            this.codigoEstacaoTxtBox.Location = new System.Drawing.Point(279, 20);
            this.codigoEstacaoTxtBox.Name = "codigoEstacaoTxtBox";
            this.codigoEstacaoTxtBox.Size = new System.Drawing.Size(126, 20);
            this.codigoEstacaoTxtBox.TabIndex = 0;
            // 
            // escolherDiretorio
            // 
            this.escolherDiretorio.HelpRequest += new System.EventHandler(this.escolherDiretorio_HelpRequest);
            // 
            // Atividade
            // 
            this.Atividade.AutoSize = true;
            this.Atividade.Location = new System.Drawing.Point(23, 269);
            this.Atividade.Name = "Atividade";
            this.Atividade.Size = new System.Drawing.Size(67, 13);
            this.Atividade.TabIndex = 3;
            this.Atividade.Text = "ActivityLabel";
            // 
            // TelaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 299);
            this.Controls.Add(this.Atividade);
            this.Controls.Add(this.boxBusca);
            this.Controls.Add(this.btnGerarRelatorio);
            this.Controls.Add(this.boxSelecao);
            this.Name = "TelaPrincipal";
            this.Text = "Gerador de Planilhas Hidroweb - Walm";
            this.boxSelecao.ResumeLayout(false);
            this.boxBusca.ResumeLayout(false);
            this.boxBusca.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boxSelecao;
        private System.Windows.Forms.Button remOfSelect;
        private System.Windows.Forms.Button addToSelectBtn;
        private System.Windows.Forms.ListBox selectLstBox;
        private System.Windows.Forms.ListBox preListBox;
        private System.Windows.Forms.Button btnGerarRelatorio;
        private System.Windows.Forms.GroupBox boxBusca;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox codigoEstacaoTxtBox;
        private System.Windows.Forms.FolderBrowserDialog escolherDiretorio;
        private System.Windows.Forms.ComboBox tipoEstacaoCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Atividade;
    }
}

