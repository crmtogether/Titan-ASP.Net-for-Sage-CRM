//******************************************************************************
//******************************************************************************
/*
*  TITAN Suite for Sage CRM
*  This suite should not be used without a valid license from 'CRM Together'
*  Web: http://www.crmtogether.com
*  Email: sagecrm@crmtogether.com
*/
//******************************************************************************

var _modeView=0;
var _modeEdit=1;
var CRMMode=_modeView;

//******************************************************************************
/*
*  Cross browser support
*/
//******************************************************************************
function XBrowserAddHandler(target, eventName, handlerName) {
    if (target.addEventListener)
        target.addEventListener(eventName, handlerName, false);
    else if (target.attachEvent) 
        target.attachEvent("on" + eventName, handlerName);
    else
        target["on" + eventName] = handlerName;
}

var js_ScriptFragment = '(?:<SCRIPT.*?>)((\n|\r|.)*?)(?:<\/SCRIPT>)';
var js_ScriptSrcFragment = '<SCRIPT.+(src[ ]*=[ ]*\'(.*?)\'|src[ ]*=[ ]*"(.*?)").+';
var js_FunctionNameFragment ='[\\s;\n]*function[\\s\n]*([a-zA-Z_\\$]+[0-9a-zA-A_\\$]*)[\\s\n]*\\(';

String.prototype.trim = function() {
	return this.replace(/^\s+|\s+$/g,"");
}
String.prototype.ltrim = function() {
	return this.replace(/^\s+/,"");
}
String.prototype.rtrim = function() {
	return this.replace(/\s+$/,"");
}
   
function extractScripts(str)
{
  var matchAll = new RegExp(js_ScriptFragment, 'img');
  var matchOne = new RegExp(js_ScriptFragment, 'im');
  var matchSrc = new RegExp(js_ScriptSrcFragment, 'im');
   
  var arr = str.match(matchAll) || [];
  var res = [];
  for (var i = 0; i < arr.length; i++)
  {
    var srcMt = arr[i].match(matchSrc);
	if (srcMt)
   	{
   		if (srcMt.length > 3) res.push(['src', srcMt[3]]);
   		else 
   		res.push(['src', srcMt[2]]);
   	}
   	var mtCode = arr[i].match(matchOne) || ['', ''];
   	_addScript(mtCode[1]);
  } 
  return res;
}
function _addScript(scriptCode)
{
  var script = document.createElement('script');
  script.type = 'text/javascript';
  script.text = scriptCode;
  document.body.appendChild(script);
}
function _reEval(value){
  src=extractScripts(value);
}
function _showElement(objpointer)
{
  objpointer.style.visibility='';
  objpointer.style.display='';
}
function _hideElementById(name)
{
  var _obj=document.getElementById(name);
  _hideElement(_obj);
}

function _hideElement(objpointer)
{
  objpointer.style.visibility='hidden';
  objpointer.style.display='none';
}
function _cloakElement(objpointer)
{
  objpointer.style.visibility='hidden';
}
function buildURL(PagePath)
{               
  var strFileName = PagePath;
  cs_debug("buildURL: strFileName:"+strFileName);
  var strPath = document.URL;
  cs_debug("buildURL: strPath:"+strPath);
  if (strPath.indexOf("eware.dll")!=-1)
  {
    var arrayApp = strPath.split("eware.dll");  
    PagePath="CustomPages/"+PagePath;
    cs_debug("eware.dll="+PagePath);  
  }else
  if (strPath.indexOf("SageCRMWS")!=-1){  
    var arrayApp = strPath.split("SageCRMWS");
    cs_debug("SageCRMWS="+strPath);  
  }else{
    var arrayApp = strPath.split("CustomPages");
    arrayApp[0]+="CustomPages/";
    cs_debug("CustomPages="+strPath);  
  }
 
  var arrayContext = strPath.split("?");
  var strAppPath = arrayApp[0];
  cs_debug("buildURL: strAppPath:"+strAppPath);
  var strContextInfo = arrayContext[1];
  strAddr= strAppPath + PagePath+"?"+strContextInfo+"&";
  cs_debug("buildURL: strAddr:"+strAddr);
  return strAddr; 
}

