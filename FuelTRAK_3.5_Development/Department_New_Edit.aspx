<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Department_New_Edit.aspx.vb"
    Inherits="Department_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Department Add and Edit</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="Javascript/CommonFunctions.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>

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
            <table align="center" class="SixHundredPXTable">
                <tr>
                    <td valign="top">
                        <table id="tblEditDept" runat="server" class="SixHundredPXTable">
                            <tr>
                                <td class="MainHeader" colspan="2" style="height: 50px">
                                    <asp:Label ID="Label1" runat="server" Text="Edit Department Information"></asp:Label></td>
                            </tr>
                            <tr id="trNewNo" runat="server">
                                <td class="StdLableTxtLeft" style="width: 150px; height: 32px">
                                    <asp:Label ID="Label2" runat="server" Text="Department Number:"></asp:Label></td>
                                <td class="NewDept_Lable_Right" style="width: 350px">
                                    <asp:TextBox ID="txtDeptNo" runat="server" CssClass="ThreeCharTxtBox" MaxLength="5"
                                        TabIndex="1" Width="50px"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDeptNo"
                                        ErrorMessage="Please Enter Department Number." Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtDeptNoHide" runat="server" Visible="False" CssClass="NewDept_Textbox"
                                        Width="1px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label3" runat="server" Text="Name:"></asp:Label></td>
                                <td class="NewDept_Lable_Right">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="TwentyCharTxtBox" MaxLength="25"
                                        TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVtxtDepartment" runat="server" Font-Size="Small"
                                        Font-Bold="False" Font-Names="arial" ErrorMessage="Please Enter Department Name."
                                        ControlToValidate="txtName" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label4" runat="server" Text="Address:"></asp:Label></td>
                                <td class="NewDept_Lable_Right">
                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="TwentyFiveCharTxtBox" MaxLength="25"
                                        TabIndex="3"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label8" runat="server" Text="Address 2:"></asp:Label>
                                </td>
                                <td class="NewDept_Lable_Right">
                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="TwentyFiveCharTxtBox" MaxLength="25"
                                        TabIndex="4"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label9" runat="server" Text="Surcharge Per Gallon:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSurchageGallon" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"
                                        TabIndex="5"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Surcharge per gallon is out of range"
                                        ControlToValidate="txtSurchageGallon" Font-Names="Arial" Font-Size="Small" MaximumValue="9.9999"
                                        MinimumValue="0.0" Type="Double" Display="Dynamic">*</asp:RangeValidator>
                                    <asp:Label ID="Label10" runat="server" Font-Italic="true" Font-Size="X-Small"
                                        Text="(0.0000 Format)"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label5" runat="server" Text="Surcharge Per Vehicle:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSurchage" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"
                                        TabIndex="5"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Surcharge per vehicle is out of range"
                                        ControlToValidate="txtSurchage" Font-Names="Arial" Font-Size="Small" MaximumValue="9.9999"
                                        MinimumValue="0.0" Type="Double" Display="Dynamic">*</asp:RangeValidator>
                                    <asp:Label ID="lblsurcharge" runat="server" Font-Italic="true" Font-Size="X-Small"
                                        Text="(0.0000 Format)"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label6" runat="server" Text="Account Number:"></asp:Label></td>
                                <td class="NewDept_Lable_Right">
                                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                        TabIndex="6"></asp:TextBox>&nbsp;
                                    <asp:Label ID="lblAccCodeMsg" runat="server" Font-Italic="true" Font-Size="X-Small"
                                        Text="(Maximum 10 Characters)"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label7" runat="server" Text="Upload Code:"></asp:Label></td>
                                <td class="NewDept_Lable_Right">
                                    <asp:TextBox ID="txtUploadcode" runat="server" CssClass="ThirtyCharTxtBox" MaxLength="30"
                                        TabIndex="7"></asp:TextBox>&nbsp;
                                    <asp:Label ID="lblUploadCodeMsg" runat="server" Font-Italic="true" Font-Size="X-Small"
                                        Text="(Maximum 30 Characters)"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="NewDept_ButtonTD" style="height: 50px">
                                    <asp:Button ID="btnOk" runat="server"  Text="Add Another" Width="100px"
                                        UseSubmitBehavior="False" TabIndex="8" />
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px"
                                        UseSubmitBehavior="False" TabIndex="9" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" CausesValidation="False"
                                        UseSubmitBehavior="False" TabIndex="10" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="NewDept_ButtonTD">
                                    &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="NewDept_ButtonTD">
                                    <asp:Button ID="btnFirst" runat="server" Text="|<" CssClass="NewDept_ButtonFooter"
                                        OnClick="First_Click" /><asp:Button ID="btnprevious" runat="server" Text="<" CssClass="NewDept_ButtonFooter" />
                                    <asp:Label ID="lblof" runat="server" Text="Label" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Bold="True" Font-Names="arial" Font-Size="Small" Width="115px"></asp:Label>
                                    <asp:Button ID="btnNext" runat="server" Text=">" CssClass="NewDept_ButtonFooter" /><asp:Button
                                        ID="btnLast" runat="server" Text=">|" CssClass="NewDept_ButtonFooter" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
