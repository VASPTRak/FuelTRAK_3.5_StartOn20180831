<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExportSummary.aspx.vb" Inherits="ExportSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export Summary</title>
<script type="text/javascript" src="Javascript/Validation.js"></script>
    <script type="text/javascript">

        function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
        function Run_Report(url)
        { window.open(url);        }
    </script>

    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table id="tblExport" align="center" cellpadding="0" cellspacing="0" class="FourHundredPXTable">
                <tr align="center">
                    <td colspan="6" class="MainHeader" style="height:50px">
                        <asp:Label ID="Label2" runat="server" Text="Export Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td class="ExportTxtLabel" style="width:200px">
                        <asp:Label ID="Label1" runat="server" >Oldest Transaction Date:</asp:Label>
                    </td>
                    <td class="ExportTxtLabelSub" style="width:200px">
                        <asp:Label ID="lblLastDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ExportTxtLabel">
                        <asp:Label ID="Label3" runat="server" >Newest Transaction Date:</asp:Label>
                    </td>
                    <td  class="ExportTxtLabelSub">
                        <asp:Label ID="lblCurrentDate" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ExportTxtLabel">
                        <asp:Label ID="Label4" runat="server" >Total Transactions:</asp:Label></td>
                    <td  class="ExportTxtLabelSub">
                        <asp:Label ID="lblRecCnt" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ExportTxtLabel">
                        <asp:Label ID="Label5" runat="server" >Export Format:</asp:Label></td>
                    <td  class="ExportTxtLabelSub">
                        <asp:Label ID="lblExportFormat" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="SearchTable" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center; height:35px; vertical-align:bottom">
                        <asp:Button ID="btnPringSummary" runat="server" Text="Export Report" Height="24px" Width="100px" />
                        <asp:Button ID="btnDownload" runat="server" Text="Download File" Height="24px" Width="100px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
