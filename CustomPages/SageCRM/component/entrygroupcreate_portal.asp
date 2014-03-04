<!-- #include file ="SageCRM_portal.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try
{
  var EntityName=new String(Request.QueryString("EntityName"));
  var EntryBlock=new String(Request.QueryString("EntryBlock"));
  var EntityWhere=new String(Request.QueryString("EntityWhere"));
  var BlockTitle=new String(Request.QueryString("BlockTitle"));
  var AfterSavePage=new String(Request.QueryString("AfterSavePage"));
  
  var WorkflowName=new String(Request.QueryString("WorkflowName"));
  var WFState=new String(Request.QueryString("WFState"));
  var Evalcode=new String(Request.Form("Evalcode"));  
  if (!Defined(WorkflowName))
  {
    WorkflowName="";
  }      
  
  block = eWare.GetBlock(EntryBlock);
  
  var record =  eWare.CreateRecord(EntityName);  
  
  if( eWare.Mode < 1 ){
    eWare.Mode = 1;
  }

  if (Defined(BlockTitle)){
    block.Title=BlockTitle;
  }

  block.DisplayForm=false;
  block.DisplayButton(Button_Default) = false;
  block.ArgObj=record;
  if (WorkflowName!="")
  {  
    record.SetWorkflowInfo(WorkflowName,WFState);     
  }
  
  if ((eWare.Mode==Save) && (block.Validate())){
    block.Execute(); ///save the changes
    //go to our new page..a summary page perhaps
    if (Defined(AfterSavePage) && AfterSavePage!=''){
      Response.Write("<script>"+
                     "document.location='"+AfterSavePage+"&1=1&nrid="+record.RecordId+"';"+
                     "</script>");
      Response.End;
    }else{
     Response.Write("<recordid>"+record.RecordId+"</recordid>");
     Response.End;
    }
  }
  emmode=eWare.Mode+1;
  if ((eWare.Mode==2) && (block.Validate()==false))
    emmode=emmode-1;
  Response.Write(block.Execute());

  //as we hide the form we need to output these values
  Response.Write('<input type="HIDDEN" NAME="em" value="'+emmode+'" />');
  Response.Write('<input type=hidden name="yearEntry" />');
  Response.Write('<input type=hidden name="monthEntry" />');
  Response.Write('<input type=hidden name="dayEntry" />');

}catch(e){
  logerror(e);
}
%>
