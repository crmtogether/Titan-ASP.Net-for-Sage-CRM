<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="$safeitemname$.aspx.cs" Inherits="$rootnamespace$.$classname$"  ValidateRequest="false" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link type="text/css" rel="Stylesheet" href="eware.css" />
    <title>Sage CRM Plain Page</title>
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
