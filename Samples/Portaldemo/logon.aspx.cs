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

        this.lbl_logoninfo.Text = "If you are an existing Panoply Technologies customer please log on here:";
        Response.Write(Response.Cookies.Count);
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool logonresult=SageCRMBaseClass1.PortalLogon(Login1.UserName.ToString(), Login1.Password.ToString());
        e.Authenticated = logonresult;
    }
}
