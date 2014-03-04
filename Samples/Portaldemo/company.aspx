<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="Default2" Title="Company" %>

<%@ MasterType VirtualPath="~/portal.master" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/support.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="69326254549019" />
    <SageCRM:SageCRMDataSource ID="SageCRMDataSource1" runat="server" TableName="company"
        WhereClause="comp_companyid=-1">
    </SageCRM:SageCRMDataSource>
    <asp:Label ID="lbl_companyinfo" runat="server" CssClass="centretext" Text="lbl_companyinfo"></asp:Label><br />
    <table style="width: 160px">
        <tr valign="top" >
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr valign="top">
            <td width="78%" >
    <SageCRM:SageCRMPortalEntryBlock ID="SageCRMPortalEntryBlock" runat="server" EntryBlockName="PortalCompanyBoxLong" BlockTitle="" />
            </td>
            <td width="2%" >&nbsp;
            </td>
            <td width="20%" style="valign:top">
                </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Button ID="Button1" runat="server" Text="Change" OnClick="Button1_Click" Width="100px" />            
                &nbsp;</td>
            <td>
            </td>
            <td style="width: 39px">
            </td>
        </tr>
    </table>
</asp:Content>

