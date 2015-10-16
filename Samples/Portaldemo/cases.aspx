<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="cases.aspx.cs" Inherits="Default2" Title="Support" %>
<%@ MasterType VirtualPath="~/portal.master" %>
<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        PortalUserName="gate" PortalUserPassword="gate" SID="56027657632658" DevelopmentPort="3894" />
    <SageCRM:SageCRMDataSource ID="SageCRMDataSource1" runat="server" SelectSQL="select * from custom_tabs where tabs_entity='PortalSupport'">
    </SageCRM:SageCRMDataSource>
    <asp:DataList ID="DataList1" runat="server" DataSourceID="SageCRMDataSource1" RepeatColumns="3" ShowFooter="False" ShowHeader="False">
        <ItemTemplate>
        <a href="<%#Eval("tabs_customfilename")%>">
            <%#Eval("tabs_caption")%></a>
        </ItemTemplate>
        <ItemStyle CssClass="centretext" Width="400px" Wrap="False" />
        <SeparatorStyle Width="10px" />
        <SelectedItemStyle ForeColor="Olive" />
       
    </asp:DataList>
    <table style="width: 100%">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width:78%">
                <SageCRM:SageCRMPortalListBlock ID="SageCRMPortalListBlock" runat="server" BlockTitle="" EntityName="Cases" EntityWhere="case_caseid=-1" FilterBlockName="PortalCaseFilter" ListBlock="PortalCaseList" />
            </td>
            <td style="width:2%">&nbsp;
            </td>
            <td style="width:20%" valign="top">
                &nbsp;<SageCRM:SageCRMPortalFilterBlock ID="SageCRMPortalFilterBlock" runat="server"
                    EntryBlockName="PortalCaseFilter" BlockTitle="Filter"  />
            </td>
        </tr>
        <tr>
            <td>
                </td>
            <td>
            </td>
            <td>
                </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <img height="217" src="gif/support.jpg" width="623" /></asp:Content>

