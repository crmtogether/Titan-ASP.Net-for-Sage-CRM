<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try
{
  var param=Request.QueryString('url');
  var surl=eWare.Url(param);
  Response.Write(surl);
}catch(e){
  logerror(e);
}
%>
