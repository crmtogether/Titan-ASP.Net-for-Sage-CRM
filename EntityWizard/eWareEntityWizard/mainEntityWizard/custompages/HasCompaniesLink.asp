<!-- #include file ="..\crmwizard.js" -->

<%

/* Search for an existing company and link it to the current **&EntityName&** record */


var Id = new String(Request.Querystring("**&idfield&**"));

if (Id.toString() == 'undefined') {
   Id = new String(Request.Querystring("Key58"));
}



Container=eWare.GetBlock("container");
Group = eWare.GetBlock("entrygroup");

CompanySearch = eWare.GetBlock("entry");

CompanySearch.FieldName = "SearchCompany";
CompanySearch.EntryType = 56;
CompanySearch.LookupFamily = "Company"; 

Group.AddEntry(CompanySearch);
Container.AddBlock(Group);


eWare.SetContext("**&EntityName&**", Id);


if (eWare.Mode < Edit) eWare.Mode = Edit;

if (eWare.Mode == Edit) {

   eWare.GetCustomEntityTopFrame("**&EntityName&**");
   Container.AddButton(eWare.Button("Cancel", "cancel.gif", eWare.URL("**&EntityName&**/**&EntityName&**Company.asp")+"&E=**&EntityName&**&**&idfield&**="+Id));
   eWare.AddContent(Container.Execute());
   Response.Write(eWare.GetPage());

}
else if (eWare.Mode == Save) {
   //Set the foreign key on the selected company record to link back to this **&EntityName&**
   CompanyId = Request.Form("SearchCompany");
   if (CompanyId != '') {
      sql = "Update Company SET **&foreignkey&**=" + Id + " WHERE Comp_CompanyId=" + CompanyId;
      eWare.ExecSql(sql);
   }

   //redirect to the **&EntityName&**Company.asp page that shows the list of companies for this **&EntityName&**
   var Url = eWare.URL("**&EntityName&**/**&EntityName&**Company.asp")+"&E=**&EntityName&**&**&idfield&**="+Id
   Response.Redirect(Url);
   

}



%>




