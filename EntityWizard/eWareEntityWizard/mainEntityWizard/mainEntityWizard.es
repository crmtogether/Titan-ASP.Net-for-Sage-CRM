/////////////////////////////////
//collect/initialize parameters//
/////////////////////////////////
var EntityName = Param('ENTITY NAME');
var ColPrefix = Param('ENTITY COLUMN PREFIX');
var ToDo = Param('ADD TO TODO?');
var Channels = Param('ADD TO CHANNELS?');
var Find = Param('ADD TO FIND?');
var New = 'on';

var HasCompanies = Param('HAS COMPANIES?');
var HasPeople = Param('HAS PEOPLE?');
var HasCommunications = Param('HAS COMMUNICATIONS?');
var HasOpportunities = Param('HAS OPPORTUNITIES?');
var HasLeads = Param('HAS LEADS?');
var HasCases = Param('HAS CASES?');
var HasAccounts = Param('HasAccounts');
var HasCommunications = Param('HAS COMMUNICATIONS?');
var HasLibrary = Param('HAS LIBRARY?');

var OwnedByCompanies = Param('OWNED BY COMPANIES?');
var OwnedByPeople = Param('OWNED BY PEOPLE?');
var OwnedByOpportunity = Param('OWNED BY OPPORTUNITIES?');
var OwnedByOrders = Param('OwnedByOrders');
var OwnedByQuotes = Param('OwnedByQuotes');
var OwnedByLeads = Param('OWNED BY LEADS?');
var OwnedByCases = Param('OWNED BY CASES?');
var OwnedByAccounts = Param('OwnedByAccounts');

var Deduplication = Param('DEDUPLICATION?');
var Workflow = Param('WORKFLOW?');
var WorkflowProgress = Param('HAS WORKFLOW PROGRESS?');
var ForDotNet = (Param('DotNet') == 'on');

var IsWebServiceTable = Param('WSDL');

var ProgressNote = '';
var ProgressTable = '';
var sSpacer = '&nbsp&nbsp&nbsp&nbsp';

var Bord_SoloOptions = Param('OfflineOptions');

var Bord_SoloDateOptions = Param('OfflineDateOptions');

var Tabs_OnlineOnly = false;

var Size_Of_ASS = 25;



//////////////
//validation//
//////////////

valid = true;
ErrorStr = '';

//Warn the user if there are spaces so they can reenter the name
if (EntityName.indexOf(" ") > 0) {
   valid = false;
   ErrorStr = ErrorStr + 'Entity Name cannot contain spaces';
}

//EntityName = EntityName.replace(" ", "");

//ensure entity name length < 23 - oracle has max of 30 chars for db object names
if( EntityName.length > 22 )
{
  valid = false;
  ErrorStr = ErrorStr+'- Could not create main entity: Entity name must less than 23 characters. '+'<BR><BR>'+sSpacer;
}

//Prefix and EntityName cannot start with numbers
if (!isOkChar(EntityName.charAt(0))) {
  valid = false;
  ErrorStr = ErrorStr + '- Entity Name must start with a valid character' + '<BR><BR>'+sSpacer;
}

//Prefix and EntityName cannot start with numbers
if (!isOkChar(ColPrefix.charAt(0))) {
  valid = false;
  ErrorStr = ErrorStr + '- Column Prefix must start with a valid character'+'<BR><BR>'+sSpacer;
}

//Do not allow user to create an entity which already exists in CRM, If it is a custom entity it will be ignored and allowed to process in case user is just adding some extra options/params.
if(TableExists(EntityName))
{   
  valid = false;
  ErrorStr = ErrorStr + 'Entity Name can not be the same as any CRM Entity Name.'+'<BR><BR>'+sSpacer; 
}

//if a table is set to have no table on the client then make the tabs not available offline
if (Bord_SoloOptions == '-1') {
   Tabs_OnlineOnly = true;
}

if( WorkflowProgress == 'on' && Workflow != 'on' )
{ 
  valid = false;
  ErrorStr = ErrorStr + '- Cannot have workflow progress without having workflow' + '<BR><BR>'+sSpacer;
}


