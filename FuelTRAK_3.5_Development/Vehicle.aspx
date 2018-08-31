<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Vehicle.aspx.vb" Inherits="Vehicle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Vehicle Search</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/Vehicle.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>
    
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
     <script src="Javascript/jquery-ui.js" type="text/javascript"></script>
    
     <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
	     
	     function check(VehId)
		 {
		    
			var ac=confirm('Are you sure you want to permanently delete this record ?');
			document.form1.Hidtxt.value=ac;
			document.form1.txtVehId.value=VehId;
			form1.submit(); 
		 }
		 
		  $(function () {      
        $('#txtDescription').keydown(function (e) {
       
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
      }); });
	 $(function () {      
        $('#txtMake').keydown(function (e) {
       
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
     }); });
    
    $(function () {      
        $('#txtModel').keydown(function (e) {
       
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
        $('#txtVinNumber').keydown(function (e) {
       
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
        <div align="left">

            <script type="text/javascript">
         

            </script>

            <table align="center" class="MaximumPXTable">
                <tr>
                    <td>
                        <table style="text-align: center" class="EightHundredPXTable">
                            <tr>
                                <td colspan="2" class="MainHeader">
                                    <asp:Label ID="Label2" runat="server" Text="Search / Edit Vehicle"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="VehicleTD" align="right">
                                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="VehicleEditBtn" OnClick="btnNew_Click"
                                        UseSubmitBehavior="False" /></td>
                                <td class="VehicleTD" align="left">
                                    <input id="btnShowSearch" type="button" value="Search" onclick="HideControls(0)"
                                        class="VehicleEditBtn" unselectable="on" />
                                    <input type="hidden" id="Hidtxt" runat="server" class="VehicleHidden" />
                                    <input type="hidden" id="txtVehId" runat="server" class="VehicleHidden" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="tblContain1" class="NineHundredPXTable">
                                        <tr>
                                            <td colspan="4" class="HeaderSearchCriteria" style="height:50px">
                                                <asp:Label ID="Label1" runat="server" Text="Please Enter Your Search Criteria and Select Search:" Font-Underline="False" Width="389px"></asp:Label>
                                                <asp:Button ID="btnSearch" CssClass="VehicleEditBtn" runat="server" Text="Search"
                                                    OnClick="btnSearch_Click" TabIndex="11" /></td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <table align="center" id="tblSearch" class="SevenHundredFiftyPXTable">
                                                    <tr>
                                                        <td class="StdLableTxtLeft" style="width: 150px; height: 32px">
                                                            <asp:Label ID="lblVehicleID" Text="Vehicle ID:" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 200px">
                                                            <asp:TextBox ID="txtVehicleID" runat="server" 
                                                                 CssClass="TenCharTxtBox" MaxLength="10" TabIndex="1"></asp:TextBox></td>
                                                        <td class="StdLableTxtLeft" style="width: 150px">
                                                            <asp:Label ID="lblYear" Text="Year" runat="server"></asp:Label></td>
                                                        <td style="text-align: left; width: 250px">
                                                            <asp:TextBox ID="txtYear" runat="server" CssClass="FourCharTxtBox" MaxLength="4" TabIndex="6"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StdLableTxtLeft" style="height: 32px">
                                                            <asp:Label ID="lblVehicleMake" Text="Vehicle Make:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtMake" runat="server" CssClass="TwentyCharTxtBox" TabIndex="2" MaxLength="20"></asp:TextBox></td>
                                                        <td class="StdLableTxtLeft">
                                                            <asp:Label ID="lblDescription" Text="Description:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="TwentyFiveCharTxtBox" TabIndex="7"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StdLableTxtLeft"  style="height: 32px">
                                                            <asp:Label ID="lblVehicleModel" Text="Model:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtModel" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20" TabIndex="3"></asp:TextBox></td>
                                                        <td class="StdLableTxtLeft">
                                                            <asp:Label ID="lblDepartment" Text="Department:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3" TabIndex="8"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StdLableTxtLeft"  style="height: 32px">
                                                            <asp:Label ID="lblVINNumber" Text="VIN:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtVinNumber" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20" TabIndex="4"></asp:TextBox></td>
                                                        <td class="StdLableTxtLeft">
                                                            <asp:Label ID="lblKeyNumber" Text="Key Number:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtKeyNo" runat="server" CssClass="FiveCharTxtBox" MaxLength="5" TabIndex="9"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StdLableTxtLeft"  style="height: 32px">
                                                            <asp:Label ID="lblLicenseNo" Text="License Number:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtLicenseNo" runat="server" CssClass="NineCharTxtBox" MaxLength="9" TabIndex="5"></asp:TextBox></td>
                                                        <td class="StdLableTxtLeft">
                                                            <asp:Label ID="lblCardNO" Text="Card:" runat="server"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtCardNo" runat="server" CssClass="SevenCharTxtBox" MaxLength="7" TabIndex="10"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" id="tdsearch" class="VehicleResult" runat="server" visible="false">
                                                <asp:Label Text="Search Result" runat="server" ID="lblresult" Font-Names="arial"
                                                    Font-Bold="True" ForeColor="black" Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="VehicleGrid" align="center">
                                                <asp:GridView Width="900px" ID="GridView1" runat="server" BackColor="White" BorderColor="black"
                                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" 
                                                    DataKeyNames="VehicleId,EXTENSION,IDENTITY,DEPT,NAME,KEY_NUMBER"
                                                    AllowPaging="True" AutoGenerateColumns="False"  AllowSorting="true" 
                                                    EmptyDataText="0 records found for selected search criteria">
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField SortExpression="IDENTITY" ItemStyle-HorizontalAlign="Center" HeaderText="Vehicle ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIdentity" Text='<%# DataBinder.Eval(Container.DataItem, "IDENTITY")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="EXTENSION" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEXTENSION" Text='<%# DataBinder.Eval(Container.DataItem, "EXTENSION")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="DEPT" ItemStyle-HorizontalAlign="Center" HeaderText="DEPT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDEPT" Text='<%# DataBinder.Eval(Container.DataItem, "DEPT")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="LICNO" ItemStyle-HorizontalAlign="Left" HeaderText="LICNO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLICNO" Text='<%# DataBinder.Eval(Container.DataItem, "LICNO")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHMAKE" ItemStyle-HorizontalAlign="Left" HeaderText="VEH MAKE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHMAKE" Text='<%# DataBinder.Eval(Container.DataItem, "VEHMAKE")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHMODEL" ItemStyle-HorizontalAlign="Left" HeaderText="VEH MODEL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHMODEL" Text='<%# DataBinder.Eval(Container.DataItem, "VEHMODEL")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="KEY_NUMBER" ItemStyle-HorizontalAlign="Center"
                                                            HeaderText="KEY NUMBER">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKEY_NUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "KEY_NUMBER")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="CARD_ID" ItemStyle-HorizontalAlign="Center" HeaderText="CARD_ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCARD_ID" Text='<%# DataBinder.Eval(Container.DataItem, "CARD_ID")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHYEAR" ItemStyle-HorizontalAlign="Center" HeaderText="VEH YEAR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHYEAR" Text='<%# DataBinder.Eval(Container.DataItem, "VEHYEAR")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHVIN" ItemStyle-HorizontalAlign="Left" HeaderText="VEH VIN">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHVIN" Text='<%# DataBinder.Eval(Container.DataItem, "VEHVIN")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="NAME" ItemStyle-HorizontalAlign="Left" HeaderText="DEPT NAME"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNAME" Text='<%# DataBinder.Eval(Container.DataItem, "NAME")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                     </Columns>
                                                    <RowStyle BackColor="white" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                                    <PagerStyle BackColor="#A5BBC5" ForeColor="Black" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="black" />
                                                    <AlternatingRowStyle BackColor="#fdfeb6" />
                                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BackColor="white" BorderColor="red"
                                                        BorderStyle="Solid" BorderWidth="1px" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
