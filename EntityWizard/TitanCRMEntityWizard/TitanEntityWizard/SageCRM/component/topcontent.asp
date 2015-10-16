<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  Titan ASP.NET Component Suite for Sage CRM
*/
//******************************************************************************
try{

  var EntityName=Request.QueryString("EntityName");
  var EntryBlockName=Request.QueryString("EntryBlockName");
  var EntityWhere=new String(Request.QueryString("EntityWhere"));

  EntityWhere_arr=EntityWhere.split("=");
  EntityWhere_valstr=new String(EntityWhere_arr[1]);
  EntityWhere_valstr_arr=EntityWhere_valstr.split(",");
  EntityWhere=EntityWhere_arr[0]+"="+EntityWhere_valstr_arr[0];
 
  var icon=Request.QueryString("icon");
   
  b_settabgroup=false;
  
  qstr=new String(Request.Querystring("J"));
  tabgroup=new String("");
  if (qstr.indexOf("NewEntity.aspx")>0)
  {
    tabgroup="new";
  }else
  if (qstr.indexOf("FindEntity.aspx")>0)
  {
    tabgroup="find";  
  }else
  if (qstr.indexOf("User.aspx")>0)
  {
    tabgroup="user";  
  }
  if ((tabgroup=="new") || (tabgroup=="find") || (tabgroup=="user"))
  {
  /*
      if (tabgroup=="newentity")
        tabgroup="New";
      eWare.SetContext(tabgroup);
      eWare.Mode=1;
      b_settabgroup=true;
      container=eWare.GetBlock("container");      
      block=eWare.GetBlock(EntryBlockName);
      container.AddBlock(block);
      record=eWare.CreateRecord(EntityName);
      container.Execute(record);      
      var page=new String(eWare.GetPage(tabgroup));
      Response.Write(page);
      */
  } 
  else
  {
      Response.Write("<DIV STYLE=\"visibility:hidden;display:none\" >");  
      %>
    <!-- #include file ="old_topcontent.asp" -->
    <%
          Response.Write("</DIV>");  
          Response.Write("<SCRIPT LANGUAGE=\"JAVASCRIPT\">");
          Response.Write("var strTopContentHTML=document.getElementById('SageCRMTopContentData');");
          Response.Write("parent.frames[3].WriteToFrame(5,\"TOPBODY VLINK=NAVY LINK=NAVY\",strTopContentHTML.innerHTML); ");
          Response.Write("</SCRIPT>");
  }

        Response.End(); 
}catch(e){
  logerror(e);
}
%>