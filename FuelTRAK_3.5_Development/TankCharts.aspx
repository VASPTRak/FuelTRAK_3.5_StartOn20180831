<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankCharts.aspx.vb" Inherits="TankCharts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tank Charts</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
     <script src="Javascript/jquery-ui.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
   
     function chksesion()
	 {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	 }
	 
	  $(function () {      
        $('#txtChartName').keydown(function (e) {
       
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
     });
    });
    
    function check()
	 {
		    var ac = confirm('Are you sure you want to delete this entry ?');
			document.getElementById('Hidtxt').value = ac;
			document.form1.submit(); 
			
	 }
        
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" id="tblSelect" class="SixHundredPXTable">
                <tr>
                    <td class="MainHeader" colspan="3">
                        <asp:Label ID="Label1" runat="server" Text="Search / Edit Tank Charts "></asp:Label></td>
                </tr>
                <tr>
                    <td class="Meter_HeadButton">
                        <asp:Button ID="btnNew" runat="server" Visible="false" Text="New" Height="24px" Width="100px"
                            UseSubmitBehavior="False" />
                        <asp:Button runat="server" ID="btnSearch" Text="Search" Visible="false" Height="24px"
                            Width="100px" />
                            
                             <input type="hidden"  id="Hidtxt" runat="server" style="width: 10px" />
                            <input type="hidden" id="txtEntryID" runat="server" style="width: 10px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="5">
                        <table align="center" id="tblSearchsub" visible="true" runat="server" class="FiveHundredPXTable">
                            <tr>
                                <td class="HeaderSearchCriteria" colspan="3" style="height: 36px">
                                    <asp:Label ID="Label8" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                    <asp:Button ID="btnSearchSub" runat="server" Text="Search" Width="100px" TabIndex="3" /></td>
                            </tr>
                            <tr>
                                <td style="width: 150px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 100px; height: 32px">
                                    <asp:Label ID="lblChartNum" runat="server" BackColor="Transparent" Text="Chart Number:"></asp:Label></td>
                                <td style="text-align: left; width: 150px; height: 32px">
                                    <asp:TextBox ID="txtChartID" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                                        TabIndex="1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 150px; height: 32px">
                                </td>
                                <td class="StdLableTxtLeft">
                                    <asp:Label ID="lblChartName" runat="server" Text="Chart Name:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtChartName" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20"
                                        TabIndex="2"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" class="VehicleGrid">
                                    <table visible="false" id="tblSearch" class="FourHundredPXTable" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="GridTankCharts" runat="server" Width="400px" AllowPaging="True"
                                                    AllowSorting="true" AutoGenerateColumns="False" DataKeyNames="ChartNumber" EmptyDataText="0 records found for selected search criteria"
                                                    BorderColor="black" BorderStyle="solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                    PageSize="10">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                        </asp:CommandField>
                                                         <asp:CommandField ShowDeleteButton="True" HeaderText="Delete">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField SortExpression="ChartNumber" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="80px" HeaderText="Number" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ChartNumber")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="ChartName" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="350px" HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "ChartName")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle Font-Size="Small" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle"
                                                        BorderWidth="1px" Font-Names="arial" />
                                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#a5bbc5" ForeColor="Black" />
                                                    <FooterStyle BackColor="#a5bbc5" ForeColor="Black" />
                                                    <RowStyle BackColor="white" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                                    <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" />
                                                    <AlternatingRowStyle BackColor="#fdfeb6" />
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
