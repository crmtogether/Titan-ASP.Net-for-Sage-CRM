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
    [ParseChildren(true, "ListBlock")]
    [DefaultProperty("ListBlock")]
    [ToolboxData("<{0}:SageCRMListBlock ID='SageCRMListBlock' runat=server></{0}:SageCRMListBlock>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMListBlock.bmp")]
    [Designer(typeof(SageCRM.AspNet.Design.ListDesigner))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentListEditor),typeof(ComponentEditor))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentListPropEditor), typeof(ComponentEditor))]
    public class SageCRMListBlock : SageCRMBaseListBlock
    {
        private string pr_showworkflowbuttons = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Sets whether to show the workflow buttons or not")]
        public virtual bool ShowWorkFlowButtons
        {
            get
            {
                return pr_showworkflowbuttons=="Y";
            }
            set
            {
                if (value==true)
                {
                    pr_showworkflowbuttons = "Y";
                }else{
                    pr_showworkflowbuttons = "N";
                }
            }
        }
        private string pr_shownewworkflowbuttons = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Sets whether to show the workflow buttons or not")]
        public virtual bool ShowNewWorkFlowButtons
        {
            get
            {
                return pr_shownewworkflowbuttons == "Y";
            }
            set
            {
                if (value == true)
                {
                    pr_shownewworkflowbuttons = "Y";
                }
                else
                {
                    pr_shownewworkflowbuttons = "N";
                }
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Workflow Table")]
        public virtual string WorkflowTable
        {
            get
            {
                string s = (string)ViewState["WorkflowTable"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["WorkflowTable"] = value;
            }
        }
        public override string _GetHTML()
        {
            if (this.SageCRMConnection != null)
            {
                if (this.EntryBlockName != "")
                {
                    return this._GetHTML("/CustomPages/SageCRM/component/searchlist.asp",
                        "&ListBlock=" + this.ListBlock + "&EntityName=" + this.EntityName + 
                        "&EntityWhere=" + this.EntityWhere + "&BlockTitle=" + this.BlockTitle +
                        "&EntryBlockName=" + this.EntryBlockName +
                        "&SelectSQL=" + this.SelectSQL, "", true,  EvalCode);
                }
                else
                {
                    return this._GetHTML("/CustomPages/SageCRM/component/list.asp",
                        "&ListBlock=" + this.ListBlock + "&EntityName=" + this.EntityName + 
                        "&EntityWhere=" + this.EntityWhere + "&BlockTitle=" + this.BlockTitle +
                        "&ShowNewWorkFlowButtons=" + this.ShowNewWorkFlowButtons +
                        "&WorkFlowTable=" + this.WorkflowTable +
                        "&ShowWorkFlowButtons=" + this.ShowWorkFlowButtons +
                        "&Filter=" + this.FilterBlockName +
                        "&SelectSQL=" + this.SelectSQL, "", true, EvalCode);
                }
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (listblock)</div>";
            }
        }
    }
}
