<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Transaction.aspx.vb" Inherits="Transactions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transaction</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/DateTime.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/transaction.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
     <script src="Javascript/jquery-ui.js" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript">

        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }

        function check(VehId)
        {
            var ac=confirm('Are you sure you want to permanently delete this record ?');
            document.form1.Hidtxt.value=ac;//
            document.form1.txtVehId.value=VehId;
            form1.submit(); 
        }
        function datecom()
        {   var firstDate = new Date(document.getElementById('DateTextBox1').value);
            var secondDate = new Date(document.getElementById('DateTextBox2').value);
            var firstYear = firstDate.getFullYear();
            var secondYear = secondDate.getFullYear();
            var firstMonth = firstDate.getMonth();
            var secondMonth = secondDate.getMonth();
            var firstDay = firstDate.getDate();
            var secondDay = secondDate.getDate();
            
            alert(firstYear + " " + secondYear);
            alert(firstMonth + " " + secondMonth);
            alert(firstDay + " " + secondDay);
            return false;
        }
        
        $(function () {      
        $('#txtVechModel').keydown(function (e) {
       
        if (e.shiftKey || e.ctrlKey || e.altKey) {
        e.preventDefault();
        } 
        else
        {        
          var key = e.keyCode;          
          if (( (key == 186) || (key == 222) )) {
            e.preventDefault();
        }
      }
     });});
     
     $(function () {      
        $('#txtVechMake').keydown(function (e) {
       
        if (e.shiftKey || e.ctrlKey || e.altKey) {
        e.preventDefault();
        } 
        else
        {        
          var key = e.keyCode;          
          if (( (key == 186) || (key == 222) )) {
            e.preventDefault();
        }
      }
     });});
     
     $(function () {      
        $('#txtVINNo').keydown(function (e) {
       
        if (e.shiftKey || e.ctrlKey || e.altKey) {
        e.preventDefault();
        } 
        else
        {        
          var key = e.keyCode;          
          if (( (key == 186) || (key == 222) )) {
            e.preventDefault();
        }
      }
     });});
     
     $(function () {      
        $('#txtDesc').keydown(function (e) {
       
        if (e.shiftKey || e.ctrlKey || e.altKey) {
        e.preventDefault();
        } 
        else
        {        
          var key = e.keyCode;          
          if (( (key == 186) || (key == 222) )) {
            e.preventDefault();
        }
      }
     });});
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div align="center">
            <table class="MaximumPXTable">
                <tr align="center">
                    <td colspan="2" class="MainHeader">
                        <asp:Label ID="Label2" runat="server" Text="Search / Edit Transactions"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" UseSubmitBehavior="False" />
                        <input id="btnShowSearch" onclick="return btnShowSearch_onclick()" onclick="ShowSearch();"
                            style="width: 100px; height: 26px;" type="button" value="Search" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="tblSearch" class="MaximumPXTable" language="javascript" onclick="return tblSearch_onclick()">
                            <tr>
                                <td colspan="9" class="HeaderSearchCriteria" style="height: 50px">
                                    <asp:Label ID="Label1" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Trans_BtnSearch" /><input
                                        type="hidden" id="Hidtxt" runat="server" style="width: 1px" tabindex="22" /><input
                                            type="hidden" id="txtVehId" runat="server" style="width: 1px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                </td>
                                <td class="StdLableTxtLeft" style="text-align: center; width: 85px">
                                    <asp:Label ID="Label30" runat="server" Text="FROM"></asp:Label></td>
                                <td style="width: 10px">
                                </td>
                                <td class="StdLableTxtLeft" style="text-align: center; width: 85px">
                                    <asp:Label ID="Label29" runat="server" Text="TO"></asp:Label></td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 125px">
                                </td>
                                <td style="width: 152px">
                                </td>
                                <td style="width: 110px">
                                </td>
                                <td style="width: 135px">
                                </td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 30px">
                                    <asp:Label ID="Label3" runat="server" Text="Date:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="DateTextBox1" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                        CausesValidation="True" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DateTextBox1"
                                        ErrorMessage="From date is required." Font-Names="Verdana" Font-Size="Small"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DateTextBox1"
                                        ErrorMessage="Invalid from date; valid date format (MM/dd/yyyy)." Font-Names="Verdana"
                                        Font-Size="Small" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                                        Display="Dynamic">*</asp:RegularExpressionValidator>
                                </td>
                                <td class="Trans_tblSearch_TDCal" style="text-align: left">
                                    <img id="StartDateImage" alt="Start Date" onclick="Javascript:cal1.popup()" runat="server"
                                        src="images/cal.gif" style="cursor: hand" tabindex="2" />
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="DateTextBox2" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                        CausesValidation="True" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DateTextBox2"
                                        ErrorMessage="To date is required." Font-Names="Verdana" Font-Size="Small" Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="DateTextBox2"
                                        ErrorMessage="Invalid to date; valid date format (MM/dd/yyyy)." Font-Names="Verdana"
                                        Font-Size="Small" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                                        Display="Dynamic">*</asp:RegularExpressionValidator></td>
                                <td class="Trans_tblSearch_TDCal" style="text-align: left">
                                    <img id="EndDateImage" alt="EndDate" onclick="Javascript:cal2.popup()" runat="server"
                                        src="images/cal.gif" style="cursor: hand" tabindex="4" />
                                </td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label8" runat="server" Text="Veh. Model:" CssClass="StdLableTxtLeft"></asp:Label>
                                    <%-- <input id="btnVechModel" type="button" value="Vehicle Model" class="Trans_tblSearch_Search_Btn"
                                        unselectable="on" />--%>
                                </td>
                                <td style="text-align: left; width: 152px;">
                                    <asp:TextBox ID="txtVechModel" runat="server" CssClass="FifteenCharTxtBox" MaxLength="20"
                                        TabIndex="13"></asp:TextBox></td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label7" runat="server" Text="Veh. Make:" CssClass="StdLableTxtLeft"></asp:Label>
                                    <%--<input id="btnVechMake" type="button" value="Vehicle Make" class="Trans_tblSearch_Search_Btn" />--%>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtVechMake" runat="server" CssClass="FifteenCharTxtBox" MaxLength="20"
                                        TabIndex="18"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 30px">
                                    <asp:Label ID="Label4" runat="server" Text="Veh. ID #:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="VehicleTextBox1" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                        TabIndex="5"></asp:TextBox></td>
                                <td>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="VehicleTextBox2" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                        TabIndex="6"></asp:TextBox></td>
                                <td>
                                </td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label9" runat="server" Text="Veh. Key #:" CssClass="StdLableTxtLeft"></asp:Label>
                                    <%--<input id="btnVechKey" type="button" value="Vehicle Key Number" class="Trans_tblSearch_Search_Btn" />--%>
                                </td>
                                <td style="text-align: left; width: 152px;">
                                    <asp:TextBox ID="txtVechKeyNo" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                                        TabIndex="14"></asp:TextBox></td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label10" runat="server" Text="Veh. Card #:" CssClass="StdLableTxtLeft"></asp:Label>
                                    <%--<input id="btnVechCrdNo" type="button" value="Vehicle Card Number" class="Trans_tblSearch_Search_Btn"
                                        unselectable="on" />--%>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtVechCrdNo" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"
                                        TabIndex="19"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 30px">
                                    <asp:Label ID="Personnel" runat="server" CssClass="StdLableTxtLeft" Text="Sentry #:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="SentryTextBox1" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                                        TabIndex="7"></asp:TextBox></td>
                                <td>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="SentryTextBox2" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                                        TabIndex="8"></asp:TextBox></td>
                                <td>
                                </td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label13" runat="server" Text="VIN #:" CssClass="StdLableTxtLeft"></asp:Label>
                                    <%--<input id="btnVINNo" type="button" value="VIN Number" class="Trans_tblSearch_Search_Btn" />--%>
                                </td>
                                <td style="text-align: left; width: 152px;">
                                    <asp:TextBox ID="txtVINNo" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20"
                                        TabIndex="15"></asp:TextBox></td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label5" runat="server" Text="License #.:" CssClass="StdLableTxtLeft"></asp:Label>
                                    <%--<input id="btnLicNo" type="button" value="License Number" class="Trans_tblSearch_Search_Btn"  unselectable="on" />--%>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtLicNo" runat="server" CssClass="NineCharTxtBox" MaxLength="9"
                                        TabIndex="20"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 30px">
                                    <asp:Label ID="Label6" runat="server" CssClass="StdLableTxtLeft" Text="Key #:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="KeyNumberTextBox1" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                                        TabIndex="9"></asp:TextBox></td>
                                <td>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="KeyNumberTextBox2" runat="server" CssClass="FiveCharTxtBox" MaxLength="5"
                                        TabIndex="10"></asp:TextBox></td>
                                <td>
                                </td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label12" runat="server" Text="Veh. Desc.:" CssClass="StdLableTxtLeft"></asp:Label></td>
                                <td style="text-align: left; width: 152px;">
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="TwentyCharTxtBox" MaxLength="50"
                                        TabIndex="16"></asp:TextBox></td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label11" runat="server" Text="Dept. #:" CssClass="StdLableTxtLeft"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtxDept" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                                        TabIndex="21"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft" style="height: 30px">
                                    <asp:Label ID="Label15" runat="server" CssClass="StdLableTxtLeft" Text="Personnel #:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPerFrom" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                        TabIndex="11"></asp:TextBox></td>
                                <td>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPerTo" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                        TabIndex="12"></asp:TextBox></td>
                                <td>
                                </td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="Label14" runat="server" Text="Veh. Year:" CssClass="StdLableTxtLeft"></asp:Label></td>
                                <td style="text-align: left; width: 152px;">
                                    <asp:TextBox ID="txtYear" runat="server" CssClass="FourCharTxtBox" MaxLength="4"
                                        TabIndex="17"></asp:TextBox></td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                <td align="center" colspan="9">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Names="Verdana"
                                        Font-Size="Small" ShowMessageBox="True" ShowSummary="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9" style="width: 900px; font-family: Arial; font-size: 9pt;" align="center">
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        BackColor="white" BorderColor="black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                        DataKeyNames="Record" EmptyDataText="0 records found for selected search criteria"
                                        GridLines="Vertical" Width="850px" PageSize="15" AllowSorting="true">
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <Columns>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:CommandField>
                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:CommandField>
                                            <asp:TemplateField SortExpression="Sentry" ItemStyle-HorizontalAlign="Center" HeaderText="Sentry">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSentry" Text='<%# DataBinder.Eval(Container.DataItem, "Sentry")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Datetime" ItemStyle-HorizontalAlign="Center" HeaderText="Date Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatetime" Text='<%# DataBinder.Eval(Container.DataItem, "Datetime")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Vehicle" ItemStyle-HorizontalAlign="Center" HeaderText="Vehicle">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicle" Text='<%# DataBinder.Eval(Container.DataItem, "Vehicle")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Personnel" ItemStyle-HorizontalAlign="Center"
                                                HeaderText="Personnel">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPersonnel" Text='<%# DataBinder.Eval(Container.DataItem, "Personnel")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Pump" ItemStyle-HorizontalAlign="Center" HeaderText="Pump">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPump" Text='<%# DataBinder.Eval(Container.DataItem, "Pump")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Product" ItemStyle-HorizontalAlign="Center" HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPump" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Quantity" ItemStyle-HorizontalAlign="Center" HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Miles" ItemStyle-HorizontalAlign="Center" HeaderText="Mileage">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMiles" Text='<%# DataBinder.Eval(Container.DataItem, "Miles")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="LICNO" ItemStyle-HorizontalAlign="Center" HeaderText="Lic No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLICNO" Text='<%# DataBinder.Eval(Container.DataItem, "LICNO")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="VEHMAKE" ItemStyle-HorizontalAlign="Center" HeaderText="Veh Make">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVEHMAKE" Text='<%# DataBinder.Eval(Container.DataItem, "VEHMAKE")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="VEHMODEL" ItemStyle-HorizontalAlign="Center" HeaderText="Veh Model">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVEHMODEL" Text='<%# DataBinder.Eval(Container.DataItem, "VEHMODEL")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="EXTENSION" ItemStyle-HorizontalAlign="Center"
                                                HeaderText="Desc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEXTENSION" Text='<%# DataBinder.Eval(Container.DataItem, "EXTENSION")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="CARD_ID" ItemStyle-HorizontalAlign="Center" HeaderText="Card No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCARDID" Text='<%# DataBinder.Eval(Container.DataItem, "CARD_ID")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="VEHVIN" ItemStyle-HorizontalAlign="Center" HeaderText="Veh. Vin. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVEHVIN" Text='<%# DataBinder.Eval(Container.DataItem, "VEHVIN")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="DEPT" ItemStyle-HorizontalAlign="Center" HeaderText="Dept No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDEPT" Text='<%# DataBinder.Eval(Container.DataItem, "DEPT")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="white" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#A5BBC5" ForeColor="Black" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="black" />
                                        <AlternatingRowStyle BackColor="#fdfeb6" />
                                        <EmptyDataRowStyle BackColor="white" BorderColor="red" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="True" ForeColor="Red" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript" language="javascript">				
        
         if (document.getElementById('DateTextBox1'))
        {
	        var cal1 = new calendar2(document.getElementById('DateTextBox1'));
	        cal1.year_scroll = true;
	        cal1.time_comp = false;
	    
	        var cal2 = new calendar2(document.getElementById('DateTextBox2'));
	        cal2.year_scroll = true;
	        cal2.time_comp = false;
	    }
        function tblSearch_onclick() {

        }

function btnShowSearch_onclick() {

}

    </script>

</body>
</html>
