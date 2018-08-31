<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Vehicle_Edit.aspx.vb" Inherits="Vehicle_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Untitled Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="Javascript/vehicle.js"></script>

    <script type="text/javascript" src="Javascript/Validation.js"></script>

    <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
	   
        function CheckNextPM()
        {
             var currMiles = document.getElementById("txtCOdometer").value * 1;              
             var nextpmmile = document.getElementById("txtNxtPmOdo").value * 1;  
            if(nextpmmile>0)
            {
                 if(currMiles > nextpmmile*1) 
                  {
                    alert("Please enter next PM odometer greater than current odometer.");
                    document.getElementById('txtNxtPmOdo').focus();
                    document.getElementById('txtNxtPmOdo').value = '';              
                  }     
             }
        }

        function checkLock()
        {
            var ac=confirm('This key will be PERMANANTLY locked out !\n Is this what you want to do ?');
            document.Form1.txtLost.value=ac;//
            Form1.submit(); 
        }
        
           function DeleteMsg()
            {   
                alert("Record deleted Successully");
                location.href="Vehicle.aspx"
            }
            function SaveMsg()
            {   
                alert("Record Save Successfully");
                location.href="Vehicle.aspx"
            }
            function SelectProduct()
            {
                if (document.getElementById('btnProduct').value == '- None -')
                {
                    alert('Please select Product.');
                    document.getElementById('txtVId').value ="False"
                }
                else
                {document.getElementById('txtVId').value ="true"}
            }           
           


            function checkMW()
            {

                var value = document.getElementById('<%=txtMileage.ClientID%>').value;
                if(value=="")
                {
                alert('Please enter Mileage Window.');
                return false;
                }
                else
                return true;
            }
    </script>

