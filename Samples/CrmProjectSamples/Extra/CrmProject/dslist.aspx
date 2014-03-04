<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dslist.aspx.cs" Inherits="dslist" %>

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
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
        <SageCRM:SageCRMConnection ID="SageCRMConnection" runat="server" CRMPath="http://localhost/crm61"
            Keys="&Key0=1&Key1=50&Key2=64&T=Company" SID="131293806331909" />
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMTabGroup ID="SageCRMTabGroup" runat="server" EntityName="CrmProject"
                        TabGroupName="CRMProject" />
                    <SageCRM:SageCRMDataSource ID="SageCRMDataSource1" runat="server" TableName="Cases"
                        WhereClause="case_caseid<10">
                    </SageCRM:SageCRMDataSource>
                </td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="case_caseid"
                        DataSourceID="SageCRMDataSource1">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="case_caseid" DataTextField="case_status"
                                HeaderText="Status" />
                            <asp:BoundField DataField="case_description" HeaderText="Description" />
                        </Columns>
                    </asp:GridView>
                    <div style="left: 400px; width: 100px; position: absolute; top: 150px; height: 100px">
                        <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock" runat="server" BlockTitle="Cases"
                            EntityName="Cases" EntityWhere="Case_CaseId=-1" EntryBlockName="CaseDetailBox" />
                    </div>
                    &nbsp;<br />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
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
