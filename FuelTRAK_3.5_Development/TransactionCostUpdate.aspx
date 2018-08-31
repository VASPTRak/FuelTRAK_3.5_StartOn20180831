<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransactionCostUpdate.aspx.vb" Inherits="TransactionCostUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Update Transaction Cost</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script type="text/javascript" src="Javascript/DateTime.js"></script>

    <!--<script type="text/javascript" src="Javascript/ExportTransaction.js"></script>-->

    <script type="text/javascript" src="Javascript/Validation.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <script language="javascript" type="text/javascript">
    
    
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	 function KeyPress(e)
     {
    
        if (window.event.keyCode < 48 || window.event.keyCode > 122)  
        { window.event.keyCode = 0;}
        if (window.event.keyCode == 92 || window.event.keyCode == 94 || window.event.keyCode == 95 || window.event.keyCode == 96 ||window.event.keyCode == 60 ||window.event.keyCode == 62 || window.event.keyCode == 63 || window.event.keyCode == 64)
        { window.event.keyCode = 0;}
        
      }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
   <div>
        <table align="center">
            <tr>
                <td valign="top">
                    <table align="center" id="tblImportPers" runat="server" class="FiveHundredPXTable" cellpadding ="3">
                        <tr>
                            <td colspan="5" class="MainHeader" style="height: 50px">
                                <asp:Label runat="server" ID="lblHeader" Text="Update Transaction Cost"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px; vertical-align: top" class="Sentry_Lable_Center">
                                <asp:Label runat="server" Text="Product Number" ID="lblHeader1" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small" ForeColor="Black"></asp:Label>
                            </td>
                            <td style="width: 100px; vertical-align: top " class="Sentry_Lable_Center">
                                <asp:Label runat="server" Text="Product Name" ID="lblHeader2" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small" ForeColor="Black" CssClass="Product_Center"></asp:Label>
                            </td>
                            <td style="width: 100px; vertical-align: top " class="Sentry_Lable_Center">
                                <asp:Label runat="server" Text="Preset Price" ID="lblHeader3" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small" ForeColor="Black" CssClass="Product_Center"></asp:Label>
                            </td>
                            <td style="width: 80px; vertical-align: top" class="Sentry_Lable_Center">
                                <asp:Label runat="server" Text="Select Product" ID="lblHeader4" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small" ForeColor="Black"></asp:Label>
                            </td>
                            <td style="width: 100px; vertical-align: top" class="Sentry_Lable_Center">
                                <asp:Label runat="server" Text="Click to View History" ID="lblHeader5" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="Small" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L1" Text="01" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtN1" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr1" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk1" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice1" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L2" Text="02" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="txtN2" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr2" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk2" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice2" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L3" Text="03" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="TxtN3" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr3" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk3" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice3" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L4" Text="04" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN4" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr4" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk4" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice4" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L5" Text="05" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN5" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr5" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk5" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice5" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L6" Text="06" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN6" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr6" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk6" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice6" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L7" Text="07" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN7" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr7" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk7" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice7" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L8" Text="08" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN8" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr8" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk8" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice8" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L9" Text="09" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="TxtN9" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr9" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk9" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice9" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L10" Text="10" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN10" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr10" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk10" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice10" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L11" Text="11" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN11" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr11" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk11" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice11" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L12" Text="12" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="TxtN12" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr12" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk12" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice12" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L13" Text="13" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="TxtN13" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr13" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" style="height: 26px" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk13" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice13" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L14" Text="14" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                 <asp:Label runat="server" ID="TxtN14" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr14" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk14" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice14" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Lable_Center">
                                <asp:Label runat="server" ID="L15" Text="15" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="TxtN15" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td class="Product_Center">
                                <asp:Label runat="server" ID="txtPr15" CssClass="TenCharTxtBox" MaxLength="10"></asp:Label>
                            </td>
                            <td align="center" class="Product_Center">
                                <asp:CheckBox runat="server" ID="Chk15" />
                            </td>
                            <td class="Product_Center">
                                <asp:Button ID="btnPrice15" runat="server" Text="View History" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center" style="height: 50px">
                                <asp:Button runat="server" ID="btnOk" Text="OK" Width="66px" />
                                <asp:Button runat="server" ID="btncancel" Text="Cancel" Width="66px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <div id="PopUpProduct" runat="server" style="position: absolute; border: solid 3px #00004C;
        background-color: #A5BBC5; top: 530px; left: 270px; width: 470px; height: 200px" >
        <table class="VehicleEditTypePopUp" align="center" style="width: 470px; height: 200px">
            <%--<tr>
                <td class="VehicleEditPMPopUpHeader" colspan="4" style="text-align: center">
                    <asp:Label ID="Label8" runat="server" Text="View History"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 505px; border-bottom: solid 1px black; text-align: right" colspan="4">
                    <asp:Button ID="btnHistory" runat="server" Text="View history" Height="24px" Font-Names="Verdana"
                        Font-Size="Small" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td style="width: 505px" colspan="4" style="text-align: right">
                    <asp:Label ID="Label2" runat="server" Text="Re-post price:"></asp:Label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                        EnableViewState="False"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="StartdateLabel" runat="server" Text="From Date:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                        EnableViewState="False"></asp:TextBox>
                    <img id="StartDateImage" alt="Start Date" runat="server" onclick="Javascript:cal1.popup()"
                        src="images/cal.gif" style="cursor: hand" tabindex="2" />
                </td>
                <td class="StdLableTxtLeft" style="height: 35px">
                    <asp:Label ID="Label1" runat="server" Text="To Date:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtEndDate" runat="server" Style="position: relative" MaxLength="10"
                        CssClass="TenCharTxtBox" EnableViewState="False"></asp:TextBox>
                    <img id="EndDateImage" alt="End Date" runat="server" onclick="Javascript:cal2.popup()"
                        src="images/cal.gif" style="cursor: hand" tabindex="4" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="padding: 0px">
                    <asp:Button ID="btnProdOK" runat="server" Text="Repost in Transaction" Height="24px"
                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" style="cursor:pointer"/>
                    <asp:Button ID="btnProdClose" runat="server" Text="Cancel" Height="24px" Width="65px"
                        Font-Names="Verdana" Font-Size="Small" CausesValidation="False" style="cursor:pointer"/>
                </td>
            </tr>
        </table>
    </div>
    
    <div id="PopUpHistory" runat="server" style="position: absolute; border: solid 3px #00004C;
        background-color: #A5BBC5; top: 0px; left: 260px; width: 510px; height:750px">
        <table class="VehicleEditTypePopUp" align="center" style="width: 505px">
            <tr>
                <td class="VehicleEditPMPopUpHeader" colspan="4" style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="History"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="VehicleGrid" align="center" colspan="4">
                    <asp:GridView Width="505px" ID="grdHistory" runat="server" BackColor="White" BorderColor="black"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                        AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true" EmptyDataText="0 history records found for selected product">
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <Columns>
                            <asp:TemplateField SortExpression="FromDate" ItemStyle-HorizontalAlign="Center" HeaderText="From Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" Text='<%# DataBinder.Eval(Container.DataItem, "FromDate")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ToDate" ItemStyle-HorizontalAlign="Center" HeaderText="To Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblToDate" Text='<%# DataBinder.Eval(Container.DataItem, "ToDate")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ResetPrice" ItemStyle-HorizontalAlign="Center" HeaderText="Reset Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblResetPrice" Text='<%# DataBinder.Eval(Container.DataItem, "ResetPrice")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="UserName" ItemStyle-HorizontalAlign="Center" HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="DateAdded" ItemStyle-HorizontalAlign="Center" HeaderText="Date of Re-post">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateAdded" Text='<%# DataBinder.Eval(Container.DataItem, "DateAdded")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="white" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                        <PagerStyle BackColor="#A5BBC5" ForeColor="Black" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="black" />
                        <AlternatingRowStyle BackColor="#fdfeb6" />
                        <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BackColor="white" BorderColor="red"
                            BorderStyle="Solid" BorderWidth="1px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="padding: 0px">
                    <asp:Button ID="btnCancelHistory" runat="server" Text="cancel" Height="24px" Font-Names="Verdana"
                        Font-Size="Small" CausesValidation="False" style="cursor:pointer"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>

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

</html>
