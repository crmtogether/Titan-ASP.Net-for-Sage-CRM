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

[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
namespace SageCRM.AspNet
{
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ParseChildren(true, "EntryBlock")]
    [DefaultProperty("EntryBlock")]
    [ToolboxData("<{0}:SageCRMEntryGroup ID='SageCRMEntryGroup' runat=server></{0}:SageCRMEntryGroup>")]
    public class SageCRMEntryGroup : SageCRMEntryGroup
    {
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("EntryBlockName")]
        [Description("The name of the Entry Block (Screen)")]
        public virtual string EntryBlock
        {
            get
            {
                string s = (string)ViewState["EntryBlock"];
                return (s == null) ? "EntryBlockName" : s;
            }
            set
            {
                ViewState["EntryBlock"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("EntityName")]
        [Description("The name of the crm entity to use")]
        public virtual string EntityName
        {
            get
            {
                string s = (string)ViewState["EntityName"];
                return (s == null) ? "EntityName" : s;
            }
            set
            {
                ViewState["EntityName"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The where clause to use on the crm entity")]
        public virtual string EntityWhere
        {
            get
            {
                string s = (string)ViewState["EntityWhere"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["EntityWhere"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The title of the Entry block")]
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
            string strhtml = this._GetHTML();
            output.Write(strhtml);
        }
        public override string _GetHTML()
        {
            if (FSageCRMConnection != null)
            {
                return this._GetHTML("/CustomPages/SageCRM/component/entrygroup.asp",
                    "&EntryBlock=" + this.EntryBlock + "&EntityName=" + this.EntityName + "&EntityWhere=" + this.EntityWhere + "&BlockTitle=" + this.BlockTitle);
            }
            else
            {
                return "No SageCRMConnection set (EntryBlock)";
            }
        }
    }
}
