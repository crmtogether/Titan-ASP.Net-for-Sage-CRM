<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET for Sage CRM
*/
//******************************************************************************
//try{

function WriteFile(_data)
{
var FileOpener = new ActiveXObject("Scripting.FileSystemObject");
var FilePointer = FileOpener.OpenTextFile("C:\\temp\\sample.txt", 8, true);
FilePointer.WriteLine(_data);
FilePointer.Close();
}

Response.clear();
  var SQL=new String(Request.Form('SelectSQL'));
  
  //WriteFile(SQL);
  if( SQL == "undefined")
  {	
	SQL=new String(Request.QueryString('SelectSQL'));
  } else {
	var r1;
    r1 = SQL.replace(/%26/g, "&");
   // SQL=r1;
  }
  //WriteFile(SQL);
  //Response.Write(SQL);
  //Response.End();
  
  //how many records to display
  var Top=new Number(Request.QueryString('Top'));
  //iTop is used when coding
  var iTop=new Number(Request.QueryString('iTop'));
  //where to display data from.
  var iFrom=new Number(Request.QueryString('iFrom'));
  /*
  test code
  
  SQL="select * from person where pers_personid<100";
  iFrom=10;
  Top=3;    

  */
  if ((iTop!="") && (Defined(iTop)))
  {
    Top=iTop;
  }
  if (isNaN(Top)) {
    Top=-1;
  }
  if (isNaN(iFrom)){
    iFrom=-1;
  }

  var result="<?xml version=\"1.0\" standalone=\"yes\"?>";
  result+="<data>";
  
  if (ado_ConnectionString!="")
  {
%>
<!-- #include file ="selectsql_ado.js" -->
<%
  }else{
%>
<!-- #include file ="selectsql_crm.js" -->
<%
  
  }

  //total row count info now
  result+="<crmtogethertrc>";
  result+="<crmtogethertotal>"+iRowCount;  
  result+="</crmtogethertotal>";    
  result+="</crmtogethertrc>";
    
  result+="</data>";
  Response.Write(result);
  debugcrm("selectsqlres", result);  
  
  Response.End();
//}catch(e){
//  logerror(e);
//  debugcrm("selectsql", result);
//}

%>