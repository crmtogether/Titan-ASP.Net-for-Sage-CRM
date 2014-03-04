<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Sage CRM ASP.NET Component Suite
*  Version 2.0
*/
//******************************************************************************
try{
  var SelectSQL=new String(Request.QueryString('SelectSQL'));
  var result="<?xml version=\"1.0\" standalone=\"yes\"?>";
  result+="<dataschema>";
  debugcrm("tableschema_sql", Request.QueryString);

  //alter the select statement so that we only get one row back
  var r, re;
  re = /Select/g;
  r = SelectSQL.replace(re, "select top 1 ");
  SelectSQL=r;
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
    result+="<FieldCaption>"+escape(eWare.GetTrans("ColNames",fieldx))+"</FieldCaption>";
    result+="</datatable>";
    eQueryFields.moveNext();
  }

  result+="</dataschema>";
debugcrm("gettableschema_selectsql", result);
  Response.Write(result);
  Response.End;
}catch(e){
  logerror(e);
  debugcrm("gettableschema_selectsql", result);
}
%>