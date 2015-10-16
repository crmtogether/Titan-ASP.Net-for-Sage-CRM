<!-- #include file ="SageCRM.js" -->
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
  Holder = eWare.GetBlock('container');
  block = eWare.GetBlock(EntryBlock);
  Holder.AddBlock(block);
  
  var record =  eWare.CreateRecord(EntityName);

    if (Defined(Evalcode))
    {
      Evalcode=unescape(Evalcode);
      eval(Evalcode);
      
    }
  
  if (( eWare.Mode < 1 )){
    eWare.Mode = 1;

    
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
     Response.clear();
     Response.Write("<recordid>"+record.RecordId+"</recordid>");
     Response.End;
    }
  }
  emmode=eWare.Mode+1;
  //Removed as broken in new 
  //eWare.SetContext("New");
  Response.Write(Holder.Execute(record));
  var _version=new String(getCRMVersion());
  try{
    if (_version.indexOf("7.2")==0)
	{
      Response.Write(eWare.GetPageNoFrameset('New'));       ///from 7.2b we must call this.
	}
  }catch(ex)
  {
    try{
      Response.Write(eWare.GetPage('New'));///the above line will fail pre 7.2 and we fall back to the old method
	}catch(ex)
    {
	}
  }
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
