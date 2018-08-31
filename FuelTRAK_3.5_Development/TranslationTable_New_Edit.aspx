<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TranslationTable_New_Edit.aspx.vb"
    Inherits="TranslationTable_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TranslationTable_New_Edit</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Javascript/Validation.js"></script>
    
     <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

    </script>u

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" class="MaximumPXTable" >
                <tr>
                    <td>
                        <table class="EightHundredPXTable">
                            <tr>
                                <td class="MainHeader" colspan="2" style="height: 50px">
                                    <asp:Label ID="lblNew_Edit" runat="server" Text="Add Translation Information"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table align="center" class="TthreeHundredPXTable">
                                        <tr>
                                            <td class="StdLableTxtLeft" style="width: 200px; height: 32px">
                                                <asp:Label ID="Label10" runat="server" Text="Pick a Field: "></asp:Label></td>
                                            <td style="text-align:left; height: 24px; width: 100px">
                                                <asp:DropDownList ID="ddlFieldDesc" runat="server" CssClass="ThirtyCharTxtBox" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="height: 32px">
                                                <asp:Label ID="Label1" runat="server" Text="Field Name:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtFieldName" runat="server" CssClass="TenCharTxtBox" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="height: 32px">
                                                <asp:Label ID="Label5" runat="server" Text="Field Length:"></asp:Label>
                                            </td>
                                            <td class="StdLableTxtLeft" style="text-align: left">
                                                <asp:TextBox ID="txtFieldLen" runat="server" CssClass="ThreeCharTxtBox" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="height: 32px">
                                                <asp:Label ID="Label4" runat="server" Text="Fill Character:"></asp:Label>
                                            </td>
                                            <td class="StdLableTxtLeft" style="height: 32px; text-align: left">
                                                <asp:TextBox ID="txtFillChar" runat="server" CssClass="OneCharTxtBox" MaxLength="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="height: 32px">
                                                <asp:Label ID="Label2" runat="server" Text="Field position in Export file: "></asp:Label></td>
                                            <td class="StdLableTxtLeft" style="text-align: left">
                                                <asp:TextBox ID="txtFieldPos" runat="server" CssClass="TwoCharTxtBox" MaxLength="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="height: 32px">
                                                <asp:Label ID="Label6" runat="server" Text="Field length in Export file: "></asp:Label></td>
                                            <td class="StdLableTxtLeft" style="text-align: left">
                                                <asp:TextBox ID="txtFieldExpLen" runat="server" CssClass="TwoCharTxtBox" MaxLength="2"></asp:TextBox></td>
                                        </tr>
                                        <tr></tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="vertical-align:top">
                                                <asp:Label ID="Label7" runat="server" Text="Starting Position for Length: "></asp:Label></td>
                                            <td class="StdLableTxtLeft" style="text-align: left">
                                                <asp:RadioButtonList ID="rdoSelect" runat="server" CssClass="TenCharTxtBox" AutoPostBack="false" Font-Names="Arial"
                                                    Font-Size="Small" ForeColor="Black" Style="cursor: hand; vertical-align: middle;
                                                    text-align: left;" Font-Bold="True">
                                                    <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                                    <asp:ListItem Value="Left">Left</asp:ListItem>
                                                    <asp:ListItem Value="Right">Right</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="height: 50px">
                                    <asp:Button ID="btnOk" runat="server" Text="OK" Width="100px" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" Height="24px" />
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" id="txtOldOrder" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
