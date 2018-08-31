<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Home</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

        function chksesion()
	    {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	    }
        function expand() {   
        window.moveTo(0,0);            
        window.resizeTo(screen.availWidth, screen.availHeight);
        }

    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <%--<table style="margin-left:20px">--%>
            <table  width="100%" align="center">
                <tr>
                    <td style="height: 200px" align="center" valign="middle">
                        <img alt="" src="images/banner.gif" height="300px" width="800px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            <strong><font size="6">We welcome your questions and comments<br />
                            </font></strong>
                            <br />
                            Send us an email, or give us a call.<br />
                            Our regular business hours are Monday - Friday, from 8:00 AM - 5:00 PM EST.<br />
                            Please provide: Company Name and Contact Information when calling or emailing.<br />
                            <br />
                            850-878-4585
                            <br />
                            <br />
                            <a href="mailto:support@trakeng.com?subject=Fuel%20Trak" class="HyperlinkInactive">support@trakeng.com</a>
                            <br />
                            <br />
                            <a href="mailto:sales@trakeng.com?subject=Fuel%20Trak" class="HyperlinkInactive">sales@trakeng.com</a>
                            <br />
                        </p>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
