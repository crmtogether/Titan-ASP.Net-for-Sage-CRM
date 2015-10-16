using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    public partial class listBlockPicker : Form
    {
        public string entityName = "";
        public string listBlock = "";
        public SageCRMConnection SageCRMConnectionObject;
        public SageCRMDataSource cbds;

        public string ListValue
        {
            get
            {
                return textBox1.Text.ToString();
            }
            set
            {
                textBox1.Text = value;
            }
        }
        public string EntityValue
        {
            get
            {
                return lblEntityName.Text;
            }
            set
            {
                lblEntityName.Text=value;
            }
        }
        public bool setUpData()
        {
            string extraSQL ="";
            if (this.SageCRMConnectionObject == null)
                return false;
            cbds = new SageCRMDataSource(); //create our datasource
            cbds.SageCRMConnection = this.SageCRMConnectionObject; //assign a connection 
            cbds.SelectSQL = "select * from custom_screenobjects "+
                " where cobj_type='List' " + 
                extraSQL +
                " order by cobj_name";
            listBox1.DataSource = cbds;
            listBox1.DisplayMember = "bord_caption";
            listBox1.ValueMember = "bord_name";
            return true;
        }
        public listBlockPicker()
        {
            InitializeComponent();
        }

    }
}