<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/portal.master" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/support.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="69326254549019" />
    <SageCRM:SageCRMBaseClass ID="SageCRMBaseClass1" runat="server" />
    <asp:Label ID="lbl_logoutinfo" runat="server" CssClass="centretext" Text="lbl_logoutinfo"></asp:Label>
</asp:Content>

