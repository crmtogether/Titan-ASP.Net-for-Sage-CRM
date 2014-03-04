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
  var PermissionsEntity=Request.QueryString('PermissionsEntity');
  var PermissionsType=Request.QueryString('PermissionsType');

  Response.Write(eWare.Button("Save", "Save.gif", "javascript:document.EntryForm.submit();",
               PermissionsEntity,PermissionsType,""));
}catch(e){
  logerror(e);
}
%>