if( valid ){

/////////////
//variables//
/////////////
var JumpIdField;
var JumpSummaryPage;
var CustomFunction;
var Grip_Jump;


var idfield = ColPrefix + "_" + EntityName + 'ID';
var customSummaryPage = EntityName + 'Summary.asp';

if (!ForDotNet)
{
  Grip_Jump = 'custom';
  CreateNewDir(GetDLLDir() + '\\CustomPages\\' + EntityName);
  JumpIdField = idfield;
  JumpSummaryPage = EntityName + '\\' + customSummaryPage
}
else
{
  Grip_Jump = 'customdotnetdll';
  JumpIdField = idfield;
  JumpSummaryPage = EntityName; // assembly dll name
  CustomFunction = 'RunDataPage';
}

var Icon = EntityName + '.gif';
var DLLDir = GetDLLDir();
var InstallDir = GetInstallDir();
var componentDir = InstallDir + '\\inf\\mainEntityWizard\\';
var customPagesDir = componentDir + '\\CustomPages\\';
var entityDir = DLLDir + '\\CustomPages\\' + EntityName + '\\';
var userIDCol = false;
var customFileName;
var customGridName;
var customEntryName;
var tabgroup;
var foreignkey;
var nameCol;
var newASP;
newASP = EntityName + 'New.asp';
var findASP;
var conflictASP;
var dedupeASP;
dedupeASP = EntityName + 'Dedupe.asp';
var newTabASP;
if( Deduplication == 'on' ) 
  newTabASP = dedupeASP;
else
  newTabASP = newASP;

customEntryName = EntityName + 'SummaryScreen';
channelIDCol=false;
userIDCol=false;
ProgressTable='';
tabgroup=EntityName;
nameCol = ColPrefix + '_Name';

CopyAspTo('\\EntityIcon.gif', '\\themes\\img\\default\\icons\\' + EntityName + '.gif');
CopyAspTo('\\SmallEntityIcon.gif', '\\themes\\img\\default\\icons\\small_' + EntityName + '.gif');
CopyAspTo('\\SummaryEntityIcon.gif', '\\themes\\img\\default\\icons\\summary\\small_' + EntityName + '.gif');
CopyAspTo('\\RelatedEntityIcon.gif', '\\themes\\img\\default\\icons\\related_' + EntityName + '.gif');

AddMessage(GetTrans('EntityWizard','tocreatecustomentity') +
    DLLDir + '\\themes\\img\\default\\icons\\' + EntityName + '.gif, small_'+EntityName+'.gif and related_'+EntityName+'.gif');


////////////
//TOP CONTENT
/////////////

//Create the top content block for this entity
  AddCustom_ScreenObjects(EntityName+'TopContent','Screen',EntityName,'Y','0',EntityName,'','',''); 
  AddCustom_Screens(EntityName+'TopContent','1',nameCol,'0','0','0','',0,'','','');


////////////
//Workflow//
////////////

//if they checked the workflow checkbox we need to pass the workflowid field to 
//the create table api
var workflowidfield=false;
if( Workflow == 'on' ) 
{
  customFileName = EntityName + 'Wf.asp';
  
  //add the E parameter to specify the entity for top frame tab silent action
  CreateNewWorkflow( EntityName + ' Workflow', false, EntityName, EntityName+'\\'+customFileName + '?E=' + EntityName );
  workflowidfield = true;

  //makes a renamed copy of the generic asp to be used for the todo custom asp file
  if (!ForDotNet)
  { 
    CopyFile(customPagesDir + 'Workflow.asp', entityDir + customFileName);

  
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&summaryASP&**', customSummaryPage);

    SetValueInFile(OwnedByCompanies, '**&bCompany&**', entityDir + customFileName);
    SetValueInFile(OwnedByPeople, '**&bPerson&**', entityDir + customFileName);
    SetValueInFile(OwnedByOpportunity, '**&bOpportunity&**', entityDir + customFileName);
    SetValueInFile(OwnedByOrders, '**&bOrder&**', entityDir + customFileName);
    SetValueInFile(OwnedByQuotes, '**&bQuote&**', entityDir + customFileName);
    SetValueInFile(OwnedByLeads, '**&bLead&**', entityDir + customFileName);
    SetValueInFile(OwnedByCases, '**&bCase&**', entityDir + customFileName);
    SetValueInFile(OwnedByAccounts, '**&bAccount&**', entityDir + customFileName);
    SetValueInFile(Channels, '**&bChannel&**', entityDir + customFileName);
    SetValueInFile(ToDo, '**&bUser&**', entityDir + customFileName);
     
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&SummaryEntry&**', EntityName + 'NewEntry');
  }
}

///////////////////////////////////////////////////////////////////////////////////////////   
//create the new entity's table and progress table (if specified) with default name field//
/////////////////////////////////////////////////////////////////////////////////////////// 

if( WorkflowProgress == 'on' )
{
  ProgressTable = EntityName + 'Progress';
  ProgressNote = ColPrefix + '_ProgressNote';
  ProgressTableId = ColPrefix + '_' + ProgressTable + 'Id';
  ProgressListASP = EntityName + 'ProgressList.asp';
  ProgressNoteBox = EntityName + 'ProgressNoteBox';
  
  //progress table
  CreateTable(ProgressTable, ColPrefix, ProgressTableId, false, false, false);
  
  AddColumn(ProgressTable, idfield, 31, 4, true, false);
  AddCustom_Edits(ProgressTable, idfield, 31, 0, '', 0, '', '', '', 'N', '', '');
    
  AddColumn(ProgressTable, nameCol, 10, '30', true, false);
  AddCustom_Edits(ProgressTable, nameCol, 10, 0, '', 30, '', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', nameCol, 1, 'Name', 'Name', 'Nom', '', 'Nombre', 'Naam', '');
  
  AddColumn(ProgressTable, ColPrefix+'_SecTerr', 31, 4, true, false);
  AddCustom_Edits(ProgressTable, ColPrefix+'_SecTerr', 53, 0, '', 0, '', '', '', '', '', '');
  
  AddColumn(ProgressTable, ProgressNote, '11','0','true','false'); 
  AddCustom_Edits(ProgressTable,ProgressNote,'11','0','','20','','','','',''); 
  AddCustom_Captions('Tags','ColNames',ProgressNote,'0','Progress Note','','','','','',''); 

  AddCustom_Edits(ProgressTable, ColPrefix+'_CreatedBy', 22, 0, '', 0, '', '', '', 'N', '', '');  
  AddCustom_Edits(ProgressTable, ColPrefix+'_UpdatedBy', 22, 0, '', 0, '', '', '', 'N', '', '');  
  AddCustom_Edits(ProgressTable, ColPrefix+'_TimeStamp', 41, 0, '', 0, '', '', '', '', '', 'Y');
  AddCustom_Edits(ProgressTable, ColPrefix+'_deleted', 31, 0, '', 0, '', '', '', '', '', 'Y');
  AddCustom_Edits(ProgressTable, ColPrefix+'_UpdatedDate', 41, 0, '', 0, '', '', '', 'N', '', '');
  AddCustom_Edits(ProgressTable, ColPrefix+'_CreatedDate', 41, 0, '', 0, '', '', '', 'N', '', '');
      
  ProgressListName = EntityName + 'ProgressList';
  AddCustom_ScreenObjects(ProgressListName,'List',ProgressTable,'Y','0',ProgressTable,'','',''); 
  AddCustom_Lists(ProgressListName,'1',ColPrefix + '_createddate','','','','','','','','','',0);
  AddCustom_Lists(ProgressListName,'2',ColPrefix + '_createdby','','','','','','','','','',0);
  AddCustom_Lists(ProgressListName,'3',ProgressNote,'','','','','','','','','',0);
  
  AddCustom_ScreenObjects(ProgressNoteBox,'Screen',ProgressTable,'Y','0',ProgressTable,'','',''); 
  AddCustom_Screens(ProgressNoteBox,'1',ProgressNote,'0','0','0','',0,'','','');

  AddCustom_Captions('Tags',ProgressTable,'RecordsFound','0','Records Found','Records Found','','','','',''); 
  AddCustom_Captions('Tags',ProgressTable,'RecordFound','0','Record Found','Record Found','','','','','');   
  AddCustom_Captions('Tags','ColNames',ColPrefix + '_createddate','0','Created Date','Created Date','','','','',''); 
  AddCustom_Captions('Tags','ColNames',ColPrefix + '_createdby','0','Created By','Created By','','','','',''); 
  AddCustom_Captions('Tags','ColNames',ProgressNote,'0','Progress Note','Progress Note','','','','',''); 
  AddCustom_Captions('Tags',ProgressTable,'Details','0','Details','Details','','','','',''); 

  if (!ForDotNet) 
  {  
    CopyFile(customPagesDir + 'ProgressList.asp', entityDir + ProgressListASP);

    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, '**&ProgressListName&**', ProgressListName);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, '**&tabgroup&**', tabgroup);
  }
}

CreateTable(EntityName, ColPrefix, idfield, true, false, false, workflowidfield, ProgressTable, ProgressNote);

AddColumn(EntityName, nameCol, 10, '30', true, false);
AddCustom_Edits(EntityName, nameCol, 10, 0, '', 30, '', '', '', '', '', '');
AddCustom_Captions('Tags', 'ColNames', nameCol, 1, 'Name', 'Name', 'Nom', '', 'Nombre', 'Naam', '');

AddColumn(EntityName, ColPrefix+'_SecTerr', 31, 4, true, false);
AddCustom_Edits(EntityName, ColPrefix+'_SecTerr', 53, 0, '', 0, '', '', '', '', '', '');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_SecTerr', 1, 'Territory', '', '', '', '', '', '');

AddCustom_Edits(EntityName, ColPrefix+'_CreatedBy', 22, 0, '', 0, '', '', '', 'N', '', '');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_CreatedBy', 1, ColPrefix+'_CreatedBy', '', '', '', '', '', '');

AddCustom_Edits(EntityName, ColPrefix+'_UpdatedBy', 22, 0, '', 0, '', '', '', 'N', '', '');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_UpdatedBy', 1, ColPrefix+'_UpdatedBy', '', '', '', '', '', '');

AddCustom_Edits(EntityName, ColPrefix+'_TimeStamp', 41, 0, '', 0, '', '', '', '', '', 'Y');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_TimeStamp', 1, ColPrefix+'_TimeStamp', '', '', '', '', '', '');

if (workflowidfield) {
   AddCustom_Edits(EntityName, ColPrefix+'_WorkflowId', 31, 0, '', 0, '', '', '', '', '', 'Y');
   AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_WorkflowId', 1, ColPrefix+'_WorkflowId', '', '', '', '', '', '');
}

