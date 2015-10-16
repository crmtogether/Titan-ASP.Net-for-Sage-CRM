<!-- #include file ="SageCRM_Portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try
{
  var Family=Request.QueryString('Family');
  var Caption=Request.QueryString('Caption');
  var result=eWare.GetTrans(Family, Caption);
  Response.Write(result);
}catch(e){
  logerror(e);
}
%>
