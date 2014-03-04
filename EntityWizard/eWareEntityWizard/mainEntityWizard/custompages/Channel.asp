<!-- #include file ="..\crmwizard.js" -->

<%

CurrentChannelID=eWare.GetContextInfo("channel", "chan_ChannelId");

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

List=eWare.GetBlock("**&customGridName&**");
List.prevURL=sURL;

container = eWare.GetBlock('container');
container.AddBlock(List);


container.DisplayButton(Button_Default) = false;

if( **&bWorkflow&** )
{
  container.WorkflowTable = '**&EntityName&**';
  container.ShowNewWorkflowButtons = true;
}
else {
  // remove this line to remove the standard new button
  container.AddButton(eWare.Button("New", "new.gif", eWare.URL("**&newTabASP&**")+"&E=**&EntityName&**", '**&EntityName&**', 'insert'));
}


if( CurrentChannelID != null && CurrentChannelID != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_ChannelId="+CurrentChannelID));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_ChannelId IS NOT NULL"));
}

Response.Write(eWare.GetPage('Channel'));

%>