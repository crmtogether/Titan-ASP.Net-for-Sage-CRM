<%  
  var record=eWare.CreateQueryObj(SQL);
  record.SelectSQL();
  if (!record.eof)
  {
     result=record.FieldValue(FieldName);
  }
%>