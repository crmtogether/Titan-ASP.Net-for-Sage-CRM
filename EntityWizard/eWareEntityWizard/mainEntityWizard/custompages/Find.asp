<!-- #include file ="..\crmwizard.js" -->

<%

eWare.SetContext("Find");

if (eWare.Mode<Edit) eWare.Mode=Edit;

var sURL=new String( Request.ServerVariables("URL")() + "?" + Request.QueryString );

searchEntry=eWare.GetBlock("**&searchEntry&**");
searchEntry.Title=eWare.GetTrans("Tabnames","Search");
searchEntry.ShowSavedSearch=true;
searchEntry.UseKeyWordSearch=true;

searchList=eWare.GetBlock("**&searchList&**");
searchContainer=eWare.GetBlock("container");

searchContainer.ButtonTitle="Search";
searchContainer.ButtonImage="Search.gif";

searchContainer.AddBlock(searchEntry);
if( eWare.Mode != 6)
  searchContainer.AddBlock(searchList);

searchContainer.AddButton(eWare.Button("Clear", "clear.gif", "javascript:document.EntryForm.em.value='6';document.EntryForm.submit();"));

searchList.prevURL=sURL;
eWare.AddContent(searchContainer.Execute(searchEntry));

Response.Write(eWare.GetPage());

%>