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
    public partial class crmEditor : Form
    {
        private SageCRMBaseListBlock _sagecrmlistblock;
        private SageCRMBaseEntryBlock _sagecrmentryblock;
        private SageCRMBaseFilterBlock _sagecrmfilterblock;
        private SageCRMTabGroup _sagecrmtabgroup;
        private SageCRMBaseClass _block;
        private SageCRMTopContent _sagecrmtopcontent;

        public crmEditor(SageCRMBaseListBlock listcomponent, SageCRMBaseEntryBlock entrycomponent, SageCRMBaseFilterBlock filtercomponent, SageCRMTabGroup tabcomponent, SageCRMTopContent topcontentcomponent)
        {
            InitializeComponent();
            this._block = listcomponent;
            string entityname = "";
            if (entrycomponent != null)
            {
                _block = entrycomponent;
                _sagecrmentryblock = entrycomponent;
                label1.Text = "Active Screen: "+(_block as SageCRMBaseEntryBlock).EntryBlockName;
                entityname = (_block as SageCRMBaseEntryBlock).EntityName;
            }
            else 
            if (listcomponent != null)
            {
                _block = listcomponent;
                _sagecrmlistblock = listcomponent;
                label1.Text = "Active List: " + (_block as SageCRMBaseListBlock).ListBlock;
                entityname = (_block as SageCRMBaseListBlock).EntityName;
            } else
            if (filtercomponent != null)
            {
                _block = filtercomponent;
                _sagecrmfilterblock = filtercomponent;
                label1.Text = "Active Filter: " + (_block as SageCRMBaseFilterBlock).EntryBlockName;
                entityname = (_block as SageCRMBaseFilterBlock).EntityName;
            }
            else if (tabcomponent != null)
            {
                _block = tabcomponent;
                _sagecrmtabgroup = tabcomponent;
                label1.Text = "Active Tab: " + (_block as SageCRMTabGroup).TabGroupName;
                entityname = (_block as SageCRMTabGroup).EntityName;
            }
            else if (topcontentcomponent != null)
            {
                _block = topcontentcomponent;
                _sagecrmtopcontent = topcontentcomponent;
                label1.Text = "Active Tab: " + (_block as SageCRMTopContent).EntryBlockName;
                entityname = (_block as SageCRMTopContent).EntityName;
            }
            string _url = _block.editorURL.ToString();
            Uri anUri = new Uri(_url);
            textBox1.Text = _url;

            //due to a persistance issue we must first set the context of the entity that we are working on
            string persistance_url = _block.pathToCRM() + _block.CRMURL("1651") + "MenuName=&BC=Admin,Admin,AdminCustomization,Customization,," + entityname + "&Parent=" + entityname + "&Act2=830";
            Uri persistance_anUri = new Uri(persistance_url);
            webBrowser1.Url = persistance_anUri;

            //sleep for a second before navigating to the correct entity
            System.Threading.Thread.Sleep(1000);
            webBrowser1.Url = anUri;

            string _url2 = "http://www.crmtogether.com/sage_crm_editor_bar/crmeditor.php";
            Uri anUri2 = new Uri(_url2);
           // webBrowser2.Url = anUri2;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}