function MakeAjaxGetRequest(RequestPath)
{
  if (window.XMLHttpRequest) {
      XmlHttp = new XMLHttpRequest();
  } else {
      // IE6 and before
      XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
  }
  
  XmlHttp.open('GET',RequestPath,false);
  XmlHttp.setRequestHeader('Content-Type', 'text/xml');
  XmlHttp.send(null);
  var strHtml = XmlHttp.responseText;
  XmlHttp=null; // always clear the XmlHttp object when you are done to avoid memory leaks
  //alert(strHtml);
  return strHtml;
}
function MakeAjaxPostRequestState(RequestPath, RequestForm, onreadystatechangeFunction)
{
  if (window.XMLHttpRequest) {
      XmlHttp = new XMLHttpRequest();
  } else {
      // IE6 and before
      XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
  }
  
  XmlHttp.open('POST',RequestPath,false);
  XmlHttp.onreadystatechange=onreadystatechangeFunction;
  //Send the proper header information along with the request
  XmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
  XmlHttp.setRequestHeader("Content-length", RequestForm.length);
  XmlHttp.setRequestHeader("Connection", "close");
  XmlHttp.send(RequestForm);
  var strHtml = XmlHttp.responseText;
  XmlHttp=null; // always clear the XmlHttp object when you are done to avoid memory leaks
  //alert(strHtml);
  return strHtml;
}
function MakeAjaxPostRequest(RequestPath, RequestForm)
{
  if (window.XMLHttpRequest) {
      XmlHttp = new XMLHttpRequest();
  } else {
      // IE6 and before
      XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
  }
  
  XmlHttp.open('POST',RequestPath,false);                     
  //Send the proper header information along with the request
  XmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
  XmlHttp.setRequestHeader("Content-length", RequestForm.length);
  XmlHttp.setRequestHeader("Connection", "close");
  XmlHttp.send(RequestForm);
  var strHtml = XmlHttp.responseText;
  XmlHttp=null; // always clear the XmlHttp object when you are done to avoid memory leaks
  //alert(strHtml);
  return strHtml;
}

function getQueryVariableFromPath(Path, variable) {
  var query = Path.substring(1);
  var vars = query.split("&");
  for (var i=0;i<vars.length;i++) {
    var pair = vars[i].split("=");
    if (pair[0] == variable) {
      return pair[1];
    }
  }
  return "";
  //alert('Query Variable ' + variable + ' not found');
} 
//Returns the value from the current query string
function getQueryVariable(variable) {
  return getQueryVariableFromPath(window.location.search,variable);
} 
//gets a handle to the client button table
function getButtonHTMLTable()
{
  var table_col=document.getElementsByTagName("TABLE");
  for(i=0;i<table_col.length;i++)
  {
    var table_obj=table_col[i];
    if (table_obj.className=="Button")
    {
      return table_obj;       
    }
  }
  return null;
}
function _getParentTable(obj)
{
  var res=null;
  var bFound=false;
  while ((!bFound) && (obj!=null))
  {
    if (obj.parentNode.tagName=="TABLE")
    {
      res=obj.parentNode;
      bFound=true;
      break;
    }
    obj=obj.parentNode;     
  }
  return res;
}
function _CommaFormatted(amount)
{
	var delimiter = ","; // replace comma if desired
	var a = amount.split('.',2)
	var d = a[1];
	var i = parseInt(a[0]);
	if(isNaN(i)) { return ''; }
	var minus = '';
	if(i < 0) { minus = '-'; }
	i = Math.abs(i);
	var n = new String(i);
	var a = [];
	while(n.length > 3)
	{
		var nn = n.substr(n.length-3);
		a.unshift(nn);
		n = n.substr(0,n.length-3);
	}
	if(n.length > 0) { a.unshift(n); }
	n = a.join(delimiter);
	if(d==null || d.length < 1) { amount = n; }
	else { amount = n + '.' + d; }
	amount = minus + amount;
	return amount;
}

function _CurrencyFormatted(amount)
{
	var i = parseFloat(amount);
	if(isNaN(i)) { i = 0.00; }
	var minus = '';
	if(i < 0) { minus = '-'; }
	i = Math.abs(i);
	i = parseInt((i + .005) * 100);
	i = i / 100;
	s = new String(i);
	if(s.indexOf('.') < 0) { s += '.00'; }
	if(s.indexOf('.') == (s.length - 2)) { s += '0'; }
	s = minus + s;
	return s;
}

