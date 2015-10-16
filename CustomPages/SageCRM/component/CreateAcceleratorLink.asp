<!-- #include file ="SageCRM.js" -->
<!-- #include file ="nonSysFields.asp" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try{
  var TableName=new String(Request.QueryString('TableName'));
  var TableId=new String(Request.QueryString('TableId'));
  var LinkTableName=new String(Request.QueryString('LinkTableName'));
  var LinkTableId=new String(Request.QueryString('LinkTableId'));

  var crec=eWare.FindRecord("AcceleratorLinks","acli_tablename='"+TableName+"' and acli_tableid="+TableId+
        " and acli_linktablename='"+LinkTableName+"' and acli_linktableid="+LinkTableId);
  if (crec.eof)  //we dont add duplicates
  {
    urec=eWare.CreateRecord("AcceleratorLinks");
    urec("acli_tablename")=TableName;
    urec("acli_tableid")=TableId;
    urec("acli_linktablename")=LinkTableName;
    urec("acli_linktableid")=LinkTableId;
    urec("acli_user_userid")=eWare.GetContextInfo("users","user_userid");
    urec.SaveChanges();
    Response.Write(urec.RecordId);//success
  }else{
    Response.Write("-1");//fail  
  }
}catch(e){
  Response.Write('Error:Saving Record: '+TableName+ '  -  Desc: ' + e.description + '  -  ErrorNo: '+e.number + '  -XML: '+Request.Form);//failed
}
Response.End();
%>