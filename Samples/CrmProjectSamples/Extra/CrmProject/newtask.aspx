<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newtask.aspx.cs" Inherits="newtask" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href="eware.css" />   
    <link type="text/css" rel="Stylesheet" href="/crm61/eware.css" />   
</head>
<body>
    <form id="EntryForm" runat="server">
    <div>
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMConnection id="SageCRMConnection" runat="server" CRMPath="http://localhost/crm61" SID="6912942136907">
</SageCRM:SageCRMConnection><SageCRM:SageCRMTabGroup id="SageCRMTabGroup" runat="server" EntityName="CrmProject" TabGroupName="CRMProject"></SageCRM:SageCRMTabGroup></td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock" runat="server" AfterSavePage="CrmProject/tasklist.aspx"
                        CreateMode="True" EntityName="projecttasks" EntityWhere="task_projecttasksid=-1"
                        EntryBlockName="tasksscreen" />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    &nbsp;&nbsp;
                    <SageCRM:SageCRMButtonSave ID="SageCRMButtonSave" runat="server" />
                </td>
            </tr>
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                &nbsp;&nbsp;
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