function GetTrans(Fam, Code)
{
  var ContextURL=buildURL('SageCRM/component/getTrans.asp');
  ContextURL+="Family="+Fam+"&Caption="+Code;
  cs_debug(ContextURL);
  return MakeAjaxGetRequest(ContextURL);
}
function GetContext(TableName, WhereClause, FieldName)
{
  var ContextURL=buildURL('SageCRM/component/getContext.asp');
  ContextURL+="TableName="+TableName+"&WhereClause="+WhereClause+"&FieldName="+FieldName;
  cs_debug(ContextURL);
  return MakeAjaxGetRequest(ContextURL);
}
function GetTabGroup(TabGroupName)
{
  var ContextURL=buildURL('SageCRM/component/tabgroup.asp');
  ContextURL+="crmtabgroup="+TabGroupName;
  cs_debug(ContextURL);         
  res = MakeAjaxGetRequest(ContextURL);
  _reEval(res);  
  return res;
}
function GetServerPage(EntityName, EntryBlock, EntityWhere, BlockTitle)
{
  var ContextURL=buildURL('SageCRM/component/page.asp');
  ContextURL+="EntityName="+EntityName+"&EntryBlock="+EntryBlock+"&EntityWhere="+EntityWhere+"&BlockTitle="+BlockTitle;
  cs_debug(ContextURL);         
  res = MakeAjaxGetRequest(ContextURL);
  _reEval(res);  
  return res;
}
var objFrame=null;
function _toggleDiv(id,flagit,s) 
{
  if (objFrame==null)
  {
    var aElement=document.createElement("iframe");
    document.appendChild(aElement);
  }
  if (flagit=="1"){
    if (document.layers) 
      document.layers[''+id+''].visibility = "show"
      else if (document.all) 
        document.all[''+id+''].style.visibility = "visible"
        else if (document.getElementById) 
          document.getElementById(''+id+'').style.visibility = "visible"
    objFrame=document.getElementById("myframe"); 
    objFrame.src = s;
  }
  else
  if (flagit=="0"){
    if (document.layers) 
      document.layers[''+id+''].visibility = "hide"
      else if (document.all) 
        document.all[''+id+''].style.visibility = "hidden"
        else if (document.getElementById) 
          document.getElementById(''+id+'').style.visibility = "hidden"
  }
}
function _getContextFromRecentList()
{
  var RecentValue = new String(getQueryVariable("RecentValue"));
  var RecentValue_arr=RecentValue.split("X");
  return RecentValue_arr[2];
}
function GetContextInfo(TableName, FieldName)
{
  var ContextURL=buildURL('SageCRM/component/getContextInfo.asp');
  ContextURL+="Context="+TableName+"&FieldName="+FieldName;
  cs_debug(ContextURL);
  var res = MakeAjaxGetRequest(ContextURL);
  res=res.trim();
  return res;
}
function clientQuery(SQL, FieldName)
{
  var ContextURL=buildURL('SageCRM/component/clientQuery.asp');
  ContextURL+="FieldName="+FieldName+"&SQL="+escape(SQL);
  cs_debug(ContextURL);
  return MakeAjaxGetRequest(ContextURL);
}
function SQLQuery(SQL)
{
  var ContextURL=buildURL('SageCRM/component/SQLQuery.asp');
  ContextURL+="SQL="+escape(SQL);
  //alert(ContextURL);
  var res = null;
  try{
    res = MakeAjaxGetRequest(ContextURL);
  }catch(ex)
  {
    alert("ContextURL failed: "+ContextURL);
  }
  //alert(res);
  var recobj="";
  try{
    recobj= eval("("+ res+ ")");//make into an object
  }catch(ex2)
  {
    //document.write(res);
    alert("SQLQuery failed:"+SQL+"-with Result:"+$);
    //alert(ex2);
  }
  return recobj;
}
function UpdateSQLQuery(SQL)
{
  var ContextURL=buildURL('SageCRM/component/UpdateSQLQuery.asp');
  ContextURL+="SQL="+escape(SQL);
  //alert(ContextURL);
  //document.write(ContextURL);
  var res = null;
  try{
    res = MakeAjaxGetRequest(ContextURL);
  }catch(ex)
  {
    alert("ContextURL failed: "+ContextURL);
  }
  ///alert(res);
}
function CreateAcceleratorLink(TableName, TableId, LinkTableName, LinkTableId)
{
  var ContextURL=buildURL('SageCRM/component/CreateAcceleratorLink.asp');
  ContextURL+="TableName="+TableName+"&TableId="+TableId
        +"&LinkTableName="+LinkTableName+"&LinkTableId="+LinkTableId;
  //alert(ContextURL);
  //document.write(ContextURL);
  var res = null;
  try{
    res = MakeAjaxGetRequest(ContextURL);
  }catch(ex)
  {
    alert("ContextURL failed: "+ContextURL);
  }
}
  
