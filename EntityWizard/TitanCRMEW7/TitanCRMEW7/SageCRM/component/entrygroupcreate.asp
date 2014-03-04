<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Sage CRM Titan ASP.NET Suite
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
  Holder = eWare.GetBlock('container');
  block = eWare.GetBlock(EntryBlock);
  Holder.AddBlock(block);
  
  var record =  eWare.CreateRecord(EntityName);

  if (( eWare.Mode < 1 )){
    eWare.Mode = 1;

    if (Defined(Evalcode))
    {
      Evalcode=unescape(Evalcode);
      eval(Evalcode);
    }
    
  }
  if ( eWare.Mode ==3 )
  {
    eWare.Mode=2;
  }

  if (Defined(BlockTitle)){
    block.Title=BlockTitle;
  }

  Holder.DisplayForm=false;
  Holder.DisplayButton(Button_Default) = false;
  block.ArgObj=record;
  if (WorkflowName!="")
  {  
    record.SetWorkflowInfo(WorkflowName,WFState);     
    Holder.ShowWorkflowButtons = true;
    Holder.WorkflowTable = EntityName;
  }
  if ((eWare.Mode==Save) && (block.Validate())){   
    Holder.Execute(record); ///save the changes
    //go to our new page..a summary page perhaps
    if (Defined(AfterSavePage) && AfterSavePage!=''){
      Response.Write("<script>"+
                     "document.location='"+eWare.Url(AfterSavePage)+"&nrid="+record.RecordId+"';"+
                     "</script>");
      Response.End;
    }else{
     Response.Write("<recordid>"+record.RecordId+"</recordid>");
     Response.End;
    }
  }
  emmode=eWare.Mode+1;
  eWare.SetContext("New");
  Response.Write(Holder.Execute(record));
  Response.Write(eWare.GetPage('New'));
  //as we hide the form we need to output these values
  Response.Write('<input type="HIDDEN" NAME="em" value="'+emmode+'" />');
/*
Removed as of version 2.1.6...now in web form templates  
  Response.Write('<input type=hidden name="yearEntry" />');
  Response.Write('<input type=hidden name="monthEntry" />');
  Response.Write('<input type=hidden name="dayEntry" />');
  */

}catch(e){
  logerror(e);
}
%>
