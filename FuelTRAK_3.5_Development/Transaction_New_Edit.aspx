<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Transaction_New_Edit.aspx.vb"
    Inherits="Transaction_New_Edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transaction_New_Edit</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Javascript/DateTime.js"></script>

    <script type="text/javascript" src="Javascript/transaction.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/jquery-1.4.2.min.js"></script>

    <script language="javascript" type="text/javascript">
        

    function Test()
    {
        document.form1.submit();
    }
    function EnterSentry()
    {
        document.getElementById('txtSentryId').value=document.getElementById('txtSentry').value
    }
    
    function redirecturl(no,rNo)
    {
    alert("Transaction saved successfully with Transaction Number :- " + no);
    location.href="Transaction_New_Edit.aspx?TxtnNo=" + rNo;
    }
    function DeleteMsg()
    {   
        alert("Record deleted Successfully");
        location.href="Transaction.aspx";
    }
    
    function OpenPopUp(strFlg,strTxtID,strTxtName) 
    { 
        var objWin, varURL 
        varURL = 'PopUp_ReportInput.aspx?PopType='+ strFlg + '&TxtID=' + strTxtID + '&TxtName=' + strTxtName
        objWin = window.open(varURL, 'List','titlebar=0,toolbar=no,height=355,width=500,top=150,left=150,status=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false'); 
    }

    function CheckSentry(strSentry,strSentryList)
    {
        var b = ''; 
        var s = strSentryList;
        while (s.length > strSentry)
        {
            var c = s.substring(0,strSentry);
            var d = c.lastIndexOf(' ');
            var e =c.lastIndexOf('\n');
            
            if (e != -1) d = e;
            if (d == -1) d = strSentry;
            b += c.substring(0,d) + '\n';
            s = s.substring(d+1);
        }
        return b+s;
    }
    
    function CheckCost()
    {
       var iPrice = document.getElementById('txtHideTankPrice').value;
       var iQty = document.getElementById('txtQty').value;
       if(iPrice > 0)
       {
           document.getElementById('txtCost').value = iPrice * iQty;
            
       }
    }
    var usernameCheckerTimer;
    

    function usernameChecker(username) 
    {
       
        clearTimeout(usernameCheckerTimer);
        if (username.length == 0)
            document.getElementById("spanAvailability").innerHTML = "";
        else
        {
            document.getElementById("spanAvailability").innerHTML = "<span style='color: #ccc;'>checking...</span>";
            usernameCheckerTimer = setTimeout("checkUsernameUsage('" + username + "');", 750);
        }
    }
       
     function ShowVehicle() {
     //alert($("#<%=txtVehID.ClientID%>")[0].value)
    $.ajax({
        type: "POST",
        url: "Transaction_New_Edit.aspx/GetVehicleInfo",
        data: '{vehID: "' + $("#<%=txtVehID.ClientID%>")[0].value + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function(response) {
            alert(response);          
        } 
    });
}
function OnSuccess(response) {
 
 if(response.d != null)
 {
     document.getElementById("spanAvailability").innerHTML = "<span style='color: DarkGreen;'>Vehicle Found.</span>";
      document.getElementById('txtDept').value = response.d;
   
      
 }
 else
 {
    document.getElementById('txtDept').value = '';
    document.getElementById("spanAvailability").innerHTML = "<span style='color: Red;'>Vehicle ID does't exists in the System.</span>";
 }
}   
   
    </script>

    <style type="text/css">
        .style1
        {
            text-align: left;
            font-family: Arial;
            font-size: small;
            font-weight: bold;
            width: 175px;
            height: 32px;
        }
        .style2
        {
            height: 32px;
        }
        .style3
        {
            text-align: left;
            font-family: Arial;
            font-size: small;
            font-weight: bold;
            width: 117px;
            height: 32px;
        }
        .style4
        {
            width: 231px;
            height: 32px;
        }
    </style>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
            EnablePageMethods="true">
        </asp:ScriptManager>
        <table class="NineHundredPXTable">
            <tr>
                <td class="MainHeader" colspan="4" style="height: 50px">
                    <asp:Label ID="lblNew_Edit" runat="server" Text="Edit Transaction Information"></asp:Label></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 32px; width: 175px">
                    <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label></td>
                <td style="text-align: left; width: 270px">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                        TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                    <img id="StartDateImage" alt="Start Date" onclick="Javascript:cal1.popup()" runat="server"
                        src="images/cal.gif" style="cursor: hand" tabindex="2"/>
                    <asp:RequiredFieldValidator ID="RFVDatetime" runat="server" ControlToValidate="txtDate"
                        ErrorMessage="Please enter Date" Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate"
                        ErrorMessage="Invalid date" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                        Display="Dynamic">*</asp:RegularExpressionValidator></td>
                <td class="StdLableTxtLeft" style="width: 117px">
                    <asp:Label ID="Label4" runat="server" Text="Key #:"></asp:Label></td>
                <td style="text-align: left; width: 231px">
                    <asp:TextBox ID="txtVechKey" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                        BackColor="lightgray"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 32px; width: 175px;">
                    <asp:Label ID="lblTime" runat="server" Text="Time:"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtTime" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                        AutoCompleteType="Disabled" TabIndex="3"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTime"
                        ErrorMessage="Invalid time" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])"
                        Display="Dynamic">*</asp:RegularExpressionValidator></td>
                <td class="StdLableTxtLeft" style="width: 117px">
                    <asp:Label ID="lblErr" runat="server" Text="Errors:"></asp:Label></td>
                <td style="text-align: left; width: 231px;">
                    <asp:TextBox ID="txtErrors" runat="server" CssClass="TenCharTxtBox" AutoCompleteType="Disabled"
                        TabIndex="10"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 32px; width: 175px;">
                    <asp:Label ID="Label11" runat="server" Text="Transaction Number:"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNumber" runat="server" CssClass="EightCharTxtBox" MaxLength="8"
                        Enabled="true" TabIndex="4"></asp:TextBox></td>
                <td class="StdLableTxtLeft" style="width: 117px">
                    <asp:Label ID="Label2" runat="server" Text="Odometer:"></asp:Label></td>
                <td style="text-align: left; width: 231px;">
                    <asp:TextBox ID="txtOdometer" runat="server" CssClass="SixCharTxtBox" MaxLength="6"
                        AutoCompleteType="Disabled" TabIndex="11"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 32px; width: 175px;">
                    <asp:Label ID="Label10" runat="server" Text="Sentry:"></asp:Label></td>
                <%--<td style=text-align:left">
                            <asp:Label ID="Label5" runat="server" Text="Sentry Name:" /></td>--%>
                <td style="text-align: left">
                    <asp:DropDownList ID="DDLstSentry" runat="server" AutoPostBack="True" meta:resourcekey="DDLstTankResource1"
                        CssClass="ThirtyCharTxtBox" TabIndex="5">
                    </asp:DropDownList>
                    <input type="hidden" id="txtHSentry" runat="server" style="width: 1px" />
                    <input type="hidden" id="txtSentryId" runat="server" style="width: 1px" />
                    <%-- <table>
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="txtSentry" runat="server" cssclass="FiveCharTxtBox" MaxLength="5"></asp:TextBox></td>
                                    <td>
                                        <input id="btnSentry" type="button" value="..." style="height: 22px; width: 30px"
                                            onclick="OpenPopUp('TransSentry','txtSentryId','txtSentryName');" hidefocus="hideFocus" /></td>
                                </tr>
                        </table>--%>
                </td>
                <%--<td style="height: 48px; text-align: left">
                            <asp:TextBox ID="txtSentryName" runat="server" CssClass="InputTextBox"></asp:TextBox></td>--%>
                <td class="StdLableTxtLeft" style="width: 117px">
                    <asp:Label ID="Label7" runat="server" Text="Hours:"></asp:Label></td>
                <td style="text-align: left; width: 231px;">
                    <asp:TextBox ID="txtHours" runat="server" CssClass="SixCharTxtBox" MaxLength="6"
                        AutoCompleteType="Disabled" TabIndex="12"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lbl1" runat="server" Text="Vehicle:"></asp:Label></td>
                <td style="text-align: left" class="style2">
                    <asp:TextBox ID="txtVehID" runat="server" CssClass="TenCharTxtBox" Enabled="true"
                        MaxLength="10" TabIndex="6" onblur="ShowVehicle(this.value);"></asp:TextBox>
                    <span id="spanAvailability"></span>
                </td>
                <td class="style3">
                    <asp:Label ID="lblHose" runat="server" Text="Hose:"></asp:Label></td>
                <td style="text-align: left; " class="style4">
                    <asp:DropDownList ID="DDLstHose" runat="server" AutoPostBack="True" CssClass="FiveCharTxtBox"
                        TabIndex="13">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 34px; width: 175px;">
                    <asp:Label ID="Label6" runat="server" Text="Description:" /></td>
                <td style="text-align: left; height: 34px;">
                    <asp:TextBox ID="txtDesc" runat="server" ReadOnly="True" BackColor="lightgray" CssClass="TwentyFiveCharTxtBox"></asp:TextBox></td>
                <td class="StdLableTxtLeft" style="width: 117px; height: 34px;">
                    <asp:Label ID="lblTank" runat="server" Text="Tank:"></asp:Label></td>
                <td style="text-align: left; width: 231px; height: 34px;">
                    <asp:TextBox ID="txtTank" runat="server" BackColor="lightgray" CssClass="TwentyCharTxtBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 32px; width: 175px;">
                    <asp:Label ID="lblDept" runat="server" Text="Department:"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDept" runat="server" MaxLength="7" BackColor="lightgray" CssClass="ThirtyCharTxtBox"></asp:TextBox>
                    <input type="hidden" runat="server" id="txtHidDept" style="width: 1px" /></td>
                <td class="StdLableTxtLeft" style="width: 117px">
                    <asp:Label ID="lblProd" runat="server" Text="Product:"></asp:Label></td>
                <td style="text-align: left; width: 231px;">
                    <asp:TextBox ID="txtProd" runat="server" ReadOnly="True" BackColor="lightgray" CssClass="TenCharTxtBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 34px; width: 175px;">
                    <asp:Label ID="lblPers" runat="server" Text="Personnel Name:"></asp:Label></td>
                <td style="text-align: left; height: 34px;">
                    <asp:TextBox ID="txtName" runat="server" CssClass="TwentyCharTxtBox" TabIndex="7"
                        EnableViewState="true"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="..." Width="30px" Height="22px" TabIndex="8" />
                    &nbsp; &nbsp;<input type="hidden" id="txtPersNum" runat="server" style="width: 1px" />
                    <input type="text" id="txtPKey" runat="server" style="width: 50px; visibility: hidden" /></td>
                <td class="StdLableTxtLeft" style="width: 117px; height: 34px;">
                    <asp:Label ID="lblQty" runat="server" Text="Quantity:"></asp:Label></td>
                <td style="text-align: left; width: 231px; height: 34px;">
                    <asp:TextBox ID="txtQty" runat="server" CssClass="NineCharTxtBox" MaxLength="9" AutoCompleteType="Disabled"
                        TabIndex="14"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFV_Qty" runat="server" ErrorMessage="Please enter Quantity"
                        ControlToValidate="txtQty">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 32px; width: 175px;">
                    <asp:Label ID="Label3" runat="server" Text="Optional:"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtoptional" runat="server" CssClass="FifteenCharTxtBox" MaxLength="13"
                        AutoCompleteType="Disabled" TabIndex="9"></asp:TextBox></td>
                <td class="StdLableTxtLeft" style="width: 117px">
                    <asp:Label ID="lblCost" runat="server" Text="Cost:"></asp:Label></td>
                <td style="text-align: left; width: 231px;">
                    <asp:TextBox ID="txtCost" runat="server" MaxLength="10" BackColor="lightgray" AutoCompleteType="Disabled"
                        CssClass="TenCharTxtBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 175px">
                </td>
                <td align="center" class="txtNewEdittd1">
                    <input type="hidden"  runat="server" id="txtHideTankNBR" style="width: 1px" />
                    <input type="hidden"  runat="server" id="txtHideTankPrice" style="width: 1px" /></td>
                <td style="width: 117px">
                </td>
                <td style="width: 231px">
                </td>
            </tr>
            <tr>
                <td class="Trans_ADD_SubTable_TD_Txt" colspan="4" style="text-align: center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center; height: 32px" colspan="4">
                    <asp:Button ID="btnOk" runat="server" Text="Add Another" Width="100px" TabIndex="15" /><asp:Button
                        ID="btnSave" runat="server" Text="Save" Width="100px" TabIndex="16" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                        Width="100px" CausesValidation="False" TabIndex="17" />
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                        CausesValidation="False" Visible="False" Width="100px" TabIndex="18" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 32px" colspan="4">
                    <asp:Label ID="lblRecNo" runat="server" Text="Label" Visible="False"></asp:Label>&nbsp;
                    <asp:Button ID="btnFirst" runat="server" Height="24px" Text="|<" Width="50px" CausesValidation="False" /><asp:Button
                        ID="btnprevious" runat="server" Height="24px" Text="<" Width="50px" CausesValidation="False" /><asp:Label
                            ID="lblof" runat="server" Font-Bold="false" Font-Names="Verdana" Font-Size="Small"
                            Text="Label" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="125px"></asp:Label><asp:Button
                                ID="btnNext" runat="server" Height="24px" Text=">" Width="50px" CausesValidation="False" /><asp:Button
                                    ID="btnLast" runat="server" Text=">|" Width="50px" CausesValidation="False" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="hidPersonnel" runat="server" />
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
