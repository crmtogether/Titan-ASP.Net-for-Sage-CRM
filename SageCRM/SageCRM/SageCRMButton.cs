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
    [ParseChildren(true, "Caption")]
    [DefaultProperty("Caption")]
    [ToolboxData("<{0}:SageCRMButton ID='SageCRMButton' runat=server></{0}:SageCRMButton>")]
    [ToolboxBitmap(typeof(ImageButton))]
    [Designer(typeof(SageCRM.AspNet.Design.ButtonDesigner))]
    public class SageCRMButton : SageCRMBaseClass
    {
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Caption")]
        public virtual string Caption
        {
            get
            {
                string s = (string)ViewState["Caption"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["Caption"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("Continue.gif")]
        [Description("")]
        public virtual string ImageName
        {
            get
            {
                string s = (string)ViewState["ImageName"];
                return (s == null) ? "Continue.gif" : s;
            }
            set
            {
                ViewState["ImageName"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The Url (or Action) that the button links to")]
        public virtual string Url
        {
            get
            {
                string s = (string)ViewState["Url"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["Url"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("PermissionsEntity is the name of the entity")]
        public virtual string PermissionsEntity
        {
            get
            {
                string s = (string)ViewState["PermissionsEntity"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["PermissionsEntity"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("PermissionsType is either VIEW, EDIT,DELETE or INSERT")]
        public virtual string PermissionsType
        {
            get
            {
                string s = (string)ViewState["PermissionsType"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["PermissionsType"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("")]
        public virtual string Target
        {
            get
            {
                string s = (string)ViewState["Target"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["Target"] = value;
            }
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(this._GetHTML());
        }
        public override string _GetHTML()
        {
            if (this.SageCRMConnection != null)
            {
                
                return this._GetHTML("/CustomPages/SageCRM/component/button.asp",
                    "&Caption=" + Caption +
                    "&Url=" + Url +
                    "&ImageName=" + ImageName +
                    "&PermissionsEntity=" + PermissionsEntity +
                    "&PermissionsType=" + PermissionsType +
                    "&Target=" + Target, "", true);
                
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (SageCRMButton)</div>";
            }
        }
    }
}
