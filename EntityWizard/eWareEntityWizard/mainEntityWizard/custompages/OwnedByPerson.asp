<!-- #include file ="..\crmwizard.js" -->

<%

CurrentPersonID=eWare.GetContextInfo("person", "Pers_PersonId");

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

if( CurrentPersonID != null && CurrentPersonID != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_PersonId="+CurrentPersonID));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_PersonId IS NULL"));
}

Response.Write(eWare.GetPage('Person'));

%>