AddCustom_Edits(EntityName, ColPrefix+'_deleted', 31, 0, '', 0, '', '', '', '', '', 'Y');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_deleted', 1, ColPrefix+'_deleted', '', '', '', '', '', '');

AddCustom_Edits(EntityName, idfield, 31, 0, '', 0, '', '', '', '', '', 'Y');
AddCustom_Captions('Tags', 'ColNames', idfield, 1, idfield, '', '', '', '', '', '');

AddCustom_Edits(EntityName, ColPrefix+'_UpdatedDate', 41, 0, '', 0, '', '', '', 'N', '', '');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_UpdatedDate', 1, ColPrefix+'_UpdatedDate', '', '', '', '', '', '');

AddCustom_Edits(EntityName, ColPrefix+'_CreatedDate', 41, 0, '', 0, '', '', '', 'N', '', '');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_CreatedDate', 1, ColPrefix+'_CreatedDate', '', '', '', '', '', '');

//Add Status column
AddColumn(EntityName, ColPrefix+'_Status', 10, 40, true, false);
AddCustom_Edits(EntityName, ColPrefix+'_Status', 21, 0, '', 0, ColPrefix+'_Status', '', '', '', '', '');
AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_Status', 1, 'Status', '', '', '', '', '', '');
AddCustom_Captions('Choices', ColPrefix+'_Status','InProgress', 1, 'In Progress', '', '', '', '', '', '');
AddCustom_Captions('Choices', ColPrefix+'_Status','Closed', 2, 'Closed', '', '', '', '', '', '');


////////////////////////////////////////////////////////////////////////////////
//create the neccessary metadata to allow for searchselects for the new entity//
////////////////////////////////////////////////////////////////////////////////
AddCustom_Captions('Tags', 'SS_SearchTables', EntityName, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
AddCustom_Captions('Tags', 'SS_ViewFields', EntityName, 0, nameCol, nameCol, nameCol, nameCol, nameCol, nameCol, nameCol);
AddCustom_Captions('Tags', 'SS_idfields', EntityName, 0, idfield, idfield, idfield, idfield, idfield, idfield, idfield);
AddCustom_Captions('Tags', 'SS_Entities', EntityName, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);

//add a few captions for list headers
AddCustom_Captions('Tags', EntityName, EntityName, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
AddCustom_Captions('Tags', EntityName, 'NoRecordsFound', 0, 'No '+ EntityName, 'No '+ EntityName, 'No '+ EntityName, 'No '+ EntityName, 'No '+ EntityName, 'No '+ EntityName, 'No '+ EntityName);
AddCustom_Captions('Tags', EntityName, 'RecordsFound', 0, EntityName, EntityName,'','','','',''); 
AddCustom_Captions('Tags', EntityName, 'RecordFound', 0, EntityName + ' Found', EntityName + ' Found','','','','',''); 
AddCustom_Captions('Tags', 'FindHead', EntityName, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
AddCustom_Captions('Tags', 'NewHead', EntityName, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);

AddCustom_Captions('Choices', 'Tables', EntityName, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);

AddCustom_Captions('Tags', 'admindescription', EntityName, 0, 'Customize '+EntityName+' fields, screens, lists, tabs, blocks, table scripts and views.', 'Customise '+EntityName+' fields, screens, lists, tabs, blocks, table scripts and views.','','','','','');
AddCustom_Captions('Tags', 'Tabnames', EntityName, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);

////////////////////////////////   
//create the custom tabs group//
//////////////////////////////// 
AddCustom_ScreenObjects(EntityName, 'TabGroup', EntityName, 'N', 0, EntityName, '', '', '');

findASP = EntityName + 'Find.asp';

//create the custom summary screen  
AddCustom_ScreenObjects(customEntryName, 'Screen', EntityName, 'N', 0, EntityName, '', '', '');   

if (!ForDotNet) 
{  
  //makes a renamed copy of the generic asp summary screen
  CopyFile(customPagesDir + 'Summary.asp', entityDir + customSummaryPage);

  //add the custom tab to the Entity's Tab Group
  AddCustom_Tabs(0, 0, 0, tabgroup, EntityName + ' Summary', 'customfile', EntityName + '\\' + customSummaryPage, '', '', 0, EntityName,Tabs_OnlineOnly);
}

//keep track of the name column, the id column and the summary page in the custom_captions
AddCustom_Captions('Tags', EntityName, 'NameColumn', 1, nameCol, nameCol, nameCol, nameCol, nameCol, nameCol, nameCol);
AddCustom_Captions('Tags', EntityName, 'IdColumn', 1, idfield, idfield, idfield, idfield, idfield, idfield, idfield);
AddCustom_Captions('Tags', EntityName, 'SummaryPage', 1, customSummaryPage, customSummaryPage, customSummaryPage, customSummaryPage, customSummaryPage, customSummaryPage, customSummaryPage);

if( WorkflowProgress == 'on' )
{
  AddCustom_Tabs('0','0','20',tabgroup,'Tracking','customfile', EntityName + '\\' + ProgressListASP,'','',0,'',Tabs_OnlineOnly);
}

///////////////////////////////
//if we're adding it to To Do//
///////////////////////////////

if( ToDo == 'on' ) 
{
  //add a foreign key to the user table
  AddColumn(EntityName, ColPrefix + '_UserId', 22, 4, true, false);
  RunSql("UPDATE Custom_Tables SET Bord_AssignedUserId=N'"+ColPrefix+"_UserId' WHERE Bord_Name = N'"+EntityName+"'");

  AddCustom_Edits(EntityName, ColPrefix + '_UserId', 22, 2, '', 0, '', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_UserId', 0, 'User', 'User', '', 'Verantwortlich', 'Usuario', 'Gebruiker', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_UserId', 22, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_UserId', 22, 2, '', 0, '', '', '', '', '', '');
  }
  
  customFileName = EntityName + 'User.asp';
  customGridName = EntityName + 'UserGrid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
  
  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'ToDo.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'ToDo.asp', entityDir + customFileName);

  
    //add the custom tab to the User Tab Entity
    AddCustom_Tabs(0, 0, 20, 'User', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
  
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);


    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
  }
}

/////////////////////////////////////
//if we're adding it to To Channels//
/////////////////////////////////////

if( Channels == 'on' ) 
{
  //add a foreign key to the channels table
  AddColumn(EntityName, ColPrefix + "_ChannelId", 23, 4, true, false);
  AddCustom_Edits(EntityName, ColPrefix + '_ChannelId', 23, 3, '', 0, '', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_ChannelId', 0, 'Team', 'Team', '', '', '', '', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + "_ChannelId", 23, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_ChannelId', 23, 3, '', 0, '', '', '', '', '', '');
  }
  
  customFileName = EntityName + 'Channel.asp';
  customGridName = EntityName + 'ChannelGrid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
  
  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }

  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'Channel.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'Channel.asp', entityDir + customFileName);
  
    //add the custom tab to the Channel Tab Entity
    AddCustom_Tabs(0, 0, 20, 'Channel', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
  
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
  }
}

//////////////////////////////
//if it's owned by companies//
//////////////////////////////

