<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransactionsImport.aspx.vb"
    Inherits="TransactionsImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transactions Import</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script type="text/javascript">
     function chksesion()
	  {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	  }
    function DisplayDiv()
     {
     
         var fuData = document.getElementById('<%= fuTXTN.ClientID %>');
         var FileUploadPath = fuData.value;
         document.form1.filepathVal.value  = fuData.value;
         if(FileUploadPath !='') 
         {
            document.getElementById("TDImage").style.display="";
            document.getElementById("TDButtons").style.display="none";                     
         }
      }
    function ValidateFileUpload(Source, args)
     {
       var fuData = document.getElementById('<%= fuTXTN.ClientID %>');
       var FileUploadPath = fuData.value;
     
       if(FileUploadPath =='')
       {
        // There is no file selected
        args.IsValid = false;
       }
       else
       {
         var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
         var filename = FileUploadPath.replace(/^.*\\/, '');         
         if (filename.indexOf( "T_ALL.txt" )>-1)
         {
         
          args.IsValid = true; // Valid file 
         }
         else
         {
          args.IsValid = false; // Not valid file 
          document.getElementById("TDImage").style.display="none";
          document.getElementById("TDButtons").style.display=""; 
         }
       }
     }
     
     function FilePath()
     {
         var fuData = document.getElementById('<%= fuTXTN.ClientID %>');
         document.form1.filepathVal.value  = fuData.value;
        // document.form1.filepathVal.value = fuData.GetNameWithoutExtension()+fuData.GetExtension();
     }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
    <div align="left">
        <table align="center" id="tblImportTXTNs" runat="server" class="FiveHundredPXTable">
            <tr>
                <td colspan="2" class="MainHeader">
                    <asp:Label ID="lblHeader" runat="server" Text="Import Transactions from text file"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 15px">
                </td>
            </tr>
            <tr>
                <td align="center" class="VehicleTD" style="text-align: center;">
                    <asp:FileUpload ID="fuTXTN" runat="server" Style="width: 400px; height: 25px" />
                    <asp:RequiredFieldValidator ID="rfvTXTNupload" runat="server" Height="25px" ControlToValidate="fuTXTN"
                        ErrorMessage="File Required!">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td id="TDButtons" runat="server" class="VehicleTD" style="text-align: center;">
                    <input id="filepathVal" type="hidden" runat="server" style="width: 5px" />
                    <asp:Button ID="btnSubmit" Text="Upload" runat="Server" OnClientClick="DisplayDiv()"
                        Width="100px" Height="35px" />
                </td>
                <td>
                 <asp:Button ID="btnListColumns" runat="server" Text="Click to see Order, Name and Size of Columns to be Import"
                        Height="24px" Font-Names="Verdana" Font-Size="Small" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td id="Td1" runat="server" class="VehicleTD" style="text-align: center">
                    <asp:CustomValidator ID="cvFileUpload" runat="server" ClientValidationFunction="ValidateFileUpload"
                        ErrorMessage="Please select a valid Transaction file."></asp:CustomValidator>
                </td>
            <%--</tr>
             <tr>--%>
                
            </tr>
            <tr>
                <td id="TDImage" class="SearchTable" align="center" style="height: 150px; display: none;
                    width: 181px;">
                    <asp:Image ID="impProcessing" runat="server" ImageUrl="~/images/processing1.gif" />
                </td>
            </tr>
        </table>
        <table align="center" id="tblGridView" runat="server" class="ThousandPXTable">
            <tr>
                <td>
                    <asp:Label ID="lblRowsAfft" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Dept_Grid_TD" align="center" style="height: 414px">
                    <asp:GridView ID="gvImportTXTNs" Width="1010PX" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" DataKeyNames="TXTN" EmptyDataText="No vehicles uploaded in FuelTRAK."
                        Style="border-top-style: ridge; border-right-style: ridge; border-left-style: ridge;
                        border-bottom-style: ridge; border-left-color: black; border-bottom-color: black;
                        border-top-color: black; border-right-color: black;" GridLines="Vertical" PageSize="10"
                        CssClass="Dept_GridView" Visible="false">
                        <FooterStyle CssClass="Dept_GridView_FooterStyle" />
                        <EmptyDataRowStyle CssClass="Dept_GridView_EmptyDataRowStyle" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Sentry">
                                <ItemTemplate>
                                    <asp:Label ID="lblSentry" Text='<%# DataBinder.Eval(Container.DataItem, "Sentry")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="TXTN Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblTXTN" Text='<%# DataBinder.Eval(Container.DataItem, "TXTN")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="450px" ItemStyle-HorizontalAlign="Center" HeaderText="DateTime">
                                <ItemTemplate>
                                    <asp:Label ID="lblDT" Text='<%# DataBinder.Eval(Container.DataItem, "EndDT")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Vehicle">
                                <ItemTemplate>
                                    <asp:Label ID="lblVehID" Text='<%# DataBinder.Eval(Container.DataItem, "Vehicle")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Vkey">
                                <ItemTemplate>
                                    <asp:Label ID="lblVKey" Text='<%# DataBinder.Eval(Container.DataItem, "Vkey")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="VCardID">
                                <ItemTemplate>
                                    <asp:Label ID="lblVCardID" Text='<%# DataBinder.Eval(Container.DataItem, "VCardID")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Personnel">
                                <ItemTemplate>
                                    <asp:Label ID="lblPersonnel" Text='<%# DataBinder.Eval(Container.DataItem, "Personnel")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Pkey">
                                <ItemTemplate>
                                    <asp:Label ID="lblPKey" Text='<%# DataBinder.Eval(Container.DataItem, "Pkey")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Miles">
                                <ItemTemplate>
                                    <asp:Label ID="lblMiles" Text='<%# DataBinder.Eval(Container.DataItem, "Miles")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Pump">
                                <ItemTemplate>
                                    <asp:Label ID="lblPump" Text='<%# DataBinder.Eval(Container.DataItem, "Pump")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Prod">
                                <ItemTemplate>
                                    <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Opt1">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpt1" Text='<%# DataBinder.Eval(Container.DataItem, "Opt1")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Opt2">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpt2" Text='<%# DataBinder.Eval(Container.DataItem, "Opt2")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Opt3">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpt3" Text='<%# DataBinder.Eval(Container.DataItem, "Opt3")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Miles Overriden">
                                <ItemTemplate>
                                    <asp:Label ID="lblMilesOverriden" Text='<%# DataBinder.Eval(Container.DataItem, "MilesOverridden")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="PCardID">
                                <ItemTemplate>
                                    <asp:Label ID="lblPCardID" Text='<%# DataBinder.Eval(Container.DataItem, "PCardID")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="CardID">
                                <ItemTemplate>
                                    <asp:Label ID="lblCardID" Text='<%# DataBinder.Eval(Container.DataItem, "CARD_ID")%>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="white" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                        <PagerStyle BackColor="#a5bbc5" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" />
                        <AlternatingRowStyle BackColor="#fdfeb6" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <%--By Soham Gangavane July 24, 2017--%>
        <div id="PopUpListColumn" runat="server" style="position: absolute; border: solid 3px #00004C;
            background-color: #A5BBC5; top: 150px; left: 200px;">
            <table>
                <tr>
                    <td>
                        <asp:GridView Width="500px" ID="grListOFColumn" runat="server" BackColor="White"
                            BorderColor="black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                            AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true" Visible="true"
                            PageSize="20">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="ColumnOrder" ReadOnly="true" HeaderText="Column Order" > <ItemStyle HorizontalAlign="Center" /> </asp:BoundField>
                                <asp:BoundField DataField="ColumnName" ReadOnly="true" HeaderText="Column Name" />
                                <asp:BoundField DataField="ColumnSize" ReadOnly="true" HeaderText="Column Size" />
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
                    <td align="center" colspan="2">
                        <asp:Button ID="btnProdOK" runat="server" Text="Ok" Height="24px" Width="65px" Font-Names="Verdana"
                            Font-Size="Small" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
