using System;
using System.Collections.Generic;
using System.Drawing.Design;
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
    abstract public class SageCRMBaseListBlock : SageCRMBaseClass
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
                string _url = this.pathToCRM() + this.CRMURL("840") + "&T=admintable" + estring + "&Parent=" + this.EntityName;
               // _url = _url.Replace("&", "&&");//escape the url
                return _url;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("CompanyGrid")]
        [Description("The name of the List Block")]
        //[Editor(typeof(SageCRM.AspNet.Design.listEditor),typeof(UITypeEditor))]
        public virtual string ListBlock
        {
            get
            {
                string s = (string)ViewState["ListBlock"];
                return (s == null) ? "CompanyGrid" : s;
            }
            set
            {
                ViewState["ListBlock"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The (optional) name of the entry block (when used as a list filter)")]
        public virtual string FilterBlockName
        {
            get
            {
                string s = (string)ViewState["FilterBlockName"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["FilterBlockName"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The (optional) name of the entry block (when used in a search screen)")]
        public virtual string EntryBlockName
        {
            get
            {
                string s = (string)ViewState["EntryBlockName"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["EntryBlockName"] = value;
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
                return (s == null) ? "Company" : s;
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
                return (s == null) ? "comp_companyid=-1" : s;
            }
            set
            {
                ViewState["EntityWhere"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The select SQL query used to populate the list")]
        public virtual string SelectSQL
        {
            get
            {
                string s = (string)ViewState["SelectSQL"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["SelectSQL"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("Company List")]
        [Description("The title of the list block")]
        public virtual string BlockTitle
        {
            get
            {
                string s = (string)ViewState["BlockTitle"];
                return (s == null) ? "Company List" : s;
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
