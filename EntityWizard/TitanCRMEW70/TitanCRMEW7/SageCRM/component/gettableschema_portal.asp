<!-- #include file ="SageCRM_Portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
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

  //ID Field is first always
  var bordRec=eWare.CreateQueryObj("select bord_idfield from custom_tables where bord_name='"+TableName+"'"); 
  bordRec.SelectSQL();
  var idFieldName=new String(bordRec("bord_idfield"));
  try
  {
    var fieldrec = eWare.CreateQueryObj("select top 1 * from "+TableName);        
    fieldrec.SelectSQL();    
  }catch(Err)
  {
    var fieldrec = eWare.FindRecord(TableName,idFieldName+"=-1");        
    //fieldrec.SelectSQL();      
  }
  idFieldName=idFieldName.toLowerCase();
  result+="<"+TableName+">";
  result+="<FieldName>"+bordRec("bord_idfield")+"</FieldName>";
  result+="<FieldType>int</FieldType>";
  result+="<FieldCaption>"+eWare.GetTrans("ColNames",bordRec("bord_idfield"))+"</FieldCaption>";
  result+="</"+TableName+">";
  eQueryFields = new Enumerator(fieldrec);
  while (!eQueryFields.atEnd()) {
    var fieldx=new String(eQueryFields.item());
    fieldx=fieldx.toLowerCase();
    fieldx=fieldx.replace(/\s/g, "");
    if (idFieldName!=fieldx)
    {
      result+="<"+TableName+">";
      result+="<FieldName>"+escape(fieldx)+"</FieldName>";
      result+="<FieldType>";
      result+="string";  //everything is returned as a string type
      result+="</FieldType>";
      result+="<FieldCaption>"+CustomEscape(eWare.GetTrans("ColNames",fieldx))+"</FieldCaption>";
      result+="</"+TableName+">";
    }
    eQueryFields.moveNext();
  }
  result+="</dataschema>";
  Response.Write(result);
  Response.End;
}catch(e){
  logerror(e);
}
%>
