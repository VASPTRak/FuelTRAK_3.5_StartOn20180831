<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VehInstMsg.aspx.vb" Inherits="vehInstMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle Instant Messages</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="VehShow" runat="server">
            <table align="center" class="NineHundredPXTable">
                <tr>
                    <td>
                        <table style="text-align: center" class="EightHundredPXTable">
                            <tr>
                                <td colspan="2" class="MainHeader">
                                    <asp:Label ID="Label2" runat="server" Text="Search / Edit Vehicle"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="tblContain1" class="NineHundredPXTable">
                                        <tr>
                                            <td colspan="4" class="HeaderSearchCriteria" style="height: 50px">
                                                <asp:Label ID="Label3" runat="server" Text="Please Enter Your Vehicle ID and Select Search:"
                                                    Font-Underline="False" Width="389px"></asp:Label>
                                                <asp:Button ID="btnSearch" CssClass="VehicleEditBtn" runat="server" Text="Search"
                                                    OnClick="btnSearch_Click" TabIndex="11" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table align="center" id="tblSearch" class="FiveHundredPXTable">
                                                    <tr>
                                                        <td class="StdLableTxtLeft" style="width: 150px; height: 32px">
                                                            <asp:Label ID="Label4" Text="Vehicle ID:" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 200px">
                                                            <asp:TextBox ID="txtVehicleID" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                                                                TabIndex="1"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" id="tdsearch" class="VehicleResult" runat="server" visible="false">
                                                <asp:Label Text="Search Result" runat="server" ID="lblresult" Font-Names="arial"
                                                    Font-Bold="True" ForeColor="black" Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="VehicleGrid" align="center">
                                                <asp:GridView Width="900px" ID="gvVehRslts" runat="server" BackColor="White" BorderColor="black"
                                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" DataKeyNames="VehicleId"
                                                    AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true" EmptyDataText="0 records found for selected search criteria">
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="VehID" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="IDENTITY" ItemStyle-HorizontalAlign="Center" HeaderText="Vehicle ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIdentity" Text='<%# DataBinder.Eval(Container.DataItem, "IDENTITY")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHMAKE" ItemStyle-HorizontalAlign="Left" HeaderText="VEH MAKE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHMAKE" Text='<%# DataBinder.Eval(Container.DataItem, "VEHMAKE")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHMODEL" ItemStyle-HorizontalAlign="Left" HeaderText="VEH MODEL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHMODEL" Text='<%# DataBinder.Eval(Container.DataItem, "VEHMODEL")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHYEAR" ItemStyle-HorizontalAlign="Center" HeaderText="VEH YEAR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHYEAR" Text='<%# DataBinder.Eval(Container.DataItem, "VEHYEAR")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="VEHVIN" ItemStyle-HorizontalAlign="Left" HeaderText="VEH VIN">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVEHVIN" Text='<%# DataBinder.Eval(Container.DataItem, "VEHVIN")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="KEY_NUMBER" ItemStyle-HorizontalAlign="Center"
                                                            HeaderText="KEY NUMBER">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKEY_NUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "KEY_NUMBER")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="CARD_ID" ItemStyle-HorizontalAlign="Center" HeaderText="CARD_ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCARD_ID" Text='<%# DataBinder.Eval(Container.DataItem, "CARD_ID")%>'
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
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="PopUpVehMsg" runat="server" style="position: absolute; border: solid 3px #00004C;
            background-color: #A5BBC5; top: 183px; left: 378px; width: 389px;">
            <table class="VehicleEditMsgPopUp" align="center" style="width: 100%; height: 100%">
                <tr>
                    <td class="VehicleEditPMPopUpHeader" colspan="2">
                        <asp:Label ID="lblVehMsgPopUp" runat="server" Text="Vehicle Message"></asp:Label></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="right" style="width: 147px">
                        <asp:Label ID="lblVehID" runat="server" Text="Vehicle ID:"></asp:Label>
                    </td>
                    <td style="width: 159px; text-align: left">
                        <asp:TextBox ID="txtPopUpVehiclelD" runat="server" CssClass="TenCharTxtBox" MaxLength="10"
                            TabIndex="39"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 147px">
                        <asp:Label ID="lblVehMsg" runat="server" Text="Instant Message:"></asp:Label>
                    </td>
                    <td style="width: 159px">
                        <asp:TextBox ID="txtInstmsg" runat="server" CssClass="FifityFiveCharTxtBox" MaxLength="50"
                            TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="Center">
                        <asp:Button ID="btnCustomMsg" runat="server" Text="Custom Message" Width="169px"></asp:Button></td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="height: 26px">
                        <asp:Button ID="btnVehMsgSave" runat="server" Text="OK" Height="24px" Width="65px"
                            Font-Names="Verdana" Font-Size="Small" CausesValidation="False" />
                        <asp:Button ID="btnVehMsgCancel" runat="server" Text="Close" Height="24px" Width="65px"
                            Font-Names="Verdana" Font-Size="Small" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="PopUpCustMsg" runat="server" style="position: absolute; border: solid 3px #00004C;
            background-color: #A5BBC5; top: 237px; left: 293px; width: 550px;">
            <table class="VehicleEditMsgPopUp" align="center" style="width: 100%">
                <tr>
                    <td class="VehicleEditPMPopUpHeader" colspan="4">
                        <asp:Label ID="Label9" runat="server" Text="Custom Message"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lstbox1" runat="server" Width="250px" SelectionMode="Multiple" Rows="5">
                        </asp:ListBox>
                    </td>
                    <td>
                        <table id="spltTable" runat="server">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnAddItems" runat="server" ImageUrl="~/images/header_bg_Expand.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnRemoveItems" runat="server" ImageUrl="~/images/header_bg_Collapse.gif" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:ListBox ID="lstbox2" runat="server" Width="250px" SelectionMode="Multiple" Rows="5">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="Center">
                        <asp:Button ID="btnCustClose" runat="server" Text="Close"></asp:Button></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
