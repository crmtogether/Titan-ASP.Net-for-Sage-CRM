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
    public partial class DataSourcePropEditor : Form
    {
        public SageCRMConnection connectionObject;
        public SageCRMDataSource Block;
        private SageCRMDataSource ds;

        private string _tableName = "";

        public DataSourcePropEditor(SageCRMDataSource _block)
        {
            this.Block = _block;
            this.connectionObject = this.Block.SageCRMConnection;
            InitializeComponent();
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
        public string TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
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


        private void populate_tb_idcolumn()
        {
            try
            {
                this.setConnection();
                this.ds.SelectSQL = "select bord_idfield from custom_tables where bord_name='" + this.TableName + "'";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                idr.Read();
                tb_idcolumn.Text = idr["bord_idfield"].ToString();
            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving Entity ID Column. " + ex3.Message.ToString());
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
                this.ds.SelectSQL = "select distinct cobj_entityname from custom_screenobjects " +
                    "where (cobj_type='Screen' or cobj_type='SearchScreen') " +  //we only show entities that have screens
                    "order by cobj_entityname";
                IDataReader idr = this.ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    cbEntityName.Items.Add(idr["cobj_entityname"].ToString());
                }
                this.setComboValue(cbEntityName, this.TableName);
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
                this.ds.SelectSQL = "select colp_colname from custom_edits where colp_entity='" + this.TableName + "' " +
                    "order by colp_colname";
                IDataReader idr = this.ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    rtb_columnlist.AppendText(idr["colp_colname"].ToString() + "\n");
                }

            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error getting column names. " + ex3.Message.ToString());
            }

        }
        private void setConnection()
        {
            this.ds = new SageCRMDataSource(); //create our datasource
            this.ds.SageCRMConnection = this.connectionObject; //assign a connection 
        }

        private void DataSourcePropEditor_Load(object sender, EventArgs e)
        {
            string _url = "http://www.crmtogether.com/sage_crm_editor_bar/ds.php";
            Uri anUri = new Uri(_url);
          //  webBrowser1.Url = anUri;
        }

        private void DataSourcePropEditor_Shown(object sender, EventArgs e)
        {
            this.populate_cbEntityName();
            this.populate_tb_idcolumn();
            this.popuplate_rtb_columnlist();
        }

        private void cbEntityName_SelectedValueChanged(object sender, EventArgs e)
        {
            this.TableName = cbEntityName.SelectedItem.ToString();
            this.populate_tb_idcolumn();
            this.popuplate_rtb_columnlist();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Block.TableName = this.TableName;
            this.Block.WhereClause = this.WhereClause;
            this.Block.SelectSQL = this.SelectSQL;
            DialogResult = DialogResult.OK;
            Close();

        }
    }
}