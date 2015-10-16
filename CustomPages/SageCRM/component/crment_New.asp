<!-- #include file ="SageCRM.js" -->
<!-- #include file ="crmconst.js" -->
<!DOCTYPE html><html lang="en"><head><META http-equiv="Content-Type" content="text/html; charset=utf-8"><META HTTP-EQUIV="Expires" CONTENT="-1"><META HTTP-EQUIV="Pragma" CONTENT="no-cache"><META HTTP-EQUIV="Cache-Control" CONTENT="no-cache,no-Store">
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
var useWorkflow=false;
%>
<html>
  <head>
<link type="text/css" rel="Stylesheet" href="../../../Themes/ergonomic.css" />
     <script language="javascript" type="text/javascript" src="../../sagecrmws/js/genFunctions.js"></script>

    <script type="text/javascript" src="js/lib/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="js/lib/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/lib/jquery.rotate.1-1.js"></script>

<script type="text/javascript" src="../../../js/lib/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="../../../js/lib/jquery-ui-1.8.23.custom.min.js"></script>
<script type="text/javascript" src="../../../js/lib/jquery.rotate.1-1.js"></script>
<script type="text/javascript" src="../../../js/crm/crmPopup.js?version=53687:53691"></script>
 
  <title>CRM Together</title>  
  </head>
  <body>
  <div>
  <%
    Response.Write(eWare.Button("Save", "save.gif", "javascript:document.forms[0].submit();", "","",""));
  %>
  </div>
<%
var Now=new Date();
if (eWare.Mode<Edit) eWare.Mode=Edit;

record=eWare.CreateRecord(EntityName);
if(useWorkflow)
  record.SetWorkFlowInfo(EntityName+" Workflow", "Logged");

EntryGroup=eWare.GetBlock(BlockName);
EntryGroup.Title=EntityName;

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
    Response.Write(eWare.Button("Save", "save.gif", "javascript:document.forms[0].submit();", "","",""));
  %>
  </div>
  <script>

 function setContextInt() {

            //fix for .net4.0
            var frm = document.getElementById("EntryForm");
            //document.EntryForm=frm;
            frm.name = "EntryForm";

            fix72Layout();
        }
        function fix72Layout() {
            var td_col = document.getElementsByTagName("TD");            
            for (var i = 0; i < td_col.length; i++) {
                var _td = td_col[i];
                if (_td.style.width == "15px") {
                    var firstRow = _td.parentNode;
                    firstRow.deleteCell(0);
                }
            }
            if ($+""!="undefined")
              $("body").css("overflow", "auto");
        }
        try {
            window.addEventListener("load", setContextInt);
            
            window.addEventListener("load", fix73Layout);
          
        } catch (ex) {
            window.attachEvent("onload", setContextInt);
              
            window.attachEvent("onload", fix73Layout);
          
        }

</script>		


  </body>
</html>