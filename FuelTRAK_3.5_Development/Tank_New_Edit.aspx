<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tank_New_Edit.aspx.vb" Inherits="Tank_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tank_New_Edit Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>

    <script language="javascript">
   
        function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
     function KeyUpEvent_txtSurchage(e)
    {
    
        var str = document.getElementById('txtprice').value;
        if(str.length == 2)
        {
             document.getElementById('txtprice').value = str + "."
        }
        else if(str.length == 6)
        {
            document.getElementById('txtprice').focus();
        }
    }
    function KeyPressNumber(e)
    {
      if (window.event.keyCode < 48 || window.event.keyCode > 57)
        { window.event.keyCode = 0;}
    }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" id="tblEditDept" runat="server" class="FiveHundredPXTable">
                <tr>
                    <td class="MainHeader" colspan="2" style="height: 50px">
                        <asp:Label ID="Label1" runat="server" Text=" Edit Tank Screen"></asp:Label></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="width: 150px; height: 32px">
                        <asp:Label ID="Label2" runat="server" Text="Tank:"></asp:Label></td>
                    <td style="text-align: left; width: 250px; height: 32px">
                        <asp:TextBox ID="txtTank" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                            ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label3" runat="server" Text="Tank Name:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox ID="txtName" runat="server" CssClass="TwentyFiveTxtBox" MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtDepartment" runat="server" Font-Size="Small"
                            Font-Bold="False" Font-Names="Verdana" ErrorMessage="Please Enter Tank Name."
                            ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label4" runat="server" Text="Tank Address:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="TwentyFiveCharTxtBox" MaxLength="25"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label5" runat="server" Text="Product:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:DropDownList ID="DropProduct" runat="server" CssClass="TwentyCharTxtBox">
                        </asp:DropDownList>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DropProduct"
                            Display="Dynamic" ErrorMessage="Please Select Product." MaximumValue="5000" MinimumValue="1"
                            Type="Integer" SetFocusOnError="True">*</asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RFV_Product" runat="server" ControlToValidate="DropProduct"
                            Display="None" ErrorMessage="Please select product" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="Small">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label6" runat="server" Text="Preset Price:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox ID="txtprice" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtprice"
                            Display="Dynamic" ErrorMessage="Preset Price is out of range" Font-Names="Verdana"
                            Font-Size="Small" MaximumValue="99.9999" MinimumValue="0.0" Type="Double">*</asp:RangeValidator>
                        <label style="font-style: italic; font-size: 7pt">
                            (00.0000 Format)</label>
                        <asp:Label ID="lblNote" Style="font-style: italic; font-size: 9pt" Text="FIFO Costing"
                            Visible="False" runat="server" ForeColor="Maroon" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label7" runat="server" Text="Tank Size:"></asp:Label></td>
                    <td style="height: 32px; text-align: left">
                        <asp:TextBox ID="txttsize" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                            Style="direction: rtl"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label8" runat="server" Text="Refill Notice:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox ID="txtRnotice" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                            Style="direction: rtl"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtRnotice"
                            ControlToValidate="txttsize" ErrorMessage="Refill can't be greater than Tank Size."
                            Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1">*</asp:CompareValidator></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label9" runat="server" Text="Export Code:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox ID="txtExCode" runat="server" CssClass="ThirtyCharTxtBox" MaxLength="30"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label10" runat="server" Text="Tank Monitor:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:DropDownList ID="DropTM" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label11" runat="server" Text="Probe Number:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox ID="txtPNO" runat="server" CssClass="OneCharTxtBox" MaxLength="1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="lblTankCharts" runat="server" Text="Assign Tank Charts:"></asp:Label></td>
                    <td style="text-align: left; height: 32px">
                        <asp:DropDownList ID="ddlTankCharts" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2" class="NewDept_ButtonTD">
                        <asp:Button ID="btnOK" runat="server" Text="Save" Width="100px" AccessKey="s" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" CausesValidation="False"
                            AccessKey="c" /></td>
                </tr>
                <tr>
                    <td colspan="2" class="NewDept_ButtonTD">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
