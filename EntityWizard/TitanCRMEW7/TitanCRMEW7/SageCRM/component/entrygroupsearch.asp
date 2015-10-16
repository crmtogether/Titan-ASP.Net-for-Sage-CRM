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

  Container.AddButton(eWare.Button("Clear", "clear.gif", "javascript:document.EntryForm.em.value='6';document.EntryForm.submit();"));

  if ((ShowList=="Y") && (Request.Form("em")!=6))
    List.ArgObj=block;

//Response.Write(Request.Querystring);
  Response.Write(Container.Execute(block));
  //eWare.AddContent(Container.Execute(block));
  //this does not seem to be working anymore 13/5/2010
  //Response.Write(eWare.GetPage('Find'));
  Response.Write('<iframe name="findiframe" ID="findiframe" onload="setupfind()" width="0" height="0" ></iframe>');
  
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
<SCRIPT language="JavaScript" type="text/javascript" > 

function setupfind()
{
  var xframe=getFrame();
  eval(xframe.document.scripts[0].innerHTML);
  tframe=getTopFrame();
  SelMenuOption=tframe.document.getElementById("SELECTMenuOption");
  for(i=0;i<SelMenuOption.options.length;i++)
  {
    var op_obj=SelMenuOption.options[i];
    if (op_obj.innerText=="<%=EntityName%>")
    {
      op_obj.selected=true;
      break;
    }
  }
  setTopImage();
}
function setTopImage()
{
  var xframe = getTopFrame();
  var ximg_col = xframe.document.getElementsByTagName("IMG");
  var ximg = ximg_col[0];
  ximg.src="/<%=sInstallName%>/Themes/img/default/Icons/<%=EntityName%>.gif";
}
function getTopFrame()
{
  return window.parent.document.frames["EWARE_TOP"];
}
function getFrame()
{
  return document.frames["findiframe"];
}
function setupTopContentFind(){
  topframe=getFrame();
  topframe.location="<%=eWare.Url(130)%>";  
}

setupTopContentFind();

</script>
<% if (getCRMVersion()=="6") { %>
    <LINK REL="stylesheet" HREF="/<%=sInstallName%>/eware.css">
    <%} else {%>
    <LINK REL="stylesheet" HREF="/<%=sInstallName%>/Themes/color1.css">
    <% } %>