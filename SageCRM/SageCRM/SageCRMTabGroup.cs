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
    [ParseChildren(true, "TabGroupName")]
    [DefaultProperty("TabGroupName")]
    [ToolboxData("<{0}:SageCRMTabGroup ID='SageCRMTabGroup' runat=server></{0}:SageCRMTabGroup>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMTabGroup.bmp")]
    [Designer(typeof(SageCRM.AspNet.Design.TabDesigner))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentTabGroupEditor), typeof(ComponentEditor))]
    public class SageCRMTabGroup : SageCRMBaseClass
    {
        [Browsable(false)]
        public override string editorURL
        {
            get
            {
                string estring = this.EntityName;
                estring = estring.ToLower();
                string _url = this.pathToCRM() + this.CRMURL("845") + "&T=admintable" + estring + "&Parent=" + this.EntityName;
             //   _url = _url.Replace("&", "&&");//escape the url
                return _url;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("Company")]
        [Description("The name of the Sage CRM Tab Group")]
        public virtual string TabGroupName
        {
            get
            {
                string s = (string)ViewState["TabGroupName"];
                return (s == null) ? "Company" : s;
            }
            set
            {
                ViewState["TabGroupName"] = value;
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
        public override string _GetHTML()
        { 
            if (this.SageCRMConnection != null)
            {
                string strParams = "&crmtabgroup=" + this.TabGroupName;
                return this._GetHTML("/CustomPages/SageCRM/component/tabgroup.asp",
                    strParams, "", true);
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (SageCRMTabGroup)</div>";
            }
        }
    }
}
