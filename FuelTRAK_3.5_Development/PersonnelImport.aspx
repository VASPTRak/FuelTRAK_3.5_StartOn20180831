<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PersonnelImport.aspx.vb"
    Inherits="PersonnelImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personnel Import</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">

     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

//            function HideCols()
//            {
//                if (document.getElementById('ddlSelectType').value=='CSV')//ddlSelectType
//                {   
//                    document.getElementById('TDFixed1').style.display='none'
//                    document.getElementById('TDFixed2').style.display='none'
//                    document.getElementById('TDFixed3').style.display='none'
//                    document.getElementById('TDFixed4').style.display='none'
//                    document.getElementById('TDCSV1').style.display=''
//                    document.getElementById('TDCSV2').style.display=''
//                    document.getElementById('TDCSV3').style.display=''
//                    
//                }
//                else
//                {   
//                    document.getElementById('TDCSV1').style.display='none'
//                    document.getElementById('TDCSV2').style.display='none'
//                    document.getElementById('TDCSV3').style.display='none'
//                    document.getElementById('TDFixed1').style.display=''
//                    document.getElementById('TDFixed2').style.display=''
//                    document.getElementById('TDFixed3').style.display=''
//                    document.getElementById('TDFixed4').style.display=''
//                }
//            }
            
