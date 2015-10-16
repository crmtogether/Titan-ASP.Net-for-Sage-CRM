<!-- #include file ="..\crmwizard.js" -->

<%

CurrentOpportunityID=eWare.GetContextInfo("Opportunity", "Oppo_OpportunityId");

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

if( CurrentOpportunityID != null && CurrentOpportunityID != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_OpportunityId="+CurrentOpportunityID));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_OpportunityId IS NULL"));
}

Response.Write(eWare.GetPage('Opportunity'));

%>