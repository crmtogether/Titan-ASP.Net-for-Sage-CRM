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
  var SelectSQL=new String(Request.QueryString('SelectSQL'));
  var result="<?xml version=\"1.0\" standalone=\"yes\"?>";
  result+="<dataschema>";
  debugcrm("tableschema_sql", Request.QueryString);
  
  //alter the select statement so that we only get one row back
  var r, re;
  re = /Select/g;
  r = SelectSQL.replace(re, "select top 1 ");
  SelectSQL=r;

  if (ado_ConnectionString!="")
  {
%>
<!-- #include file ="gettableschema_selectsql_ado.js" -->
<%
  }else{
%>
<!-- #include file ="gettableschema_selectsql_crm.js" -->
<%
  
  }

  result+="</dataschema>";
debugcrm("gettableschema_selectsql", result);
  Response.Write(result);
  Response.End;
}catch(e){
  logerror(e);
  debugcrm("gettableschema_selectsql", result);
}
%>