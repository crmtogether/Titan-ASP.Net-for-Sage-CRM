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
    [ToolboxData("<{0}:SageCRMPortalFilterBlock ID='SageCRMPortalFilterBlock' runat=server></{0}:SageCRMPortalFilterBlock>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMFilterBlock.bmp")]
    [Designer(typeof(SageCRM.AspNet.Design.FilterDesigner))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentFilterBlockEditor), typeof(ComponentEditor))]
    public class SageCRMPortalFilterBlock : SageCRMBaseFilterBlock
    {
        protected override void RenderContents(HtmlTextWriter output)
        {
            string strhtml = this._GetHTML();
            output.Write(strhtml);
        }
        public override string _GetHTML()
        {
            string filename = "filter_portal.asp";
            if (this.SageCRMConnection != null)
            {
                return this._GetHTML(filename,
                        "&EntryBlock=" + this.EntryBlockName +
                        "&EntityName=" + this.EntityName +
                        "&BlockTitle=" + this.BlockTitle, "", true, EvalCode);
            }
            else
            {
                return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (PortalFilterBlock)</div>";
            }
        }
    }
}
