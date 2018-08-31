<%@ Page Language="vB" AutoEventWireup="false" CodeFile="~/translationTable.aspx.vb"
    Inherits="TranslationTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Translation Table Selection</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Javascript/ExportTransaction.js"></script>

    <script language="javascript" type="text/javascript">
     
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }

        function Run_Report(url)
        { window.open(url);        }
        
        function check(field,order)
        {
            var ac=confirm('Are you sure you want to permanently delete this record ?');
            document.form1.Hidtxt.value=ac;//
            document.form1.txtFieldName.value=field;
            document.form1.txtOrder.value=order;
            form1.submit(); 
        }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table class="MaximumPXTable" align="center">
                <tr>
                    <td>
                        <table align="center" class="MaximumPXTable">
                            <tr>
                                <td class="MainHeader" colspan="2" style="height: 50px">
                                    <asp:Label ID="Label1" runat="server" Text="Translation Table"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="Translation_List" style="height:50px; vertical-align:middle; text-align: center">
                                    <asp:Label ID="Label2" runat="server" Text="Export Format:"></asp:Label>
                                    <asp:RadioButtonList CssClass="RadioBtnList" ID="rdoSelect" runat="server" AutoPostBack="True"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Selected="True" Value="1">Export 1 &nbsp; &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">Export 2</asp:ListItem></asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td class="800px" align="center" valign="middle" colspan="2">
                                    <asp:GridView ID="GridView1" runat="server" Width="800px" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                        DataKeyNames="n_order,field_name" EmptyDataText="No fields are selected for export transactions"
                                        GridLines="Vertical" PageSize="15" style="border-top-style: ridge; border-right-style: ridge; 
                                        border-left-style: ridge; border-bottom-style: ridge; border-left-color: black; border-bottom-color: black; 
                                        border-top-color: black; border-right-color: black;" >
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <EmptyDataRowStyle BackColor="#00004c" BorderColor="SteelBlue" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" ForeColor="Red" />
                                        <Columns>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial"  Font-Size="10pt"/>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt"
                                                    Width="70px" />
                                            </asp:CommandField>
                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt"/>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="70px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="n_order" HeaderText="Record Order" >
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="70px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="field_desc" HeaderText="Field Name">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="140px"/>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="length" HeaderText="Field Length">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="150px"/>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="n_length" HeaderText="Field Length In Export File">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="150px"/>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="startpos" HeaderText="Starting From">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="150px"/>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="10pt" />
                                            </asp:BoundField>
                                        </Columns>
                                        <RowStyle BackColor="white" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                        <PagerStyle BackColor="#a5bbc5" ForeColor="Black" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" />
                                        <AlternatingRowStyle BackColor="#fdfeb6" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" colspan="3">
                                    &nbsp;<asp:Button ID="btnAdd" runat="server" Style="position: relative" Text="Add"
                                        Font-Names="arial" Font-Size="Small" Height="24px" Width="100px" />&nbsp;
                                    <asp:Button ID="PrintTranslationTable" runat="server" Style="position: relative"
                                        Text="Print" Font-Names="arial" Font-Size="Small" Height="24px" Width="100px" />
                                    <asp:Button ID="btnCancel" runat="server" Style="position: relative" Text="Cancel"
                                        Font-Names="arial" Font-Size="Small" Height="24px" Width="100px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 12px">
                                    <input type="hidden" id="Hidtxt" runat="server" style="height: 5px" />
                                    <input type="hidden" id="txtFieldName" runat="server" style="height: 5px" />
                                    <input type="hidden" id="txtOrder" runat="server" style="height: 5px" />
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
