<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MainMenu.aspx.vb" Inherits="MainMenu" %>

<%@ Register Src="~/UserControl/Header.ascx" TagName="header" TagPrefix="cont" %>
<%@ Register Src="~/UserControl/LeftMenu.ascx" TagName="header1" TagPrefix="menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FuelTrak : Fuel Management System Software </title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="javascript/Validation.js"></script>
    

    <script type="text/javascript">
        if(window.history)
        {
          window.history.forward(1);
        }        
        function Resize()
        {		
	        window.moveTo(0,0);	        	        
            window.resizeTo(screen.availWidth,screen.availHeight);	        
        }
        function OpenHelp()
        {    window.open('Help/TrakHelp.htm');return false;}
        
        function goToPage7631(mySelect)
        {    frames['PageFrame'].location.href = mySelect;}

        function hst()
        {    history.forward();}
        
        function chksesion()
	  {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	  }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table border="0" class="MainMenuTable">
                <tr>
                    <td style="text-align: left; vertical-align: top">
                        <asp:Label ID="Label1" runat="server" Width="100px"></asp:Label>
                        <menu:header1 ID="menu" runat="server" /></td>
                    <td style="text-align: left; vertical-align: top">
                        <iframe name="PageFrame" class="MainMenuFrame" runat="server" frameborder="0" scrolling="yes" 
                            src="Home.aspx" id="IFRAME1" style="width: 1024px; height: 800px;"></iframe>
                    </td>
                    <a href="http://www.milonic.com/beginner.php" style="display:none">http://www.milonic.com/beginner.php</a>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