if( OwnedByCompanies == 'on' )
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_CompanyId', 56, 4, true, false);
  RunSql("UPDATE Custom_Tables SET Bord_CompanyUpdateFieldName=N'"+ColPrefix+"_CompanyId' WHERE Bord_Name = N'"+EntityName+"'");
  AddCustom_Edits(EntityName, ColPrefix + '_CompanyId', 56, 20, '', Size_Of_ASS, 'Company', '', '', '', '', '');

  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_CompanyId', 0, 'Company', 'Company', '', 'Firma', 'Compañía', 'Bedrijf', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_CompanyId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_CompanyId', 56, 20, '', Size_Of_ASS, 'Company', '', '', '', '', '');
  }

  //Set view field value for advanced search select


  AddCustom_Data('Custom_Edits','ColP','ColP_ColPropsId','ColP_ColName,ColP_SSViewField','Test_CompanyId,",comp_name,"','1');
//  AddCustom_Data('Custom_Edits','ColP','ColP_ColPropsId','ColP_ColName,ColP_SSViewField',ColPrefix+'_CompanyId,",comp_name,"','1');
  
  

customFileName = 'Company' + EntityName + '.asp';
  customGridName = 'Company' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
     
  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }

  if (!ForDotNet) 
  {  

    //makes a renamed copy of the generic asp that we can replace params into
    //CopyFile(customPagesDir + 'OwnedByCompany.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'OwnedByCompany.asp', entityDir + customFileName);
     
    //add the custom tab to the Company Tab Group
    AddCustom_Tabs(0, 0, 20, 'Company', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
  } 

  //add this to the top content
  AddCustom_Screens(EntityName+'TopContent','2',ColPrefix+'_CompanyId','0','0','0','',0,'','','','company');

}

///////////////////////////
//if it's owned by people//
///////////////////////////

if( OwnedByPeople == 'on' )
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_PersonId', 56, 4, true, false);
  RunSql("UPDATE Custom_Tables SET Bord_PersonUpdateFieldName=N'"+ColPrefix+"_PersonId' WHERE Bord_Name = N'"+EntityName+"'");
  AddCustom_Edits(EntityName, ColPrefix + '_PersonId', 56, 20, '', Size_Of_ASS, 'Person', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_PersonId', 0, 'Person', 'Person', '', 'Person', 'Persona', 'Persoon', '');
  AddCustom_Captions('Choices', 'MergePerson', EntityName, 70, 'Merge ' + EntityName, 'Merge ' + EntityName, '', '', '', '', '');

  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_PersonId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_PersonId', 56, 20, '', Size_Of_ASS, 'Person', '', '', '', '', '');
  }  

  //Set view field value for advanced search select
  AddCustom_Data('Custom_Edits','ColP','ColP_ColPropsId','ColP_ColName,ColP_SSViewField',ColPrefix+'_PersonId,",pers_fullname,"','1');
  if (OwnedByAccounts == 'on') { //restrict to only people within the account
     AddCustom_Data('Custom_Edits','ColP','ColP_ColPropsId','ColP_ColName,ColP_Restricted',ColPrefix+'_PersonId,'+ColPrefix+'_AccountId','1');   
  } 
  else if (OwnedByCompanies == 'on') { //restrict to only people within the company
     AddCustom_Data('Custom_Edits','ColP','ColP_ColPropsId','ColP_ColName,ColP_Restricted',ColPrefix+'_PersonId,'+ColPrefix+'_CompanyId','1');   
  } 

  customFileName = 'Person' + EntityName + '.asp';
  customGridName = 'Person' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
  
  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }

  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp that we can replace params into
    //CopyFile(customPagesDir + 'OwnedByPerson.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'OwnedByPerson.asp', entityDir + customFileName);
     
    //add the custom tab to the Person Tab Group
    AddCustom_Tabs(0, 0, 20, 'Person', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
  }
  //add this to the top content
  AddCustom_Screens(EntityName+'TopContent','2',ColPrefix+'_PersonId','0','0','0','',0,'','','','person');

}

////////////////////////////////
//if it's owned by Opportunity//
////////////////////////////////

if( OwnedByOpportunity == 'on' )
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_OpportunityId', 56, 4, true, false);
  AddCustom_Edits(EntityName, ColPrefix + '_OpportunityId', 56, 0, '', Size_Of_ASS, 'Opportunity', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_OpportunityId', 0, 'Opportunity', 'Opportunity', 'l\'Occasion', 'Potential', 'Oportunidad', 'Kans', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_OpportunityId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_OpportunityId', 56, 0, '', Size_Of_ASS, 'Opportunity', '', '', '', '', '');
  }
  
  customFileName = 'Opportunity' + EntityName + '.asp';
  customGridName = 'Opportunity' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
  
  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }
  
  if (!ForDotNet) 
  {       
    //makes a renamed copy of the generic asp that we can replace params into
    //CopyFile(customPagesDir + 'OwnedByOpportunity.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'OwnedByOpportunity.asp', entityDir + customFileName);    
 
    //add the custom tab to the Opportunity Tab Group
    AddCustom_Tabs(0, 0, 20, 'Opportunity', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
   }
}

////////////////////////////////
//if it's owned by Orders
////////////////////////////////

if( OwnedByOrders == 'on' ) 
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_OrderId', 56, 4, true, false);
  AddCustom_Edits(EntityName, ColPrefix + '_OrderId', 56, 0, '', Size_Of_ASS, 'Orders', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_OrderId', 0, 'Order', 'Order', '', '', '', '', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_OrderId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_OrderId', 56, 0, '', Size_Of_ASS, 'Orders', '', '', '', '', '');
  }
  
  customFileName = 'Order' + EntityName + '.asp';
  customGridName = 'Order' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
  
  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }
  
  if (!ForDotNet) 
  {       
    //makes a renamed copy of the generic asp that we can replace params into 
    CopyFile(customPagesDir + 'OwnedByOrder.asp', entityDir + customFileName);    
 
    //add the custom tab to the Order Tab Group
    AddCustom_Tabs(0, 0, 20, 'Order', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
   }
}

////////////////////////////////
//if it's owned by Quotes//
////////////////////////////////

if( OwnedByQuotes == 'on' ) 
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_QuoteId', 56, 4, true, false);
  AddCustom_Edits(EntityName, ColPrefix + '_QuoteId', 56, 0, '', Size_Of_ASS, 'Quotes', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_QuoteId', 0, 'Quote', 'Quote', '', '', '', '', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_QuoteId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_QuoteId', 56, 0, '', Size_Of_ASS, 'Quotes', '', '', '', '', '');
  }
  
  customFileName = 'Quote' + EntityName + '.asp';
  customGridName = 'Quote' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
  
  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }

  if (!ForDotNet) 
  {       
    //makes a renamed copy of the generic asp that we can replace params into 
    CopyFile(customPagesDir + 'OwnedByQuote.asp', entityDir + customFileName);    
 
    //add the custom tab to the Quote Tab Group
    AddCustom_Tabs(0, 0, 20, 'Quote', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
   }
}


//////////////////////////
//if it's owned by Leads//
//////////////////////////

if( OwnedByLeads == 'on' )
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_LeadId', 56, 4, true, false);
  AddCustom_Edits(EntityName, ColPrefix + '_LeadId', 56, 0, '', Size_Of_ASS, 'Lead', '', '', '', '', '');  
  FamilyType='Choices';
  Family='ss_entities';
  Code='Lead';
  Captions['US']='Lead';
  AddCaption();

  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_LeadId', 0, 'Lead', 'Lead', '', 'Nicht Qualifiziert', 'Mando', 'Lead', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_LeadId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_LeadId', 56, 0, '', Size_Of_ASS, 'Lead', '', '', '', '', '');  
  }
  
  customFileName = 'Lead' + EntityName + '.asp';
  customGridName = 'Lead' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 

  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }

  if (!ForDotNet) 
  {    
    //makes a renamed copy of the generic asp that we can replace params into
    //CopyFile(customPagesDir + 'OwnedByLead.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'OwnedByLead.asp', entityDir + customFileName);
     
    //add the custom tab to the Lead Tab Group
    AddCustom_Tabs(0, 0, 20, 'Lead', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');
  }
}

