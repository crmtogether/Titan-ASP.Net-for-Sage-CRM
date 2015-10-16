<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

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
<SageCRM:SageCRMConnection id="SageCRMConnection" runat="server" SID="131293806331909" CRMPath="http://localhost/crm61" Keys="&Key0=1&Key1=50&Key2=64&T=Company"></SageCRM:SageCRMConnection>    
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMTabGroup ID="SageCRMTabGroup" runat="server" />
                </td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <SageCRM:SageCRMListBlock ID="SageCRMListBlock" runat="server" BlockTitle="Crm Projects List"
                        EntityName="CrmProject" EntityWhere="proj_projectid=-1" ListBlock="ListCrmProject" FilterBlockName="FilterCrmProjectScreen" />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    <SageCRM:SageCRMFilterBlock ID="SageCRMFilterBlock" runat="server" EntityName="CrmProject"
                        EntryBlockName="FilterCrmProjectScreen" />
                    &nbsp;&nbsp;
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
