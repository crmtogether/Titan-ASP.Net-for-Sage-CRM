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

public partial class tasklist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Get("nrid") != null)
        {
            //update the record just created with the project id
            SageCRMDataSource1.TableName = "projecttasks";
            SageCRMDataSource1.Parameters.Add("task_crmprojectid", Session["proj_projectid"]);
            SageCRMDataSource1.WhereClause="task_projecttasksid="+Request.QueryString.Get("nrid").ToString();
            SageCRMDataSource1.UpdateRecord();
        }
        this.SageCRMListBlock.EntityWhere = "task_crmprojectid="+Session["proj_projectid"];
    }
}
