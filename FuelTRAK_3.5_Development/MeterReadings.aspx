<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MeterReadings.aspx.vb" Inherits="MeterReadings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Meter Readings</title>

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Javascript/MeterReadings.js"></script>

    <script type="text/javascript" src="Javascript/Validation.js"></script>

    <script type="text/javascript" src="Javascript/DateTime.js"></script>

    <script language="javascript" type="text/javascript">
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
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table id="tblSelect" class="MaximumPXTable">
                <tr>
                    <td align="center">
                        <table class="EightHundredPXTable">
                            <tr>
                                <td class="MainHeader">
                                    <asp:Label ID="Label1" runat="server" Width="800px" Text=" Search / Edit Meter Readings"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="Meter_HeadButton">
                                    <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Height="24px"
                                        Width="100px" UseSubmitBehavior="False" />
                                    <input id="btnSearchshow" type="button" value="Search" onclick="HideControls(3)"
                                        style="width: 100px; height: 24px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblSearch" class="EightHundredPXTable">
                            <tr>
                                <td>
                                    <table align="center" class="EightHundredPXTable">
                                        <tr>
                                            <td class="HeaderSearchCriteria" colspan="4" style="height: 35px">
                                                <asp:Label ID="Label8" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                    Width="100px" TabIndex="4" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 310px; height: 35px">
                                            </td>
                                            <td class="StdLableTxtLeft" style="height: 32px; width: 90px">
                                                <asp:Label ID="Label2" runat="server" Text="Sentry:"></asp:Label>
                                            </td>
                                            <td style="text-align: left; height: 35px; width: 100px">
                                                <asp:TextBox ID="txtSentry" runat="server" CssClass="ThreeCharTxtBox" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td style="width: 310px; height: 35px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="StdLableTxtLeft" style="height: 32px">
                                                <asp:Label ID="Label3" runat="server" Text="Date:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtDate" runat="server" CausesValidation="True" ValidationGroup="date"
                                                    CssClass="EightCharTxtBox" MaxLength="10" TabIndex="2"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate"
                                                    Display="Dynamic" ErrorMessage="Invalid from date; valid date format (MM/dd/yyyy)."
                                                    Font-Names="Verdana" Font-Size="Small" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                                <img id="StartDateImage" alt="Start Date" onclick="Javascript:cal1.popup()" runat="server"
                                                    src="images/cal.gif" style="cursor: hand" tabindex="3" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 36px">
                                                <input type="hidden" id="Hidtxt" runat="server" style="width: 1px" />
                                                <input type="hidden" id="txtVehId" runat="server" style="width: 1px" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Names="Verdana"
                                                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td colspan="4" class="Meter_Grid">
                                                <asp:GridView ID="GridView1" runat="server" Width="800px" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataKeyNames="RECORD" EmptyDataText="0 records found for selected search criteria"
                                                    PageSize="15" BorderColor="black" AllowSorting="true" BorderStyle="solid" BorderWidth="1px"
                                                    BackColor="White" CellPadding="3" GridLines="Vertical">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                                                Font-Size="10pt" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField SortExpression="SENTRY" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="SENTRY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSENTRY" Text='<%# DataBinder.Eval(Container.DataItem, "SENTRY")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Pump" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="Pump">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPump" Text='<%# DataBinder.Eval(Container.DataItem, "Pump")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="METERREAD" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="80px" HeaderText="METERREAD">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMETERREAD" Text='<%# DataBinder.Eval(Container.DataItem, "METERREAD")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField SortExpression="DATETIME" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTIME" Text='<%# DataBinder.Eval(Container.DataItem, "DATETIME","{0:MM/dd/yyyy}")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField SortExpression="DATETIME" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="TIME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTIMEs" Text='<%# DataBinder.Eval(Container.DataItem, "DATETIME","{0:T}")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#a5bbc5" ForeColor="Black" />
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
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
    //if (document.getElementById('StartDateTextBox').style.visibility = "visible")
    
     if (document.getElementById('txtDate'))
    {
       
	    var cal1 = new calendar2(document.getElementById('txtDate'));
	    cal1.year_scroll = true;
	    cal1.time_comp = false;
	}
				
    </script>

</body>
</html>
