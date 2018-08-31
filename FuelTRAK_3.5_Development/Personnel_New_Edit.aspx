<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Personnel_New_Edit.aspx.vb"
    Inherits="Personnel_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Personnel New Edit</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/personnel.js"></script>

    <script type="text/javascript">

        function chksesion()
	    {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	    }

        function DeleteMsg()
        {   
            alert("Record deleted Successfully");
            location.href="Personnel.aspx";
        }
        
        function saveCall()
        {
            alert("Record Saved Successfully");
            location.href="Personnel.aspx";
        }
        
        function check()
        {
            var ac=confirm('This key will be PERMANANTLY locked out !\n Is this what you want to do ?');
            document.form1.txtLost.value=ac;//
            form1.submit(); 
        }
    </script>
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" width="EightHundredPXTable">
                <tr>
                    <td class="MainHeader" colspan="4" style="height: 50px">
                        <asp:Label ID="lblNew_Edit" runat="server" CssClass="MainHeader" Text="Add New Personnel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; height: 24px">
                    </td>
                    <td style="width: 325px; height: 24px">
                    </td>
                    <td style="width: 175px; height: 24px">
                    </td>
                    <td style="width: 150px; height: 24px">
                    </td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 28px">
                        <asp:Label ID="Label1" runat="server" Text="Personnel ID:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtPersonnelId" runat="server" CssClass="TenCharTxtBox" Font-Names="arial"
                            Font-Size="Small" MaxLength="10" TabIndex="1" Enabled="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPersonnelId"
                            ErrorMessage="Please Enter Personnel ID." Font-Names="arial" Font-Size="Small">*</asp:RequiredFieldValidator></td>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label5" runat="server" Text="Key #:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtKey" runat="server" CssClass="FiveCharTxtBox" MaxLength="5" Font-Names="arial"
                            Font-Size="Small" Enabled="False" ReadOnly="True" TabIndex="8"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblLoskKey" runat="server" Visible="false" TabIndex="15" BackColor="darkRed"
                            ForeColor="white" Text="Key Has Been Locked."></asp:Label></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 28px">
                        <asp:Label ID="lblPersonnelID2" runat="server" Text="Personnel ID 2:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtPersonnelID2" runat="server" CssClass="TenCharTxtBox" Font-Names="arial"
                            Font-Size="Small" MaxLength="10" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 28px">
                        <asp:Label ID="Label2" runat="server" Text="First Name:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFirstname" runat="server" CssClass="FifteenCharTxtBox" Font-Names="arial"
                            Font-Size="Small" MaxLength="15" TabIndex="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVFirstname" runat="server" ControlToValidate="txtFirstname"
                            ErrorMessage="Please enter First Name" Font-Names="arial" Font-Size="Small">*</asp:RequiredFieldValidator></td>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label6" runat="server" Text="Key Exp Date:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtKetexp" runat="server" CssClass="EightCharTxtBox" MaxLength="7"
                            Font-Names="arial" Font-Size="Small" TabIndex="9"></asp:TextBox><label style="font-family: Arial;
                                font-style: italic; font-size: 7pt">MM/YYYY</label></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 28px">
                        <asp:Label ID="Label3" runat="server" Text="Middle Initial:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtMI" runat="server" CssClass="TwoCharTxtBox" MaxLength="1" Font-Names="arial"
                            Font-Size="Small" TabIndex="4"></asp:TextBox></td>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label7" runat="server" Text="Card #:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCard" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"
                            Font-Names="arial" Font-Size="Small" TabIndex="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 28px">
                        <asp:Label ID="Label4" runat="server" Text="Last Name:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="TwentyCharTxtBox" Font-Names="arial"
                            Font-Size="Small" MaxLength="20" TabIndex="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                            ErrorMessage="Please enter Last Name" Font-Bold="False" Font-Names="arial" Font-Size="Small">*</asp:RequiredFieldValidator></td>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label8" runat="server" Text="Card Exp Date:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCardexp" runat="server" CssClass="EightCharTxtBox" MaxLength="7"
                            Font-Names="arial" Font-Size="Small" TabIndex="11"></asp:TextBox><label style="font-style: italic;
                                font-family: arial; font-size: 7pt">MM/YYYY</label></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 28px">
                        <asp:Label ID="Label10" runat="server" Text="Department #:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="DDLstDepartment" runat="server" CssClass="ThirtyCharTxtBox"
                            Font-Names="arial" Font-Size="Small" TabIndex="6">
                        </asp:DropDownList><asp:TextBox ID="txtPersonalIdHide" runat="server" Visible="False"
                            Width="8px"></asp:TextBox></td>
                    <td class="StdLableTxtLeft">
                        <asp:Label ID="Label9" runat="server" Text="Account ID #:"></asp:Label></td>
                    <td style="vertical-align: top; text-align: left">
                        <asp:TextBox ID="txtAccId" runat="server" CssClass="TenCharTxtBox" MaxLength="11"
                            Font-Names="arial" Font-Size="Small" TabIndex="12"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 28px">
                        <asp:Label ID="Label11" runat="server" Text="Comments:"></asp:Label></td>
                    <td style="text-align: left" rowspan="2">
                        <asp:TextBox ID="txtText" runat="server" Style="height: 50px" Width="210px" MaxLength="3"
                            TextMode="MultiLine" Font-Names="arial" Font-Size="Small" TabIndex="7"></asp:TextBox></td>
                    <td colspan="2" class="Per_New_lblNew_CheckBox">
                        <asp:CheckBox ID="CBoxTemdisabled" runat="server" Font-Names="arial" Font-Size="10pt"
                            Text="Temporarily Disable " Font-Bold="true" TextAlign="left" TabIndex="13" /></td>
                </tr>
                <tr>
                    <td style="height: 28px">
                    </td>
                    <td colspan="2" class="Per_New_lblNew_CheckBox">
                        <asp:CheckBox ID="CBoxRequiredIdentity" runat="server" Font-Names="arial" Font-Size="10pt"
                            Text="Require ID Entry " Font-Bold="true" TextAlign="left" TabIndex="14" /></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="DeletedLabel">
                        <asp:Label ID="lblDelPers" runat="server" Text="Deleted Personnel" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center" style="border-top: solid 1px black; border-bottom: solid 1px black;
                border-left: solid 1px black; border-right: solid 1px black; background-color: #a5bbc5;
                width: 784px;">
                <tr>
                    <td colspan="4" align="center" valign="baseline">
                        <asp:Button ID="btnEncodeKey" runat="server" Text="Encode Key" Visible="false" />
                      <asp:HyperLink ID="encodeKeyLink" style="text-decoration:none" runat="server" Text="Encode Key" Width="90px" BorderWidth="1px" Height="18px" BackColor="#DDDDDD" ForeColor="Black"  />
                        <%--<asp:HyperLink ID="encodeKeyLink" runat="server" Text="Encode Key" CssClass="VehicleEditBtn2"
                            ImageUrl="~/images/ButtonEncodeKey3.gif" />--%>
                        <input id="btnLostKey" disabled="disabled" runat="server" onclick="check();" type="button"
                            value="Lost Key" />
                        <input id="btnKeyHistory" disabled="disabled" runat="server" type="button" value="Key History" />
                        <input id="btnLostCard" disabled="disabled" runat="server" onclick="check();" type="button"
                            value="Lost Card" />
                        <asp:Button ID="btnOk" runat="server" Text="Add Another" Width="100px" OnClick="btnOk_Click"
                            Font-Names="arial" Font-Size="Small" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            Font-Names="arial" Font-Size="Small" CausesValidation="False" Visible="False"
                            Width="100px" /></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <input id="txtLost" runat="server" style="width: 32px" type="hidden" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" style="height: 30px; vertical-align: top">
                        <asp:Button ID="btnFirst" runat="server" Height="24px" Text="|<" Width="50px" CausesValidation="False" /><asp:Button
                            ID="btnprevious" runat="server" Height="24px" Text="<" Width="50px" CausesValidation="False" />
                        <asp:Label ID="lblof" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                            Font-Bold="false" Font-Names="arial" Font-Size="12pt" Text="Label" Width="115px"></asp:Label>
                        <asp:Button ID="btnNext" runat="server" Height="24px" Text=">" Width="50px" CausesValidation="False" /><asp:Button
                            ID="btnLast" runat="server" Text=">|" Width="50px" CausesValidation="False" />
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td class="VehicleEditTDRight" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" TabIndex="15" /></td>
                    <td class="VehicleEditTDLeft" colspan="2">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" OnClick="btnCancel_Click"
                            Font-Names="arial" Font-Size="Small" CausesValidation="False" TabIndex="16" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
