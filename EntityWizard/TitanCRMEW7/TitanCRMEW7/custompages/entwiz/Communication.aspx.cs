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

public partial class Communication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ExtraKeys="";
       if ((false) || (false) || (false) || (false) || (false) 
          || (false) || (false))    
       {
          //set key values on url so they will be picked up in webpicker	
           SageCRMDataSource1.TableName = "xxxxxxxxx";
           SageCRMDataSource1.WhereClause = getWhereClause();
           IDataReader idr = SageCRMDataSource1.SelectData();  //get our IDataReader class
           //loop through our class
           while (idr.Read())
           {
               if ((false) && (idr["xxxx_CompanyId"]!=null))
               {
                   ExtraKeys = ExtraKeys + "&Key1=" + idr["xxxx_CompanyId"].ToString();
               }
               if ((false) && (idr["xxxx_PersonId"] != null))
               {
                   ExtraKeys = ExtraKeys + "&Key2=" + idr["xxxx_PersonId"].ToString();
               }
               if ((false) && (idr["xxxx_OpportunityId"] != null))
               {
                   ExtraKeys = ExtraKeys + "&Key7=" + idr["xxxx_OpportunityId"].ToString();
               }
               else if ((false) && (idr["xxxx_CaseId"] != null))
               {
                   ExtraKeys = ExtraKeys + "&Key8=" + idr["xxxx_CaseId"].ToString();
               }
               else if ((false) && (idr["xxxx_AccountId"] != null))
               {
                   ExtraKeys = ExtraKeys + "&Key24=" + idr["xxxx_AccountId"].ToString();
               }
               if ((false) && (idr["xxxx_OrderId"] != null))
               {
                   ExtraKeys = ExtraKeys + "&Key24=" + idr["xxxx_OrderId"].ToString();
               }
               if ((false) && (idr["xxxx_QuoteId"] != null))
               {
                   ExtraKeys = ExtraKeys + "&Key24=" + idr["xxxx_QuoteId"].ToString();
               }
           }
        }
        string task=this.SageCRMTabGroup.CRMURL("361").ToString();
        string appt = this.SageCRMTabGroup.CRMURL("362").ToString();
        string iKey_CustomEntity = "58";
        string prevurl = Request.ServerVariables["URL"].ToString() + "?" + Request.QueryString.ToString();
        task += "&Key58=" + getEntityIDFieldValue();
        task += "&" + getEntityIDField() + "=" + getEntityIDFieldValue();
        task += ExtraKeys + "&Key-1=" + iKey_CustomEntity + "&E=xxxxxxxxx";
        appt += "&Key58=" + getEntityIDFieldValue();
        appt += "&" + getEntityIDField() + "=" + getEntityIDFieldValue();
        appt += ExtraKeys + "&Key-1=" + iKey_CustomEntity + "&E=xxxxxxxxx";
        prevurl = prevurl.Replace("&", "%26");
        this.SageCRMButtonTask.Url = "link:" + HttpUtility.UrlEncode(task + "&PrevCustomURL=" + prevurl);
        this.SageCRMButtonAppt.Url = "link:" + HttpUtility.UrlEncode(appt + "&PrevCustomURL=" + prevurl);

        this.SageCRMListBlock.EntityWhere=getWhereClause();
        this.SageCRMListBlock.EvalCode += "block.PrevUrl=eWare.Url(\"xxxxxxxxx/Communication.aspx\");";
        this.SageCRMTopContent.EntityWhere = getEntityIDField() + "=" + getEntityIDFieldValue();
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
        return "Comm_xxxxxxxxxId=" + getEntityIDFieldValue();
    }
}
