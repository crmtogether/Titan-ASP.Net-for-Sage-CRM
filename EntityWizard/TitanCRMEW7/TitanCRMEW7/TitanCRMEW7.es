/////////////////////////////////
//collect/initialize parameters//
/////////////////////////////////
var EntityName = Param('ENTITY NAME');
var ColPrefix = Param('ENTITY COLUMN PREFIX');
var ToDo = Param('ADD TO TODO?');
var Find = Param('ADD TO FIND?');
var New = 'on';
var ForDotNet=false;

var HasCompanies = Param('HAS COMPANIES?');
var HasPeople = Param('HAS PEOPLE?');
var HasCommunications = Param('HAS COMMUNICATIONS?');
var HasOpportunities = Param('HAS OPPORTUNITIES?');
var HasLeads = Param('HAS LEADS?');
var HasCases = Param('HAS CASES?');
var HasCommunications = Param('HAS COMMUNICATIONS?');
var HasLibrary = Param('HAS LIBRARY?');

var Workflow = Param('WORKFLOW?');
var WorkflowProgress = Param('HAS WORKFLOW PROGRESS?');

var IsWebServiceTable = Param('WSDL');

var ProgressNote = '';
var ProgressTable = '';
var sSpacer = '&nbsp&nbsp&nbsp&nbsp';

var Tabs_OnlineOnly = true;

var Size_Of_ASS = 25;

var Valid_CHARS="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

var type_Reference = 'reference';
var type_Association = 'association';
var type_Parent = 'parent';
var type_Child = 'child';

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
var customSummaryPage = 'Summary.aspx';
var customSummaryPageCS = 'Summary.aspx.cs';

Grip_Jump = 'custom';

CreateNewDir(GetDLLDir() + '\\CustomPages\\' + EntityName);
CreateNewDir(GetDLLDir() + '\\CustomPages\\' + EntityName+'\\App_Data');
CreateNewDir(GetDLLDir() + '\\CustomPages\\' + EntityName+'\\Bin');

CopyASPTo('\\entwiz\\Bin\\SageCRM.dll','\\CustomPages\\'+EntityName+'\\Bin\\SageCRM.dll');
CopyASPTo('\\entwiz\\Bin\\SageCRM.dll','\\CustomPages\\'+EntityName+'\\Bin\\SageCRM.dll.refresh');

CopyASPTo('\\entwiz\\eware.css','\\CustomPages\\'+EntityName+'\\eware.css');
CopyASPTo('\\entwiz\\color1.css','\\CustomPages\\'+EntityName+'\\color1.css');
CopyASPTo('\\entwiz\\web.config','\\CustomPages\\'+EntityName+'\\web.config');
CopyASPTo('\\entwiz\\crmclient.js','\\CustomPages\\'+EntityName+'\\crmclient.js');

JumpIdField = idfield;
JumpSummaryPage = EntityName + '\\' + customSummaryPage

var Icon = EntityName + '.gif';
var DLLDir = GetDLLDir();
var InstallDir = GetInstallDir();
var componentDir = InstallDir + '\\inf\\TitanCRMEW7\\';
var customPagesDir = componentDir + '\\CustomPages\\entwiz\\';
var entityDir = DLLDir + '\\CustomPages\\' + EntityName + '\\';
var userIDCol = false;
var customFileName;
var customGridName;
var customEntryName;
var tabgroup;
var foreignkey;
var nameCol;
var newASP;
var newASPCS;
newASP = 'NewEntity.aspx';
newASPCS = 'NewEntity.aspx.cs';
var findASP;
var findASPCS;
var conflictASP;

var newTabASP;
newTabASP = newASP;

customEntryName = EntityName + 'SummaryScreen';
channelIDCol=false;
userIDCol=false;
ProgressTable='';
tabgroup=EntityName;
nameCol = ColPrefix + '_Name';

CopyAspTo('\\EntityIcon.gif', '\\Themes\\img\\color\\icons\\' + EntityName + '.gif');
CopyAspTo('\\SmallEntityIcon.gif', '\\Themes\\img\\color\\icons\\small_' + EntityName + '.gif');
CopyAspTo('\\SummaryEntityIcon.gif', '\\Themes\\img\\color\\icons\\summary\\small_' + EntityName + '.gif');
CopyAspTo('\\RelatedEntityIcon.gif', '\\Themes\\img\\color\\icons\\related_' + EntityName + '.gif');

