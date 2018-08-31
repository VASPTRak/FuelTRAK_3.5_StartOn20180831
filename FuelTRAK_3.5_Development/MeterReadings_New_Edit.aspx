<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MeterReadings_New_Edit.aspx.vb"
    Inherits="MeterReadings_New_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MeterReading</title>

    <script type="text/javascript" src="Javascript/DateTime.js"></script>

    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/MeterReadings.js"></script>

    <script type="text/javascript" src="Javascript/Validation.js"></script>
    
    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>
     <script language="javascript" type="text/javascript">
     
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

    function DeleteMsg()
    {   
        alert("Record deleted Successfully");
        location.href="MeterReadings.aspx";
    }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
         <asp:HiddenField runat="server" ID="hdfRecord" Value ="" />
        <table align="center">
            <tr>
                <td>
                    <table align="center" class="SixHundredPXTable">
                        <tr>
                            <td>
                                <table align="center" id="tblEditDept" runat="server" style="width: 491px" >
                                    <tr>
                                        <td colspan="2" class="MainHeader" style="height:50px">
                                            <asp:Label ID="lblNew_Edit" runat="server" Text="Add Meter Reading"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width:100px"></td>
                                        <td style="width:280px"></td>
                                    </tr>
                                    <tr>
                                        <td class="StdLableTxtLeft" style="height:32px">
                                            <asp:Label ID="lblSentry" runat="server" Text="Sentry:"></asp:Label></td>
                                        <td style="text-align:left; height:32px; width: 280px;">
                                            <asp:DropDownList ID="DDLstSentry" runat="server" cssclass="ThirtyCharTxtBox" TabIndex="1">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="StdLableTxtLeft" style="height:32px">
                                            <asp:Label ID="lblHose" runat="server" Text="Hose:"></asp:Label></td>
                                        <td style="text-align:left; height:32px; width: 280px;">
                                            <asp:TextBox ID="TxtHoseDDl" runat="server" MaxLength="2" CssClass="ThreeCharTxtBox" TabIndex="2"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtHoseDDl"
                                                ErrorMessage="Hose Number must be 1 through 8 !" MaximumValue="8" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="StdLableTxtLeft" style="height:32px">
                                            <asp:Label ID="lblreading" runat="server" Text="Reading:"></asp:Label></td>
                                        <td style="text-align:left; height:32px; width: 280px;">
                                            <asp:TextBox ID="txtReading" runat="server" Cssclass="TenCharTxtBox" MaxLength="10" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVtxtReading" runat="server" ErrorMessage="Please enter meter reading"
                                                Font-Names="Verdana" Font-Size="Small" ControlToValidate="txtReading">*</asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RVtxtReading" runat="server" ErrorMessage="Reading is out of range"
                                                ControlToValidate="txtReading" Font-Names="Verdana" Font-Size="Small" MaximumValue="99999999.9"
                                                Type="Double" MinimumValue="0.1" Display="Dynamic">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="StdLableTxtLeft" style="height:32px">
                                            <asp:Label ID="lblTime" runat="server" Text="Time:"></asp:Label></td>
                                        <td style="text-align:left; height:32px; width: 280px;">
                                            <asp:TextBox ID="txtTime" runat="server" Cssclass="FiveCharTxtBox" MaxLength="5" TabIndex="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_Time" runat="server" ControlToValidate="txtTime"
                                                ErrorMessage="Please enter time" Font-Names="Verdana" Font-Size="Small" Display="Dynamic"
                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTime"
                                                ErrorMessage="Invalid Time" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="StdLableTxtLeft">
                                            <asp:Label ID="lblDate" runat="server" BorderColor="White" Text="Date:"></asp:Label></td>
                                        <td style="text-align:left; width: 280px;">
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="EightCharTxtBox" MaxLength="10" TabIndex="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                                ErrorMessage="Please enter date" Font-Names="Verdana" Font-Size="Small" Display="Dynamic"
                                                SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate"
                                                Display="Dynamic" ErrorMessage="Date is Invalid" SetFocusOnError="True"
                                                ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                                <img id="StartDateImage" alt="Start Date" onclick="Javascript:cal1.popup()" runat="server" src="images/cal.gif"
                                                    style="cursor: hand" TabIndex="5" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left">
                                            <asp:TextBox ID="txtHose" runat="server" CssClass="TwoCharTxtBox" MaxLength="2" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" style="height:50px">
                                            <asp:Button ID="btnOk" runat="server" Text="Add Another" CssClass="Meter_New_Button" TabIndex="6" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Meter_New_Button"
                                                CausesValidation="False" TabIndex="7" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Meter_New_Button"
                                                CausesValidation="False" OnClick="btnDelete_Click" Visible="False" TabIndex="8" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Meter_New_Button" TabIndex="9" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" style="height:32px">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    <tr>
                        <td>
                            <table id="tblFindRec" class="SixHundredPXTable">
                                <tr>
                                    <td class="Meter_New_tblFindRec_TD">
                                        <asp:Button ID="btnFirst" runat="server" Text="|<" CssClass="Meter_New_FotterButton"
                                            CausesValidation="False" />
                                        <asp:Button ID="btnprevious" runat="server" Text="<" CssClass="Meter_New_FotterButton"
                                            CausesValidation="False" />
                                        <asp:Label ID="lblof" runat="server" Text="Label" CssClass="Meter_New_FotterLabel" 
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                                        <asp:Button ID="btnNext" runat="server" Text=">" CssClass="Meter_New_FotterButton"
                                            CausesValidation="False" />
                                        <asp:Button ID="btnLast" runat="server" Text=">|" CssClass="Meter_New_FotterButton"
                                            CausesValidation="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     </form>
     <script type="text/javascript" language="javascript">				
    
     if (document.getElementById('txtDate'))
    {
       
	    var cal1 = new calendar2(document.getElementById('txtDate'));
	    cal1.year_scroll = true;
	    cal1.time_comp = false;
	}
				
    </script>

</body>
</html>
