<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="newsletter.aspx.cs" Inherits="Default2" Title="Newsletter" %>
<%@ MasterType VirtualPath="~/portal.master" %>
<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/newsletter.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="69326254549019" />
    <asp:Label ID="lbl_newsletterinfo" runat="server" CssClass="centretext" Text="lbl_newsletterinfo"></asp:Label>
    <table>
        <tr valign="top">
            <td>
            </td>
            <td style="width: 3px">
            </td>
            <td>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <SageCRM:SageCRMPortalEntryBlock ID="SageCRMPortalEntryBlock" runat="server" AfterSavePage="registeredsuccessfully.aspx?"
                    BlockTitle="" CreateMode="True" EntityName="lead" EntityWhere="lead_leadid=-1"
                    EntryBlockName="ss_lead" />
            </td>
            <td style="width: 3px">
            </td>
            <td>
                </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Button ID="btn_save" runat="server" Text="Submit Request" OnClick="btn_save_Click" Width="100px" /></td>
            <td style="width: 3px">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

