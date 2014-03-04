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
  var FieldValue=Request.QueryString('FieldValue');  
  eWare.VisitorInfo(FieldName)=FieldValue;
}catch(e){
  logerror(e);
}
%>