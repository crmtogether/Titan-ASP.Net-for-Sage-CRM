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

public partial class CompanyLink : System.Web.UI.Page
{
    public string Comp_CompanyId="";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SageCRMEntryItem.EvalCode = "eWare.Mode =1;";
        this.SageCRMEntryItem.EvalCode += "Group = eWare.GetBlock(\"entrygroup\");";
        this.SageCRMEntryItem.EvalCode += "CompanySearch = eWare.GetBlock(\"entry\");";
        this.SageCRMEntryItem.EvalCode += "CompanySearch.FieldName = \"SearchCompany\";";
        this.SageCRMEntryItem.EvalCode += "CompanySearch.EntryType = 56;";
        this.SageCRMEntryItem.EvalCode += "CompanySearch.LookupFamily = \"Company\";";
        this.SageCRMEntryItem.EvalCode += "Group.AddEntry(CompanySearch);";
        this.SageCRMEntryItem.EvalCode += "Group.DisplayForm=false;";
        this.SageCRMEntryItem.EvalCode += "Response.Write(Group.Execute());";

       // Response.Write(Request.Form.ToString());
        if (Request.Form["SearchCompany"] != null)
        {
            this.Comp_CompanyId = Request.Form["SearchCompany"].ToString();
        }
        if (this.Comp_CompanyId != "") 
        {
           SageCRMDataSource1.TableName = "Company";
           SageCRMDataSource1.WhereClause = getWhereClause();
           SageCRMDataSource1.Parameters.Add("Comp_xxxxxxxxxId", getEntityIDFieldValue());
           SageCRMDataSource1.UpdateRecord();
           Response.Redirect(SageCRMTabGroup.CRMURL("entwiz/CompanyList.aspx").ToString() + "&" + getEntityIDField()
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
       this.Comp_CompanyId = Request.Form.Get("SearchCompany");
       return "Comp_CompanyId=" + this.Comp_CompanyId;
   }

}

