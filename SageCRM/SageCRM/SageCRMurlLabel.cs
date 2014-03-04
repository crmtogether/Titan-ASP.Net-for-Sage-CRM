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
    [ParseChildren(true, "Text")]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:SageCRMurlLabel ID='SageCRMurlLabel' Text='SageCRMurlLabel' runat=server></{0}:SageCRMurlLabel>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMurlLabel.bmp")]
    public class SageCRMurlLabel : SageCRMBaseClass 
    {

        private string FURL="";
        [Browsable(true)]
        [Bindable(true)]
        [Category("Data")]
        [DefaultValue("YourPageHere.aspx")]
        [Description("The page location to be used as the link")]
        public virtual string PageLocation
        {
            get
            {
                string s = (string)ViewState["PageLocation"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["PageLocation"] = value;
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Link name that is displayed")]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.A;
            }
        }
        protected override void AddAttributesToRender(
             HtmlTextWriter writer)
        {
            FURL = this._GetHTML();
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Href,
                FURL);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (Text == String.Empty)
            {
                Text = FURL;
            }
            output.Write(Text);
        }
        public override string _GetHTML() {
            if (SageCRMConnection != null)
            {
                return this._GetHTML("/CustomPages/SageCRM/component/url.asp",
                    "&url=" + PageLocation, "", true);
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (SageCRMurlLabel)</div>";
            }
        }
    }
}