function OfficeDisplayDiv(divName, className, msg)
{
  var CoreMsg="<TABLE WIDTH=100% CLASS="+className+"><TR><TD>"+msg+"</TD></TR></TABLE>";
  var Obj_SageCRMEntryBlock=document.getElementById("SageCRMEntryBlock");
  Obj_SageCRMEntryBlock.insertAdjacentHTML("beforeBegin", CoreMsg);
}
function OfficeDisplayErrorDiv(divName, msg)
{
  OfficeDisplayDiv(divName, "ErrorContent", msg)
}
function OfficeDisplayInfoDiv(divName, msg)
{
  OfficeDisplayDiv(divName, "InfoContent", msg)
}
function OfficeDisplayError(msg)
{
  OfficeDisplayErrorDiv("SageCRMEntryBlock", msg);
}
function OfficeDisplayInfo(msg)
{
  OfficeDisplayInfoDiv("SageCRMEntryBlock", msg);
}
function getEntryGroup(EntityName, EntryBlock, EntityWhere, BlockTitle)
{
  var ContextURL=buildURL('SageCRM/component/entrygroup.asp');
  cs_debug("getEntryGroup 1:"+ContextURL);
  ContextURL+="EntityName="+EntityName+"&EntryBlock="+EntryBlock+"&EntityWhere="+EntityWhere+"&BlockTitle="+BlockTitle;
  cs_debug("getEntryGroup 2:"+ContextURL);
  res = MakeAjaxPostRequest(ContextURL,EntryBlock+"_em=0");
  _reEval(res);
  return res;
}
function getEntryGroupEdit(EntityName, EntryBlock, EntityWhere, BlockTitle)
{
  var ContextURL=buildURL('SageCRM/component/entrygroup.asp');
  cs_debug("getEntryGroup 1:"+ContextURL);
  ContextURL+="EntityName="+EntityName+"&EntryBlock="+EntryBlock+"&EntityWhere="+EntityWhere+"&BlockTitle="+BlockTitle;
  cs_debug("getEntryGroup 2:"+ContextURL);
  res = MakeAjaxPostRequest(ContextURL,EntryBlock+"_em=1");
  _reEval(res);
  return res;
}
function getFilterBlock(EntryBlock)
{
  var ContextURL=buildURL('SageCRM/component/filter.asp');
  cs_debug("getFilterBlock 1:"+ContextURL);
  ContextURL+="EntryBlock="+EntryBlock;
  cs_debug("getFilterBlock 2:"+ContextURL);
  res = MakeAjaxPostRequest(ContextURL,EntryBlock+"_em=2");
  _reEval(res);
  return res;
}
function getWFButton(searchTag)
{
  var anc_col=document.getElementsByTagName("A");

  for(i=0;i<anc_col.length;i++)
  {
    var a_obj=anc_col[i];
    if (a_obj.className=="WFBUTTON")
    {
       if (searchTag==a_obj.innerText)
       {
         return a_obj;       
       }
    }
  }
  return null;
}
function getPageButton(searchTag)
{
  var anc_col=document.getElementsByTagName("A");

  for(i=0;i<anc_col.length;i++)
  {
    var a_obj=anc_col[i];
    if (a_obj.className=="ButtonItem")
    {
       if (searchTag==a_obj.innerText)
       {
         return a_obj;       
       }
    }
  }
  return null;
}
function _validated(html)
{               
  if (html.indexOf("Required.gif")>0)
    return false;
  return true;
}
function getSystemEntryBox(searchTag)
{
  var tableBody=null;
  var escapeLoops=false;
  //alert('dddd');  
  var g=document.getElementsByTagName("TABLE");
  //alert(g.length);
  for(i=0;i<g.length;i++)
  {
    var oTable=g[i];
    //loop through the rows
    //alert("Table="+i+"===="+oTable.innerHTML);
    for(var liCount=0;liCount<oTable.rows.length;liCount++)
    {
      var rowObj=oTable.rows[liCount];
      for(var colCount=0;colCount<rowObj.cells.length;colCount++)
      {
        var cellObj=rowObj.cells[colCount];      
        var stext=new String(cellObj.innerText);
        if (stext==searchTag)
        {
          //_cloakElement(oTable);
          //hide next table then break from loops
          var oTable=g[i+1];
          //alert();
          _cloakElement(oTable);
          if (oTable.childNodes[0].tagName=="TBODY")
            tableBody=oTable.childNodes[0];
        }
      }
      if (escapeLoops)
        break;      
    }
    if (escapeLoops)
      break;      
  }
  return tableBody;  
}
function getEntryGroupNew(EntityName, EntryBlock, EntityWhere, BlockTitle)
{
  var ContextURL=buildURL('SageCRM/component/entrygroupcreate.asp');
  cs_debug("getEntryGroupNew 1:"+ContextURL);
  ContextURL+="EntityName="+EntityName+"&EntryBlock="+EntryBlock+"&EntityWhere="+EntityWhere+"&BlockTitle="+BlockTitle;
  cs_debug("getEntryGroupNew 2:"+ContextURL);  
  res = MakeAjaxGetRequest(ContextURL);
  _reEval(res);
  return res;
}
function getEntryGroupNewPost(EntityName, EntryBlock, EntityWhere, BlockTitle, PostData,EvalCode)
{
  var ContextURL=buildURL('SageCRM/component/entrygroupcreate.asp');
  cs_debug("getEntryGroupNewPost 1:"+ContextURL);
  ContextURL+="EntityName="+EntityName+"&EntryBlock="+EntryBlock+"&EntityWhere="+EntityWhere+"&BlockTitle="+BlockTitle;
  cs_debug("getEntryGroupNewPost 2:"+ContextURL);  
  res = MakeAjaxPostRequest(ContextURL,PostData+"&EvalCode="+EvalCode);
  _reEval(res);
  return res;
}
function getEntryGroupEditPost(EntityName, EntryBlock, EntityWhere, BlockTitle, PostData,EvalCode)
{
  var ContextURL=buildURL('SageCRM/component/entrygroup.asp');
  cs_debug("getEntryGroupNewPost 1:"+ContextURL);
  ContextURL+="EntityName="+EntityName+"&EntryBlock="+EntryBlock+"&EntityWhere="+EntityWhere+"&BlockTitle="+BlockTitle;
  cs_debug("getEntryGroupNewPost 2:"+ContextURL);  
  res = MakeAjaxPostRequest(ContextURL,PostData+"&EvalCode="+EvalCode);
  _reEval(res);
  return res;
}

