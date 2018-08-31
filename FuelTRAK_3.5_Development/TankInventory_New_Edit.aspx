<%@ Page Language="VB" AutoEventWireup="true" CodeFile="TankInventory_New_Edit.aspx.vb"
    Inherits="TankInventory_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tank Inventory Add Update</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/DateTime.js"></script>

    <script type="text/javascript" src="Javascript/Validation.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/tankInventory.js"></script>

    <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
    
        function DeleteMsg()
        {   
        alert("Record deleted Successfully");
        location.href="TankInventory.aspx"
        }

        function CountDecimals()
        {
            var count = 0;
            var valtext;
            valtext=document.getElementById('txt3').value;
            
            for(var i=0;i < valtext.length;i++)
            {
                if(valtext.substring(i,i+1) == '.')
                count++;
            }
            if (count > 1)
            {    alert('Price cannot contain more than one decimal.');    }
            
            if (count == 0)
            {
                alert('Please insert one decimal into price.');
                document.getElementById('txt3').focus();
            }
        }
        
        function validate()
        {
            
            if(document.getElementById('lbl1').innerText == 'Level Prior to Delivery :')
            {
            
                if(parseInt(document.getElementById('txt1').value) >= parseInt(document.getElementById('ddlTankSize').value))
                {
                    
                    alert('Tank holds a maximum of ' + document.getElementById('ddlTankSize').value + ' gallons!');
                     return false;
                }
                else
                { 
                    return true;
                }
            }
            else
            {
                return true;
            }
            
         
        }
      
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hdfRecord" Value ="" />
        <asp:HiddenField runat="server" ID="hdfEntryType" Value ="" />
        <div>
            <table align="center" class="EightHundredPXTable">
                <tr>
                    <td colspan="3" valign="top">
                        <table align="center" id="tblEditDept" class="SixHundredPXTable" runat="server">
                            <tr>
                                <td colspan="3" class="MainHeader" style="height: 50px">
                                    <asp:Label ID="lblNew_Edit" runat="server" Text="Tank Inventory Edit Screen"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:RegularExpressionValidator ID="RFV_txt7" runat="server" ControlToValidate="txt4"
                                        Display="Dynamic" ErrorMessage="Invalid Time" SetFocusOnError="True" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="REV_6" runat="server" ErrorMessage="Invalid Date "
                                        ControlToValidate="txt5" Display="Dynamic" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                                        SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lblTank" runat="server" Text="Tank:"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:DropDownList ID="DDLstTank" runat="server" CssClass="ThirtyCharTxtBox" AutoPostBack="True">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" valign="middle" style="width: 140px">
                                    <asp:Label ID="lbl1" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt1" runat="server" CssClass="TenCharTxtBox" MaxLength="5" CausesValidation="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txt1" runat="server" ControlToValidate="txt1"
                                        ErrorMessage="RFV_txt1" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl2" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt2" runat="server" CssClass="TenCharTxtBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txt2" runat="server" ControlToValidate="txt2"
                                        ErrorMessage="RFV_txt2">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr runat="server" id="trHide">
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl3" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt3" runat="server" CssClass="TenCharTxtBox" Font-Bold="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txt3" runat="server" ControlToValidate="txt3"
                                        ErrorMessage="RFV_txt3" Font-Names="Verdana" Font-Size="Small" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Price is out of range"
                                        Font-Names="Verdana" Font-Size="Small" MaximumValue="9.9999" MinimumValue="0.0"
                                        Type="Double" Display="Dynamic" ControlToValidate="txt3">*</asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl4" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt4" runat="server" CssClass="TenCharTxtBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txt4" runat="server" ControlToValidate="txt4"
                                        ErrorMessage="RFV_txt4" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl5" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt5" runat="server" CssClass="TenCharTxtBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txt5" runat="server" ControlToValidate="txt5"
                                        ErrorMessage="RFV_txt5" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl6" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt6" runat="server" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl7" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt7" runat="server" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl8" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt8" runat="server" CssClass="TenCharTxtBox" MaxLength="10"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl10" runat="server"></asp:Label></td>
                                <td class="TankInv_Add_Left" style="width: 248px">
                                    <asp:TextBox ID="txt10" runat="server" CssClass="TenCharTxtBox" MaxLength="5"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 92px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 140px">
                                    <asp:Label ID="lbl9" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="height: 26px">
                                    <asp:Button ID="btnOk" runat="server" Text="Add Another" Width="100px"
                                        Font-Names="Verdana" Font-Size="Small" />
                                    <asp:Button ID="btnSave" runat="server" Height="24px" Text="Save" Width="100px" Font-Names="Verdana"
                                        Font-Size="Small" />
                                    <asp:Button ID="btnCancel" runat="server" Height="24px" Text="Cancel" Width="100px"
                                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" Visible="false" />&nbsp;
                                    <asp:Button ID="btnEditCancel" runat="server" Height="24px" Text=" Edit Cancel" Width="100px"
                                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="100px" Height="24px"
                                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" Visible="False" /></td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" colspan="3">
                                    <table id="tblFindRec" align="center">
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnFirst" runat="server" Text="|<" Height="24px" Width="50px" CausesValidation="False" />
                                                <asp:Button ID="btnprevious" runat="server" Text="<" Height="24px" Width="50px" CausesValidation="False" />
                                                <asp:Label ID="lblof" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Names="Verdana" Font-Size="Small" Text="Label" Width="115px"
                                                    Visible="False"></asp:Label>
                                                <asp:Button ID="btnNext" runat="server" Text=">" Height="24px" Width="50px" CausesValidation="False" /><asp:Button
                                                    ID="btnLast" runat="server" Text=">|" Width="50px" CausesValidation="False" /></td>
                                        </tr>
                                    </table>
                                    <asp:DropDownList ID="ddlTankSize" runat="server" Width="0px" AutoPostBack="True">
                                    </asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>
</body>
</html>
