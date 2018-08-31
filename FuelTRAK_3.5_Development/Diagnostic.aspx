<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Diagnostic.aspx.vb" Inherits="Diagnostic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diagnostic</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <link href="Stylesheet/start/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
    function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

         }
     </script>
    <style type="text/css">
        .style1
        {
            width: 33%;
        }
        .style3
        {
            width: 250px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" width="70%">
            <tr>
                <td align="center" valign="middle">
                    <asp:Label ID="lblDiagnostic" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Medium" ForeColor="Navy" Text="Sentry LIVE Diagnostic"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div style="border:10px solid White;" >
        <table width="70%">
         <tr>
                <td align="center" valign="middle" class="style1">
                    <asp:Label ID="lblSentry" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                        ForeColor="Navy" Text="Sentry # 001 – Public Works"></asp:Label>
                </td>
            </tr></table>
        <br />
        <br />
        <table align="center" border="0" width="80%">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                        ForeColor="Navy" Text="Status:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                        ForeColor="White" Text="Online / Communicating to Fuel Island / Sentry"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                        ForeColor="Navy" Text="Hose1:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                        ForeColor="White" Text="Working"></asp:Label> 
                </td>
                <td>
                    <asp:Label ID="lblMannual1" runat="server" Text="Put into Manual Override?" 
                        Font-Bold="True"></asp:Label>
                    <asp:RadioButton ID="rbnYes1" runat="server" Text = "Yes" AutoPostBack="True" />
                    <asp:RadioButton ID="rbnNo1" runat="server" Text = "No" Checked="True" 
                        AutoPostBack="True"/>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                        ForeColor="Navy" Text="Hose2:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStatus1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                        ForeColor="White" Text="Working"></asp:Label>
                </td>
                <td>
                     <asp:Label ID="lblMannual2" runat="server" Text="Put into Manual Override?" 
                         Font-Bold="True"></asp:Label>
                    <asp:RadioButton ID="rbnYes" runat="server" Text = "Yes" AutoPostBack="True"/>
                    <asp:RadioButton ID="rbnNo" runat="server" Text = "No" Checked="True" 
                         AutoPostBack="True"/>
                </td>
            </tr>
        </table>
        <br /><br /><br /><br /><br /><br /><br />
        <table align="center" border="0" width="100%">
            <tr>
                <td align="center" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" class="style1">
                    <asp:Button ID="btnDisconnect" runat="server" Text="Disconnect" onclientclick="window.close()"/>
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
