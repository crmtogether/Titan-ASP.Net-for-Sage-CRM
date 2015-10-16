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
    [ToolboxData("<{0}:SageCRMTopContent ID='SageCRMTopContent' runat=server></{0}:SageCRMTopContent>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMTopContent.bmp")]
    [Designer(typeof(SageCRM.AspNet.Design.TopContentDesigner))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentTopContentEditor), typeof(ComponentEditor))]
    public class SageCRMTopContent : SageCRMBaseClass
    {
        [Browsable(false)]
        public override string editorURL
        {
            get
            {
                string estring = this.EntityName;
                estring = estring.ToLower();
                string _url = this.pathToCRM() + this.CRMURL("835") + "&T=admintable" + estring + "&Parent=" + this.EntityName;
                _url = _url.Replace("&", "&&");//escape the url
                return _url;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("CompanyTopContent")]
        [Description("The name of the Entry Block (Screen)")]
        public virtual string EntryBlockName
        {
            get
            {
                string s = (string)ViewState["EntryBlockName"];
                return (s == null) ? "CompanyTopContent" : s;
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
        [DefaultValue("company.gif")]
        [Description("The name of the entity Icon (image) to use")]
        public virtual string Icon
        {
            get
            {
                string s = (string)ViewState["Icon"];
                return (s == null) ? "company.gif" : s;
            }
            set
            {
                ViewState["Icon"] = value;
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
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            string strhtml = this._GetHTML();
            output.Write(strhtml);
        }
        public override string _GetHTML()
        {
            StringBuilder sb = new StringBuilder();

            if (this.SageCRMConnection != null)
            {
                string html= this._GetHTML("/CustomPages/SageCRM/component/topcontent.asp",
                    "&EntryBlockName=" + this.EntryBlockName + "&EntityName=" + this.EntityName +
                    "&EntityWhere=" + this.EntityWhere + "&icon=" + this.Icon, "", true);
                return html;
#if false
                //old code
                if (DesignMode)
                {
                    return html;
                }
                else
                {
                    sb.Append("<script type=\"text/javascript\">");
                    sb.Append("parent.frames[3].WriteToFrame(5,'TOPBODY VLINK=#003B72 LINK=#003B72','" + html + "');");
                    sb.Append("</script>");
                    string dmstr = sb.ToString();
                    dmstr = dmstr.Replace("\n","");
                    dmstr = dmstr.Replace("\r", "");
                    return dmstr;
                }
#endif
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (TopContent)</div>";
            }
        }
    }
}
