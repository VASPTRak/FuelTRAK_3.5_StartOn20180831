<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportViewer.aspx.vb" Inherits="ReportViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
namespace="CrystalDecisions.Web" tagprefix="CR" %>

<%-- <%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %> --%>
 <%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
  
      
<%--<link href="C:\Program Files (x86)\Business Objects\Common\4.0\crystalreportviewers12\css\default.css"
        rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" language="javascript" src="javascript/Validation.js"></script>

    <script language="javascript" type="text/javascript">
            function expand() 
            {
                window.moveTo(0,0);
                window.resizeTo(screen.availWidth, screen.availHeight);
            }
    </script>

   <%-- <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />--%>
    <%--<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />--%>
</head>
<body onload="expand()">
    <form id="form1" runat="server">
        <div>
            <table width="90%" align="center">
             <%--   <tr>
                    <td align="right" style="width: 60%">
                        <a href="Reports/InventoryReconciliation.rpt">Reports/InventoryReconciliation.rpt</a>
                        <asp:DropDownList ID="DDLExportType" runat="server" Width="168px" Visible="False">
                        </asp:DropDownList>
                        <asp:Button ID="btnExport" runat="server" Text="Export" Visible="False" />
                    </td>
                    <td width="50%" align="left">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="56px" Visible="False"/>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" valign="top">
                        <table>
                            <tr>
                                <td align="right" valign="top" style="height: 56px">
                                    &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_over.gif"
                                        Style="cursor: hand" Width="32px" ToolTip="Export" Height="28px" Visible="false" /></td>
                                <td align="left" style="height: 56px">
                                   <%-- <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" />--%>
                                    <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" BorderWidth="0px"
                                        ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False"
                                        HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasZoomFactorList="False"
                                        PrintMode="ActiveX" Width="350px"  
                                        EnableDrillDown="False" Height="50px" HasDrillUpButton="False" 
                                        
                                        HasRefreshButton="True"
                                        HasPrintButton="true"
                                        ReuseParameterValuesOnRefresh="True"
                                        
                                        GroupTreeStyle-Font-Underline="True"
                                        DisplayGroupTree="False"
                                        
                                        
                                        BorderStyle="Ridge"
                                         />
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
