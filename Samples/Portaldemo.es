
FamilyType='Tags';
Family='Screens';
Code='PortalCaseFilter';
CaptionUS='PortalCaseFilter';
AddCaption();

FamilyType='Tags';
Family='Screens';
Code='PortalCommFilter';
CaptionUS='PortalCommFilter';
AddCaption();

FamilyType='Tags';
Family='Screens';
Code='PortalCompanyBoxLong';
CaptionUS='PortalCompanyBoxLong';
AddCaption();

DeleteCustom_ScreenObject('PortalCaseList','',1);

ObjectName='PortalCaseList';
ObjectType='List';
EntityName='Cases';
TargetTable='Cases';
var CObjId10481 = AddScreenObject();

var ListId12550 = AddCustom_Lists('PortalCaseList','1','case_referenceid','','','','','custom','','','casedetail.aspx','case_caseid',0,'','Cases');

var ListId12551 = AddCustom_Lists('PortalCaseList','2','case_createddate','','','','','','','','','',0,'','Cases');

var ListId12552 = AddCustom_Lists('PortalCaseList','3','case_status','','','','','','','','','',0,'','Cases');

var ListId12553 = AddCustom_Lists('PortalCaseList','4','case_stage','','','','','','','','','',0,'','Cases');

var ListId12554 = AddCustom_Lists('PortalCaseList','5','case_priority','','','','','','','','','',0,'','Cases');

var ListId12555 = AddCustom_Lists('PortalCaseList','6','case_problemnote','','','','','','','','','',0,'','Cases');

DeleteCustom_ScreenObject('Portalcommunications','',1);

ObjectName='Portalcommunications';
ObjectType='List';
EntityName='Communication';
TargetTable='vListCommunication';
var CObjId10484 = AddScreenObject();

var ListId12546 = AddCustom_Lists('Portalcommunications','1','comm_datetime','','','','','','','','','',0,'','Communications');

var ListId12547 = AddCustom_Lists('Portalcommunications','2','comm_action','','','','','','','','','',0,'','Communications');

var ListId12548 = AddCustom_Lists('Portalcommunications','3','cmli_comm_userid','','','','','','','','','',0,'','Communications');

var ListId12549 = AddCustom_Lists('Portalcommunications','4','comm_note','','','','','','','','','',0,'','Communications');

DeleteCustom_ScreenObject('PortalPersonList','',1);

ObjectName='PortalPersonList';
ObjectType='List';
EntityName='Person';
TargetTable='Person';
var CObjId10483 = AddScreenObject();

var ListId12538 = AddCustom_Lists('PortalPersonList','1','pers_firstname','','','','','','','','','',0,'','Person');

var ListId12539 = AddCustom_Lists('PortalPersonList','2','pers_lastname','','','','','','','','','',0,'','Person');

var ListId12540 = AddCustom_Lists('PortalPersonList','3','pers_gender','','','','','','','','','',0,'','Person');

var ListId12541 = AddCustom_Lists('PortalPersonList','4','pers_title','','','','','','','','','',0,'','Person');

var ListId12542 = AddCustom_Lists('PortalPersonList','5','pers_emailaddress','','','','','','','','','',0,'','Person');

ObjectName='Portaltabs';
ObjectType='TabGroup';
EntityName='Visitor';
var CObjId10478 = AddScreenObject();

ObjectName='PortalCompanyBoxLong';
ObjectType='Screen';
EntityName='Company';
var CObjId10482 = AddScreenObject();

ObjectName='PortalCommFilter';
ObjectType='SearchScreen';
EntityName='Communication';
Properties='PortalCommFilter';
var CObjId10485 = AddScreenObject();

ObjectName='PortalSupport';
ObjectType='TabGroup';
EntityName='Visitor';
var CObjId10479 = AddScreenObject();

ObjectName='PortalCaseFilter';
ObjectType='SearchScreen';
EntityName='Cases';
var CObjId10480 = AddScreenObject();

DeleteCustom_ScreenObject('PortalCaseFilter','',1);

