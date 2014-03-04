<!-- #include file ="SageCRM.js" -->
<%

try
{
  var lookupfamily=Request.QueryString('lookupfamily');
    
  Container=eWare.GetBlock("container");
  Group = eWare.GetBlock("entrygroup");
  
  CompanySearch = eWare.GetBlock("entry");
  
  CompanySearch.FieldName = "Search"+lookupfamily;
  CompanySearch.EntryType = 56;
  CompanySearch.LookupFamily = lookupfamily; 
  
  Group.AddEntry(CompanySearch);
  Container.AddBlock(Group);
  
  Response.Write(Container.Execute());

}catch(e){
  logerror(e);
}
%>