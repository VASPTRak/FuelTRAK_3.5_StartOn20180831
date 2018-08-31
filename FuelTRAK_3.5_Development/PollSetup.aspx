<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PollSetup.aspx.vb" Inherits="PollSetup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Polling Setup</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="javascript/PollSetup.js"></script>

    <script type="text/javascript" language="javascript" src="Javascript/Validation.js"></script>

    <style type="text/css">
        .Auto
        {
            border-right: solid 1px #000000;
            border: 1px solid black;
        }
        .Auto1
        {
            border: 1px solid black;
        }
        .style1
        {
            height: 100px;
            width: 257px;
        }
        .style2
        {
            border: 1px solid black;
            width: 449px;
        }
        .style3
        {
            height: 162px;
        }
        </style>

    <script type="text/javascript" language="javascript">  
     
   
    
            function chksesion()
	    {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	    }

        function KeyUpEvent_txtPollfromdt(e)
        {   var str = document.getElementById('txtPollfromdt').value;
            if(str.length == 2)
            { document.getElementById('txtPollfromdt').value = str + "/";            }
            else if(str.length == 5)
            { document.getElementById('txtPollfromdt').value = str + "/";            }      
        }
        
        
         function Radio_Click()
         
         {
            var radio1 = document.getElementById("<%=rdoUfixed.ClientID %>");
            var textBox = document.getElementById("<%=txtFixedfilename.ClientID %>");
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }
        
        function enableTextBox() 
        {       
            
        if (document.getElementById("chkExecuteBatchProcess").checked == true)
            document.getElementById("txtExportFLocation").disabled = false;
        else
            document.getElementById("txtExportFLocation").disabled = true;
    }


    </script>