//////////////////////////
//if it's owned by Cases//
//////////////////////////

if( OwnedByCases == 'on' )
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_CaseId', 56, 4, true, false);
  AddCustom_Edits(EntityName, ColPrefix + '_CaseId', 56, 0, '', Size_Of_ASS, 'Case', '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_CaseId', 0, 'Case', 'Case', 'Case', 'Ereignis', 'Caso', 'Zaak', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_CaseId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_CaseId', 56, 0, '', Size_Of_ASS, 'Case', '', '', '', '', '');
  }
  
  customFileName = 'Case' + EntityName + '.asp';
  customGridName = 'Case' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 

  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }

  if (!ForDotNet) 
  {    
    //makes a renamed copy of the generic asp that we can replace params into
    //CopyFile(customPagesDir + 'OwnedByCase.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'OwnedByCase.asp', entityDir + customFileName);
     
    //add the custom tab to the Lead Tab Group
    AddCustom_Tabs(0, 0, 20, 'Case', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');  
  }
}

//////////////////////////
//if it's owned by Accounts//
//////////////////////////

if( OwnedByAccounts == 'on' )
{
  //add a foreign key in the company table
  AddColumn(EntityName, ColPrefix + '_AccountId', 56, 4, true, false);
  RunSql("UPDATE Custom_Tables SET Bord_AccountUpdateFieldName=N'"+ColPrefix+"_AccountId' WHERE Bord_Name = N'"+EntityName+"'");
  AddCustom_Edits(EntityName, ColPrefix + '_AccountId', 56, 0, '', Size_Of_ASS, 'Account', '', '', '', '', '');
  if (OwnedByCompanies == 'on') { //restrict to only accounts within the company
     AddCustom_Data('Custom_Edits','ColP','ColP_ColPropsId','ColP_ColName,ColP_Restricted',ColPrefix+'_AccountId,'+ColPrefix+'_CompanyId','1');   
  } 

  AddCustom_Captions('Tags', 'ColNames', ColPrefix + '_AccountId', 0, 'Account', 'Account', 'Account', 'Account', 'Account', 'Account', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_AccountId', 56, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_AccountId', 56, 0, '', Size_Of_ASS, 'Account', '', '', '', '', '');
  }
  
  customFileName = 'Account' + EntityName + '.asp';
  customGridName = 'Account' + EntityName + 'Grid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 

  if (ForDotNet)
  {
    RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
  }

  if (!ForDotNet) 
  {    
    //makes a renamed copy of the generic asp that we can replace params into
    CopyFile(customPagesDir + 'OwnedByAccount.asp', entityDir + customFileName);
     
    //add the custom tab to the Lead Tab Group
    AddCustom_Tabs(0, 0, 20, 'Account', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&customGridName&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&newTabASP&**', EntityName + '/' + newTabASP);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, '**&bWorkflow&**', 'false');  
  }
}


///////////////////////
//if it has companies//
///////////////////////

