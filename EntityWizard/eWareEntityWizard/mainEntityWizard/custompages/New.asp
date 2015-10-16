<!-- #include file ="..\crmwizard.js" -->
<!-- #include file ="..\crmconst.js" -->

<%

var Now=new Date();
if (eWare.Mode<Edit) eWare.Mode=Edit;

record=eWare.CreateRecord("**&EntityName&**");
if( **&bWorkflow&** )
  record.SetWorkFlowInfo("**&EntityName&** Workflow", "Logged");

EntryGroup=eWare.GetBlock("**&searchEntry&**");
EntryGroup.Title="**&EntityName&**";

context=Request.QueryString("context");
if(!Defined(context) )
  context=Request.QueryString("Key0");

if( !**&bDedupe&** )
  eWare.SetContext("New");

if( context == iKey_CompanyId && **&bCompany&** )
{
  CompId = eWare.GetContextInfo('Company','Comp_CompanyId');
  if ((Defined(CompId)) && (CompId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_CompanyId");
    Entry.DefaultValue = CompId;
  }
}
else if( context == iKey_PersonId && **&bPerson&** )
{
  PersId = eWare.GetContextInfo('Person','Pers_PersonId');
  if ((Defined(PersId)) && (PersId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_PersonId");
    Entry.DefaultValue = PersId;
  }
}
else if( context == iKey_UserId && **&bUser&** )
{
  UserId = eWare.GetContextInfo('User', 'User_UserId');
  if ((Defined(UserId)) && (UserId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_UserId");
    Entry.DefaultValue = UserId;
  }
}
else if( context == iKey_ChannelId && **&bChannel&** )
{
  ChanId = eWare.GetContextInfo('Channel', 'Chan_ChannelId');
  if ((Defined(ChanId)) && (ChanId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_ChannelId");
    Entry.DefaultValue = ChanId;
  }
}
else if( context == iKey_LeadId && **&bLead&** )
{
  LeadId = eWare.GetContextInfo('Lead','Lead_LeadId');
  if ((Defined(LeadId)) && (LeadId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_LeadId");
    Entry.DefaultValue = LeadId;
  }
}
else if( context == iKey_OpportunityId && **&bOpportunity&** )
{
  OppoId = eWare.GetContextInfo('Opportunity','Oppo_OpportunityId');
  if ((Defined(OppoId)) && (OppoId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_OpportunityId");
    Entry.DefaultValue = OppoId;
  }
}
else if( context == iKey_OrderId && **&bOrder&** )
{
  OrderId = eWare.GetContextInfo('Orders','Orde_OrderQuoteId');
  if ((Defined(OrderId)) && (OrderId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_OrderId");
    Entry.DefaultValue = OrderId;
  }
}
else if( context == iKey_QuoteId && **&bQuote&** )
{
  QuoteId = eWare.GetContextInfo('Quotes','Quot_OrderQuoteId');
  if ((Defined(QuoteId)) && (QuoteId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_QuoteId");
    Entry.DefaultValue = QuoteId;
  }
}
else if( context == iKey_CaseId && **&bCase&** )
{
  CaseId = eWare.GetContextInfo('Case','Case_CaseId');
  if ((Defined(CaseId)) && (CaseId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_CaseId");
    Entry.DefaultValue = CaseId;
  }
}
else if( context == iKey_AccountId && **&bAccount&** )
{
  AccountId = eWare.GetContextInfo('Account','Acc_AccountId');
  if ((Defined(AccountId)) && (AccountId > 0))
  {
    Entry = EntryGroup.GetEntry("**&ColPrefix&**_AccountId");
    Entry.DefaultValue = AccountId;
  }
}


names = Request.QueryString("fieldname");
if( Defined(names) )
{
  vals = Request.QueryString("fieldval");
  //get values from dedupe box
  for( i = 1; i <= names.Count; i++)
  {
    Entry = EntryGroup.GetEntry(names(i));
    if( Entry != null )
      Entry.DefaultValue = vals(i);
  }
}

container=eWare.GetBlock("container");
container.AddBlock(EntryGroup);

container.AddButton(
   eWare.Button("Cancel", "cancel.gif", 
      eWare.Url("521")));

if( **&bWorkflow&** )
{
  container.ShowWorkflowButtons = true;
  container.WorkflowTable = '**&EntityName&**';
}

eWare.AddContent(container.Execute(record));

if(eWare.Mode==Save)
  Response.Redirect("**&summaryASP&**?J=**&EntityName&**/**&summaryASP&**&E=**&EntityName&**&**&idfield&**="+record("**&idfield&**")+"&"+Request.QueryString);
else
{
  RefreshTabs=Request.QueryString("RefreshTabs");
  if( RefreshTabs = 'Y' )
    Response.Write(eWare.GetPage('New'));
  else
    Response.Write(eWare.GetPage());
}

%>