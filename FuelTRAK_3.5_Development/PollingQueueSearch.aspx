<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PollingQueueSearch.aspx.vb"
    Inherits="PollingQueueSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Polling Queue Table</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Javascript/PollingQueue.js"></script>
    
     <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

 </script>
    
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" class="MaximumPXTable">
                <tr>
                    <td align="center" valign="middle">
                        <table align="center" id="tblSelect" class="EightHundredPXTable">
                            <tr>
                                <td class="MainHeader">
                                    <asp:Label ID="Label1" runat="server" Text="Search / Edit Polling Queue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TankInv_Header_Button">
                                    <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" />
                                    <input id="btnSearchshow" type="button" value="Search" onclick="HideControls(3)"   class="VehicleEditBtn" />
                                    <input type="hidden" id="Hidtxt" runat="server" style="width:10px" />
                                    <input type="hidden" id="txtVehId" runat="server" style="width:10px" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table id="tblSearch" class="TankInv_SearchTable">
                                        <tr>
                                            <td class="HeaderSearchCriteria" colspan="2" style="height:50px">
                                                <asp:Label ID="Label8" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                    Width="100px" TabIndex="2" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <table class="TankInv">
                                                    <tr>
                                                        <td class="StdLableTxtLeft">
                                                            <asp:Label ID="Label2" runat="server" Text="Device Type:"></asp:Label></td>
                                                        <td class="TankInv_Left">
                                                            <asp:DropDownList ID="DDLstType" runat="server" Font-Names="Verdana" ValidationGroup="50" TabIndex="1">
                                                                <asp:ListItem Value=" ">Select Type</asp:ListItem>
                                                                <asp:ListItem Value="S">Sentry</asp:ListItem>
                                                                <asp:ListItem Value="TM">Tank Monitor</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtType" runat="server" Visible="False" Width="1px"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Names="Verdana"
                                                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" colspan="2" class="TankGrid">
                                                <asp:GridView ID="GRVPollingQueue" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" EmptyDataText="0 records found for selected search criteria"
                                                    PageSize="15" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" BackColor="White"
                                                    CellPadding="3" GridLines="Vertical">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="DeviceType" HeaderText="Device Type">
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DeviceID" HeaderText="Device ID">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DeviceName" HeaderText="Device Name">
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Command" HeaderText="Command">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TimeQueued" DataFormatString="{0:HH:mm}" HeaderText="Time Queued"
                                                            HtmlEncode="False">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle" />
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
