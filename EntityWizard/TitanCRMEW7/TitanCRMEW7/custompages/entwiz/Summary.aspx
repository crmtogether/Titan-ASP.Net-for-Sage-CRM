<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Summary" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href=<%=SageCRMTabGroup.getInstallName(Request.RawUrl.ToString())%>/Themes/color.css />   
</head>
<body>
    <form id="EntryForm" runat="server">
    <div>
        <SageCRM:SageCRMConnection ID='SageCRMConnection' runat="server" CRMPath="http://localhost/crm" SID="30421626033752" />
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMTabGroup ID="SageCRMTabGroup" runat="server" EntityName="xxxxxxxxx"
                        TabGroupName="aspxxxxxxx" />
                </td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock" runat="server" BlockTitle="xxxxxxxxx"
                        EntityName="xxxxxxxxx" EntityWhere="xxxx_xxxxxxxxxID=-1" EntryBlockName="xxxxxxxxxNewEntry" ShowWorkFlowButtons="True" WorkFlowName="xxxxxxxxxWorkflow" />
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    <SageCRM:SageCRMButtonChange ID="SageCRMButtonChange" runat="server" />
                </td>
            </tr>
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                &nbsp;&nbsp;
                    <SageCRM:SageCRMTopContent ID="SageCRMTopContent" runat="server" EntityName="xxxxxxxxx" EntityWhere="xxxx_xxxxxxxxxID=-1" EntryBlockName="xxxxxxxxxTopContent" />
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