</head>
<body onload="EnableAutoPoll(0);" class="HomeFrame">
    <form id="form1" runat="server">
    <div>
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="Polling Setup" Font-Bold="true"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </table>
        
        <table align="center" id="tblImportPers" runat="server" class="EightHundredPXTable">
            <tr>
                <td colspan="2">
                    <table align="left" id="tbl1" class="EightHundredPXTable" style="border-bottom: solid 1px black;
                        margin-left: 100px; border-top: solid 1px black; border-left: solid 1px black;
                        border-right: solid 1px black">
                        <tr>
                            <td align="left" style="height: 40px">
                                <table align="center" style="margin-top: 15px; max-width: 1000px">
                                    <tr>
                                        <td align="right" valign="middle" style="height: 12px; width: 35px">
                                            <input id="chkEnableAutoPoll" runat="server" type="checkbox" onclick="EnableAutoPoll(1);"
                                                tabindex="1" />
                                        </td>
                                        <td align="left" class="StdLableTxtLeft" style="height: 12px; width: 150px">
                                            <asp:Label ID="Label3" runat="server" Text="Enable Auto Poll"></asp:Label>
                                        </td>
                                        <td align="center" valign="middle" style="height: 12px; width: 60px">
                                            <asp:Label ID="Label2" runat="server" Text="Time:"></asp:Label>
                                        </td>
                                        <td align="left" style="height: 22px; width: 50">
                                            <input id="txtTime" runat="server" style="height: 15px; width: 40px" type="text"
                                                maxlength="5" tabindex="2" />
                                        </td>
                                        <td align="center" valign="middle" style="height: 12px; width: 70px">
                                            <asp:Label ID="Label4" runat="server" Text="Days:"></asp:Label>
                                        </td>
                                        <td align="center" style="height: 22px; width: 400px">
                                            <asp:CheckBoxList ID="ChkListDays" runat="server" RepeatColumns="7" RepeatDirection="Horizontal"
                                                Height="70px" TabIndex="3">
                                                <asp:ListItem>Mon</asp:ListItem>
                                                <asp:ListItem>Tue</asp:ListItem>
                                                <asp:ListItem>Wed</asp:ListItem>
                                                <asp:ListItem>Thu</asp:ListItem>
                                                <asp:ListItem>Fri</asp:ListItem>
                                                <asp:ListItem>Sat</asp:ListItem>
                                                <asp:ListItem>Sun</asp:ListItem>
                                                <asp:ListItem>Hourly</asp:ListItem>
                                                <asp:ListItem>Instant</asp:ListItem>
                                                <asp:ListItem>Email</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="text-align: center; height: 35px; vertical-align: bottom">
                                            <input type="button" id="btnSentryAutopoll" onclick="DispPanel(1);" value="Sentrys To Autopoll"
                                                style="width: 170px; height: 30px" tabindex="4" />
                                            <input type="button" id="btnTMAutopoll" onclick="DispPanel(2);" value="Tank Monitors To Autopoll"
                                                style="width: 170px; height: 30px" tabindex="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTime"
                                                Display="Dynamic" ErrorMessage="Invalid time" Font-Names="Verdana" Font-Size="Small"
                                                ToolTip="Time must be in 24 Hours format" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])">Time must be in 24 Hours format</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table align="center" class="EightHundredPXTable">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="left" class="style1">
                                            <asp:CheckBoxList ID="chkPollLst" runat="server" Width="700px" RepeatColumns="3"
                                                RepeatDirection="Horizontal" TabIndex="6" Font-Names="Arial" Font-Size="Smaller">
                                                <asp:ListItem>Poll Twice Daily</asp:ListItem>
                                                <asp:ListItem>Poll Accounts</asp:ListItem>
                                                <asp:ListItem>Send Messages</asp:ListItem>
                                                <asp:ListItem>Send PMs</asp:ListItem>
                                                <asp:ListItem>Poll Manual Override</asp:ListItem>
                                                <asp:ListItem>Re-Poll to Update Records</asp:ListItem>
                                                <asp:ListItem>Poll OBDII Messages</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td align="left" style="height: 125px; top: 20px; width: 357px;">
                                            <asp:RadioButtonList ID="RDBPollLst" runat="server" Width="200px" TabIndex="3" Font-Names="Arial"
                                                Font-Size="Smaller">
                                                <asp:ListItem>Normal Poll</asp:ListItem>
                                                <asp:ListItem>Repoll All Transactions</asp:ListItem>
                                                <asp:ListItem>Repoll from Date</asp:ListItem>
                                            </asp:RadioButtonList>
                                        <%--</td>
                                        <td align="left" style="height: 125px; top: 20px; width: 357px;">--%>
                                            <asp:TextBox ID="txtPollfromdt" CssClass="TenCharTxtBox" MaxLength="10" runat="server"
                                                Style="border-top: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black; margin-left:20px;
                                                border-right: solid 1px black"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPollfromdt"
                                                Display="Dynamic" ErrorMessage="Invalid repoll from date." Font-Names="Verdana"
                                                Font-Size="Small" ToolTip="Repoll from date must be in MM/DD/YYYY format." ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">Repoll from date must be in MM/DD/YYYY format.</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <table class="VehicleEditTableSub1">
                        <tr>
                            <td align="center" colspan="2" style="height: 20px">
                                <input id="chkAutoExport" runat="server" type="checkbox" onclick="EnableAutoExport(1);"
                                    tabindex="1" />
                                <asp:Label ID="lblAutoExport" runat="server" Text="Enable Auto Export" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="Smaller" Style="align: center;" />
                                <asp:Label ID="lblAutoexportTime" runat="server" Text=" At This Time:" Font-Names="Arial"
                                    Font-Size="Smaller" Font-Bold="True" />
                                <input id="txtAutoExportTime" runat="server" style="height: 15px; width: 40px" type="text"
                                    maxlength="5" tabindex="2" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 0.5px">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAutoExportTime"
                                    Display="Dynamic" ErrorMessage="Invalid time" Font-Names="Verdana" Font-Size="Small"
                                    ToolTip="Time must be in 24 Hours format" ValidationExpression="((?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9])">Time must be in 24 Hours format</asp:RegularExpressionValidator>
                                <%-- <hr style="color: #000000;" /> --%>
                            </td>
                        </tr>
                        <tr>
                           <td style="width: 550px" class="Auto" >
                                <table>
                                    <tr>
                                        <td align="left" style="width: 400px">
                                            <asp:RadioButton ID="rdoUDFName" GroupName="GRP" runat="server" Style="align: left"
                                                Text="Use Date As File Name" onclick="Radio_Click()" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="Smaller" />
                                            <%--<asp:Label ID="lblUseDateAsfilename" runat="server" Text="Use Date As File Name"
                                                                Style="align: left;" Font-Bold="True" Font-Names="Arial" Font-Size="Smaller"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:RadioButton ID="rdoAutoIncrmnt" GroupName="GRP" runat="server" Style="align: left"
                                                Text="Use Auto Increment File Name" Font-Bold="True" Font-Names="Arial" Font-Size="Smaller"
                                                onclick="Radio_Click()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 414px">
                                            <asp:RadioButton ID="rdoUfixed" GroupName="GRP" runat="server" Style="align: left"
                                                Text="Use Fixed File Name" Font-Bold="True" Font-Names="Arial" Font-Size="Smaller"
                                                onclick="Radio_Click()" />
                                            <asp:Label ID="Label10" runat="server" Text="File Name:" Style="margin-left: 20px;"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Smaller"></asp:Label>
                                            <asp:TextBox ID="txtFixedfilename" runat="server" Width="128px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 550px">
                                            &nbsp;
                                            <asp:Label ID="lblExportFExtn" runat="server" Text="Export File Extension:" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="Smaller"></asp:Label>
                                            <asp:TextBox ID="txtExportFileExtn" runat="server" Width="45px"></asp:TextBox>
                                            &nbsp;
                                            <asp:Label ID="lblExportFLocation" runat="server" Text="Export File Location:" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="Smaller"></asp:Label>
                                            <asp:TextBox ID="txtExportFLocation" runat="server" Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style2">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <input id="chkExecuteBatchProcess" runat="server" type="checkbox" tabindex="1" onchange="javascript:enableTextBox();" />
                                            <asp:Label ID="lblZeroQtyTransaction" runat="server" Text="Execute Batch Process After Auto Export"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Smaller" />
                                        </td>
                                        
                                    </tr>
                                    <tr></tr><tr></tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblBatchProcessFNmae" runat="server" Text="Batch Process File Name:"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Smaller"></asp:Label>
                                            <asp:TextBox ID="txtBatchProcessFileName" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblBatchFileLocatiion" runat="server" Text="Batch File Location:"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Smaller"></asp:Label>
                                            <asp:TextBox ID="txtBatchFileLocation" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width: 1000px">
                                <tr>
                                 <td align="left" valign="top" style="height: 22px; width:500px">
                                <table>
                                    <tr>
                                        <td align="left" valign="top" style="width: 300px; height: 22px;">
                                            <input id="chkIncPrevExprtTxtn" runat="server" type="checkbox" tabindex="1" />
                                            <asp:Label ID="lblprevTxtn" runat="server" Text="Include Previously Exported Transactions"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Smaller" />
                                        </td>
                                         </tr>
                                          <tr>
                                        <td align="left" valign="top" style="width: 300px; height: 22px;">
                                            <input id="chkIncZeroQtyTxtn" runat="server" type="checkbox" tabindex="1" />
                                            <asp:Label ID="lblzeroqtytxtn" runat="server" Text="Include Zero Qty.transactions"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Smaller" />
                                        </td>
                                         </tr>
                                          <tr>
                                         <td align="left" valign="top" style="width: 300px; height: 22px;">
                                            <input id="chkAppendtoPrevExprtFile" runat="server" type="checkbox" tabindex="1" />
                                            <asp:Label ID="lblAppendtoFile" runat="server" Text="Append to previously created Export File"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Smaller" />
                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                                    <td align="left" valign="top" style="height: 22px">
                                            <asp:RadioButtonList ID="rdbExportSelection" runat="server" Font-Bold=true RepeatColumns="1"> 
                                                <asp:ListItem Text="Export 1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Export 2" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr></tr> <tr></tr><tr></tr> <tr></tr><tr></tr> <tr></tr>
           
            <tr >
                <td>
                    <table style="width: 1000px;">
                        <tr>
                            <%-- <td align="left" style="height: 26px; vertical-align: bottom; width: 200px;">
                                        <asp:Label ID="lblVeh" runat="server" Text="Vehicles" Font-Bold="True" Font-Names="Arial"></asp:Label>
                                    </td>
                                    <td align="left" style="height: 26px; vertical-align: bottom; width: 200px;">
                                        <asp:Label ID="lblPersonnel" runat="server" Text="Personnel" Font-Bold="True" Font-Names="Arial"></asp:Label>
                                    </td>
                                      <td align="left" style="height: 21px; vertical-align: bottom; width: 200px;">
                                        <asp:Label ID="Label6" runat="server" Text="Lockouts" Font-Bold="True" Font-Names="Arial"></asp:Label>
                                    </td>--%>
                            <td>
                                <asp:Label ID="lblVeh" runat="server" Text="Vehicles" Font-Bold="True" Font-Names="Arial"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPersonnel" runat="server" Text="Personnel" Font-Bold="True" Font-Names="Arial"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Lockouts" Font-Bold="True" Font-Names="Arial"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 1045px;">
                        <tr>
                            <td style="max-width: 150px">
                                <table class="VehicleEditTableSub" runat="server" id="tblVeh" style="margin-top: 0px">
                                    <tr>
                                        <td align="left">
                                            <asp:CheckBoxList ID="chkVehLst" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                TabIndex="8" Font-Names="Arial" Font-Size="Smaller">
                                                <asp:ListItem>Send All Vehicle Info</asp:ListItem>
                                                <asp:ListItem>Send New,Deleted &amp; Updated Vehicle Info</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="max-width: 150px">
                                <table class="VehicleEditTableSub" runat="server" id="tblPersonnel" style="margin-top:0px">
                                    <tr>
                                        <td align="left">
                                            <asp:CheckBoxList ID="chkPersonnelLst" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                TabIndex="9" Font-Names="Arial" Font-Size="Smaller">
                                                <asp:ListItem>Send All Personnel Info</asp:ListItem>
                                                <asp:ListItem>Send New &amp; Deleted Personnel Info</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="max-width: 150px">
                                <table class="VehicleEditTableSub" runat="server" id="tblLock" style="margin-top: 0px">
                                    <tr>
                                        <td align="left" style="height: 28px">
                                            <asp:CheckBoxList ID="chkLockoutLst" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                TabIndex="10" Font-Names="Arial" Font-Size="Smaller">
                                                <asp:ListItem>Send Key Lockouts</asp:ListItem>
                                                <asp:ListItem>Send Card Lockouts</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 80px; width: 357px;">
                    <asp:Button ID="btnOk" runat="server" Text="Ok" Width="100px" TabIndex="11" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="true" Width="100px"
                        TabIndex="12" />
                </td>
            </tr>
       <%-- </table>
        
        <table>--%>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="SentryInputPanel" runat="server" Style="position: absolute; left: -1000px;
                        background-color: #C0C0C0; top: 50; white-space: nowrap; z-index: 101; border-right: #000000 1px solid;
                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                        <table>
                            <tr>
                                <td class="ImportPers_Header">
                                    <asp:Label ID="Label8" runat="server" Text="Pick the Sentrys to be Auto Polled"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 293px">
                                    <div style="overflow-y: scroll; height: 250px; top: 50; border-right: #000000 1px solid;
                                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                                        <asp:GridView ID="GrdSentry" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#CCCCCC" BorderWidth="1px" EmptyDataText="0 records found." CellPadding="1"
                                            CssClass="th" DataKeyNames="NUMBER" PagerStyle-CssClass="note">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" CssClass="note" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#000084" Font-Names="Arial" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="AutoPoll">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSentrySelectAll" runat="server" Text="Select All" />
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Sentrychk1" Checked='<%# DataBinder.Eval( Container.DataItem, "Poll") %>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NUMBER" HeaderText="Number" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="MasterName">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid1" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" value="Close" onclick="HidePanel(1);" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="TMInputPanel" runat="server" Style="position: absolute; left: -1000px;
                        background-color: #C0C0C0; white-space: nowrap; z-index: 101; top: 50; border-right: #000000 1px solid;
                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                        <table>
                            <tr>
                                <td class="ImportPers_Header">
                                    <asp:Label ID="Label9" runat="server" Text="Pick the Tank Monitor to be Auto Polled"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="overflow-y: scroll; height: 250px; top: 50; border-right: #000000 1px solid;
                                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                                        <asp:GridView ID="GrdTM" runat="server" EmptyDataText="0 records found." AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="1" CssClass="th"
                                            DataKeyNames="NUMBER" PagerStyle-CssClass="note">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" CssClass="note" ForeColor="#000066" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="AutoPoll">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkTMSelectAll" runat="server" Text="Select All" />
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                    <HeaderStyle CssClass="Header_Grid" Font-Names="Arial" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="TMchk1" Checked='<%# DataBinder.Eval( Container.DataItem, "Poll") %>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Poll" HeaderText="POLL">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Number" HeaderText="Number">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid1" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#000084" Font-Names="Arial" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" value="Close" onclick="HidePanel(2);" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 26px">
                    <asp:Panel ID="panel" runat="server" Style="position: absolute; left: -1000px; background-color: #C0C0C0;
                        top: 50; white-space: nowrap; z-index: 101; border-right: #000000 1px solid;
                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                        <table>
                            <tr>
                                <td class="ImportPers_Header">
                                    <asp:Label ID="Label5" runat="server" Text="Pick the Sentrys to be Auto Polled"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 293px">
                                    <div style="overflow-y: scroll; height: 250px; top: 50; border-right: #000000 1px solid;
                                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#CCCCCC" BorderWidth="1px" EmptyDataText="0 records found." CellPadding="1"
                                            CssClass="th" DataKeyNames="NUMBER" PagerStyle-CssClass="note">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" CssClass="note" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#000084" Font-Names="Arial" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="AutoPoll">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSentrySelectAll" runat="server" Text="Select All" />
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Sentrychk1" Checked='<%# DataBinder.Eval( Container.DataItem, "Poll") %>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NUMBER" HeaderText="Number" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="MasterName">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid1" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" value="Close" onclick="HidePanel(1);" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Style="position: absolute; left: -1000px; background-color: #C0C0C0;
                        white-space: nowrap; z-index: 101; top: 50; border-right: #000000 1px solid;
                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                        <table>
                            <tr>
                                <td class="ImportPers_Header">
                                    <asp:Label ID="Label7" runat="server" Text="Pick the Tank Monitor to be Auto Polled"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="overflow-y: scroll; height: 250px; top: 50; border-right: #000000 1px solid;
                                        border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid">
                                        <asp:GridView ID="GridView2" runat="server" EmptyDataText="0 records found." AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="1" CssClass="th"
                                            DataKeyNames="NUMBER" PagerStyle-CssClass="note">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" CssClass="note" ForeColor="#000066" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="AutoPoll">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkTMSelectAll" runat="server" Text="Select All" />
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                    <HeaderStyle CssClass="Header_Grid" Font-Names="Arial" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="TMchk1" Checked='<%# DataBinder.Eval( Container.DataItem, "Poll") %>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Poll" HeaderText="POLL">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Number" HeaderText="Number">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name">
                                                    <HeaderStyle CssClass="Header_Grid" />
                                                    <ItemStyle CssClass="ItemStyle_Grid1" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#000084" Font-Names="Arial" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" value="Close" onclick="HidePanel(2);" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <p>
                        <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal></p>
                    <p>
                        <asp:Literal ID="CheckBoxIDsArraySentry" runat="server"></asp:Literal></p>
                </td>
            </tr>
            <%-- <tr> <td> <asp:Panel runat ="Server"  ID="testOanel">
                <asp:Button ID ="btnTest" runat="server" Text="TEST" />
                </asp:Panel>
                </td> </tr>--%>
        </table>
    </div>
    </form>
</body>
</html>
