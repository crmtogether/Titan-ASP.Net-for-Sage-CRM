<!-- #include file ="..\crmwizard.js" -->

<%

Container=eWare.GetBlock("container");
List=eWare.GetBlock("**&ProgressListName&**");
Container.AddBlock(List);
Container.DisplayButton(1)=false;

var Id = new String(Request.Querystring("**&idfield&**"));

if (Id.toString() == 'undefined') {
   Id = new String(Request.Querystring("Key58"));
}

var Idarr = Id.split(",");

eWare.SetContext("**&tabgroup&**", Idarr[0]);

eWare.AddContent(Container.Execute('**&idfield&**='+Idarr[0]));
eWare.GetCustomEntityTopFrame("**&EntityName&**");
Response.Write(eWare.GetPage());


%>







