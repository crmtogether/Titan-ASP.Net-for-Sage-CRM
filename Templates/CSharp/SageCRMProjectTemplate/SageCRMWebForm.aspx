<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SageCRMWebForm.aspx.cs" Inherits="$safeprojectname$.SageCRMWebForm"  ValidateRequest="false" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sage CRM</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link type="text/css" rel="Stylesheet" href="eware.css" />   
    <script type="text/javascript" language="javascript" src="crmclient.js" ></script>
</head>
<body>
    <form id="EntryForm" runat="server">
    <div>
<input type="hidden" name="yearEntry" />
<input type="hidden" name="monthEntry" />
<input type="hidden" name="dayEntry" />    
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
