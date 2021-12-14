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
  var _code="";
  var testMode = false;
  var TableName=new String(Request.QueryString('TableName'));
  var WhereClause=new String(Request.QueryString('WhereClause'));
  var NoTLS=new String(Request.QueryString('NoTLS'));
  var bNoTLS=false;
  if (NoTLS=="Y")
  {
    bNoTLS=true;
  }
  
  var Workflow=new String(Request.QueryString('w'));
  var WorkflowState=new String(Request.QueryString('s'));
  
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
  urec=eWare.CreateRecord(TableName);
  
  if ((Workflow!="")&&(Workflow+""!="undefined"))
  {
    urec.setWorkFlowInfo(Workflow, WorkflowState);
  }
  
  var bhas_comm_channelid=false;
  for (var i = 0 ; i < xmlDocRoot.childNodes.length; i++) {
    dataNode=xmlDocRoot.childNodes[i];
    var testStr=new String(dataNode.nodeName);
    var testStr=testStr.toLowerCase();
    if (testStr=="comm_secterr")
    {
     // dataNode.text=CRM.GetContextInfo("user","User_PrimaryTerritory");
    }else
	if (testStr=="comm_channelid")
    {
      bhas_comm_channelid=true;
    }
    if (!isInArray(testStr)){
      if (testMode)
        Response.Write(dataNode.nodeName+"="+dataNode.text+"<br />");
      var ftype=getFieldType(dataNode.nodeName);
      _code+=".ftype="+ftype+".."+dataNode.nodeName+"="+fixPercentageIssue(dataNode.text);
      if (((ftype=="41")||(ftype=="42")) && (dataNode.text.indexOf(",")>-1))
      {
        var dat_val=dataNode.text;
        var evaldt="var d=new Date("+dat_val+");";
        eval(evaldt);    
        urec(dataNode.nodeName)=d.getVarDate();
      }else{
        if ((dataNode.nodeName=="comm_datetime")&&(dataNode.text.indexOf("Z")==-1))
		{
			//clever..
			urec(dataNode.nodeName)=(new Date()).getVarDate();
		}else{
			urec(dataNode.nodeName)=fixPercentageIssue(dataNode.text);
		}
      }
    }
  }
  
  if ((TableName.toLowerCase()=="communication")&&(!bhas_comm_channelid)) //patch to ensure that the team is set
  {
    urec("comm_channelid")=CRM.GetContextInfo("user","user_primarychannelid");
  }
  _code+="-save called-";
  if (bNoTLS)
  {
    urec.SaveChangesNoTLS();
  }else{
    urec.SaveChanges();
  }
  Response.Write(urec.RecordId);//success
}catch(e){
  Response.Write('Error:Saving Record: '+TableName+ '  -  Desc: ' + e.description + '  -  ErrorNo: '+e.number + '  -XML: '+Request.Form+" code trace:"+_code);//failed
}
Response.End;
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