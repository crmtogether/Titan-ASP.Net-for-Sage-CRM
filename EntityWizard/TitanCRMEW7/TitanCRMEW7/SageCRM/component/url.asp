<!-- #include file ="SageCRM.js" -->
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
  var param=Request.QueryString('url');
  var surl=eWare.Url(param);
  Response.Write(surl);
}catch(e){
  logerror(e);
}
%>
