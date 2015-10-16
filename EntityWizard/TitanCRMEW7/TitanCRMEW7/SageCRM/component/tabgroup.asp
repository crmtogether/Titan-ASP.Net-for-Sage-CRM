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
  var crmtabgroup=Request.QueryString('crmtabgroup');
  //Response.Write(Request.QueryString);
  var strtabgroup=eWare.GetTabs(crmtabgroup);
  Response.Write(strtabgroup);
}catch(e){
  debugcrm("tabgroup", e.description);
  logerror(e);
}
%>