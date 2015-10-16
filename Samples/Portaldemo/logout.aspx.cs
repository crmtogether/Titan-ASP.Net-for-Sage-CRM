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

        Master.requiresAuthentication = true;
        SageCRMBaseClass1.PortalLogout();
        lbl_logoutinfo.Text = "You are now logged out. Thank you for using the Panoply Technologies Portal";
    }
}
