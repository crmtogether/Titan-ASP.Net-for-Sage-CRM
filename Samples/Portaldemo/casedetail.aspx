<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="casedetail.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/portal.master" %>
<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/support.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="56027657632658" />
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
    <table style="width: 167px">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="width: 3px">
            </td>
        </tr>
        <tr>
            <td>
                <SageCRM:SageCRMPortalEntryBlock ID="SageCRMPortalEntryBlock" runat="server" BlockTitle=""
                    CreateMode="True" EntityName="Cases" EntityWhere="Case_CaseId=-1" EntryBlockName="sscaseentry" OnBeforeRendering="SageCRMPortalEntryBlock_BeforeRendering" 
                     />
            </td>
            <td>
            </td>
            <td style="width: 3px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Change" Width="100px" OnClick="Button1_Click" /></td>
            <td>
            </td>
            <td style="width: 3px">
                </td>
        </tr>
        <tr >
            <td>
                Add document and create
                </td>
            <td>
            </td>
            <td style="width: 3px">
                </td>        
        </tr>
        <tr >
            <td>
                Document Notes:
                <br />
                <asp:TextBox ID="libr_note" runat="server" Height="64px" TextMode="MultiLine" Width="229px"></asp:TextBox></td>
            <td>
            </td>
            <td style="width: 3px">
                </td>        
        </tr>
        <tr >
            <td>
                <SageCRM:SageCRMPortalListBlock ID="SageCRMPortalListBlock" runat="server" BlockTitle="Library" EntityName="Library" EntityWhere="libr_caseid=-1" ListBlock="ssLibraryList" />
                </td>
            <td>
            </td>
            <td style="width: 3px">
                </td>        
        </tr>
        <tr>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            <td>
            </td>
            <td style="width: 3px">
                </td>        
        </tr>
        <tr >
            <td>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Create Library Record" />
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
            <td>
            </td>
            <td style="width: 3px">
                </td>        
        </tr>        
    </table>

    <SageCRM:SageCRMDataSource ID="SageCRMDataSource2" runat="server" TableName="cases">
    </SageCRM:SageCRMDataSource>
    &nbsp;
</asp:Content>