function submitEntryGroup(EntityName, EntryBlock, EntityWhere, BlockTitle,EvalCode)
{
  var poststring=new String("");
  for(i=0; i<document.EntryForm.elements.length; i++)
  {
    //alert("The field name is: " + document.EntryForm.elements[i].name + " and it’s value is: " + document.EntryForm.elements[i].value + ".<br />");
    var tstr=new String(document.EntryForm.elements[i].name);  
    if (tstr.indexOf("HIDDEN")!=0){
      if (poststring!="")
        poststring+="&";
      poststring+=document.EntryForm.elements[i].name+"="+document.EntryForm.elements[i].value;    
    }  
  }
  var res=getEntryGroupNewPost(EntityName, EntryBlock, EntityWhere, BlockTitle,poststring,EvalCode);
  _reEval(res);
  return res;
}
function submitEditEntryGroup(EntityName, EntryBlock, EntityWhere, BlockTitle,EvalCode)
{
  var poststring=new String("");
  for(i=0; i<document.EntryForm.elements.length; i++)
  {
    //alert("The field name is: " + document.EntryForm.elements[i].name + " and it’s value is: " + document.EntryForm.elements[i].value + ".<br />");
    var tstr=new String(document.EntryForm.elements[i].name);  
    if (tstr.indexOf("HIDDEN")!=0){
      if (poststring!="")
        poststring+="&";
      poststring+=document.EntryForm.elements[i].name+"="+document.EntryForm.elements[i].value;    
    }  
  }
  var res=getEntryGroupEditPost(EntityName, EntryBlock, EntityWhere, BlockTitle,poststring,EvalCode);
  _reEval(res);
  return res;
}
function getButton(Caption, Image, url)
{
  var ContextURL=buildURL('SageCRM/component/button.asp');
  cs_debug(ContextURL);
  ContextURL+="Caption="+Caption+"&ImageName="+Image+"&Url="+ url;
  cs_debug(ContextURL);
  return MakeAjaxGetRequest(ContextURL);
}
function getList(ListBlock,EntityName,EntityWhere,BlockTitle)
{
  var ContextURL=buildURL('SageCRM/component/list.asp');
  cs_debug(ContextURL);
  ContextURL+="EntityName="+EntityName+"&ListBlock="+ListBlock+"&EntityWhere="+EntityWhere+"&BlockTitle="+BlockTitle+"&SelectSQL=";
  cs_debug(ContextURL);
  var res = MakeAjaxPostRequest(ContextURL,"");
  _reEval(res);
  return res;
}
function attachTagEvent(tagName, event, functionPtr)
{
  var tagList=document.getElementsByTagName(tagName);
  for(i=0;i<tagList.length;i++)
  {
    var obj=tagList[i];
    obj.attachEvent(event, functionPtr);
  }
}
function URL(path)
{
  var ContextURL=buildURL('SageCRM/component/url.asp');
  cs_debug(ContextURL);
  ContextURL+="url="+path;
  cs_debug(ContextURL);
  return MakeAjaxPostRequest(ContextURL,"");
}
function _getCRMFrame(fname)
{
  var frm = window.parent.frames;
  for (i=0; i < frm.length; i++) 
  {
    var _fn=new String(frm(i).name);
    if (fname.toLowerCase()==_fn.toLowerCase())
    {
      return frm(i);
    }
  }
  return null;
}
function getEWARETOP()
{
  return _getCRMFrame("EWARE_TOP");
}
function _getSELECTUser()
{
  var Top=getEWARETOP();
  var x=Top.document.getElementById("SELECTUser");
  return x;
}
function _getSELECTChannel()
{
  var Top=getCRMFrame("EWARE_TOP");
  var x=Top.document.getElementById("SELECTChannel");
}

