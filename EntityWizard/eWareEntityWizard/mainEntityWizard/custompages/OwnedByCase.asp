<!-- #include file ="..\crmwizard.js" -->

<%

CurrentCaseID=eWare.GetContextInfo("case", "Case_CaseId");

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

if( CurrentCaseID != null && CurrentCaseID != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_CaseId="+CurrentCaseID));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_CaseId IS NULL"));
}

Response.Write(eWare.GetPage('case'));

%>