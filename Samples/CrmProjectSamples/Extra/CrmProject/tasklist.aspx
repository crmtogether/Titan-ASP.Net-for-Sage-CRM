<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tasklist.aspx.cs" Inherits="tasklist" %>

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
        <SageCRM:SageCRMConnection id="SageCRMConnection" runat="server" SID="6912942136907" CRMPath="http://localhost/crm61">
</SageCRM:SageCRMConnection>
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
<SageCRM:SageCRMTabGroup id="SageCRMTabGroup" runat="server" TabGroupName="CRMProject" EntityName="CrmProject"></SageCRM:SageCRMTabGroup>                
                    <SageCRM:SageCRMDataSource ID="SageCRMDataSource1" runat="server">
                    </SageCRM:SageCRMDataSource>
                    </td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <SageCRM:SageCRMListBlock ID="SageCRMListBlock" runat="server" BlockTitle="Tasks"
                        EntityName="projecttasks" EntityWhere="task_projecttasksid=-1" ListBlock="tasklist" />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    &nbsp;&nbsp;
                    <SageCRM:SageCRMButton ID="SageCRMButton" runat="server" Caption="New" ImageName="New.gif"
                        Url="CrmProject/newtask.aspx" />
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
