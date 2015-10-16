<!-- #include file ="..\crmwizard.js" -->

<%

/* Search for an existing person and link it to the current **&EntityName&** record */


var Id = new String(Request.Querystring("**&idfield&**"));
if (Id.toString() == 'undefined') {
   Id = new String(Request.Querystring("Key58"));
}


Container=eWare.GetBlock("container");
Group = eWare.GetBlock("entrygroup");

PersonSearch = eWare.GetBlock("entry");

PersonSearch.FieldName = "SearchPerson";
PersonSearch.EntryType = 56;
PersonSearch.LookupFamily = "Person"; 

Group.AddEntry(PersonSearch);
Container.AddBlock(Group);


eWare.SetContext("**&EntityName&**", Id);


if (eWare.Mode < Edit) eWare.Mode = Edit;

if (eWare.Mode == Edit) {

   eWare.GetCustomEntityTopFrame("**&EntityName&**");
   Container.AddButton(eWare.Button("Cancel", "cancel.gif", eWare.URL("**&EntityName&**/**&EntityName&**People.asp")+"&E=**&EntityName&**&**&idfield&**="+Id));
   eWare.AddContent(Container.Execute());
   Response.Write(eWare.GetPage());

}
else if (eWare.Mode == Save) {
   //Set the foreign key on the selected person record to link back to this **&EntityName&**
   PersonId = Request.Form("SearchPerson");
   if (PersonId != '') {
      sql = "Update Person SET **&foreignkey&**=" + Id + " WHERE Pers_PersonId=" + PersonId;
      eWare.ExecSql(sql);
   }

   //redirect to the **&EntityName&**Person.asp page that shows the list of people for this **&EntityName&**
   var Url = eWare.URL("**&EntityName&**/**&EntityName&**Person.asp")+"&E=**&EntityName&**&**&idfield&**="+Id
   Response.Redirect(Url);
   

}



%>




