<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try
{
  var crmtabgroup=Request.QueryString('crmtabgroup');
  //Response.Write(Request.QueryString);
  if ((getCRMVersion().indexOf("7.2")==0)||(getCRMVersion().indexOf("7.3")==0)||(getCRMVersion().indexOf("7.4")==0))
  {                        
    var sHTML = "";    
    try{
      sHTML=(eWare.GetPage(crmtabgroup));
    }catch(ex)
    {
      sHTML=(eWare.GetPage(crmtabgroup));
    }   
    Response.Write(sHTML);
  }else{
    var strtabgroup=eWare.GetTabs(crmtabgroup);
    Response.Write(CRM.GetCustomEntityTopFrame(crmtabgroup));
    Response.Write(strtabgroup);    
  }

}catch(e){
  debugcrm("tabgroup", e.description);
  logerror(e);
}

var _vsql="select * from custom_sysparams where parm_name='version'";
var _vq=CRM.CreateQueryObj(_vsql);
_vq.SelectSQL();
var _version="";
if (!_vq.eof)
{
  _version=_vq("parm_value");
}
if (_version=="7.2b")
{
%>
<script>
///needed to fix up screens from 7.2b due to changes made to the CRM api
function fix72b()
{
  var SECONDSET=document.getElementById("SECONDSET");
  if ((SECONDSET!=null)&& (SECONDSET+""!="undefined"))
  {
    SECONDSET.style.position='absolute';
    SECONDSET.style.top="0px";
  }
}
window.onload=fix72b;
</script>

<%

}

%>