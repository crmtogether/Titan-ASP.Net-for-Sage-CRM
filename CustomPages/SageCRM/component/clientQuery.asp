<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET for Sage CRM
*/
//******************************************************************************
//try{
Response.clear();
  var SQL=new String(Request.QueryString('SQL'));
  var FieldName=new String(Request.QueryString('FieldName'));
  var result="";  
  //we stop any delete method being run
  if (SQL.indexOf('delete')!=-1)
  {
      Response.Write("Error: Delete cannot be run from client API");  
      Response.End();
  }
  if (ado_ConnectionString!="")
  {
%>
<!-- #include file ="selectsql_ado_client.js" -->
<%
  }else{
%>
<!-- #include file ="selectsql_crm_client.js" -->
<%
  
  }  
  Response.Write(result);  
  Response.End();
//}catch(e){
//  logerror(e);
//  debugcrm("selectsql", result);
//}

%>