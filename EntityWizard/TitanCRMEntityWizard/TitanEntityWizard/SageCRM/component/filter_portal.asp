<!-- #include file ="SageCRM_Portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try{
  var EntityName=new String(Request.QueryString("EntityName"));
  EntityName=getZeroField(EntityName);
  var EntryBlock=new String(Request.QueryString("EntryBlock"));
  EntryBlock=getZeroField(EntryBlock);
  var BlockTitle=new String(Request.QueryString("BlockTitle"));
  BlockTitle=getZeroField(BlockTitle);

  Container=eWare.GetBlock("container");
  block = eWare.GetBlock(EntryBlock);

  if (Defined(BlockTitle)){
    block.Title=BlockTitle;
  }

  Container.DisplayForm=false;
  block.DisplayForm=false;
  block.DisplayButton(Button_Default) = false;
  Container.ButtonTitle="Filter";
  Container.ButtonImage="Filter.gif";
  Container.ButtonAlignment=Left;
  Container.ButtonLocation=Bottom;

  Container.AddBlock(block);

  Response.Write(Container.Execute(block));

  //as we hide the form we need to output these values
  Response.Write('<INPUT TYPE="HIDDEN" NAME="em" VALUE="2" />');
  Response.Write('<input type=hidden name="yearEntry" />');
  Response.Write('<input type=hidden name="monthEntry" />');
  Response.Write('<input type=hidden name="dayEntry" />');
  Response.Write('<input type=hidden name="HIDDEN_FilterMode" />');
}catch(e){
  logerror(e);
}
%>
