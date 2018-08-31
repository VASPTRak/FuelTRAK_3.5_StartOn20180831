<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PollingActions.aspx.vb"
    Inherits="PollingActions" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save" />
            <br />
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <script type="text/javascript" language="javascript">
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
             function EndRequestHandler(sender, args)
             {
               if (args.get_error() != undefined)
              {
               var errorMessage = args.get_response().get_statusCode() ;
               if (args.get_response().get_statusCode() == '500')
               {
                args.set_errorHandled(true);
               }              
              }          
            }

            </script>
            <div>
                <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="1000">
                </asp:Timer>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Polling Updates:" Font-Bold="true"></asp:Label>&nbsp;
                    <br />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Polling Data Refresh Every Second:" Font-Bold="true"></asp:Label>&nbsp;
                    <br />
                    <br />
                    <asp:GridView ID="gvPollingLog" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                        GridLines="None" CellPadding="4" ForeColor="#333333" OnRowDataBound="gvPollingLog_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderText="Datetime">
                                <ItemTemplate>
                                    <asp:Label ID="lblNUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "Datetime")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="450px" ItemStyle-HorizontalAlign="Center" HeaderText="Message">
                                <ItemTemplate>
                                    <asp:Label ID="lblNUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "Message")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" HeaderText="DeviceID">
                                <ItemTemplate>
                                    <asp:Label ID="lblNUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "DeviceID")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" HeaderText="DeviceType">
                                <ItemTemplate>
                                    <asp:Label ID="lblNUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "DeviceType")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