AddMessage(GetTrans('EntityWizard','tocreatecustomentity') +
    DLLDir + '\\Themes\\img\\color\\icons\\' + EntityName + '.gif, small_'+EntityName+'.gif and related_'+EntityName+'.gif');

/////////////
//table
/////////////
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
  customFileName = 'Wf.aspx';
  customFileNameCS = 'Wf.aspx.cs';
  
  //add the E parameter to specify the entity for top frame tab silent action
  CreateNewWorkflow( EntityName + ' Workflow', false, EntityName, EntityName+'\\'+customFileName + '?E=' + EntityName );
  workflowidfield = true;

  //makes a renamed copy of the generic asp to be used for the todo custom asp file
  if (!ForDotNet)
  { 
    CopyFile(customPagesDir + 'Wf.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'Wf.aspx.cs', entityDir + customFileNameCS);
  
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'entwiz', EntityName);     
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxxNewEntry', EntityName + 'NewEntry');
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'entwiz', EntityName);     
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxxNewEntry', EntityName + 'NewEntry');
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);
    

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
  ProgressListASP = 'ProgressList.aspx';
  ProgressListASPCS = 'ProgressList.aspx.cs';
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
    CopyFile(customPagesDir + 'ProgressList.aspx', entityDir + ProgressListASP);
    CopyFile(customPagesDir + 'ProgressList.aspx.cs', entityDir + ProgressListASPCS);

    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, 'xxxxxxxxxProgressList', ProgressListName);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASP, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + ProgressListASPCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASPCS, 'xxxxxxxxxProgressList', ProgressListName);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASPCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASPCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + ProgressListASPCS, 'xxxx', ColPrefix);

  }
}


if (workflowidfield) {
   AddCustom_Edits(EntityName, ColPrefix+'_WorkflowId', 31, 0, '', 0, '', '', '', '', '', 'Y');
   AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_WorkflowId', 1, ColPrefix+'_WorkflowId', '', '', '', '', '', '');
}

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

findASP = 'FindEntity.aspx';
findASPCS = 'FindEntity.aspx.cs';

//create the custom summary screen  
AddCustom_ScreenObjects(customEntryName, 'Screen', EntityName, 'N', 0, EntityName, '', '', '');   

