<!-- #include file ="SageCRM_Portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try{
  var SelectSQL=new String(Request.QueryString('SelectSQL'));

  var result="<?xml version=\"1.0\" standalone=\"yes\"?>";
  result+="<dataschema>";
  //alter the select statement so that we only get one row back
  var r, re;
  re = /Select/g;
  r = SelectSQL.replace(re, "select top 1 ");
  SelectSQL=r;
  var QueryObj=eWare.CreateQueryObj(SelectSQL);
  QueryObj.SelectSQL();

  eQueryFields = new Enumerator(QueryObj);
  while (!eQueryFields.atEnd()) {
    var fieldx=eQueryFields.item();
    fieldx=fieldx.toLowerCase();
    fieldx=fieldx.replace(/\s/g, "");    
    result+="<datatable>";
    result+="<FieldName>"+escape(fieldx)+"</FieldName>";
    result+="<FieldType>";
    result+="string";  //everything is returned as a string type
    result+="</FieldType>";
    result+="<FieldCaption>"+escape(eWare.GetTrans("ColNames",fieldx))+"</FieldCaption>";
    result+="</datatable>";
    eQueryFields.moveNext();
  }

  result+="</dataschema>";

  Response.Write(result);
  debugcrm("tableschema_sql_portal_res", result);

  Response.End;
}catch(e){
  logerror(e);
}
%>
