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
Response.clear();
  var testMode = false;
  var TableName=new String(Request.QueryString('TableName'));
  var WhereClause=new String(Request.QueryString('WhereClause'));
  debugcrm("updaterecord_xmldata", "data="+Request.Form);
  xmlDoc = Server.CreateObject("MSXML.DOMDocument");
  //actual line
  if (!testMode){
    xmlDoc.loadXML(Request.Form);
  }else{
    //testing line
    xmlDoc.load(Server.MapPath("sampledata.xml"));
  }
  strQuery = "data";
  xmlDocRoot = xmlDoc.selectSingleNode( strQuery );
  //Response.Write(xmlDocRoot.childNodes.length);
  urec=eWare.FindRecord(TableName,WhereClause);
  
  var noOfRecordsToUpdate=urec.RecordCount;

  for (var i = 0 ; i < xmlDocRoot.childNodes.length; i++) {
    dataNode=xmlDocRoot.childNodes[i];
    var testStr=new String(dataNode.nodeName);
    var testStr=testStr.toLowerCase();
    if (!isInArray(testStr)){
      if (testMode)
        Response.Write(dataNode.nodeName+"="+dataNode.text+"<br />");
      var ftype=getFieldType(dataNode.nodeName);
      if (((ftype=="41")||(ftype=="42")) && (dataNode.text.indexOf(",")>-1))
      {
        var dat_val=dataNode.text;
        var evaldt="var d=new Date("+dat_val+");";
        eval(evaldt);    
        urec(dataNode.nodeName)=d.getVarDate();
      }else{
        urec(dataNode.nodeName)=fixPercentageIssue(dataNode.text);
      }
    }
  }
  urec.SaveChanges();
  Response.Write(noOfRecordsToUpdate);//success
}catch(e){
  debugcrm("updaterecord_res", "Failed: update record");
  Response.Write(0);//failed
}
Response.End();
//array function to check for the fields that are not actual db field but are
//returned from the custom_edits table.
function isInArray(val){
  result=false;
  for (var j = 0; j < nonSysField.length; j++){
    if (nonSysField[j]==val){
      result=true;
    }
  }
  return result;
}

%>