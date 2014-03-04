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
  var params=Request.QueryString('params');
  var method=Request.QueryString('method');

  if(method='exec')
  {
    eWare.RefreshMetaData();  
  }

}catch(e){
  logerror(e);
}
%>
