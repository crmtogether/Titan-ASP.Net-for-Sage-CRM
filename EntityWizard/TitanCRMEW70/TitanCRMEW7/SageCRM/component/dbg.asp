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
  //change this to the path for the page you want to debug
  var pagepath="full debug path here";

  var pagepath="http://localhost:5768/dslist2.aspx?";
  
  var testurl = new String(eWare.Url("test.asp"));
  QuesArr=testurl.split("?");
  Qstr=QuesArr[1];
  //extract the SID
  var urlarr = Qstr.split("&");
  var ourQstr=new String("");
  for (var i=0;i<(urlarr.length-1);i++){
    bstr=new String(urlarr[i]);
    if ((bstr.indexOf("Key")==0) ) {    
//    if ((bstr.indexOf("SID")==0) || (bstr.indexOf("Key")==0) ) {
      if (ourQstr!="")
        ourQstr+="&";
      ourQstr+=bstr;
    }
  }
  var param=pagepath + ourQstr;
  
  Response.Write('<script>');
  Response.Write('document.location.href="'+param+'";');
  Response.Write('</script>');
}catch(e){
  logerror(e);
}
%>
