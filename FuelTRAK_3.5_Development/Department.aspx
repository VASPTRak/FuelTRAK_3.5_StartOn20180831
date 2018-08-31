<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Department.aspx.vb" Inherits="Department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Department</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/Department.js"></script>
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
     <script src="Javascript/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript" src="Javascript/Validation.js"></script>

    <script language="javascript">
   
     function chksesion()
	 {
	    alert("Session expired, Please login again.");
	    window.location.assign('LoginPage.aspx')
	 }
     function check(VehId)
     {
        var ac=confirm('Are you sure you want to permanently delete this record ?');
        document.form1.Hidtxt.value=ac;//
        document.form1.txtVehId.value=VehId;
        form1.submit(); 
     }
     
      $(function () {      
        $('#txtDepartmentName').keydown(function (e) {
       
        if (e.shiftKey || e.ctrlKey || e.altKey) {
        e.preventDefault();
        } 
        else
        {        
          var key = e.keyCode;          
          if (( (key == 186) || (key == 222) )) {
            e.preventDefault();
        }
      }
     });
    });

    </script>
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" id="tblSelect" class="SixHundredPXTable">
                <tr>
                    <td colspan="4" class="MainHeader">
                        <asp:Label ID="Label1" runat="server" Text="Search / Edit Department"></asp:Label></td>
                </tr>
            </table>
            <table align="center" class="SixHundredPXTable">
                <tr>
                    <td colspan="4" class="Dept_Header_Button">
                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="Dept_Buttons" OnClick="btnNew_Click"
                            UseSubmitBehavior="False" Visible="false" />
                        <input id="btnSearchshow" type="button" value="Search" visible="false" onclick="HideControls(3)"
                            class="Dept_Buttons" /></td>
                </tr>
                <tr>
                    <td class="HeaderSearchCriteria" style="height: 50px" colspan="4" id="tblSearch">
                        <asp:Label ID="Label8" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Dept_Buttons1"
                            OnClick="btnSearch_Click" TabIndex="3" /></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 32px">
                    </td>
                    <td class="StdLableTxtLeft" style="width: 150px">
                        <asp:Label ID="Label19" Text="Department Name:" runat="server"></asp:Label></td>
                    <td style="text-align: left; width: 150px">
                        <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="TwentyCharTxtBox" TabIndex="1"></asp:TextBox></td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label ID="Label20" Text="Department Number:" runat="server"></asp:Label></td>
                    <td align="left" style="height: 32px">
                        <asp:TextBox ID="txtDepartmentNumber" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3"
                            TabIndex="2"></asp:TextBox></td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" id="tdsearch" runat="server" visible="false" class="Dept_Header">
                        <asp:Label Text="Search Results:" runat="server" ID="lblresult"></asp:Label></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td class="Dept_Grid_TD" align="center">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="true" AllowPaging="True"
                            AutoGenerateColumns="False" DataKeyNames="DeptId" EmptyDataText="0 records found for selected search criteria"
                            Style="border-top-style: ridge; border-right-style: ridge; border-left-style: ridge;
                            border-bottom-style: ridge; border-left-color: black; border-bottom-color: black;
                            border-top-color: black; border-right-color: black;" GridLines="Vertical" PageSize="20"
                            CssClass="Dept_GridView">
                            <FooterStyle CssClass="Dept_GridView_FooterStyle" />
                            <EmptyDataRowStyle CssClass="Dept_GridView_EmptyDataRowStyle" />
                            <Columns>
                                <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                    <HeaderStyle Font-Names="Arial" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                        Width="70px" />
                                    <ItemStyle Font-Names="Arial" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                        Width="70px" />
                                </asp:CommandField>
                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                    <HeaderStyle Font-Names="Arial" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                        Width="70px" />
                                    <ItemStyle Font-Names="Arial" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                        Width="70px" />
                                </asp:CommandField>
                                <asp:TemplateField ItemStyle-Width="160px" SortExpression="NUMBER" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Department ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNUMBER" Text='<%# DataBinder.Eval(Container.DataItem, "NUMBER")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="300px" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
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
            <input type="hidden" id="txtVehId" runat="server" /><input type="hidden" id="Hidtxt"
                runat="server" />
        </div>
    </form>
</body>
</html>
