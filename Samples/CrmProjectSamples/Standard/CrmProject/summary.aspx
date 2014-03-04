<%@ Page Language="C#" AutoEventWireup="true" CodeFile="summary.aspx.cs" Inherits="summary" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <link href="eware.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href="/crm61/eware.css" />   
    <link href="/crm61/eware.css" rel="stylesheet" />
    <link href="/crm61/eware.css" rel="stylesheet" />
</head>
<body>
<SageCRM:SageCRMConnection id="SageCRMConnection" runat="server" CRMPath="http://localhost/crm61" SID="131293806331909"></SageCRM:SageCRMConnection>    
    <form id="EntryForm" runat="server">
    <div>
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMTabGroup ID="SageCRMTabGroup" runat="server" EntityName="CrmProject" TabGroupName="CRMProject" />
                </td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock" runat="server" BlockTitle="Crm Project"
                        EntityName="CrmProject" EntityWhere="proj_projectid=6000" EntryBlockName="SummaryCrmProjectscreen" />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    <SageCRM:SageCRMButtonChange ID="SageCRMButtonChange" runat="server" />
                    <br />
                    <SageCRM:SageCRMButton ID="SageCRMButton" runat="server" Caption="Javascript Demo"
                        Url="javascript:alert('test message');" />
                </td>
            </tr>
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                &nbsp;&nbsp;
                    <SageCRM:SageCRMTopContent ID="SageCRMTopContent" runat="server" EntityName="CrmProject"
                        EntityWhere="proj_projectid=-1" EntryBlockName="TopContentCrmProjectScreen" />
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
