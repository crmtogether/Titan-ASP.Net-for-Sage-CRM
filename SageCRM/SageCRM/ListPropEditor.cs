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
    public partial class ListPropEditor : Form
    {
        public SageCRMConnection connectionObject;
        public SageCRMBaseListBlock Block;
        private SageCRMDataSource ds;
        private string _entityName="";

        private string _entryblockname = "";
        private string _filterblockname = "";
        private string _workflowtable = "";
        private bool _ShowWorkflowButtons = false;
        private bool _ShowNewWorkflowButtons = false;

        public void hideWorkFlow()
        {
            this.gb_workflow.Visible = false;
        }

        public void showWorkFlow()
        {
            this.gb_workflow.Visible = true;
        }

        public bool ShowWorkflowButtons
        {
            get
            {
                return this._ShowWorkflowButtons;
            }
            set
            {
                this._ShowWorkflowButtons = value;
            }
        }
        public bool ShowNewWorkflowButtons
        {
            get
            {
                return this._ShowNewWorkflowButtons;
            }
            set
            {
                this._ShowNewWorkflowButtons = value;
            }
        }

        public string WhereClause
        {
            get
            {
                return this.rtb_whereclause.Text;
            }
            set
            {
                this.rtb_whereclause.Text = value;
            }

        }
        public string SelectSQL
        {
            get
            {
                return this.rtb_selectsql.Text;
            }
            set
            {
                this.rtb_selectsql.Text = value;
            }

        }
        public string WorkFlowTable
        {
            get
            {
                return _workflowtable;
            }
            set
            {
                _workflowtable = value;
            }
        }

        public string EntryBlockName
        {
            get
            {
                return _entryblockname;
            }
            set
            {
                _entryblockname = value;
            }
        }
        public string FilterBlockName
        {
            get
            {
                return _filterblockname;
            }
            set
            {
                _filterblockname = value;
            }
        }
        public string ListBlock
        {
            get
            {
                return tbListBlock.Text;
            }
            set
            {
                tbListBlock.Text = value;
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
                _entityName=value;
            }
        }


        public ListPropEditor(SageCRMBaseListBlock _Block)
        {
            this.Block = _Block;
            this.connectionObject = this.Block.SageCRMConnection;
            InitializeComponent();
        }

        private void populate_listblock() 
        {
            lb_lists.Items.Clear();
            try
            {
                string entityFilter = "";
                if (this.EntityName != "")
                    entityFilter = "and cobj_entityname='" + this.EntityName + "' ";
                this.setConnection();
                this.ds.SelectSQL = "select cobj_name from custom_screenobjects " + //create our sql to get our entities
                               "where (cobj_type='List') " +
                               entityFilter +
                               "order by cobj_name";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    lb_lists.Items.Add(idr["cobj_name"].ToString());
                }
                //lb_lists.SelectedValue = this.ListBlock;
            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving list names. " + ex3.Message.ToString());
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
        //Entity list
        private void populate_cbEntityName()
        {
            try
            {
                this.cbEntityName.Items.Clear();
                this.setConnection();
                this.ds.SelectSQL = "select distinct cobj_entityname from custom_screenobjects "+
                    "where (cobj_type='List') " +  //we only show entities that have lists
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
        private void popuplate_rtb_columnlist()
        {
            try
            {
                rtb_columnlist.Clear();
                this.setConnection();
                this.ds.SelectSQL = "select colp_colname from custom_edits where colp_entity='"+this.EntityName+"' "+
                    "order by colp_colname";
                IDataReader idr = this.ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    rtb_columnlist.AppendText(idr["colp_colname"].ToString()+"\n");
                }

            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error getting column names. " + ex3.Message.ToString());
            }

        }
        private void populate_cb_workflowtable()
        {
            try
            {
                this.setConnection();
                this.ds.SelectSQL = "select bord_name from custom_tables where bord_workflowidfield is not null";
                IDataReader idr = this.ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    cb_workflowtable.Items.Add(idr["bord_name"].ToString());
                }

            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving workflow entity names. " + ex3.Message.ToString());
            }

        }
        private void populate_cb_entryblockname()
        {
            try
            {
                this.cb_entryblockname.Items.Clear();
                this.setConnection();
                this.ds.SelectSQL = "select distinct cobj_name from custom_screenobjects " +
                                    "where (cobj_type='Screen' or cobj_type='SearchScreen') " +
                                    " and cobj_entityname='"+this.EntityName+"' "+
                                     "order by cobj_name";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    cb_entryblockname.Items.Add(idr["cobj_name"].ToString());
                }

            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving Entry Block Name. " + ex3.Message.ToString());
            }
        }
        private void populate_tb_idcolumn()
        {
            try
            {
                this.setConnection();
                this.ds.SelectSQL = "select bord_idfield from custom_tables where bord_name='" + this.EntityName + "'";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                idr.Read();
                tb_idcolumn.Text=idr["bord_idfield"].ToString();
            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving Entity ID Column. " + ex3.Message.ToString());
            }  
        }
        private void populate_cb_filterblockname()
        {
            try
            {
                cb_filterblockname.Items.Clear();
                this.setConnection();
                this.ds.SelectSQL = "select distinct cobj_name from custom_screenobjects " +
                                    "where (cobj_type='SearchScreen' or cobj_type='filterbox') " +
                                    " and cobj_entityname='" + this.EntityName + "' " +
                                     "order by cobj_name";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    cb_filterblockname.Items.Add(idr["cobj_name"].ToString());
                }

            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving Filter Block Name. " + ex3.Message.ToString());
            }
        }
        private void setConnection()
        {
           this.ds = new SageCRMDataSource(); //create our datasource
           this.ds.SageCRMConnection = this.connectionObject; //assign a connection 
        }

        private void ListPropEditor_Shown(object sender, EventArgs e)
        {
            this.populate_cbEntityName();
            this.populate_listblock();
            this.populate_cb_workflowtable();
            this.populate_cb_entryblockname();
            this.populate_cb_filterblockname();
            this.populate_tb_idcolumn();
            this.popuplate_rtb_columnlist();
            //set the combo fields value
            this.setComboValue(cb_entryblockname, this.EntryBlockName);
            this.setComboValue(cb_filterblockname, this.FilterBlockName);
            this.setComboValue(cb_workflowtable, this.WorkFlowTable);
            this.setComboValue(cb_shownewflowbtns, this.ShowNewWorkflowButtons.ToString());
            this.setComboValue(cb_showflowbtns, this.ShowWorkflowButtons.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Block.BlockTitle = this.BlockTitle;
            this.Block.ListBlock = this.ListBlock;
            this.Block.EntityName = this.EntityName;
            this.Block.EntityWhere = this.WhereClause;
            this.Block.SelectSQL = this.SelectSQL;
            this.Block.EntryBlockName = this.EntryBlockName;
            this.Block.FilterBlockName = this.FilterBlockName;
            if (this.Block is SageCRMListBlock)
            {
                (this.Block as SageCRMListBlock).ShowNewWorkFlowButtons = this.ShowNewWorkflowButtons;
                (this.Block as SageCRMListBlock).ShowWorkFlowButtons = this.ShowWorkflowButtons;
                (this.Block as SageCRMListBlock).WorkflowTable = this.WorkFlowTable;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cbEntityName_SelectedValueChanged(object sender, EventArgs e)
        {
            this.EntityName = cbEntityName.SelectedItem.ToString();
            lb_lists.Items.Clear();
            this.populate_listblock();
            this.populate_cb_entryblockname();
            this.populate_cb_filterblockname();
            this.populate_tb_idcolumn();
            this.popuplate_rtb_columnlist();
        }

        private void lb_lists_SelectedValueChanged(object sender, EventArgs e)
        {
            tbListBlock.Text = lb_lists.SelectedItem.ToString();
        }

        private void cb_workflowtable_SelectedValueChanged(object sender, EventArgs e)
        {
            this.WorkFlowTable = cb_workflowtable.SelectedItem.ToString();
        }

        private void cb_filterblockname_SelectedValueChanged(object sender, EventArgs e)
        {
            this.FilterBlockName = cb_filterblockname.SelectedItem.ToString();
        }

        private void cb_entryblockname_SelectedValueChanged(object sender, EventArgs e)
        {
            this.EntryBlockName = cb_entryblockname.SelectedItem.ToString();
        }

        private void cb_showflowbtns_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cb_showflowbtns.SelectedItem.ToString()=="True")
            {
              this.ShowNewWorkflowButtons = true;
            }else{
              this.ShowNewWorkflowButtons = false;
            }
        }

        private void cb_shownewflowbtns_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cb_shownewflowbtns.SelectedItem.ToString() == "True")
            {
                this.ShowWorkflowButtons = true;
            }
            else
            {
                this.ShowWorkflowButtons = false;
            }
        }

        private void ListPropEditor_Load(object sender, EventArgs e)
        {
            string _url = "http://www.crmtogether.com/sage_crm_editor_bar/list.php";
            Uri anUri = new Uri(_url);
            //webBrowser1.Url = anUri;
        }

    }
}