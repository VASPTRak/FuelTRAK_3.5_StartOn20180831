<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankReconcileDetail.aspx.vb" Inherits="TankReconcileDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }


 </script>
 
    <title>Untitled Page</title>
    
 <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblSentryName" runat="server" Text="Sentry Name" Cssclass="MainHeader"></asp:Label>&nbsp;
        <asp:Label ID="lblTankInfo" runat="server" Font-Size="X-Large" ForeColor="black" Height="40px"
                Style="z-index: 100; left: 16px; position: absolute; top: 72px" Text="Tank Information"
                Width="600px"></asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 101; left: 24px; position: absolute; top: 128px" Text="On-Hand as of:"
            Width="112px"></asp:Label>
        <asp:Label ID="lblBeginHeight" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 102; left: 504px; position: absolute; top: 160px"
            Text="On-Hand as of:" Width="256px"></asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 103; left: 184px; position: absolute; top: 200px" Text="Total Delivered:"
            Width="120px"></asp:Label>
        &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label10" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 103; left: 240px; position: absolute; top: 232px" Text="Subtotal:"
            Width="64px"></asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 103; left: 312px; position: absolute; top: 256px" Text="================="
            Width="168px"></asp:Label>
        <asp:Label ID="lblBeginGallons" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 104; left: 312px; position: absolute; top: 160px"
            Text="On-Hand as of:" Width="168px"></asp:Label>
        <asp:Label ID="lblDelivery" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 105; left: 312px; position: absolute; top: 200px" Text="Total Dispensed:"
            Width="168px"></asp:Label>
        <asp:Label ID="lblSubTotal" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 105; left: 312px; position: absolute; top: 232px" Text="Total Dispensed:"
            Width="168px"></asp:Label>
        <asp:Label ID="lblDispensed1" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 106; left: 176px; position: absolute; top: 288px"
            Text="Total Dispensed:" Width="128px"></asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 107; left: 152px; position: absolute; top: 328px" Text="Calculated On-Hand:"
            Width="152px"></asp:Label>
        <asp:Label ID="Label12" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 107; left: 24px; position: absolute; top: 528px" Text="*all numbers are in gallons unless otherwise noted"
            Width="392px"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 107; left: 504px; position: absolute; top: 328px" Text="(Subtotal minus Total Dispensed)"
            Width="264px"></asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 107; left: 504px; position: absolute; top: 288px" Text="(from Fuel Management System)"
            Width="264px"></asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 107; left: 520px; position: absolute; top: 128px" Text="- from tank monitor"
            Width="152px"></asp:Label>
        <asp:Label ID="Label8" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 107; left: 536px; position: absolute; top: 392px" Text="- from tank monitor"
            Width="152px"></asp:Label>
        <asp:Label ID="lblDispensed" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 108; left: 312px; position: absolute; top: 288px"
            Text="On-Hand as of:" Width="168px"></asp:Label>
        <asp:Label ID="lblCalculated" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 109; left: 312px; position: absolute; top: 328px"
            Text="On-Hand as of:" Width="168px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 110; left: 24px; position: absolute; top: 392px" Text="On-Hand as of:"
            Width="112px"></asp:Label>
        <asp:Label ID="lblEndHeight" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 111; left: 504px; position: absolute; top: 432px"
            Text="On-Hand as of:" Width="256px"></asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 112; left: 168px; position: absolute; top: 472px" Text="Over/Short (+/-):"
            Width="128px"></asp:Label>
        <asp:Label ID="lblDifference" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 117; left: 312px; position: absolute; top: 472px"
            Text="Over/Short (+/-):" Width="128px"></asp:Label>
        <asp:Label ID="lblEndGallons" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 114; left: 312px; position: absolute; top: 432px"
            Text="On-Hand as of:" Width="168px"></asp:Label>
        <asp:Label ID="lblEndDate" runat="server" Font-Size="Larger" ForeColor="black" Height="24px"
            Style="z-index: 115; left: 144px; position: absolute; top: 392px" Text="On-Hand as of:"
            Width="368px"></asp:Label>
        <asp:Label ID="lblStartDate" runat="server" Font-Size="Larger" ForeColor="black"
            Height="24px" Style="z-index: 116; left: 144px; position: absolute; top: 128px"
            Text="On-Hand as of:" Width="360px"></asp:Label>
    </div>
    </form>
</body>
</html>
