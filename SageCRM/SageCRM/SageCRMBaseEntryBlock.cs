using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Security.Permissions;

[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
namespace SageCRM.AspNet
{
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ParseChildren(true, "EntryBlockName")]
    [DefaultProperty("EntryBlockName")]
    [ToolboxData("<{0}:SageCRMBaseEntryBlock ID='SageCRMBaseEntryBlock' runat=server></{0}:SageCRMBaseEntryBlock>")]
  //[ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMEntryBlock.bmp")]
  //[Designer(typeof(SageCRM.AspNet.Design.EntryDesigner))]
  //[Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentEntryBlockEditor), typeof(ComponentEditor))]
    abstract public class SageCRMBaseEntryBlock : SageCRMBaseClass
    {
        [Browsable(false)]
        public override string editorURL
        {
            get
            {
                string estring = EntityName;
                estring = estring.ToLower();
                string _url = pathToCRM() + CRMURL("835") + "&T=admintable" + estring + "&Parent=" + EntityName;
                //_url = _url.Replace("&", "&&");//escape the url
                return _url;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Allows ASP be coded from ASP.Net for block Entry items")]
        public virtual string EvalCode
        {
            get
            {
                string s = (string)ViewState["EvalCode"];
                return s ?? "";
            }
            set
            {
                ViewState["EvalCode"] = value;
            }
        }     

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("companyboxlong")]
        [Description("The name of the Entry Block (Screen)")]
        //[Editor(typeof(SageCRM.AspNet.Design.entryBlockPicker), typeof(UITypeEditor))]
        public virtual string EntryBlockName
        {
            get
            {
                string s = (string)ViewState["EntryBlockName"];
                return s ?? "companyboxlong";
            }
            set
            {
                ViewState["EntryBlockName"] = value;
            }
        }

        [Browsable(true), Category("Data"), DefaultValue(false), Description("The mode of the screen is set to create a record if set")]
        public virtual bool CreateMode { get; set; }

        [Browsable(true), Category("Data"), DefaultValue(false), Description("If in search mode it flags whether the list is rendered by the Entry component also")]
        public virtual bool ShowSearchList { get; set; }

        [Browsable(true), Category("Data"), DefaultValue(false), Description("The mode of the screen is set to search. It should be associated with a list")]
        public virtual bool SearchMode { get; set; }

        protected SageCRMBaseEntryBlock()
        {
            AjaxSearchMode = false;
            SearchMode = false;
            ShowSearchList = false;
            CreateMode = false;
        }

        [Browsable(true), Category("Data"), DefaultValue(false), Description("The mode of the screen is set to search. Used for ajax search only")]
        public virtual bool AjaxSearchMode { get; set; }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The name of the location to go to after saving. Only used when in CreateMode")]
        public virtual string AfterSavePage
        {
            get
            {
                string s = (string)ViewState["AfterSavePage"];
                return s ?? "";
            }
            set
            {
                ViewState["AfterSavePage"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The (optional)name of the crm list. Only used when in SearchMode")]
        public virtual string ListBlockName
        {
            get
            {
                string s = (string)ViewState["ListBlockName"];
                return s ?? "";
            }
            set
            {
                ViewState["ListBlockName"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("Company")]
        [Description("The name of the crm entity to use")]
        public virtual string EntityName
        {
            get
            {
                string s = (string)ViewState["EntityName"];
                return s ?? "Company";
            }
            set
            {
                ViewState["EntityName"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("comp_companyid=-1")]
        [Description("The where clause to use on the crm entity")]
        public virtual string EntityWhere
        {
            get
            {
                string s = (string)ViewState["EntityWhere"];
                return s ?? "comp_companyid=-1";
            }
            set
            {
                ViewState["EntityWhere"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("Company")]
        [Description("The title of the Entry block")]
        public virtual string BlockTitle
        {
            get
            {
                string s = (string)ViewState["BlockTitle"];
                return s ?? "Company";
            }
            set
            {
                ViewState["BlockTitle"] = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            showTrialML = true;
            string strhtml = _GetHTML();

            output.Write(strhtml);
        }
    }
}
