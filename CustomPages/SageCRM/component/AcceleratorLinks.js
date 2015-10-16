//******************************************************************************
//******************************************************************************
/*
*  Accelerator for Sage CRM
*  This software should only be used with a valid 'Accelerator for Sage CRM' license
*  Web: http://www.crmtogether.com
*  Email: sagecrm@crmtogether.com
*  Requires crmtogether.js script file to work and field "comm_acceleratorlink" to be on screen
*  as well as Entity metadata
*/
//******************************************************************************
var _Datacomm_acceleratorlink=null;
function AcceleratorLinksCustomOnload()
{
  _Datacomm_acceleratorlink=document.getElementById("_Datacomm_acceleratorlink");
  if (_Datacomm_acceleratorlink==null)
    return;
  _Datacomm_acceleratorlink.innerHTML="";
  if (getCommId()=="")
  {
    hide_Captcomm_acceleratorlink();
    //_Datacomm_acceleratorlink.innerHTML="Links can only be added after the communication has been created";
    return;//ie we dont work on the new screen...the comm must exist first
  }
  AcceleratorLinksAddList();
  AcceleratorLinksAddButton();
}
function hide_Captcomm_acceleratorlink()
{
  _hideElementById("_Captcomm_acceleratorlink");
}
function AcceleratorLinksAddButton()
{
  return;
  var ht=getButton("Add Person", "PersLink.gif", "javascript:AcceleratorLinksAddContent()");
  _Datacomm_acceleratorlink.innerHTML+=ht;
}
function AcceleratorLinksAddList()
{
  var _commid=getCommId();
  var data=SQLQuery("select * from vAcceleratorLinkCommPers where acli_tableid="+_commid);
  var linksHTML=new String("");
  for(var i=0;i<data.length;i++)
  {
    if (i>0)
      linksHTML+=", ";
    linksHTML+=data[i].pers_firstname + " " + data[i].pers_lastname;
    //original line with remove option
    //linksHTML+="<a href=\"javascript:RemovePerson("+data[i].pers_personid+",'"+data[i].pers_firstname + " " + data[i].pers_lastname+"')\" >"+data[i].pers_firstname + " " + data[i].pers_lastname+"</a>"
  }
  if (linksHTML=="")
  {
    hide_Captcomm_acceleratorlink()
    return;  
  }
  //var ht=getList("AcceleratorLinksList","AcceleratorLinks","acli_tablename='Communication' and acli_tableid=453","")
  //_Datacomm_acceleratorlink.innerHTML+=ht;
  _Datacomm_acceleratorlink.innerHTML+=linksHTML;
}

function RemovePerson(personid, personname)
{
  if (confirm("Are you sure you want to remove "+personname+"?")) {
    var sql="Update AcceleratorLinks set acli_deleted=1 where acli_tableid="+getCommId()+" and acli_linktableid="+personid;
    UpdateSQLQuery(sql);
    _Datacomm_acceleratorlink.innerHTML="";
    AcceleratorLinksAddList();
    AcceleratorLinksAddButton();    
  }
}
function AddPerson()
{
  CreateAcceleratorLink("Communication", getCommId(), "Person", getPersonId());
  _Datacomm_acceleratorlink.innerHTML="";
  AcceleratorLinksAddList();
  AcceleratorLinksAddButton();
}
function getPersonId()
{
  var obj=document.getElementById("acli_linktableid");
  return obj.value;
}
function getCommId()
{
  var keystring=GetKeys();
  Key6=getQueryVariableFromPath(keystring, "Key6");
  return Key6;
}
function CancelAdd()
{
  _Datacomm_acceleratorlink.innerHTML="";
  AcceleratorLinksAddList();
  AcceleratorLinksAddButton();
}
function AcceleratorLinksAddContent()
{
  _Datacomm_acceleratorlink.innerHTML="";
  AcceleratorLinksAddList();
  var ht=getEntryGroupNew("AcceleratorLinks","AcceleratorLinksScreen", "","");
  var ht2=getButton("Cancel", "WorkflowReject.gif", "javascript:CancelAdd()");
  var ht3=getButton("Add", "WorkflowAccept.gif", "javascript:AddPerson()");
  _Datacomm_acceleratorlink.innerHTML+="<table>"+
                                      "<tr><td>"+ht2+"</td></tr>"+
                                      "<tr><td>"+ht3+"</td></tr>"+
                                      "<tr><td>"+ht+"</td></tr>"+
                                      "</table>";

}

if(document.all)
{
  // Microsoft IE (W3 compliant)
  window.attachEvent("onload", AcceleratorLinksCustomOnload);
}
else
{
  // Firefox
  window.addEventListener("onload", AcceleratorLinksCustomOnload, true);
}
