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
using SageCRM.AspNet;

public partial class dslist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string companyid = this.getCompanyId();
        this.SageCRMDataSource1.WhereClause = "case_primarycompanyid=" + companyid;
        //build our url
        string strUrl = this.SageCRMEntryBlock.CRMURL("CrmProject/dslist.aspx");
        strUrl += "&case_caseid={0}";
        (this.GridView1.Columns[0] as HyperLinkField).DataNavigateUrlFormatString = strUrl;
        //set the where condition
        if ((Request.QueryString.Get("case_caseid") != null) &&
            (Request.QueryString.Get("case_caseid").ToString() != ""))
        {
            this.SageCRMEntryBlock.EntityWhere = "case_caseid=" + Request.QueryString.Get("case_caseid").ToString();
        }
        else
        {
            this.SageCRMEntryBlock.EntityWhere = "case_caseid=-1";
        }
    }
    public string getCompanyId()
    {
        string result = "-1";
        SageCRMDataSource ds = new SageCRMDataSource();
        ds.SageCRMConnection = this.SageCRMConnection;
        ds.TableName = "CrmProject";
        ds.WhereClause = "proj_projectid=" + Session["proj_projectid"];
        IDataReader idr = ds.SelectData();
        while (idr.Read())
        {
            result =idr["proj_companyid"].ToString();
            if (result == "")
                result = "-1";
        }
        return result;
    }
}
