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

public partial class portal : System.Web.UI.MasterPage
{
    public bool requiresAuthentication=false;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_welcomenote.Text = " <a href=\"logon.aspx\" >Logon</a>";
        if (this.securityLayer())
        {
            lbl_welcomenote.Text = "Welcome to Panoply Technologies Support. You are currently logged in as " +
                SageCRMBaseClass1.GetVisitorInfo("pers_fullname");
            lbl_welcomenote.Text += " <a href=\"logout.aspx\" >Logout</a>";
        }else
        {
            if (this.requiresAuthentication)
            {
                this.setErrorMessage();
                lbl_welcomenote.Text = " <a href=\"logon.aspx\" >Logon</a>";
            }
        }
    }
    public bool securityLayer()
    {
        if (!SageCRMBaseClass1.Authenticated())
        {
            return false;
        }
        return true;
    }
    public void setErrorMessage()
    {
        this.lbl_errmsg.Text = "You must be logged onto the system to access this page";
    }
    public void setMenuIndex(int iSelected)
    {
        this.GridView1.SelectedIndex = iSelected;
    }
}
