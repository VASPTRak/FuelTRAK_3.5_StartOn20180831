<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InventoryPopup.aspx.vb"
    Inherits="InventoryPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Entry Type</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="Javascript/InventoryPopUp.js"></script>
    
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
            <center>
                <table class="ThreeHundredPXTable">
                <tr>
                    <td class="MainHeader" colspan="2" style="height: 50px">
                        <asp:Label ID="InvPopUp" runat="server" text="New Tank Inventory Type"></asp:Label>
                    </td>
                </tr>
                    <tr>
                        <td style="width: 150px; height: 85px">
                            <asp:Button ID="btnFuelDel" runat="server" Text="Fuel Delivery" CssClass="InvPopButton" />
                        </td>
                        <td style="width: 150px; height: 85px">
                            <asp:Button ID="btnTankSet" runat="server" Text="Tank Setting" CssClass="InvPopButton"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px; height: 85px">
                            <asp:Button ID="btnTankDip" runat="server" Text="Tank Dipping" CssClass="InvPopButton" />
                        </td>
                        <td style="width: 150px; height: 85px">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="InvPopButton"/>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
