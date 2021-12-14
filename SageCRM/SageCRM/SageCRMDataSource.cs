using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Specialized;
using System.Security;

using System.Configuration;

[assembly: TagPrefix("SageCRM.AspNet", "SageCRM")]
namespace SageCRM.AspNet
{
    [AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxBitmap(typeof(resfinder), "SageCRM.SageCRMDataSource.bmp")]
//    [Designer(typeof(SageCRM.AspNet.Design.DataSourceDesigner))]
//    [Editor(typeof(SageCRM.AspNet.Design.SageCRMComponentDataSourcePropEditor), typeof(ComponentEditor))]
    public class SageCRMDataSource : DataSourceControl
    {
        public SageCRMDataSource() : base() { }

        protected SageCRMConnection FSageCRMConnection;

        protected OrderedDictionary prParameters;

        protected string prNewRecordId;

        public IDataReader idr;

        public bool Cachable = false;

        protected bool lastUpdateResult=false;

        private string FWorkflow = "";
        private string FWorkflowState = "";

        private string pr_NoTLS = "N";
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Specifies whether the insert or update runs any table level scripts")]
        public virtual bool NoTLS
        {
            get { return pr_NoTLS == "Y"; }
            set { pr_NoTLS = value ? "Y" : "N"; }
        }

        [Browsable(false)]
        public bool isPortal
        {
            get
            {
                if (this.SageCRMConnection != null)
                {
                    return ( (this.SageCRMConnection.IsPortal) && (!this.SageCRMConnection.DesignRequest) ) ;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(false)]
        public OrderedDictionary Parameters
        {
            get
            {
                if (this.prParameters == null)
                {
                    this.prParameters = new OrderedDictionary();
                }
                return this.prParameters;
            }
            set
            {
                this.prParameters = value;
            }
        }
        public string InsertRecord()
        {
            this.Cachable = false;
            this.Insert(this.Parameters, new DataSourceViewOperationCallback(this.HandleInsertCallback));
            SageCRMDataSourceView tview = (SageCRMDataSourceView)this.GetView(String.Empty);
            return tview.RecordID;
        }
        private bool HandleInsertCallback(int affectedRows, Exception ex)
        {
            affectedRows = 1;
            return true;
        }
        public bool UpdateRecord()
        {
            this.Cachable = false;
            //keysParams is not used as we use the where clause
            OrderedDictionary keysParams = new OrderedDictionary();
            OrderedDictionary OldValues = new OrderedDictionary();
            Update(keysParams, this.Parameters, OldValues, new DataSourceViewOperationCallback(this.HandleUpdateCallback));
            return lastUpdateResult;
        }
        private bool HandleUpdateCallback(int affectedRows, Exception ex)
        {
            if (affectedRows > 0)
            {
                lastUpdateResult = true;
                return true;
            }
            else
            {
                lastUpdateResult = false;
                return false;
            }
        }
        public bool DeleteRecord()
        {
            //keysParams is not used as we use the where clause
            OrderedDictionary keysParams = new OrderedDictionary();
            Delete(keysParams, this.Parameters, new DataSourceViewOperationCallback(this.HandleDeleteCallback));
            return true;
        }
        private bool HandleDeleteCallback(int affectedRows, Exception ex)
        {
            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public 
        [Browsable(false)]
        [Bindable(true)]
        [Category("Data")]
        [Description("The SageCRMConnection object")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [NotifyParentProperty(true)]
        public virtual SageCRMConnection SageCRMConnection
        {
            get
            {
                if (FSageCRMConnection == null)
                { 
                    FSageCRMConnection = this.IterateThroughChildren(this.Page);
                    if ((FSageCRMConnection==null) && (!DesignMode))
                    {
                      FSageCRMConnection = new SageCRMConnection();
                    }
                }
                return FSageCRMConnection;
            }
            set
            {
                FSageCRMConnection = value;
            }
        }
        private string getExecSQLFile()
        {
            if (this.isPortal)
            {
                return "execsql_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/execsql.asp";
            }
        }
        public void SetWorkflowInfo(string workflowname, string workflowstate)
        {
            FWorkflow = workflowname;
            FWorkflowState = workflowstate;
        }
        public string ExecSQL(string sqlExec)
        {
            SageCRMCustom FSageCRMCustom;
            if (this.SageCRMConnection != null)
            {
                FSageCRMCustom = new SageCRMCustom(); //we use this to make our request
                FSageCRMCustom.SageCRMConnection = FSageCRMConnection;
                return FSageCRMCustom._GetHTML(this.getExecSQLFile(), "&ExecSQL=" + sqlExec, "", false);
            }
            else
            {
                return "No SageCRMConnection set (ExecSQL Method)";
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The Top N number of records to return from the query. Ignored if linked to a data aware control.")]
        public virtual string Top
        {
            get
            {
                string s = (string)ViewState["Top"];
                return s ?? "";
            }
            set
            {
                ViewState["Top"] = value;
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The orderby clause. Ignored if SelectSQL is set.")]
        public virtual string OrderBy
        {
            get
            {
                string s = (string)ViewState["OrderBy"];
                return s ?? "";
            }
            set
            {
                ViewState["OrderBy"] = value;
                this.SelectSQL = "";
                RaiseDataSourceChangedEvent(EventArgs.Empty);
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The select sql query to run. Ignored if TableName is set.")]
        public virtual string SelectSQL
        {
            get
            {
                string s = (string)ViewState["SelectSQL"];
                return s ?? "";
            }
            set
            {
                ViewState["SelectSQL"] = value;
                if (value != "")
                {
                    this.TableName = "";
                    this.WhereClause = "";
                }
                RaiseDataSourceChangedEvent(EventArgs.Empty);
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The name of the table to query")]
        public virtual string TableName
        {
            get
            {
                string s = (string)ViewState["TableName"];
                return s ?? "";
            }
            set
            {
                ViewState["TableName"] = value;
                RaiseDataSourceChangedEvent(EventArgs.Empty);
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The where clause to run against the table")]
        public virtual string WhereClause
        {
            get
            {
                string s = (string)ViewState["WhereClause"];
                return s ?? "";
            }
            set
            {
                ViewState["WhereClause"] = value;
                RaiseDataSourceChangedEvent(EventArgs.Empty);
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("Option to translate the data")]
        public virtual string Translate
        {
            get
            {
                string s = (string)ViewState["Translate"];
                return s ?? "";
            }
            set
            {
                ViewState["Translate"] = value;
                RaiseDataSourceChangedEvent(EventArgs.Empty);
            }
        }
        [Browsable(true)]
        [Category("Data")]
        [DefaultValue("")]
        [Description("The name of the columns to return")]
        public virtual string ColumnList
        {
            get
            {
                string s = (string)ViewState["ColumnList"];
                return s ?? "";
            }
            set
            {
                ViewState["ColumnList"] = value;
                RaiseDataSourceChangedEvent(EventArgs.Empty);
            }
        }
        protected SageCRMConnection IterateThroughChildren(Control parent)
        {
            Control c2 = null;
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
                    c2 = IterateThroughChildren(c);
                    if (c2 is SageCRMConnection)
                        return (c2 as SageCRMConnection);
                }
            }
            return null;
        }

        public IDataReader SelectData()
        {
            SageCRMDataSourceView sdv = this.Open() as SageCRMDataSourceView;
            sdv.Select(DataSourceSelectArguments.Empty, this.do_nada);
            idr = new SageCRMDataViewReader(sdv.dv);
            return idr;
        }
        public void do_nada(IEnumerable data) { }
        public DataSet getDataSet()
        {
            DataSourceView tmp_dsv = this.Open();
            tmp_dsv.Select(DataSourceSelectArguments.Empty, do_nada);
            SageCRMDataSourceView s_dsv = (tmp_dsv as SageCRMDataSourceView);
            DataSet newds=new DataSet();
            newds.Tables.Add(s_dsv.data);
            return newds;
        }   
        public DataSourceView Open()
        {
            return this.GetView(String.Empty);
        }
        public void Insert(IDictionary values, DataSourceViewOperationCallback callback)
        {
            this.GetView(String.Empty).Insert(values,callback);
        }
        public void Update(IDictionary keys, IDictionary values, IDictionary oldvalues, DataSourceViewOperationCallback callback)
        {
            this.GetView(String.Empty).Update(keys, values, oldvalues, callback);
        }
        public void Delete(IDictionary keys, IDictionary values, DataSourceViewOperationCallback callback)
        {
            this.GetView(String.Empty).Delete(keys, values, callback);
        }
        
        // Return a strongly typed view for the current data source control.
        private SageCRMDataSourceView view = null;
        protected override DataSourceView GetView(string viewName)
        {
            if (null == view)
            {
                view = new SageCRMDataSourceView(this, this.TableName, this.WhereClause, this.SageCRMConnection, this.SelectSQL, this.Top, this.NoTLS, this.Cachable, this.ColumnList, 
                    this.FWorkflow, this.FWorkflowState, this.OrderBy, this.Translate);
            }
            return view;
        }
        // The ListSourceHelper class calls GetList, which
        // calls the DataSourceControl.GetViewNames method.
        // Override the original implementation to return
        // a collection of one element, the default view name.
        protected override ICollection GetViewNames()
        {
            ArrayList al = new ArrayList(1);
            al.Add(SageCRMDataSourceView.DefaultViewName);
            return al as ICollection;
        }
    }
    // The SageCRMDataSourceView class encapsulates the
    // capabilities of the SageCRMDataSource data source control.
    public class SageCRMDataSourceView : DataSourceView
    {
        public SageCRMConnection FSageCRMConnection;
        public string RecordID = "";

        protected string FTableName;
        protected string FWhereClause;
        protected string FSelectSQL;
        protected string FTop;
        protected SageCRMCustom FSageCRMCustom;
        protected bool FNoTLS = false;

        protected string FColumnList; //comma delimited list of columns to be returned. allows us to use find record to only return some columns
        protected string FTranslate;

        protected string idField = "";
        public DataView dv;//used for the data reader

        public DataTable data;
        public DataSet dataset;
        public DataSet objData;
        public DataSet objSchema;

        public bool FCachable = false;

        public string FWorkflow = "";
        public string FWorkflowState = "";
        public string FOrderBy = "";

        public bool isPortal
        {
            get
            {
                if (this.FSageCRMConnection != null)
                {
                    return ((this.FSageCRMConnection.IsPortal) && (!this.FSageCRMConnection.DesignRequest));

                }
                else
                {
                    return false;
                }
            }
        }

        public SageCRMDataSourceView(IDataSource owner, string TableName, string WhereClause,
            SageCRMConnection SageCRMConnectionObj, string SelectSQL, string iTop, bool pNoTLS, bool pCachable,string columnlist,
            string Workflow, string WorkflowState, string OrderBy="", string translate="N")
            : base(owner, DefaultViewName)
        {
            FSageCRMConnection = SageCRMConnectionObj;
            FTableName = TableName;
            FWhereClause = WhereClause;
            FSelectSQL = SelectSQL;
            FTop = iTop;
            FNoTLS = pNoTLS;
            FSageCRMCustom = new SageCRMCustom(); //we use this to make our request
            FSageCRMCustom.SageCRMConnection = FSageCRMConnection;
            FCachable = pCachable;
            FColumnList = columnlist;
            FTranslate = translate;
            FWorkflow = Workflow;
            FWorkflowState = WorkflowState;
            FOrderBy = OrderBy;
        }

        // The data source view is named. However, the SageCRMDataSource
        // only supports one view, so the name is ignored, and the
        // default name used instead.
        public static string DefaultViewName = "SageCRMView";

        private string getTableSchema_file()
        {
            if (this.isPortal)
            {
                return "gettableschema_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/gettableschema.asp";
            }
        }
        private string getTableSchema_Selectsqlfile()
        {
            if (this.isPortal)
            {
                return "gettableschema_selectsql_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/gettableschema_selectsql.asp";
            }
        }
        private string getSelectSql_file()
        {
            if (this.isPortal)
            {
                return "selectsql_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/selectsql.asp";
            }
        }
        private string getFindRecord_file()
        {
            if (this.isPortal)
            {
                return "findrecord_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/findrecord.asp";
            }
        }

        // Get data from the underlying data source.
        // Build and return a DataView, regardless of mode.
        /*
            Parameters
            arguments
                A DataSourceSelectArguments that is used to request 
                operations on the data beyond basic data retrieval.
            Return Value
                An IEnumerable list of data from the underlying data storage.
            RemarksRemarks
                The ExecuteSelect method is called to retrieve data from the 
                underlying data store and return it as an IEnumerable object. 
                All data source controls support data retrieval from their 
                underlying data storage, even if other operations such as 
                insertion and sorting are not supported. Because a data-bound 
                control can request a list of data at any time as a result of 
                a DataSourceChanged event or a DataBind method call, 
                the data retrieval must be performed on demand.         
         */
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            IEnumerable dataList = null;
            DataColumn col;
            string xmlData = "";
            string xmlSchema = "";
            int rowcount = 0;


            this.FSelectSQL = this.FSelectSQL.Replace("+", "%2B");
            this.FWhereClause = this.FWhereClause.Replace("+", "%2B");

            string dataFile = "";
            string dataSchemaFile = "";
            if (FSelectSQL != "")
            {
                string _datacachekey = "dataFile_" + this.FSelectSQL;
                _datacachekey = _datacachekey.Replace(" ", "_");
               //get the schema xml
                dataFile = this.getSelectSql_file();
               dataSchemaFile = this.getTableSchema_Selectsqlfile();
               xmlSchema = _getFromCache("dataSchemaFile_" + this.FSelectSQL);
               //MR 4.7.1.1-fix fro caching of invalis session data
               if ((xmlSchema == null)||(xmlSchema == ""))
               {
                   xmlSchema = FSageCRMCustom._GetHTML(this.getTableSchema_Selectsqlfile(), "", "SelectSQL=" + this.FSelectSQL, false); //Fixes problem? "", "SelectSQL=" + this.FSelectSQL
               }
               if (FCachable)
               {
                   //check have we it already
                   xmlData = _getFromCache(_datacachekey);
               }
               //MR 4.7.1.1-fix fro caching of invalis session data
                //get the data xml
                if ((xmlData == null)||(xmlData == ""))
               {
                   xmlData = FSageCRMCustom._GetHTML(this.getSelectSql_file(),
                       "&Top=" + selectArgs.MaximumRows.ToString() +
                       "&iTop=" + this.FTop +
                       "&iFrom=" + selectArgs.StartRowIndex.ToString() +
                       "&iSort=" + selectArgs.SortExpression.ToString() +
                       "&grc=" + selectArgs.RetrieveTotalRowCount.ToString() +
                        "", "SelectSQL=" + this.FSelectSQL, false);  // Fixes Problem? "", "SelectSQL=" + this.FSelectSQL
                   if (FCachable)
                   {
                       //set the cache
                           _setInCache(_datacachekey, xmlData);
                       }
                   }
               }
            else
            {
                dataFile = this.getFindRecord_file();
                dataSchemaFile = this.getTableSchema_file();
                string _datacachekey = "dataFile_" + this.FTableName + "_" + this.FWhereClause + "_" + this.FColumnList;
                _datacachekey = _datacachekey.Replace(" ", "_");
                //get the schema xml
                //to do turn back on....
                xmlSchema = _getFromCache("dataSchemaFile_" + this.FTableName+ "_" + this.FColumnList);
                //MR 4.7.1.1-fix fro caching of invalis session data
                if ((xmlSchema == null) || (xmlSchema == ""))
                {
                    FSageCRMCustom.customPostData = "columnList=" + this.FColumnList;
                    xmlSchema = FSageCRMCustom._GetHTML(this.getTableSchema_file(), "&TableName=" + this.FTableName, "", false);
                        _setInCache("dataSchemaFile_" + this.FTableName + "_" + this.FColumnList, xmlSchema);
                    }
                if (FCachable)
                {
                    //check have we it already
                    xmlData = _getFromCache(_datacachekey);
                }
                //MR 4.7.1.1-fix fro caching of invalis session data
                if ((xmlData == null)||(xmlData == ""))
                {
                    //15 May 2020 - clever fix for ampersand appearing in the where clause
                    if (this.FWhereClause.IndexOf("&")>=0)
                    {
                        this.FWhereClause = this.FWhereClause.Replace("&", "%26");
                    }
                    //get the data xml
                    FSageCRMCustom.customPostData = "columnList=" + this.FColumnList;
                    FSageCRMCustom.customPostData += "&translate=" + this.FTranslate;
                    xmlData = FSageCRMCustom._GetHTML(this.getFindRecord_file(),
                        "&TableName=" + this.FTableName +
                        "&WhereClause=" + this.FWhereClause +
                        "&Top=" + selectArgs.MaximumRows.ToString() +
                        "&iTop=" + this.FTop +
                        "&iFrom=" + selectArgs.StartRowIndex.ToString() +
                        "&iSort=" + selectArgs.SortExpression.ToString() +
                        "&grc=" + selectArgs.RetrieveTotalRowCount.ToString()+
                        "&OrderBy=" + this.FOrderBy 
                        , "", false);
                    if (FCachable)
                    {
                        //set the cache
                            _setInCache(_datacachekey, xmlData);
                        }
                    }
                }
            //build our StreamReaders
            StringReader xmlStreamSchema = new StringReader(xmlSchema.ToString());
            StringReader xmlStreamData = new StringReader(xmlData.ToString());
            //the data dataset
            try
            {
                objData = new DataSet();
                objData.ReadXml(xmlStreamData);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error in schema data (check properties)." +
                    System.Environment.NewLine +
                    "CRMPortalPath=" + this.FSageCRMConnection.CRMPortalPath +
                    System.Environment.NewLine +
                    "ASP File=" + dataFile +
                    System.Environment.NewLine +
                    "dataSchemaFile="+dataSchemaFile +
                    System.Environment.NewLine +
                    "TableName=" + this.FTableName +
                    System.Environment.NewLine +
                    "WhereClause=" + this.FWhereClause +
                    System.Environment.NewLine +
                    "SelectSQL=" + this.FSelectSQL +
                    System.Environment.NewLine +
                    "Top=" + selectArgs.MaximumRows.ToString() +
                    System.Environment.NewLine +
                    "iTop=" + this.FTop +
                    System.Environment.NewLine +
                    "iFrom=" + selectArgs.StartRowIndex.ToString() +
                    System.Environment.NewLine +
                    "iSort=" + selectArgs.SortExpression.ToString() +
                    System.Environment.NewLine +
                    "grc=" + selectArgs.RetrieveTotalRowCount.ToString() +
                    System.Environment.NewLine +
                    "OrderBy=" + this.FOrderBy +
                    System.Environment.NewLine +
                    "Error Msg=" + e.Message +
                    System.Environment.NewLine +
                    "xmlSchema=" + xmlSchema.ToString());
            }
            //the schema dataset
            try
            {
                objSchema = new DataSet();
                objSchema.ReadXml(xmlStreamSchema);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error in data schema." + 
                    System.Environment.NewLine+
                    "ASP File=" + dataFile +
                    System.Environment.NewLine +
                    "dataSchemaFile=" + dataSchemaFile +
                    System.Environment.NewLine +
                    "TableName=" + this.FTableName +
                    System.Environment.NewLine +
                    "WhereClause=" + this.FWhereClause +
                    System.Environment.NewLine +
                    "SelectSQL=" + this.FSelectSQL+
                    System.Environment.NewLine +
                    "Top=" + selectArgs.MaximumRows.ToString() +
                    System.Environment.NewLine +
                    "iTop=" + this.FTop +
                    System.Environment.NewLine +
                    "iFrom=" + selectArgs.StartRowIndex.ToString() +
                    System.Environment.NewLine +
                    "iSort=" + selectArgs.SortExpression.ToString() +
                    System.Environment.NewLine +
                    "grc=" + selectArgs.RetrieveTotalRowCount.ToString() +
                    System.Environment.NewLine +
                    "OrderBy=" + this.FOrderBy +
                    System.Environment.NewLine +
                    "Error Msg="+e.Message +
                    System.Environment.NewLine +
                    "xmlSchema=" + xmlSchema.ToString());
            }
 
            if (FTableName != "")
            {
                data = new DataTable(FTableName);
            }
            else
            {
                data = new DataTable();
            }

            if (objSchema.Tables.Count == 0)
            {
                throw new InvalidOperationException("No table data found.");
            }
            //setup the schema of the columns using our schema dataset
            for (int i = 0; i < objSchema.Tables[0].Rows.Count; i++)  //each row describes a column
            {
               string fieldType= objSchema.Tables[0].Rows[i]["FieldType"].ToString(); 
               string fieldName=objSchema.Tables[0].Rows[i]["FieldName"].ToString();
               string fieldCaption = objSchema.Tables[0].Rows[i]["FieldCaption"].ToString();
               if (i == 0)
               {
                   idField = fieldName;
               }
               if (fieldType=="int"){
                  col = new DataColumn(fieldName, typeof(int));
               }else
               if (fieldType == "datetime")
               {
                 col = new DataColumn(fieldName, typeof(DateTime));
                 col.DateTimeMode = System.Data.DataSetDateTime.Utc;
               }
               else
               {  //default string
                 col = new DataColumn(fieldName, typeof(string));
               }
               col.AllowDBNull = true;
               col.Caption = fieldCaption;
               if (fieldName != "undefined")
               {
                   //seen with external dbs
                   data.Columns.Add(col);
               }
            }
            string colName="";
            if (objData.Tables.Count > 0)
            {
                int icrmtogethertrc = 1;
                if (objData.Tables[0].TableName != "crmtogethertrc")
                {
                    for (int i = 0; i < objData.Tables[0].Rows.Count; i++)
                    {
                        string dataValues = "";
                        string[] dataValuesArr;
                        string[] stringSeparators = new string[] { "[stopitnoe]" };
                        for (int j = 0; j < data.Columns.Count; j++) //we loop though the schema columns to that the data is read in order
                        {
                            colName = data.Columns[j].ColumnName.ToString();
                            string valStr = objData.Tables[0].Rows[i][colName].ToString();
                            //if (dataValues != "")  //this line meant that if the first column was null the colummn order would be wrong
                            if (j>0)
                                dataValues += "[stopitnoe]";
                            if (data.Columns[j].DataType == typeof(int))
                            {
                                //we may do something here later
                                //okay so when we have a 3rd party system that has an id field thats a string this is a problem
                                //clever....
                                int n;
                                bool isNumeric = int.TryParse(valStr, out n);
                                if (!isNumeric)
                                {
                                    data.Columns[j].DataType = typeof(string);
                                }
                                ////
                            }
                            else
                                if (data.Columns[j].DataType == typeof(DateTime))
                                {
                                    //we may do something here later
                                }
                            dataValues += System.Web.HttpUtility.UrlDecode(valStr);
                        }

                        dataValuesArr = dataValues.Split(stringSeparators, StringSplitOptions.None);
                        data.Rows.Add(CopyRowData(dataValuesArr, data.NewRow()));
                        rowcount++;
                    }
                }
                else
                {
                    icrmtogethertrc=0;
                }
                //get total row count data

                if (objData.Tables[icrmtogethertrc].TableName == "crmtogethertrc")
                {
                    for (int i = 0; i < objData.Tables[icrmtogethertrc].Rows.Count; i++)
                    {
                        string trc_res = objData.Tables[icrmtogethertrc].Rows[i]["crmtogethertotal"].ToString();
                        selectArgs.TotalRowCount = Convert.ToInt32(trc_res);
                    }
                }
            }
            data.AcceptChanges();
            this.dv = new DataView(data);
            if (selectArgs.SortExpression != String.Empty)
            {
                this.dv.Sort = selectArgs.SortExpression;
            }
            dataList = this.dv;
            if (null == dataList)
            {
                throw new InvalidOperationException("No data loaded from data source.");
            }
            this.dataset = this.data.DataSet;
            return dataList;
        }

        public override bool CanSort {
            get { return false; } 
        }
        private DataRow CopyRowData(string[] source, DataRow target)
        {
            try
            {
                for (int i = 0; i < source.Length; i++)
                {
                    try
                    {
                        target[i] = source[i];
                    }
                    catch (System.ArgumentException)
                    {
                        target[i] = DBNull.Value;
                    }
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                // There are more columns in this row than
                // the original schema allows.  Stop copying
                // and return the DataRow.
                return target;
            }
            return target;
        }
        public override bool CanDelete
        {
            get
            {
                return true;
            }
        }
        private string getDeleteRecord_file()
        {
            if (this.isPortal)
            {
                return "deleterecord_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/deleterecord.asp";
            }
        }
            

        /*
            Parameters
            keys
            An IDictionary of object or row keys to be deleted by the ExecuteDelete operation.
            oldValues
            An IDictionary of name/value pairs that represent data elements and their original values.
            Return Value
            The number of items that were deleted from the underlying data storage.          
         */
        protected override int ExecuteDelete(IDictionary keys, IDictionary values)
        {
            string whereClause = "";
            foreach (DictionaryEntry KeyEntry in keys)
            {
                if (whereClause != "")
                    whereClause += " and ";
                whereClause += KeyEntry.Key.ToString() + "=" + KeyEntry.Value.ToString();
            }
            if (whereClause == "")
            {
                foreach (DictionaryEntry KeyEntry in values)
                {  //the first field is the id field
                    whereClause += KeyEntry.Key.ToString() + "=" + KeyEntry.Value.ToString();
                    break;
                }
            }
            if (whereClause == "")
            {
                whereClause = this.FWhereClause;
            }
            string RequestResult = FSageCRMCustom._GetHTML(this.getDeleteRecord_file(), "&TableName=" + this.FTableName + "&WhereClause=" + whereClause, "", false);
            return Convert.ToInt32(RequestResult);
        }
        // The SageCRMDataSourceView does not currently
        // permit insertion of a new record. You can
        // modify or extend this sample to do so.
        public override bool CanInsert
        {
            get
            {
                return true;
            }
        }
        /*
         *  Parameters
            values
            An IDictionary of name/value pairs used during an insert operation.
            Return Value
            The number of items that were inserted into the underlying data storage.
         */
        protected override int ExecuteInsert(IDictionary values)
        {
            DataColumn col;
            //get the schema xml
            string xmlSchema = _getFromCache("insert_TableName=" + this.FTableName);
            if ((xmlSchema == null) || (xmlSchema == ""))
            {
                xmlSchema = FSageCRMCustom._GetHTML(this.getTableSchema_file(), "&TableName=" + this.FTableName, "", false);
                    _setInCache("insert_TableName=" + this.FTableName, xmlSchema);
                }
            //build our StreamReaders
            StringReader xmlStreamSchema = new StringReader(xmlSchema.ToString());
            //the schema dataset
            DataSet objSchema = new DataSet();
            objSchema.ReadXml(xmlStreamSchema);
            DataTable data = new DataTable();
            //setup the schema of the columns using our schema dataset
            for (int i = 0; i < objSchema.Tables[0].Rows.Count; i++)  //each row describes a column
            {
               string fieldType= objSchema.Tables[0].Rows[i]["FieldType"].ToString(); 
               string fieldName=objSchema.Tables[0].Rows[i]["FieldName"].ToString();
               string fieldCaption = objSchema.Tables[0].Rows[i]["FieldCaption"].ToString();
               if (i == 0)
               {
                   idField = fieldName;
               }
               if (fieldType=="int"){
                  col = new DataColumn(fieldName, typeof(int));
               }else
               if (fieldType == "datetime")
               {
                 col = new DataColumn(fieldName, typeof(DateTime));
                 col.DateTimeMode = System.Data.DataSetDateTime.Utc;
               }
               else
               {  //default string
                 col = new DataColumn(fieldName, typeof(string));
               }
               col.AllowDBNull = true;
               col.Caption = fieldCaption;
               data.Columns.Add(col);
            }
            // Enumerate and display all key and value pairs.
            string KeyName = "";
            string KeyValue = "";
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            //build the header
            builder.AppendFormat("<data>");
            foreach (DictionaryEntry Entry in values)
            {
                KeyName = Entry.Key.ToString();
                builder.AppendFormat("<" + KeyName + ">");
                if (Entry.Value == null)
                {
                    KeyValue = "";
                }
                else
                {
                    KeyValue = SecurityElement.Escape(Entry.Value.ToString());
                }
                builder.AppendFormat(KeyValue);
                builder.AppendFormat("</" + KeyName + ">");
            }
            builder.AppendFormat("</data>");
            string _NoTLS = "";
            if (FNoTLS)
            {
                _NoTLS = "&NoTLS=Y";
            }
            //april 2018..add in workflow to mobilex...w= is workflow and s= is state
            string _workflow = "";
            if ((FWorkflow!="")&&(FWorkflowState!=""))
            {
                _workflow = "&w=" + FWorkflow + "&s=" + FWorkflowState;
            }
            string RequestResult = FSageCRMCustom._GetHTML(this.getInsertRecord_file(), "&TableName=" + this.FTableName + _NoTLS+_workflow, builder.ToString(), false);
            this.RecordID = RequestResult;
            //in this case RequestResult is the id of the new record
            return 1;
        }
        private string getInsertRecord_file()
        {
            if (this.isPortal)
            {
                return "insertrecord_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/insertrecord.asp";
            }
        }
            
        // The SageCRMDataSourceView does not currently
        // permit update operations. You can modify or
        // extend this sample to do so.
        public override bool CanUpdate
        {
            get
            {
                return true;
            }
        }
        public override bool CanPage
        {
            get
            {
                return true;
            }
        }
        public override bool CanRetrieveTotalRowCount
        {
            get
            {
                return true;
            }
        }
        /*
            Parameters
            keys
            An IDictionary of object or row keys to be updated by the update operation.
            values
            An IDictionary of name/value pairs that represent data elements and their new values.
            oldValues
            An IDictionary of name/value pairs that represent data elements and their original values.
            Return Value
            The number of items that were updated in the underlying data storage.        
         */
        protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
        {
            // Enumerate and display all key and value pairs.
            string KeyName="";
            string KeyValue="";
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            //build the header
            builder.AppendFormat("<data>");
            foreach (DictionaryEntry Entry in values)
            {
                KeyName = Entry.Key.ToString();
                builder.AppendFormat("<" + KeyName + ">");
                if (Entry.Value == null)
                {
                    KeyValue = "";
                }
                else
                {
                    KeyValue = SecurityElement.Escape(Entry.Value.ToString());
                }
                builder.AppendFormat(KeyValue);
                builder.AppendFormat("</" + KeyName + ">");
            }
            builder.AppendFormat("</data>");

            string whereClause="";
            foreach (DictionaryEntry KeyEntry in keys)  
            {
                if (whereClause != "")
                    whereClause += " and ";
                whereClause += KeyEntry.Key.ToString() + "=" + KeyEntry.Value.ToString();
            }
            if (whereClause == "")
            {
                foreach (DictionaryEntry KeyEntry in oldValues)  
                {  //the first field is the id field
                      whereClause += KeyEntry.Key.ToString() + "=" + KeyEntry.Value.ToString();
                      break;
                }
            }
            if (whereClause == "")
            {
                whereClause = this.FWhereClause;
            }
            string _NoTLS = "";
            if (FNoTLS)
            {
                _NoTLS = "&NoTLS=Y";
            }
            string RequestResult = FSageCRMCustom._GetHTML(this.getUpdateRecord_file(), "&TableName=" + this.FTableName + "&WhereClause=" + whereClause + _NoTLS, builder.ToString(), false);
            //string RequestResult = FSageCRMCustom._GetHTML(this.getUpdateRecord_file(), "&TableName=" + this.FTableName + "&WhereClause=" + whereClause, "<data><pers_lastname>testthis</pers_lastname></data>", false);

            // Alex: changed to Int32.TryParse to resolve AC-59: Sync issue on our live system
            //return Convert.ToInt32(RequestResult);
            int resultInt;
            bool parseResult = Int32.TryParse(RequestResult, out resultInt);
            return parseResult ? resultInt : -1;
        }
        private string getUpdateRecord_file()
        {
            if (this.isPortal)
            {
                return "updaterecord_portal.asp";
            }
            else
            {
                return "/CustomPages/SageCRM/component/updaterecord.asp";
            }
        }

        private string _getFromCache(string cacheKey)
        {
            if (ConfigurationManager.AppSettings["DisableCaching"] == "Y")  ///mr 6 apr 18 - added this into try improve speed
            {
                return "";
            }

            string cacheValue = (string) HttpRuntime.Cache[cacheKey];
            if (string.IsNullOrEmpty(cacheValue))
                return "";

            if (cacheValue.ToLower().IndexOf("<html><head>") == 0)
                return "";

            return cacheValue;
        }
        private void _setInCache(string cacheKey, string cachevalue)
        {
            if (ConfigurationManager.AppSettings["DisableCaching"] == "Y")  ///mr 6 apr 18 - added this into try improve speed
            {
                return;
            }

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