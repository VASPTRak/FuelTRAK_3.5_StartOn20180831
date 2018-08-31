<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankRecon.aspx.vb" Inherits="TankRecon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tank Inventory Reconciliation Form Search</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/DateTime.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>

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
                                    <asp:Label ID="Label2" runat="server" Text="Tank Inventory Reconciliation Search"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" runat="server" id="tblGrid" visible="true" style="height: 76px">
                                    <table id="tblContain1" runat="server" class="SixHundredPXTable">
                                        <tr>
                                            <td class="Sentry_Tank_Dropdown">
                                                <asp:DropDownList runat="server" ID="ddlMonth" Width="200px" AutoPostBack="true">
                                                    <asp:ListItem Text="Please Select Month" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="JAN" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="FEB" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="MAR" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="APR" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="MAY" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="JUN" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="JULY" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="AUG" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="SEP" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="OCT" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="NOV" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="DEC" Value="12"></asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="Sentry_Tank_Dropdown">
                                                <asp:DropDownList runat="server" ID="ddlYear" Width="200px" Visible="false" AutoPostBack="true">
                                                    <asp:ListItem Text="Please Select Year" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                                    <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                                    <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                                    <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                                    <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                    <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                    <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                    <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="Sentry_Tank_Dropdown">
                                                <asp:DropDownList runat="server" ID="ddltank" Width="200px" Visible="false" AutoPostBack="true">
                                                </asp:DropDownList></td>
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
        
         if (document.getElementById('dtStart'))
        {
	        var cal1 = new calendar2(document.getElementById('dtStart'));
	        cal1.year_scroll = true;
	        cal1.time_comp = false;
	    }
//	    else 
        if (document.getElementById('dtEnd'))
	    {
	        var cal2 = new calendar2(document.getElementById('dtEnd'));
	        cal2.year_scroll = true;
	        cal2.time_comp = false;
	    }
    </script>

</body>
</html>
