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

    string oppo_opportunityid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Panoply Technologies";

        Master.setMenuIndex(4);
        Master.requiresAuthentication = true;
        if (!this.securityLayer())
            return;
        SageCRMPortalEntryBlock.CreateMode = true;
        SageCRMPortalEntryBlock.EntityWhere = "";

        if (Request.QueryString.Get("nrid") != null)
            oppo_opportunityid = Request.QueryString.Get("nrid");
        if (Request.QueryString.Get("oppo_opportunityid") != null)
            oppo_opportunityid = Request.QueryString.Get("oppo_opportunityid");

        if (oppo_opportunityid.Length > 0)
        {
            SageCRMPortalEntryBlock.CreateMode = false;
            SageCRMPortalEntryBlock.EntityWhere = "oppo_opportunityid=" + oppo_opportunityid;
        }
        else
        {
            this.Button1.Text = "Save";
        }

    }
    public bool securityLayer()
    {

        if (!this.Master.securityLayer())
        {
            SageCRMPortalEntryBlock.Visible = false;
            Button1.Visible = false;
            DataList1.Visible = false;
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
    protected void SageCRMPortalEntryBlock_BeforeRendering(object source, ref string HTMLSource)
    {
        //extract the new record id...
        //when createrecord mode is set and a new record created
        //the Entryblock returns "<recordid>newid</recordid>"....
        //eg "<recordid>123</recordid>"
        if (HTMLSource.StartsWith("<recordid>") == true)
        {
            //parse out the new record id.
            //You could possibly parse out this using some xml objects
            int iEnd = HTMLSource.IndexOf("</recordid>");
            oppo_opportunityid = HTMLSource.Substring(10, iEnd - 10);
            //update the record with the default values
            SageCRMDataSource2.WhereClause = "oppo_opportunityid=" + oppo_opportunityid;
            SageCRMDataSource2.Parameters.Add("oppo_primarycompanyid", SageCRMPortalEntryBlock.GetVisitorInfo("comp_companyid"));
            SageCRMDataSource2.Parameters.Add("oppo_primarypersonid", SageCRMPortalEntryBlock.GetVisitorInfo("pers_personid"));
            SageCRMDataSource2.UpdateRecord();
            //redirect to the details page with the new id
            Response.Redirect("opportunitydetail.aspx?oppo_opportunityid=" + oppo_opportunityid);
        }
    }
}
