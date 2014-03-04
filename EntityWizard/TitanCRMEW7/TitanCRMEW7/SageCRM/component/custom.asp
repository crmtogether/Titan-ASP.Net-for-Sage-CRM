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
