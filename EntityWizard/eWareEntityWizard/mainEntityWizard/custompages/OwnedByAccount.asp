<!-- #include file ="..\crmwizard.js" -->

<%

CurrentAccountID=eWare.GetContextInfo("account", "Acc_AccountId");

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

if( CurrentAccountID != null && CurrentAccountID != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_AccountId="+CurrentAccountID));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_AccountId IS NULL"));
}

Response.Write(eWare.GetPage('Account'));

%>