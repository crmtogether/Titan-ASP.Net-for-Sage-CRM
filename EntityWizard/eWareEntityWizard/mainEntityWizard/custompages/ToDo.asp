<!-- #include file ="..\crmwizard.js" -->

<%

CurrentUser=eWare.GetContextInfo("selecteduser", "User_UserId");

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

List=eWare.GetBlock("**&customGridName&**");
List.prevURL=sURL;

container = eWare.GetBlock('container');
container.AddBlock(List);

container.DisplayButton(Button_Default) = false;

// new button
if( **&bWorkflow&** )
{
  container.WorkflowTable = '**&EntityName&**';
  container.ShowNewWorkflowButtons = true;
}
else {
  // remove this code to remove the standard new button
  NewButton = eWare.GetBlock("content");
  NewButton.contents = eWare.Button("New", "new.gif", eWare.URL("**&newTabASP&**")+"&E=**&EntityName&**", '**&EntityName&**', 'insert');
  NewButton.NewLine = false;
  container.AddBlock( NewButton );
}


if( CurrentUser != null && CurrentUser != '' )
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_UserId="+CurrentUser));
}
else
{
  eWare.AddContent(container.Execute("**&ColPrefix&**_UserId IS NULL"));
}

Response.Write(eWare.GetPage('User'));

%>