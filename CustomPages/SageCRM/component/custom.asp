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

  if (method=="button"){

     Response.Write(eWare.Button("Cancel", "cancel.gif", "link"));

  }else{
    Response.Write("Invalid method name");
  }
}catch(e){
  logerror(e);
}
%>
