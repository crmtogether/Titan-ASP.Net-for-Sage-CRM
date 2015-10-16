<!-- #include file ="SageCRM_Portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Sage CRM ASP.NET Component Suite
*  Version 2.0
*/
//******************************************************************************
try
{
  var FieldName=Request.QueryString('FieldName');  
  var result=eWare.VisitorInfo(FieldName);
  Response.Write(result);
}catch(e){
  logerror(e);
}
%>
