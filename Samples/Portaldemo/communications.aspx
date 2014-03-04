<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="communications.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/portal.master" %>
<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/support.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="69326254549019" />
    <table>
        <tr valign="top" >
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr valign="top" >
            <td style="width:78%" ><SageCRM:SageCRMPortalListBlock BlockTitle="" EntityName="Communication" EntityWhere="" ID="SageCRMPortalListBlock" ListBlock="Portalcommunications" runat="server" FilterBlockName="PortalCommFilter" />
                &nbsp;</td>
            <td  style="width:2%" >
            &nbsp;
            </td>
            <td style="width:20%" >
                <SageCRM:SageCRMPortalFilterBlock ID="SageCRMPortalFilterBlock" runat="server" BlockTitle="Filter" EntityName="Communication" EntryBlockName="PortalCommFilter" />
            </td>
        </tr>
        <tr valign="top" >
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