//            function DefaultHideCols()
//            {
//                document.getElementById('TDFixed1').style.display='none'
//                document.getElementById('TDFixed2').style.display='none'
//                document.getElementById('TDFixed3').style.display='none'
//                document.getElementById('TDFixed4').style.display='none'
//                document.getElementById('TDCSV1').style.display=''
//                document.getElementById('TDCSV2').style.display=''
//                document.getElementById('TDCSV3').style.display=''
//                for(var i=1; i<=14; i++) 
//                {
//	                document.getElementById('CSV'+i).disabled=true;
//                }
//                
//            }
            function VisibleTextBox(ChkIndex)
            {
           
            
                if (document.getElementById('DropFileType').value=='CSV')
                {
                    if (document.getElementById('chk'+ ChkIndex ).checked == true )
                    {                    
                        document.getElementById('CSV'+ ChkIndex ).value='';
                        document.getElementById('CSV'+ ChkIndex ).focus();}
                    else
                    { document.getElementById('CSV'+ ChkIndex ).value='';
                    }
                 }
                else
                {
                    if (document.getElementById('chk'+ ChkIndex ).checked == true )
                    {
                        document.getElementById('FixedS'+ ChkIndex ).value='';
                        document.getElementById('FixedL'+ ChkIndex ).value='';
                        document.getElementById('FixedS'+ ChkIndex ).focus();
                    }
                    else
                    {
                        document.getElementById('FixedS'+ ChkIndex ).value='';
                        document.getElementById('FixedL'+ ChkIndex ).value='';
                    }
                }
            }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table align="center" class="SixHundredPXTable">
                <tr>
                    <td valign="top">
                        <table id="tblImportPers" runat="server" class="SixHundredPXTable">
                            <tr>
                                <td class="MainHeader" colspan="2">
                                    <asp:Label ID="Label1" runat="server" Text="Personnel Import Screen"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2"><br /></td>
                            </tr>
                            <tr>
                                <td class="ImportPers_Lable_Left" colspan="2" style="height: 24px; text-align:center; vertical-align: middle">
                                    <asp:Label ID="lblFileType" runat="server" Text="File Type:"></asp:Label>
                                    &nbsp;<asp:DropDownList ID="DropFileType" runat="server" AutoPostBack="True">
                                        <asp:ListItem>CSV</asp:ListItem>
                                        <asp:ListItem>FIXED</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <table class="FiveHundredPXTable">
                                        <tr>
                                            <td class="ImportPers_Lable_Felds_Header1" rowspan="2" style="width:200px">
                                                <asp:Label ID="Label2" runat="server" Text="Fields"></asp:Label></td>
                                            <td class="ImportPers_Lable_Felds_Header" rowspan="2" style="width:225px"></td>
                                            <td id="TDCSV1" runat="server" style="width:80px">
                                                <asp:Label ID="Label4" runat="server" Text="CSV"></asp:Label></td>
                                            <td id="TDFixed1" runat="server" colspan="2">
                                                <asp:Label ID="Label5" runat="server" Text="Fixed"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td id="TDCSV2" runat="server">
                                                <asp:Label ID="Label7" runat="server" Text="Field #"></asp:Label></td>
                                            <td id="TDFixed2" runat="server" class="ImportPers_Lable_Felds_Header">
                                                <asp:Label ID="Label9" runat="server" Text="Start Pos" ></asp:Label></td>
                                            <td id="TDFixed3" runat="server" class="ImportPers_Lable_Felds_Header">
                                                <asp:Label ID="Label6" runat="server" Text="Length"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td rowspan="14" valign="top" class="ImportPers_Lable_Felds_Header1">
                                                <table style="height: 392px">
                                                    <tr>
                                                        <td style="width:150px">
                                                            <asp:Label ID="Label8" runat="server" Text="PIN"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Last Name" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" Text="First Name"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="Middle Initial"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" Text="Account ID"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label25" runat="server" Text="Department Number" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label28" runat="server" Text="Type"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label31" runat="server" Text="Key Number"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label34" runat="server" Text="Key Exp."></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label37" runat="server" Text="Card ID"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label40" runat="server" Text="Card Exp."></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label43" runat="server" Text="Require ID Entry" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label46" runat="server" Text="Locked" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label49" runat="server" Text="Gate Security Level"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" rowspan="14" class="ImportPers_Lable_Felds_Header" >
                                                <table>
                                                    <tr>
                                                        <td style="width:200px">
                                                            <input id="chk1" type="checkbox" runat="server" onclick="VisibleTextBox(1)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid1" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk2" type="checkbox" runat="server" onclick="VisibleTextBox(2)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid2" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk3" type="checkbox" runat="server" onclick="VisibleTextBox(3)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid3" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk4" type="checkbox" runat="server" onclick="VisibleTextBox(4)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid4" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk5" type="checkbox" runat="server" onclick="VisibleTextBox(5)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid5" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk6" type="checkbox" runat="server" onclick="VisibleTextBox(6)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid6" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk7" type="checkbox" runat="server" onclick="VisibleTextBox(7)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid7" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="Chk8" type="checkbox" runat="server" onclick="VisibleTextBox(8)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid8" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <input id="chk9" type="checkbox" runat="server" onclick="VisibleTextBox(9)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid9" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk10" type="checkbox" runat="server" onclick="VisibleTextBox(10)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid10" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk11" type="checkbox" runat="server" onclick="VisibleTextBox(11)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid11" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk12" type="checkbox" runat="server" onclick="VisibleTextBox(12)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid12" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk13" type="checkbox" runat="server" onclick="VisibleTextBox(13)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid13" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input id="chk14" type="checkbox" runat="server" onclick="VisibleTextBox(14)" />
                                                            <input type="hidden" runat="server" style="width:85px" id="Hid14" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td rowspan="14" valign="top" runat="server" id="TDCSV3">
                                                <table>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV1" runat="server" CssClass="FiveCharTxtBox" ></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV2" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV3" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV4" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV5" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV6" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV7" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV8" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV9" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV10" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV11" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV12" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV13" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <asp:TextBox ID="CSV14" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td rowspan="14"  runat="server" id="TDFixed4" colspan="2" class="ImportPers_Lable_Felds_Header">
                                                <table>
                                                    <tr>
                                                        <td style="width:80px">
                                                            <asp:TextBox ID="FixedS1" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td style="width:80px">
                                                            <asp:TextBox ID="FixedL1" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS2" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL2" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS3" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL3" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS4" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL4" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS5" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL5" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS6" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL6" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS7" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL7" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS8" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL8" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS9" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL9" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS10" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL10" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS11" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL11" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS12" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL12" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS13" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL13" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="FixedS14" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="FixedL14" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>


                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>

                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>

                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>

                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>

                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>

                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>

                                            
                                            <%-- <td rowspan="14" colspan="2" class="ImportPers_Lable_Felds_Header" >
                                                <asp:Label ID="Label13" runat="server" Text="Fields"></asp:Label>
                                            </td>--%>

                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle" >
                                   <input id="chkDel1" runat="server" type="checkbox"/></td>
                                <td align="left" class="ImportPers_Lable_Felds_Header1">
                                    <asp:Label ID="Label3" runat="server" Text="Delete Personnel Not included in File" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td  align="right" >
                                    <input id="chkDel2" runat="server" type="checkbox"/></td>
                                <td  align="left" class="ImportPers_Lable_Felds_Header1">
                                    <asp:Label ID="Label10" runat="server" Text="Delete Personnel Before Import" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2"><br /></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnSaveFormat" runat="server" Text="Save Format" Width="88px" />
                                    <asp:Button ID="btnImport" runat="server" Text="Import" Width="88px" /> </td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
