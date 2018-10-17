<%@CodePage=65001 Language=JavaScript %>
<%
//******************************************************************************
//******************************************************************************
//  TITAN ASP.NET for Sage CRM
//******************************************************************************

//we check that calls come from local machine only...this is to prevent sql injection attacks by accessing the .asp pages directly
//this should be commented in when code deployed to live server
var localip=new String(Request.ServerVariables("LOCAL_ADDR"));
var remoteip=new String(Request.ServerVariables("REMOTE_ADDR"));

if (localip.indexOf(remoteip)!=0)
{
  Response.Write("Cannot execute this page from remote server with IP address: "+remoteip);
  Response.End();
}


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
    
  CRM = eWare = Server.CreateObject("eWare.eWareSelfService");
  var eMsg=eWare.Init(
      Request.Querystring,
      Request.Form,
    	Request.Cookies("eware"),true);

	if (eMsg!="")
	{
		Response.Write(eMsg);
		Response.End;
	}
	var Button_Default="1", Button_Delete="2", Button_Continue="4";

	Response.Expires=-1;

	function Defined(Arg)
	{
		return (Arg+""!="undefined");
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
    Response.Write("\nError in executing page:\n"+ Request.ServerVariables("PATH_INFO") +"\n\n");
    Response.Write(e+"\n\n");
    Response.Write("Description:"+e.description+"\n");
    Response.Write("Number:"+(e.number & 0xFFFF)+"\n\n");
    Response.Write("Request QueryString\n\n");
    Response.Write(Request.QueryString+"\n\n");
    Response.Write("Unescaped\n");
    Response.Write(unescape(Request.QueryString)+"\n\n");
    Response.Write("Request Form\n\n");
    Response.Write(Request.Form+"\n");
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
  function _xlog(sender, msg)
  {
        return;
      var _logr=CRM.CreateRecord("ctportallog");
      _logr("ctpo_description")=sender;
      _logr("ctpo_details")=msg;
      _logr.SaveChanges();
  }
%>