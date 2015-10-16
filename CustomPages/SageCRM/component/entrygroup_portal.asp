<!-- #include file ="SageCRM_portal.js" -->
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
  var EntityWhere=Request.QueryString("EntityWhere");
  var BlockTitle=Request.QueryString("BlockTitle");

  block = eWare.GetBlock(EntryBlock);
  var record=eWare.FindRecord(EntityName, EntityWhere);

  if (Defined(BlockTitle)){
    block.Title=BlockTitle;
  }

  block.DisplayForm=false;

  block.DisplayButton(Button_Default) = false;
  block.ArgObj=record;

  Response.Write(block.Execute());

  if (eWare.Mode==Save){
    emmode=1;
  }else{
    emmode=eWare.Mode+1;
  }

  //as we hide the form we need to output these values
  Response.Write('<input type="HIDDEN" NAME="em" value="'+emmode+'" />');
  Response.Write('<input type=hidden name="yearEntry" />');
  Response.Write('<input type=hidden name="monthEntry" />');
  Response.Write('<input type=hidden name="dayEntry" />');
}catch(e){
  logerror(e);
}
%>
