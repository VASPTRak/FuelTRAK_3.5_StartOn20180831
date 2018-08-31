<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VehCustMsgs.aspx.vb" Inherits="VehCustMsgs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Vehicle Custom Message</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table class="MaximumPXTable" align="center">
                <tr>
                    <td class="MainHeader">
                        <asp:UpdatePanel ID="upnl" runat="server">
                            <ContentTemplate>
                                <table id="tblVehMsgs" class="MaximumPXTable" align="center">
                                    <tr>
                                        <td>
                                            <table align="center" class="MaximumPXTable">
                                                <tr>
                                                    <td class="MainHeader" colspan="2" style="height: 50px">
                                                        <asp:Label ID="Label1" runat="server" Text="Vehicle Messages"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="750px" align="center" valign="middle" colspan="2">
                                                        <asp:GridView ID="gvVehMsgs" runat="server" Width="700px" AutoGenerateColumns="False"
                                                            DataKeyNames="ID" EmptyDataText="0 records found for selected search criteria"
                                                            BorderColor="black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                            PageSize="15" Font-Bold="False" Font-Size="10pt">
                                                            <Columns>
                                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                                                        Font-Size="10pt" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Names="Arial"
                                                                        Font-Size="10pt" />
                                                                </asp:CommandField>
                                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMsgID" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Messages">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMsgs" Text='<%# DataBinder.Eval(Container.DataItem, "VehMessages")%>'
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="500px" ID="txtVehMessages" Text='<%# DataBinder.Eval(Container.DataItem, "VehMessages")%>'>
                                                                        </asp:TextBox>
                                                                        <asp:RequiredFieldValidator runat="server" ID="rfdGLEVELS" ControlToValidate="txtVehMessages"
                                                                            ErrorMessage="*Required" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtNewMsg" runat="Server" Width="500px"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="500px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <FooterTemplate>
                                                                        <asp:Button runat="server" ID="Insert" Text="Insert" CommandName="InsertNew" />
                                                                        <asp:Button runat="server" ID="Cancel" Text="Cancel" CommandName="CancelNew" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                Message:<asp:TextBox runat="server" ID="NoDataMessage" />
                                                                <asp:Button runat="server" ID="NoDataInsert" CommandName="NoDataInsert" Text="Insert" />
                                                            </EmptyDataTemplate>
                                                            <EmptyDataRowStyle Font-Size="Small" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                BorderWidth="1px" Font-Names="Verdana" />
                                                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#A5BBC5" ForeColor="Black" />
                                                            <FooterStyle BackColor="#A5BBC5" ForeColor="Black" />
                                                            <RowStyle BackColor="White" ForeColor="Black" />
                                                            <SelectedRowStyle BackColor="#FDFEB6" Font-Bold="True" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#A5BBC5" Font-Bold="True" ForeColor="Black" />
                                                            <AlternatingRowStyle BackColor="#FDFEB6" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="middle" colspan="2">
                                                        &nbsp;<asp:Button ID="btnAdd" runat="server" Style="position: relative" Text="Add"
                                                            Font-Names="arial" Font-Size="Small" Height="24px" Width="100px" />&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Style="position: relative" Text="Cancel"
                                                            Font-Names="arial" Font-Size="Small" Height="24px" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
