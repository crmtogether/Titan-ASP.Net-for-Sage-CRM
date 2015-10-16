<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Sage CRM ASP.NET Component Suite
*  Version 2.0
*/
//******************************************************************************

try{
  var TableName=new String(Request.QueryString('TableName'));
  var WhereClause=new String(Request.QueryString('WhereClause'));

  xmlDoc = Server.CreateObject("MSXML.DOMDocument");
  xmlDoc.loadXML(Request.Form);
  strQuery = "data";
  xmlDocRoot = xmlDoc.selectSingleNode( strQuery );
  urec=eWare.FindRecord(TableName,WhereClause);
  var noOfRecordsToDelete=urec.RecordCount;
  urec.DeleteRecord=true;
  urec.SaveChanges();
  Response.Write(noOfRecordsToDelete);//success
}catch(e){
  Response.Write("-1");//failed
}
Response.End();
%>