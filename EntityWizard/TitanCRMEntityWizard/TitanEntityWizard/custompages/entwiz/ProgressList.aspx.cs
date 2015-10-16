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

public partial class ProgressList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

        return getEntityIDField() + "=" + getEntityIDFieldValue();
    }
}
