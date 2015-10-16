using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Security.Permissions;
using System.IO;
using System.Net;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
namespace SageCRM.AspNet
{
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ParseChildren(true, "EntryBlockName")]
    [DefaultProperty("EntryBlockName")]
    [ToolboxData("<{0}:SageCRMBaseFilterBlock ID='SageCRMBaseFilterBlock' runat=server></{0}:SageCRMBaseFilterBlock>")]
  //  [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMFilterBlock.bmp")]
//    [Designer(typeof(SageCRM.AspNet.Design.FilterDesigner))]
//    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentFilterBlockEditor), typeof(ComponentEditor))]
    abstract public class SageCRMBaseFilterBlock : SageCRMBaseClass
    {
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Allows ASP be coded from ASP.Net for block Entry items")]
        public virtual string EvalCode
        {
            get
            {
                string s = (string)ViewState["EvalCode"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["EvalCode"] = value;
            }
        }     



        [Browsable(false)]
        public override string editorURL
        {
            get
            {
                string estring = this.EntityName;
                estring = estring.ToLower();
                string _url = this.pathToCRM() + this.CRMURL("835") + "&T=admintable" + estring + "&Parent=" + this.EntityName;
                //_url = _url.Replace("&", "&&");//escape the url
                return _url;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("CaseFilterBox")]
        [Description("The name of the Filter Entry Block (Screen)")]
        public virtual string EntryBlockName
        {
            get
            {
                string s = (string)ViewState["EntryBlockName"];
                return (s == null) ? "CaseFilterBox" : s;
            }
            set
            {
                ViewState["EntryBlockName"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("Cases")]
        [Description("The name of the crm entity to use")]
        public virtual string EntityName
        {
            get
            {
                string s = (string)ViewState["EntityName"];
                return (s == null) ? "Cases" : s;
            }
            set
            {
                ViewState["EntityName"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The title of the Filter Entry block")]
        public virtual string BlockTitle
        {
            get
            {
                string s = (string)ViewState["BlockTitle"];
                return (s == null) ? "" : s;
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
            string strhtml = this._GetHTML();
            output.Write(strhtml);
        }
    }
}
