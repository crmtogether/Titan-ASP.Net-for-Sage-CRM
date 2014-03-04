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
    public partial class FilterPropEditor : Form
    {
        private string _entityName;
        
        public SageCRMConnection connectionObject;
        public SageCRMBaseFilterBlock Block;
        private SageCRMDataSource ds;

        public string EntryBlockName
        {
            get
            {
                return tb_entryblockname.Text;
            }
            set
            {
                tb_entryblockname.Text = value;
            }
        }
        public string BlockTitle
        {
            get
            {
                return tbBlockTitle.Text;
            }
            set
            {
                tbBlockTitle.Text = value;
            }
        }

        public string EntityName
        {
            get
            {
                return _entityName;
            }
            set
            {
                _entityName = value;
            }
        }


        public FilterPropEditor(SageCRMBaseFilterBlock _Block)
        {
            this.Block = _Block;
            this.connectionObject = this.Block.SageCRMConnection;
            InitializeComponent();
        }
        //Entity list
        private void populate_cbEntityName()
        {
            try
            {
                this.cbEntityName.Items.Clear();
                this.setConnection();
                this.ds.SelectSQL = "select distinct cobj_entityname from custom_screenobjects " +
                    "where (cobj_type='SearchScreen' or cobj_type='filterbox') " +
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

        private void cbEntityName_SelectedValueChanged(object sender, EventArgs e)
        {
            this.EntityName = cbEntityName.SelectedItem.ToString();
            lb_screens.Items.Clear();
            this.populate_lb_screens();
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
                               "where (cobj_type='SearchScreen' or cobj_type='filterbox') " +
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
        private void setConnection()
        {
            this.ds = new SageCRMDataSource(); //create our datasource
            this.ds.SageCRMConnection = this.connectionObject; //assign a connection 
        }

        private void FilterPropEditor_Load(object sender, EventArgs e)
        {
            string _url = "http://www.crmtogether.com/sage_crm_editor_bar/filter.php";
            Uri anUri = new Uri(_url);
           // webBrowser1.Url = anUri;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Block.BlockTitle = this.BlockTitle;
            this.Block.EntityName = this.EntityName;
            this.Block.EntryBlockName = this.EntryBlockName;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lb_screens_SelectedValueChanged(object sender, EventArgs e)
        {
            this.tb_entryblockname.Text = lb_screens.SelectedItem.ToString();
        }

        private void FilterPropEditor_Shown(object sender, EventArgs e)
        {
            this.populate_cbEntityName();
            this.populate_lb_screens();
        }

    }
}