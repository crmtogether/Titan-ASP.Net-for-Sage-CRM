//******************************************************************************
//******************************************************************************
/*
*  TITAN Suite for Sage CRM
*  This suite cannot be used without a valid license from 'CRM Together'.
*  Web: http://www.crmtogether.com
*  Email: sagecrm@crmtogether.com
*  Requires crmtogether.js script file to work
*/
//******************************************************************************
var companyid=-1;

function submitAjaxForm()
{
  if (CRMMode==_modeView)
  {
     getEditBox();
     CRMMode=_modeEdit;
  }else{
    res=submitEditEntryGroup("Company", "companyboxlong", "comp_companyid="+companyid, "","");
    setContent(res);
    if (_validated(res))
    {    
      _setButton(UpdateButton);
      CRMMode=_modeView;
      _refreshTopContent();
    }
  }
}

function _refreshTopContent()
{
  var c=GetServerPage("Company", "companyboxlong", "comp_companyid="+companyid, "");
}

function _setButton(html)
{
  Button_table.rows[0].cells[0].innerHTML=html;
  Button_table.rows[0].cells[2].innerHTML="";    
}
function _validated(html)
{               
  if (html.indexOf("Required.gif")>0)
    return false;
  return true;
}

var contentHolder=null;

function setContent(html)
{
  if (contentHolder==null)
  {
    var tableBody=getSystemEntryBox('Company ');
    var w_obj=tableBody.parentNode;
    var w_obj1=w_obj.parentNode;
    contentHolder=w_obj1;  
  }
  contentHolder.innerHTML="";
  contentHolder.innerHTML=html;
}
var changeButton=null
var UpdateButton=null;
var Button_table=null;

function updateChangeBtn()
{
  changeButton=getPageButton("Change");
  UpdateButton=getButton("Change", "Edit.gif", "javascript:submitAjaxForm();");
  Button_table=_getParentTable(changeButton);        
  _setButton(UpdateButton);
}
function getEditBox()
{  
  //now we get the entry group from the back end
  var content=getEntryGroupEdit("Company", "companyboxlong", "comp_companyid="+companyid, "");
  setContent(content);
  
  SaveButton=getButton("Save", "Save.gif", "javascript:submitAjaxForm();");     
  Button_table.rows[0].cells[0].innerHTML=SaveButton;
  Button_table.rows[0].cells[2].innerHTML="";
}
function setContext()
{
  companyid=GetContextInfo("company","comp_companyid");
  if (companyid=="")  //we may have got here from the recent list
  {
    companyid=_getContextFromRecentList();
  }
}
function CompanyBoxLongOnload()
{
  setContext();
  updateChangeBtn();
}

///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////// 
if(document.all)
{
  // Microsoft IE (W3 compliant)
  window.attachEvent("onload", CompanyBoxLongOnload);
}
else
{
  // Firefox
  window.addEventListener("onload", CompanyBoxLongOnload, true);
}
