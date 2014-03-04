<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************

try{
  var caption=new String(Request.QueryString('Caption'));
  var imagename=new String(Request.QueryString('ImageName'));
  var url=new String(Request.QueryString('Url'));
  var permissionsentity="";
  if (Defined(Request.QueryString('PermissionsEntity')))
    permissionsentity=new String(Request.QueryString('PermissionsEntity'));
  var permissionstype="";
  if (Defined(Request.QueryString('PermissionsType')))
    permissionstype=new String(Request.QueryString('PermissionsType'));
  var target = "";
  if (Defined(Request.QueryString('Target')))
    target=new String(Request.QueryString('Target'));

  if (url.indexOf("link:")>-1){
    var newurl=new String(url);
    newurl=newurl.substring(5,newurl.length);
  }else
  if (url.indexOf("javascript:")>-1){
    var newurl=url;
  }else{
    var newurl=eWare.Url(url);
  }
  Response.Write(eWare.Button(caption, imagename, newurl, permissionsentity,permissionstype,target));
  Response.End;
}catch(e){
  logerror(e);
}
%>
