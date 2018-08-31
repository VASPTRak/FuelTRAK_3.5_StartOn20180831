<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankMonitor.aspx.vb" Inherits="TankMonitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tank Monitor Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="Javascript/TankMonitor.js"></script>
    
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
     <script src="Javascript/jquery-ui.js" type="text/javascript"></script>
    
     <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	
	 $(function () {      
        $('#txtTMName').keydown(function (e) {
       
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

 </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" id="tblSelect" class="EightHundredPXTable">
                <tr>
                    <td class="MainHeader" colspan="2" style="height: 50px">
                        <asp:Label ID="Label1" runat="server" Text="Search / Edit Tank Monitor "></asp:Label></td>
                </tr>
                <tr>
                    <td class="Meter_HeadButton">
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Width="100px"
                            UseSubmitBehavior="False" />
                        <asp:Button runat="server" ID="btnSearch" Text="Search" Height="24px" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table id="tblSearchsub" runat="server" class="FiveHundredPXTable">
                            <tr>
                                <td class="HeaderSearchCriteria" colspan="3" style="height: 50px">
                                    <asp:Label ID="Label8" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                    <asp:Button ID="btnSearchsub" runat="server" Text="Search" Width="100px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 120px"></td>
                                <td class="StdLableTxtLeft" style="height: 32px; width: 150px">
                                    <asp:Label ID="Label2" runat="server" BackColor="Transparent" Text="Tank Monitor ID:"></asp:Label></td>
                                <td style="text-align: left; width: 230px">
                                    <asp:TextBox ID="txtTMID" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="StdLableTxtLeft" style="height: 32px">
                                    <asp:Label ID="Label3" runat="server" Text="Tank Monitor Name:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtTMName" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="VehicleGrid" align="center">
                                    <table visible="false" id="tblSearch" class="ThreeHundredPXTable" runat="server">
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="GridTM" runat="server" Width="456px"  AllowSorting="true" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataKeyNames="Number" EmptyDataText="0 records found for selected search criteria"
                                                    Style="border-top-style: ridge; border-right-style: ridge; border-left-style: ridge;
                                                    border-bottom-style: ridge; border-left-color: black; border-bottom-color: black;
                                                    border-top-color: black; border-right-color: black;" CellPadding="3" GridLines="Vertical">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField SortExpression="Number" HeaderText="Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Number")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Name" HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="316px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle Font-Size="Small" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle"
                                                        BorderWidth="1px" Font-Names="arial" />
                                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#A5BBC5" ForeColor="Black" />
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <RowStyle BackColor="White" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#FDFEB6" Font-Bold="True" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="Black" />
                                                    <AlternatingRowStyle BackColor="#FDFEB6" />
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
