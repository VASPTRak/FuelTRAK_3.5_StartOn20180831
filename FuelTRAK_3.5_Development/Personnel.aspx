<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Personnel.aspx.vb" Inherits="Personnel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Personnel Search</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/DateTime.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/personnel.js"></script>

    <script type="text/javascript" src="Javascript/Validation.js"></script>

    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="Javascript/jquery-ui.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function chksesion() {
            alert("Session expired, Please login again.");
            window.location.assign('LoginPage.aspx')
        }


        function check() {
            var ac = confirm('Are you sure you want to permanently delete this record ?');
            //document.form1.Hidtxt.value = ac;
            var hidden = ac.toString();
            $("#Hidtxt").val(hidden)
            //document.form1.Hidtxt.value=ac.toString();
            //document.form1.txtVehId.value = PerId;
            form1.submit();
        }

        //function check(PerId)
        //   {
        //       var ac=confirm('Are you sure you want to permanently delete this record ?');
        //       document.form1.Hidtxt.value=ac;//
        //       document.form1.txtVehId.value=PerId;
        //       form1.submit(); 
        //   }

        $(function () {
            $('#txtLastname').keydown(function (e) {

                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                }
                else {
                    var key = e.keyCode;
                    if (((key == 186) || (key == 222))) {
                        e.preventDefault();
                    }
                }
            });
        });
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div style="overflow: auto">
            <table align="center" width="MaximumPXTable">
                <tr>
                    <td class="MainHeader" colspan="3" style="text-align: center">
                        <asp:Label ID="Label2" runat="server" Text="Search / Edit Personnel"></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" OnClick="btnNew_Click"
                            UseSubmitBehavior="False" />
                        <input id="btnShowSearch" type="button" value="Search" onclick="ShowSearch()" style="width: 100px" />
                        <input type="hidden" id="Hidtxt" runat="server" style="width: 10px" />
                        <input type="hidden" id="txtVehId" runat="server" style="width: 10px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table align="center" id="tblContain1" runat="server" class="SixHundredPXTable">
                            <tr>
                                <td>
                                    <table id="tblSearch" runat="server" class="SixHundredPXTable">
                                        <tr>
                                            <td class="HeaderSearchCriteria" colspan="6" style="height: 50px">
                                                <asp:Label ID="Label1" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                    Width="100px" AccessKey="s" TabIndex="7" /></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="width: 100px; height: 26px">
                                                <asp:Label ID="lblPersonnelID" Text="Personnel ID:" runat="server"></asp:Label></td>
                                            <td style="height: 26px; text-align: left">
                                                <asp:TextBox ID="txtPersonnelID" runat="server" Visible="true" CssClass="TenCharTxtBox" MaxLength="10" TabIndex="1"></asp:TextBox></td>
                                            <td style="height: 26px"></td>
                                            <td class="StdLableTxtLeft" style="height: 26px">
                                                <asp:Label ID="lblAccountID" Text="Account ID:" runat="server"></asp:Label></td>
                                            <td style="height: 26px; text-align: left">
                                                <asp:TextBox ID="txtAccountID" runat="server" CssClass="TenCharTxtBox" MaxLength="11" TabIndex="4"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="height: 26px">
                                                <asp:Label ID="lblLastname" Text="Last Name:" runat="server"></asp:Label></td>
                                            <td style="height: 26px; text-align: left">
                                                <asp:TextBox ID="txtLastname" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20" TabIndex="2"></asp:TextBox></td>
                                            <td></td>
                                            <td class="StdLableTxtLeft" style="height: 26px">
                                                <asp:Label ID="lblKeyNumber" Text="Key Number:" runat="server"></asp:Label></td>
                                            <td style="height: 26px; text-align: left">
                                                <asp:TextBox ID="txtKeynumber" runat="server" CssClass="FiveCharTxtBox" MaxLength="5" TabIndex="5"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="StdLableTxtLeft" style="height: 28px">
                                                <asp:Label ID="lblDepartment" Text="Department:" runat="server"></asp:Label></td>
                                            <td style="height: 28px; text-align: left">
                                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3" TabIndex="3"></asp:TextBox></td>
                                            <td style="height: 28px"></td>
                                            <td class="StdLableTxtLeft" style="height: 28px">
                                                <asp:Label ID="lblCardNumber" Text="Card Number:" runat="server"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="EightCharTxtBox" MaxLength="7" TabIndex="6"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 26px"></td>
                                            <td colspan="1" style="height: 26px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5"></td>
                                        </tr>
                                        <tr>
                                            <td id="tdsearch" class="Per_Header" colspan="5" runat="server" visible="false" style="height: 19px; text-align: center">
                                                <asp:Label ID="lblresult" Text="Search Results" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" class="Per_tdsearch">
                                                <asp:GridView runat="server" ID="GridView1" AllowSorting="true" BackColor="White"
                                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                    Width="100%" Style="border-top-style: ridge; border-right-style: ridge; border-left-style: ridge; border-bottom-style: ridge; border-left-color: navy; border-bottom-color: navy; border-top-color: navy; border-right-color: navy;"
                                                    AllowPaging="True" AutoGenerateColumns="False"
                                                    DataKeyNames="PersonnelId,IDENTITY,LAST_NAME,FiRST_Name,MI,KEY_NUMBER" EmptyDataText="0 records found for selected search criteria"
                                                    PageSize="15">
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <RowStyle BackColor="white" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                                    <PagerStyle BackColor="#A5BBC5" ForeColor="Black" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="Black" />
                                                    <AlternatingRowStyle BackColor="#fdfeb6" />
                                                    <EmptyDataRowStyle BackColor="#00004C" BorderColor="SteelBlue" BorderStyle="Solid"
                                                        BorderWidth="1px" Font-Bold="True" ForeColor="Red" />
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:CommandField>
                                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField SortExpression="IDENTITY" ItemStyle-HorizontalAlign="Center" HeaderText="Personnel ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIdentity" Text='<%# DataBinder.Eval(Container.DataItem, "IDENTITY")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="LAST_NAME" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Last Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLASTNAME" Text='<%# DataBinder.Eval(Container.DataItem, "LAST_NAME")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="FIRST_NAME" ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="First Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFIRSTNAME" Text='<%# DataBinder.Eval(Container.DataItem, "FIRST_NAME")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="MI" ItemStyle-HorizontalAlign="Center" HeaderText="MI">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMI" Text='<%# DataBinder.Eval(Container.DataItem, "MI")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="ACCT_ID" ItemStyle-HorizontalAlign="Center" HeaderText="ACCT ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblACCTID" Text='<%# DataBinder.Eval(Container.DataItem, "ACCT_ID")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="KEY_NUMBER" ItemStyle-HorizontalAlign="Center"
                                                            HeaderText="KEY NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKEYNUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "KEY_NUMBER")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="CARD_ID" ItemStyle-HorizontalAlign="Center" HeaderText="CARD ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCARDID" Text='<%# DataBinder.Eval(Container.DataItem, "CARD_ID")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="DEPT" ItemStyle-HorizontalAlign="Center" HeaderText="Dept">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDEPT" Text='<%# DataBinder.Eval(Container.DataItem, "DEPT")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
