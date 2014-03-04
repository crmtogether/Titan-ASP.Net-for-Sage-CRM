<%  
  var ado_connection=Server.CreateObject("ADODB.Connection");
  var QueryObj = Server.CreateObject("ADODB.Recordset");

  ado_connection.Open(ado_ConnectionString);
    
  SelectSQL=unescape(SelectSQL);  //required for ado
  QueryObj.Open(SelectSQL , ado_connection, adOpenForwardOnly, adLockReadOnly, adCmdText);

  eQueryFields = new Enumerator(QueryObj.Fields);

  icol=0;
  while (!eQueryFields.atEnd()) {
    var fieldx=new String("");
    fieldx=QueryObj.Fields(icol).Name;       
    fieldx=fieldx.toLowerCase();
    //fieldx=fieldx.rtrim();
    fieldx=fieldx.replace(/\s/g, "");
    result+="<datatable>";
    if (fieldx=="")
    {
      fieldx="nocolumnname"+icol;
    }    
    result+="<FieldName>"+escape(fieldx)+"</FieldName>";
    result+="<FieldType>";
    result+="string";  //everything is returned as a string type
    result+="</FieldType>";
    result+="<FieldCaption>"+CustomEscape(eWare.GetTrans("ColNames",fieldx))+"</FieldCaption>";
    result+="</datatable>";
    eQueryFields.moveNext();
    icol++;
  }  

  QueryObj.Close();
  QueryObj=null;
  ado_connection.Close();
%>