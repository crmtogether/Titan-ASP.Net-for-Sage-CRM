<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="opportunitydetail.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/portal.master" %>
<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/support.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="69326254549019" />
    <SageCRM:SageCRMDataSource ID="SageCRMDataSource1" runat="server" SelectSQL="select * from custom_tabs where tabs_entity='PortalSupport'">
    </SageCRM:SageCRMDataSource>
    <asp:DataList ID="DataList1" runat="server" DataSourceID="SageCRMDataSource1" RepeatColumns="3"
        ShowFooter="False" ShowHeader="False">
        <ItemTemplate>
            <a href='<%#Eval("tabs_customfilename")%>'>
                <%#Eval("tabs_caption")%>
            </a>
        </ItemTemplate>
        <ItemStyle CssClass="centretext" Width="400px" Wrap="False" />
        <SeparatorStyle Width="10px" />
        <SelectedItemStyle ForeColor="Olive" />
    </asp:DataList>
    <table style="width: 268px">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
    <SageCRM:SageCRMPortalEntryBlock ID="SageCRMPortalEntryBlock" runat="server" BlockTitle="" CreateMode="True" EntityName="Opportunity" EntityWhere="oppo_opportunityId=-1" EntryBlockName="SSOpportunityEntry" OnBeforeRendering="SageCRMPortalEntryBlock_BeforeRendering" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Change" Width="100px" /></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <SageCRM:SageCRMDataSource ID="SageCRMDataSource2" runat="server">
    </SageCRM:SageCRMDataSource>
</asp:Content>

