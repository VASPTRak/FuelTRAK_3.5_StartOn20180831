<%@ Page Language="VB" AutoEventWireup="false" CodeFile="User.aspx.vb" Inherits="User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User list Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    
    <script type="text/javascript" language="javascript" src="Javascript/User.js"></script>
 
    <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
	      function check()
		 {
			var ac=confirm('Are you sure you want to permanently delete this record ?');
			document.form1.Hidtxt.value=ac;
			document.form1.submit(); 
		 }

 </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table class="SixHundredPXTable" align="center">
                <tr>
                    <td class="MainHeader" style="height: 50px">
                        <asp:Label ID="Label2" runat="server" Text="Search / Edit User"></asp:Label></td>
                </tr>
                <tr>
                    <td style="height: 50px">
                        <asp:Button runat="server" ID="btnNew" Text="New" Width="95px" />
                        <asp:Button runat="server" ID="btnsearch" Text="Search" Width="95px" /></td>
                </tr>
                <tr class="Meter_SearchTable" style="background-color: Silver;">
                    <td runat="server" id="TduserList" width="600px" visible="false" class="Per_tdsearch">
                        <input type="hidden" id="Hidtxt" runat="server" />
                        <asp:GridView runat="server" ID="Griduser1" Width="600px" BorderColor="#999999" BorderStyle="None"
                            BackColor="white" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="user_ID" EmptyDataText="0 records found for selected search criteria">
                            <FooterStyle BackColor="#a5bbc5" ForeColor="Black" />
                            <RowStyle BackColor="white" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                            <PagerStyle BackColor="#a5bbc5" ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" />
                            <AlternatingRowStyle BackColor="#fdfeb6" />
                            <EmptyDataRowStyle BackColor="Silver" BorderColor="SteelBlue" BorderStyle="Solid"
                                BorderWidth="1px" Font-Bold="True" ForeColor="Red" />
                            <Columns>
                                <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:CommandField>
                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:CommandField>
                                <asp:BoundField DataField="Login" HeaderText="Login Name" />
                                <asp:BoundField DataField="FNAME" HeaderText="First Name" />
                                <asp:BoundField DataField="Lname" HeaderText="Last Name" />
                                <asp:BoundField DataField="Level" HeaderText="Level" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
