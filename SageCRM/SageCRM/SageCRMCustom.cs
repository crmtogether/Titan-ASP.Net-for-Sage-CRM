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
    [ParseChildren(true, "Method")]
    [DefaultProperty("Method")]
    [ToolboxData("<{0}:SageCRMCustom ID='SageCRMCustom' runat=server></{0}:SageCRMCustom>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMCustom.bmp")]
    public class SageCRMCustom : SageCRMBaseClass
    {
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Extra parameter information. Name value pairs")]
        public virtual string Params
        {
            get
            {
                string s = (string)ViewState["Params"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["Params"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The custom method to call")]
        public virtual string Method
        {
            get
            {
                string s = (string)ViewState["Method"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["Method"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The custom page to call. The page must be in the CustomPages folder")]
        public virtual string CustomPage
        {
            get
            {
                string s = (string)ViewState["CustomPage"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["CustomPage"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(this._GetHTML());
        }
        public override string _GetHTML()
        {
            if (SageCRMConnection != null)
            {
                if (CustomPage != "")
                {
                    return this._GetHTML("/CustomPages/" + CustomPage,
                        "&method=" + Method + "&" + Params, "", true);
                }else{
                    return this._GetHTML("/CustomPages/SageCRM/component/custom.asp",
                        "&method=" + Method + "&" + Params, "", true);
                }
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (SageCRMCustom)</div>";
            }
        }
    }
}
