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
using System.Security.Permissions;
using Microsoft.Win32;
using System.IO;

public partial class getFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the library record path
        SageCRM.AspNet.SageCRMDataSource ds = new SageCRM.AspNet.SageCRMDataSource();
        ds.SageCRMConnection = this.SageCRMConnection2;
        ds.TableName = "Library";
        //NOTE: This page lacks any security check as to whether the document belongs to a person/company
        //When deployed to a live server further development is required to secure this page.
        //CRM Together take no responsibility for this as this site is for sample purposes. 
        ds.WhereClause = "libr_libraryid="+Request.QueryString["libr_libraryid"];

        IDataReader idr = ds.SelectData();  //get our IDataReader class

        string filepath="";
        string filename="";
        //loop through our class
        while (idr.Read())
        {
            filepath=idr["libr_filepath"].ToString();
            filename = idr["libr_filename"].ToString();
        }
        string fullpath = getDocStore() + filepath + filename;
        System.IO.FileInfo file = new System.IO.FileInfo(fullpath);
        //stream the file out
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" +filename);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = getMimeType(fullpath);
        Response.WriteFile(file.FullName);
        Response.End();
    }
    private string getMimeType(string fileName)
    {
        string mimeType = "application/unknown";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (regKey != null && regKey.GetValue("Content Type") != null)
            mimeType = regKey.GetValue("Content Type").ToString();
        return mimeType;
    }
    public string getDocStore()
    {
        /*
         * select * from custom_sysparams
                where parm_name='DocStore'
         * */
        SageCRM.AspNet.SageCRMDataSource ds = new SageCRM.AspNet.SageCRMDataSource();
        ds.SageCRMConnection = this.SageCRMConnection2;
        ds.SelectSQL = "select * from custom_sysparams where parm_name='DocStore'";
        IDataReader idr = ds.SelectData();  //get our IDataReader class
        //loop through our class
        idr.Read();
        return idr["parm_value"].ToString();
    }
}