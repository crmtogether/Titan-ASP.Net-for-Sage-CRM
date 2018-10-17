<!-- #include file ="SageCRM.js" -->
<!-- #include file ="nonSysFields.asp" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************

Response.clear();
  var testMode = false;
  var TableName=new String(Request.QueryString('TableName'));
  var WhereClause=new String(Request.QueryString('WhereClause'));
  var NoTLS=new String(Request.QueryString('NoTLS'));
  var bNoTLS=false;
  if (NoTLS=="Y")
  {
    bNoTLS=true;
  }
  debugcrm("updaterecord_xmldata", "data="+Request.Form);
  xmlDoc = Server.CreateObject("MSXML.DOMDocument");
  //actual line

//patch/workaround for % chars and plus + chars
var find = " _pct_ ";
var regex = new RegExp(find, "g");
var frm=new String(Request.Form);
frm=frm.replace(regex, "%");

var find2 = " _pls_ ";
var regex2 = new RegExp(find2, "g");
frm=frm.replace(regex2, "+");

var find3 = " _apo_ ";
var regex3 = new RegExp(find3, "g");
frm=frm.replace(regex3, "'");
//patch/workaround
  
  //actual line
  if (!testMode){
    xmlDoc.loadXML(frm);

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
  
  if (bNoTLS)
  {
    urec.SaveChangesNoTLS();
  }else{
    urec.SaveChanges();
  }
  
  Response.Write(noOfRecordsToUpdate);//success

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