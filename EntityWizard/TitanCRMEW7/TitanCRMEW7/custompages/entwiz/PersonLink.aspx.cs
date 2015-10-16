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

public partial class PersonLink : System.Web.UI.Page
{
    public string Comp_CompanyId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SageCRMEntryItem.EvalCode = "eWare.Mode =1;";
        this.SageCRMEntryItem.EvalCode += "Group = eWare.GetBlock(\"entrygroup\");";
        this.SageCRMEntryItem.EvalCode += "PersonSearch = eWare.GetBlock(\"entry\");";
        this.SageCRMEntryItem.EvalCode += "PersonSearch.FieldName = \"SearchPerson\";";
        this.SageCRMEntryItem.EvalCode += "PersonSearch.EntryType = 56;";
        this.SageCRMEntryItem.EvalCode += "PersonSearch.LookupFamily = \"Person\";";
        this.SageCRMEntryItem.EvalCode += "Group.AddEntry(PersonSearch);";
        this.SageCRMEntryItem.EvalCode += "Group.DisplayForm=false;";
        this.SageCRMEntryItem.EvalCode += "Response.Write(Group.Execute());";

        // Response.Write(Request.Form.ToString());
        if (Request.Form["SearchPerson"] != null)
        {
            this.Comp_CompanyId = Request.Form["SearchPerson"].ToString();
        }
        if (this.Comp_CompanyId != "")
        {
            SageCRMDataSource1.TableName = "Person";
            SageCRMDataSource1.WhereClause = getWhereClause();
            SageCRMDataSource1.Parameters.Add("pers_xxxxxxxxxId", getEntityIDFieldValue());
            SageCRMDataSource1.UpdateRecord();
            Response.Redirect(SageCRMTabGroup.CRMURL("entwiz/PeopleList.aspx").ToString() + "&" + getEntityIDField()
                + "=" + getEntityIDFieldValue());

        }
    }
    public string getEntityIDField()
    {
        return "xxxx_xxxxxxxxxid";
    }
    public string getEntityIDFieldValue()
    {
        string res = Request.QueryString.Get(this.getEntityIDField());
        if ((res != null) && (res != ""))
        {
            Session.Add(getEntityIDField(), res);
        }
        else
        {
            if (Session[getEntityIDField()] == null)
            {
                //redirect to user page
                Response.Redirect(this.SageCRMTabGroup.CRMURL("183"));
            }
            res = Session[this.getEntityIDField()].ToString();
        }
        return res;
    }
    public string getWhereClause()
    {
        this.Comp_CompanyId = Request.Form.Get("SearchPerson");
        return "pers_personId=" + this.Comp_CompanyId;
    }

}
