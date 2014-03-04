<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Sage CRM ASP.NET Component Suite
*  Version 2.0
*/
//******************************************************************************
try{
  eWare.Mode=2;
  var EntityName=new String(Request.QueryString("EntityName"));
  EntityName=getZeroField(EntityName);
  var EntryBlock=new String(Request.QueryString("EntryBlock"));
  EntryBlock=getZeroField(EntryBlock);
  var BlockTitle=new String(Request.QueryString("BlockTitle"));
  BlockTitle=getZeroField(BlockTitle);
  var Evalcode=new String(Request.Form("Evalcode"));    

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

  if (Defined(Evalcode))
  {
    Evalcode=unescape(Evalcode);
    eval(Evalcode);
  }
      
  Response.Write(Container.Execute(block));

  //as we hide the form we need to output these values
  Response.Write('<INPUT TYPE="HIDDEN" NAME="em" VALUE="2" />');
/*
Removed as of version 2.1.6...now in web form templates  
  Response.Write('<input type=hidden name="yearEntry" />');
  Response.Write('<input type=hidden name="monthEntry" />');
  Response.Write('<input type=hidden name="dayEntry" />');
  */
  Response.Write('<input type=hidden name="HIDDEN_FilterMode" />');
    
}catch(e){
  logerror(e);
}
%>
