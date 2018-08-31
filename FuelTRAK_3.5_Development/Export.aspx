<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Export.aspx.vb" Inherits="Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Export Report</title>
    <script language="javascript" type="text/javascript">
    
 
        function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	
        function CloseParent()
        {
//            alert("1");
//            window.opener.close();
        }
    </script>
</head>
<body onload="CloseParent();">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