if (!ForDotNet) 
{  
  //makes a renamed copy of the generic asp summary screen
  CopyFile(customPagesDir + 'Summary.aspx', entityDir + customSummaryPage);
  CopyFile(customPagesDir + 'Summary.aspx.cs', entityDir + customSummaryPageCS);

  SearchAndReplaceInCustomFile(entityDir + 'Summary.aspx', 'xxxx_xxxxxxxxxid', idfield);
  SearchAndReplaceInCustomFile(entityDir + 'Summary.aspx.cs', 'xxxx_xxxxxxxxxid', idfield);

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
// Notes relationships
AddCustom_Relationship(EntityName, idfield, 'Notes', 'note_foreignid', type_Child, true);
AddCustom_Relationship('Notes', 'note_foreignid', EntityName, idfield, type_Parent, false);


///////////////////////////////
//if we're adding it to To Do//
///////////////////////////////

if( ToDo == 'on' ) 
{
  //add a foreign key to the user table
  AddColumn(EntityName, ColPrefix + '_UserId', 22, 4, true, false);
  RunSql("UPDATE Custom_Tables SET Bord_AssignedUserId=N'"+ColPrefix+"_UserId' WHERE Bord_Name = N'"+EntityName+"'");

  AddCustom_Edits(EntityName, ColPrefix + '_UserId', 22, 2, '', 0, '', '', '', '', '', '');
  AddCustom_Relationship(EntityName, ColPrefix + '_UserId', 'Users', 'User_UserId', type_Reference, false);
  AddCustom_Relationship('Users', 'User_UserId', EntityName, ColPrefix + '_UserId', type_Association, true);  
  AddCustom_Captions('Tags', 'ColNames', ColPrefix+'_UserId', 0, 'User', 'User', '', 'Verantwortlich', 'Usuario', 'Gebruiker', '');
  if( WorkflowProgress == 'on' )
  {
    AddColumn(ProgressTable, ColPrefix + '_UserId', 22, 4, true, false);
    AddCustom_Edits(ProgressTable, ColPrefix + '_UserId', 22, 2, '', 0, '', '', '', '', '', '');
    AddCustom_Relationship(ProgressTable, ColPrefix + '_UserId', 'Users', 'User_UserId', type_Reference, false);
    AddCustom_Relationship('Users', 'User_UserId', ProgressTable, ColPrefix + '_UserId', type_Association, true);
  }
  
  customFileName = 'User.aspx';
  customFileNameCS = 'User.aspx.cs';
  customGridName = EntityName + 'UserGrid';

  //create the custom list block  
  AddCustom_ScreenObjects(customGridName, 'List', EntityName, 'N', 0, EntityName, '', '', '');
  AddCustom_Lists(customGridName, 1, nameCol, '', '', '', '', Grip_Jump, '','', JumpSummaryPage, JumpIdField); 
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'User.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'User.aspx.cs', entityDir + customFileNameCS);
  
    //add the custom tab to the User Tab Entity
    AddCustom_Tabs(0, 0, 20, 'User', EntityName, 'customfile', EntityName + '\\' + customFileName, '', '', 0, EntityName,Tabs_OnlineOnly);

    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxxUserGrid', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'entwiz/NewEntity.aspx', EntityName + '/' + newTabASP);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxxUserGrid', customGridName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);

    if( Workflow == 'on' )
      SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxxWorkflow', EntityName);
    else
      SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxxWorkflow', '');
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
  AddCustom_Relationship('Company', foreignkey, EntityName, idfield, type_Association, true);
  AddCustom_Relationship(EntityName, idfield, 'Company', foreignkey, type_Reference, false);
  
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
  
  //add it to the company entry box
  AddCustom_Screens('CompanyBoxLong', 20, foreignkey, 0, 1, 1, 'N');
  
  //add it to the top content
  AddCustom_ScreenObjects('CompanyTopContent','Screen','Company','N','0','Company','','','');
  AddCustom_Screens('CompanyTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);

  
  customFileName = 'CompanyList.aspx';
  customFileNameCS = 'CompanyList.aspx.cs';
  customGridName = 'CompanyGrid';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'CompanyList.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'CompanyList.aspx.cs', entityDir + customFileNameCS);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 2, tabgroup, 'Companies', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'company',Tabs_OnlineOnly);
        
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'Comp_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'entwiz', EntityName);     

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'Comp_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'entwiz', EntityName);     
  }

  //Make an asp page that allows user to search for companies to link to the entity
  AddCustom_Captions('Tags','Colnames','SearchCompany',0,'Search for company', '', '', '', '', '', '');

  if (!ForDotNet) 
  {   
    customFileName = 'CompanyLink.aspx';
    customFileNameCS = 'CompanyLink.aspx.cs';
    
    CopyFile(customPagesDir + 'CompanyLink.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'CompanyLink.aspx.cs', entityDir + customFileNameCS);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);    
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'Comp_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);
    CopyAspTo('\\CompLink.gif', '\\themes\\img\\color\\buttons\\CompLink.gif');
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'entwiz', EntityName);     

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);    
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'Comp_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'entwiz', EntityName);     

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
  AddCustom_Relationship('Person', foreignkey, EntityName, idfield, type_Association, true);
  AddCustom_Relationship(EntityName, idfield, 'Person', foreignkey, type_Reference, false);
  
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);  
  
  //add it to the person entry screen
  AddCustom_Screens('PersonBoxLong', 20, foreignkey, 0, 1, 1, 'N');
  
  //add it to the top content
  AddCustom_ScreenObjects('PersonTopContent','Screen','Person','N','0','Person','','',''); 
  AddCustom_Screens('PersTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);

  customFileName = 'PeopleList.aspx';
  customFileNameCS = 'PeopleList.aspx.cs';
  customGridName = 'PersonListBlock';

  if (!ForDotNet) 
  {    
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'PeopleList.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'PeopleList.aspx.cs', entityDir + customFileNameCS);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 3, tabgroup, 'People', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'person',Tabs_OnlineOnly);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'pers_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'entwiz', EntityName);     
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'pers_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'entwiz', EntityName);     
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);

  }

  //Make an asp page that allows user to search for people to link to the entity
  AddCustom_Captions('Tags','Colnames','SearchPerson',0,'Search for person', '', '', '', '', '', '');

  if (!ForDotNet) 
  {  
    customFileName = 'PersonLink.aspx';
    customFileNameCS = 'PersonLink.aspx.cs';
    CopyFile(customPagesDir + 'PersonLink.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'PersonLink.aspx.cs', entityDir + customFileNameCS);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'pers_xxxxxxxxxId', foreignkey);
    CopyAspTo('\\PersLink.gif', '\\themes\\img\\color\\buttons\\PersLink.gif');
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'entwiz', EntityName);     

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'pers_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'entwiz', EntityName);     

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
  AddCustom_Relationship('Cases', foreignkey, EntityName, idfield, type_Association, true);
  AddCustom_Relationship(EntityName, idfield, 'Cases', foreignkey, type_Reference, false);  
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
    
  //add it to the cases entry screen
  AddCustom_Screens('CaseDetailBox', 20, foreignkey, 0, 1, 1, 'N');
  
  //add it to the top content
  AddCustom_ScreenObjects('CaseTopContent','Screen','Cases','N','0','Cases','','',''); 
  AddCustom_Screens('CaseTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);
  
  customFileName = 'Cases.aspx';
  customFileNameCS = 'Cases.aspx.cs';
  customGridName = 'CaseListBlock';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'Cases.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'Cases.aspx.cs', entityDir + customFileNameCS);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 4, tabgroup, 'Cases', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'cases',Tabs_OnlineOnly);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'Case_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'Case_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);

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
  AddCustom_Relationship('Communication', foreignkey, EntityName, idfield, type_Association, true);
  AddCustom_Relationship(EntityName, idfield, 'Communication', foreignkey, type_Reference, false);
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
  
  //add it to the communications entry screen
  AddCustom_Screens('CustomCommunicationDetailBox', 20, foreignkey, 1, 1, 1, 'N');
    
  customFileName = 'Communication.aspx';
  customFileNameCS = 'Communication.aspx.cs';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'Communication.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'Communication.aspx.cs', entityDir + customFileNameCS);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 4, tabgroup, 'Communications', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'communication',Tabs_OnlineOnly);

    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'Comm_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'Comm_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);

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
  AddCustom_Relationship('Opportunity', foreignkey, EntityName, idfield, type_Association, true);
  AddCustom_Relationship(EntityName, idfield, 'Opportunity', foreignkey, type_Reference, false);
  
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);

  //add it to the top content
  AddCustom_ScreenObjects('OppoTopContent','Screen','Opportunity','N','0','Opportunity','','',''); 
  AddCustom_Screens('OppoTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);

  //add it to the opportunity entry screen
  AddCustom_Screens('OpportunityDetailBox', 20, foreignkey, 0, 1, 1, 'N'); 
    
  customFileName = 'Opportunity.aspx';
  customFileNameCS = 'Opportunity.aspx.cs';
  customGridName = 'OpportunityListBlock';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'Opportunity.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'Opportunity.aspx.cs', entityDir + customFileNameCS);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 6, tabgroup, 'Opportunities', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'opportunity',Tabs_OnlineOnly);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'oppo_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'oppo_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);

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
  AddCustom_Relationship('Lead', foreignkey, EntityName, idfield, type_Association, true);
  AddCustom_Relationship(EntityName, idfield, 'Lead', foreignkey, type_Reference, false);
  
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
  
  //add it to the top content
  AddCustom_ScreenObjects('LeadTopContent','Screen','Lead','Y','0','Lead','','','');  
  AddCustom_Screens('LeadTopContent','1',foreignkey,'0','0','0','',0,'','','',Grip_Jump);
    
  //add it to the lead entry screen
  AddCustom_Screens('LeadCustomScreen', 20, foreignkey, 1, 1, 1, 'N'); 
    
  customFileName = 'Lead.aspx';
  customFileNameCS = 'Lead.aspx.cs';
  customGridName = 'LeadListBlock';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'Lead.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'Lead.aspx.cs', entityDir + customFileNameCS);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 7, tabgroup, 'Leads', 'customfile', EntityName + '\\' + customFileName, '', '', 0, 'lead',Tabs_OnlineOnly);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'lead_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'lead_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

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
  AddCustom_Relationship('Library', foreignkey, EntityName, idfield, type_Association, true);
  AddCustom_Relationship(EntityName, idfield, 'Library', foreignkey, type_Reference, false);
  
  AddCustom_Captions('Tags', 'ColNames', foreignkey, 0, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName, EntityName);
    
  //add it to the library entry screen
  AddCustom_Screens('LibraryItemBoxLong', 20, foreignkey, 1, 1, 1, 'N'); 
    
  customFileName = 'Library.aspx';
  customFileNameCS = 'Library.aspx.cs';
  customGridName = 'LibraryList';
  
  if (!ForDotNet) 
  {  
    //makes a renamed copy of the generic asp to be used for the todo custom asp file
    CopyFile(customPagesDir + 'Library.aspx', entityDir + customFileName);
    CopyFile(customPagesDir + 'Library.aspx.cs', entityDir + customFileNameCS);
    
    //add the custom tab to the entity's tabgroup
    AddCustom_Tabs(0, 0, 8, tabgroup, 'Library', 'customfile', EntityName + '\\' + customFileName);
    
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'libr_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxxxxxxx', EntityName); 
    SearchAndReplaceInCustomFile(entityDir + customFileName, 'xxxx', ColPrefix);

    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'aspxxxxxxx', tabgroup);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'libr_xxxxxxxxxId', foreignkey);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxxxxxxx', EntityName); 
    SearchAndReplaceInCustomFile(entityDir + customFileNameCS, 'xxxx', ColPrefix);

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

