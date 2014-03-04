<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="logon.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/support.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lbl_logoninfo" runat="server" CssClass="centretext" Text="lbl_logoninfo"></asp:Label>
    <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4"
        BorderStyle="Solid" BorderWidth="1px" DisplayRememberMe="False" Font-Names="Verdana"
        Font-Size="0.8em" ForeColor="#333333" OnAuthenticate="Login1_Authenticate" TitleText="Portal Logon"
        Width="272px" DestinationPageUrl="cases.aspx">
        <TextBoxStyle Font-Size="0.8em" />
        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
    </asp:Login>
    &nbsp; &nbsp;&nbsp;
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="69326254549019" />
    <SageCRM:SageCRMBaseClass ID="SageCRMBaseClass1" runat="server" />
</asp:Content>

