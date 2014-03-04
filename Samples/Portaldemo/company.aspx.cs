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
using System.ComponentModel;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Panoply Technologies";

        Master.setMenuIndex(2);
        Master.requiresAuthentication = true;
        if (!this.securityLayer())
            return;
        string compid = SageCRMPortalEntryBlock.GetVisitorInfo("comp_companyid");
        string persid = SageCRMPortalEntryBlock.GetVisitorInfo("pers_personid");
        SageCRMPortalEntryBlock.EntityWhere = "comp_companyid=" + compid;
        lbl_companyinfo.Text = "Below is your company information that we contain on our databse.<br />"+
            "<br />Please verify the information to help us ensure that it is up to date."+
            "<br />Only the primary contact in your company can edit this data." +
            "";
        SageCRMDataSource1.TableName = "Company";
        SageCRMDataSource1.WhereClause = "comp_companyid=" + compid;
        IDataReader idr = SageCRMDataSource1.SelectData();
        //only the primaru person can edit the company information
        idr.Read();
        string ppersid = idr["comp_primarypersonid"] as string;
        if (ppersid != persid)
        {
            Button1.Visible = false;
        }
        else
        {
            Button1.Visible = true;
        }
    }

    public bool securityLayer()
    {       
        if (!this.Master.securityLayer())
        {
            SageCRMPortalEntryBlock.Visible = false;
            Button1.Visible = false;
            lbl_companyinfo.Text = "";
            return false;
        }
        return true;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.Button1.Text == "Change")
        {
            this.Button1.Text = "Save";
        }
        else
        {
            this.Button1.Text = "Change";
        }
    }
}