//create the custom search entry block    

customEntryName = EntityName + 'SearchBox';

AddCustom_ScreenObjects(customEntryName, 'SearchScreen', EntityName, 'N', 0, EntityName, '', '', '',customGridName);   
AddCustom_Screens(customEntryName, 1, nameCol, 0, 1, 1, 'N');
if( ToDo == 'on' ) 
{
  AddCustom_Screens(customEntryName, 1, ColPrefix+'_UserId', 0, 1, 1, 'N');
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
    CopyFile(customPagesDir + 'FindEntity.aspx', entityDir + findASP);
    CopyFile(customPagesDir + 'FindEntity.aspx.cs', entityDir + findASPCS);

    //add the custom tab to the Find Tab Entity
    AddCustom_Tabs(0, 0, 20, 'Find', EntityName, 'customfile', EntityName + '\\' + findASP, '', Icon, 0, EntityName,Tabs_OnlineOnly);
      
    //substitute in params for the custom file
    SearchAndReplaceInCustomFile(entityDir + findASP, 'xxxxxxxxxSearchBox', customEntryName);
    SearchAndReplaceInCustomFile(entityDir + findASP, 'xxxxxxxxxGrid', customGridName);
    SearchAndReplaceInCustomFile(entityDir + findASP, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + findASP, 'xxxx', ColPrefix);
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
  newASP = 'NewEntity.aspx';
  newASPCS = 'NewEntity.aspx.cs';
  newTabASP = newASP;
  
  customEntryName = EntityName + 'NewEntry';

  //create the custom entry screen  
  AddCustom_ScreenObjects(customEntryName, 'Screen', EntityName, 'N', 0, EntityName, '', '', '');   
  AddCustom_Screens(customEntryName, 1, nameCol, 0, 1, 1, 'N');
  
  if( ToDo == 'on' ) 
  {
    AddCustom_Screens(customEntryName, 2, ColPrefix+'_UserId', 0, 1, 1, 'N');
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
    CopyFile(customPagesDir + 'NewEntity.aspx', entityDir + newASP);
    CopyFile(customPagesDir + 'NewEntity.aspx.cs', entityDir + newASPCS);
  
    //add the custom tab to the New Tab Entity
    AddCustom_Tabs(0, 0, 20, 'New', EntityName, 'customfile', EntityName + '\\' + newTabASP, '', Icon, 0, EntityName,Tabs_OnlineOnly);

    SearchAndReplaceInCustomFile(entityDir + newASP, 'entwiz/FindEntity.aspx', EntityName + '/' + findASP);
    SearchAndReplaceInCustomFile(entityDir + newASP, 'xxxxxxxxx', EntityName);
    SearchAndReplaceInCustomFile(entityDir + newASP, 'xxxxxxxxxSearchBox', customEntryName);
    SearchAndReplaceInCustomFile(entityDir + newASP, 'xxxx_xxxxxxxxxid', idfield);
    SearchAndReplaceInCustomFile(entityDir + newASP, 'xxxx', ColPrefix);
    SearchAndReplaceInCustomFile(entityDir + newASP, 'entwiz', EntityName );
    
  }
}

