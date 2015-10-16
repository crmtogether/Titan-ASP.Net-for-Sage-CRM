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
  var FieldValue=Request.QueryString('FieldValue');  
  eWare.VisitorInfo(FieldName)=FieldValue;
}catch(e){
  logerror(e);
}
%>