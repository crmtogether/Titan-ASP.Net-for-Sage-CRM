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
    [ParseChildren(true, "EntryBlockName")]
    [DefaultProperty("EntryBlockName")]
    [ToolboxData("<{0}:SageCRMPortalEntryBlock ID='SageCRMPortalEntryBlock' runat=server></{0}:SageCRMPortalEntryBlock>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMEntryBlock.bmp")]
    [Designer(typeof(SageCRM.AspNet.Design.EntryDesigner))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentEntryBlockEditor), typeof(ComponentEditor))]
    public class SageCRMPortalEntryBlock : SageCRMBaseEntryBlock
    {
        private string pr_stopdefaultaction = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("If true the block does not change mode")]
        public virtual bool StopDefaultAction
        {
            get
            {
                return pr_stopdefaultaction == "Y";
            }
            set
            {
                if (value == true)
                {
                    pr_stopdefaultaction = "Y";
                }
                else
                {
                    pr_stopdefaultaction = "N";
                }
            }
        }

        private bool pr_editMode = false;
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Specifies whether the screen renders in edit mode")]
        public virtual bool ScreenEditMode
        {
            get { return pr_editMode; }
            set { pr_editMode = value; }
        }

        private bool pr_saveMode = false;
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Specifies whether the screen renders in edit mode")]
        public virtual bool ScreenSaveMode
        {
            get { return pr_saveMode; }
            set { pr_saveMode = value; }
        }
        public override string _GetHTML()
        {
            string filename = "entrygroup_portal.asp";
            string listblock = "";
            string showlist = "";
            if (this.SageCRMConnection != null)
            {
                if (this.CreateMode == true)
                {
                    filename = "entrygroupcreate_portal.asp";
                }
                if (this.SearchMode == true)
                {
                    filename = "entrygroupsearch_portal.asp";
                    listblock = this.ListBlockName;
                    if (this.ShowSearchList == true)
                    {
                        showlist = "&showlist=Y";
                    }
                }
                return this._GetHTML(filename,
                        "&EntryBlock=" + this.EntryBlockName +
                        "&EntityName=" + this.EntityName +
                        "&EntityWhere=" + this.EntityWhere +
                        "&BlockTitle=" + this.BlockTitle +
                        "&AfterSavePage=" + this.AfterSavePage +
                        "&ListBlock=" + listblock +
                        showlist, "", true, EvalCode);
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (PortalEntryBlock)</div>";
            }
        }
    }
}
