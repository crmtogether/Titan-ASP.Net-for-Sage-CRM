//******************************************************************************
//******************************************************************************
/*
*  TITAN Suite for Sage CRM...now everything is possible
*  Web: http://www.crmtogether.com
*  Email: sagecrm@crmtogether.com
*  Requires crmtogether.js script file to work
*  This script will preview links that are hovered over with the shift key pressed.
*/
//******************************************************************************
function _CustomOnload()
{
  attachTagEvent("A","onmouseout",_linkPreviewUndo);
  attachTagEvent("A","onmouseover",_linkPreview);
}
function _linkPreviewUndo(e)
{
  if (!e) var e = window.event;
  	var relTarg = e.relatedTarget || e.toElement;
  var p_obj=null;
  if ( (relTarg!=null) && (relTarg.tagName=="A"))
  {
    p_obj=relTarg;
  }else{
    p_obj=relTarg.childNodes[0];
  }
  if ( (p_obj!=null) && (p_obj.tagName=="A") )
  {
    if (p_obj.href.indexOf("javascript")==-1)
      _toggleDivObj(0,p_obj.href);
  }
}

function _linkPreview(e)
{
  if(!event.ctrlKey) 
    return;

  if (!e) var e = window.event;
  	var relTarg = e.relatedTarget || e.toElement;
  var p_obj=null;
  if (relTarg.tagName=="A")
  {
    p_obj=relTarg;
  }else{
    p_obj=relTarg.parentNode;
  }
  if ( (p_obj!=null) && (p_obj.tagName=="A") )
  {
    if (p_obj.href.indexOf("javascript")==-1)
      _toggleDivObj(1,p_obj.href);
  }  
}
if(document.all)
{
  // Microsoft IE (W3 compliant)
  window.attachEvent("onload", _CustomOnload);
}
else
{
  // Firefox
  window.addEventListener("onload", _CustomOnload, true);
}