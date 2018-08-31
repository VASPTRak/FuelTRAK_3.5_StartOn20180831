<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="Reports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function AssignReportName()
        {
            document.getElementById('rptHiddenVal').value = document.getElementById('lstReport').value;            
        }   
        function chksesion()
        {   window.location.assign('LoginPage.aspx')        }
        
        function MaintainState()
        {
            var dt = new Date();
            var path1 = document.getElementById('lstReport');    

            for (var i = 0;  path1.options.length != 0;i=i)
            {            path1.options[i] = null;		}
            option0 = new Option("Transaction List in Date/Time Order",11);
            option1 = new Option("Transaction List in Sentry Order",12);
            option2 = new Option("Transaction List in Personnel Order",13);
            option3 = new Option("Transaction List in Vehicle Order",14);
            path1.options[0] = option0;
            path1.options[1] = option1;
            path1.options[2] = option2;
            path1.options[3] = option3;
            path1.options[0].selected = "selected";
            path1.SelectedIndex = 0;
            AssignReportName();
        }
        function Run_Report(url)    { window.open(url);        }
    </script>
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" style="width:800px">
                <tr>
                    <td align="center" colspan="2">
                        <label id="lblReport" class="MainHeader">
                            Select A Report</label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px; text-align:center"></td>
                </tr>
                <tr>
                    <td align="left" style="width:200px; vertical-align:top; font-family: Arial; font-size: 15px; font-weight: normal">
                        <%  If (Not (Session("User_name") Is Nothing)) Then%>
                            <%  If (Session("User_Level").ToString() = "1") Then%>
                            <asp:RadioButtonList ID="rdoSelectReport" runat="server" CellPadding="5" CellSpacing="5"
                                ForeColor="black" Font-Size="12pt" AutoPostBack="True" Style="cursor: hand" Visible="False">
                                <asp:ListItem Value="1" Selected="True">Transaction</asp:ListItem>
                                <asp:ListItem Value="2">Site</asp:ListItem>
                                <asp:ListItem Value="3">Billing</asp:ListItem>
                              <asp:ListItem Value="4">Vehicle</asp:ListItem>
                                <asp:ListItem Value="5">Personnel</asp:ListItem>
                                <asp:ListItem Value="6">Fuel Use</asp:ListItem>
                                <asp:ListItem Value="7">Inventory</asp:ListItem>
                                <asp:ListItem Value="8">Misc. Reports</asp:ListItem>
                            </asp:RadioButtonList>
                            <% Else%>
                            <asp:RadioButtonList ID="rdoSelectReport1" runat="server" CellPadding="5" CellSpacing="5"
                                ForeColor="black" Font-Size="12pt" AutoPostBack="True" Style="cursor: hand" Visible="False">
                                <asp:ListItem Value="1" Selected="True">Transaction</asp:ListItem>
                                <asp:ListItem Value="2" >Site</asp:ListItem>
                                <asp:ListItem Value="3">Billing</asp:ListItem>
                                <asp:ListItem Value="4">Vehicle</asp:ListItem>
                                <asp:ListItem Value="5">Personnel</asp:ListItem>
                                <asp:ListItem Value="6">Fuel Use</asp:ListItem>
                                <asp:ListItem Value="7">Inventory</asp:ListItem>
                                <asp:ListItem Value="8">Misc. Reports</asp:ListItem>
                            </asp:RadioButtonList>
                            <% End If%>
                        <% End If%>
                    </td>
                    <td style="text-align: left" valign="middle"  >
                        <select size="5" id="lstReport" runat="server" onclick="AssignReportName();" class="ReptSelect"
                            onkeyup="AssignReportName(); ">
                            <option selected="selected" value="11">Transaction List in Date/Time Order</option>
                            <%--<option value="12">Transaction List in Sentry Order</option>--%>
                            <option value="13">Transaction List in Personnel Order</option>
                            <%--<option value="14">Transaction List in Vehicle Order</option>--%>
                            <option value="15">Transaction List of Errors</option>
                            <%--<option value="16">Transaction List by Master Key Usage</option>
                            <option value="17">Transaction List by Vehicle Type</option>--%>
                            <%--<option value="401">Transaction List by Dept</option>--%>
                           <%-- <option value="402">Transaction Detail – Exceeds Miles Window</option>
                            <option value="403">Transaction Detail – MPG out-of-range</option>--%>
                        </select>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstReport"
                            ErrorMessage="Please select Report Type">*</asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Button ID="ReportOkButton" runat="server" Text="Next" Width="100px" />
                        <asp:TextBox ID="ReportNameTextBox" runat="server" Width="349px" Text="11" Visible="false"></asp:TextBox>
                        <input id="rptHiddenVal" type="hidden" value="11" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
