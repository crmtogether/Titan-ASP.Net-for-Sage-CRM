using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Panoply Technologies";
        Master.setMenuIndex(5);
        Master.requiresAuthentication = true;
        if (this.securityLayer())
            return;
        string persid=SageCRMPortalListBlock.GetVisitorInfo("pers_personid");
        SageCRMPortalListBlock.SelectSQL = "SELECT * from vlistcommunication "+
            " where pers_personid=" + persid;
    }

    public bool securityLayer()
    {
        if (!this.Master.securityLayer())
        {
            SageCRMPortalListBlock.Visible = false;
            SageCRMPortalFilterBlock.Visible = false;
            return false;
        }
        return true;
    }

}
