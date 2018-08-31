<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Vehicle_Import.aspx.vb"
    Inherits="Vehicle_Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Vehicle Import</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script type="text/javascript">
     function chksesion()
	  {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	  }
    function DisplayDiv()
     {
         var fuData = document.getElementById('<%= fuVehs.ClientID %>');
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
       var fuData = document.getElementById('<%= fuVehs.ClientID %>');
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
         
         if (filename == "TrakTagConfigurations.txt")
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
         var fuData = document.getElementById('<%= fuVehs.ClientID %>');
         document.form1.filepathVal.value  = fuData.value;
     }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div align="left">
            <table align="center" id="tblImportVehs" runat="server" class="FiveHundredPXTable">
                <tr>
                    <td colspan="2" class="MainHeader">
                        <asp:Label ID="lblHeader" runat="server" Text="Import Vehicles from TRAK Tag file"></asp:Label></td>
                </tr>
                <tr>
                    <td style="height: 15px">
                    </td>
                </tr>
                <tr>
                    <td align="center" class="VehicleTD" style="text-align: center;">
                        <asp:FileUpload ID="fuVehs" runat="server" Style="width: 400px; height: 25px" />
                        <asp:RequiredFieldValidator ID="rfvVehsupload" runat="server" Height="25px" ControlToValidate="fuVehs"
                            ErrorMessage="File Required!">
                        </asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td id="TDButtons" runat="server" class="VehicleTD" style="text-align: center;">
                        <input id="filepathVal" type="hidden" runat="server" style="width: 5px" />
                        <asp:Button ID="btnSubmit" Text="Upload" runat="Server" OnClientClick="DisplayDiv()"
                            Width="100px" Height="35px" />
                    </td>
                </tr>
                <tr>
                    <td id="Td1" runat="server" class="VehicleTD" style="text-align: left">
                        <asp:CustomValidator ID="cvFileUpload" runat="server" ClientValidationFunction="ValidateFileUpload"
                            ErrorMessage="Please select a valid TrakTagConfigurations.txt file"></asp:CustomValidator></td>
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
                    <td class="Dept_Grid_TD" align="center">
                        <asp:GridView ID="gvImportVehs" Width="900PX" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="Sys_Nbr" EmptyDataText="No vehicles uploaded in FuelTRAK." Style="border-top-style: ridge;
                            border-right-style: ridge; border-left-style: ridge; border-bottom-style: ridge;
                            border-left-color: black; border-bottom-color: black; border-top-color: black;
                            border-right-color: black;" GridLines="Vertical" PageSize="30" CssClass="Dept_GridView"
                            Visible="false">
                            <FooterStyle CssClass="Dept_GridView_FooterStyle" />
                            <EmptyDataRowStyle CssClass="Dept_GridView_EmptyDataRowStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" HeaderText="Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT" Text='<%# DataBinder.Eval(Container.DataItem, "DT")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left" HeaderText="Vehicle ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVehID" Text='<%# DataBinder.Eval(Container.DataItem, "Tag_ID")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Miles">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMiles" Text='<%# DataBinder.Eval(Container.DataItem, "Miles")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Hours">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHours" Text='<%# DataBinder.Eval(Container.DataItem, "Hours")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Limit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLimit" Text='<%# DataBinder.Eval(Container.DataItem, "Limit")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Fuels">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFuels" Text='<%# DataBinder.Eval(Container.DataItem, "Fuels")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Power">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPower" Text='<%# DataBinder.Eval(Container.DataItem, "Power")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Clicks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClicks" Text='<%# DataBinder.Eval(Container.DataItem, "Clicks")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Tag Mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTagMode" Text='<%# DataBinder.Eval(Container.DataItem, "Tag_Mode")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" HeaderText="Ignition State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIgntStat" Text='<%# DataBinder.Eval(Container.DataItem, "Igtn_Stat_det")%>'
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
        </div>
    </form>
</body>
</html>
