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
cs_debug("loaded commlist.js");
var PreviewIFrame=document.createElement("IFRAME");
PreviewIFrame.setAttribute('id', 'PreviewIFrame');
PreviewIFrame.setAttribute('width', "1150px");
PreviewIFrame.setAttribute('height', "500px");
PreviewIFrame.style.position="absolute";
PreviewIFrame.style.left="100px";
PreviewIFrame.style.top="25px";

function CustomOnload()
{
  cs_debug("CustomOnload");
  document.body.appendChild(PreviewIFrame);
  _hideElement(PreviewIFrame);
  //attachTagEvent("A","onmouseout",_linkPreviewUndo);
  cs_debug("attaching events");

  	if (typeof window.addEventListener === 'function') {
          attachTagEvent("A","mouseover",_linkPreview);
		    attachTagEvent("BODY","keydown",_linkPreviewUndo);  
	} else if (typeof window.attachEvent === 'function') {
          attachTagEvent("A","onmouseover",_linkPreview);
		    attachTagEvent("BODY","onkeydown",_linkPreviewUndo);  
	}	
	


}
function _linkPreviewUndo(e)
{
cs_debug("_linkPreviewUndo");
  if (e.keyCode == 27)
  {
    _toggleDivObj(0,"");
  }
}

function _linkPreview(e)
{
cs_debug("_linkPreview");
   var e=(typeof event!='undefined')?window.event:e;// IE : Moz 
 
cs_debug("e.ctrlKey:"+e.ctrlKey);
  if(!e.ctrlKey) 
  {
    return;
  }
  //if (!e) var e = window.event;
  cs_debug("e.ctrlKey hit");
  //	var relTarg = e.relatedTarget || e.toElement;
  var relTarg = e.toElement;
	 // cs_debug("relTarg:");
  var p_obj=null;
    cs_debug("relTarg.tagName:"+relTarg.tagName);
  if ((relTarg.tagName=="A"))
  {
    p_obj=relTarg;
    //alert(relTarg.href);  
  }else{
    p_obj=relTarg.parentNode;
  }
   cs_debug("p_obj.tagName="+p_obj.tagName);
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
  cs_debug("toggle off");
  _hideElement(PreviewIFrame);
}else{
    cs_debug("toggle on");
  _showElement(PreviewIFrame);  
  PreviewIFrame.setAttribute('src', linkLocation);
}
}
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////// 
window.onload=CustomOnload;
/*
--browser updates mean this this not always working anymore
if(document.all)
{
  // Microsoft IE (W3 compliant)
  cs_debug("ms loader");
  window.attachEvent("onload", CustomOnload);
}
else
{
  // Firefox
  cs_debug("non ms loader");
  window.addEventListener("load", CustomOnload, true);
}
*/