///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
//set this true to show alert messages
var debugMode=false;
//this is the load function that is called. 
//enter your customisations here
function CRMTogetherOnload()
{

  //when in a NEW screen you can access the company id via
  //  document.EntryForm.case_primarycompanyid.value
  //and the personid via
  //  document.EntryForm.case_primarypersonid.value
  
  //When you do need to display a message you can use the methods below
  //  OfficeDisplayInfo("displays info msg in office screen");
  //  OfficeDisplayError("displays error msg in office screen");
  
  //This is how to get the context information 
  //  var personid=GetContext("person","pers_personid="+getQueryVariable("id"),"pers_personid");
  //  cs_debug(personid);
  //  var companyid=GetContext("comany","comp_companyid="+personid,"comp_companyid");
  //  cs_debug(companyid);

  //This is how you run a query on the client. It only returns one field l value however
  //var companyName=clientQuery("select top 1 comp_name from company", "comp_name");

  //Use this to display a preview of a screen
  //var ht=getEntryGroup("company","companyboxlong", "comp_companyid=43","test");
  //cs_debug(ht);
  
  //to attach an event to all the same tags use the following
  //attachTagEvent(TagName,eventName,yourFunctionName);
  //attachTagEvent("A","onmouseover",testFunc);

  //to build a url using the server API
  //var x=URL("RMA/RMAChannel.asp");
  //MakeAjaxGetRequest(x);
  //alert(x);

/*
  //var ContextURL=buildURL("SageCRM/component/ChannelContext.asp");
  var ContextURL=buildURL("RMA/RMAChannel.asp");
  cs_debug("ContextURL="+ContextURL);  
  var x=MakeAjaxGetRequest(ContextURL);
  alert(x);

  var ContextURL2=buildURL("Namaste/Team.aspx");
  //var ContextURL=buildURL("RMA/RMAChannel.asp");
  cs_debug("ContextURL2="+ContextURL2);  
  //document.write(ContextURL2);
  var x2=MakeAjaxGetRequest(ContextURL2);
  alert(x2);
  */

}

function cs_debug(msg)
{
  if (debugMode)
    alert(msg);
}
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
if(document.all)
{
  // Microsoft IE (W3 compliant)
  window.attachEvent("onload", CRMTogetherOnload);
}
else
{
  // Firefox
  window.addEventListener("onload", CRMTogetherOnload, true);
}
