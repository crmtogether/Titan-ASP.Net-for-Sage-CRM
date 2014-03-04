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

        Master.setMenuIndex(3);
        Master.requiresAuthentication = true;
        if (!this.securityLayer())
            return;
        string compid = SageCRMPortalListBlock.GetVisitorInfo("comp_companyid");
        SageCRMPortalListBlock.EntityWhere = "pers_companyid="+compid;
        lbl_personinfo.Text = "This is a list of the people within your company that we have.";
        SageCRMDataSource1.SelectSQL = "select count(*) as 'totalpeople' from person where pers_companyid=" + compid;
    }
    public bool securityLayer()
    {

        if (!this.Master.securityLayer())
        {
            SageCRMPortalListBlock.Visible = false;
            GridView1.Visible = false;
            lbl_personinfo.Text = "";
            return false;
        }
        return true;
    }

}
