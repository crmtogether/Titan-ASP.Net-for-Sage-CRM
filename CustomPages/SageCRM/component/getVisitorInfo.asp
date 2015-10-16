<!-- #include file ="SageCRM_Portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
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
