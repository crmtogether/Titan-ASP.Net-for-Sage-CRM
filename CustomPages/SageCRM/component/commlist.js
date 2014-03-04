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
var PreviewIFrame=document.createElement("IFRAME");
PreviewIFrame.setAttribute('id', 'PreviewIFrame');
PreviewIFrame.setAttribute('width', "1150px");
PreviewIFrame.setAttribute('height', "500px");
PreviewIFrame.style.position="absolute";
PreviewIFrame.style.left="100px";
PreviewIFrame.style.top="25px";

function CustomOnload()
{
  document.body.appendChild(PreviewIFrame);
  _hideElement(PreviewIFrame);
  //attachTagEvent("A","onmouseout",_linkPreviewUndo);
  attachTagEvent("A","onmouseover",_linkPreview);
  attachTagEvent("BODY","onkeydown",_linkPreviewUndo);  
}
function _linkPreviewUndo(e)
{
  if (e.keyCode == 27)
  {
    _toggleDivObj(0,"");
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
    //alert(relTarg.href);  
  }else{
    p_obj=relTarg.parentNode;
  }
  if ( (p_obj!=null) && (p_obj.tagName=="A") )
  {
    if (p_obj.href.indexOf("javascript")==-1)
      _toggleDivObj(1,p_obj.href);
  }  
}

function _toggleDivObj(mode, linkLocation)
{
if (mode==0)
{
  _hideElement(PreviewIFrame);
}else{
  _showElement(PreviewIFrame);  
  PreviewIFrame.setAttribute('src', linkLocation);
}
}
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////// 
if(document.all)
{
  // Microsoft IE (W3 compliant)
  window.attachEvent("onload", CustomOnload);
}
else
{
  // Firefox
  window.addEventListener("onload", CustomOnload, true);
}
