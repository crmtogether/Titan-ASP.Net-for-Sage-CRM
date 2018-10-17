<!-- #include file ="SageCRM_Portal.js" -->

<%
//******************************************************************************
//******************************************************************************
/*
  TITAN ASP.NET for Sage CRM
*/
//******************************************************************************
try{
  var SQL=new String(Request.Form('SelectSQL'));
  if( SQL.toString() == "undefined")
  {	
	SQL=new String(Request.QueryString('SelectSQL'));
  }
  //how many records to display
  var Top=new Number(Request.QueryString('Top'));
  //iTop is used when coding
  var iTop=new Number(Request.QueryString('iTop'));  
  //where to display data from.
  var iFrom=new Number(Request.QueryString('iFrom'));
  debugcrm("selectsql_portal", Request.QueryString);
  /*
  test code
  
  SQL="select * from person where pers_personid<100";
  iFrom=10;
  Top=3;    

  */
  if ((iTop!="") && (Defined(iTop)))
  {
    Top=iTop;
  }
  if (isNaN(Top)){
    Top=-1;
  }
  if (isNaN(iFrom)){
    iFrom=-1;
  }

  var result="<?xml version=\"1.0\" standalone=\"yes\"?>";
  result+="<data>";
  var record=eWare.CreateQueryObj(SQL);
  record.SelectSQL();
  iTopCount=0;
  iRowCount=0;  
  while (!record.eof)
  {
      iRowCount++;
      if  ( (iFrom==-1) || (iFrom<iRowCount) )
      {
        if ( (Top==-1) || (Top==0) ||(iTopCount<Top) ){
          eQueryFields = new Enumerator(record);
          result+="<datatable>";
          while (!eQueryFields.atEnd()) {
            var fieldx=eQueryFields.item();
            fieldx=fieldx.toLowerCase();
            fieldx=fieldx.replace(/\s/g, "");        
            fieldval=record.FieldValue(fieldx);
            if (!Defined(fieldval)){
              fieldval="";
            }
            result+="<"+escape(fieldx)+">"+CustomEscape(fieldval)+"</"+escape(fieldx)+">";
            eQueryFields.moveNext();
          }
          result+="</datatable>";
        }            
        iTopCount++;        
      }
      record.Next();
  }
  //total row count info now
  result+="<crmtogethertrc>";
  result+="<crmtogethertotal>"+iRowCount;  
  result+="</crmtogethertotal>";    
  result+="</crmtogethertrc>";
    
  result+="</data>";
  Response.Write(result);
  debugcrm("selectsqlres_portal", result);    
  Response.End();
}catch(e){
  logerror(e);
  debugcrm("selectsql_portal", result);
}

%>