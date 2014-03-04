<!-- #include file ="..\crmwizard.js" -->

<%

CurrentCompanyID=eWare.GetContextInfo("company", "Comp_CompanyId");

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

if( CurrentCompanyID != null && CurrentCompanyID != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_CompanyId="+CurrentCompanyID));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_CompanyId IS NULL"));
}

Response.Write(eWare.GetPage('Company'));

%>