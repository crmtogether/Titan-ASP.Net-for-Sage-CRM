using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SageCRM.AspNet;

namespace SageCRM
{
    public partial class TabPropEditor : Form
    {
        public SageCRMConnection connectionObject;
        public SageCRMTabGroup Block;
        private SageCRMDataSource ds;

        private string _entityname = "";

        public TabPropEditor(SageCRMTabGroup _Block)
        {
            this.Block = _Block;
            this.connectionObject = this.Block.SageCRMConnection;
            InitializeComponent();
        }

        public string EntityName
        {
            get
            {
                return this._entityname;
            }
            set
            {
                this._entityname = value;
            }
        }

        public string TabGroupName
        {
            get
            {
                return this.tb_tabgroupname.Text;
            }
            set
            {
                this.tb_tabgroupname.Text = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Block.EntityName = this.EntityName;
            this.Block.TabGroupName = this.TabGroupName;
            DialogResult = DialogResult.OK;
            Close();
        }
        private void setConnection()
        {
            this.ds = new SageCRMDataSource(); //create our datasource
            this.ds.SageCRMConnection = this.connectionObject; //assign a connection 
        }

        //Entity list
        private void populate_cbEntityName()
        {
            try
            {
                this.cbEntityName.Items.Clear();
                this.setConnection();
                this.ds.SelectSQL = "select distinct cobj_entityname from custom_screenobjects " +
                    "where cobj_type='TabGroup' " +  //we only show entities that have lists
                    "order by cobj_entityname";
                IDataReader idr = this.ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    cbEntityName.Items.Add(idr["cobj_entityname"].ToString());
                }
                this.setComboValue(cbEntityName, this.EntityName);
            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving entity names. " + ex3.Message.ToString());
            }
        }
        private void setComboValue(ComboBox cb, string value)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i].ToString().ToLower() == value.ToLower())
                {
                    cb.SelectedItem = cb.Items[i].ToString();
                    break;
                }
            }

        }

        private void populate_lb_screens()
        {
            try
            {
                lb_screens.Items.Clear();
                string entityFilter = "";
                if (this.EntityName != "")
                    entityFilter = "and cobj_entityname='" + this.EntityName + "' ";
                this.setConnection();
                this.ds.SelectSQL = "select cobj_name from custom_screenobjects " + //create our sql to get our entities
                               "where cobj_type='TabGroup' " +
                               entityFilter +
                               "order by cobj_name";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    lb_screens.Items.Add(idr["cobj_name"].ToString());
                }
            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving list names. " + ex3.Message.ToString());
            }
        }

        private void cbEntityName_SelectedValueChanged(object sender, EventArgs e)
        {
            this.EntityName = cbEntityName.SelectedItem.ToString();
            lb_screens.Items.Clear();
            this.populate_lb_screens();
        }

        private void TabPropEditor_Load(object sender, EventArgs e)
        {
            string _url = "http://www.crmtogether.com/sage_crm_editor_bar/tab.php";
            Uri anUri = new Uri(_url);
           // webBrowser1.Url = anUri;
        }

        private void TabPropEditor_Shown(object sender, EventArgs e)
        {
            this.populate_cbEntityName();
            this.populate_lb_screens();
        }

        private void cbEntityName_SelectedValueChanged_1(object sender, EventArgs e)
        {
            this._entityname = cbEntityName.SelectedItem.ToString();
            this.populate_lb_screens();
        }

        private void lb_screens_SelectedValueChanged(object sender, EventArgs e)
        {
            this.tb_tabgroupname.Text = lb_screens.SelectedItem.ToString();
        }

    }
}