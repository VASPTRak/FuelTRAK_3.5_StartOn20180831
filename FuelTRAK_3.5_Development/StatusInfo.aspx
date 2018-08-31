<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatusInfo.aspx.vb" Inherits="CreateUser" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CreateUser</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="javascript/CommonFunctions.js"></script>

    <script type="text/javascript" language="javascript" src="javascript/Validation.js"></script>

    <script type="text/javascript" language="javascript" src="javascript/PollSetup.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/User.js"></script>

    <script language="javascript" type="text/javascript">
     function chksesion()
                    {   window.location.assign('LoginPage.aspx')        }
        function check()
		 {
		    var ac = confirm('Are you sure you want to Update Customer Status ?');
			document.getElementById('Hidtxt').value = ac;
			document.form1.submit(); 
			
		 }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table align="center" class="EightHundredPXTable">
                <tr>
                    <td valign="top" align="center">
                        <table class="SevenHundredPXTable">
                            <tr>
                                <td colspan="3" class="MainHeader" style="height: 50px">
                                    <asp:Label ID="Label7" runat="server" Text="System Status"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="width: 150px">
                                    <asp:Label ID="Label1" runat="server" Text="Fleet Owner :"></asp:Label></td>
                                <td class="Status_TextBox" style="width: 350px">
                                    <asp:TextBox ID="txtOwner" runat="server" CssClass="ThirtyCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label2" runat="server" Text="System Type:"></asp:Label></td>
                                <td style="text-align: left; width: 350px;">
                                    <asp:DropDownList ID="ddlSentry" runat="server" Font-Names="arial" CssClass="FifteenCharTxtBox"
                                        Font-Size="Small" Width="136px">
                                        <asp:ListItem>Sentry 5</asp:ListItem>
                                        <asp:ListItem>Sentry 6</asp:ListItem>
                                        <asp:ListItem Selected="True">Sentry Gold</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="Label3" runat="server" Text="System Number:"></asp:Label></td>
                                <td class="Status_TextBox" style="height: 35px; width: 350px;">
                                    <asp:TextBox ID="txtSysNo" runat="server" CssClass="FourCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled"></asp:TextBox>
                                    <input type="hidden" id="Hidtxt" runat="server" class="VehicleHidden" /></td>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="lblTXTNCost" runat="server" Text="Costing Method:"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="lblCommPort" runat="server" Text="COM Port #:"></asp:Label></td>
                                <td class="Status_TextBox" style="height: 35px; width: 350px;">
                                    <asp:TextBox ID="txtCommPort" runat="server" CssClass="TenCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Com Port"
                                        ControlToValidate="txtCommPort" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"
                                        Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                <td align="left">
                                    <table border="1">
                                        <tr>
                                            <td style="width: 50px">
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdbCosting" runat="server" Width="200px" TabIndex="7">
                                                    <asp:ListItem>Transaction Costing</asp:ListItem>
                                                    <asp:ListItem Enabled="false">Price Average Costing</asp:ListItem>
                                                    <asp:ListItem Enabled="false">Fixed Price Costing</asp:ListItem>
                                                    <asp:ListItem>First In First Out Costing</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="lblEnbrLen" runat="server" Text="Pin Lenght:"></asp:Label></td>
                                <td class="Status_TextBox" style="height: 35px; width: 350px;">
                                    <asp:TextBox ID="txtEnbrLen" runat="server" CssClass="OneCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="lblVnbrLen" runat="server" Text="Vin Lenght:"></asp:Label></td>
                                <td class="Status_TextBox" style="height: 35px; width: 350px;">
                                    <asp:TextBox ID="txtVnbrLen" runat="server" CssClass="OneCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="lblPMCnt" runat="server" Text="PM TXTN Count:"></asp:Label></td>
                                <td class="Status_TextBox" style="height: 35px; width: 350px;">
                                    <asp:TextBox ID="txtPMCnt" runat="server" CssClass="OneCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label4" runat="server" Text="Stored Records:"></asp:Label></td>
                                <td class="Status_TextBox" style="width: 350px">
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="Label5" runat="server" Text="Transactions:"></asp:Label></td>
                                <td style="text-align: left; width: 350px;">
                                    <asp:TextBox ID="txtTxtnCnt" runat="server" CssClass="EightCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="Label6" runat="server" Text="Vehicles:"></asp:Label></td>
                                <td style="text-align: left; width: 350px;">
                                    <asp:TextBox ID="txtVehCnt" runat="server" CssClass="EightCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 35px">
                                    <asp:Label ID="Label8" runat="server" Text="Personnel:"></asp:Label></td>
                                <td style="text-align: left; width: 350px;">
                                    <asp:TextBox ID="txtPersCnt" runat="server" CssClass="EightCharTxtBox" EnableViewState="False"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label9" runat="server" Text="Last Transaction Date:"></asp:Label></td>
                                <td class="Status_TextBox" style="width: 350px">
                                    <asp:TextBox ID="txtTransDate" runat="server" CssClass="TenCharTxtBox" ReadOnly="True"
                                        EnableViewState="False" BackColor="#E0E0E0" AutoCompleteType="Disabled"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label10" runat="server" Text="Last Polling Date:"></asp:Label></td>
                                <td class="Status_TextBox" style="width: 350px">
                                    <asp:TextBox ID="txtPollDate" runat="server" EnableViewState="False" CssClass="TenCharTxtBox"
                                        BackColor="#E0E0E0" AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 50px; vertical-align: bottom; text-align: center">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="False" Width="100px" />
                                    <asp:Button ID="btnCancle" runat="server" Text="Cancel" CausesValidation="False"
                                        Width="100px" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
