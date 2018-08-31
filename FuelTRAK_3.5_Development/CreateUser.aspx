<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateUser.aspx.vb" Inherits="CreateUser" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CreateUser</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="javascript/CommonFunctions.js"></script>

    <script type="text/javascript" language="javascript" src="javascript/Validation.js"></script>

    <script language="javascript">
   
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
            <table align="center" class="FourHundredPXTable">
                <tr>
                    <td colspan="2" class="MainHeader" style="height: 50px">
                        <asp:Label ID="Label7" runat="server" Text="Add New User"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="width: 220px">
                        <asp:Label ID="Label1" runat="server" Text="User Name:"></asp:Label></td>
                    <td class="User_TextBox" style="width: 200px">
                        <asp:TextBox ID="txtUName" runat="server" EnableViewState="False" CssClass="TenCharTxtBox" MaxLength="20"
                            AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter user name"
                            ControlToValidate="txtUName" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator>
                       <%-- <asp:RegularExpressionValidator ID="usrRegExFieldVal" ControlToValidate="txtUName"
                            ValidationExpression="^.{1,10}$" ErrorMessage="Maximum of 10 characters in length"
                            runat="server" Width="245px"></asp:RegularExpressionValidator>--%>
                            </td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label></td>
                    <td class="User_TextBox">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" EnableViewState="False"
                            CssClass="TenCharTxtBox" AutoCompleteType="Disabled"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter password"
                            ControlToValidate="Password" Font-Names="Arial" Font-Size="Small" Font-Bold="True"
                            Display="Dynamic">*</asp:RequiredFieldValidator> 
                        <%--<asp:RegularExpressionValidator ID="pwdRegExFieldVal" ControlToValidate="Password"
                            ValidationExpression="^.{1,10}$" ErrorMessage="Maximum of 10 characters in length"
                            runat="server" Width="245px"></asp:RegularExpressionValidator>--%>
                            </td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label3" runat="server" Text="First Name:"></asp:Label></td>
                    <td class="User_TextBox">
                        <asp:TextBox ID="Fname" runat="server" EnableViewState="False" CssClass="FifteenCharTxtBox"
                            MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter first name"
                            ControlToValidate="Fname" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label4" runat="server" Text="Last Name:"></asp:Label></td>
                    <td class="User_TextBox">
                        <asp:TextBox ID="Lname" runat="server" EnableViewState="False" CssClass="TwentyCharTxtBox"
                            MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter last name"
                            ControlToValidate="Lname" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label5" runat="server" Text="Level:"></asp:Label></td>
                    <td class="User_TextBox">
                        <asp:DropDownList ID="DDLstLevel" runat="server">
                            <asp:ListItem Value="0">--Select User Level--</asp:ListItem>
                            <asp:ListItem Value="1">Admin</asp:ListItem>
                            <asp:ListItem Value="2">User</asp:ListItem>
                            <asp:ListItem Value="3">Reports &amp; Inventory</asp:ListItem>
                            <asp:ListItem Value="4">Reports Only</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DDLstLevel"
                            ErrorMessage="Please select User Level" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Operator="GreaterThan" SetFocusOnError="True" ValueToCompare="0" Display="Dynamic">*</asp:CompareValidator></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label6" runat="server" Text="COM Port #:" Visible="False"></asp:Label></td>
                    <td class="User_TextBox">
                        <asp:TextBox ID="txtComPort" runat="server" EnableViewState="False" CssClass="EightCharTxtBox"
                            MaxLength="8" Visible="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Com Port"
                            ControlToValidate="txtComPort" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            Display="Dynamic">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="vertical-align: middle; text-align: center">
                        <asp:Button ID="btnCreate" runat="server" Text="Add" Width="100px" />
                        <asp:Button ID="btnCancle" runat="server" Text="Cancel" CausesValidation="False"
                            Width="100px" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                        <asp:TextBox ID="dtime" runat="server" ReadOnly="True" Visible="False" Width="1px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
