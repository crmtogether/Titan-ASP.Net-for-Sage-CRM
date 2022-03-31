<!-- #include file ="SageCRM_portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try
{
  var sql="select Parm_Value from Custom_SysParams where Parm_Name='Version'";
  var q=CRM.CreateQueryObj(sql);
  q.SelectSQL();
  if(!q.eof)
  {
	Response.Write('Version:'+q('parm_value'));  
  }else{
    Response.Write('No version found');  
  }
}catch(e){
  logerror(e);
}
%>