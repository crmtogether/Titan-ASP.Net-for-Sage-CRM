using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.IO;
using System.Net;
using System.Configuration;
using System.Net.NetworkInformation;

[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
namespace SageCRM.AspNet
{
    //declaring the event handler delegate
    public delegate void BeforeRenderingEventHandler(object source, ref string HTMLSource);

    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    public class SageCRMBaseClass : WebControl
    {
        public bool objectMethod = false;//don't throw in trial stuff for an object menthod

        private string FReleasedServer = "localhost";

        protected SageCRMConnection FSageCRMConnection;
        protected string FSageCRMConnectionID;
        protected bool FUpdator;

        public bool NoPostData = false;//we use this to prevent unnecessary post data being submited to the asp pages-needed for file large uploads

        public string customPostData = "";

        private string FVersion="3.2.0";

        public bool showTrialML = false;//READ THIS do SageCRMBaseClass set this to true here..it is set in the component itself
        //declaring the event
        public event BeforeRenderingEventHandler BeforeRendering;

        public bool onlyUseTLS12 = false;

        [Browsable(false)]
        public virtual string editorURL
        {
            get
            {
                return "not_implemented";
            }
        }



        [Browsable(false)]
        [Bindable(false)]
        [Category("Data")]
        [Description("The SageCRMConnection object")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SageCRMConnection SageCRMConnection
        {
            get
            {   //always get the connection from the page
                if (FSageCRMConnection == null)
                  FSageCRMConnection = IterateThroughChildren(Page);

                if ((FSageCRMConnection == null) && (!DesignMode))
                    FSageCRMConnection = new SageCRMConnection();

                return FSageCRMConnection;
            }
            set
            {
                if (value != null)
                    FSageCRMConnection = value;
            }
        }

        protected SageCRMConnection IterateThroughChildren(Control parent)
        {
//            c2=Page.FindControl("SageCRMConnection");
//            return (c2 as SageCRMConnection);
            if (parent == null)
                return null;
            foreach (Control c in parent.Controls)
            {
                if (c.GetType().ToString().Equals("SageCRM.AspNet.SageCRMConnection"))
                {
                      return (c as SageCRMConnection);
                }

                if (c.Controls.Count > 0)
                {
                    Control c2 = IterateThroughChildren(c);
                    if (c2 != null)
                        return (c2 as SageCRMConnection);
                }
            }
            return null;
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            string strhtml = _GetHTML();
            //event here to allow developers to alter the html on the server side

            output.WriteEncodedText(strhtml);
        }
        public virtual string _GetHTML()
        {
            return DesignMode ? "NO RUNTIME RENDERING FOR THIS CONTROL (SageCrmBaseClass)" : "";
        }

        //return the version of the software
        public virtual string getVersion()
        {
            return FVersion;
        }
        //does the same as the crm install 
        public virtual string getInstallName(string sPath)
        {
            string InstallName = "";
            var sbPath = new StringBuilder();
            sbPath.Append(sPath);
            string Path = sbPath.ToString();

            int iEndChar = 0;
            int iStartChar = 0;

            Path = Path.ToLower();

            iEndChar = Path.IndexOf("/custompages", StringComparison.Ordinal);
            if (iEndChar == -1)  //we may still be in dev mode (not designmode now)..assume we are
            {
                if (false)
                {
                    //dev
                    SageCRMConnection.CRMPath = "http://dev1.crmtogether.com/CRM2018R1/";
                }
                Path = SageCRMConnection.CRMPath;  //http://localhost/crm
                Path = Path.ToLower();
                iStartChar = Path.LastIndexOf("/", StringComparison.Ordinal);
                InstallName = Path.Substring(iStartChar);
            }
            else
            if (iEndChar != -1)
            {
                Path = Path.Substring(0, iEndChar);
                iStartChar = Path.LastIndexOf("/", StringComparison.Ordinal);
                InstallName = Path.Substring(iStartChar);
            }
            return InstallName;
        }
        public virtual void setuplive()
        {
            //get the sid
            SageCRMConnection.SID = HttpContext.Current.Request.QueryString.Get("SID");
            //get the path
            string cpath = "http://" + HttpContext.Current.Request.ServerVariables.Get("SERVER_NAME");

          //  if (HttpContext.Current.Request.ServerVariables.Get("SERVER_PORT") != "")
            //    SageCRMConnection.CRMPath += ":" + HttpContext.Current.Request.ServerVariables.Get("SERVER_PORT");

            string cinstall = getInstallName(HttpContext.Current.Request.ServerVariables.Get("URL"));

            if (SageCRMConnection.IsPortal)
            {
                SageCRMConnection.Keys = "";
                //keep the design time set path as this is the Portal (self service) mode
                foreach(string qstr in HttpContext.Current.Request.QueryString)
                {
                    SageCRMConnection.Keys += "&" + qstr.ToString();
                }
                return;
            }

            if (!SageCRMConnection.UseCodedPath)
                SageCRMConnection.CRMPath = cpath + cinstall;

            //get the keys...for crm persistance and context
            SageCRMConnection.Keys = "";
            string checkstring = "";
            for(int i=-1; i<100; i++)
            {
                checkstring = "Key" + i;
                if ((HttpContext.Current.Request.QueryString.Get(checkstring) != null) &&
                    (HttpContext.Current.Request.QueryString.Get(checkstring) != "") &&
                    (HttpContext.Current.Request.QueryString.Get(checkstring) != "undefined"))
                {
                    // fix here for urls that have a comma in them that breaks the asp
                    string keyval = HttpContext.Current.Request.QueryString.Get(checkstring);
                    //string[] stringSeparators = new string[] { "%2c" };
                    //string[] keyvalarr = keyval.Split(stringSeparators, StringSplitOptions.None);
                    string[] keyvalarr = keyval.Split(',');
                    SageCRMConnection.Keys += "&" + checkstring + "=" + keyvalarr[0];
                }
            }
            checkstring = "T";
            if ((HttpContext.Current.Request.QueryString.Get(checkstring) != null) &&
                (HttpContext.Current.Request.QueryString.Get(checkstring) != "") &&
                (HttpContext.Current.Request.QueryString.Get(checkstring) != "undefined"))
            {
                SageCRMConnection.Keys += "&" + checkstring + "=" + HttpContext.Current.Request.QueryString.Get(checkstring);
            }
            checkstring = "J";
            if ((HttpContext.Current.Request.QueryString.Get(checkstring) != null) &&
                (HttpContext.Current.Request.QueryString.Get(checkstring) != "") &&
                (HttpContext.Current.Request.QueryString.Get(checkstring) != "undefined"))
            {
                SageCRMConnection.Keys += "&" + checkstring + "test=" + HttpContext.Current.Request.QueryString.Get(checkstring);
            }
            checkstring = "F";
            if ((HttpContext.Current.Request.QueryString.Get(checkstring) != null) &&
                (HttpContext.Current.Request.QueryString.Get(checkstring) != "") &&
                (HttpContext.Current.Request.QueryString.Get(checkstring) != "undefined"))
            {
                SageCRMConnection.Keys += "&" + checkstring + "=" + HttpContext.Current.Request.QueryString.Get(checkstring);
            }
        }

        public string CRMURL(string Path)
        {
            return SageCRMConnection != null 
                ? _GetHTML("/CustomPages/SageCRM/component/Url.asp", "&url=" + Path, "", false, false) 
                : "No SageCRMConnection set (CRMURL Method)";
        }

        public string UpdateRecentList(string Uid, string Entity, string EntityId, string SummaryAction, string EntityKey, string Description)
        {
            return SageCRMConnection != null
                ? _GetHTML("/CustomPages/SageCRM/component/recentlist.asp", "&Uid=" + Uid + "&Entity=" + Entity +
                    "&EntityId=" + EntityId + "&Action=" + SummaryAction + "&EntityKey=" + 
                    EntityKey + "&Description=" + Description, "", false, false)
                : "No SageCRMConnection set (UpdateRecentList Method)";
        }

        public string GetTrans(string Family, string Caption)
        {
            if (SageCRMConnection == null)
                return "No SageCRMConnection set (GetTrans Method)";

            string res = _getFromCache("Trans_" + Family + "_" + Caption);
            if ((res != null) && (res != ""))
            {
                return res;
            }
            //MR Feb 2019 updated to encode the caption as sometimes they have ampersand 
            //HttpUtility.HtmlEncode(Caption)
            res= _GetHTML(SageCRMConnection.IsPortal 
                    ? "GetTrans_portal.asp"
                    : "/CustomPages/SageCRM/component/GetTrans.asp", "&Family=" + HttpUtility.UrlEncode(Family) + "&Caption=" + HttpUtility.UrlEncode(Caption), "", false, false);
            
            _setInCache("Trans_" + Family + "_" + Caption, res);

             return res;
        }

        public bool PortalLogout()
        {
            if (SageCRMConnection != null)
            {
                HttpCookie icookie = new HttpCookie("EWARESESS", "");
                HttpContext.Current.Response.Cookies.Add(icookie);
            }
            return true;
        }

        public string crmEncode(string val)
        {
            string res = val;
            res = res.Replace("%", "%%25");//clever 
            /*
            | %7C
            \ %5C
            ^ %5E
            ~ %7E
            [ %5B
            ] %5D
            ` %60
            ; %3B
            / %2F
            ? %3F
            : %3A
            @ %40
            = %3D
            & %26
            $ %24
             * */
            res = res.Replace("}", "%7D");//clever 
            res = res.Replace("{", "%7B");//clever 
            res = res.Replace("#", "%23");//clever 

            //res = HttpUtility.UrlEncode(res);
            //res = HttpUtility.HtmlEncode(val);
            return res;
        }

        public bool PortalLogon(string username, string password)
        {
            if (SageCRMConnection != null)
            {
                string pwd = "";
                pwd = portalEncoder(password);

                string strresult = _GetHTML("Authenticated.asp", "", "username=" + username + "&password=" + pwd, false, false);
                
                bool bresult = Convert.ToBoolean(strresult);
             
                if (bresult)
                {
                    //clever fix for £ sign
                    pwd = portalEncoder2(pwd);
                    //dev code only
                   // HttpCookie icookieEWAREID3 = new HttpCookie("Last working password", pwd);
                    //HttpContext.Current.Response.Cookies.Add(icookieEWAREID3);

                    SageCRMConnection.PortalUserName = username;
                    SageCRMConnection.PortalUserPassword = pwd;
                    if (ConfigurationManager.AppSettings["EnhancedSecurityPassword"] == "Y")
                    {
                        SageCRMConnection.PortalUserPassword = Encrypt.EncryptString(SageCRMConnection.PortalUserPassword, ConfigurationManager.AppSettings["EnhancedSecurityPasswordPhrase"].ToString());
                    }
                    
                    HttpCookie icookie = new HttpCookie("EWARESESS", "userid=" + SageCRMConnection.PortalUserName + "&password=" + pwd);
                    HttpContext.Current.Response.Cookies.Add(icookie);
                    HttpCookie icookieEWAREID = new HttpCookie("EWAREID", "abc");
                    HttpContext.Current.Response.Cookies.Add(icookieEWAREID);
                    HttpCookie icookieEWARESELOGON = new HttpCookie("EWARESELOGON", SageCRMConnection.PortalUserName);
                    HttpContext.Current.Response.Cookies.Add(icookieEWARESELOGON);
                }
                else
                {
                   // this code was to determine if there was an encoding issue
                   // HttpCookie icookieEWAREID2 = new HttpCookie("Last Failed password", strresult+"=="+ pwd);
                   //HttpContext.Current.Response.Cookies.Add(icookieEWAREID2);
                }
                return bresult;
            }
            return false;
        }

        public bool Authenticated()
        {
            if (SageCRMConnection != null)
            {
                string strresult = _GetHTML("Authenticated.asp", "", "", false, false);
                return Convert.ToBoolean(strresult);
            }
            return false;
        }

        public string AuthenticationError()
        {
            return SageCRMConnection != null 
                ? _GetHTML("AuthenticationError.asp", "", "", false, false) 
                : "No SageCRMConnection set (AuthenticationError Method)";
        }
        private string _getuniqueidforportal()
        {
            // Load Header collection into NameValueCollection object.
            int loop1;
            if (Context.Request.Headers["Cookie"] == null)
            {
                return "";
            }
            string Cookieval = Context.Request.Headers["Cookie"];
            //  Response.Write(Cookieval + "<br />");
            string[] coll = Cookieval.Split(';');
            //Response.Write(coll.Length.ToString() + "<br />");
            for (loop1 = 0; loop1 < coll.Length; loop1++)
            {
                //Response.Write("<br />"+coll[loop1]);
                string[] nval = coll[loop1].Split('=');
                //Response.Write("="+nval[0] + "=x=" + nval[1] + "<br />");
                nval[0] = nval[0].Trim();
                if (nval[0] == "ASP.NET_SessionId")
                {
                    return nval[1];
                }
            }
            return "";
        }
        public string GetVisitorInfo(string FieldName)
        {
			this.NoPostData = true;
            string res="";
            string uniquesessionid = _getuniqueidforportal();
            string _cachekey = uniquesessionid + "_" + FieldName;
            //turned off caching here for now as looks likem PortalUserName is not set
            if (uniquesessionid != "")
            {
                res = _getFromCache(_cachekey);
            if (ConfigurationManager.AppSettings["DisableCaching"] == "Y")  
            {
                res = "";
            }
            if ((res!=null)&&(res!=""))
            {
                return res;
            }
            }
            res = SageCRMConnection != null 
                ? _GetHTML("getVisitorInfo.asp", "&FieldName=" + FieldName, "", false, false) 
                : "No SageCRMConnection set (GetVisitorInfo Method)";
            this.NoPostData = false;
            if ((uniquesessionid != "") && (res != "No SageCRMConnection set (GetVisitorInfo Method)"))
            {
                _setInCache(_cachekey, res);
            }
            return res;
        }

        public string SetVisitorInfo(string FieldName, string FieldValue)
        {
            this.NoPostData = true;

            string uniquesessionid = _getuniqueidforportal();
            string _cachekey = uniquesessionid + "_" + FieldName;
           
            string res= SageCRMConnection != null 
                ? _GetHTML("setVisitorInfo.asp", "&FieldName=" + FieldName + "&FieldValue=" + FieldValue, "", false, false) 
                : "No SageCRMConnection set (SetVisitorInfo Method)";
            this.NoPostData = false;
            if (ConfigurationManager.AppSettings["DisableCaching"] != "Y")  
            {
                if ((uniquesessionid != "") && (res != "No SageCRMConnection set (GetVisitorInfo Method)"))
                {
                    _setInCache(_cachekey, res);
                }
            }
            return res;
        }

        public string GetContextInfo(string Context, string FieldName)
        {
            return GetContextInfo(Context, FieldName, false);
        }
        public string GetContextInfo(string Context, string FieldName, bool usecache)
        {
            this.NoPostData = true;
            string _sid = "";
            if ((HttpContext.Current.Request!=null)&&(HttpContext.Current.Request.QueryString!=null))
            {
                _sid=HttpContext.Current.Request.QueryString.Get("SID");
            }
            if (_sid==null)
            {
                _sid = "";
            }
            string _cachestring = "GetContextInfo_" + Context + "_" + FieldName + "_" + _sid;
            string res = "";
            if ((_sid!="")&&(usecache))
            {
                res=_getFromCache(_cachestring);
                if ((res != null)&&(res != ""))
                {
                    return res;
                }
            }
            res = SageCRMConnection != null
                ? _GetHTML("/CustomPages/SageCRM/component/GetContextInfo.asp", "&Context=" + Context + "&FieldName=" + FieldName, "", false, false)
                : "No SageCRMConnection set (GetContextInfo Method)";
            this.NoPostData = false;
            if ((_sid != "") && (usecache) && (res != "No SageCRMConnection set (GetContextInfo Method)"))
            {
                _setInCache(_cachestring, res);            
            }
            return res;
        }

        //takes a url string and parses out the parts up to the crm install name
        public string pathToCRM()
        {
            string _url = SageCRMConnection.CRMPath;
            int lio=_url.LastIndexOf("/");
            if (lio==(_url.Length-1))  //ie the url is something like http://server/crm/
            {
                //we need to find the second last '/' char so
                char[] myChars={'/'};
                lio=_url.LastIndexOfAny(myChars, (_url.Length - 2), _url.Length);
            } 
            _url = _url.Substring(0, lio);
            return _url;
        }
        //call this version of _GetHTML for any methods that are called as it does not insert the "trial" message.
        public virtual string _GetHTML(string vpath, string extraparams, string xmldata, bool CallRenderEvent, bool hideTrial)
        {
            objectMethod = true;
            return _GetHTML(vpath, extraparams, xmldata, CallRenderEvent);
#if false
            objectMethod = false;
#endif
        }
        public virtual string _GetHTML(string vpath, string extraparams, string xmldata, bool CallRenderEvent)
        {
            return _GetHTML(vpath, extraparams, xmldata, CallRenderEvent, "");
        }
        public virtual string _GetHTML(string vpath, string extraparams, string xmldata, bool CallRenderEvent, string Evalcode)
        {
            onlyUseTLS12 = (ConfigurationManager.AppSettings["onlyUseTLS12"] == "Y");
            if (onlyUseTLS12)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            else
            {
                System.Net.ServicePointManager.SecurityProtocol |=
                    SecurityProtocolType.Ssl3 |
                    SecurityProtocolType.Tls |
                    SecurityProtocolType.Tls11 |
                    SecurityProtocolType.Tls12;
            }

            if (SageCRMConnection == null)
            {
                return "NOSAGECRMConnection Object(base)";
            }
            // used to build entire input
            StringBuilder sb = new StringBuilder();
            string PostData = "";
            // used on each read operation
            byte[] buf = new byte[8192];
            
            if (HttpContext.Current != null)  
            {
                
                //check if we are inside crm
                string tmpsid = HttpContext.Current.Request.QueryString.Get("SID");
                if ((tmpsid != "") && (tmpsid != "undefined") && (tmpsid != null))
                {
                    setuplive();
                }
                //get the  post data
                if (!DesignMode)
                {
                    if (!string.IsNullOrEmpty(Evalcode))
                    {
                        PostData = "Evalcode=" + HttpUtility.UrlEncode(Evalcode);
                    }
                    bool hasEM = false;
                    System.Collections.IEnumerator en = HttpContext.Current.Request.Form.GetEnumerator();
                    //int ix = 0;
                        while (en.MoveNext())
                        {
                            if (this is SageCRMEntryBlock && ((SageCRMEntryBlock)this).StopDefaultAction || 
                                this is SageCRMPortalEntryBlock && ((SageCRMPortalEntryBlock)this).StopDefaultAction)
                            {
                                break;
                            }
                            if (PostData != "")
                                PostData += "&";
                            string _vdata = HttpContext.Current.Request.Form[(string)en.Current];
                            //_vdata=_vdata.Replace("%", "%25");
                            //_vdata = HtmlEncode(_vdata);
                            //_vdata = 
                            
                            if ((en.Current != null) && (en.Current.ToString() == "em"))//clever...multiple values crashed accelerator screens with multiple values on it
                            {
                                if (_vdata.IndexOf(",") > 0)
                                {
                                    _vdata = _vdata.Substring(0, 1);
                                    hasEM = true;
                                }
                            }

                            _vdata = HttpUtility.UrlEncode(_vdata);
                            PostData += en.Current + "=" + _vdata;
                            //PostData += en.Current + "=" + HttpUtility.UrlDecode(HttpContext.Current.Request.Form[(string)en.Current]);
                            //if (ix > 19)
                            //  break;
                            //ix++;
                        }
                        if (this is SageCRMPortalEntryBlock && ((SageCRMPortalEntryBlock)this).ScreenEditMode)
                        {
                            if (!hasEM)
                            {
                                PostData += "em=1";
                            }
                        }else
                        if (this is SageCRMPortalEntryBlock && ((SageCRMPortalEntryBlock)this).ScreenSaveMode)
                        {
                            if (!hasEM)
                            {
                                PostData += "em=2";
                            }
                        }
                    //old code...broke for some characters
    //                PostData = HttpUtility.UrlDecode(HttpContext.Current.Request.Form.ToString());
                   // PostData = PostData.Replace(Environment.NewLine, "%0D%0A");//fix for crlf breaking stuff
                }

                 
            }
            if (!DesignMode)
            {
                if (!string.IsNullOrEmpty(xmldata))  //this is for our datasource object
                {
                    PostData = HttpUtility.UrlDecode(xmldata);//for utf-8 we need to do this otherwise odd chars like the euro can break things.
                }
            }

            var encoding = new UTF8Encoding();
            //ASCIIEncoding encoding = new ASCIIEncoding();

            //NoPostData-added april 2015 
            if ((PostData == null) || (this.NoPostData))
            {
                PostData = "";
            }
            if ((PostData == "") && (this.customPostData != null) && (this.customPostData != ""))  //april 2018...using in sagecrmdatasource to allow us limit the columns returned in find record
            {
                PostData = this.customPostData;
            }
            
            byte[] data = encoding.GetBytes(PostData);
            if (vpath.EndsWith("/selectsql.asp") || vpath.EndsWith("/gettableschema_selectsql.asp") 
                || vpath.EndsWith("gettableschema_selectsql_portal.asp") || vpath.EndsWith("selectsql_portal.asp")
                || vpath.EndsWith("deleterecord_portal.asp")
                 || vpath.EndsWith("uthenticated.asp")) //vpath.EndsWith("/selectsql.asp") || vpath.EndsWith("/gettableschema_selectsql.asp")
            {
                if (vpath.EndsWith("uthenticated.asp"))
                {
                    //12 June 2020 - clever fix for ampersand and % and + in password on portal
                    string[] separatingStrings = { "password=" };
                    string[] portalUserPassword_arr = PostData.Split(separatingStrings, System.StringSplitOptions.None);
                    if (portalUserPassword_arr.Length > 1)
                    {
                        string _pwd = portalUserPassword_arr[1];
                        _pwd = portalEncoder(_pwd);
                        PostData = portalUserPassword_arr[0] + "&z=" + portalUserPassword_arr.Length + "&password="+ _pwd;
                    }
                    data = encoding.GetBytes(PostData);
                }
                else 
                if (xmldata.Contains("%2B") && !xmldata.Contains("+"))
                {
                    xmldata = xmldata.Replace("%2B", "+");
                    data = encoding.GetBytes(xmldata.Replace("%", "%25").Replace(" ", "%20").Replace("+", "%2B"));
                }
                else
                {
                    data = encoding.GetBytes(xmldata.Replace("%", "%25").Replace(" ", "%20").Replace("+", "%2B")); //.Replace(" ", "%20")
                }

            }
            else
            {
                data = encoding.GetBytes(PostData);
            }

            // prepare the web page we will be asking for
            string requeststring = "";
            if ((SageCRMConnection.IsPortal) && (!SageCRMConnection.DesignRequest))
            {
                requeststring = SageCRMConnection.CRMPortalPath + vpath + "?1=1" + SageCRMConnection.Keys + extraparams;
                if (HttpContext.Current != null)
                {  //the live request now set
                    requeststring = SageCRMConnection.CRMPortalPath + vpath + "?1=1" + HttpContext.Current.Request.QueryString.ToString() + extraparams;
                }
            }
            else
            {
                requeststring = SageCRMConnection.CRMPath + vpath + "?SID=" + SageCRMConnection.SID + SageCRMConnection.Keys + extraparams;
            }
            if ( (DesignMode) && (SageCRMConnection.IsPortal))
            {  //used to allow us to see the grid in design mode...ie we by pass security...requires server be localhost only also
                requeststring += "&netdesign=true";
            }
            if (SageCRMConnection.SageCRMVersion.ToString() != "")
            {
                requeststring += "&crmver=" + SageCRMConnection.SageCRMVersion.ToString();
            }
            //mr fix to get the pages to render with scripts in place
           
            
            if ((HttpContext.Current != null) && (!SageCRMConnection.IsPortal))
            {
                string sRQ = HttpContext.Current.Request.QueryString.ToString();
                int iSID = sRQ.IndexOf("SID");
                if ((!DesignMode) && (iSID > -1))   //iSID is used to check if we are debugging
                {
                    string RQ = HttpContext.Current.Request.Url.ToString();
                    int eware_index = RQ.IndexOf("eware.dll");
                    if (eware_index == -1)
                    {
                        RQ = RQ.ToLower();
                        eware_index = RQ.IndexOf("custompages");
                    }
                    if (!SageCRMConnection.UseCodedPath)  //fix here as this broke when using coded paths
                    {
                        if ((eware_index - 1) > 0)
                            SageCRMConnection.CRMPath = RQ.Substring(0, (eware_index - 1));
                    }
                    //create the live request
                    string my_qstr = "";  //fix for querystring breaking with certain characters
                    System.Collections.IEnumerator en1 = HttpContext.Current.Request.QueryString.GetEnumerator();
                    while (en1.MoveNext())
                    {
                        string unencval = HttpContext.Current.Request.QueryString[(string)en1.Current];
                        string[] unencval_arr = unencval.Split(',');///fix for elements with multiple values
                        string namval=en1.Current.ToString();
                        if (namval.IndexOf("SID") == 0 || 
                            namval.IndexOf("Key") == 0 || 
                            namval.IndexOf("T") == 0 || 
                            namval.IndexOf("J") == 0 || 
                            namval.IndexOf("F") == 0)
                        {
                            if (my_qstr != "")
                                my_qstr += "&";
                            string str_unencval = unencval_arr[0];
                            //str_unencval = str_unencval.Replace("%", "%25");
                            my_qstr += en1.Current + "=" + HttpUtility.UrlEncode(str_unencval);
                        }
                    }
                    //REMOVE...DEBUG ONLY
                   // SageCRMConnection.CRMPath = "http://dev1.crmtogether.com/crm2018r1";
                    requeststring = SageCRMConnection.CRMPath + vpath + "?" + my_qstr.ToString() + extraparams;
                }
            }
            //add in the db...which will be used later
            //..Y=N to stop it from being added to the history
            requeststring += "&2=2&Y=N&CRMDB=" + SageCRMConnection.CRMDB;
            //ignore SSL cert issues....see
            //http://stackoverflow.com/questions/12506575/how-to-ignore-the-certificate-check-when-ssl
            //and
            //http://weblog.west-wind.com/posts/2011/Feb/11/HttpWebRequest-and-Ignoring-SSL-Certificate-Errors
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            if (ConfigurationManager.AppSettings["Expect100Continue"] != "Y")  ///mr 6 apr 18 - added this into try improve speed
            {
                ServicePointManager.Expect100Continue = false;
                //ref: https://en.code-bude.net/2013/01/21/3-things-you-should-know-to-speed-up-httpwebrequest/
            }
            if (ConfigurationManager.AppSettings["DefaultConnectionLimit"] != null)  ///mr 6 apr 18 - added this into try improve speed
            {
                ServicePointManager.DefaultConnectionLimit = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultConnectionLimit"].ToString());
            }
            //portal code
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requeststring);
           
            if (ConfigurationManager.AppSettings["DetectProxy"] != "Y")  ///mr 6 apr 18 - added this into try improve speed
            {
                request.Proxy = null;
                //ref: https://en.code-bude.net/2013/01/21/3-things-you-should-know-to-speed-up-httpwebrequest/
            }
            if ((ConfigurationManager.AppSettings["CRMNetworkUser"] != null) &&
                (ConfigurationManager.AppSettings["CRMNetworkUser"] != ""))
            {
                NetworkCredential nc = new NetworkCredential(ConfigurationManager.AppSettings["CRMNetworkUser"],
                    ConfigurationManager.AppSettings["CRMNetworkUserPassword"],
                    ConfigurationManager.AppSettings["CRMNetworkDomain"]);
                var cache = new CredentialCache();
                //Uri requestUri = null;
                //Uri.TryCreate(requeststring, UriKind.Absolute, out requestUri);
                cache.Add(new Uri(SageCRMConnection.CRMPath), ConfigurationManager.AppSettings["CRMNetworkUserAuthType"], nc);
                // Assign Credentials
                //took out as seems to work without it
                //request.PreAuthenticate = true;//http://geekswithblogs.net/ranganh/archive/2006/02/21/70212.aspx
                request.Credentials = nc;
            }
            if (HttpContext.Current != null)
            {
                //we use the port to check if we are running this from thed designer (ie debug mode)
                //if (SageCRMConnection.DevelopmentPort == HttpContext.Current.Request.ServerVariables.Get("SERVER_PORT"))
                //new way to detect if we are debugging
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    request.Headers.Add(HttpRequestHeader.Cookie, "EWARESESS=userid=" +
                        SageCRMConnection.PortalUserName + "&password=" + SageCRMConnection.PortalUserPassword);
                }
                else
                {
                    //make sure that we send in the cookies from the original request as 
                    //we are imiatating the original callers credentials
                    if (SageCRMConnection.IsPortal)
                    {
                        if (ConfigurationManager.AppSettings["EnhancedSecurityPassword"] == "Y")
                        {
                            if ((HttpContext.Current.Request.Cookies["EWARESESS"] != null) && (HttpContext.Current.Request.Cookies["EWARESESS"]["userid"]!=null))
                            {
                                SageCRMConnection.PortalUserName = HttpContext.Current.Request.Cookies["EWARESESS"]["userid"].ToString();
                                SageCRMConnection.PortalUserPassword = HttpContext.Current.Request.Cookies["EWARESESS"]["password"].ToString();
                            }
                        }
                        string passwordInCookie = SageCRMConnection.PortalUserPassword;
                        try
                        {
                            if (ConfigurationManager.AppSettings["EnhancedSecurityPassword"] == "Y")
                            {
                                passwordInCookie = Encrypt.DecryptString(passwordInCookie, ConfigurationManager.AppSettings["EnhancedSecurityPasswordPhrase"].ToString());
                                request.Headers.Add(HttpRequestHeader.Cookie, "EWARESESS=userid=" + SageCRMConnection.PortalUserName + "&password=" + passwordInCookie);
                            }
                            else
                            {
                                request.Headers.Add("Cookie", Context.Request.Headers["Cookie"]);
                            }
               
                        }
                        catch (Exception exdecrypt)
                        {
                            request.Headers.Add("Cookie", Context.Request.Headers["Cookie"]);
                        }                
                    }
                    else
                    {
                        request.Headers.Add("Cookie", Context.Request.Headers["Cookie"]);
                    }
                }
            }
            else
            {
                if (SageCRMConnection.IsPortal)
                {
                    if (ConfigurationManager.AppSettings["EnhancedSecurityPassword"] == "Y")
                    {
                        if ((HttpContext.Current.Request.Cookies["EWARESESS"] != null) && (HttpContext.Current.Request.Cookies["EWARESESS"]["userid"] != null))
                        {
                            SageCRMConnection.PortalUserName = HttpContext.Current.Request.Cookies["EWARESESS"]["userid"].ToString();
                            SageCRMConnection.PortalUserPassword = HttpContext.Current.Request.Cookies["EWARESESS"]["password"].ToString();
                        }
                    }
                    string passwordInCookie = SageCRMConnection.PortalUserPassword;
                    try
                    {
                        if (ConfigurationManager.AppSettings["EnhancedSecurityPassword"] == "Y")
                        {
                            passwordInCookie = Encrypt.DecryptString(passwordInCookie, ConfigurationManager.AppSettings["EnhancedSecurityPasswordPhrase"].ToString());
                            //debugging lines          
                            // throw new Exception("Exception Msg: <br />" +
                                   //       ": " + passwordInCookie + "<br />");

                        }
                    }
                    catch (Exception exdecrypt)
                    {
                        //this can happen if we are changing to using encrypted.... 
                    }
                    request.Headers.Add(HttpRequestHeader.Cookie, "EWARESESS=userid=" + SageCRMConnection.PortalUserName + "&password=" + passwordInCookie);
                } else
                {
                    request.Headers.Add(HttpRequestHeader.Cookie, "EWARESESS=userid=" + SageCRMConnection.PortalUserName + "&password=" + SageCRMConnection.PortalUserPassword);
                }
            }
           
            request.Accept = "*/*";
            request.Method = "POST";

//some code here commented out but might come in handy...
            //should resolved any IIS issues with chunking and sending 100-continue messages
//            request.ProtocolVersion = HttpVersion.Version10;
  //          request.SendChunked = false;
    //        request.KeepAlive = false;

            //default browser
            //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            request.UserAgent = SageCRMConnection.UserAgent;
            request.ContentType = "application/x-www-form-urlencoded; encoding='utf-8'";

            request.ContentLength = data.Length;
            Stream newStream = request.GetRequestStream();
            // Send the data.

            newStream.Write(data, 0, data.Length);
            newStream.Close();
            //the following lines are useful for debugging purposes
//            throw new Exception("Exception Msg: <br />" +
  //              "CRM Request URL: " + requeststring + "<br />" +
    //            "CRM Request Data: " + data.Length.ToString() + "<br />" );

            // execute the request
            HttpWebResponse response = null; 
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {   
	            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(e, true);
                string v_netuser = "";

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CRMNetworkUser"]))
                    v_netuser = ConfigurationManager.AppSettings["CRMNetworkUser"];

                throw new Exception("Exception Msg: " + e.Message + "\n" +
                    "Exception Source: " + e.Source + "\n" +
                    "CRM Request URL: " + requeststring+"\n"+
                    "Method:"+ trace.GetFrame(0).GetMethod().Name + "\n" +
                    "Line: " + trace.GetFrame(0).GetFileLineNumber()+"\n"+
                    "Column: " + trace.GetFrame(0).GetFileColumnNumber()+"\n"+
                    "CRM Request Data: " + PostData + "\n" +
                    "Exception Data: " + e.Data + "\n"+
                    "CRMNetworkUser: " + v_netuser + 
                    "CRMNetworkDomain: " + ConfigurationManager.AppSettings["CRMNetworkDomain"]);

            }

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            Cookie MyCookie = new Cookie("EWARESESS", "userid=" + SageCRMConnection.PortalUserName + "&password=" + SageCRMConnection.PortalUserPassword);
            response.Cookies.Add(MyCookie);

            int count = 0;
            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);
                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    string tempString = Encoding.UTF8.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            string dmstr = sb.ToString();

            if (!SageCRMConnection.IsPortal)
            {
                //fix up any image locations for design mode so that they are rendered
                if ((DesignMode) || (HttpContext.Current == null))
                {
                    //fix the images
                    dmstr = dmstr.Replace("SRC=\"/", "SRC=\"http://" + FReleasedServer + "/");
                    dmstr = dmstr.Replace("src=\"/", "src=\"http://" + FReleasedServer + "/");
                    //fix the search selects
                    dmstr = dmstr.Replace("value=\"/", "src=\"http://" + FReleasedServer + "/");
                }
                else
                {
                    //now we only replace for design time debugging
                    //if (SageCRMConnection.DevelopmentPort == HttpContext.Current.Request.ServerVariables.Get("SERVER_PORT"))
                    //new way to detect if we are debugging
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        //ie for running in dev mode..which is not the same as designmode 
                        dmstr = dmstr.Replace("SRC=\"/", "SRC=\"http://" + HttpContext.Current.Request.ServerVariables.Get("SERVER_NAME") + "/");
                        dmstr = dmstr.Replace("src=\"/", "src=\"http://" + HttpContext.Current.Request.ServerVariables.Get("SERVER_NAME") + "/");
                        //fix the search selects
                        dmstr = dmstr.Replace("value=\"/", "src=\"http://" + HttpContext.Current.Request.ServerVariables.Get("SERVER_NAME") + "/");
                    }
                }
            }
            if ((!DesignMode) && (HttpContext.Current != null))
            {
                if (HttpContext.Current.Request.QueryString.Get("displaysettings") == "Y")
                {
                    dmstr += "<br /><br />Released Version " + FVersion + "<br />Server:" + FReleasedServer;
                }
            }

            if ( (BeforeRendering != null) && (CallRenderEvent==true) )
                BeforeRendering(this, ref dmstr);

            dmstr=_setTrialHTML(dmstr);

            return dmstr;
        }
        public string HtmlEncode(string text)
        {
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
            StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

            foreach (char c in chars)
            {
                int value = Convert.ToInt32(c);
                if (value > 127)
                    result.AppendFormat("&#{0};", value);
                else
                    result.Append(c);
            }

            return result.ToString();
        }
        //update 20th March 14 - removed license code
        public string _setTrialHTML(string renderHtml){
            return renderHtml;
        }

        private string _getFromCache(string cacheKey)
        {
            if (ConfigurationManager.AppSettings["DisableCaching"] =="Y")
            {
                return "";
            }
            string cacheValue = (string)HttpRuntime.Cache[cacheKey];
            if (string.IsNullOrEmpty(cacheValue))
                return "";

            if (cacheValue.ToLower().IndexOf("<html><head>") == 0)
                return "";

            return cacheValue;
        }

        public string portalEncoder(string _pwd)
        {
            _pwd = HttpUtility.UrlEncode(_pwd, Encoding.UTF8);
            return _pwd;
        }
        public string portalEncoder2(string _pwd)
        {
            //this is used in the self-service cookie
            _pwd = _pwd.Replace("%c2%a3", "%C3%82%C2%A3");// %C3%82%C2%A3  this is based of a test with crms ss portal
            return _pwd;
        }
        private void _setInCache(string cacheKey, string cachevalue)
        {

            if (ConfigurationManager.AppSettings["DisableCaching"] != "Y")
            {
                if ((cachevalue != null) && (cachevalue.ToLower().IndexOf("<html><head>") == -1))
                {
                    HttpRuntime.Cache.Insert(cacheKey,
                           cachevalue,//Records
                           null,//No Dependency
                           System.Web.Caching.Cache.NoAbsoluteExpiration,//No Absolute Expiration
                           TimeSpan.FromMinutes(60));//Expire in 30 mins

                }
            }

        }

    }

}
