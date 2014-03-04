<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try{
Response.clear();
  var param=Request.QueryString('ExecSQL');
  var query=eWare.CreateQueryObj(param);
  query.ExecSql();
  Response.Write("1");
}catch(e){
  Response.Write("0");
}
Response.End;
%>
