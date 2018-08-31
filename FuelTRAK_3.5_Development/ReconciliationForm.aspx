<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReconciliationForm.aspx.vb"
    Inherits="ReconciliationForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reconciliation</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	   function Run_Report(url)    { window.open(url);        }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" width="800px">
                <tr>
                    <td class="MainHeader" colspan="4" style="height: 42px">
                        <asp:Label ID="lblNew_Edit" runat="server" CssClass="MainHeader" Text="Inventory Reconciliation Form"></asp:Label>
                    </td>
                    <td class="MainHeader" colspan="4" style="height: 42px">
                        <asp:Button ID="btnRunReport" runat="server" Text="Run Report" CssClass="MainHeader" Height="31px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px">
                        <asp:Label ID="lblTankLOC" runat="server" Text="Label" Width="144px" CssClass="ReconFormLableLeft">TANK LOCATION</asp:Label></td>
                    <td style="width: 301px">
                        <asp:TextBox ID="txtTankLoc" runat="server" CssClass="ReconFormTxtBoxTop"></asp:TextBox></td>
                    <td style="text-align: right; width: 140px;">
                        <asp:Label ID="lblDate" runat="server" Text="Label" Font-Size="x-small" Width="25px">DATE</asp:Label>
                        <asp:Label ID="lblYear" runat="server" Text="Label" Font-Size="x-small" Width="50px">YEAR:</asp:Label></td>
                    <td style="width: 301px">
                        <asp:TextBox ID="txtDateYear" runat="server"
                         CssClass="ReconFormTxtBoxTop"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 250px">
                        <asp:Label ID="lblTankNO" runat="server" Text="Label" Width="144px" CssClass="ReconFormLableLeft">TANK NUMBER</asp:Label></td>
                    <td style="width: 301px">
                        <asp:TextBox ID="txtTankNO" runat="server" CssClass="ReconFormTxtBoxTop"></asp:TextBox></td>
                    <td style="text-align: right; width: 140px;">
                        <asp:Label ID="lblMonth" runat="server" Text="Label" Font-Size="x-small" Width="50px">MONTH:</asp:Label></td>
                    <td style="width: 301px">
                            <asp:GridView ID="gvReconciliation" runat="server" Width="800px" AllowPaging="True"
                                AutoGenerateColumns="False" PageSize="31" BorderColor="black" AllowSorting="true"
                                BorderStyle="solid" BorderWidth="1px" BackColor="White" CellPadding="3" 
                                GridLines="Vertical">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" HeaderText="EDIT">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px" />
                                    </asp:CommandField>
                                    <asp:TemplateField SortExpression="day" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="123px"
                                        HeaderText="DAY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDT" Text='<%# DataBinder.Eval(Container.DataItem, "day")%>' 
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="PUMP_TOTALIZERS" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="250px" HeaderText="AMOUNT DISPENSED(Totalizer)" HeaderStyle-Wrap="true"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGVPumpTot" Text='<%# DataBinder.Eval(Container.DataItem, "DSP1")%>'
                                                runat="server">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TANK_DELIVERIES" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="250px" HeaderText="INPUT Deliveries" HeaderStyle-Wrap="true"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTankDeliveries" Text='<%# DataBinder.Eval(Container.DataItem, "TANK_DELIVERIES")%>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TANK_LEVELS" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="250px" HeaderText="TANK VOLUME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTankLevels" Text='<%# DataBinder.Eval(Container.DataItem, "TANK_LEVELS")%>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" />
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#a5bbc5" 
                                    ForeColor="Black" />
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle BackColor="white" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" 
                                    Font-Size="smaller" />
                                <AlternatingRowStyle BackColor="#fdfeb6" />
                            </asp:GridView>
                        <asp:TextBox ID="txtMonth" runat="server" CssClass="ReconFormTxtBoxTop"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 250px">
                        <asp:Label ID="lblTCEQOwnerID" runat="server" Text="Label" Width="144px" CssClass="ReconFormLableLeft">TCEQ OWNER ID:</asp:Label></td>
                    <td style="width: 301px">
                        <asp:TextBox ID="txtTCEQOwnerID" runat="server" CssClass="ReconFormTxtBoxTop"></asp:TextBox></td>
                    <td style="width: 140px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px">
                        <asp:Label ID="lblTCEQFacilityID" runat="server" Text="Label" Width="144px" CssClass="ReconFormLableLeft">TCEQ FACILITY ID:</asp:Label></td>
                    <td style="width: 301px">
                        <asp:TextBox ID="txtTCEQFacilityID" runat="server" CssClass="ReconFormTxtBoxTop"></asp:TextBox></td>
                    <td style="width: 140px">
                    </td>
                    <td style="width: 301px">
                    </td>
                </tr>
            </table>
            <table align="center" width="800px">
                <tr style="height: 15px">
                    <td style="width: 85px">
                    </td>
                    <td style="width: 123px">
                        <asp:Label ID="lblDay" runat="server" Text="Label" CssClass="ReconFormLableCenter">DAY</asp:Label></td>
                    <td style="width: 250px">
                        <asp:Label ID="lblColA" runat="server" Text="Label" CssClass="ReconFormLableCenter">COLUMN A</asp:Label></td>
                    <td style="width: 250px">
                        <asp:Label ID="lblColB" runat="server" Text="Label" CssClass="ReconFormLableCenter">COLUMN B</asp:Label></td>
                    <td style="width: 250px">
                        <asp:Label ID="lblColC" runat="server" Text="Label" CssClass="ReconFormLableCenter">COLUMN C</asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 85px">
                    </td>
                    <td style="width: 123px">
                    </td>
                    <td style="width: 250px">
                        <asp:Label ID="lblAmtDisp" runat="server" Text="Label" Font-Size="x-small" Width="150px">AMOUNT DISPENSED</asp:Label></td>
                    <td style="width: 250px">
                        <asp:Label ID="lblInput" runat="server" Text="Label" CssClass="ReconFormLableCenter">INPUT</asp:Label></td>
                    <td style="width: 250px">
                        <asp:Label ID="lblTankVol" runat="server" Text="Label" CssClass="ReconFormLableCenter">TANK VOLUME</asp:Label></td>
                </tr>
                <tr style="height: 15px">
                    <td style="width: 85px">
                    </td>
                    <td style="width: 123px">
                    </td>
                    <td style="width: 250px">
                    </td>
                    <td style="width: 250px">
                        <asp:Label ID="lblColGallonsb" runat="server" Text="Label" CssClass="ReconFormLableCenter">(gallons)</asp:Label></td>
                    <td style="width: 250px">
                        <asp:Label ID="lblColGallonsc" runat="server" Text="Label" CssClass="ReconFormLableCenter">(gallons)</asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 75px; height: 22px;">
                    </td>
                    <td style="width: 123px; height: 22px;">
                    </td>
                    <td style="width: 250px; height: 22px;">
                        <asp:TextBox ID="txtPrevMntAmtCnt" runat="server" Height="10px" Font-Size="x-small"></asp:TextBox></td>
                    <td style="width: 250px; height: 22px;">
                    </td>
                    <td style="width: 250px; height: 22px;">
                        <asp:TextBox ID="txtPrevMnthLevels" runat="server" Height="10px" Font-Size="x-small"></asp:TextBox>
                        <asp:Label ID="Label12" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">A</asp:Label></td>
                </tr>
            </table>
            <table align="center" width="800px">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto"  Height="300" Width="100%"
                            Font-Size="Small">
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table align="center" width="800px">
                <tr>
                    <td style="width: 75px; height: 15px">
                    </td>
                    <td style="width: 123px">
                    </td>
                    <td style="width: 250px">
                        <asp:TextBox ID="txtCntColA" runat="server" Height="10px" Font-Size="x-small"></asp:TextBox>
                        <asp:Label ID="Label19" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">1</asp:Label></td>
                    <td style="width: 250px">
                        <asp:TextBox ID="txtCntColB" runat="server" Height="10px" Font-Size="x-small"></asp:TextBox>
                        <asp:Label ID="Label20" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">2</asp:Label></td>
                    <td style="width: 250px">
                        <asp:TextBox ID="txtCntColC" runat="server" Height="10px" Font-Size="x-small"></asp:TextBox>
                        <asp:Label ID="Label21" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">B</asp:Label></td>
                </tr>
            </table>
            <table align="center" width="800px">
                <tr>
                    <td style="width: 75px" align="left">
                        <asp:Label ID="lblLine1" runat="server" Text="Label" CssClass="ReconFormLableLeft">LINE 1</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine1C1" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="lblLine2" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">A</asp:Label></td>
                    <td>
                        &nbsp;<asp:Label ID="Label23" runat="server" Text="Label">-</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine1C2" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label24" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">1</asp:Label></td>
                    <td>
                        <asp:Label ID="Label25" runat="server" Text="Label">+</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine1C3" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label26" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">2</asp:Label></td>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="Label">=</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine1C4" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label27" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">C</asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 75px" align="left">
                        <asp:Label ID="Label29" runat="server" Text="Label" CssClass="ReconFormLableLeft">LINE 2</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine2C1" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label30" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">1</asp:Label></td>
                    <td>
                        &nbsp;<asp:Label ID="Label31" runat="server" Text="Label">x</asp:Label></td>
                    <td>
                        &nbsp;<asp:Label ID="Label32" runat="server" Text="Label" CssClass="ReconFormLableLeft">0.01 + 130</asp:Label></td>
                    <td>
                        <asp:Label ID="Label33" runat="server" Text="Label">=</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine2C3" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label34" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">D</asp:Label></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px" align="left">
                        <asp:Label ID="Label35" runat="server" Text="Label" CssClass="ReconFormLableLeft">LINE 3</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine3Col1" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label36" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">B</asp:Label></td>
                    <td>
                        &nbsp;<asp:Label ID="Label37" runat="server" Text="Label">+</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine3Col2" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label38" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">D</asp:Label></td>
                    <td>
                        <asp:Label ID="Label39" runat="server" Text="Label">=</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine3Col3" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label40" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">E</asp:Label></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px" align="left">
                        <asp:Label ID="Label41" runat="server" Text="Label" CssClass="ReconFormLableLeft">LINE 4</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine4Col1" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label42" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">B</asp:Label></td>
                    <td>
                        &nbsp;<asp:Label ID="Label43" runat="server" Text="Label">-</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine4Col2" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label44" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">D</asp:Label></td>
                    <td>
                        <asp:Label ID="Label45" runat="server" Text="Label">=</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine4Col3" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label46" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">F</asp:Label></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px" align="left">
                        <asp:Label ID="Label47" runat="server" Text="Label" CssClass="ReconFormLableLeft">LINE 5 IS</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine5Col1" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label48" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">C</asp:Label></td>
                    <td>
                        &nbsp;<asp:Label ID="Label49" runat="server" Text="Label"><</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLine5Col2" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label50" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">E</asp:Label></td>
                    <td>
                        <asp:Label ID="Label51" runat="server" Text="Label">?</asp:Label></td>
                    <td style="text-align: left">
                        &nbsp;<asp:Label ID="Label52" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">YES or NO</asp:Label>
                        <asp:TextBox ID="txtLine5Col3" runat="server" Width="57px" Height="10px" Font-Size="x-small"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px; height: 22px;" align="left">
                        <asp:Label ID="Label53" runat="server" Text="Label" CssClass="ReconFormLableLeft">LINE 6 IS</asp:Label></td>
                    <td style="height: 22px">
                        <asp:TextBox ID="txtLine6Col1" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        <asp:Label ID="Label54" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">C</asp:Label></td>
                    <td style="height: 22px">
                        &nbsp;<asp:Label ID="Label55" runat="server" Text="Label">></asp:Label></td>
                    <td style="height: 22px">
                        <asp:TextBox ID="txtLine6Col2" runat="server" CssClass="ReconFormTxtBoxRight"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label56" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">F</asp:Label></td>
                    <td style="height: 22px">
                        <asp:Label ID="Label57" runat="server" Text="Label">?</asp:Label></td>
                    <td style="text-align: left; height: 22px;">
                        &nbsp;<asp:Label ID="Label58" runat="server" Text="Label" CssClass="ReconFormTxtBoxLabel">YES or NO</asp:Label>
                        <asp:TextBox ID="txtLine6Col3" runat="server" Width="57px" Height="10px" Font-Size="x-small"></asp:TextBox></td>
                    <td style="height: 22px">
                    </td>
                    <td style="height: 22px">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
