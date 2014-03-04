<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.Net Suite for Sage CRM from CRM Together
*  
*  To debug do something like this and setting the table name
*  http://localhost/crm61/custompages/sagecrm/component/gettableschema.asp?SID=40740076948278&TableName=company
*/
//******************************************************************************
try{
  var TableName=new String(Request.QueryString('TableName'));

  var result="<?xml version=\"1.0\" standalone=\"yes\"?>";
  result+="<dataschema>";
  
//******************************************************************************
//Original query...changed as the crm metadata is missing some information
//  var fieldrec = eWare.CreateQueryObj("select colp_colname, colp_entrytype from custom_edits where ColP_Entity = '"+TableName+ "'"+
//    " and ((colp_system is null) or (colp_system='')) ORDER BY ColP_ColPropsId");
//******************************************************************************
  var useX=true;
  
  try
  {  
    var fieldrec = eWare.CreateQueryObj("select top 1 * from "+TableName);    
    fieldrec.SelectSQL();
    eQueryFields = new Enumerator(fieldrec);
  }catch(e)
  {   //for external tables and views
    useX=false;
    var fieldrec = eWare.CreateQueryObj("select colp_colname, colp_entrytype from custom_edits where ColP_Entity = '"+TableName+ "'"+
                     " and ((colp_system is null) or (colp_system='')) ORDER BY ColP_ColPropsId");
    fieldrec.SelectSQL();
  }  

  //ID Field is first always
  var bordRec=eWare.CreateQueryObj("select bord_idfield from custom_tables where bord_name='"+TableName+"'"); 
  bordRec.SelectSQL();
  var idFieldName=new String(bordRec("bord_idfield"));
  idFieldName=idFieldName.toLowerCase();
  result+="<"+TableName+">";
  result+="<FieldName>"+bordRec("bord_idfield")+"</FieldName>";
  result+="<FieldType>int</FieldType>";
  result+="<FieldCaption>"+eWare.GetTrans("ColNames",bordRec("bord_idfield"))+"</FieldCaption>";
  result+="</"+TableName+">";
  eQueryFields = new Enumerator(fieldrec);
  if (useX){
    while (!eQueryFields.atEnd()) {
      var fieldx=new String(eQueryFields.item());
      fieldx=fieldx.toLowerCase();
      fieldName=new String(fieldx);
      fieldName=fieldName.replace(/\s/g, "");
      if (idFieldName!=fieldx)
      {
        result+="<"+TableName+">";
        result+="<FieldName>"+escape(fieldName)+"</FieldName>";
        result+="<FieldType>";
        result+="string";  //everything is returned as a string type
        result+="</FieldType>";
        result+="<FieldCaption>"+CustomEscape(eWare.GetTrans("ColNames",fieldx))+"</FieldCaption>";
        result+="</"+TableName+">";
      }
      eQueryFields.moveNext();
    }
  }else{
    while (!fieldrec.eof){
      fieldx=fieldrec("colp_colname");
      fieldName=new String(fieldx);
      fieldName=fieldName.replace(/\s/g, "");
      if (idFieldName!=fieldx)
      {
        result+="<"+TableName+">";
        result+="<FieldName>"+escape(fieldName)+"</FieldName>";
        result+="<FieldType>";
        result+="string";  //everything is returned as a string type
        result+="</FieldType>";
        result+="<FieldCaption>"+CustomEscape(eWare.GetTrans("ColNames",fieldx))+"</FieldCaption>";
        result+="</"+TableName+">";
      }      
      fieldrec.NextRecord();
    } 
  }
  result+="</dataschema>";
  Response.Write(result);
  debugcrm("gettableschema", result);
  Response.End();
}catch(e){
  logerror(e);
  debugcrm("gettableschema", result);
}
%>