<!-- #include file ="..\crmwizard.js" -->

<%

CurrentLeadID=eWare.GetContextInfo("lead", "Lead_LeadId");

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

List=eWare.GetBlock("**&customGridName&**");
List.prevURL=sURL;

container = eWare.GetBlock('container');
container.AddBlock(List);

if( !(**&bWorkflow&**) )
{
  container.AddButton(eWare.Button("New", "new.gif", eWare.URL("**&newTabASP&**")+"&E=**&EntityName&**", '**&EntityName&**', 'insert'));
}

container.DisplayButton(Button_Default) = false;

if( **&bWorkflow&** )
{
  container.WorkflowTable = '**&EntityName&**';
  container.ShowNewWorkflowButtons = true;
}

if( CurrentLeadID != null && CurrentLeadID != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_LeadId="+CurrentLeadID));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_LeadId IS NULL"));
}

Response.Write(eWare.GetPage('Lead'));

%>