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
  var crmtabgroup=Request.QueryString('crmtabgroup');
  //Response.Write(Request.QueryString);
  var strtabgroup=eWare.GetTabs(crmtabgroup);
  Response.Write(strtabgroup);
}catch(e){
  debugcrm("tabgroup", e.description);
  logerror(e);
}
%>