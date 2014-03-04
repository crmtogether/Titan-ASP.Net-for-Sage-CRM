<%@ Page Language="C#" AutoEventWireup="true" CodeFile="$codebesiderootname$.cs" Inherits="$codebesideclassname$"  ValidateRequest="false" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href="eware.css" />   
</head>
<body>
    <form id="EntryForm" runat="server">
    <div>
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
      <table style="height: 500px; vertical-align:top" width="100%" border="0"  cellpadding="0" cellspacing="0" >
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                    &nbsp;</td>
            </tr>
            <tr valign="top" >
                <td style="width: 2%; height: 90%" valign="top" >&nbsp;</td>
                <td style="width: 77%; height: 90%" valign="top" >
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                </td>
                <td style="width: 20%; height: 90%" valign="top" >
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr valign="top" >
                <td colspan="3" style="height: 5%" valign="top" >
                &nbsp;&nbsp;
                </td>
            </tr>
        </table>    
    </div>
    </form>
<script type="text/javascript" language="javascript">

        function setContextInt() {
            //fix for .net4.0
            var frm = document.getElementById("EntryForm");
            //document.EntryForm=frm;
            frm.name = "EntryForm";
        }
        window.attachEvent("onload", setContextInt);
</script>	
</body>
</html>
