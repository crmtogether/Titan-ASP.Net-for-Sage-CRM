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
    public partial class Form1 : Form
    {
        
        public bool loggedOn=false;

        public CookieContainer Cookies;
        
        public localhost.Service1 proxy;

        private BindingSource bindingSource1 = new BindingSource();

        public void setwb()
        {
            string _url2 = "http://www.crmtogether.com/sage_crm_portaldemo/form1.php";
            Uri anUri2 = new Uri(_url2);
            webBrowser1.Url = anUri2;

            lbl_caseinfo.Text = "Sample Grid populated from Portal webservice.";
        }
        public Form1()
        {
            proxy = new localhost.Service1();
            InitializeComponent();
            setwb();

            logon form = new logon();
            form.ShowDialog();
            this.loggedOn = form.loggedOn;
            Cookies = form.Cookies;
            //we must be logged on to get to here
            getPortalData();
        }
        public void getPortalData()
        {
            if (!this.loggedOn)
            {
                MessageBox.Show("Invlaid logon. Data cannot be shown");
            }
            else
            {
                proxy.CookieContainer = Cookies;
                lbl_companyname.Text = proxy.getCompanyName();

                textBox1.Text = lbl_companyname.Text;
                textBox2.Text = proxy.getCompanyWebSite();

                string _url2 = textBox2.Text;
                Uri anUri2 = new Uri(_url2);
                webBrowser2.Url = anUri2;

                dataGridView1.DataSource = bindingSource1;
                DataTable dt = new DataTable();
                dt.Load(proxy.getCases().CreateDataReader());
                bindingSource1.DataSource = dt;
            }
        }
    }
}