<%@ Page Language="C#" AutoEventWireup="true" CodeFile="summary.aspx.cs" Inherits="summary" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href="/crm61/eware.css" />   
    <link href="eware.css" rel="stylesheet" type="text/css" />
    <link href="/crm61/eware.css" rel="stylesheet" />
    <link href="/crm61/eware.css" rel="stylesheet" />
</head>
<body onload="load()" >
    <form id="EntryForm" runat="server">    
    <div>
<SageCRM:SageCRMConnection id="SageCRMConnection" runat="server" CRMPath="http://localhost/crm61" SID="6912942136907">
</SageCRM:SageCRMConnection>
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMTabGroup ID="SageCRMTabGroup" runat="server" EntityName="CrmProject" TabGroupName="CRMProject" />
                </td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >    
                <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                <asp:Table ID="Table1" runat="server"  Height="50%" Width="100%">
                  <asp:TableRow >
                     <asp:TableCell >
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                          <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock" runat="server" BlockTitle="Crm Project"
                            EntityName="CrmProject" EntityWhere="proj_projectid=6000" EntryBlockName="SummaryCrmProjectscreen" />                            
                       </ContentTemplate>
                    </asp:UpdatePanel>
`                   </asp:TableCell>
                    <asp:TableCell >
                        <SageCRM:SageCRMButtonSave ID='SageCRMButtonSave' runat="server"  OnBeforeRendering="SageCRMButtonSave_BeforeRendering" />
                      <SageCRM:SageCRMButtonChange ID="SageCRMButtonChange" runat="server" OnBeforeRendering="SageCRMButtonChange_BeforeRendering" />
                    </asp:TableCell>
                </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table2" runat="server"  Height="50%" Width="100%">
                  <asp:TableRow >
                     <asp:TableCell >
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                         <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock1" runat="server" BlockTitle="Crm Project"
                         EntityName="CrmProject" EntityWhere="proj_projectid=6000" EntryBlockName="CrmProjectScreen2" />                                                        
                       </ContentTemplate>                    
                     </asp:UpdatePanel>
                     </asp:TableCell>
                     <asp:TableCell >
                         <SageCRM:SageCRMButtonSave ID='SageCRMButtonSave1' runat="server"  OnBeforeRendering="SageCRMButtonSave_BeforeRendering" />
                        <SageCRM:SageCRMButtonChange ID="SageCRMButtonChange1" runat="server" OnBeforeRendering="SageCRMButtonChange_BeforeRendering" />
                     </asp:TableCell>
                   </asp:TableRow>
                </asp:Table>
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    <SageCRM:SageCRMButton ID="SageCRMButton" runat="server" Caption="Javascript Demo"
                        Url="javascript:alert('test message');" />
                </td>
            </tr>
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMTopContent ID="SageCRMTopContent" runat="server" EntityName="CrmProject"
                        EntityWhere="proj_projectid=-1" EntryBlockName="TopContentCrmProjectScreen" />
                </td>
            </tr>
        </table>    
    </div>
    <input type="hidden" id="ajaxmode" value="" /> 
    </form>
<script type="text/javascript" >
<!--
function ajax_change(){
  getButtons();
  __doPostBack("UpdatePanel1", "EntryForm");
  toggleAllChangeButtons("SageCRMButtonChange"); 
  toogleMode();
  if (Mode==1)
  {
    displayButton("SageCRMButtonSave");
    hideButton("SageCRMButtonChange");
  }else
  {
    displayButton("SageCRMButtonChange1");    
  }
}
function ajax_change1(){
  getButtons();
  __doPostBack("UpdatePanel2", "EntryForm");
  toggleAllChangeButtons("SageCRMButtonChange1");
  toogleMode();
  if (Mode==1)
  {
    displayButton("SageCRMButtonSave1");
    hideButton("SageCRMButtonChange1");
  }else   
  {
    displayButton("SageCRMButtonChange");  
  }
}
function toogleMode()
{
  if (Mode==0)
  {
    Mode=1;
  }else
  { 
    Mode=0;
  }
}
var Mode=0;
//array to hold all our buttons
var buttonArray=new Array();
function toggleAllChangeButtons(ignoreButtonName)
{
      ignoreButton=document.getElementById(ignoreButtonName);
      for (j=0; j<buttonArray.length;j++)
      {
        aButton=buttonArray[j];
        if (ignoreButton.id!=aButton.id)
        {
          hideButton(aButton.id);
        }else
        {
          displayButton(aButton.id);
        }
      }
}
function hideButton(btnName)
{
  var aButton=document.getElementById(btnName);
  aButton.style.display='none';
  aButton.style.visibility='hidden';            
}
function displayButton(btnName)
{
  var aButton=document.getElementById(btnName);
  aButton.style.display='';
  aButton.style.visibility='';            
}
var buttonCount=0;
function addButtonToArray(buttonObj)
{
  buttonArray[buttonCount]=buttonObj;
  buttonCount++;
}
function getButtons(buttonArray)
{
  //reset the array count;
  buttonCount=0
  //get buttons and add to array
  cButton = document.getElementById("SageCRMButtonChange");
  addButtonToArray(cButton);
  cButton = document.getElementById("SageCRMButtonChange1");
  addButtonToArray(cButton);
  cButton = document.getElementById("SageCRMButtonSave");
  addButtonToArray(cButton);
  cButton = document.getElementById("SageCRMButtonSave1");
  addButtonToArray(cButton);
}

  hideButton("SageCRMButtonSave");   
  hideButton("SageCRMButtonSave1");   

// -->
</script>
<script type="text/javascript" language="javascript" >
function load()
{
    try
    {
    //register out page loaded event handle
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(updatePanel_LoadedHandler); 
    }
        catch (ex) {} 
}

function updatePanel_LoadedHandler(sender, args)
{
    if (typeof(sender) === "undefined") 
        return;
    var updatedPanels = args.get_panelsUpdated();
    for (var i = 0; i < updatedPanels.length; i++) 
    {
        _reEval(updatedPanels[i].innerHTML); 
    }
}

var js_ScriptFragment = '(?:<SCRIPT.*?>)((\n|\r|.)*?)(?:<\/SCRIPT>)';
var js_ScriptSrcFragment = '<SCRIPT.+(src[ ]*=[ ]*\'(.*?)\'|src[ ]*=[ ]*"(.*?)").+';
var js_FunctionNameFragment ='[\\s;\n]*function[\\s\n]*([a-zA-Z_\\$]+[0-9a-zA-A_\\$]*)[\\s\n]*\\(';

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
</script> 

   
</body>
</html>
