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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.remOfSelect = new System.Windows.Forms.Button();
            this.addToSelectBtn = new System.Windows.Forms.Button();
            this.selectLstBox = new System.Windows.Forms.ListBox();
            this.preListBox = new System.Windows.Forms.ListBox();
            this.btnGerarRelatorio = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.codigoEstacaoTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.remOfSelect);
            this.groupBox1.Controls.Add(this.addToSelectBtn);
            this.groupBox1.Controls.Add(this.selectLstBox);
            this.groupBox1.Controls.Add(this.preListBox);
            this.groupBox1.Location = new System.Drawing.Point(26, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 186);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Estações";
            // 
            // remOfSelect
            // 
            this.remOfSelect.Location = new System.Drawing.Point(183, 95);
            this.remOfSelect.Name = "remOfSelect";
            this.remOfSelect.Size = new System.Drawing.Size(34, 23);
            this.remOfSelect.TabIndex = 3;
            this.remOfSelect.Text = "<";
            this.remOfSelect.UseVisualStyleBackColor = true;
            this.remOfSelect.Click += new System.EventHandler(this.remOfSelect_Click);
            // 
            // addToSelectBtn
            // 
            this.addToSelectBtn.Location = new System.Drawing.Point(184, 52);
            this.addToSelectBtn.Name = "addToSelectBtn";
            this.addToSelectBtn.Size = new System.Drawing.Size(34, 23);
            this.addToSelectBtn.TabIndex = 2;
            this.addToSelectBtn.Text = ">";
            this.addToSelectBtn.UseVisualStyleBackColor = true;
            this.addToSelectBtn.Click += new System.EventHandler(this.addToSelectBtn_Click);
            // 
            // selectLstBox
            // 
            this.selectLstBox.FormattingEnabled = true;
            this.selectLstBox.Location = new System.Drawing.Point(224, 19);
            this.selectLstBox.Name = "selectLstBox";
            this.selectLstBox.ScrollAlwaysVisible = true;
            this.selectLstBox.Size = new System.Drawing.Size(170, 147);
            this.selectLstBox.TabIndex = 1;
            // 
            // preListBox
            // 
            this.preListBox.FormattingEnabled = true;
            this.preListBox.Location = new System.Drawing.Point(7, 20);
            this.preListBox.Name = "preListBox";
            this.preListBox.ScrollAlwaysVisible = true;
            this.preListBox.Size = new System.Drawing.Size(170, 147);
            this.preListBox.TabIndex = 0;
            // 
            // btnGerarRelatorio
            // 
            this.btnGerarRelatorio.Location = new System.Drawing.Point(321, 261);
            this.btnGerarRelatorio.Name = "btnGerarRelatorio";
            this.btnGerarRelatorio.Size = new System.Drawing.Size(105, 23);
            this.btnGerarRelatorio.TabIndex = 1;
            this.btnGerarRelatorio.Text = "Gerar Relatório";
            this.btnGerarRelatorio.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.statusText);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.codigoEstacaoTxtBox);
            this.groupBox2.Location = new System.Drawing.Point(26, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar Estação";
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Location = new System.Drawing.Point(292, 22);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(10, 13);
            this.statusText.TabIndex = 3;
            this.statusText.Text = " ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(167, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código:";
            // 
            // codigoEstacaoTxtBox
            // 
            this.codigoEstacaoTxtBox.Location = new System.Drawing.Point(53, 19);
            this.codigoEstacaoTxtBox.Name = "codigoEstacaoTxtBox";
            this.codigoEstacaoTxtBox.Size = new System.Drawing.Size(108, 20);
            this.codigoEstacaoTxtBox.TabIndex = 0;
            // 
            // TelaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 298);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnGerarRelatorio);
            this.Controls.Add(this.groupBox1);
            this.Name = "TelaPrincipal";
            this.Text = "Hidroweb - Projetics";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button remOfSelect;
        private System.Windows.Forms.Button addToSelectBtn;
        private System.Windows.Forms.ListBox selectLstBox;
        private System.Windows.Forms.ListBox preListBox;
        private System.Windows.Forms.Button btnGerarRelatorio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox codigoEstacaoTxtBox;
        private System.Windows.Forms.Label statusText;
    }
}

