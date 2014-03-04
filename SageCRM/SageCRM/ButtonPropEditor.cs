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
    public partial class ButtonPropEditor : Form
    {
        public SageCRMConnection connectionObject;
        public SageCRMButton Block;
        private SageCRMDataSource ds;
        private string _imagename;
        private string _entityname;

        public string Caption
        {
            get
            {
                return this.tb_caption.Text;
            }
            set
            {
                this.tb_caption.Text=value;
            }
        }
        public string Url
        {
            get
            {
                return this.tb_urlscript.Text;
            }
            set
            {
                this.tb_urlscript.Text = value;
            }
        }
        public string Target
        {
            get
            {
                return this.cb_target.Text;
            }
            set
            {
                this.cb_target.Text = value;
            }
        }
        public string PermissionsEntity
        {
            get
            {
                return _entityname;
            }
            set
            {
                this._entityname = value;
            }
        }
        public string PermissionsType
        {
            get
            {
                return this.cb_permtype.Text;
            }
            set
            {
                this.cb_permtype.Text = value;
            }
        }
        public string ImageName
        {
            get
            {
                return this._imagename;
            }
            set
            {
                this._imagename = value;
            }
        }
        public ButtonPropEditor(SageCRMButton _Block)
        {
            this.Block = _Block;
            this.connectionObject = this.Block.SageCRMConnection;
            InitializeComponent();
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
                this.cb_EntityName.Items.Clear();
                cb_EntityName.Items.Add("");
                this.setConnection();
                this.ds.SelectSQL = "select distinct cobj_entityname from custom_screenobjects " +
                    "where cobj_type='TabGroup' " +  //we only show entities that have lists
                    "order by cobj_entityname";
                IDataReader idr = this.ds.SelectData();  //get our IDataReader class
                //loop through our class
                while (idr.Read())
                {
                    cb_EntityName.Items.Add(idr["cobj_entityname"].ToString());
                }
                this.setComboValue(cb_EntityName, this.PermissionsEntity);
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
        private void setListValue(ListBox cb, string value)
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
        private void populate_lb_buttonimages()
        {
            SageCRMCustom FSageCRMCustom;
            string imageliststr = "";
            string[] imageListArr;
            string[] stringSeparators = new string[] { "," };
            try
            {
                lb_buttonimages.Items.Clear();
                if (this.connectionObject != null)
                {
                    FSageCRMCustom = new SageCRMCustom(); //we use this to make our request
                    FSageCRMCustom.SageCRMConnection = connectionObject;
                    imageliststr =FSageCRMCustom._GetHTML("/CustomPages/SageCRM/component/imagelist.asp", "", "", false);
                    imageListArr = imageliststr.Split(stringSeparators, StringSplitOptions.None);
                    for (int i = 0; i < imageListArr.Length; i++)
                    {
                        lb_buttonimages.Items.Add(imageListArr[i].ToString());
                    }
                    setListValue(lb_buttonimages,this.ImageName);
                }
                else
                {
                    MessageBox.Show("No SageCRMConnection set (populate_lb_buttonimages Method)");
                }

            }
            catch (Exception ex3)
            {
                MessageBox.Show("Error retrieving image list. " + ex3.Message.ToString());
            }
        }

        private void ButtonPropEditor_Load(object sender, EventArgs e)
        {
            string _url = "http://www.crmtogether.com/sage_crm_editor_bar/button.php";
            Uri anUri = new Uri(_url);
            //webBrowser1.Url = anUri;
        }

        private void ButtonPropEditor_Shown(object sender, EventArgs e)
        {
            populate_cbEntityName();
            populate_lb_buttonimages();
        }

        private void lb_buttonimages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show the preview
            string _url = this.connectionObject.CRMPath + "/Img/Buttons/" + lb_buttonimages.SelectedItem.ToString();
            Uri anUri = new Uri(_url);
            webBrowser2.Url = anUri;
            this.ImageName=lb_buttonimages.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Block.Caption = this.Caption;
            this.Block.ImageName = this.ImageName;
            this.Block.PermissionsEntity = this.PermissionsEntity;
            this.Block.PermissionsType = this.PermissionsType;
            this.Block.Url = this.Url;
            this.Block.Target = this.Target;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cb_EntityName_SelectedValueChanged(object sender, EventArgs e)
        {
            this.PermissionsEntity = cb_EntityName.SelectedItem.ToString();
        }

        private void cb_permtype_SelectedValueChanged(object sender, EventArgs e)
        {
            this.PermissionsType = cb_permtype.SelectedItem.ToString();
        }

        private void cb_target_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Target = cb_target.SelectedItem.ToString();
        }
    }
}