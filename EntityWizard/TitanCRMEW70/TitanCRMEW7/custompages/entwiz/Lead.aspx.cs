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

public partial class Lead : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ExtraKeys = "";
        if ((false) || (false))
        {
            //set key values on url so they will be picked up in webpicker	
            SageCRMDataSource1.TableName = "xxxxxxxxx";
            SageCRMDataSource1.WhereClause = getWhereClause();
            IDataReader idr = SageCRMDataSource1.SelectData();  //get our IDataReader class
            //loop through our class
            while (idr.Read())
            {
                if ((false) && (idr["xxxx_CompanyId"] != null))
                {
                    ExtraKeys = ExtraKeys + "&Key1=" + idr["xxxx_CompanyId"].ToString();
                }
                if ((false) && (idr["xxxx_PersonId"] != null))
                {
                    ExtraKeys = ExtraKeys + "&Key2=" + idr["xxxx_PersonId"].ToString();
                }
            }
        }
        string leadlink = this.SageCRMTabGroup.CRMURL("191").ToString();
        string iKey_CustomEntity = "58";
        string prevurl = Request.ServerVariables["URL"].ToString() + "?" + Request.QueryString.ToString();
        leadlink += ExtraKeys + "&Key-1=" + iKey_CustomEntity  + "&E=xxxxxxxxx";
        leadlink += "&Key58=" + getEntityIDFieldValue();
        leadlink += "&" + getEntityIDField() + "=" + getEntityIDFieldValue();
        prevurl = prevurl.Replace("&", "%26");
        this.SageCRMButton.Url = "link:" + HttpUtility.UrlEncode(leadlink + "&PrevCustomURL=" + prevurl);

        this.SageCRMListBlock.EntityWhere = getWhereClause();
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
        return "lead_xxxxxxxxxId=" + getEntityIDFieldValue();
    }
}
