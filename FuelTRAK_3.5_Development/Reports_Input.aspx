<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reports_Input.aspx.vb" Inherits="Reports_Input" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>

    <script type="text/javascript" language="javascript">

        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }

        function getQueryVariable(variable) 
        {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i=0;i<vars.length;i++) 
            {
                var pair = vars[i].split("=");
                if (pair[0] == variable) {return pair[1];            }
            } 
            alert('Query Variable ' + variable + ' not found');
        }
        
        
        
        function Run_Report(url)    { window.open(url);        }
        
        function KeyUpEvent_StartDateTextBox(e)
        {   var str = document.getElementById('StartDateTextBox').value;
            if(str.length == 2)
            { document.getElementById('StartDateTextBox').value = str + "/";            }
            else if(str.length == 5)
            { document.getElementById('StartDateTextBox').value = str + "/";            }      
        }
    
        function KeyUpEvent_EndDateTextBox(e)
        {   var str = document.getElementById('EndDateTextBox').value;
            if(str.length == 2)
           {    document.getElementById('EndDateTextBox').value = str + "/";           }
           else if(str.length == 5)
           {    document.getElementById('EndDateTextBox').value = str + "/";           }       
        }
        
        function OpenPopUp(strFlg,strTxtName) 
        { 
            var objWin, varURL 
            varURL = 'PopUp_ReportInput.aspx?PopType='+ strFlg + '&TxtName=' + strTxtName
            objWin = window.open(varURL, 'List','titlebar=0,toolbar=no,height=355,width=500,top=150,left=150,status=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false'); 
        }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div align="center">
            <table class="SevenHundredPXTable">
                <tr>
                    <td valign="middle" colspan="5" class="MainHeader" style="height: 50px">
                        <asp:Label ID="lblRepName" runat="server" Text="Report Name"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="5" align="center">
                        <asp:Label ID="lblValidateDigitString" runat="server" ForeColor="Red" Text="Label"
                            Visible="False"></asp:Label></td>
                </tr>
                <tr>
                    <td valign="middle" colspan="5" align="center">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="StartDateTextBox"
                            ErrorMessage="Invalid from date" Font-Names="arial" Font-Size="Small" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                            ToolTip="From date must be in MM/DD/YYYY format." Display="Dynamic">From date must be in MM/DD/YYYY format.</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="StartDateTextBox"
                            ErrorMessage="From date is required" Font-Names="arial" Font-Size="Small" ToolTip="From date is compulsory to view report"
                            Display="Dynamic">From date is compulsory to view report</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator3" runat="server" ControlToValidate="StartTimeTextBox"
                                ErrorMessage="Invalid from time" Font-Names="arial" Font-Size="Small" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])"
                                ToolTip="From time must be in 24 Hours format" Display="Dynamic">From time must be in 24 Hours format</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="EndDateTextBox"
                                    ErrorMessage="To date is required" Font-Names="arial" Font-Size="Small" ToolTip="To date is compulsory to view report"
                                    Display="Dynamic">To date is compulsory to view report</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator2" runat="server" ControlToValidate="EndDateTextBox"
                                        ErrorMessage="Invalid to date" Font-Names="arial" Font-Size="Small" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                                        ToolTip="To date must be in MM/DD/YYYY format." Display="Dynamic">To date must be in MM/DD/YYYY format.</asp:RegularExpressionValidator><asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator4" runat="server" ControlToValidate="EndTimeTextBox"
                                            ErrorMessage="Invalid to time" Font-Names="arial" Font-Size="Small" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])"
                                            ToolTip="To time must be in 24 Hours format" Display="Dynamic">To time must be in 24 Hours format</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMPG"
                            ErrorMessage="MPG value is required" Font-Names="arial" Font-Size="Small" ToolTip="MPG value is compulsory to view report"
                            Display="Dynamic">MPG value is compulsory to view report</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                    </td>
                    <td class="RptInputLbl" style="width: 100px">
                        <label id="Label1" runat="server">
                            From</label></td>
                    <td style="width: 75px">
                    </td>
                    <td class="RptInputLbl" style="width: 75px">
                        <label id="Label2" runat="server">
                            To</label></td>
                    <td style="width: 75px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="DateTR" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartdateLabel" runat="server" Text="Date:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="1"></asp:TextBox><label id="Label3" runat="server">
                                <font style="color: Red;">*</font></label>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <img id="StartDateImage" alt="Start Date" onclick="Javascript:cal1.popup()" runat="server"
                            src="images/cal.gif" style="cursor: hand" tabindex="2" /></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="2"></asp:TextBox>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <img id="EndDateImage" alt="End Date" onclick="Javascript:cal2.popup()" runat="server"
                            src="images/cal.gif" style="cursor: hand" tabindex="4" /></td>
                    <td valign="middle" align="left" style="height: 32px">
                    </td>
                </tr>
                <tr id="TimeTR" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartTimeLabel" runat="server" Text="Time:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartTimeTextBox" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                            TabIndex="3"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndTimeTextBox" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                            TabIndex="4"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="Vehicle" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartVehLabel" runat="server" Text="Vehicle ID:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartVehTextBox" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="5"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndVehTextBox" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="6"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="Department" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartDeptLabel" runat="server" Text="Department #:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartDeptTextBox" runat="server" CssClass="SevenCharTxtBox" MaxLength="3"
                            TabIndex="7"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndDeptTextBox" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                            TabIndex="8"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="PersonnelLastName" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartPerLabel" runat="server" Text="Personnel Last Name:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartPerTextBox" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="9"></asp:TextBox></td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndPerTextBox" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="10"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="Tank" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartTankTabel" runat="server" Text="Tank #:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartTankTextBox" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                            TabIndex="11"></asp:TextBox>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <input id="btnTankFrom" type="button" value="..." style="width: 25px" onclick="OpenPopUp('Tank','StartTankTextBox');" /></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndTankTextBox" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                            TabIndex="12"></asp:TextBox></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <input id="btnTankTo" type="button" value="..." style="width: 25px" onclick="OpenPopUp('Tank','EndTankTextBox');" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="VehicleType" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartVehTypeLabel" runat="server" Text="Vehicle Type:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartVehTypeTextBox" runat="server" CssClass="TwoCharTxtBox" MaxLength="2"
                            TabIndex="13"></asp:TextBox></td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndVehTypeTextBox" runat="server" CssClass="TwoCharTxtBox" MaxLength="2"
                            TabIndex="14"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="VehicleKey" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartVKeyLabel" runat="server" Text="Vehicle Key#:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartVKeyTextBox" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                            TabIndex="15"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndVKeyTextBox" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                            TabIndex="16"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="Sentry" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="StartSentryLabel" runat="server" Text="Sentry#:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="StartSentryTextBox" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                            TabIndex="17"></asp:TextBox></td>
                    <td valign="middle" align="left">
                        <input id="btnSentryFrom" type="button" value="..." style="width: 25px" onclick="OpenPopUp('Sentry','StartSentryTextBox');" />
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="EndSentryTextBox" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                            TabIndex="18"></asp:TextBox></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <input id="btnSentryTo" type="button" value="..." style="width: 25px" onclick="OpenPopUp('Sentry','EndSentryTextBox');" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="PerNum" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="Label4" runat="server" Text="Personnel ID:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="txtStartPerNum" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="19"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="txtEndPerNum" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="20"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="SPNNum" runat="server">
                    <td class="StdLableTxtLeft" style="height: 32px; vertical-align: middle">
                        <asp:Label ID="lblSPN" runat="server" Text="SPN ID:"></asp:Label></td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="txtSPNStart" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="19"></asp:TextBox>
                        <asp:RegularExpressionValidator ControlToValidate="txtSPNStart" ErrorMessage="Please Enter Numbers only in SPN."
                            ID="revtxtSPNStart" runat="server" Display="Dynamic" Font-Names="arial" Font-Size="Small"
                            ValidationExpression="^\d+$">*
                        </asp:RegularExpressionValidator>
                    </td>
                    <td>
                    </td>
                    <td valign="middle" align="left" style="height: 32px">
                        <asp:TextBox ID="txtSPNEnd" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="20"></asp:TextBox></td>
                    <td>
                        <asp:RegularExpressionValidator ControlToValidate="txtSPNEnd" Font-Names="arial"
                            Font-Size="Small" ErrorMessage="Please Enter Numbers only in SPN." ID="revtxtSPNEnd"
                            runat="server" Display="Dynamic" ValidationExpression="^\d+$">*
                        </asp:RegularExpressionValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trTransMpgRpt" runat="server">
                    <td class="StdLableTxtLeft" style="vertical-align: middle; height: 32px">
                        <asp:Label ID="Label5" runat="server" Text="MPG:"></asp:Label>
                    </td>
                    <td align="left" style="height: 32px" valign="middle">
                        <asp:TextBox ID="txtMPG" runat="server" CssClass="TenCharTxtBox" MaxLength="9" TabIndex="21"></asp:TextBox>
                        <asp:CompareValidator ID="validator" runat="server" ControlToValidate="txtMPG" Operator="DataTypeCheck"
                            Type="Double" Display="Dynamic" ErrorMessage="Please Enter Numbers only in MPG.">*</asp:CompareValidator>
                    </td>
                    <td>
                    </td>
                    <td align="left" style="height: 32px" valign="middle">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="5" valign="middle" style="height: 50px">
                        <asp:CheckBox ID="CheckBox" runat="server" Font-Names="arial" Font-Size="Small" Font-Bold="true"
                            ForeColor="black" Visible="false" TabIndex="21" /></td>
                </tr>
                <tr>
                    <td align="center" colspan="5" valign="middle">
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="5" align="center" style="height: 50px">
                        <asp:Button ID="btncancel" runat="server" AccessKey="c" CausesValidation="False"
                            CssClass="Sentry_New_Button" Text="Cancel" Width="90px" />
                        <asp:Button ID="RunReportButton" runat="server" Text="Run Report" Width="90px" TabIndex="22" /></td>
                </tr>
                <tr>
                    <td align="center" colspan="5" valign="middle">
                        &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="5" align="right">
                        <input id="txtReportID" runat="server" type="text" visible="false" /></td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>

    <script type="text/javascript" language="javascript">				
        //if (document.getElementById('StartDateTextBox').style.visibility = "visible")
        
         if (document.getElementById('StartDateTextBox'))
        {
	        var cal1 = new calendar2(document.getElementById('StartDateTextBox'));
	        cal1.year_scroll = true;
	        cal1.time_comp = false;
	    }
//	    else 
        if (document.getElementById('EndDateTextBox'))
	    {
	        var cal2 = new calendar2(document.getElementById('EndDateTextBox'));
	        cal2.year_scroll = true;
	        cal2.time_comp = false;
	    }
    </script>

</body>
</html>
