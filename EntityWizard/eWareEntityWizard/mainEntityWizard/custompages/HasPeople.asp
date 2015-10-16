<!-- #include file ="..\crmwizard.js" -->

<%

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

Container=eWare.GetBlock("container");

List=eWare.GetBlock("PersonGrid");
List.prevURL=sURL;
var Id = new String(Request.Querystring("**&idfield&**"));
if (Id.toString() == 'undefined') {
   Id = new String(Request.Querystring("Key58"));
}

if (Id.toString() != 'undefined') {

   eWare.SetContext("**&EntityName&**", Id);

   //If dedupe is turned on, change action below from 141 to 1201 to get the new individual dedupe screen

   Container.AddBlock(List);
   Container.AddButton(eWare.Button("New", "new.gif", eWare.URL(141)+"&Key-1="+iKey_CustomEntity+"&PrevCustomURL="+List.prevURL+"&E=**&EntityName&**", 'Person', 'insert'));
   Container.AddButton(eWare.Button("Link", "perslink.gif", eWare.URL("**&EntityName&**/**&EntityName&**PersonLink.asp"+"?J=**&EntityName&**/**&EntityName&**Person.asp")+"&E=**&EntityName&**&**&idfield&**="+Id));
   Container.DisplayButton(1)=false;
 
   if( Id != '')
   {
     eWare.AddContent(Container.Execute("**&foreignkey&**="+Id));
   }
}

eWare.GetCustomEntityTopFrame("**&EntityName&**");
Response.Write(eWare.GetPage());

%>