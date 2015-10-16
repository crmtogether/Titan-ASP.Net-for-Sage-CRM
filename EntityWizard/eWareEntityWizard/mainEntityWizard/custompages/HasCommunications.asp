<!-- #include file ="..\crmwizard.js" -->

<%

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

Container=eWare.GetBlock("container");

List=eWare.GetBlock("CommunicationList");
List.prevURL=sURL;
var Id = new String(Request.Querystring("**&idfield&**"));
var ExtraKeys = '';

if (Id.toString() == 'undefined') {
   Id = new String(Request.Querystring("Key58"));
}

if (Id.toString() != 'undefined') {


   eWare.SetContext("**&EntityName&**", Id);

   if ((**&bCompany&**) || (**&bAccount&**) || (**&bPerson&**) || (**&bOpportunity&**) || (**&bCase&**) 
      || (**&bOrder&**) || (**&bQuote&**))    
   {
      //set key values on url so they will be picked up in webpicker	
      recObj = eWare.FindRecord('**&EntityName&**','**&idfield&**='+Id);
      if ((**&bCompany&**) && (recObj('**&ColPrefix&**_CompanyId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key1='+recObj('**&ColPrefix&**_CompanyId'); 
      }
      if ((**&bPerson&**) && (recObj('**&ColPrefix&**_PersonId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key2='+recObj('**&ColPrefix&**_PersonId'); 
      }
      if ((**&bOpportunity&**) && (recObj('**&ColPrefix&**_OpportunityId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key7='+recObj('**&ColPrefix&**_OpportunityId'); 
      }
      else if ((**&bCase&**) && (recObj('**&ColPrefix&**_CaseId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key8='+recObj('**&ColPrefix&**_CaseId'); 
      }
      else if ((**&bAccount&**) && (recObj('**&ColPrefix&**_AccountId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key24='+recObj('**&ColPrefix&**_AccountId'); 
      }
      if ((**&bOrder&**) && (recObj('**&ColPrefix&**_OrderId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key24='+recObj('**&ColPrefix&**_OrderId'); 
      }
      if ((**&bQuote&**) && (recObj('**&ColPrefix&**_QuoteId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key24='+recObj('**&ColPrefix&**_QuoteId'); 
      }



   }

   Container.AddBlock(List);
   Container.AddButton(eWare.Button("New Task", "newTask.gif", eWare.URL(361) + ExtraKeys + "&Key-1="+iKey_CustomEntity+"&PrevCustomURL="+List.prevURL+"&E=**&EntityName&**", 'communication', 'insert'));
   Container.AddButton(eWare.Button("New Appointment", "newAppointment.gif", eWare.URL(362)+ ExtraKeys + "&Key-1="+iKey_CustomEntity+"&PrevCustomURL="+List.prevURL+"&E=**&EntityName&**", 'communication', 'insert'));
   Container.DisplayButton(1)=false;

   if( Id != '')
   {
     eWare.AddContent(Container.Execute("**&foreignkey&**="+Id));
   }
}

eWare.GetCustomEntityTopFrame("**&EntityName&**");
Response.Write(eWare.GetPage());

%>