if( HasCompanies == 'on' )
{
  foreignkey = 'Comp_' + EntityName + 'Id';
  
  //add a foreign key to the company table
  AddColumn('Company', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Company', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
  
  //add it to the company entry box
  AddCustom_Screens('CompanyBoxLong', 20, foreignkey, 0, 1, 1, 'N');
  
  //add it to the top content
  AddCustom_ScreenObjects('CompanyTopContent','Screen','Company','N','0','Company','','','');
  AddCustom_Screens('CompanyTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);

  
  customFileName = EntityName + 'Company.asp';
  customGridName = 'CompanyGrid';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'HasCompanies.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'HasCompanies.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 2, tabgroup, 'Companies', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'company',Tabs_OnlineOnly);
        
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
  }

  //Make an asp page that allows user to search for companies to link to the entity
  AddCustom_Captions('Tags','Colnames','SearchCompany',0,'Search for company', '', '', '', '', '', '');

  if (!ForDotNet) 
  {   
    customFileName = EntityName+'CompanyLink.asp';
    CopyFile(customPagesDir + 'HasCompaniesLink.asp', entityDir + customFileName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    CopyAspTo('\\CompLink.gif', '\\themes\\img\\default\\buttons\\CompLink.gif');
  }
  
} 

////////////////////
//if it has people//
////////////////////

if( HasPeople == 'on' )
{
  foreignkey = 'Pers_' + EntityName + 'Id';

  //add a foreign key to the people table
  AddColumn('Person', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Person', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);  
  
  //add it to the person entry screen
  AddCustom_Screens('PersonBoxLong', 20, foreignkey, 0, 1, 1, 'N');
  
  //add it to the top content
  AddCustom_ScreenObjects('PersonTopContent','Screen','Person','N','0','Person','','',''); 
  AddCustom_Screens('PersTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);

  customFileName = EntityName + 'Person.asp';
  customGridName = 'PersonListBlock';

  if (!ForDotNet) 
  {    
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'HasPeople.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'HasPeople.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 3, tabgroup, 'People', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'person',Tabs_OnlineOnly);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
  }

  //Make an asp page that allows user to search for people to link to the entity
  AddCustom_Captions('Tags','Colnames','SearchPerson',0,'Search for person', '', '', '', '', '', '');

  if (!ForDotNet) 
  {  
    customFileName = EntityName+'PersonLink.asp';
    CopyFile(customPagesDir + 'HasPeopleLink.asp', entityDir + customFileName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    CopyAspTo('\\PersLink.gif', '\\themes\\img\\default\\buttons\\PersLink.gif');
  }
  
} 

///////////////////
//if it has cases//
///////////////////

if( HasCases == 'on' )
{
  foreignkey = 'Case_' + EntityName + 'Id';
  
  //add a foreign key to the cases table
  AddColumn('Cases', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Cases', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
    
  //add it to the cases entry screen
  AddCustom_Screens('CaseDetailBox', 20, foreignkey, 0, 1, 1, 'N');
  
  //add it to the top content
  AddCustom_ScreenObjects('CaseTopContent','Screen','Cases','N','0','Cases','','',''); 
  AddCustom_Screens('CaseTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);
  
  customFileName = EntityName + 'Case.asp';
  customGridName = 'CaseListBlock';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'HasCases.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'HasCases.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 4, tabgroup, 'Cases', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'cases',Tabs_OnlineOnly);
    SetValueInFile(OwnedByCompanies,'**&bCompany&**',entityDir + customFileName);
    SetValueInFile(OwnedByPeople,'**&bPerson&**',entityDir + customFileName);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
  }
  
} 

if( HasAccounts == 'on' )
{
  foreignkey = 'Acc_' + EntityName + 'Id';
  
  //add a foreign key to the cases table
  AddColumn('Account', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Account', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
    
  //add it to the cases entry screen
  AddCustom_Screens('AccountBoxLong', 20, foreignkey, 0, 1, 1, 'N');
  
  //add it to the top content
  AddCustom_ScreenObjects('AccountTopContent','Screen','Account','N','0','Account','','',''); 
  AddCustom_Screens('AccountTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);
  
  customFileName = EntityName + 'Account.asp';
  customGridName = 'AccountListBlock';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'HasAccounts.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 4, tabgroup, 'Account', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'account',Tabs_OnlineOnly);
    SetValueInFile(OwnedByCompanies,'**&bCompany&**',entityDir + customFileName);
    SetValueInFile(OwnedByPeople,'**&bPerson&**',entityDir + customFileName);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
  }
  
} 


////////////////////////////
//if it has communications//
////////////////////////////

if( HasCommunications == 'on' )
{
  foreignkey = 'Comm_' + EntityName + 'Id';
  
  //add a foreign key to the communications table
  AddColumn('Communication', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Communication', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
  
  //add it to the communications entry screen
  AddCustom_Screens('CustomCommunicationDetailBox', 20, foreignkey, 1, 1, 1, 'N');
    
  customFileName = EntityName + 'Communication.asp';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'HasCommunications.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'HasCommunications.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 4, tabgroup, 'Communications', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'communication',Tabs_OnlineOnly);

    SetValueInFile(OwnedByCompanies,'**&bCompany&**',entityDir + customFileName);
    SetValueInFile(OwnedByPeople,'**&bPerson&**',entityDir + customFileName);
    SetValueInFile(OwnedByOpportunity,'**&bOpportunity&**',entityDir + customFileName);
    SetValueInFile(OwnedByOrders,'**&bOrder&**',entityDir + customFileName);
    SetValueInFile(OwnedByQuotes,'**&bQuote&**',entityDir + customFileName);
    SetValueInFile(OwnedByCases,'**&bCase&**',entityDir + customFileName);
    SetValueInFile(OwnedByAccounts,'**&bAccount&**',entityDir + customFileName);

    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
  }  
}

///////////////////////////
//if it has opportunities//
///////////////////////////

if( HasOpportunities == 'on' )
{
  foreignkey = 'Oppo_' + EntityName + 'Id';

  //add a foreign key to the oppo table
  AddColumn('Opportunity', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Opportunity', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);

  //add it to the top content
  AddCustom_ScreenObjects('OppoTopContent','Screen','Opportunity','N','0','Opportunity','','',''); 
  AddCustom_Screens('OppoTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);

  //add it to the opportunity entry screen
  AddCustom_Screens('OpportunityDetailBox', 20, foreignkey, 0, 1, 1, 'N'); 
    
  customFileName = EntityName + 'Opportunity.asp';
  customGridName = 'OpportunityListBlock';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'HasOpportunities.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'HasOpportunities.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 6, tabgroup, 'Opportunities', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'opportunity',Tabs_OnlineOnly);

    SetValueInFile(OwnedByCompanies,'**&bCompany&**',entityDir + customFileName);
    SetValueInFile(OwnedByPeople,'**&bPerson&**',entityDir + customFileName);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&ColPrefix&**', ColPrefix);
  }  
} 

///////////////////
//if it has leads//
///////////////////

if( HasLeads == 'on' )
{
  foreignkey = 'Lead_' + EntityName + 'Id';

  //add a foreign key to the leads table
  AddColumn('Lead', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Lead', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
  
  //add it to the top content
  AddCustom_ScreenObjects('LeadTopContent','Screen','Lead','Y','0','Lead','','','');  
  AddCustom_Screens('LeadTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);
    
  //add it to the lead entry screen
  AddCustom_Screens('LeadCustomScreen', 20, foreignkey, 1, 1, 1, 'N'); 
    
  customFileName = EntityName + 'Lead.asp';
  customGridName = 'LeadListBlock';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'HasLeads.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'HasLeads.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 7, tabgroup, 'Leads', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'lead',Tabs_OnlineOnly);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName);
  }
  
} 

///////////////////////
//if it has a library//
///////////////////////

if( HasLibrary == 'on' )
{
  foreignkey = 'Libr_' + EntityName + 'Id';

  //add a foreign key to the library table
  AddColumn('Library', foreignkey, 56, 4, true, false);
  AddCustom_Edits('Library', foreignkey, 56, 19, '', Size_Of_ASS, EntityName, '', '', '', '', '');
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
    
  //add it to the library entry screen
  AddCustom_Screens('LibraryItemBoxLong', 20, foreignkey, 1, 1, 1, 'N'); 
    
  customFileName = EntityName + 'Library.asp';
  customGridName = 'LibraryList';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    //CopyFile(customPagesDir + 'HasLibrary.asp', customPagesDir + customFileName);
    CopyFile(customPagesDir + 'HasLibrary.asp', entityDir + customFileName);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 8, tabgroup, 'Library', 'customfile', EntityName + '\\' + customFileName);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&tabgroup&**', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&foreignkey&**', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, '**&EntityName&**', EntityName); 
  }
}

// always create the entity grid and searchbox
// this allows entrys of type searchselect for this entity

customGridName = EntityName + 'Grid';

//create the custom search list block  
AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
if (ForDotNet)
{
  RunSql("UPDATE Custom_Lists SET Grip_CustomFunction=N'"+CustomFunction+"' WHERE Grip_GridName = N'" +customGridName +"' AND Grip_ColName = N'"+nameCol+"'");
}

if( ToDo == 'on' ) 
{
  AddCustom_Lists(customGridName, 2, ColPrefix+'_UserId', '', '', '', '', '', '','', '', ''); 
}
if( Channels == 'on' ) 
{
  AddCustom_Lists(customGridName, 3, ColPrefix+'_ChannelId', '', '', '', '', '', '', '', '', ''); 
}
if( OwnedByCompanies == 'on' )
{
  AddCustom_Lists(customGridName, 4, ColPrefix+'_CompanyId', '', '', '', '', '', '', '', '', ''); 
}
if( OwnedByAccounts == 'on' )
{
  AddCustom_Lists(customGridName, 5, ColPrefix+'_AccountId', '', '', '', '', '', '', '', '', ''); 
}
if( OwnedByPeople == 'on' )
{
  AddCustom_Lists(customGridName, 6, ColPrefix+'_PersonId', '', '', '', '', '', '', '', '', ''); 
}
if( OwnedByOpportunity == 'on' )
{
  AddCustom_Lists(customGridName, 7, ColPrefix+'_OpportunityId', '', '', '', '', '', '', '', '', ''); 
}
if( OwnedByOrders == 'on' )
{
  AddCustom_Lists(customGridName, 8, ColPrefix+'_OrderId', '', '', '', '', '', '', '', '', ''); 
}
if( OwnedByQuotes == 'on' )
{
  AddCustom_Lists(customGridName, 9, ColPrefix+'_QuoteId', '', '', '', '', '', '', '', '', ''); 
}

if( OwnedByLeads == 'on' )
{
  AddCustom_Lists(customGridName, 10, ColPrefix+'_LeadId', '', '', '', '', '', '', '', '', ''); 
}
if( OwnedByCases == 'on' )
{
  AddCustom_Lists(customGridName, 11, ColPrefix+'_CaseId', '', '', '', '', '', '', '', '', ''); 
}

//create the custom search entry block    

customEntryName = EntityName + 'SearchBox';

