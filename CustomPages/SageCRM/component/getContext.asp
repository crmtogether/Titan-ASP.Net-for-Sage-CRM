<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET for Sage CRM
*/
//******************************************************************************
try{
  var TableName=new String(Request.QueryString('TableName'));
  var WhereClause=new String(Request.QueryString('WhereClause'));
  var FieldName=new String(Request.QueryString('FieldName'));
/*  
//s  test code
  
  TableName="Company";
 // iFrom=10;
  iTop=1;
  WhereClause="comp_companyid=123";
  FieldName="comp_name";
  */  
  debugcrm("getContext_qs", Request.QueryString);
  if (!Defined(WhereClause)){
    WhereClause="";
  }
  var record=eWare.FindRecord(TableName,WhereClause);
  result=new String("");
  if (!record.eof)
  {
      result=record[FieldName];
  }
  debugcrm("getContext", result);
  Response.Write(result);
  Response.End();
}catch(e){
  logerror(e);
}

function _getuser(uid){
  if (((Defined(uid)) && (uid!='')) && (!isNaN(uid))) {
    var urec=eWare.FindRecord("Users","user_userid="+uid);
    return urec("user_firstname")+" "+urec("user_lastname");
  }
  return '';
}
%>