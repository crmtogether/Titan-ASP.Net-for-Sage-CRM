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
using System.IO;

public partial class Default2 : System.Web.UI.Page
{
    string caseid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Panoply Technologies";

        Master.setMenuIndex(4);
        Master.requiresAuthentication = true;
        if (!this.securityLayer())
            return;
        SageCRMPortalEntryBlock.CreateMode = true;
        SageCRMPortalEntryBlock.EntityWhere = "";
        if (Request.QueryString.Get("nrid")!=null)
            caseid = Request.QueryString.Get("nrid");
        if (Request.QueryString.Get("case_caseid") != null)
            caseid = Request.QueryString.Get("case_caseid");

        if (caseid.Length > 0)
        {
            SageCRMPortalEntryBlock.CreateMode = false;
            SageCRMPortalEntryBlock.EntityWhere = "case_caseid=" + caseid;
            SageCRMPortalListBlock.EntityWhere = "libr_caseid=" + caseid;
        }
        else
        {
            this.Button1.Text = "Save";
        }
    }
    public bool securityLayer()
    {

        if (!this.Master.securityLayer())
        {
            SageCRMPortalEntryBlock.Visible = false;
            Button1.Visible = false;
            DataList1.Visible = false;
            return false;
        }
        return true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.Button1.Text == "Change")
        {
            this.Button1.Text = "Save";
        }
        else
        {
            this.Button1.Text = "Change";
        }

    }
    protected void SageCRMPortalEntryBlock_BeforeRendering(object source, ref string HTMLSource)
    {
        //extract the new record id...
        //when createrecord mode is set and a new record created
        //the Entryblock returns "<recordid>newid</recordid>"....
        //eg "<recordid>123</recordid>"
        if (HTMLSource.StartsWith("<recordid>") == true)
        {
            //parse out the new record id.
            //You could possibly parse out this using some xml objects
            int iEnd = HTMLSource.IndexOf("</recordid>");
            caseid = HTMLSource.Substring(10, iEnd-10);
            //update the record with the default values
            SageCRMDataSource2.WhereClause = "case_caseid=" + caseid;
            SageCRMDataSource2.Parameters.Add("case_primarycompanyid", SageCRMPortalEntryBlock.GetVisitorInfo("comp_companyid"));
            SageCRMDataSource2.Parameters.Add("case_primarypersonid", SageCRMPortalEntryBlock.GetVisitorInfo("pers_personid"));
            SageCRMDataSource2.Parameters.Add("case_source", "Web");
            SageCRMDataSource2.Parameters.Add("case_stage", "Logged");
            SageCRMDataSource2.Parameters.Add("case_priority", "Normal");
            SageCRMDataSource2.Parameters.Add("case_status", "In Progress");
            SageCRMDataSource2.UpdateRecord();
            //redirect to the details page with the new id
            Response.Redirect("casedetail.aspx?case_caseid=" + caseid);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (!this.FileUpload1.HasFile)
            return;

        //this.FileUpload1.SaveAs(@"C:\\Program Files\\Sage\\CRM\\CRM62sp1\\Library\\" + FileUpload1.FileName.ToString());
        string path = getPath(FileUpload1.FileName.ToString());
        this.FileUpload1.SaveAs(@path );

        //create library record
        SageCRM.AspNet.SageCRMDataSource ds = new SageCRM.AspNet.SageCRMDataSource();
        ds.SageCRMConnection = this.SageCRMDataSource2.SageCRMConnection;
        ds.TableName = "Library";
        ds.Parameters.Add("libr_caseid", this.caseid);
        ds.Parameters.Add("libr_personid", SageCRMPortalEntryBlock.GetVisitorInfo("pers_personid"));
        ds.Parameters.Add("libr_companyid", SageCRMPortalEntryBlock.GetVisitorInfo("comp_companyid"));
        string folderpath = getLibrFolder("company", SageCRMPortalEntryBlock.GetVisitorInfo("comp_companyid"));
        ds.Parameters.Add("libr_filepath", folderpath);
        ds.Parameters.Add("libr_filename", this.FileUpload1.FileName.ToString());
        ds.Parameters.Add("libr_note", this.libr_note.Text.ToString());
        //Label1.Text= ds.InsertRecord();
        ds.InsertRecord();

    }
    public static string GetUniqueFilename(string FileName)
    {
        int count = 0;
        string Name = "";
        if (System.IO.File.Exists(FileName))
        {
            System.IO.FileInfo f = new System.IO.FileInfo(FileName);
            if (!string.IsNullOrEmpty(f.Extension))
            {
                Name = f.FullName.Substring(0, f.FullName.LastIndexOf('.'));
            }
            else
            {
                Name = f.FullName;
            }
            while (File.Exists(FileName))
            {
                count++;
                FileName = Name + count.ToString() + f.Extension;
            }
        }
        return FileName;
    }
    public string getPath(string fname)
    {
        string FilePath = getLibrFolder("company", SageCRMPortalEntryBlock.GetVisitorInfo("comp_companyid"));
        //now save the contents to the file path
        string path = getDocStore() + "\\" + FilePath + "\\";
        string fullpath = path + fname;
        //make sure the folder exists
        folderCheck(fullpath);
        //make sure that we have a unique filename
        fullpath = GetUniqueFilename(fullpath);
        return fullpath;
    }
    protected void folderCheck(string path)
    {
        string[] stringSeparatorEquals = new string[] { "\\" };
        string[] resSplit;
        resSplit = path.Split(stringSeparatorEquals, StringSplitOptions.None);
        string newpath = "";
        for (int i = 0; i < resSplit.Length - 1; i++)
        {
            if (newpath != "")
                newpath += "\\";
            newpath += resSplit[i];
            if (!DirExists(newpath))
            {
                Directory.CreateDirectory(newpath);
            }
        }
    }
    public static bool DirExists(string sDirName)
    {
        try
        {
            return (System.IO.Directory.Exists(sDirName));    //Check for folder
        }
        catch (Exception)
        {
            return (false);                                 //Exception occured, return False
        }
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
       public string getLibrFolder(string EntityName, string EntityID)
        {
            string res = "";
            EntityName = EntityName.ToLower();

            SageCRM.AspNet.SageCRMDataSource ds = new SageCRM.AspNet.SageCRMDataSource();
            ds.SageCRMConnection = this.SageCRMConnection2;
            ds.TableName = EntityName;
            string librfield = "comp_librarydir";
            if (EntityName == "person")
            {
                ds.WhereClause = "pers_personid=" + EntityID;
                librfield = "pers_librarydir";
                IDataReader idr = ds.SelectData();  //get our IDataReader class
                //loop through our class
                idr.Read();
                res = idr[librfield].ToString() + "\\";
            }
            else
                if (EntityName == "company")
                {
                    ds.WhereClause = "comp_companyid=" + EntityID;
                    IDataReader idr = ds.SelectData();  //get our IDataReader class
                    //loop through our class
                    idr.Read();
                    res = idr[librfield].ToString() + "\\";
                }
                else
                    if (EntityName == "user")
                    {
                        ds.TableName = "users";
                        ds.WhereClause = "user_userid=" + EntityID;
                        IDataReader idr = ds.SelectData();  //get our IDataReader class
                        //loop through our class
                        idr.Read();
                        res = idr["user_firstname"].ToString() + " " + idr["user_lastname"].ToString() + "\\";
                    }
            return res;
        }

}
