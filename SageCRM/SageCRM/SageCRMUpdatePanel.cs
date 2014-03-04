using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Security.Permissions;
using System.ComponentModel;
using System.Drawing;
/*
[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
 */ 
namespace SageCRM.AspNet
{
    /*
    [AspNetHostingPermission(SecurityAction.Demand,
    Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:SageCRMUpdatePanel ID='SageCRMUpdatePanel' runat=server></{0}:SageCRMUpdatePanel>")]
    */
    class SageCRMUpdatePanel : UpdatePanel
    //public class SageCRMUpdatePanel : UpdatePanel
    {
        private static readonly Regex REGEX_CLIENTSCRIPTS =
            //new Regex("<SCRIPT\\s((?<aname>[-\\w]+)=[\"'](?<avalue>.*?) [\"']\\s?)*\\s*>(?<SCRIPT>.*?)</SCRIPT>", 
            new Regex("(?:<SCRIPT.*?>)((\n|\r|.)*?)(?:<\\/SCRIPT>)", 
            RegexOptions.Singleline | RegexOptions.IgnoreCase | 
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private bool m_RegisterInlineClientScripts = true;

        /// <summary>

        /// If the updatepanel shall parse and append inline scripts, default true

        /// </summary>

        public bool RegisterInlineClientScripts
        {
            get
            {
                return this.m_RegisterInlineClientScripts;
            }
            set
            {
                this.m_RegisterInlineClientScripts = value;
            }
        }
        
        protected virtual string AppendInlineClientScripts(string htmlsource)
        {
            if (this.ContentTemplate != null && htmlsource.IndexOf
                ("<SCRIPT", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
 
                MatchCollection matches = REGEX_CLIENTSCRIPTS.Matches(htmlsource);

                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        
                        string script = matches[i].Groups["script"].Value;
                        string scriptID = script.GetHashCode().ToString();
                        string scriptSrc = "";

                        CaptureCollection aname = matches[i].Groups["aname"].Captures;
                        CaptureCollection avalue = matches[i].Groups["avalue"].Captures;
                        for (int u = 0; u < aname.Count; u++)
                        {
                            if (aname[u].Value.IndexOf("src", 
                                StringComparison.CurrentCultureIgnoreCase) == 0)
                            {
                                scriptSrc = avalue[u].Value;
                                break;
                            }
                        }

                        if (scriptSrc != "")
                        {
                            ScriptManager.RegisterClientScriptInclude
                                (this, this.GetType(), scriptID, scriptSrc);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock
                                (this, this.GetType(), scriptID, script, true);
                        }
                        //Modifying the response context somehow breaks the 

                        //ASP.NET AJAX implementation

                        //htmlsource = htmlsource.Replace(matches[i].Value, "");

                    }
                }
            }
            return htmlsource;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            ScriptManager sm = ScriptManager.GetCurrent(Page);
            if (this.RegisterInlineClientScripts && sm != null && sm.IsInAsyncPostBack)
            {
                using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new StringWriter()))
                {
                    base.Render(htmlwriter);

                    string html;
                    html = htmlwriter.InnerWriter.ToString();
                    html = this.AppendInlineClientScripts(html);
                    writer.Write(html);
                }
            }
            else
            {
                base.Render(writer);
            }
        }

    }
}



