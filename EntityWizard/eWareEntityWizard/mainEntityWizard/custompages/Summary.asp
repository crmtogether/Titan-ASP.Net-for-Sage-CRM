<!-- #include file ="..\crmwizard.js" -->

<%

if( eWare.Mode != Save ){
  F=Request.QueryString("F");
  if( F == "**&newASP&**" ) eWare.Mode=Edit;
}

Container=eWare.GetBlock("container");
Entry=eWare.GetBlock("**&SummaryEntry&**");
Entry.Title="**&EntityName&**";
Container.AddBlock(Entry);
Container.DisplayButton(1)=false;

var Id = new String(Request.Querystring("**&idfield&**"));

if (Id.toString() == 'undefined') {
  Id = new String(Request.Querystring("Key58"));
  if (Id.toString() == 'undefined') {
    Id = new String(Request.Querystring("Key0"));
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

   eWare.SetContext("**&tabgroup&**", UseId);

   record = eWare.FindRecord("**&EntityName&**", "**&idfield&**="+UseId);

   //if were deleting
   if( Request.Querystring("em") == 3 )
   {
     record.DeleteRecord = true;
     record.SaveChanges();

     // need to redirect back to the place where we got to the summary from
     // -- but we cant refresh the top frame easily so just go back to find
     // -- every time
     PrevCustomURL = new String(Request.QueryString("F"));
     URLarr=PrevCustomURL.split(",");
     if(URLarr[0].toUpperCase() != "**&newASP&**")
       Response.Redirect(eWare.URL("**&findASP&**?J=**&findASP&**&E=**&EntityName&**"));
     else
       Response.Redirect(eWare.URL("**&newASP&**?J=**&newASP&**&E=**&EntityName&**"));
   }
   else
   {
     if( **&bWorkflow&** )
     {   
       Container.ShowWorkflowButtons = true;
       Container.WorkflowTable = "**&EntityName&**";
     }

     if(eWare.Mode == Edit)
     {
       Container.DisplayButton(Button_Continue) = true;
       Container.AddButton(eWare.Button("Delete", "delete.gif", "javascript:x=location.href;i=x.search('&em=');if (i >= 0) {   x=x.substr(0,i)+x.substr(i+2+3,x.length);}x=x+'&'+'em'+'='+'3';location.href=x", "**&EntityName&**", "DELETE"));
       Container.AddButton(eWare.Button("Save", "save.gif", "javascript:x=location.href;if (x.charAt(x.length-1)!='&')if (x.indexOf('?')>=0) x+='&'; else x+='?';x+='**&idfield&**="+UseId+"';document.EntryForm.action=x;document.EntryForm.submit();", "**&EntityName&**", "EDIT"));
     }
     else
     {
       Container.DisplayButton(Button_Continue) = true;
       Container.AddButton(eWare.Button("Change","edit.gif","javascript:x=location.href;if (x.charAt(x.length-1)!='&')if (x.indexOf('?')>=0) x+='&'; else x+='?';x+='**&idfield&**="+UseId+"';document.EntryForm.action=x;document.EntryForm.submit();", "**&EntityName&**", "EDIT"));
     }

     eWare.AddContent(Container.Execute(record));
  }
  eWare.GetCustomEntityTopFrame("**&EntityName&**");
  Response.Write(eWare.GetPage());
}

%>