using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace PortalClient
{
    public partial class logon : Form
    {
        public CookieContainer Cookies;///we need this to maintain state 

        public bool loggedOn=false;
        public logon()
        {
            InitializeComponent();
            string _url2 = "http://www.crmtogether.com/sage_crm_portaldemo/logon.php";
            Uri anUri2 = new Uri(_url2);
            webBrowser1.Url = anUri2;

            lbl_portallogoninfo.Text = "Sample portal webservice client application from www.crmtogether.com";
        }

        private void btn_logon_Click(object sender, EventArgs e)
        {

            localhost.Service1 proxy = new localhost.Service1();
           //Set the Cookie Container on the proxy
           if (Cookies==null)
           {
                Cookies = new CookieContainer();
           }
           proxy.CookieContainer = Cookies;
           this.loggedOn=proxy.PortalLogon(this.tb_username.Text, this.tb_password.Text);
           this.Close();
        }

    }
}