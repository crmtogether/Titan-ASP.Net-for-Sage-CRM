<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET for Sage CRM
*/
//******************************************************************************
Response.clear();
  var SQL=new String(Request.QueryString('SQL'));
  
  var result="";  
  //we stop anything other than an update method being run
  if (SQL.indexOf('Update')!=0)
  {
      Response.Write("Error: Cannot be run from client API");  
      Response.End();
  }
  //Response.Write(SQL);
//  eWare.ExecSql(SQL);
%>