AddCustom_ScreenObjects(customEntryName, 'SearchScreen', EntityName, 'N', 0, EntityName, '', '', '',customGridName);   
AddCustom_Screens(customEntryName, 1, nameCol, 0, 1, 1, 'N');
if( ToDo == 'on' ) 
{
  AddCustom_Screens(customEntryName, 1, ColPrefix+'_UserId', 0, 1, 1, 'N');
}
if( Channels == 'on' ) 
{
  AddCustom_Screens(customEntryName, 2, ColPrefix+'_ChannelId', 0, 1, 1, 'N');
}
if( OwnedByCompanies == 'on' )
{
  AddCustom_Screens(customEntryName, 3, ColPrefix+'_CompanyId', 0, 1, 1, 'N');
}
if( OwnedByAccounts == 'on' )
{
  AddCustom_Screens(customEntryName, 4, ColPrefix+'_AccountId', 0, 1, 1, 'N');
}
if( OwnedByPeople == 'on' )
{
  AddCustom_Screens(customEntryName, 5, ColPrefix+'_PersonId', 1, 1, 1, 'N');
}
if( OwnedByOpportunity == 'on' )
{
  AddCustom_Screens(customEntryName, 6, ColPrefix+'_OpportunityId', 0, 1, 1, 'N');
}
if( OwnedByOrders == 'on' )
{
  AddCustom_Screens(customEntryName, 7, ColPrefix+'_OrderId', 0, 1, 1, 'N');
}
if( OwnedByQuotes == 'on' )
{
  AddCustom_Screens(customEntryName, 8, ColPrefix+'_QuoteId', 0, 1, 1, 'N');
}
if( OwnedByLeads == 'on' )
{
  AddCustom_Screens(customEntryName, 9, ColPrefix+'_LeadId', 0, 1, 1, 'N');
}
if( OwnedByCases == 'on' )
{
  AddCustom_Screens(customEntryName, 10, ColPrefix+'_CaseId', 0, 1, 1, 'N');
}


if (ForDotNet) {
  var FilterName = EntityName + 'FilterBox';
  AddCustom_ScreenObjects(FilterName, 'SearchScreen', EntityName, 'N', 0, EntityName, '', '', '','');   
  AddCustom_Screens(FilterName, 1, nameCol, 0, 1, 1, 'N');
  RunSql("UPDATE Custom_Tables SET Bord_ViewFileName=N'"+ EntityName +"', Bord_ViewAction = '"+CustomFunction+"', Bord_EntityKind = 'dotnet' WHERE Bord_Name = N'"+EntityName+"'");
}

if (!ForDotNet) {
  RunSql("UPDATE Custom_Tables SET Bord_ViewFileName=N'"+ EntityName + '/' + customSummaryPage +"', Bord_EntityKind = 'asp' WHERE Bord_Name = N'"+EntityName+"'");
}


/////////////////////////////////
//if we're adding it to To Find//
/////////////////////////////////

if( Find == 'on' )
{
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the find custom asp file
    CopyFile(customPagesDir + 'Find.asp', entityDir + findASP);

    //add the custom tab to the Find Tab Entity
    AddCustom_Tabs(0, 0, 20, 'Find', EntityName, 'customfile', EntityName + '\\' + findASP, '', Icon, 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + findASP, '**&searchEntry&**', customEntryName);
    SearchAndReplaceInCustomFile(entityDir + findASP, '**&searchList&**', customGridName);
    SearchAndReplaceInCustomFile(entityDir + findASP, '**&EntityName&**', EntityName);
  }
}

  

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//
//                                              if we're adding it to To New//
//
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

