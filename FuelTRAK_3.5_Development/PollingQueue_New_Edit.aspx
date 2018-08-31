<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PollingQueue_New_Edit.aspx.vb"
    Inherits="PollingQueue_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Polling Queue Add Update</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

</head>

 <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

 </script>
 
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" width="SixHundredPXTable">
                <tr>
                    <td valign="top">
                        <table align="center" id="tblEditDept" runat="server" class="ThreeHundredPXTable">

                            <tr>
                                <td class="MainHeader" colspan="2" align="center" style="height:50px; vertical-align:bottom">
                                    <asp:Label ID="Label1" runat="server" Text="Edit Tank"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width:150px; height:24px"></td>
                                <td style="width:150px; height:24px"></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height:32px">
                                    <asp:Label ID="Label2" runat="server" Text="Device Id:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlDeviceId" runat="server" CssClass="TenCharTxtBox" Height="24px" TabIndex="1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height:32px">
                                    <asp:Label ID="Label3" runat="server" Text="Device Type:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblDeviceType" runat="server" CssClass="FifteenCharTxtBox" BackColor="White" Height="24px" TabIndex="2"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height:32px">
                                    <asp:Label ID="Label4" runat="server" Text="Command:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCommand" runat="server" CssClass="EightCharTxtBox" Height="24px" TabIndex="3">
                                        <asp:ListItem>Poll</asp:ListItem>
                                        <asp:ListItem>Test</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2" class="NewDept_ButtonTD" style="height: 50px">
                                    <asp:Button ID="btnOK" runat="server" Text="Save" Width="100px" AccessKey="s" TabIndex="4" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" CausesValidation="False"
                                        AccessKey="c" TabIndex="5" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
