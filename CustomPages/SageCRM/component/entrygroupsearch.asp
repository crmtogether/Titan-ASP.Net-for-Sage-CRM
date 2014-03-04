<!-- #include file ="SageCRM.js" -->
<%

try{
  eWare.SetContext("Find");
  
  var EntityName=new String(Request.QueryString("EntityName"));
  EntityName=getZeroField(EntityName);
  var EntryBlock=new String(Request.QueryString("EntryBlock"));
  EntryBlock=getZeroField(EntryBlock);
  var EntityWhere=new String(Request.QueryString("EntityWhere"));
  EntityWhere=getZeroField(EntityWhere);
  var BlockTitle=new String(Request.QueryString("BlockTitle"));
  BlockTitle=getZeroField(BlockTitle);
  var ListBlock=new String(Request.QueryString("ListBlock"));
  ListBlock=getZeroField(ListBlock);
  var ShowList=new String(Request.QueryString("showlist"));
  ShowList=getZeroField(ShowList);
  var Evalcode=new String(Request.Form("Evalcode"));
  
  Container=eWare.GetBlock("container");
  block = eWare.GetBlock(EntryBlock);
  if ((ShowList=="Y") && (Request.Form("em")!=6))
    List=eWare.GetBlock(ListBlock);

  if (Defined(BlockTitle)){
    block.Title=BlockTitle;
  }

  block.ShowSavedSearch=true;
  block.UseKeyWordSearch=true;

  Container.DisplayForm=false;
  block.DisplayForm=false;
  block.DisplayButton(Button_Default) = false;
  Container.ButtonTitle="Search";
  Container.ButtonImage="Search.gif";

  Container.AddBlock(block);
  if ((ShowList=="Y") && (Request.Form("em")!=6))
    Container.AddBlock(List);

  Container.AddButton(eWare.Button("Clear", "clear.gif", "javascript:document.forms[0].em.value='6';document.form[0].submit();"));

  if ((ShowList=="Y") && (Request.Form("em")!=6))
    List.ArgObj=block;

//Response.Write(Request.Querystring);
  Response.Write(Container.Execute(block));
  //eWare.AddContent(Container.Execute(block));
  try{
    Response.Write(eWare.GetPageNoFrameset('Find'));
  }catch(ex)
  {
    Response.Write(eWare.GetPage('Find'));
  }   
  //Response.Write('<iframe name="findiframe" ID="findiframe" onload="setupfind()" width="0" height="0" ></iframe>');
  
  //as we hide the form we need to output these values
  Response.Write('<INPUT TYPE="HIDDEN" NAME="em" VALUE="2" />');
/*
Removed as of version 2.1.6...now in web form templates
  Response.Write('<input type=hidden name="yearEntry" />');
  Response.Write('<input type=hidden name="monthEntry" />');
  Response.Write('<input type=hidden name="dayEntry" />');
  */
}catch(e){
  logerror(e);
}  
%>
