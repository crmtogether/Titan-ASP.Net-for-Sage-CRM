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
  var PermissionsEntity=Request.QueryString('PermissionsEntity');
  var PermissionsType=Request.QueryString('PermissionsType');
  var BlockName=Request.QueryString('BlockName');

  //change in 2.1.6 for ajax support now means we have to look after getting the mode for this button
  eEntries = new Enumerator(Request.Form);
  while (!eEntries.atEnd()) {
    y = new String(eEntries.item());
    len=y.length;
    idx=y.indexOf("_em");
    if ( ((idx+3)==len) && (idx!=-1) )  //we have the mode
    {
      eWare.Mode=Request.Form(y);
      if (eWare.Mode==2)
        break;
    }
    eEntries.moveNext();      
  }
  if ((BlockName!="") && (Defined(BlockName)))
  {
    block=eWare.GetBlock(BlockName);
    if (!block.Validate())
    {
      eWare.Mode=Edit;
    }
  }
  if (eWare.Mode==Edit){
    Response.Write(eWare.Button("Save", "Save.gif", "javascript:document.EntryForm.submit();",
                 PermissionsEntity,PermissionsType,""));
  }else{
    Response.Write(eWare.Button("Change", "Edit.gif", "javascript:document.EntryForm.submit();",
                 PermissionsEntity,PermissionsType,""));
  }
}catch(e){
  logerror(e);
}
%>
