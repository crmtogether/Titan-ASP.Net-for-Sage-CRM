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
  var result=eWare.AuthenticationError;
  Response.Write(result);
}catch(e){
  logerror(e);
}
%>
