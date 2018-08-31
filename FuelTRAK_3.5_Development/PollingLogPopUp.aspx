<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PollingLogPopUp.aspx.vb"
    Inherits="PollingLogPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>

<script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

</script>

<body>
    <form id="form1" runat="server" title="Polling Log">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="upnlPollingLog" runat="server">
                <ContentTemplate>
                    &nbsp;<asp:GridView ID="gvPollingLog" runat="server" AutoGenerateColumns="False"
                        HeaderStyle-HorizontalAlign="Center" GridLines="None" CellPadding="4" ForeColor="#333333">
                        <Columns>
                            <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="Message" HeaderText="Message">
                                <ItemStyle Width="450px" />
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="DeviceID" HeaderText="Sentry ID">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="DeviceType" HeaderText="Sentry Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BorderColor="#0000C0" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <EditRowStyle BackColor="#999999" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
