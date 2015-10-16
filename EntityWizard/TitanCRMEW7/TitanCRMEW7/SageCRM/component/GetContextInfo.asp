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
  var Context=Request.QueryString('Context');
  var FieldName=Request.QueryString('FieldName');
  var result=eWare.GetContextInfo(Context, FieldName);
  Response.Write(result);
}catch(e){
  logerror(e);
}
%>
