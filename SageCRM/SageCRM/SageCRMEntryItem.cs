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
/*
[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
namespace SageCRM.AspNet
{
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ParseChildren(true, "EvalCode")]
    [DefaultProperty("EvalCode")]
    [ToolboxData("<{0}:SageCRMEntryItem ID='SageCRMEntryItem' runat=server></{0}:SageCRMEntryItem>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMEntryBlock.bmp")]
    public class SageCRMEntryItem : SageCRMBaseEntryBlock
    {
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Allows ASP be coded from ASP.Net for block Entry items")]
        public virtual string EvalCode
        {
            get
            {
                string s = (string)ViewState["EvalCode"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["EvalCode"] = value;
            }
        }     
   
                
          public override string _GetHTML()
          {
                string filename = "entryitem.asp";

                if (this.SageCRMConnection != null)
                {
                    string res= this._GetHTML("/CustomPages/SageCRM/component/" + filename,
                            "", "", true, EvalCode);
                    return res;
                }
                else
                {
                    return "<div id=\"designmode1\" style=\"background-color:DarkGray;height:50px;width:125px;\">No SageCRMConnection set (EntryItem)</div>";
                }
            }
    }
}
*/