<%@ Page Language="VB" AutoEventWireup="true" CodeFile="ReportExport.aspx.vb" Inherits="Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export Report</title>
    <%--<link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />--%>
    <%--<script type="text/javascript" language="javascript" src="javascript/Validation.js"></script>--%>

    <script language="javascript" type="text/javascript">

        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }

        function CallLink()
        {
            alert(document.getElementById('DDLExportType').value);
            location.href=("Export.aspx?abc=" + document.getElementById('DDLExportType').value) ;
            setInterval ('self.close()', 15000);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center" style="height:300px">
                <tr>
                    <td align="center">
                        <br />
                        <asp:Label ID="Label1" runat="server" Width="432px" Style="background-color: #5D7B9D"></asp:Label><br />
                        <asp:Label ID="Label2" runat="server" Width="432px" Style="font-weight: bold; font-size: 15pt;
                            color: white; font-family: Arial; background-color: black"></asp:Label><br />
                        <br />
                        <asp:Label ID="lbl1" runat="server" Text="Please select an Export format from the list."
                            Style="font-weight: bold; font-size: 10pt; color: black; font-family: Arial"></asp:Label>
                        <br />
                        <br />
                        <asp:DropDownList ID="DDLExportType" runat="server" Width="232px">
                        </asp:DropDownList><br />
                        <br />
                        <input id="Button1" type="button" runat="server" value="Ok" onclick="CallLink();"
                            style="width: 80px" />
                        <asp:Button ID="btnExport" runat="server" Text="Ok" Width="72px" Visible="False" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" Width="72px" Visible="False" />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