if( New == 'on' )
{
  newASP = EntityName + 'New.asp';
  newTabASP = newASP;
  
  customEntryName = EntityName + 'NewEntry';

  //create the custom entry screen  
  AddCustom_ScreenObjects(customEntryName, 'Screen', EntityName, 'N', 0, EntityName, '', '', '');   
  AddCustom_Screens(customEntryName, 1, nameCol, 0, 1, 1, 'N');
  
  if( ToDo == 'on' ) 
  {
    AddCustom_Screens(customEntryName, 2, ColPrefix+'_UserId', 0, 1, 1, 'N');
  }
  if( Channels == 'on' ) 
  {
    AddCustom_Screens(customEntryName, 3, ColPrefix+'_ChannelId', 0, 1, 1, 'N');
  }
  if( OwnedByCompanies == 'on' )
  {
    AddCustom_Screens(customEntryName, 4, ColPrefix+'_CompanyId', 0, 1, 1, 'N');
  }
  if( OwnedByAccounts == 'on' )
  {
    AddCustom_Screens(customEntryName, 5, ColPrefix+'_AccountId', 0, 1, 1, 'N');
  }
  if( OwnedByPeople == 'on' )
  {
    AddCustom_Screens(customEntryName, 6, ColPrefix+'_PersonId', 1, 1, 1, 'N');
  }
  if( OwnedByOpportunity == 'on' )
  {
    AddCustom_Screens(customEntryName, 7, ColPrefix+'_OpportunityId', 0, 1, 1, 'N');
  }
  if( OwnedByOrders == 'on' )
  {
    AddCustom_Screens(customEntryName, 8, ColPrefix+'_OrderId', 0, 1, 1, 'N');
  }
  if( OwnedByQuotes == 'on' )
  {
    AddCustom_Screens(customEntryName, 9, ColPrefix+'_QuoteId', 0, 1, 1, 'N');
  }
  if( OwnedByLeads == 'on' )
  {
    AddCustom_Screens(customEntryName, 10, ColPrefix+'_LeadId', 0, 1, 1, 'N');
  }
  if( OwnedByCases == 'on' )
  {
    AddCustom_Screens(customEntryName, 11, ColPrefix+'_CaseId', 0, 1, 1, 'N');
  }
  
  //////////////////////////////////////////
  //if we want Deduplication on the entity//
  //////////////////////////////////////////
  
  if( Deduplication == 'on' )
  {
    dedupeASP = EntityName + 'Dedupe.asp';
    conflictASP = EntityName + "Conflict.asp";
    newTabASP = dedupeASP;
    dedupeBoxName = EntityName + 'BoxDedupe';
    
    //create the dedupe entry box
    AddCustom_ScreenObjects(dedupeBoxName, 'Screen', EntityName, 'N', 0, EntityName, '', '', '');
    AddCustom_Screens(dedupeBoxName, 1, nameCol, false, 1, 1, 'Y', 0, '', '', '', '');

    // add default match rule
    AddCustom_Data("MatchRules","MaRu","MaRu_MatchRulesID",
      "MaRu_Tablename,MaRu_Fieldname,MaRu_MatchType",
      EntityName+","+nameCol+",Contains",
      "1,2");

    //create the custom caption that indicates this entity can have matchrules
    AddCustom_Captions('Choices','MatchRulesAllowed',EntityName,'0','true','true','true','true','true','true','true');

    if (!ForDotNet) 
    {      
      //dedupe asp
      CopyFile(customPagesDir + 'Dedupe.asp', entityDir + dedupeASP);

      //substitute in params for the custom file
      SearchAndReplaceInCustomFile(entityDir + dedupeASP, '**&EntityName&**', EntityName);
      SearchAndReplaceInCustomFile(entityDir + dedupeASP, '**&newASP&**', EntityName + '/' + newASP);
      SearchAndReplaceInCustomFile(entityDir + dedupeASP, '**&conflictASP&**', EntityName + '/' + conflictASP);
      SearchAndReplaceInCustomFile(entityDir + dedupeASP, '**&dedupeBoxName&**', dedupeBoxName);
    
      //conflict asp
      CopyFile(customPagesDir + 'Conflict.asp', entityDir + conflictASP);

      SearchAndReplaceInCustomFile(entityDir + conflictASP, '**&customGridName&**', customGridName);
      SearchAndReplaceInCustomFile(entityDir + conflictASP, '**&EntityName&**', EntityName);
      SearchAndReplaceInCustomFile(entityDir + conflictASP, '**&newASP&**', EntityName + '/' + newASP);
      SearchAndReplaceInCustomFile(entityDir + conflictASP, '**&dedupeASP&**', EntityName + '/' + dedupeASP);
    }
    
    //add captions for conflict screen
    AddCustom_Captions( 'Tags','GenCaptions','Dedupe'+EntityName,'1','Attention: You may be entering a '+EntityName+' record that already exists in the database. To use one of the existing '+EntityName+ ' records, click on the '+EntityName+' hyperlink', '', '', '', '', '', '');
    AddCustom_Captions( 'Tags','GenCaptions','DedupeIgnore'+EntityName,'1','Ignore Warning and Enter '+EntityName, '', '', '', '', '', '');
    AddCustom_Captions( 'Tags','GenCaptions','DedupeBack'+EntityName,'1','Back to '+EntityName+ ' Entry', '', '', '', '', '', '');

  }


  //Set up the offline options if they are selected
  if (Bord_SoloOptions != '') {
     
     //set the value into custom_tables
     AddCustom_Data("Custom_Tables","Bord","Bord_TableId","Bord_Name,Bord_SoloOptions",EntityName+","+Bord_SoloOptions,
                     "1");    

     if (WorkflowProgress == 'on')  {
        //set the options for the progress table
        if (Bord_SoloOptions != '1') {
           AddCustom_Data("Custom_Tables","Bord","Bord_TableId",
                          "Bord_Name,Bord_SoloOptions",
                          ProgressTable+","+Bord_SoloOptions,
                          "1"); 
        }
        else {
           //main table is set to filter to client, progress table should be filtered based on main table
           // set solo options for progress table = 3 (eware_special)
           AddCustom_Data("Custom_Tables","Bord","Bord_TableId",
                          "Bord_Name,Bord_SoloOptions",
                          ProgressTable+",3",
                          "1"); 
           //add record to entitylinks to make the progress table filter from main table
           AddCustom_Data("Rep_EntityLinks","RLink","RLink_EntityLinkId",
                          "RLink_Parent,RLink_Child,RLink_LinkField,RLink_LinkIDField",
                          EntityName+","+ProgressTable+","+idfield+","+idfield,
                          "1,2");                                                                              
        }
                              
     }    

                                               
  } 


  //Set up the offline Date options if they are selected
  if (Bord_SoloDateOptions != '') {
     
     //set the value into custom_tables
     AddCustom_Data("Custom_Tables","Bord","Bord_TableId","Bord_Name,Bord_SoloDateOptions",EntityName+","+Bord_SoloDateOptions,
                     "1");                                               
  } 

  //set up the web service access if selected
  if (IsWebServiceTable != '') {
     //set the value into custom_tables
     AddCustom_Data("Custom_Tables","Bord","Bord_TableId","Bord_Name,Bord_WebServiceTable",EntityName+","+'Y',
                     "1"); 

  }

  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the new custom asp file
    CopyFile(customPagesDir + 'New.asp', entityDir + newASP);
  
    //add the custom tab to the New Tab Entity
    AddCustom_Tabs(0, 0, 20, 'New', EntityName, 'customfile', EntityName + '\\' + newTabASP, '', Icon, 0, EntityName,Tabs_OnlineOnly);

    SetValueInFile(OwnedByCompanies, '**&bCompany&**', entityDir + newASP);
    SetValueInFile(OwnedByPeople, '**&bPerson&**', entityDir + newASP);
    SetValueInFile(OwnedByOpportunity, '**&bOpportunity&**', entityDir + newASP);
    SetValueInFile(OwnedByOrders, '**&bOrder&**', entityDir + newASP);
    SetValueInFile(OwnedByQuotes, '**&bQuote&**', entityDir + newASP);
    SetValueInFile(OwnedByLeads, '**&bLead&**', entityDir + newASP);
    SetValueInFile(OwnedByCases, '**&bCase&**', entityDir + newASP);
    SetValueInFile(OwnedByAccounts, '**&bAccount&**', entityDir + newASP);
    SetValueInFile(Channels, '**&bChannel&**', entityDir + newASP);
    SetValueInFile(ToDo, '**&bUser&**', entityDir + newASP);
    SetValueInFile(Deduplication, '**&bDedupe&**', entityDir + newASP);
      
    
    SearchAndReplaceInCustomFile(entityDir + newASP, '**&EntityNamefind.asp&**', EntityName + '/' + findASP);
    SearchAndReplaceInCustomFile(entityDir + newASP, '**&EntityName&**', EntityName);
    SearchAndReplaceInCustomFile(entityDir + newASP, '**&searchEntry&**', customEntryName);
    SearchAndReplaceInCustomFile(entityDir + newASP, '**&idfield&**', idfield);
    SearchAndReplaceInCustomFile(entityDir + newASP, '**&summaryASP&**', customSummaryPage);//sean
    SearchAndReplaceInCustomFile(entityDir + newASP, '**&ColPrefix&**', ColPrefix);
    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + newASP, '**&bWorkflow&**', 'true');
    else
      SearchAndReplaceInCustomFile(entityDir + newASP, '**&bWorkflow&**', 'false');
  }

}

//substitute in params for the summary page
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&SummaryEntry&**', customEntryName);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&tabgroup&**', tabgroup);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&EntityName&**', EntityName);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&idfield&**', idfield);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&newASP&**', newASP);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&findASP&**', EntityName + '/' + findASP);


if( Workflow == 'on' )
  SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&bWorkflow&**', 'true');
else
  SearchAndReplaceInCustomFile(entityDir + customSummaryPage, '**&bWorkflow&**', 'false');

CopyASPTo('crmconst.js','custompages\\crmconst.js');
CopyASPTo('crmwizard.js','custompages\\crmwizard.js');
CopyASPTo('crmwizardnolang.js','custompages\\crmwizardnolang.js');

//AddMessage('<BR><H2>PLEASE RESTART IIS TO COMPLETE THE INSTALLATION</H2>');
//AddMessage('PLEASE RESTART IIS TO COMPLETE THE INSTALLATION');

} //closing bracket for: if( valid ){


//Related Entities Relationships tab
var TabsId = AddCustom_Tabs(0,0,21,tabgroup,'Relationships','customdotnetdll','RelatedEntities','','',0,'',true);
AddCustom_Data('Custom_Tabs','Tabs','Tabs_TabId','Tabs_TabId,Tabs_NewWindow,Tabs_CustomFunction',TabsId+',"","RunREList"','1');


function isOkChar(sChar) {
  var okChar = (((sChar >= 'a') && (sChar <= 'z'))  
                ||  ((sChar >= 'A') && (sChar <= 'Z')));
  return (okChar);
}

function SetValueInFile(bValue,sFieldName, sFileName) {

  //substitute in params for the custom file
  if( bValue == 'on' ) 
    SearchAndReplaceInCustomFile(sFileName, sFieldName, 'true');
  else
    SearchAndReplaceInCustomFile(sFileName, sFieldName, 'false');

}