// Adding new screen PortalCaseFilter
EntryScreenName='PortalCaseFilter';
FieldName='case_referenceid';
Newline=true;
AddEntryScreenField();

FieldName='case_status';
Newline=true;
AddEntryScreenField();

FieldName='case_stage';
Newline=true;
AddEntryScreenField();

DeleteCustom_ScreenObject('PortalCommFilter','',1);

// Adding new screen PortalCommFilter
EntryScreenName='PortalCommFilter';
FieldName='comm_action';
Newline=true;
AddEntryScreenField();

FieldName='comm_priority';
Newline=true;
AddEntryScreenField();

FieldName='comm_status';
Newline=true;
AddEntryScreenField();

DeleteCustom_ScreenObject('PortalCompanyBoxLong','',1);

// Adding new screen PortalCompanyBoxLong
EntryScreenName='PortalCompanyBoxLong';
FieldName='comp_name';
ColSpan=3;
AddEntryScreenField();

FieldName='comp_employees';
Newline=true;
AddEntryScreenField();

if (CRM.GetCRMVersion()=="6.2" || CRM.GetCRMVersion()=="7.0")
{
FieldName='comp_phonenumber';
AddEntryScreenField();
}

FieldName='comp_territory';
AddEntryScreenField();

FieldName='comp_source';
Newline=true;
AddEntryScreenField();

FieldName='comp_status';
AddEntryScreenField();

FieldName='comp_website';
AddEntryScreenField();

var TabsId10884 = AddCustom_Tabs(0,0,1,'PortalSupport','Overview','customfile','cases.aspx','','DefaultButtonGroup.gif',0,'',false);

var TabsId10885 = AddCustom_Tabs(0,0,2,'PortalSupport','Report a problem','customfile','casedetail.aspx','','DefaultButtonGroup.gif',0,'',false);

var TabsId10886 = AddCustom_Tabs(0,0,3,'PortalSupport','Request product information','customfile','opportunitydetail.aspx','','DefaultButtonGroup.gif',0,'',false);

var TabsId10887 = AddCustom_Tabs(0,0,1,'Portaltabs','About Us','customfile','aboutus.aspx','','DefaultButtonGroup.gif',0,'',false);

var TabsId10888 = AddCustom_Tabs(0,0,2,'Portaltabs','Services','customfile','services.aspx','','DefaultButtonGroup.gif',0,'',false);
/*
//commented out due to bug in 6.2 breaking self service blocks when user not logged in
var TabsId10889 = AddCustom_Tabs(0,0,3,'Portaltabs','Newsletter','customfile','newsletter.aspx','','DefaultButtonGroup.gif',0,'',false);
*/
var TabsId10890 = AddCustom_Tabs(0,0,4,'Portaltabs','Your Company','customfile','company.aspx','','DefaultButtonGroup.gif',0,'',false);

var TabsId10891 = AddCustom_Tabs(0,0,5,'Portaltabs','Your People','customfile','people.aspx','','DefaultButtonGroup.gif',0,'',false);

var TabsId10892 = AddCustom_Tabs(0,0,6,'Portaltabs','Your Cases','customfile','cases.aspx','','DefaultButtonGroup.gif',0,'',false);

var TabsId10893 = AddCustom_Tabs(0,0,7,'Portaltabs','Your Communications','customfile','communications.aspx','','DefaultButtonGroup.gif',0,'',false);

DeleteCustom_ScreenObject('ssLibraryList','',1);

ObjectName='ssLibraryList';
ObjectType='List';
EntityName='Library';
AllowDelete=false;
TargetTable='vLibrary';
var CObjId216 = AddScreenObject();

var ListId12464 = AddCustom_Lists('ssLibraryList','1','libr_updateddate','','','','','custom','','','getFile.aspx','libr_libraryid',0,'','Library');

var ListId12465 = AddCustom_Lists('ssLibraryList','2','libr_note','','','','','','','','','',0,'','Library');

var ListId12466 = AddCustom_Lists('ssLibraryList','3','libr_filename','','','','','','','','','',0,'','Library');

