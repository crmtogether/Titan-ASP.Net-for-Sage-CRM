<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewCrmProject.aspx.cs" Inherits="SageCRMWebForm2" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href=" /crm61/eware.css" />   
</head>
<body>
    <form id="EntryForm" runat="server">
    <div>
<SageCRM:SageCRMConnection id="SageCRMConnection" runat="server" CRMPath="http://localhost/crm61" SID="103583643736904"></SageCRM:SageCRMConnection>    
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    &nbsp;</td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock" runat="server" BlockTitle="New CRM Project"
                        CreateMode="True" EntityName="CrmProject" EntityWhere="proj_projectid=-1" EntryBlockName="newCrmProjectscreen" AfterSavePage="CrmProject/summary.aspx" />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    &nbsp;<SageCRM:SageCRMButtonSave ID="SageCRMButtonSave" runat="server" />
                    &nbsp;
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
