<%  
  var ado_connection=Server.CreateObject("ADODB.Connection");
  var record = Server.CreateObject("ADODB.Recordset");

  ado_connection.Open(ado_ConnectionString);
    
  SQL=unescape(SQL);  //required for ado
  record.Open(SQL , ado_connection, adOpenForwardOnly, adLockReadOnly, adCmdText);

  if (!record.EOF)
  {
    var fieldval=new String(eQueryFields.item());
    fieldval=fieldval.rtrim();
    result+=CustomEscape(fieldval);      
  }  
  record.Close();
  record=null;
  ado_connection.Close();
%>