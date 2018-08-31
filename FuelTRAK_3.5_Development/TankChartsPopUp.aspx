<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankChartsPopUp.aspx.vb" 
    Inherits="TankChartsPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	function Run_Report(url)    { window.open(url);        }

    </script>

    <title>Chart Detail</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center">
                <tr>
                    <td colspan="3" style="border-right: black 1px solid; border-top: black 1px solid;
                        border-left: black 1px solid; border-bottom: black 1px solid; height: 43px">
                        <asp:Label ID="LabelChartDetail" runat="server" Text="Chart Detail" Width="372px"
                            Font-Bold="True" Font-Size="16pt"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 270px; height: 600px; border: solid 1px black; text-align: left;">
                        <span style="font-size: 23px"><strong>The Tank Chart consists of a number of inch/gallon
                            pairs.<br />
                            <br />
                            The Tank Chart creation process has created the inches.<br />
                            <br />
                            Enter the gallon levels.<br />
                            <br />
                            Then Press Verify to ensure the tank chart is valid.</strong></span></td>
                    <td colspan="2" style="width: 300px; height: 600px; border: solid 1px black; text-align: left">
                        <div id="grdCharges" runat="server" style="width: 300px; height: 600px; overflow: auto;">
                            <asp:GridView ID="GridTankCharts" runat="server" Width="270px" AutoGenerateColumns="False"
                                DataKeyNames="ChartNumber" EmptyDataText="0 records found for selected search criteria"
                                BorderColor="#999999" BorderStyle="None" CellPadding="3" GridLines="Vertical"
                                PageSize="15">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" HeaderText="Edit" EditText="Edit" UpdateText="Update"
                                        CancelText="Cancel">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                            Font-Size="10pt" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                            Font-Size="10pt" />
                                    </asp:CommandField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" HeaderText="Inches">
                                        <ItemTemplate>
                                            <asp:Label ID="lblILevel" Text='<%# DataBinder.Eval(Container.DataItem, "ILevel")%>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px" HeaderText="Gallons">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGLevel" Text='<%# DataBinder.Eval(Container.DataItem, "GLevel")%>'
                                                runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" Width="45px" ID="txtGLEVELS" Text='<%# DataBinder.Eval(Container.DataItem, "GLevel")%>'>
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfdGLEVELS" ControlToValidate="txtGLEVELS"
                                                ErrorMessage="*Required" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle Font-Size="Small" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle"
                                    BorderWidth="1px" Font-Names="Verdana" />
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#a5bbc5" ForeColor="Black" />
                                <FooterStyle BackColor="#a5bbc5" ForeColor="Black" />
                                <RowStyle BackColor="white" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" />
                                <AlternatingRowStyle BackColor="#fdfeb6" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                        border-bottom: black 1px solid; height: 63px; text-align: center" colspan="3">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" Height="30" />
                        &nbsp
                        <asp:Button ID="btnClose" runat="server" Text="Close" Width="100px" Height="30" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
