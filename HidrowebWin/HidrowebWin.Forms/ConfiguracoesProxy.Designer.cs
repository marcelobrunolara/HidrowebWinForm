namespace HidrowebWin.Forms
{
    partial class ConfiguracoesProxy
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
            this.selectWeb = new System.Windows.Forms.GroupBox();
            this.restRadop = new System.Windows.Forms.RadioButton();
            this.SoapRadio = new System.Windows.Forms.RadioButton();
            this.configProxy = new System.Windows.Forms.GroupBox();
            this.proxyText = new System.Windows.Forms.TextBox();
            this.portText = new System.Windows.Forms.TextBox();
            this.userNameText = new System.Windows.Forms.TextBox();
            this.passText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.selectWeb.SuspendLayout();
            this.configProxy.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectWeb
            // 
            this.selectWeb.Controls.Add(this.SoapRadio);
            this.selectWeb.Controls.Add(this.restRadop);
            this.selectWeb.Location = new System.Drawing.Point(13, 61);
            this.selectWeb.Name = "selectWeb";
            this.selectWeb.Size = new System.Drawing.Size(180, 67);
            this.selectWeb.TabIndex = 0;
            this.selectWeb.TabStop = false;
            this.selectWeb.Text = "Web Service";
            // 
            // restRadop
            // 
            this.restRadop.AutoSize = true;
            this.restRadop.Location = new System.Drawing.Point(16, 29);
            this.restRadop.Name = "restRadop";
            this.restRadop.Size = new System.Drawing.Size(47, 17);
            this.restRadop.TabIndex = 0;
            this.restRadop.TabStop = true;
            this.restRadop.Text = "Rest";
            this.restRadop.UseVisualStyleBackColor = true;
            // 
            // SoapRadio
            // 
            this.SoapRadio.AutoSize = true;
            this.SoapRadio.Location = new System.Drawing.Point(100, 29);
            this.SoapRadio.Name = "SoapRadio";
            this.SoapRadio.Size = new System.Drawing.Size(54, 17);
            this.SoapRadio.TabIndex = 1;
            this.SoapRadio.TabStop = true;
            this.SoapRadio.Text = "SOAP";
            this.SoapRadio.UseVisualStyleBackColor = true;
            // 
            // configProxy
            // 
            this.configProxy.Controls.Add(this.label4);
            this.configProxy.Controls.Add(this.label3);
            this.configProxy.Controls.Add(this.label2);
            this.configProxy.Controls.Add(this.label1);
            this.configProxy.Controls.Add(this.passText);
            this.configProxy.Controls.Add(this.userNameText);
            this.configProxy.Controls.Add(this.portText);
            this.configProxy.Controls.Add(this.proxyText);
            this.configProxy.Location = new System.Drawing.Point(13, 130);
            this.configProxy.Name = "configProxy";
            this.configProxy.Size = new System.Drawing.Size(180, 141);
            this.configProxy.TabIndex = 1;
            this.configProxy.TabStop = false;
            this.configProxy.Text = "Configurações de Proxy";
            // 
            // proxyText
            // 
            this.proxyText.Location = new System.Drawing.Point(6, 45);
            this.proxyText.Name = "proxyText";
            this.proxyText.Size = new System.Drawing.Size(119, 20);
            this.proxyText.TabIndex = 0;
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(131, 45);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(43, 20);
            this.portText.TabIndex = 1;
            // 
            // userNameText
            // 
            this.userNameText.Location = new System.Drawing.Point(55, 75);
            this.userNameText.Name = "userNameText";
            this.userNameText.Size = new System.Drawing.Size(119, 20);
            this.userNameText.TabIndex = 2;
            // 
            // passText
            // 
            this.passText.Location = new System.Drawing.Point(56, 102);
            this.passText.Name = "passText";
            this.passText.PasswordChar = '*';
            this.passText.Size = new System.Drawing.Size(118, 20);
            this.passText.TabIndex = 3;
            this.passText.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Endereço";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Porta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Usuário:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Senha:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(13, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 43);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(144, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Habilitar Config. de Proxy";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ConfiguracoesProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 322);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.configProxy);
            this.Controls.Add(this.selectWeb);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracoesProxy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracoes de Proxy";
            this.selectWeb.ResumeLayout(false);
            this.selectWeb.PerformLayout();
            this.configProxy.ResumeLayout(false);
            this.configProxy.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox selectWeb;
        private System.Windows.Forms.RadioButton restRadop;
        private System.Windows.Forms.RadioButton SoapRadio;
        private System.Windows.Forms.GroupBox configProxy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passText;
        private System.Windows.Forms.TextBox userNameText;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.TextBox proxyText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}