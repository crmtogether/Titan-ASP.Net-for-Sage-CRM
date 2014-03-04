<!-- #include file ="..\crmwizard.js" -->

<%

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

Container=eWare.GetBlock("container");

List=eWare.GetBlock("AccountGrid");
List.prevURL=sURL;
var Id = new String(Request.Querystring("**&idfield&**"));
var ExtraKeys = '';

if (Id.toString() == 'undefined') {
   Id = new String(Request.Querystring("Key58"));
}

if (Id.toString() != 'undefined') {


   eWare.SetContext("**&EntityName&**", Id);

   if ((**&bCompany&**) || (**&bPerson&**)) {
      //set key values on url so they will be picked up in webpicker	
      recObj = eWare.FindRecord('**&EntityName&**','**&idfield&**='+Id);
      if ((**&bCompany&**) && (recObj('**&ColPrefix&**_CompanyId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key1='+recObj('**&ColPrefix&**_CompanyId'); 
      }
      if ((**&bPerson&**) && (recObj('**&ColPrefix&**_PersonId') != undefined)) {
         ExtraKeys = ExtraKeys + '&Key2='+recObj('**&ColPrefix&**_PersonId'); 
      }

   }

   Container.AddBlock(List);
   Container.AddButton(eWare.Button("New", "new.gif", eWare.URL(282)+ ExtraKeys + "&Key-1="+iKey_CustomEntity+"&PrevCustomURL="+List.prevURL+"&E=**&EntityName&**", 'account', 'insert'));
   Container.DisplayButton(1)=false;

   if( Id != '')
   {
     eWare.AddContent(Container.Execute("**&foreignkey&**="+Id));
   }
}

eWare.GetCustomEntityTopFrame("**&EntityName&**");
Response.Write(eWare.GetPage());

%>