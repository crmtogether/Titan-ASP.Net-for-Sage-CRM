<!-- #include file ="SageCRM_portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Sage CRM .NET Component Suite
*  Version 2.0
*  Used by SageCRMPortalEntryBlock component
*/
//******************************************************************************
try{
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

  Container=eWare.GetBlock("container");
  block = eWare.GetBlock(EntryBlock);
  if ((ShowList=="Y") && (Request.Form("em")!=6))
    List=eWare.GetBlock(ListBlock);

  if (Defined(BlockTitle)){
    block.Title=BlockTitle;
  }

  Container.DisplayForm=false;
  block.DisplayForm=false;
  block.DisplayButton(Button_Default) = false;
  Container.ButtonTitle="Search";
  Container.ButtonImage="Search.gif";
  Container.AddBlock(block);
  if ((ShowList=="Y") && (Request.Form("em")!=6))
    Container.AddBlock(List);

//  eWare.Button is not supported so we manually add it
//  Container.AddButton(eWare.Button("Clear", "clear.gif", "javascript:document.EntryForm.em.value='6';document.EntryForm.submit();"));
  Container.AddButton('<A CLASS=ButtonItem ONFOCUS="if (event && event.altKey) click();" ACCESSKEY="l" HREF="javascript:document.EntryForm.em.value=\'6\';document.EntryForm.submit();">' +
                      '<IMG SRC="http://localhost/CRM61/img/Buttons/clear.gif" BORDER=0 ALIGN=MIDDLE></A></TD><TD>&nbsp;</TD><TD>'+
                      '<A CLASS=ButtonItem ONFOCUS="if (event && event.altKey) click();" ACCESSKEY="l" HREF="javascript:document.EntryForm.em.value=\'6\';document.EntryForm.submit();">C'+
                      '<FONT STYLE="text-decoration:underline">l</FONT>ear</A>');

  if ((ShowList=="Y") && (Request.Form("em")!=6))
    List.ArgObj=block;

  Response.Write(Container.Execute(block));


  //as we hide the form we need to output these values
  Response.Write('<INPUT TYPE="HIDDEN" NAME="em" VALUE="2" />');
  Response.Write('<input type=hidden name="yearEntry" />');
  Response.Write('<input type=hidden name="monthEntry" />');
  Response.Write('<input type=hidden name="dayEntry" />');
}catch(e){
  logerror(e);
}
%>
