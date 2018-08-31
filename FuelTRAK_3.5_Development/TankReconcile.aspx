<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankReconcile.aspx.vb" Inherits="TankReconcile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

        function Run_Report(url)
        {
            window.open(url);
            //window.open(url,'Popup','width=300px,height=200px,titlebar=no,left='+((screen.width -300) / 2)+',top='+ (screen.height - 200) / 2+'');return false;
        }
       </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" Height="128px" Width="757px">
            &nbsp;<asp:Label ID="lbDistrict" runat="server" Font-Size="Larger" ForeColor="White"
                Text="Choose a District:"></asp:Label>
            <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" Font-Size="Larger"
                Width="140px">
                <asp:ListItem Selected="True" Value="1">DISTRICT 1</asp:ListItem>
                <asp:ListItem Value="2">DISTRICT 2</asp:ListItem>
                <asp:ListItem Value="3">DISTRICT 3</asp:ListItem>
                <asp:ListItem Value="4">DISTRICT 4</asp:ListItem>
                <asp:ListItem Value="5">DISTRICT 5</asp:ListItem>
                <asp:ListItem Value="6">DISTRICT 6</asp:ListItem>
                <asp:ListItem Value="7">DISTRICT 7</asp:ListItem>
                <asp:ListItem Value="8">DISTRICT 8</asp:ListItem>
                <asp:ListItem Value="9">DISTRICT 9</asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp;<br />
            <asp:Label ID="Label3" runat="server" Font-Size="Larger" ForeColor="White" Text="Choose a Tank:" style="z-index: 100; left: 208px; position: absolute; top: 96px"></asp:Label>
            <asp:DropDownList ID="ddlTank" runat="server" Font-Size="Larger" Width="336px" style="z-index: 101; left: 328px; position: absolute; top: 88px">
            </asp:DropDownList><br />
            <asp:Label ID="lblSentry" runat="server" Font-Size="Larger" ForeColor="White" Text="Choose a Sentry:" style="z-index: 102; left: 200px; position: absolute; top: 64px"></asp:Label>
            <asp:DropDownList ID="ddlSentry" runat="server" Font-Size="Larger" Width="336px" AutoPostBack="True" style="z-index: 104; left: 328px; position: absolute; top: 56px">
            </asp:DropDownList>
            <br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        </asp:Panel>
        &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnLaunch" runat="server" Font-Size="Larger" Height="41px" Text="Inventory Reconciliation"
                Width="240px" style="z-index: 100; left: 520px; position: absolute; top: 424px" Visible="False" />
        <asp:Calendar ID="calBeginDate" runat="server" BackColor="White" BorderColor="#999999"
            CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
            ForeColor="Black" Height="180px" Style="z-index: 101; left: 176px; position: absolute;
            top: 184px" Width="200px">
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <SelectorStyle BackColor="#CCCCCC" />
            <WeekendDayStyle BackColor="#FFFFCC" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        </asp:Calendar>
        <asp:Calendar ID="calEndDate" runat="server" BackColor="White" BorderColor="#999999"
            CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
            ForeColor="Black" Height="180px" Style="z-index: 102; left: 464px; position: absolute;
            top: 184px" Width="200px">
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <SelectorStyle BackColor="#CCCCCC" />
            <WeekendDayStyle BackColor="#FFFFCC" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        </asp:Calendar>
        <asp:Label ID="Label1" runat="server" Font-Size="Larger" ForeColor="White" Style="z-index: 103;
            left: 240px; position: absolute; top: 160px" Text="Begin Date"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Size="Larger" ForeColor="White" Style="z-index: 104;
            left: 552px; position: absolute; top: 160px" Text="End Date"></asp:Label>
        <asp:Label ID="lblBeginDate" runat="server" Font-Size="Larger" ForeColor="White" Style="z-index: 105;
            left: 192px; position: absolute; top: 368px" Text="End Date"></asp:Label>
        <asp:Label ID="lblEndDate" runat="server" Font-Size="Larger" ForeColor="White" Style="z-index: 106;
            left: 480px; position: absolute; top: 368px" Text="End Date"></asp:Label>
        <asp:TextBox ID="hidPumpList" runat="server" Style="z-index: 107; left: 472px; position: absolute;
            top: 328px" Visible="False"></asp:TextBox>
        <asp:Button ID="btnReport" runat="server" Font-Bold="False" Font-Size="Larger" Height="40px"
            Style="z-index: 108; left: 192px; position: absolute; top: 400px" Text="Run Report"
            Width="240px" />
        <asp:Label ID="lblError" runat="server" Font-Size="Larger" ForeColor="Red" Style="z-index: 110;
            left: 200px; position: absolute; top: 448px" Text="Please Select a Sentry!" Visible="False"
            Width="232px"></asp:Label>
    
    </div>
    </form>
</body>
</html>
