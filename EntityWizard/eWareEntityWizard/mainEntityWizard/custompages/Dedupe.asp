<!-- #include file ="..\crmwizard.js" -->

<%

eWare.SetContext("New");

var Now=new Date();
if (eWare.Mode<1) eWare.Mode=1;

matchRulesRS = eWare.FindRecord("matchrules","MaRu_TableName=N'**&EntityName&**'");
if( matchRulesRS.eof ) Response.Redirect(eWare.URL("**&newASP&**") + "&E=**&EntityName&**&RefreshTabs=Y");

Entry=eWare.GetBlock("**&dedupeBoxName&**");
Entry.Title="**&EntityName&**";
container=eWare.GetBlock("container");
container.AddBlock(Entry);
container.ButtonImage="nextcircle.gif";
container.ButtonTitle="Enter **&EntityName&** Details";

context=Request.QueryString("context");
if(!Defined(context) )
  context=Request.QueryString("Key0");

container.FormAction=eWare.URL("**&conflictASP&**") + "&E=**&EntityName&**&context=" + context;

eWare.AddContent(container.Execute());
Response.Write(eWare.GetPage());
%>
