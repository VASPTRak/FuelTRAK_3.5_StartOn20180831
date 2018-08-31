




<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sentry.aspx.vb" Inherits="Sentry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sentry Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/Sentry.js"></script>
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
     <script src="Javascript/jquery-ui.js" type="text/javascript"></script>
    
     <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	
	 $(function () {      
        $('#txtSName').keydown(function (e) {
       
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
            <table id="tblSelect" class="SixHundredPXTable" align="center">
                <tr>
                    <td class="MainHeader" colspan="2" style="height: 50px">
                        <asp:Label ID="Label1" runat="server" Text="Search / Edit Sentry "></asp:Label></td>
                </tr>
                <tr>
                    <td class="Meter_HeadButton">
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Height="24px"
                            Width="100px" UseSubmitBehavior="False" AccessKey="a" />
                        <asp:Button runat="server" ID="btnSearch" Text="Search" Height="24px" Width="100px"
                            AccessKey="v" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <table id="tblSearchsub" runat="server" class="TankInv_SearchTable">
                            <tr>
                                <td class="HeaderSearchCriteria" colspan="3" style="text-align: center">
                                    <asp:Label ID="Label8" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                    <asp:Button ID="btnSearchsub" runat="server" Text="Search" Width="100px" AccessKey="s" tabindex="3" /></td>
                            </tr>
                            <tr>
                                <td style="width: 150px">
                                </td>
                                <td class="StdLableTxtLeft" style="width: 150px; height: 32px">
                                    <asp:Label ID="Label2" runat="server" BackColor="Transparent" Text="Sentry ID:"></asp:Label></td>
                                <td class="TankInv_Left" style="width: 300px; height: 32px">
                                    <asp:TextBox ID="txtSID" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3" tabindex="1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 150px; height: 32px"></td>
                                <td class="StdLableTxtLeft" style="width: 150px; height: 32px">
                                    <asp:Label ID="Label3" runat="server" Text="Sentry Name:"></asp:Label>
                                </td>
                                <td class="TankInv_Left" style="width: 300px; height: 32px">
                                    <asp:TextBox ID="txtSName" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20" tabindex="2"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" class="VehicleGrid">
                                    <table visible="false" id="tblSearch" class="FiveHundredPXTable" runat="server">
                                        <tr>
                                            <td colspan="3" style="text-align: center" >
                                            <asp:GridView ID="GridTM" runat="server" AllowSorting="true" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" EmptyDataText="0 records found for selected search criteria" style="border-top-style: ridge; border-right-style: ridge; 
                                                    border-left-style: ridge; border-bottom-style: ridge; border-left-color: black; border-bottom-color: black; 
                                                    border-top-color: black; border-right-color: black;"
                                                    PageSize="15" BorderColor="Silver" BorderStyle="None" CellPadding="3" GridLines="Vertical">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" Font-Names="Arial"
                                                            Font-Size="10pt" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" Font-Names="Arial"
                                                            Font-Size="10pt" />
                                                            </asp:CommandField>
                                                            <asp:TemplateField SortExpression="Number" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" HeaderText="Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Number")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Name" ItemStyle-HorizontalAlign="left" ItemStyle-Width="350px" HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                            </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="red" HorizontalAlign="Center" VerticalAlign="Middle"
                                                        Font-Size="Small" BackColor="336699" Font-Names="arial" />
                                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#A5BBC5" ForeColor="Black" />
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <RowStyle BackColor="white" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                                    <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="black" />
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
