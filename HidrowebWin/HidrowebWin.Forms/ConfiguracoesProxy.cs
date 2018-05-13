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
    public partial class ConfiguracoesProxy : Form
    {
        public ConfiguracoesProxy()
        {

            var enabled = System.Configuration.ConfigurationSettings.AppSettings.Keys;

            //if (enabled == "F")
            //{
            //    this.selectWeb.Enabled = false;
            //    this.configProxy.Enabled = false;
            //}
            //else
            //{
            //    this.proxyText.Text = Properties.Settings.Default[Constants.ProxyEnabled].ToString();
            //    this.portText.Text = Properties.Settings.Default[Constants.Port].ToString();
            //    this.userNameText.Text = Properties.Settings.Default[Constants.User].ToString();
            //    this.passText.Text = Properties.Settings.Default[Constants.Password].ToString();

            //    SoapRadio.Checked = Properties.Settings.Default[Constants.WebService].ToString() == "S";
            //    restRadop.Checked = Properties.Settings.Default[Constants.WebService].ToString() == "R";
            //}

            InitializeComponent();
        }
    }
}
