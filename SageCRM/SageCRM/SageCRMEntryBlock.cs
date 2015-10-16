using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Security.Permissions;
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
    [ToolboxData("<{0}:SageCRMEntryBlock ID='SageCRMEntryBlock' runat=server></{0}:SageCRMEntryBlock>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMEntryBlock.bmp")]
    [Designer(typeof(SageCRM.AspNet.Design.EntryDesigner))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentEntryBlockEditor), typeof(ComponentEditor))]
    public class SageCRMEntryBlock : SageCRMBaseEntryBlock
    {
        private string pr_checkLocks = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Specifies whether the system checks if a record is in use")]
        public virtual bool CheckLocks
        {
            get { return pr_checkLocks == "Y"; }
            set { pr_checkLocks = value ? "Y" : "N"; }
        }

        /*
        private string pr_udb = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Specifies whether the system display buttons by default")]
        public virtual bool UseDefaultButtons
        {
            get
            {
                return pr_udb == "Y";
            }
            set
            {
                if (value)
                {
                    pr_udb = "Y";
                }
                else
                {
                    pr_udb = "N";
                }
            }
        }
*/
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The name of the workflow to be used when creating the entity. Only valid in New mode.")]
        public virtual string WorkFlowName
        {
            get
            {
                string s = (string)ViewState["WorkFlowName"];
                return s ?? "";
            }
            set
            {
                ViewState["WorkFlowName"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The initial workflow state to set the record to. Only valid in New mode and when WorkFlowName is set.")]
        public virtual string WFState
        {
            get
            {
                string s = (string)ViewState["WFState"];
                return s ?? "";
            }
            set
            {
                ViewState["WFState"] = value;
            }
        }
        private string pr_showworkflowbuttons = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Sets whether to show the workflow links or not")]
        public virtual bool ShowWorkFlowButtons
        {
            get { return pr_showworkflowbuttons == "Y"; }
            set { pr_showworkflowbuttons = value ? "Y" : "N"; }
        }
        private string pr_stopdefaultaction = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("If true the block does not change mode")]
        public virtual bool StopDefaultAction
        {
            get { return pr_stopdefaultaction == "Y"; }
            set { pr_stopdefaultaction = value ? "Y" : "N"; }
        }
        public override string _GetHTML()
        {
            string filename = "entrygroup.asp";
            string listblock = "";
            string showlist = "";

            if (SageCRMConnection != null)
            {
                if (CreateMode)
                    filename = "entrygroupcreate.asp";
                
                if (SearchMode)
                {
                    filename = "entrygroupsearch.asp";
                    listblock = ListBlockName;
                    if (ShowSearchList)
                        showlist = "&showlist=Y";
                }
                
                if (AjaxSearchMode)
                    showlist += "&AjaxSearch=True";
                
                return _GetHTML("/CustomPages/SageCRM/component/" + filename,
                        "&EntryBlock=" + EntryBlockName +
                        "&CheckLocks=" + CheckLocks +
                        "&EntityName=" + EntityName +
                        "&EntityWhere=" + EntityWhere +
                        "&BlockTitle=" + BlockTitle +
                        "&AfterSavePage=" + AfterSavePage +
                        "&ListBlock=" + listblock +
                        "&ShowWorkFlowButtons=" + ShowWorkFlowButtons.ToString() +
                        "&WorkflowName=" + WorkFlowName +
                        "&WFState=" + WFState +
                        "&E=" + EntityName +
                      //"&udb=" + UseDefaultButtons +    ///use default buttons
                        showlist, "", true, EvalCode);
            }

            return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (EntryBlock)</div>";
        }
    }
}
