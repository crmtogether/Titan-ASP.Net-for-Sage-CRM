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
    [ToolboxData("<{0}:SageCRMPortalListBlock ID='SageCRMPortalListBlock' runat=server></{0}:SageCRMPortalListBlock>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMListBlock.bmp")]
    [Designer(typeof(SageCRM.AspNet.Design.ListDesigner))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentListEditor), typeof(ComponentEditor))]
    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentListPropEditor), typeof(ComponentEditor))]
    public class SageCRMPortalListBlock : SageCRMBaseListBlock
    {
        public override string _GetHTML()
        {
                if (this.EntryBlockName != "")
                {
                    return this._GetHTML("/CustomPages/SageCRM/component/searchlist.asp",
                        "&ListBlock=" + this.ListBlock + "&EntityName=" + this.EntityName +
                        "&EntityWhere=" + this.EntityWhere + "&BlockTitle=" + this.BlockTitle +
                        "&EntryBlockName=" + this.EntryBlockName +
                        "&SelectSQL=" + this.SelectSQL, "", true, EvalCode);
                }
                else
                {
                    return this._GetHTML("list_portal.asp",
                        "&ListBlock=" + this.ListBlock + "&EntityName=" + this.EntityName +
                        "&EntityWhere=" + this.EntityWhere + "&BlockTitle=" + this.BlockTitle +
                        "&Filter=" + this.FilterBlockName +
                        "&SelectSQL=" + this.SelectSQL, "", true, EvalCode);
                }
        }
    }
}
