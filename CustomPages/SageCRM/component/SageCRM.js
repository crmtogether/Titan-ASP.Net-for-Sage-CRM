<%@ CodePage=65001 Language=JavaScript%>
<%
//******************************************************************************
//******************************************************************************
//*  TITAN ASP.NET Suite for Sage CRM
//******************************************************************************

//work around for sage crm query object buffer bug
var ado_ConnectionString="";
//sample connection string that uses a dns
//ado_ConnectionString="DSN=crmdns;UID=sa;PWD=Passw0rd;Database=CRM62sp1";
//ado_ConnectionString="driver={SQL Server};server=(local)\SQLEXPRESS;uid=sa;pwd=Passw0rd;database=CRM62sp1";
//ado_ConnectionString="";
var adOpenForwardOnly=0;
var adLockReadOnly=1; 
var adCmdText=1;

var adPersistXML=1;

//CRM Together check that calls come from local machine only...this is to prevent sql injection attacks by accessing the .asp pages directly
///this should be commented in when code deployed to live server
/*
var localip=new String(Request.ServerVariables("LOCAL_ADDR"));
var remoteip=new String(Request.ServerVariables("REMOTE_ADDR"));

if (localip.indexOf(remoteip)!=0)
{
  Response.Write("Cannot execute this page from remote server with IP address: "+remoteip);
  Response.End();
}
*/
	// This is the eWare standard include file for javascript based asp pages
	// using the eWare business object.

	// This file should be included on the first line of your asp file.

	// Please consult the eWare documentation for more information.

	// Pages expire right away

	Response.Expires=-1;

	// eWare mode constants

	var View=0, Edit=1, Save=2, PreDelete=3, PostDelete=4, Clear=6;

	// eWare button position constants

	var Bottom=0, Left=1, Right=2, Top=3;

	// eWare caption location constants

	var CapDefault=0, CapTop=1, CapLeft=2, CapLeftAligned=3, CapRight=4, CapRightAligned=5, CapLeftAlignedRight=6;

  //field entry types
  var iEntryType_Text = 10;
  var iEntryType_MultiText = 11;
  var iEntryType_EmailText = 12;
  var iEntryType_UrlText = 13;
  var iEntryType_Select = 21;
  var iEntryType_UserSelect = 22;
  var iEntryType_ChannelSelect = 23
  var iEntryType_Integer = 31;
  var iEntryType_DateTime = 41;
  var iEntryType_Date = 42;
  var iEntryType_CheckBox = 45;
  var iEntryType_AdvSearchSelect = 56;

	// determine is this is a wap page

	var Accept=new String(Request.ServerVariables("HTTP_ACCEPT"));
	var IsWap=(Accept.indexOf("wml")!=-1);

	var Button_Default="1", Button_Delete="2", Button_Continue="4";

    var iKey_CustomEntity = 58;

	// create and initialise the eWare object

    var sInstallName = getInstallName(Request.ServerVariables("URL"));
    var ClassName = "eWare."+sInstallName;

	var CRM = eWare = Server.CreateObject(ClassName);
	eWare.Host = "DPP";
	
	var eMsg = null;
	
    try
    {
        eMsg = eWare.Init(
            Request.Querystring,
            Request.Form,
            Request.ServerVariables("HTTPS"),
            Request.ServerVariables("SERVER_NAME"),
            true,
            Request.ServerVariables("HTTP_USER_AGENT"),
            Accept);
    }
    catch(e)
    {
        Response.Write("Error occurred during the call to eWare.Init().");
        
        if (e && e.message)
            Response.Write("Exception: " + e.message);
    }
	
	// check for errors

	if (eMsg!="")
	{
		Response.Write(eMsg);
		Response.Write("<br />");
		Response.Write("In IIS check the application pool that your custom site is running in and make sure it is the same application pool as CRM.");
		Response.End;
	}

	// this function is quite useful

	function Defined(Arg)
	{
		return (Arg+""!="undefined");
	}

	function getInstallName(sPath) {
		//Parse the install name out of the path
		var Path = new String(sPath);
		var InstallName = '';
		var iEndChar=0;iStartChar=0;

		Path = Path.toLowerCase();
		iEndChar = Path.indexOf('/custompages');
		if (iEndChar != -1) {
			//find the first '/' before this
			iStartChar = Path.substr(0,iEndChar).lastIndexOf('/');
			iStartChar++
			InstallName = Path.substring(iStartChar,iEndChar);
		}
		return InstallName;

	}

  //splits a comma delimited string and returns the top
  function getZeroField(value){
    var tmparr=value.split(",");
    if ((tmparr.length>0) && (tmparr[0]=="")){
      return tmparr[1];
    }else{
      return tmparr[0];
    }
  }
  //takes a date string in the format dd/mm/yyyyy and converts it to mm/dd/yyyyy
  function fixSQLDate(strDate){
    if ((!Defined(strDate)) || (strDate==""))
      return "";
    bConvert=true;  //change this to false if the mssql system uses the dd/mm/yyyy format
    if (!bConvert)
      return strDate;
    var sDate = new String(strDate);
    return sDate.substr(3,2) + "/" + sDate.substr(0,2) + "/" + sDate.substr(6,4);
  }

  function logerror(e){
    Response.Write("<b>Error in executing page:</b>"+ Request.ServerVariables("PATH_INFO") +"<br />");
    Response.Write(e+"<br />");
    Response.Write("Description:"+e.description+"<br />");
    Response.Write("Number:"+(e.number & 0xFFFF)+"<br />");
    Response.Write("<br /><b>Request QueryString</b><br />");
    Response.Write(Request.QueryString+"<br /><br />");
    Response.Write("<b>Request Form</b><br />");
    Response.Write(Request.Form+"<br />");
    debugcrm("crm_together_error_log", e.description);      
  }
  
  function debugcrm(filename, msg){
    if (false){
      var fs=Server.CreateObject("Scripting.FileSystemObject")
      fname=fs.CreateTextFile("c:\\temp\\"+filename+".txt",true);
      fname.WriteLine(msg);
      fname.Close();    
    }
  }
  function fixPercentageIssue(testsql)  //replace double % with single
  {
    var sql=new String(testsql);
    return sql.replace(/\%%/g,'%');  
  }
  function escapeSQL(testsql)
  {
    var sql=new String(testsql);
    return sql.replace(/\"/g,'\'');
  }
  function getEntityRecord(Entity, EntityId)
  {
     var idcol=getEntityIdColumn(Entity);
     var erec=eWare.FindRecord(Entity, idcol+"="+EntityId);
     return erec;
  }
  function getEntityIdColumn(Entity)
  {
     var etable=eWare.FindRecord("custom_tables", "bord_name='"+Entity+"'");
     return etable("bord_idfield");
  }
  function getEntityDescColumn(Entity)
  {
     var etable=eWare.FindRecord("custom_tables", "bord_name='"+Entity+"'");
     if (etable("bord_descriptionfield")!="")
     {
       return etable("bord_descriptionfield");   
     }else{
       return etable("bord_idfield");        
     }
  }
  function getFieldType(fieldName)
  {
     var etable=eWare.FindRecord("custom_edits", "ColP_ColName='"+fieldName+"'");
     if (etable("ColP_EntryType")!="")
     {
       return etable("ColP_EntryType");   
     }
     return 10;         
  }  
  function getUserName(userid)
  {
     var etable=eWare.FindRecord("users", "user_userid='"+userid+"'");
     return etable("user_firstname")+"&nbsp;"+etable("user_lastname");
  }
  function getTableDescField(tablename, recobj)
  {
     tablename=tablename.toLowerCase();
     switch (tablename) {
       case "company":
         return recobj("comp_name");         
         break;
       case "person":
         return recobj("pers_firstname")+" "+recobj("pers_lastname");         
         break;
       case "opportunity":
         return recobj("oppo_description");         
         break;
       case "cases":
         return recobj("case_description");         
         break;
       case "solutions":
         return recobj("soln_description");         
         break;
       case "lead":
         return recobj("lead_companyname");         
         break;
      default:
        return recobj(getEntityDescColumn(tablename));
        break;
     }
  }
  function getTerrCaption(code)
  {
    if ((!Defined(code)) || (code==""))
      return code;
    var recobj=eWare.FindRecord("Territories","terr_territoryid="+code);
    if (recobj.eof)
    {
      return code;  
    }else
    {
      return recobj("terr_caption");  
    }
  }
    
String.prototype.startsWith = function(str)
{return (this.match("^"+str)==str)}
    
String.prototype.trim = function() {
	return this.replace(/^\s+|\s+$/g,"");
}
String.prototype.ltrim = function() {
	return this.replace(/^\s+/,"");
}
String.prototype.rtrim = function() {
	return this.replace(/\s+$/,"");
}    
  function CustomEscape(fieldvalue)
  {
    var MStr=new String(fieldvalue);
    MStr=encodeURI(MStr);
	//var amp=escape("&");
	var re = /&/g;
    MStr=MStr.replace(re,"&amp;");
    //plus sign now
	var re2 = /\+/g;
    MStr=MStr.replace(re2,"%2B");
    return MStr;
  }
function getCRMVersion()
{
  var recobj=eWare.FindRecord("custom_sysparams","parm_name='Version'");
  return recobj("parm_value");
}

function getCRMTheme()
{
  var recobj=eWare.FindRecord("userSettingsdefault","usetdef_key='PreferredCssTheme'");
  
  return '<LINK REL="stylesheet" HREF="/'+sInstallName+'/Themes/'+recobj("usetdef_value")+'.css" />';
  
}

%>