<%@ Page Language="VB" AutoEventWireup="true" CodeFile="TankInventory.aspx.vb" Inherits="InventoryEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tank Inventory</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="Javascript/DateTime.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/calendar2.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/tankInventory.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>
    
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
     <script src="Javascript/jquery-ui.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
    
    function check(VehId)
        {
            var ac=confirm('Are you sure you want to permanently delete this record ?');
            document.form1.Hidtxt.value=ac;
            document.form1.txtVehId.value=VehId;
            form1.submit(); 
        }
        
  function HideControls(i)
    {   if(i == 13)
        {   document.getElementById('btnSearchshow').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
        }
       else if(i == 1)
        {   
            document.getElementById('tblSearch').style.visibility = "hidden";
        }
        else if(i == 2)
        {   document.getElementById('tblSearch').style.visibility = "hidden"; }
        else if(i == 3)
        {   document.getElementById('tblSearch').style.visibility = "visible";
            //document.getElementById('txtType').style.visibility = "hidden"
            document.getElementById('btnSearch').focus();
            
            document.getElementById('btnSearchshow').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
        }
        else if(i == 4)
        {   document.getElementById('tblSearch').style.visibility = "visible";
            document.getElementById('btnSearchshow').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
        }
    }
    
     function ShowControls()
     {      document.getElementById('tblSearch').style.visibility = "visible";     }

    function EnableDisable(i)
    {   
        if(i == 1)//Tank Type
        {
            //document.getElementById('txtType').style.visibility = "hidden";
            document.getElementById('DDLstType').style.visibility = "visible";
            document.getElementById('DDLstType').focus();
            return true;
        }
        else if(i == 2)//Tank Id
        {
            document.getElementById('txtTank').value = "";
            //document.getElementById('txtType').style.visibility = "hidden";
            document.getElementById('txtTank').style.visibility = "visible";
            document.getElementById('txtTank').focus();
            return true;
        }
        else if(i == 3)//Date
        {
            document.getElementById('txtDate').value = "";
           // document.getElementById('txtType').style.visibility = "hidden";
            document.getElementById('txtDate').style.visibility = "visible";
            document.getElementById('txtDate').focus();
            return true;
        }
        else if(i == 4)//Tank Name
        {
            document.getElementById('txtTankName').value="";
           // document.getElementById('txtType').style.visibility = "hidden";
            document.getElementById('txtTankName').style.visibility = "visible";
            document.getElementById('txtTankName').focus();
            return true;
        }
    }

    
    function Opn()
    {   document.forms[0].
        //window.open("TankInventory_New_Edit.aspx","","","replace");
        window.location.replace("TankInventory_New_Edit.aspx?Test=Test");
    }
    
    function KeyUpEvent_txtDate(e)
    {
      if (window.event.keyCode < 48 || window.event.keyCode > 57)
        { window.event.keyCode = 0;        }
    
        var str = document.getElementById('txtDate').value;
        
        if(str.length == 2)
        {             document.getElementById('txtDate').value = str + "/";        }
        else if(str.length == 5)
        {             document.getElementById('txtDate').value = str + "/";        }
         if(str.length == 10)
        {        
            if(ValidateDate(str))
            {                document.getElementById('txtDate').focus();           }
            else
            {                document.getElementById('btnSearch').focus();         }               
         }      
     }
     
     function check()
	 {
		    var ac = confirm('Are you sure you want to delete this entry ?');
			document.getElementById('Hidtxt').value = ac;
			document.form1.submit(); 
			
	 }
	 
	  $(function () {      
        $('#txtTankName').keydown(function (e) {
       
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
            <table class="EightHundredPXTable" align="center">
                <tr>
                    <td align="center" valign="middle">
                        <table id="tblSelect" class="EightHundredPXTable">
                            <tr>
                                <td class="MainHeader" style="height: 50px">
                                    <asp:Label ID="Label1" runat="server" Text="Search / Edit Tank Inventory"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TankInv_Header_Button">
                                    <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" />
                                    <input id="btnSearchshow" type="button" value="Search" onclick="HideControls(3)"
                                        class="VehicleEditBtn" />
                                    <input type="hidden" id="Hidtxt" runat="server" style="width: 10px" />
                                    <input type="hidden" id="txtEntryID" runat="server" style="width: 10px" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table id="tblSearch" class="SevenHundredPXTable">
                                        <tr>
                                            <td class="HeaderSearchCriteria" colspan="2">
                                                <asp:Label ID="Label8" runat="server" Text="Please Enter Your Search Criteria and Select Search:"></asp:Label>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                    Width="100px" TabIndex="6" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <table align="center" class="ThreeHundredPXTable">
                                                    <tr>
                                                        <td class="StdLableTxtLeft" style="width: 150px; height: 32px">
                                                            <asp:Label ID="Label2" runat="server" BackColor="Transparent" Text="Type:"></asp:Label></td>
                                                        <td style="text-align: left; width: 150px">
                                                            <asp:DropDownList ID="DDLstType" runat="server" CssClass="TwentyCharTxtBox" Font-Names="Verdana"
                                                                ValidationGroup="50" TabIndex="1">
                                                                <asp:ListItem Value=" ">Select Type</asp:ListItem>
                                                                <asp:ListItem Value="R">Fuel Delivery</asp:ListItem>
                                                                <asp:ListItem Value="D">Tank Dipping</asp:ListItem>
                                                                <asp:ListItem Value="S">Tank Setting</asp:ListItem>
                                                                <asp:ListItem Value="L">Tank Level</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtType" runat="server" Visible="False" Width="1px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StdLableTxtLeft" style="height: 32px">
                                                            <asp:Label ID="Label3" runat="server" Text="Date:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="TenCharTxtBox" MaxLength="10" TabIndex="2"></asp:TextBox>
                                                            <img id="StartDateImage" alt="Start Date" onclick="Javascript:cal1.popup()" runat="server"
                                                                src="images/cal.gif" style="cursor: hand" tabindex="3" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate"
                                                                Display="Dynamic" ErrorMessage="Invalid from date; valid date format (MM/dd/yyyy)."
                                                                Font-Names="Verdana" Font-Size="Small" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StdLableTxtLeft" style="height: 32px">
                                                            <asp:Label ID="Label4" runat="server" Text="Tank No.:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtTank" runat="server" CssClass="ThreeCharTxtBox" MaxLength="3" TabIndex="4"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StdLableTxtLeft" style="height: 32px">
                                                            <asp:Label ID="Label5" runat="server" Text="Tank Name:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtTankName" runat="server" CssClass="TwentyCharTxtBox" MaxLength="20" TabIndex="5"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Names="Verdana"
                                                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" colspan="2" class="TankGrid">
                                                <asp:GridView ID="GridView1" runat="server" Width="700px"  AllowPaging="true" AutoGenerateColumns="False"
                                                    DataKeyNames="RECORD" EmptyDataText="0 records found for selected search criteria"
                                                    PageSize="10" BorderColor="black" AllowSorting="true" BorderStyle="solid" BorderWidth="1px"
                                                    BackColor="White" CellPadding="3" GridLines="Vertical">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField SortExpression="Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="Tank">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="ENTRY_TYPE" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="80px" HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "ENTRY_TYPE")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="DATETIME" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTIME" Text='<%# DataBinder.Eval(Container.DataItem, "DATETIME","{0:MM/dd/yyyy}")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="DATETIME" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="TIME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTIMEs" Text='<%# DataBinder.Eval(Container.DataItem, "DATETIME","{0:T}")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="QTY_MEAS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                            HeaderText="Qty Meas.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQTYMEAS" Text='<%# DataBinder.Eval(Container.DataItem, "QTY_MEAS")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="QTY_ADDED" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="80px" HeaderText="Qty Add.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQtyAdd" Text='<%# DataBinder.Eval(Container.DataItem, "QTY_ADDED")%>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#a5bbc5" ForeColor="Black" />
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <RowStyle BackColor="white" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#fdfeb6" Font-Bold="True" ForeColor="black" />
                                                    <HeaderStyle BackColor="#a5bbc5" Font-Bold="True" ForeColor="black" />
                                                    <AlternatingRowStyle BackColor="#fdfeb6" />
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
    </form>

    <script type="text/javascript" language="javascript">				
    //if (document.getElementById('StartDateTextBox').style.visibility = "visible")
    
     if (document.getElementById('txtDate'))
    {
	    var cal1 = new calendar2(document.getElementById('txtDate'));
	    cal1.year_scroll = true;
	    cal1.time_comp = false;
	}
				
    </script>

</body>
</html>
