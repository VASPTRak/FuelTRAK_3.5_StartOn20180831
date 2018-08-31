<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UndoExport.aspx.vb" Inherits="UndoExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Undo Export</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Javascript/Validation.js"></script>
    
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
        <div>
            <table id="tblExport" cellpadding="0" align="Center" class="MaximumPXTable">
                <tr align="center">
                    <td colspan="6" class="MainHeader" style="height: 50px">
                        <asp:Label ID="Label2" runat="server" Text="Undo Export"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="StdLableTxtLeft" style="height: 50px; text-align: center">
                        <asp:Label ID="lblResult" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="Small"
                            ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MaximumPXTable" align="center" colspan="6">
                        <asp:Button ID="btnUndoExport" runat="server" Text="Undo Export" Height="24px" Width="100px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
