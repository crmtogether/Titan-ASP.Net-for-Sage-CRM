<%

  var QueryObj=eWare.CreateQueryObj(SelectSQL);
  QueryObj.SelectSQL();

  eQueryFields = new Enumerator(QueryObj);
  icol=0;
  while (!eQueryFields.atEnd()) {
    icol++;
    var fieldx=eQueryFields.item();
    fieldx=fieldx.toLowerCase();
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
  }


%>