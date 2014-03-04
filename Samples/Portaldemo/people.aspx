<%@ Page Language="C#" MasterPageFile="~/portal.master" AutoEventWireup="true" CodeFile="people.aspx.cs" Inherits="Default2" Title="People" %>
<%@ MasterType VirtualPath="~/portal.master" %>
<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <img height="217" src="gif/support.jpg" width="623" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <SageCRM:SageCRMConnection ID="SageCRMConnection2" runat="server" CRMPath="http://localhost/crm61"
        DevelopmentPort="3894" PortalUserName="gate" PortalUserPassword="gate" SID="69326254549019" />
    <asp:Label ID="lbl_personinfo" runat="server" CssClass="centretext" Text="lbl_personinfo"></asp:Label>
    <SageCRM:SageCRMDataSource ID="SageCRMDataSource1" runat="server" SelectSQL="select count(*) as 'totalpeople' from person where pers_companyid=34">
    </SageCRM:SageCRMDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SageCRMDataSource1">
        <Columns>
            <asp:BoundField DataField="totalpeople" HeaderText="Total people">
                <HeaderStyle CssClass="centretext" />
                <ItemStyle CssClass="centretext" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <br />
    <SageCRM:SageCRMPortalListBlock ID="SageCRMPortalListBlock" runat="server" BlockTitle=""
        EntityName="person" EntityWhere="pers_personid=-1" ListBlock="PortalPersonList" />
</asp:Content>

