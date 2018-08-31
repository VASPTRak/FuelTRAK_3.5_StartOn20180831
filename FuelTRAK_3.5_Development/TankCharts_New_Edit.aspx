<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankCharts_New_Edit.aspx.vb"
    Inherits="TankCharts_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TankCharts_Edit Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
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
            <table align="center" class="NineHundredPXTable" id="tblNewChartDetail" runat="server">
                <tr>
                    <td class="MainHeader" colspan="3" style="height: 50px">
                        <asp:Label ID="lblText" runat="server" Text="Add Tank Chart"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 32px">
                    </td>
                    <td class="StdLableTxtLeft" style="width: 185px">
                        <asp:Label ID="lblChartName" runat="server" Text="Chart Name:"></asp:Label></td>
                    <td style="text-align: left; width: 215px">
                        <asp:TextBox ID="txtChartName" runat="server" CssClass="TwentyCharTxtBox" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqChartName" runat="server" ErrorMessage="Please enter chart name"
                            ControlToValidate="txtChartName" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 32px">
                    </td>
                    <td class="StdLableTxtLeft" style="width: 185px">
                        <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label></td>
                    <td style="text-align: left; width: 215px">
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="TwentyCharTxtBox" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 32px">
                    </td>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="lblTankSize" runat="server" Text="Tank Size:"> </asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtTankSize" runat="server" CssClass="EightCharTxtBox" TabIndex="3"></asp:TextBox>
                        <label style="font-style: normal; font-size: 7pt;">
                            Inches</label>
                        <asp:RequiredFieldValidator ID="reqTankSize" runat="server" ErrorMessage="Please enter tank size"
                            ControlToValidate="txtTankSize" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 32px">
                    </td>
                    <td class="StdLableTxtLeft" style="width: 32px">
                        <asp:Label ID="lblIncCtr" runat="server" Text="Fuel Increment:" Width="174px"></asp:Label></td>
                    <td style="text-align: left; width: 100px">
                        <asp:TextBox ID="txtIncCTR" runat="server" CssClass="EightCharTxtBox" TabIndex="4">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqIncCTR" runat="server" ErrorMessage="Please enter tank fuel increment counter"
                            ControlToValidate="txtIncCTR" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 32px">
                    </td>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="lblProduct" runat="server" Text="Product Type:"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlProdSelect" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Please Select Product" Value="0"></asp:ListItem>
                            <asp:ListItem Text="UNLEADED"></asp:ListItem>
                            <asp:ListItem Text="DIESEL"></asp:ListItem>
                            <asp:ListItem Text="OTHERS"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtProdVal" runat="server" Visible="false" CssClass="TenCharTxtBox"
                            MaxLength="10" TabIndex="5"></asp:TextBox>
                        <asp:CompareValidator ID="rfvProdVal" ControlToValidate="ddlProdSelect" Operator="NotEqual"
                            ValueToCompare="0" ErrorMessage="Please choose product" Display="Dynamic" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 32px">
                    </td>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="lblZeroOffset" runat="server" Text="Zero Offset:"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtZeroOffset" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20"
                            TabIndex="5"></asp:TextBox>
                        <label style="font-style: normal; font-size: 7pt">
                            Clicks</label>
                        <asp:RequiredFieldValidator ID="reqZeroOffset" runat="server" ErrorMessage="Please enter zero offset"
                            ControlToValidate="txtZeroOffset" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator></td>
                    <td rowspan="3" style="text-align: left">
            <table align="center" class="NineHundredPXTable" id="tblNewChartDetailSub" runat="server" border="1" cellpadding="3" style="width: 163px">
                <tr>
                    <td style="width: 47px">
                        <asp:Label ID="LabelUnl" runat="server" Text="Label">Unleaded</asp:Label></td>
                    <td style="width: 47px">
                        <asp:Label ID="LabelUnlQty" runat="server" Text="Label">0.0465</asp:Label></td>
               </tr>
                <tr>
                    <td style="width: 47px; height: 21px">
                        <asp:Label ID="LabelDie" runat="server" Text="Label">Diesel</asp:Label></td>
                    <td style="width: 47px; height: 21px">
                        <asp:Label ID="LabelDieQty" runat="server" Text="Label">0.0399</asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 21px; text-align: center;">
                        <asp:Label ID="LabelFuelNote" runat="server" Text="*Typical Standard Values"></asp:Label></td>
                </tr>
           </table>
                        </td>
                </tr>
                <tr align="center">
                    <td colspan="3" style="text-align: center; vertical-align: bottom">
                        <asp:RadioButtonList ID="rbtnList" runat="server" Width="100%" Height="32px" CssClass="TankChartInchesBox"
                            RepeatDirection="horizontal" TabIndex="5">
                            <asp:ListItem Text="Entry every 1 inch" Value="1" Selected="True" />
                            <asp:ListItem Text="Entry every 1/2 inch" Value="0.5" />
                            <asp:ListItem Text="Entry every 1/4 inch" Value="0.25" />
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 26px">
                        <asp:Button ID="btnContinue" runat="server" Text="Continue" TabIndex="4" />
                        <asp:Button ID="btnViewChart" runat="server" Text="View Chart" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
