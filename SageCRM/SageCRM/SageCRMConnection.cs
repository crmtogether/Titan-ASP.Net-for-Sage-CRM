using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Security.Permissions;
using System.IO;
using System.Net;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Web.UI.Design;
using System.Web.Configuration;
/*
 * Design time class only
 */
[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
internal class resfinder { 

}

namespace SageCRM.AspNet
{
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ParseChildren(true, "SID")]
    [DefaultProperty("SID")]
    [ToolboxData("<{0}:SageCRMConnection ID='SageCRMConnection' runat=server></{0}:SageCRMConnection>")]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMConnection.bmp")]
    public class SageCRMConnection : WebControl
    {
        private string pr_isPortal = "N";
        private string pr_useCodedCRMPath = "N";

        //this is used when in design mode for the self service editors
        public bool DesignRequest = false;

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Use hard-coded CRM Path")]
        public virtual bool UseCodedPath
        {
            get
            {
                return pr_useCodedCRMPath == "Y";
            }
            set
            {
                if (value == true)
                {
                    pr_useCodedCRMPath = "Y";
                }
                else
                {
                    pr_useCodedCRMPath = "N";
                }
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Set to true if the connection is to the Portal")]
        public virtual bool IsPortal
        {
            get
            {
                return pr_isPortal == "Y";
            }
            set
            {
                if (value == true)
                {
                    pr_isPortal = "Y";
                }
                else
                {
                    pr_isPortal = "N";
                }
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The version of Sage CRM")]
        [NotifyParentProperty(true)]
        public virtual string SageCRMVersion
        {
            get
            {
                string s = (string)ViewState["SageCRMVersion"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["SageCRMVersion"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("http://localhost/crm")]
        [Description("The path to our crm install. ")]
        [NotifyParentProperty(true)]
        public virtual string CRMPath {
            get
            {
                string s = (string)ViewState["CRMPath"];
                return (s == null) ? "http://localhost/crm" : s;
            }
            set
            {
                ViewState["CRMPath"] = value;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("http://localhost/Portaldemo/SageCRM/component/")]
        [Description("The path to the asp components.")]
        [NotifyParentProperty(true)]
        public virtual string CRMPortalPath
        {
            get
            {
                string s = (string)ViewState["CRMPortalPath"];
                return (s == null) ? "http://localhost/Portaldemo/SageCRM/component/" : s;
            }
            set
            {
                ViewState["CRMPortalPath"] = value;
            }
        }
        /*
         * Removed this as could not get it working at design time so pointless having it in
         * http://flimflan.com/blog/AccessingWebconfigAtDesignTimeInNET20.aspx
         * http://www.simple-talk.com/community/blogs/damon_armstrong/archive/2006/10/20/3031.aspx
         * Will figure out again
         * 
        private bool pr_useWebConfig = false;
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Sets if in design time the propeties should be take from web.config")]
        [NotifyParentProperty(true)]
        public virtual bool UseWebConfigSettings
        {
            get
            {
                return pr_useWebConfig;
            }
            set
            {
                pr_useWebConfig = value;
                setupFromWebConfig();
            }
        }
         * */
        private bool pr_designvisible = true;
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(true)]
        [Description("Sets if the design html is displayed or not.")]
        public virtual bool DesignVisible
        {
            get
            {
                return pr_designvisible;
            }
            set
            {
                pr_designvisible = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The version of the crm database.")]
        [NotifyParentProperty(true)]
        public virtual string CRMDB
        {
            get
            {
                string s = (string)ViewState["CRMDB"];
                return s;
            }
            set
            {
                ViewState["CRMDB"] = value;
            }
        }   

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The Portal user name")]
        [NotifyParentProperty(true)]
        public virtual string PortalUserName
        {
            get
            {
                string s = (string)ViewState["PortalUserName"];
                return s;
            }
            set
            {
                ViewState["PortalUserName"] = value;
                if (value != "")
                {
                    this.IsPortal = true;
                }
                else
                {
                    this.IsPortal = false;
                }
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The Portal user password")]
        [NotifyParentProperty(true)]
        public virtual string PortalUserPassword
        {
            get
            {
                string s = (string)ViewState["PortalUserPassword"];
                return s;
            }
            set
            {
                ViewState["PortalUserPassword"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("nosidset")]
        [Description("The current SID")]
        [NotifyParentProperty(true)]
        public virtual string SID
        {
            get
            {
                string s = (string)ViewState["SID"];
                return s;
            }
            set
            {
                ViewState["SID"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)")]
        [Description("The user agent that makes the request")]
        [NotifyParentProperty(true)]
        public virtual string UserAgent
        {
            get
            {
                string s = (string)ViewState["UserAgent"];
                return (s == null) ? "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)" : s;
            }
            set
            {
                ViewState["UserAgent"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [Description("The current Keys")]
        [DefaultValue("KeyN=Y")]
        [NotifyParentProperty(true)]
        public virtual string Keys
        {
            get
            {
                string s = (string)ViewState["Keys"];
                return s;
            }
            set
            {
                ViewState["Keys"] = value;
            }
        }
        protected void setupFromWebConfig()
        {
            /*
             * below is the xml that was in the web.config file
             * 
             * 
             * 	<appSettings>
		<!-- 
    Sage CRM connection details. This allows you to set the connection details here and on the control set the 
    "UseWebConfigSettings" property to true. This saves you having to set the properties for a connection on every page
    This is setting only used at Design time and when debugging. 
    -->
		<add key="SageCRMConnection_SID" value="122243633836051"/>
		<add key="SageCRMConnection_CRMPath" value="http://localhost/crm61"/>
		<add key="SageCRMConnection_Keys" value="&amp;Key0=1&amp;Key1=43&amp;Key2=57"/>
		<add key="SageCRMConnection_CRMDB" value=""/>
		<add key="SageCRMConnection_CRMPortalPath" value="http://localhost/Portaldemo/SageCRM/component/"/>
		<add key="SageCRMConnection_PortalUserName" value=""/>
		<add key="SageCRMConnection_PortalUserPassword" value=""/>
		<add key="SageCRMConnection_SageCRMVersion" value=""/>
		<add key="SageCRMConnection_UserAgent" value="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"/>
		<add key="SageCRMConnection_DesignVisible" value="true"/>
		<add key="SageCRMConnection_IsPortal" value="false"/>         
             * 
	</appSettings>
             * 
             * 
             * 
             * 
            if (this.UseWebConfigSettings)
            {
                //ie design time or dubugging via sage crm
                if ((DesignMode) || (System.Diagnostics.Debugger.IsAttached))
                {

                    if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_SID"] != null)
                        this.SID = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_SID"];
                    else
                        this.SID = "1111111";
                    
                    //IWebApplication webApp = (IWebApplication)this.Parent.Site.GetService(typeof(IWebApplication));
                  //  IWebApplication webApp = this.Site.GetService(typeof(IWebApplication)) as IWebApplication;
                    //Configuration config = webApp.OpenWebConfiguration(true);
  //                  System.Configuration.Configuration rootWebConfig1 =
//                                   System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
                  //  if (config != null)
                   // {
                      //  ConfigurationSectionGroup appset = rootWebConfig1.GetSectionGroup("appSettings");
                      //  if ( appset!= null)
                        //{
                       // if (config.AppSettings==null)
                        this.SID = "44444444";
                        //}
                   // }

                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_CRMPath"] != null)
                      this.CRMPath = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_CRMPath"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_Keys"] != null)
                      this.Keys = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_Keys"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_CRMDB"] != null)
                      this.CRMDB = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_CRMDB"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_CRMPortalPath"] != null)
                      this.CRMPortalPath = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_CRMPortalPath"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_PortalUserName"] != null)
                      this.PortalUserName = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_PortalUserName"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_PortalUserPassword"] != null)
                      this.PortalUserPassword = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_PortalUserPassword"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_SageCRMVersion"] != null)
                      this.SageCRMVersion = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_SageCRMVersion"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_UserAgent"] != null)
                      this.UserAgent = System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_UserAgent"];
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_DesignVisible"] != null)
                      this.DesignVisible = (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_DesignVisible"]=="true");
                   if (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_IsPortal"] != null)
                      this.IsPortal = (System.Configuration.ConfigurationManager.AppSettings["SageCRMConnection_IsPortal"]=="true");                                       
                }
            }
             * */
        }
        protected override void Render(HtmlTextWriter writer) {
//            setupFromWebConfig();
            if (DesignMode)
            {
                //as we are in design mode this object may have been created AFTER the 
                //other crm controls and therefore they will not render...so we need to tell 
                //them to render
                if (DesignVisible)
                {
                    writer.Write("<div id=\"designmode1\" style=\"background-color:#94ab37;height:25px;width:125px;\">SageCRMConnection. This component should be placed at the top left hand side of the screen.</div>");
                }
                else
                {
                    writer.Write("<div></div>");
                }
//                IterateThroughSageChildren(this.Page);
  //              writer.Write(this.Page.Controls.Count.ToString());
            }
            else
            {
//                writer.Write("<div id=\"TrialVersion\" style=\"background-color:#94ab37;height:25px;width:125px;\">SageCRMConnection Trial Version"+
  //                  "<br />Expires....</div>");
                //   writer.Write("<br />SID="+this.SID);
             //   writer.Write("<br />CRMPath=" + this.CRMPath);
             //   writer.Write("<br />Keys=" + this.Keys);
            }
        }
        protected bool IterateThroughSageChildren(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                string controlname = c.GetType().ToString();
                if ((controlname.Contains("SageCRM.AspNet")) && 
                    (!controlname.Equals("SageCRM.AspNet.SageCRMConnection")))
                 
                {
                        //string tmpName = (c as SageCRMBaseClass).Name;
                   //(c as SageCRMBaseClass).Render(
                   //(c as SageCRMBaseClass).Name = c.ID;
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter tw = new HtmlTextWriter(sw);
                    c.RenderControl(tw);
                }

                if (c.Controls.Count > 0)
                {
                    IterateThroughSageChildren(c);

                }
            }
            return false;
        }

    }
}
