<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindEntity.aspx.cs" Inherits="FindEntity" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href=<%=SageCRMEntryBlock.getInstallName(Request.RawUrl.ToString())%>/Themes/color.css />   
</head>
<body>
    <form id="EntryForm" runat="server">
    <div>
        <SageCRM:SageCRMConnection ID='SageCRMConnection' runat="server" CRMPath="http://localhost/crm" SID="174751497732771" />
        <SageCRM:SageCRMEntryBlock ID="SageCRMEntryBlock" runat="server" BlockTitle="xxxxxxxxx"
            EntityName="xxxxxxxxx" EntityWhere="xxxx_xxxxxxxxxID=-1" EntryBlockName="xxxxxxxxxSearchBox"
            ListBlockName="xxxxxxxxxGrid" SearchMode="True" ShowSearchList="True" />
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
    </div>
    </form>
</body>
</html>
