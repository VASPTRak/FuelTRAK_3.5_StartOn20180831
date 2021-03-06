<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExportTransactions.aspx.vb"
    Inherits="ExportTransactions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export Transactions</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script type="text/javascript" src="Javascript/DateTime.js"></script>

    <!--<script type="text/javascript" src="Javascript/ExportTransaction.js"></script>-->

    <script type="text/javascript" src="Javascript/Validation.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <script type="text/javascript">
   
        function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
        function datecom()
        {   var firstDate = new Date(document.getElementById('txtStartDate').value);
            var secondDate = new Date(document.getElementById('txtEndDate').value);
            var firstYear = firstDate.getFullYear();
            var secondYear = secondDate.getFullYear();
            var firstMonth = firstDate.getMonth();
            var secondMonth = secondDate.getMonth();
            var firstDay = firstDate.getDate();
            var secondDay = secondDate.getDate();
            
            alert(firstYear + " " + secondYear);
            alert(firstMonth + " " + secondMonth);
            alert(firstDay + " " + secondDay);
            return false;
        }
        
        function ExportTrans1(strno)
        {
            alert (strno);
        }
       
       function DisplayDiv()
       {
       //alert(document.getElementById("TDImage"));
        document.getElementById("TDImage").style.display="";
        document.getElementById("TDButtons").style.display="none";
       }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
    <div align="center">
        <table id="tblExport" class="SixHundredPXTable">
            <tr align="center">
                <td colspan="6" style="height: 9px">
                    <%--<asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            ForeColor="Red" Text="Export Transaction" Width="100%" BackColor="Silver" Font-Underline="False"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="MainHeader" style="height: 50px">
                    <asp:Label ID="Label2" runat="server" Text="Export Transactions"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="StartdateLabel" runat="server" Text="Start Date:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                        EnableViewState="False"></asp:TextBox>
                    <img id="StartDateImage" alt="Start Date" runat="server" onclick="Javascript:cal1.popup()"
                        src="images/cal.gif" style="cursor: hand" tabindex="2" />
                </td>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="Label1" runat="server" Text="End Date:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtEndDate" runat="server" Style="position: relative" MaxLength="10"
                        CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                    <img id="EndDateImage" alt="End Date" runat="server" onclick="Javascript:cal2.popup()"
                        src="images/cal.gif" style="cursor: hand" tabindex="4" />
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="Label3" runat="server" Text="Start Time:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtStartTime" runat="server" Style="position: relative" MaxLength="5"
                        CssClass="FiveCharTxtBox" EnableViewState="False"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="Label4" runat="server" Text="End Time:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtEndTime" runat="server" Style="position: relative" MaxLength="5"
                        CssClass="FiveCharTxtBox" EnableViewState="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="Label5" runat="server" Text="Start Dept:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtStartDept" runat="server" Style="position: relative" EnableViewState="False"
                        CssClass="ThreeCharTxtBox" MaxLength="3"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="Label6" runat="server" Text="End Dept:">End Dept:</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtEndDept" runat="server" Style="position: relative" EnableViewState="False"
                        CssClass="ThreeCharTxtBox" MaxLength="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="Label7" runat="server" Text="Export Format:" BorderColor="White"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:RadioButtonList Style="vertical-align: middle; cursor: hand; text-align: left"
                        ID="rdoSelect" runat="server" ForeColor="Black" Font-Size="Small" Font-Names="Arial"
                        Font-Bold="False" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">Export1</asp:ListItem>
                        <asp:ListItem Value="2">Export2</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                </td>
            </tr>
            <%--By Soham Gangavane July 18,2017--%>
            <%--<tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="lblFileLocation" runat="server" Text="End Dept:">Enter File Location:</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtFileLocation" runat="server" Style="position: relative" EnableViewState="False"
                        Text="C:\" MaxLength="200"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>--%>
            <%--<tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="lblFileName" runat="server" Text="End Dept:">Enter File Name:</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtFileName" runat="server" Style="position: relative" EnableViewState="False"
                        Text="Export" MaxLength="200"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>--%>
             <%--<tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="lblFileType" runat="server" Text="End Dept:">Select File Extension:</asp:Label>
                </td>
                <td style="text-align: left">
                   <asp:DropDownList runat="server" ID="ddlFileType">
                   <asp:ListItem Selected="True" Text="Select File Extension" Value="select"></asp:ListItem>
                   <asp:ListItem  Text=".TXT" Value="txt"></asp:ListItem>
                   <asp:ListItem  Text=".CSV" Value="csv"></asp:ListItem>
                   <asp:ListItem  Text=".XLS" Value="xls"></asp:ListItem>
                   <asp:ListItem  Text=".DOC" Value="doc"></asp:ListItem>
                   </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>--%>
            <tr>
                <td class="SearchTable" align="center" colspan="4" style="height: 25px">
                    <asp:CheckBox ID="chkPrevTxtns" runat="server" CssClass="ExportTransLabel" Text="Include Previously Exported Transactions" />
                </td>
            </tr>
            <tr>
                <td class="SearchTable" align="center" colspan="4" style="height: 25px">
                    <asp:CheckBox ID="chkIncZeroQty" runat="server" CssClass="ExportTransLabel" Text="Include Zero Quantity Transactions" />
                </td>
            </tr>
            <tr>
                <td id="TDButtons" runat="server" align="center" colspan="4" style="height: 50px">
                    <asp:Button ID="btnCreateExport" runat="server" Text="Create Export" Height="24px"
                        Width="102px" OnClientClick="DisplayDiv()" />
                    <asp:Button ID="btnJustTrnslate" runat="server" Text="Translate Only" Height="24px"
                        Width="102px" />
                    <asp:Button ID="btnTransTable" runat="server" Text="Translation Table" Width="120px" />
                    <asp:Button ID="btnUndoExport" runat="server" Text="Undo Export" Width="105px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" class="SearchTable" style="height: 27px">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                        ShowMessageBox="True" ShowSummary="False" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtStartDept"
                        ControlToValidate="txtEndDept" ErrorMessage="End dept should be greater than start dept"
                        Operator="GreaterThanEqual" Style="position: relative; left: -110px; top: 104px;"
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="Red" Type="Integer"
                        Display="None" SetFocusOnError="True"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="position: relative;
                        left: 8px; top: 72px;" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                        ForeColor="Red" ErrorMessage="Please Enter Start Date" ControlToValidate="txtStartDate"
                        Display="None" SetFocusOnError="True" Width="110px"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter End Date" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="True" Style="left: -182px;
                        position: relative; top: 96px" Width="110px"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStartTime"
                        Display="None" ErrorMessage="Please Enter Start Time" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="True" Style="left: -262px;
                        position: relative; top: 16px" Width="110px"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEndTime"
                        Display="None" ErrorMessage="Please Enter End Time" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="True" Style="left: -62px;
                        position: relative; top: 104px" Width="110px"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="Small" ForeColor="Red" Style="left: -22px; position: relative;
                        top: 104px" Width="110px" ErrorMessage="Invalid Start Date" ControlToValidate="txtStartDate"
                        Display="None" SetFocusOnError="True" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Invalid End Date" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="True" Style="left: 184px;
                        position: relative; top: 104px" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                        Width="110px"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="Small" ForeColor="Red" Style="left: -166px; position: relative;
                        top: 16px" Width="110px" ErrorMessage="Invalid Start Time" ControlToValidate="txtStartTime"
                        Display="None" SetFocusOnError="True" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="Small" ForeColor="Red" Style="left: 72px; position: relative;
                        top: 104px" Width="110px" ErrorMessage="Invalid End Time" ControlToValidate="txtEndTime"
                        Display="None" SetFocusOnError="True" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td id="TDImage" class="SearchTable" align="center" colspan="4" style="height: 24px;
                    display: none">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/processing1.gif" />
                </td>
            </tr>
        </table>
    </div>
    </form>

        <script type="text/javascript" language="javascript">				
            
             if (document.getElementById('txtStartDate'))
            {
	            var cal1 = new calendar2(document.getElementById('txtStartDate'));
	            cal1.year_scroll = true;
	            cal1.time_comp = false;
    	    
	            var cal2 = new calendar2(document.getElementById('txtEndDate'));
	            cal2.year_scroll = true;
	            cal2.time_comp = false;
	        }
        </script>

</body>
</html>
