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
  var PermissionsEntity=Request.QueryString('PermissionsEntity');
  var PermissionsType=Request.QueryString('PermissionsType');

  Response.Write(eWare.Button("Save", "Save.gif", "javascript:document.EntryForm.submit();",
               PermissionsEntity,PermissionsType,""));
}catch(e){
  logerror(e);
}
%>
