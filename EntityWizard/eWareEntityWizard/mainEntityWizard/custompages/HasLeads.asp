<!-- #include file ="..\crmwizard.js" -->

<%

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

Container=eWare.GetBlock("container");

List=eWare.GetBlock("LeadGrid");
List.prevURL=sURL;
var Id = new String(Request.Querystring("**&idfield&**"));
if (Id.toString() == 'undefined') {
   Id = new String(Request.Querystring("Key58"));
}

if (Id.toString() != 'undefined') {

   eWare.SetContext("**&EntityName&**", Id);

   Container.AddBlock(List);
   Container.AddButton(eWare.Button("New", "new.gif", eWare.URL(191)+"&Key-1="+iKey_CustomEntity+"&PrevCustomURL="+List.prevURL+"&E=**&EntityName&**", 'lead', 'insert'));
   Container.DisplayButton(1)=false;

   if( Id != '')
   {
     eWare.AddContent(Container.Execute("**&foreignkey&**="+Id));
   }
}

eWare.GetCustomEntityTopFrame("**&EntityName&**");
Response.Write(eWare.GetPage());

%>