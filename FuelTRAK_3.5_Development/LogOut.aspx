<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LogOut.aspx.vb" Inherits="LogOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LogOut Page</title>
    <script type="text/javascript">
    history.forward();
    function chksesion()
	  {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	  }
    </script>
</head>
<body bgcolor="#336699" style="font-size: 12pt">
    <form id="form1" runat="server">
    <table height=100% width=100% align=center>
    <tr>
    <td align=center valign=middle>
    <table>
    <tr>
    <td>
    </td>
    </tr>
    
    <tr>
    <td>
    </td>
    </tr>
    
    <tr>
    <td>
    </td>
    </tr>
    
    </table>
    
        <table style="width: 100%">
            <tr><td align="center">
        <table border="0" style="width:36%; border-top-width: 1px; border-left-width: 1px; border-left-color: black; border-top-color: black; border-right-style: none; border-bottom-style: none; left: 282px; top: 47px; height: 1px; text-align: center;">
            <tr bgcolor="silver">
                <td align="center" colspan="3" bcolor=silver><FONT face="Arial" color="#0000cc" style="vertical-align: middle; text-align: center"  >
                            <span style="color: red; font-family: Times New Roman; border-top-style: none; font-weight:bold; border-right-style: none; border-left-style: none; border-bottom-style: none; width: 50%;">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                    Text="Log Off" Width="50%"></asp:Label></span></FONT></td>
            </tr>
            
           
            <tr>
                <td align="Center" colspan="3">
                    <font face="Arial" style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"><a href ="loginPage.aspx" target="_parent" style="border-top-width: 1px; border-left-width: 1px; border-left-color: orangered; 
                    border-bottom-width: 1px; border-bottom-color: orangered; color: white; border-top-color: orangered; font-family: Arial; border-right-width: 1px; font-variant: normal; border-right-color: orangered" shape="rect">
                        <span style="font-size: 10pt">Log off</span></a></font></td>
                    
            </tr>
        </table>
                </td>
            </tr>
        </table>
        
        </td>
    </tr>
    </table>    
    
    </form>
</body>
</html>