</head>
<body class="HomeFrame">
    <form id="Form1" runat="server">
    <input type="hidden" id="txtVId" runat="server" />
    <div runat="server" id="MainDiv">
        <table align="center" class="MaximumPXTable" style="width: 1024px; overflow: scroll;">
            <tr>
                <td class="MainHeader" colspan="6" style="height: 32px">
                    <asp:Label ID="lblNew_Edit" runat="server" Text="Add New Vehicle"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MaximumPXTable" colspan="6" style="height: 25px">
                    <asp:TextBox ID="txtvehicleIdHide" runat="server" Visible="False" Height="10px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="VehID" runat="Server" Text="Vehicle ID:"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtVehicleID" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                        TabIndex="1"></asp:TextBox>
                </td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVehicleID"
                    ErrorMessage="Vehicle ID required" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"
                    Display="Dynamic" ValidationGroup="G1">*</asp:RequiredFieldValidator><td class="StdLableTxtLeft"
                        style="width: 217px">
                        <asp:Label ID="Dept" runat="server" Text="Department:"></asp:Label>
                    </td>
                <td style="width: 252px; text-align: left;">
                    <asp:DropDownList ID="DDLstDepartment" runat="server" Style="position: absolute;
                        top: 82px;" CssClass="TwentyFiveCharTxtBox" onclick="this.size=1;" onMouseOver="this.size=20;"
                        onMouseOut="this.size=1;">
                    </asp:DropDownList>
                </td>
                <td class="StdLableTxtLeft" style="width: 200px">
                    <asp:Label ID="LastFueler" runat="server" Text="Last Fueler"></asp:Label>
                </td>
                <td style="text-align: left; width: 206px;">
                    <asp:TextBox ID="txtLastfueler" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                        TabIndex="22" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="Make" runat="server" Text="Make:"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtMake" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20"
                        TabIndex="2"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px">
                    <asp:Label ID="SubDept" runat="server" Text="Sub Department:" Width="110px"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;">
                    <asp:TextBox ID="txtSubdepartment" runat="server" CssClass="TwentyCharTxtBox" TabIndex="13"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 200px">
                    <asp:Label ID="LastFuelDate" runat="server" Text="Last Fuel Date/Time:"></asp:Label>
                </td>
                <td style="text-align: left; width: 206px;">
                    <asp:TextBox ID="txtFueldatetime" runat="server" CssClass="SixteenCharTxtBox" Enabled="False"
                        TabIndex="23"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="Model" runat="server" Text="Model:"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtModel" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20"
                        TabIndex="3"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px">
                    <asp:Label ID="AcctID" runat="server" Text="Acct ID:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;">
                    <asp:TextBox ID="txtAccid" runat="server" CssClass="TenCharTxtBox" TabIndex="14"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 200px">
                    <asp:Label ID="ThisVehicle" runat="server" Text="This Vehicle Uses:"></asp:Label>
                </td>
                <td style="text-align: left; width: 206px;">
                    <asp:Button ID="btnVehicleUses" runat="server" TabIndex="24" BackColor="#A5BBC5"
                        ForeColor="#A50000" Height="24px" Width="80px" Text="Mileage" CausesValidation="False"
                        Font-Bold="true" />
                    <label style="font-style: normal; font-size: 7pt">
                        Click to change</label>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="Type" runat="server" Text="Type:"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtType" runat="server" CssClass="FifteenCharTxtBox" MaxLength="15"
                        TabIndex="4"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px">
                    <asp:Label ID="ExportCode" runat="server" Text="Export Code:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;">
                    <asp:TextBox ID="txtExportcode" runat="server" CssClass="TwentyFiveCharTxtBox" MaxLength="25"
                        TabIndex="15"></asp:TextBox>
                </td>
                <td colspan="2" style="text-align: left; width: 400px;">
                    <div id="divmile" runat="server">
                        <table style="width: 400px">
                            <tr>
                                <td class="StdLableTxtLeft" style="width: 200px">
                                    <asp:Label ID="Label1" runat="server" Text="Current Odometer: "></asp:Label>
                                </td>
                                <td style="text-align: left; width: 200px;">
                                    <asp:TextBox ID="txtCOdometer" runat="server" CssClass="EightCharTxtBox" TabIndex="25">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="width: 200px">
                                    <asp:Label ID="Label10" runat="server" Text="Previous Odometer: "></asp:Label>
                                </td>
                                <td style="text-align: left; width: 200px;">
                                    <asp:TextBox ID="txtPOdometer" runat="server" CssClass="EightCharTxtBox" TabIndex="25">0</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divHours" runat="server">
                        <table style="width: 400px">
                            <tr>
                                <td class="StdLableTxtLeft" style="width: 200px">
                                    <asp:Label ID="Label2" runat="server" Text="Current Hours:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 200px;">
                                    <asp:TextBox ID="txtCHours" runat="server" CssClass="SixCharTxtBox" MaxLength="6"
                                        TabIndex="26">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="width: 200px">
                                    <asp:Label ID="Label11" runat="server" Text="Previous Hour: "></asp:Label>
                                </td>
                                <td style="text-align: left; width: 200px;">
                                    <asp:TextBox ID="txtPHours" runat="server" CssClass="SixCharTxtBox" MaxLength="6"
                                        TabIndex="26">0</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="Year" runat="server" Text="Year:"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtYear" runat="server" CssClass="FourCharTxtBox" MaxLength="4"
                        TabIndex="5"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px">
                    <asp:Label ID="TagID" runat="server" Text="Tag ID:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;">
                    <asp:TextBox ID="txtTagID" runat="server" CssClass="SixteenCharTxtBox" MaxLength="16"
                        TabIndex="16"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="LicenseNo" runat="server" Text="License #:"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtLicense" runat="server" CssClass="NineCharTxtBox" MaxLength="9"
                        TabIndex="6"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px">
                    <asp:Label ID="KeyNo" runat="server" Text="Key #:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;">
                    <asp:TextBox ID="txtKey" runat="server" CssClass="FiveCharTxtBox" MaxLength="5" Enabled="False"
                        ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="lblLoskKey" runat="server" Visible="false" TabIndex="15" BackColor="darkRed"
                        ForeColor="white" Text="Key Has Been Locked."></asp:Label>
                </td>
                <td class="StdLableTxtLeft" style="width: 200px">
                    <asp:Label ID="lblActCodes" runat="server" Text="Number of Active Codes:"></asp:Label>
                </td>
                <td style="width: 206px" align="left">
                    <asp:TextBox ID="txtActCodes" runat="server" CssClass="ThreeCharTxtBox" MaxLength="16"
                        TabIndex="27" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="VINNo" runat="server" Text="VIN Number:"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtVinNumber" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20"
                        TabIndex="7"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px">
                    <asp:Label ID="KeyExp" runat="server" Text="Key Exp:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;">
                    <asp:TextBox ID="txtKeyexp" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"
                        TabIndex="18"></asp:TextBox>
                    <label style="font-style: normal; font-size: 7pt">
                        (mm/yyyy)</label>
                </td>
                <td valign="middle" class="StdLableTxtLeft" style="width: 100px">
                    <asp:Label ID="lblCode1" runat="server" Text="Trouble Codes:"></asp:Label>
                </td>
                <td valign="middle" class="StdLableTxtLeft" style="width: 200px">
                    <asp:DropDownList ID="ddlCodes" runat="server" Width="160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 42px;">
                    <asp:Label ID="Description" runat="server" Text="Description:"></asp:Label>
                </td>
                <td class="VehicleEditTDBox" style="width: 224px; height: 42px;">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="TwentyCharTxtBox" MaxLength="25"
                        TabIndex="8"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFV_Description" runat="server" ControlToValidate="txtDescription"
                        ErrorMessage="Description Missing" Font-Names="Verdana" Font-Size="Small" Font-Bold="True"
                        Display="Dynamic" ValidationGroup="G1">*</asp:RequiredFieldValidator>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px; height: 42px;">
                    <asp:Label ID="CardNo" runat="server" Text="Card #:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px; height: 42px;">
                    <asp:TextBox ID="txtCard" runat="server" CssClass="TenCharTxtBox" TabIndex="19"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 200px; text-align: left">
                    <asp:Label ID="LabeMaxEngtemp" runat="server" Text="Max Engine Temp:"></asp:Label>
                </td>
                <td style="width: 206px; text-align: left">
                    <asp:TextBox ID="TxtMaxEngTemp" runat="server" CssClass="FourCharTxtBox" MaxLength="4"
                        TabIndex="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="Products" runat="server" Text="Products:"></asp:Label>
                    <input type="hidden" runat="server" id="txtprodval1" value="- None -" class="VehicleEditHidden" />
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:Button ID="btnProduct" runat="server" TabIndex="9" Text="- None -" Width="85px"
                        Font-Names="Times New Roman" Font-Size="Small" CausesValidation="False" /><asp:CustomValidator
                            ID="CustomValidator1" runat="server" ErrorMessage="*" ClientValidationFunction="SelectProduct();">*</asp:CustomValidator>
                    <label style="font-style: normal; font-size: 7pt">
                        Click to change</label>
                </td>
                <td class="StdLableTxtLeft" style="width: 217px">
                    <asp:Label ID="CardExp" runat="server" Text="Card Exp:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;">
                    <asp:TextBox ID="txtCardexp" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"
                        TabIndex="20"></asp:TextBox>
                    <label style="font-style: normal; font-size: 7pt">
                        (mm/yyyy)</label>
                </td>
                <td rowspan="2" class="StdLableTxtLeft" style="width: 200px; text-align: left; vertical-align: top;
                    padding-top: 12px">
                    <asp:Label ID="LabelMaxRPM" runat="server" Text="Max RPM:"></asp:Label>
                </td>
                <td rowspan="2" style="width: 206px; text-align: left; vertical-align: top; padding-top: 10px">
                    <asp:TextBox ID="TxtMaxRPM" runat="server" CssClass="FourCharTxtBox" MaxLength="4"
                        TabIndex="31"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="Label3" runat="server" Text="Mileage Window:" Width="130px"></asp:Label>
                </td>
                <td style="text-align: left; width: 224px;">
                    <asp:TextBox ID="txtMileage" runat="server" CssClass="FourCharTxtBox" TabIndex="10">0300</asp:TextBox>
                </td>
                <td valign="top" class="StdLableTxtLeft" rowspan="2" style="width: 217px">
                    <asp:Label ID="Comments" runat="server" Text="Comments:"></asp:Label>
                </td>
                <td style="text-align: left; width: 252px;" rowspan="2">
                    <asp:TextBox ID="txtComments" runat="server" TabIndex="21" Font-Names="Verdana" Font-Size="Small"
                        Height="51px" CssClass="FifteenCharTxtBox" MaxLength="3" TextMode="MultiLine"
                        Width="246px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 210px; height: 30px;">
                    <asp:Label ID="lblGPSId" runat="server" Text="GPS ID:"></asp:Label>
                </td>
                <td class="StdLableTxtLeft" style="width: 224px" align="left">
                    <asp:TextBox ID="txtGPSId" runat="server" CssClass="TenCharTxtBox" MaxLength="16"
                        TabIndex="11" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="DeletedLabel">
                    <asp:Label ID="lblDelVeh" runat="server" Text="Deleted Vehicle" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center" style="background: #A5BBC5; width: 70%; border-top: solid 1px black;
            border-bottom: solid 1px black; border-left: solid 1px black; border-right: solid 1px black">
            <tr>
                <td class="StdLableTxtLeft" style="width: 185px">
                    <asp:Label ID="FuelLimitPer" runat="server" Text="Fuel Limit Per Transaction"></asp:Label>
                </td>
                <td style="width: 147px; text-align: left">
                    <asp:DropDownList ID="DDLFuelLimit" runat="server" Width="60px" BorderStyle="Solid"
                        BorderWidth="1px" TabIndex="32">
                        <asp:ListItem>NO</asp:ListItem>
                        <asp:ListItem>004</asp:ListItem>
                        <asp:ListItem>008</asp:ListItem>
                        <asp:ListItem>012</asp:ListItem>
                        <asp:ListItem>016</asp:ListItem>
                        <asp:ListItem>020</asp:ListItem>
                        <asp:ListItem>024</asp:ListItem>
                        <asp:ListItem>032</asp:ListItem>
                        <asp:ListItem>040</asp:ListItem>
                        <asp:ListItem>052</asp:ListItem>
                        <asp:ListItem>060</asp:ListItem>
                        <asp:ListItem>080</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>120</asp:ListItem>
                        <asp:ListItem>152</asp:ListItem>
                        <asp:ListItem>180</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFuellimit" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                        BorderStyle="Solid" BorderWidth="1px" Visible="False"></asp:TextBox>
                </td>
                <td style="width: 25px">
                </td>
                <td class="StdLableTxtLeft" style="width: 168px">
                    <asp:Label ID="Daily" runat="server" Text="Daily Transaction Limit:"></asp:Label>
                </td>
                <td colspan="2" align="left" style="width: 150px">
                    <asp:TextBox ID="txtDailylimit" runat="server" CssClass="ThreeCharTxtBox" MaxLength="2"
                        TabIndex="30"></asp:TextBox><asp:CheckBox ID="CboxNolimit" runat="server" AutoPostBack="True"
                            CssClass="VehicleEditTDSubChk" Text="No Limit" Checked="True" Width="88px" TabIndex="36" />
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 26px; width: 185px;">
                    <asp:Label ID="FuelerCard" runat="server" Text="Master Key:"></asp:Label>
                </td>
                <td class="VehicleEditTDSubChk" style="width: 147px">
                    <input id="CboxFuelercard" type="checkbox" value="" runat="server" tabindex="33" />
                </td>
                <td>
                </td>
                <td class="StdLableTxtLeft" style="width: 168px">
                    <asp:Label ID="Disabled" runat="server" Text="Disable (temporary)"></asp:Label>
                </td>
                <td class="VehicleEditTDSubChk" style="width: 160px">
                    <input id="CboxDisabled" type="checkbox" value="" runat="server" tabindex="37" />
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 26px; width: 185px;">
                    <asp:Label ID="SecondKey" runat="server" Text="Second Key:"></asp:Label>
                </td>
                <td class="VehicleEditTDSubChk" style="width: 147px">
                    <input id="CboxSecondkey" type="checkbox" value="" runat="server" tabindex="34" />
                </td>
                <td>
                </td>
                <td class="StdLableTxtLeft" style="width: 168px">
                    <asp:Label ID="FacilityCard" runat="server" Text="Facility Card:"></asp:Label>
                </td>
                <td class="VehicleEditTDSubChk" style="width: 160px">
                    <input id="CboxFacilitycard" type="checkbox" value="" runat="server" tabindex="38" />
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 26px; width: 185px;">
                    <asp:Label ID="CheckMiles" runat="server" Text="Check Miles:"></asp:Label>
                </td>
                <td class="VehicleEditTDSubChk" style="width: 147px">
                    <input id="CboxCheckmiles" type="checkbox" value="" runat="server" tabindex="35" />
                </td>
                <td>
                </td>
                <td class="StdLableTxtLeft" style="width: 168px">
                    <asp:TextBox ID="txtPMValue" runat="server" BorderColor="transparent" BorderStyle="None"
                        ForeColor="black" BackColor="#A5BBC5" Width="12px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 185px; height: 28px;">
                </td>
                <td style="width: 147px; height: 28px;">
                </td>
                <td style="height: 28px">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 24px; text-align: center">
                    <input id="txtLost" runat="server" style="width: 32px" type="hidden" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Names="Verdana"
                        Font-Size="Small" ShowMessageBox="True" ShowSummary="False" ValidationGroup="G1" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table align="center">
                        <tr>
                            <td align="center" class="VehicleEditTD" style="width: 74px; height: 27px;">
                                <asp:Button ID="btnEncodeKey" runat="server" Text="Encode Key" CssClass="VehicleEditBtn2"
                                    Enabled="true" Visible="false" />
                            </td>
                            <%--<td align="center"  style="border: 1px solid black; height: 27px;">--%>
                            <td>
                                <asp:HyperLink ID="encodeKeyLink" CssClass="VehicleEditBtn2" Style="text-decoration: none"
                                    runat="server" Text="Encode Key" Width="90px" BorderWidth="1px" Height="20px"
                                    BackColor="#DDDDDD" ForeColor="Black" />
                                <%--ImageUrl="~/images/ButtonEncodeKey3.gif" />--%>
                                <%-- <asp:ImageButton  ID="encodeKeyLink" runat="Server"  CssClass="VehicleEditBtn2"
                                        ImageUrl="~/images/ButtonEncodeKey3.gif" />--%>
                            </td>
                            <td align="center" class="VehicleEditTD" style="height: 27px">
                                <input id="btnLostKey" type="button" value="Lost Key" runat="server" onclick="checkLock();"
                                    disabled="disabled" class="VehicleEditBtn2" />
                            </td>
                            <td align="center" class="VehicleEditTD" style="height: 27px">
                                <input id="btnLostCard" type="button" value="Lost Card" runat="server" onclick="checkLock();"
                                    disabled="disabled" class="VehicleEditBtn2" />
                            </td>
                            <td align="center" class="VehicleEditTD" style="height: 27px">
                                <asp:Button ID="btnMessage" runat="server" Text="Messages" CssClass="VehicleEditBtn2" />
                            </td>
                            <td align="center" class="VehicleEditTD" style="height: 27px">
                                <asp:Button ID="btnPM" runat="server" Text="PM" CssClass="VehicleEditBtn2" CausesValidation="False" />
                            </td>
                            <td align="center" class="VehicleEditTD" style="width: 132px; height: 27px;">
                                <asp:Button ID="btnOk" runat="server" Text="Add Another" CssClass="VehicleEditBtn2" />
                            </td>
                            <td align="center" class="VehicleEditTD" style="height: 27px">
                                <asp:Button ID="btnDeleteVehicle" runat="server" Text="Delete" CssClass="VehicleEditBtn2"
                                    CausesValidation="False" Enabled="False" />
                            </td>
                            <td style="height: 27px">
                                <asp:Button ID="btnVehPath" CssClass="VehicleEditBtn2" Text="GPS" Width="106px" runat="server" />
                                <%-- <asp:ImageButton ImageUrl="~/images/ButtonGPS.gif" ID="btnVehPath" CssClass="VehicleEditBtn2"
                                        Width="106px" runat="server" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="VehicleEditTDBtn" colspan="9" style="height: 26px">
                                <asp:Button ID="btnFirst" runat="server" CausesValidation="False" Text="|<" Width="50px" /><asp:Button
                                    ID="btnprevious" runat="server" CausesValidation="False" Text="<" Width="50px" /><asp:Label
                                        ID="lblof" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" Text="Label"
                                        Width="115px"></asp:Label><asp:Button ID="btnNext" runat="server" CausesValidation="False"
                                            Text=">" Width="50px" /><asp:Button ID="btnLast" runat="server" CausesValidation="False"
                                                Text=">|" Width="50px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td class="VehicleEditTDRight" align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="VehicleEditBtn" TabIndex="36"
                        OnClientClick="return checkMW()" />
                </td>
                <td class="VehicleEditTDLeft" align="left">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="VehicleEditBtn"
                        CausesValidation="False" TabIndex="37" />
                </td>
            </tr>
        </table>
    </div>
    <div id="PopPM" runat="server" style="position: absolute; border: outset 3px #00004C;
        background-color: #A5BBC5; width: 300px; height: 225px; top: 119px; left: 438px;">
        <table class="VehicleEditPMPopUp" id="TABLE1">
            <tr>
                <td colspan="2" class="VehicleEditPMPopUpHeader">
                    <label>
                        Enter PM Data:</label>
                </td>
            </tr>
            <tr>
                <td class="VehicleEditPMPopUpLbl">
                    <asp:Label ID="Label4" runat="server" CssClass="VehicleEditPMPopUpLbl" Text="Current Odometer:"
                        Width="143px"></asp:Label>
                </td>
                <td id="test">
                    <asp:TextBox ID="txtCurrOdomtr" runat="server" CssClass="VehicleEditPMPopUpInput"
                        Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="VehicleEditPMPopUpLbl">
                    <asp:Label ID="Label5" runat="server" CssClass="VehicleEditPMPopUpLbl" Text="Next PM Odometer:"
                        Width="139px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNxtPmOdo" MaxLength="7" runat="server" CssClass="VehicleEditPMPopUpInput"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="VehicleEditPMPopUpLbl">
                    <asp:Label ID="Label6" runat="server" CssClass="VehicleEditPMPopUpLbl" Text="PM Interval:"
                        Width="133px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPmIntr" MaxLength="5" runat="server" CssClass="VehicleEditPMPopUpInput"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="VehicleEditPMPopUpLbl">
                    <asp:Label ID="Label7" runat="server" CssClass="VehicleEditPMPopUpLbl" Text="PM Txtn Count:"
                        Width="143px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPmTxtn" MaxLength="2" runat="server" CssClass="VehPMTxtCnt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 36px" valign="bottom">
                    <asp:Button ID="btnDis" runat="server" Text="Discontinue Message" Font-Names="Verdana"
                        Font-Size="Small" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnPMOK" runat="server" Text="Ok" Font-Names="Verdana" Font-Size="Small"
                        CssClass="VehicleEditBtn" CausesValidation="False" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="Small"
                        CssClass="VehicleEditBtn" CausesValidation="False" />
                </td>
            </tr>
        </table>
        <%-- </center>--%>
    </div>
    <div id="PopUpProduct" runat="server" style="position: absolute; border: solid 3px #00004C;
        background-color: #A5BBC5; top: 77px; left: 645px;">
        <table class="VehicleEditTypePopUp" align="center">
            <tr>
                <td class="VehicleEditPMPopUpHeader" colspan="2">
                    <asp:Label ID="Label8" runat="server" Text="Check Box to Select Fuel Type(s)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd1" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd2" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2" style="height: 22px">
                                <asp:CheckBox ID="CheckProd3" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd4" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd5" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd6" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd7" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2" style="height: 22px">
                                <asp:CheckBox ID="CheckProd8" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd9" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd10" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd11" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd12" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd13" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd14" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                        <tr>
                            <td align="Left" colspan="2">
                                <asp:CheckBox ID="CheckProd15" runat="server" CssClass="VehicleEditTypePopUpTxt" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnProdOK" runat="server" Text="Ok" Height="24px" Width="65px" Font-Names="Verdana"
                        Font-Size="Small" CausesValidation="False" />
                    <asp:Button ID="btnProdClose" runat="server" Text="Cancel" Height="24px" Width="65px"
                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
    <div id="PopUpVehMsg" runat="server" style="position: absolute; border: solid 3px #00004C;
        background-color: #A5BBC5; top: 181px; left: 382px; width: 389px;">
        <table class="VehicleEditMsgPopUp" align="center" style="width: 100%; height: 100%">
            <tr>
                <td class="VehicleEditPMPopUpHeader" colspan="2">
                    <asp:Label ID="lblVehMsgPopUp" runat="server" Text="Vehicle Message"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="right" style="height: 26px">
                    <asp:Label ID="lblRemCnt" runat="server" Text="Remaining Count:"></asp:Label>
                </td>
                <td style="height: 26px; text-align: left;">
                    <asp:TextBox ID="txtVehMsgCnt" runat="server" Width="43px"></asp:TextBox>
                </td>
                <asp:RegularExpressionValidator ID="regexvalMsgCnt" ControlToValidate="txtVehMsgCnt"
                    ValidationExpression="[0-9]" runat="server" ErrorMessage="*Only Number."></asp:RegularExpressionValidator></tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblVMLastDT" runat="server" Text="LastDate:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtVMLastDT" runat="server" Width="101px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblVMExpDT" runat="server" Text="Exp.Date:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtVMExpDT" runat="server" Width="101px" MaxLength="10"></asp:TextBox>
                </td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtVMExpDT"
                    ErrorMessage="Invalid from date" Font-Names="arial" Font-Size="Small" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                    ToolTip="From date must be in MM/DD/YYYY format." Display="Dynamic">Date must be in MM/DD/YYYY format.</asp:RegularExpressionValidator></tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblMSGTop1" runat="server" Text="MessageTop Line:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVehMsg1" runat="server" CssClass="TwentyFiveCharTxtBox" MaxLength="25"
                        TabIndex="39"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="Right">
                    <asp:Label ID="lblMSGTop2" runat="server" Text="Message Bottom Line:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVehMsg2" runat="server" CssClass="TwentyFiveCharTxtBox" MaxLength="25"
                        TabIndex="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="Center">
                    <asp:Button ID="btnCustomMsg" runat="server" Text="Custom Message" Width="169px">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="Center">
                    <asp:Button ID="btnVehMsgDis" runat="server" Text="Discontinue Message" Width="169px">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 26px">
                    <asp:Button ID="btnVehMsgSave" runat="server" Text="OK" Height="24px" Width="65px"
                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" />
                    <asp:Button ID="btnVehMsgCancel" runat="server" Text="Close" Height="24px" Width="65px"
                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
    <div id="PopUpCustMsg" runat="server" style="position: absolute; border: solid 3px #00004C;
        background-color: #A5BBC5; top: 237px; left: 293px; width: 550px;">
        <table class="VehicleEditMsgPopUp" align="center" style="width: 100%">
            <tr>
                <td class="VehicleEditPMPopUpHeader" colspan="4">
                    <asp:Label ID="Label9" runat="server" Text="Custom Message"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox ID="lstbox1" runat="server" Width="250px" SelectionMode="Multiple" Rows="5">
                    </asp:ListBox>
                </td>
                <td>
                    <table id="spltTable" runat="server">
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnAddItems" runat="server" ImageUrl="~/images/header_bg_Expand.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnRemoveItems" runat="server" ImageUrl="~/images/header_bg_Collapse.gif" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:ListBox ID="lstbox2" runat="server" Width="250px" SelectionMode="Multiple" Rows="5">
                    </asp:ListBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="Center">
                    <asp:Button ID="btnCustClose" runat="server" Text="Close"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
