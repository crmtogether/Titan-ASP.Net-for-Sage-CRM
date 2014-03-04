<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cases.aspx.cs" Inherits="Cases" %>

<%@ Register Assembly="SageCRM" Namespace="SageCRM.AspNet" TagPrefix="SageCRM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href=<%=SageCRMTabGroup.getInstallName(Request.RawUrl.ToString())%>/Themes/color1.css />   
<meta http-equiv="cache-control" content="no-cache" />
<meta http-equiv="pragma" content="no-cache" />
<meta http-equiv="expires" content="-1" />    
</head>
<body>
    <SageCRM:SageCRMConnection ID='SageCRMConnection' runat="server" CRMPath="http://localhost/crm" SID="86651472463233" />
    <form id="EntryForm" runat="server">
    <div>
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    <SageCRM:SageCRMTabGroup ID="SageCRMTabGroup" runat="server" EntityName="xxxxxxxxx"
                        TabGroupName="aspxxxxxxx" />
                </td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    <SageCRM:SageCRMListBlock ID="SageCRMListBlock" runat="server" BlockTitle="" EntityName="Cases"
                        EntityWhere="Case_CaseId=-1" ListBlock="CaseGrid" />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    &nbsp;<SageCRM:SageCRMButton ID="SageCRMButton" runat="server" Caption="New" ImageName="New.gif"
                        Url="282" />
                    &nbsp;
                </td>
            </tr>
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                <SageCRM:SageCRMTopContent ID="SageCRMTopContent" runat="server" EntityName="xxxxxxxxx" EntityWhere="xxxx_xxxxxxxxxID=-1" EntryBlockName="xxxxxxxxxTopContent" />
                    <SageCRM:SageCRMDataSource ID="SageCRMDataSource1" runat="server">
                    </SageCRM:SageCRMDataSource>
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
