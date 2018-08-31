<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tank.aspx.vb" Inherits="Tank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tank Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
       function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div align="center" style="overflow: auto">
            <table class="EightHundredPXTable">
                <tr>
                    <td align="center">
                        <table class="SixHundredPXTable">
                            <tr>
                                <td class="MainHeader" colspan="2" style="height: 50px">
                                    <asp:Label ID="Label2" runat="server" Text="Search / Edit Tank"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" OnClick="btnNew_Click"
                                        Visible="false" UseSubmitBehavior="False" AccessKey="n" />
                                    <asp:Button runat="server" ID="btnSearch" Text="Search" Width="100px" AccessKey="s" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" runat="server" id="tblGrid" visible="false">
                                    <table id="tblContain1" runat="server" class="SixHundredPXTable">
                                        <tr>
                                            <td>
                                                <table id="tblSearch" runat="server" class="FourHundrePXTable">
                                                    <tr>
                                                        <td colspan="2" class="Per_tdsearch">
                                                            <asp:GridView runat="server" ID="GridTank" AllowSorting="true" BackColor="White"
                                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                                Width="600px" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Record"
                                                                EmptyDataText="0 records found for selected search criteria" PageSize="15">
                                                                <FooterStyle BackColor="#a5bbc5" ForeColor="Black" />
                                                                <RowStyle BackColor="white" ForeColor="Black" />
                                                                <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                                                <PagerStyle BackColor="#a5bbc5" ForeColor="Black" HorizontalAlign="Center" />
                                                                <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" />
                                                                <AlternatingRowStyle BackColor="#fdfeb6" />
                                                                <EmptyDataRowStyle BackColor="Silver" BorderColor="SteelBlue" BorderStyle="Solid"
                                                                    BorderWidth="1px" Font-Bold="False" ForeColor="Red" />
                                                                <Columns>
                                                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                                    </asp:CommandField>
                                                                    <asp:TemplateField SortExpression="Number" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" HeaderText="Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Number")%>'
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField SortExpression="Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="230px" HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
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
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
