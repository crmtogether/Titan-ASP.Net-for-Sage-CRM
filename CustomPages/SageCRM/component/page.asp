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

  var EntityName=Request.QueryString("EntityName");
  var EntryBlock=Request.QueryString("EntryBlock");
  var EntityWhere=new String(Request.QueryString("EntityWhere"));
  EntityWhere_arr=EntityWhere.split("=");
  EntityWhere_valstr=new String(EntityWhere_arr[1]);
  EntityWhere_valstr_arr=EntityWhere_valstr.split(",");
  if (Defined(EntityWhere_valstr_arr[0]))
    EntityWhere=EntityWhere_arr[0]+"="+EntityWhere_valstr_arr[0];
  var BlockTitle=Request.QueryString("BlockTitle");
  var CheckLocks=new String(Request.QueryString("CheckLocks"));
  var AjaxSearch=new String(Request.QueryString("AjaxSearch"));
  var crmMode=new String(Request.Form(EntryBlock+"_em"));
  var ShowWorkFlowButtons=new String(Request.QueryString("ShowWorkFlowButtons"));
  var Evalcode=new String(Request.Form("Evalcode"));
  
  debugcrm("entrygroup_frm", Request.Form);

  Response.Write("&nbsp;");
  if (Defined(crmMode))
  {
    eWare.Mode=crmMode;
  }
  debugcrm("entrygroup_rf", Request.Form);

  block = eWare.GetBlock(EntryBlock);
  
  if (Defined(Evalcode))
  {
    Evalcode=unescape(Evalcode);
    eval(Evalcode);
  }
    
  if (CheckLocks=="False")  
  {
    block.CheckLocks=false; //default is true  
  }

  var record=eWare.FindRecord(EntityName, EntityWhere);

  if (Defined(BlockTitle)){
    block.Title=BlockTitle;
  }
  
  block.DisplayForm=false;

  block.DisplayButton(Button_Default) = false;
  block.ArgObj=record;

  result='<span id="'+EntryBlock+'_wrapper">';  

  if ( (ShowWorkFlowButtons=="True") && (!record.eof) ) {
    block.ShowWorkflowButtons = true;
    block.WorkflowTable = EntityName;
  }

  result+=block.Execute(record);

  //remove any trailing </FORM> tag that crm puts in RANDOMLY when workflow is used
  var re = /FORM/g;             
  result = result.replace(re, " ");

  if (eWare.Mode==Save){
    emmode=1;
  }else{
    emmode=eWare.Mode+1;
  }
  if (AjaxSearch=="True")
  {
    result+='<input type="hidden" name="em" value="'+emmode+'" />';  
  }else{
    result+='<input type="hidden" name="'+EntryBlock+'_em" value="'+emmode+'" />';    
  }
  result+='</span>';  
//  Response.Write(result);

//  eWare.GetCustomEntityTopFrame(EntityName);
  eWare.AddContent(result);
  Response.Write(eWare.GetPage());

  debugcrm("entrygroup", result);

}catch(e){
  logerror(e);
}
%>