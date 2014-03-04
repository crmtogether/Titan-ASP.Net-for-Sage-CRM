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

public partial class List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //SET UP THE TAB GROUP
        //get the prime entity "Key0" on the query string tells you this
        //1 = company
        //2 = person
        string PrimaryKey = "";
        if ((Request.QueryString.Get("Key0") != null) &&
            (Request.QueryString.Get("Key0").ToString() != ""))
        {
            PrimaryKey = Request.QueryString.Get("Key0").ToString();
        }
        if (PrimaryKey == "2")
        {
            this.SageCRMTabGroup.EntityName = "person";
        }
        else
        {
            this.SageCRMTabGroup.EntityName = "company";
        }
        //SET UP THE LIST
        if (PrimaryKey == "2")
        {
            this.SageCRMListBlock.EntityWhere = "proj_personid=" + this.SageCRMListBlock.GetContextInfo("person", "pers_personid");
        }
        else
        {
            this.SageCRMListBlock.EntityWhere = "proj_companyid=" + this.SageCRMListBlock.GetContextInfo("company", "comp_companyid");
        }
    }
}
