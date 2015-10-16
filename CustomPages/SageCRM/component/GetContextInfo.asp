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
  var Context=Request.QueryString('Context');
  var FieldName=Request.QueryString('FieldName');
  var result=eWare.GetContextInfo(Context, FieldName);
  Response.Write(result);
}catch(e){
  logerror(e);
}
%>
