<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SlideShow.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SlideShow</title>
    
     <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}


 </script>
    
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
    <div align="center">
        &nbsp;</div>
    </form>
</body>
</html>