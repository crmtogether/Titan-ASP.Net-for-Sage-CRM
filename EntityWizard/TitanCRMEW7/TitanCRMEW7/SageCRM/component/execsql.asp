<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Sage CRM ASP.NET Component Suite
*  Version 2.0
*/
//******************************************************************************
try{
  var param=Request.QueryString('ExecSQL');
  var query=eWare.CreateQueryObj(param);
  query.ExecSql();
  Response.Write("1");
}catch(e){
  Response.Write("0");
}
Response.End;
%>
