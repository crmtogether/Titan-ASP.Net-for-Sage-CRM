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
    public partial class EntryPropEditor : Form
    {
        public SageCRMConnection connectionObject;
        public SageCRMBaseEntryBlock Block;
        private SageCRMDataSource ds;

        private string _entityName="";
        private bool _createmode = false;
        private bool _searchmode = false;
        private bool _ajaxsearchmode = false;
        private bool _showsearchlist = false;
        private bool _checklocks = true;
        private string _listblock = "";
        private bool _ShowWorkflowButtons = false;

        public string WorkFlowName
        {
            get
            {
                return tb_workflow.Text;
            }
            set
            {
                tb_workflow.Text = value;
            }
        }
        public string WFState
        {
            get
            {
                return tb_state.Text;
            }
            set
            {
                tb_state.Text = value;
            }
        }
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
        public string AfterSavePage
        {
            get
            {
                return tb_aftersavepage.Text;
            }
            set
            {
                tb_aftersavepage.Text = value;
            }
        }
        public bool CheckLocks
        {
            get
            {
                return this._checklocks;
            }
            set
            {
                this._checklocks = value;
            }
        }
        public bool CreateMode
        {
            get
            {
                return this._createmode;
            }
            set
            {
                this._createmode = value;
            }
        }
        public bool SearchMode
        {
            get
            {
                return this._searchmode;
            }
            set
            {
                this._searchmode = value;
            }
        }
        public bool AjaxSearchMode
        {
            get
            {
                return this._ajaxsearchmode;
            }
            set
            {
                this._ajaxsearchmode = value;
            }
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
        public bool ShowSearchList
        {
            get
            {
                return this._showsearchlist;
            }
            set
            {
                this._showsearchlist = value;
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

        public string ListBlock
        {
            get
            {
                return _listblock;
            }
            set
            {
                _listblock = value;
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


        public EntryPropEditor(SageCRMBaseEntryBlock _Block)
        {
            this.Block = _Block;
            this.connectionObject = this.Block.SageCRMConnection;
            if ((this.Block is SageCRMEntryBlock)==false)
            {
                lbl_checklocks.Visible = false;
                cbCheckLocks.Visible = false;
                lbl_showflowbtns.Visible = false;
                cb_showflowbtns.Visible = false;
                lbl_state.Visible = false;
                tb_state.Visible = false;
            }
            InitializeComponent();
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
                               "where (cobj_type='Screen' or cobj_type='SearchScreen')   " +
                               entityFilter +
                               "order by cobj_name";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    lb_screens.Items.Add(idr["cobj_name"].ToString());
                }
                //lb_lists.SelectedValue = this.ListBlock;
            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving list names. " + ex3.Message.ToString());
            }
        }

        private void populate_cb_Listblock() 
        {
            try
            {
                cb_Listblock.Items.Clear();
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
                    cb_Listblock.Items.Add(idr["cobj_name"].ToString());
                }
                setComboValue(cb_Listblock,this.ListBlock);
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
                    "where (cobj_type='Screen' or cobj_type='SearchScreen') " +  //we only show entities that have screens
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
        private void populate_tb_idcolumn()
        {
            try
            {
                this.setConnection();
                this.ds.SelectSQL = "select bord_idfield from custom_tables where bord_caption='" + this.EntityName + "'";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                while (idr.Read())
                {
                    tb_idcolumn.Text = idr["bord_idfield"].ToString();
                }
            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving Entity ID Column. " + ex3.Message.ToString());
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
            this.populate_cb_Listblock();
            this.populate_lb_screens();
            this.populate_tb_idcolumn();
            this.popuplate_rtb_columnlist();
            //set the combo fields value
            this.setComboValue(cbCheckLocks, this.CheckLocks.ToString());
            this.setComboValue(cb_searchmode, this.SearchMode.ToString());
            this.setComboValue(cb_ajaxsearchmode, this.AjaxSearchMode.ToString());
            this.setComboValue(cb_createmode, this.CreateMode.ToString());
            this.setComboValue(cb_showsearchlist, this.ShowSearchList.ToString());
            this.setComboValue(cb_showflowbtns, this.ShowWorkflowButtons.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Block.BlockTitle = this.BlockTitle;
            this.Block.ListBlockName = this.ListBlock;
            this.Block.EntityName = this.EntityName;
            this.Block.EntityWhere = this.WhereClause;
            this.Block.EntryBlockName = this.EntryBlockName;
            this.Block.CreateMode = this.CreateMode;
            this.Block.SearchMode = this.SearchMode;
            this.Block.ShowSearchList = this.ShowSearchList;
            this.Block.AfterSavePage = this.AfterSavePage;
            this.Block.AjaxSearchMode = this.AjaxSearchMode;
            if (this.Block is SageCRMEntryBlock)
            {
                (this.Block as SageCRMEntryBlock).ShowWorkFlowButtons = this.ShowWorkflowButtons;
                (this.Block as SageCRMEntryBlock).CheckLocks = CheckLocks;
                (this.Block as SageCRMEntryBlock).WorkFlowName = this.WorkFlowName;
                (this.Block as SageCRMEntryBlock).WFState = this.WFState;
            }
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cbEntityName_SelectedValueChanged(object sender, EventArgs e)
        {
            this.EntityName = cbEntityName.SelectedItem.ToString();
            lb_screens.Items.Clear();
            this.populate_cb_Listblock();
            this.populate_lb_screens();
            this.populate_tb_idcolumn();
            this.popuplate_rtb_columnlist();
        }

        private void lb_lists_SelectedValueChanged(object sender, EventArgs e)
        {
            this.EntryBlockName = lb_screens.SelectedItem.ToString();
        }

        private void cb_showflowbtns_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cb_createmode.SelectedItem.ToString()=="True")
            {
              this.CreateMode = true;
            }else{
                this.CreateMode = false;
            }
        }

        private void cb_shownewflowbtns_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cb_searchmode.SelectedItem.ToString() == "True")
            {
                this.SearchMode = true;
            }
            else
            {
                this.SearchMode = false;
            }

        }

        private void ListPropEditor_Load(object sender, EventArgs e)
        {
            string _url = "http://www.crmtogether.com/sage_crm_editor_bar/list.php";
            Uri anUri = new Uri(_url);
          //  webBrowser1.Url = anUri;
        }

        private void cb_Listblock_SelectedValueChanged(object sender, EventArgs e)
        {
            this.ListBlock = cb_Listblock.SelectedItem.ToString();
        }

        private void cb_showsearchlist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cb_showsearchlist.SelectedItem.ToString() == "True")
            {
                this.ShowSearchList = true;
            }
            else
            {
                this.ShowSearchList = false;
            }
        }

        private void cbCheckLocks_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cbCheckLocks.SelectedItem.ToString() == "True")
            {
                this.CheckLocks = true;
            }
            else
            {
                this.CheckLocks = false;
            }

        }

        private void cb_ajaxsearchmode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cb_ajaxsearchmode.SelectedItem.ToString() == "True")
            {
                this.AjaxSearchMode = true;
            }
            else
            {
                this.AjaxSearchMode = false;
            }

        }

        private void cb_showflowbtns_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (this.cb_showflowbtns.SelectedItem.ToString() == "True")
            {
                this.ShowWorkflowButtons = true;
            }
            else
            {
                this.ShowWorkflowButtons = false;
            }
        }

    }
}