if( ToDo == 'on' ) 
{
  AddCustom_Lists(customGridName, 2, ColPrefix+'_UserId', '', '', '', '', '', '','', '', ''); 
}

//substitute in params for the summary page
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, 'entwiz', customEntryName);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, 'aspxxxxxxx', tabgroup);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, 'xxxxxxxxx', EntityName);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, 'xxxx_xxxxxxxxxid', idfield);
SearchAndReplaceInCustomFile(entityDir + customSummaryPage, 'xxxx', ColPrefix);

//copy our asp bridge files
var aspArr=new Array();
aspArr[0]="SageCRM.js";
aspArr[1]="Authenticated.asp";
aspArr[2]="AuthenticationError.asp";
aspArr[3]="button.asp";
aspArr[4]="change.asp";
aspArr[5]="custom.asp";
aspArr[6]="dbg.asp";
aspArr[7]="deleterecord.asp";
aspArr[8]="deleterecord_portal.asp";
aspArr[9]="entrygroup.asp";
aspArr[10]="entrygroup_portal.asp";
aspArr[11]="entrygroupcreate.asp";
aspArr[12]="entrygroupcreate_portal.asp";
aspArr[13]="entrygroupsearch.asp";
aspArr[14]="entrygroupsearch_portal.asp";
aspArr[15]="entryitem.asp";
aspArr[16]="execsql.asp";
aspArr[17]="filter.asp";
aspArr[18]="filter_portal.asp";
aspArr[19]="findrecord.asp";
aspArr[20]="findrecord_portal.asp";
aspArr[21]="GetContextInfo.asp";
aspArr[22]="gettableschema.asp";
aspArr[23]="gettableschema_portal.asp";
aspArr[24]="gettableschema_selectsql.asp";
aspArr[25]="gettableschema_selectsql_portal.asp";
aspArr[26]="GetTrans.asp";
aspArr[27]="GetTrans_portal.asp";
aspArr[28]="getVisitorInfo.asp";
aspArr[29]="imagelist.asp";
aspArr[30]="insertrecord.asp";
aspArr[31]="insertrecord_portal.asp";
aspArr[32]="list.asp";
aspArr[33]="list_portal.asp";
aspArr[34]="lookup.asp";
aspArr[35]="nonSysFields.asp";
aspArr[36]="old_topcontent.asp";
aspArr[37]="recentlist.asp";
aspArr[38]="SageCRM_Portal.js";
aspArr[39]="save.asp";
aspArr[40]="searchlist.asp";
aspArr[41]="selectsql.asp";
aspArr[42]="selectsql_portal.asp";
aspArr[43]="setVisitorInfo.asp";
aspArr[44]="tabgroup.asp";
aspArr[45]="topcontent.asp";
aspArr[46]="updaterecord.asp";
aspArr[47]="updaterecord_portal.asp";
aspArr[48]="url.asp";

for(var ix=0;ix<aspArr.length;ix++)
{
  CopyASPTo('\\SageCRM\\component\\'+aspArr[ix],'\\custompages\\SageCRM\\component\\'+aspArr[ix]);
}

} //closing bracket for: if( valid ){

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

