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
//**********************************************
//* functions END
//**********************************************
%>
<%
var CRMPath=sInstallName;
var EntityName=getEntityName();
var SummaryPage="demoent_Summary.asp";
var NewPage="demoent_New.asp";
var BlockName=getBlockName(EntityName);
var IdField=getIdField(EntityName);
%>
<html>
  <head>
  <link type="text/css" rel="Stylesheet" href="/<%=CRMPath%>/Themes/default.css" />   
  <title>CRM</title>  
  </head>
  <body>
<%

Container=eWare.GetBlock("container");
Entry=eWare.GetBlock(BlockName);
Entry.Title=EntityName;
Container.AddBlock(Entry);
Container.DisplayButton(1)=false;

var Id = new String(Request.Querystring(IdField));

if (Id.toString() == 'undefined') {
  Id = new String(Request.Querystring("Key58"));
  if (Id.toString() == 'undefined') {
    Id = new String(Request.Querystring("Key0"));
  }
  if (Id.toString() == 'undefined') {
    Id = new String(Request.Querystring("Id"));
  }
}

var UseId = 0;

if (Id.indexOf(',') > 0) {
   var Idarr = Id.split(",");
   UseId = Idarr[0];
}
else if (Id != '') 
  UseId = Id;

if (UseId != 0) {

  var Idarr = Id.split(",");

  record = eWare.FindRecord(EntityName, IdField+"="+UseId);
  Response.Write(Container.Execute(record));
}
%>
<script>
  var a_col=document.getElementsByTagName("A");
  for(var i=0;i<a_col.length;i++)
  {
    a_col[i].href+="&NavigateAway=true";   
  }
</script>

  </body>
</html>