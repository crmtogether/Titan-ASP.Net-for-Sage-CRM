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

public partial class Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        this.SageCRMEntryBlock.EntityWhere=getWhereClause();
        this.SageCRMTopContent.EntityWhere = getWhereClause();
    }
    public string getEntityIDField()
    {
        return "xxxx_xxxxxxxxxid";
    }
    public string getEntityIDFieldValue()
    {
        //check for a new record
        string res = Request.QueryString.Get("nrid");
        if ((res == null) || (res == ""))
        {
            res = Request.QueryString.Get(this.getEntityIDField());
        }
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
            res = Session[getEntityIDField()].ToString();
        }
        this.SageCRMEntryBlock.EvalCode = "eWare.SetContext(EntityName, "+res+");";        
        return res;
    }
    public string getWhereClause()
    {
        return getEntityIDField() + "=" + getEntityIDFieldValue();
    }
}
