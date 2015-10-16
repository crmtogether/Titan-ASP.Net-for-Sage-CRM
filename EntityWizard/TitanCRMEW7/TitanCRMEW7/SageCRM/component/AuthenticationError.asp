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
  var result=eWare.AuthenticationError;
  Response.Write(result);
}catch(e){
  logerror(e);
}
%>
