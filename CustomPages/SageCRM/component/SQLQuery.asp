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
  
  var result="";  
  //we stop any delete method being run
  if (SQL.indexOf('delete')!=-1)
  {
      Response.Write("Error: Delete cannot be run from client API");  
      Response.End();
  }
  //For the Discovery grid we do this due to bug in Sage CRM's query object (due to be fixed in 7.1)
  //ado_ConnectionString="DSN=crmdns;UID=sa;PWD=Passw0rd;Database=CRM62sp1";
  if (ado_ConnectionString!="")
  {
%>
<!-- #include file ="selectsql_ado_SQLclient_JSON.js" -->
<%
  }else{
%>
<!-- #include file ="selectsql_crm_SQLclient_JSON.js" -->
<%
  
  }  
  Response.Write(result);  
  Response.End();
//}catch(e){
//  logerror(e);
//  debugcrm("selectsql", result);
//}

%>