<!-- #include file ="SageCRM.js" -->
<%


  
Response.Write("<HTML><HEAD><LINK REL=\"stylesheet\" HREF=\"/"+sInstallName+"/themes/color1.css\"><META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
		Body="<BODY>";
		EndBody="</BODY></HTML>";
		
		Response.Write(Body);
		
		eWare.Mode=Edit;


  var TableName="crreportsParams";
  var WhereClause="crpa_crmreportsid=" + Request.QueryString('crre_crreportsid');
  var FieldName="crpa_paramvalue";  
    
  var EntityName=new String("Orders");
  
  Container=eWare.GetBlock("container");
  block = eWare.GetBlock("HoldingBlock");
  Container.AddBlock(block);

  var record=eWare.FindRecord(TableName,WhereClause);
  
  var result=new String("");
	
		while (!record.eof)
		{
			try
			{
				block.AddEntry(record[FieldName]);
			}	
			catch(e)    
			{
			}
			record.NextRecord();
		}
  
  Container.DisplayForm=true;
  block.DisplayForm=false;
  block.DisplayButton(Button_Default) = false;
  Container.DisplayButton(Button_Default) = false;
  Container.ButtonTitle="Search";
  Container.ButtonImage="Search.gif";

  Container.AddButton(eWare.Button("Run Report", "save.gif", "javascript:runreport()"));
 
  Response.Write(Container.Execute());
  

%>
<script language="Javascript">

  function runreport()
  {

	document.forms[0].action="Default.aspx?crre_crreportsid=<%=Request.QueryString('crre_crreportsid')%>&SID=<%=Request.QueryString('SID')%>";
	document.forms[0].submit();
  }

</script>
<%
Response.Write(EndBody);
%>