<!-- #include file ="SageCRM.js" -->
<!-- #include file ="crmconst.js" -->
<%
//**********************************************
//* functions START
//**********************************************
function getEntityName()
{
  var res = new String(Request.QueryString("E"));
  var res_arr=res.split(",");
  return res_arr[0];
}
function getBlockName(EntityName)
{
  return EntityName+"NewEntry";
}
function getIdField(EntityName)
{
  var rec=eWare.FindRecord("custom_tables","bord_name='"+EntityName+"'")
  return rec("bord_idfield");
}
function getFieldPrefix(EntityName)
{
  var rec=eWare.FindRecord("custom_tables","bord_name='"+EntityName+"'")
  return rec("bord_prefix");
}
//**********************************************
//* functions END
//**********************************************
%>
<%
var CRMPath=sInstallName;
var EntityName=getEntityName();
var SummaryPage="crment_Summary.asp";
var NewPage="crment_New.asp";
var BlockName=getBlockName(EntityName);
var IdField=getIdField(EntityName);
var FieldPrefix=getFieldPrefix(EntityName);
%>
<html>
  <head>
  <link type="text/css" rel="Stylesheet" href="/<%=CRMPath%>/Themes/default.css" />   
  <title>CRM Together</title>  
  </head>
  <body>
  <div>
  <%
    Response.Write(eWare.Button("Save", "save.gif", "javascript:document.EntryForm.submit();", "","",""));
  %>
  </div>
<%
var Now=new Date();
if (eWare.Mode<Edit) eWare.Mode=Edit;

record=eWare.CreateRecord(EntityName);
if(false)
  record.SetWorkFlowInfo(EntityName+" Workflow", "Logged");

EntryGroup=eWare.GetBlock(BlockName);
EntryGroup.Title=EntityName;

context=Request.QueryString("context");
if(!Defined(context) )
  context=Request.QueryString("Key0");

if( context == iKey_CompanyId && false )
{
  CompId = eWare.GetContextInfo('Company','Comp_CompanyId');
  if ((Defined(CompId)) && (CompId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_CompanyId");
    Entry.DefaultValue = CompId;
  }
}
else if( context == iKey_PersonId && false )
{
  PersId = eWare.GetContextInfo('Person','Pers_PersonId');
  if ((Defined(PersId)) && (PersId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_PersonId");
    Entry.DefaultValue = PersId;
  }
}
else if( context == iKey_UserId && true )
{
  UserId = eWare.GetContextInfo('User', 'User_UserId');
  if ((Defined(UserId)) && (UserId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_UserId");
    Entry.DefaultValue = UserId;
  }
}
else if( context == iKey_ChannelId && false )
{
  ChanId = eWare.GetContextInfo('Channel', 'Chan_ChannelId');
  if ((Defined(ChanId)) && (ChanId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_ChannelId");
    Entry.DefaultValue = ChanId;
  }
}
else if( context == iKey_LeadId && false )
{
  LeadId = eWare.GetContextInfo('Lead','Lead_LeadId');
  if ((Defined(LeadId)) && (LeadId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_LeadId");
    Entry.DefaultValue = LeadId;
  }
}
else if( context == iKey_OpportunityId && false )
{
  OppoId = eWare.GetContextInfo('Opportunity','Oppo_OpportunityId');
  if ((Defined(OppoId)) && (OppoId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_OpportunityId");
    Entry.DefaultValue = OppoId;
  }
}
else if( context == iKey_OrderId && false )
{
  OrderId = eWare.GetContextInfo('Orders','Orde_OrderQuoteId');
  if ((Defined(OrderId)) && (OrderId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_OrderId");
    Entry.DefaultValue = OrderId;
  }
}
else if( context == iKey_QuoteId && false )
{
  QuoteId = eWare.GetContextInfo('Quotes','Quot_OrderQuoteId');
  if ((Defined(QuoteId)) && (QuoteId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_QuoteId");
    Entry.DefaultValue = QuoteId;
  }
}
else if( context == iKey_CaseId && false )
{
  CaseId = eWare.GetContextInfo('Case','Case_CaseId');
  if ((Defined(CaseId)) && (CaseId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_CaseId");
    Entry.DefaultValue = CaseId;
  }
}
else if( context == iKey_AccountId && false )
{
  AccountId = eWare.GetContextInfo('Account','Acc_AccountId');
  if ((Defined(AccountId)) && (AccountId > 0))
  {
    Entry = EntryGroup.GetEntry(FieldPrefix+"_AccountId");
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
container.DisplayButton(Button_Default) = false;

if( false )
{
  container.ShowWorkflowButtons = true;
  container.WorkflowTable = EntityName;
}

if(eWare.Mode==Save)
{
  container.Execute(record);
  Response.Redirect(SummaryPage + "?J="+EntityName+"/"+SummaryPage+"&E="+EntityName+"&"+IdField+"="+record(IdField)+"&"+Request.QueryString);
}
else
{
  Response.Write(container.Execute(record));
}

%>
  <div>
  <%
    Response.Write(eWare.Button("Save", "save.gif", "javascript:document.EntryForm.submit();", "","",""));
  %>
  </div>
  </body>
</html>