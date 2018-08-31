<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Product.aspx.vb" Inherits="Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product Page</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
    <div>
        <table align="center" class="FiveHundredPXTable">
            <tr>
                <td valign="top" align="center">
                    <table align="center" id="tblImportPers" runat="server" style="width: 100%">
                        <tr>
                            <td style="float: left; width: 430px; margin-right: 10px">
                                <br />
                                <br />
                                <br />
                                <table align="center" id="Table2" runat="server" class="FiveHundredPXTable" cellspacing ="1">
                                    <tr>
                                        <td align="center">
                                            <table style="width: 430px" align="center">
                                                <tr>
                                                    <td colspan="4" class="MainHeader" style="height: 50px">
                                                        <asp:Label runat="server" ID="lblHeader" Text="Product List"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" Text="#" ID="lblHeader1" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="X-Small" ForeColor="Black"></asp:Label>
                                                    </td>
                                                    <td style="width: 100px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" Text="Name" ID="lblHeader2" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="X-Small" ForeColor="Black" CssClass="Product_Center"></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" Text="Primary" ID="lblHeader3" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="X-Small" ForeColor="Black"></asp:Label><br />
                                                        <font size="1" face="arial">(Used as Fuel)</font>
                                                    </td>
                                                    <td style="width: 100px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" Text="Export Code" ID="lblHeader4" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="X-Small" ForeColor="Black"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L1" Text="01" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="txtN1" Style="position: relative" MaxLength="10"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk1" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="txtE1" Style="position: relative" MaxLength="10"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L2" Text="02" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="txtN2" Style="position: relative" MaxLength="10"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk2" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE2" Style="position: relative" MaxLength="10"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L3" Text="03" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN3" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk3" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE3" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L4" Text="04" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN4" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk4" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE4" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L5" Text="05" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN5" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk5" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE5" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L6" Text="06" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN6" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk6" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE6" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L7" Text="07" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN7" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk7" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE7" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L8" Text="08" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN8" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk8" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE8" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L9" Text="09" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN9" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk9" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE9" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L10" Text="10" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN10" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk10" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE10" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L11" Text="11" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN11" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk11" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE11" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L12" Text="12" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN12" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk12" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE12" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L13" Text="13" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN13" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk13" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE13" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L14" Text="14" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN14" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk14" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE14" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:Label runat="server" ID="L15" Text="15" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtN15" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:CheckBox runat="server" ID="Chk15" />
                                                    </td>
                                                    <td style="width: 50px; vertical-align: top" align="center">
                                                        <asp:TextBox runat="server" ID="TxtE15" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="center" style="height: 50px">
                                                        <asp:Button runat="server" ID="btnOk" Text="OK" Width="66px" Style="cursor: pointer" />
                                                        <asp:Button runat="server" ID="btncancel" Text="Cancel" Width="66px" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 60%;">
                                <table align="center" id="Tt1" runat="server" class="FiveHundredPXTable">
                                    <tr>
                                        <td>
                                            <table style="width: 550px; background-color: #FFD966" cellpadding="0" border="0" align="center" cellspacing="4">
                                                <tr>
                                                    <td colspan="6" class="MainHeader" style="height: 50px" align="center">
                                                        <asp:Label runat="server" ID="Label1" Text="Update Transaction Cost"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:Label runat="server" Text="Preset Price for Product" ID="Label6" Font-Bold="True"
                                                            Font-Names="Arial" Font-Size="X-Small" ForeColor="Black"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;" align="center">
                                                        <asp:Label runat="server" Text="Starting Date" ID="Label2" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="X-Small" ForeColor="Black"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;" align="center">
                                                        <asp:Label runat="server" Text="Ending Date" ID="Label3" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="X-Small" ForeColor="Black" CssClass="Product_Center"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;" align="center">
                                                        <asp:Label runat="server" Text="Click to Post new price" ID="Label4" Font-Bold="True"
                                                            Font-Names="Arial" Font-Size="X-Small" ForeColor="Black"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;" align="center">
                                                        <asp:Label runat="server" Text="Show History" ID="Label5" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="X-Small" ForeColor="Black"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr1" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate1" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage1" alt="Start Date" runat="server" onclick="Javascript:cal1.popup()" 
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate1" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage1" alt="End Date" runat="server" onclick="Javascript:cal2.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice1" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20 px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory1" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20 px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr2" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate2" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage2" alt="Start Date" runat="server" onclick="Javascript:cal3.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate2" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage2" alt="End Date" runat="server" onclick="Javascript:cal4.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice2" runat="server" Text="POST" Width="65px" Height="20 px"
                                                            Font-Names="Verdana" Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory2" runat="server" Text="HISTORY" Width="100px" Height="20 px" 
                                                            Font-Names="Verdana" Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr3" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate3" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage3" alt="Start Date" runat="server" onclick="Javascript:cal5.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate3" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage3" alt="End Date" runat="server" onclick="Javascript:cal6.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice3" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20 px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory3" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20 px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr4" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate4" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage4" alt="Start Date" runat="server" onclick="Javascript:cal7.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate4" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage4" alt="End Date" runat="server" onclick="Javascript:cal8.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice4" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory4" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr5" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate5" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage5" alt="Start Date" runat="server" onclick="Javascript:cal9.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate5" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage5" alt="End Date" runat="server" onclick="Javascript:cal10.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice5" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory5" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr6" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate6" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="Img1" alt="Start Date" runat="server" onclick="Javascript:cal11.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate6" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage6" alt="End Date" runat="server" onclick="Javascript:cal12.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice6" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory6" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr7" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate7" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage7" alt="Start Date" runat="server" onclick="Javascript:cal13.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate7" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage7" alt="End Date" runat="server" onclick="Javascript:cal14.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice7" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory7" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr8" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate8" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage8" alt="Start Date" runat="server" onclick="Javascript:cal15.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate8" runat="server" Style="position: relative" MaxLength="10"  Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage8" alt="End Date" runat="server" onclick="Javascript:cal16.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice8" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory8" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr9" Style="position: relative; text-align: right"  Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate9" runat="server" CssClass="TenCharTxtBox" MaxLength="10"  Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage9" alt="Start Date" runat="server" onclick="Javascript:cal17.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate9" runat="server" Style="position: relative" MaxLength="10"  Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage9" alt="End Date" runat="server" onclick="Javascript:cal18.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice9" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory9" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr10" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate10" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage10" alt="Start Date" runat="server" onclick="Javascript:cal19.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate10" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage10" alt="End Date" runat="server" onclick="Javascript:cal20.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice10" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory10" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr11" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate11" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage11" alt="Start Date" runat="server" onclick="Javascript:cal21.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate11" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage11" alt="End Date" runat="server" onclick="Javascript:cal22.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice11" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory11" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr12" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate12" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage12" alt="Start Date" runat="server" onclick="Javascript:cal23.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate12" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage12" alt="End Date" runat="server" onclick="Javascript:cal24.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice12" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory12" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr13" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate13" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage13" alt="Start Date" runat="server" onclick="Javascript:cal25.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate13" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage13" alt="End Date" runat="server" onclick="Javascript:cal26.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice13" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory13" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr14" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate14" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage14" alt="Start Date" runat="server" onclick="Javascript:cal27.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate14" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage14" alt="End Date" runat="server" onclick="Javascript:cal28.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice14" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory14" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top">
                                                        <asp:TextBox runat="server" ID="TxtPr15" Style="position: relative; text-align: right" Height = "12px"
                                                            MaxLength="10" CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtStartDate15" runat="server" CssClass="TenCharTxtBox" MaxLength="10" Height = "12px"
                                                            EnableViewState="False"></asp:TextBox>
                                                        <img id="StartDateImage15" alt="Start Date" runat="server" onclick="Javascript:cal29.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:TextBox ID="txtEndDate15" runat="server" Style="position: relative" MaxLength="10" Height = "12px"
                                                            CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                                                        <img id="EndDateImage15" alt="End Date" runat="server" onclick="Javascript:cal30.popup()"
                                                            src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnPrice15" runat="server" Text="POST" Width="65px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                    <td style="width: 20%; vertical-align: top" align="center">
                                                        <asp:Button ID="btnHistory15" runat="server" Text="HISTORY" Width="100px" Font-Names="Verdana" Height = "20px"
                                                            Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="PopUpHistory" runat="server" style="position: absolute; border: solid 3px #00004C;
        background-color: #A5BBC5; top: 0px; left: 40px; width: 970px; height: 750px">
        <table class="VehicleEditTypePopUp" align="center" style="width: 970px">
            <tr>
                <td class="VehicleEditPMPopUpHeader" colspan="4" style="text-align: center">
                    <h1>
                        Product Pricing History</h1>
                </td>
            </tr>
            <tr>
                <td class="VehicleGrid" align="center" colspan="4">
                    <asp:GridView Width="800px" ID="grdHistory" runat="server" BackColor="White" BorderColor="black"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Font-Size="Medium"
                        PageSize="10" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true"
                        EmptyDataText="0 history records found for selected product">
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <Columns>
                            <asp:TemplateField SortExpression="ProductNumber" ItemStyle-HorizontalAlign="Center"
                                HeaderText="Product Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ProductNumber")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ProductName" ItemStyle-HorizontalAlign="Center"
                                HeaderText="Product Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductName" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="FromDate" ItemStyle-HorizontalAlign="Center" HeaderText="From Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" Text='<%# DataBinder.Eval(Container.DataItem, "FromDate")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ToDate" ItemStyle-HorizontalAlign="Center" HeaderText="To Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblToDate" Text='<%# DataBinder.Eval(Container.DataItem, "ToDate")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ResetPrice" ItemStyle-HorizontalAlign="Center"
                                HeaderText="Reset Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblResetPrice" Text='<%# DataBinder.Eval(Container.DataItem, "ResetPrice")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="UserName" ItemStyle-HorizontalAlign="Center" HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="DateAdded" ItemStyle-HorizontalAlign="Center"
                                HeaderText="Date of Re-post">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateAdded" Text='<%# DataBinder.Eval(Container.DataItem, "DateAdded")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="white" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                        <PagerStyle BackColor="#A5BBC5" ForeColor="Black" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="black" />
                        <AlternatingRowStyle BackColor="#fdfeb6" />
                        <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BackColor="white" BorderColor="red"
                            BorderStyle="Solid" BorderWidth="1px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="padding: 0px">
                    <asp:Button ID="btnCancelHistory" runat="server" Text="Exit" Height="24px" Font-Names="Verdana"
                        Font-Size="Small" CausesValidation="False" Style="cursor: pointer" />
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script language="javascript" type="text/javascript">
    
      if (document.getElementById('txtStartDate1'))
        {
	        var cal1 = new calendar2(document.getElementById('txtStartDate1'));
	        cal1.year_scroll = true;
	        cal1.time_comp = false;
	    
	        var cal2 = new calendar2(document.getElementById('txtEndDate1'));
	        cal2.year_scroll = true;
	        cal2.time_comp = false;
	        
	        
	         var cal3 = new calendar2(document.getElementById('txtStartDate2'));
	        cal3.year_scroll = true;
	        cal3.time_comp = false;
	    
	        var cal4 = new calendar2(document.getElementById('txtEndDate2'));
	        cal4.year_scroll = true;
	        cal4.time_comp = false;
	        
	         var cal5 = new calendar2(document.getElementById('txtStartDate3'));
	        cal5.year_scroll = true;
	        cal5.time_comp = false;
	    
	        var cal6 = new calendar2(document.getElementById('txtEndDate3'));
	        cal6.year_scroll = true;
	        cal6.time_comp = false;
	        
	         var cal7 = new calendar2(document.getElementById('txtStartDate4'));
	        cal7.year_scroll = true;
	        cal7.time_comp = false;
	    
	        var cal8 = new calendar2(document.getElementById('txtEndDate4'));
	        cal8.year_scroll = true;
	        cal8.time_comp = false;
	        
	         var cal9 = new calendar2(document.getElementById('txtStartDate5'));
	        cal9.year_scroll = true;
	        cal9.time_comp = false;
	    
	        var cal10 = new calendar2(document.getElementById('txtEndDate5'));
	        cal10.year_scroll = true;
	        cal10.time_comp = false;
	        
	         var cal11 = new calendar2(document.getElementById('txtStartDate6'));
	        cal11.year_scroll = true;
	        cal11.time_comp = false;
	    
	        var cal12 = new calendar2(document.getElementById('txtEndDate6'));
	        cal12.year_scroll = true;
	        cal12.time_comp = false;
	        
	         var cal13 = new calendar2(document.getElementById('txtStartDate7'));
	        cal13.year_scroll = true;
	        cal13.time_comp = false;
	    
	        var cal14 = new calendar2(document.getElementById('txtEndDate7'));
	        cal14.year_scroll = true;
	        cal14.time_comp = false;
	        
	         var cal15 = new calendar2(document.getElementById('txtStartDate8'));
	        cal15.year_scroll = true;
	        cal15.time_comp = false;
	    
	        var cal16 = new calendar2(document.getElementById('txtEndDate8'));
	        cal16.year_scroll = true;
	        cal16.time_comp = false;
	        
	         var cal17 = new calendar2(document.getElementById('txtStartDate9'));
	        cal17.year_scroll = true;
	        cal17.time_comp = false;
	    
	        var cal18 = new calendar2(document.getElementById('txtEndDate9'));
	        cal18.year_scroll = true;
	        cal18.time_comp = false;
	        
	         var cal19 = new calendar2(document.getElementById('txtStartDate10'));
	        cal19.year_scroll = true;
	        cal19.time_comp = false;
	    
	        var cal20 = new calendar2(document.getElementById('txtEndDate10'));
	        cal20.year_scroll = true;
	        cal20.time_comp = false;
	        
	         var cal21 = new calendar2(document.getElementById('txtStartDate11'));
	        cal21.year_scroll = true;
	        cal21.time_comp = false;
	    
	        var cal22 = new calendar2(document.getElementById('txtEndDate11'));
	        cal22.year_scroll = true;
	        cal22.time_comp = false;
	        
	         var cal23 = new calendar2(document.getElementById('txtStartDate12'));
	        cal23.year_scroll = true;
	        cal23.time_comp = false;
	    
	        var cal24 = new calendar2(document.getElementById('txtEndDate12'));
	        cal24.year_scroll = true;
	        cal24.time_comp = false;
	        
	         var cal25 = new calendar2(document.getElementById('txtStartDate13'));
	        cal25.year_scroll = true;
	        cal25.time_comp = false;
	    
	        var cal26 = new calendar2(document.getElementById('txtEndDate13'));
	        cal26.year_scroll = true;
	        cal26.time_comp = false;
	        
	         var cal27 = new calendar2(document.getElementById('txtStartDate14'));
	        cal27.year_scroll = true;
	        cal27.time_comp = false;
	    
	        var cal28 = new calendar2(document.getElementById('txtEndDate14'));
	        cal28.year_scroll = true;
	        cal28.time_comp = false;
	        
	         var cal29 = new calendar2(document.getElementById('txtStartDate15'));
	        cal29.year_scroll = true;
	        cal29.time_comp = false;
	    
	        var cal30 = new calendar2(document.getElementById('txtEndDate15'));
	        cal30.year_scroll = true;
	        cal30.time_comp = false;
	        
	      
	    }
    
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	 function KeyPress(e)
     {
      if (window.event.keyCode < 48 || window.event.keyCode > 122)  
        { window.event.keyCode = 0;}
        if (window.event.keyCode == 92 || window.event.keyCode == 94 || window.event.keyCode == 95 || window.event.keyCode == 96 ||window.event.keyCode == 60 ||window.event.keyCode == 62 || window.event.keyCode == 63 || window.event.keyCode == 64)
        { window.event.keyCode = 0;}
     }
     
     function KeyPressProduct(e)
     {
     
     var keyCode = e.which ? e.which : e.keyCode;
     var ret = ((keyCode < 48 || keyCode > 57));
       
     if(ret){
        ret = (keyCode == 46);
        if(ret)
            return true
        else
            return false
      }
      else
        return true;

        
     }
      //By Soham Gangavane Sep 15, 2017
      function KeyUpEvent_DateTextBox(e,txtDate)
        {   var str = document.getElementById(txtDate).value;
            if(str.length == 2)
            { document.getElementById(txtDate).value = str + "/";            }
            else if(str.length == 5)
            { document.getElementById(txtDate).value = str + "/";            }   
            
               
        }
      
    </script>

</body>
</html>
