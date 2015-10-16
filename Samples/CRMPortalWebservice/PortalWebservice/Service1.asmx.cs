using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web.UI;
using System.Collections.Generic;
using SageCRM.AspNet;

namespace PortalWebservice
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {
        private SageCRMBaseClass SageObject;
        private SageCRMConnection SageCon;

        public void getSageObject()
        {
            if (SageObject==null)
            {
                SageCon=new SageCRMConnection();
                SageCon.IsPortal=true;
                //change the CRMPortalPath to point to your install
                SageCon.CRMPortalPath = "http://localhost/Portaldemo/SageCRM/component/";
                SageObject=new SageCRMBaseClass();
                SageObject.SageCRMConnection=SageCon;
            }
        }

        [WebMethod(Description = "Portal demo (PortalLogon method) from www.crmtogether.com", EnableSession = true)]
        public bool PortalLogon(string username, string password)
        {
            getSageObject();
            return SageObject.PortalLogon(username,password);
        }
        [WebMethod(Description = "Portal demo (PortalLogout method) from www.crmtogether.com", EnableSession = true)]
        public bool PortalLogout()
        {
            getSageObject();
            return SageObject.PortalLogout();
        }
        [WebMethod(Description = "Portal demo (getCompanyName method) from www.crmtogether.com", EnableSession = true)]
        public string getCompanyName()
        {
            getSageObject();
            return SageObject.GetVisitorInfo("comp_name");
        }
        [WebMethod(Description = "Portal demo (getCompanyWebSiet method) from www.crmtogether.com", EnableSession = true)]
        public string getCompanyWebSite()
        {
            getSageObject();
            return SageObject.GetVisitorInfo("comp_website");
        }
        [WebMethod(Description = "Portal demo (getCases method) from www.crmtogether.com", EnableSession = true)]
        public DataSet getCases()
        {
            getSageObject();
            SageCRMDataSource ds = new SageCRMDataSource();
            ds.SageCRMConnection = this.SageCon;
            ds.TableName = "Cases";
            string id = SageObject.GetVisitorInfo("pers_personid");
            ds.WhereClause = "case_primarypersonid=" + id;
            return ds.getDataSet();
        }

    }
}
