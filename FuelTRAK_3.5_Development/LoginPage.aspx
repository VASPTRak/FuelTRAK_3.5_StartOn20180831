<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginPage.aspx.vb" Inherits="LoginPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FuelTRAK Login Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

//      function back_block()
//        {
//          window.history.forward(-1) 
//        }
        function expand() 
        {   
            window.moveTo(0,0); window.resizeTo(screen.availWidth, screen.availHeight);
            document.getElementById('txtUname').focus();   
        }
    
        function DIV1_onclick() 
        {

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 42px;
            height: 79px;
        }
        .style2
        {
            height: 79px;
        }
    </style>
</head>
<body onload="expand()">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="border: 0px; width: 900px; height: 1px" id="TABLE3">
               <tr>
                    <td valign="top" align="center" colspan="5" style="height: 175px">
                        &nbsp;<asp:Image ID="imgSiteLogo" runat="server" ImageUrl="~/images/banner_new.png"
                                    Width="932px" Height="124" />
                        <strong><font size="6">Fuel Management System Software</font></strong>
                     </td>
               </tr>
                <tr>
                    <td valign="top" align="center" rowspan="2">
                        <table cellpadding="0" cellspacing="0" width="400">
                            <!-- MSCellFormattingTableID="2" -->
                            <tr>
                                <td style="width:327">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height:100%">
                                        <tr>
                                            <td valign="bottom" style="height:100%; width:100%">
                                                <br />
                                                <!-- MSCellFormattingType="content" -->
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width:42px" valign="top">
                                                            <img src="images/check.jpg" alt="bullet" width="16" height="16" />
                                                        </td>
                                                        <td valign="top" align="left" style="width:100%">
                                                            <font size="4"><a href="http://www.trakeng.com/"><font color="#F7C80F"><u>Trak Engineering,
                                                                Inc.</u></font></a><font color="#99CCFF"></font> is a progressive leader in developing
                                                                and manufacturing computerized fuel management solutions in the United States. </font>
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  valign="top" class="style1">
                                                            <img src="images/check.jpg" width="16" height="16" alt="bullet" />
                                                        </td>
                                                        <td valign="top" align="left" class="style2">
                                                            <font size="4">Our systems are designed to help government entities and private industries
                                                                make efficient use of fuel and fuel-related consumables. </font>
                                                         <br />
                                                         <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:42px"  valign="top">
                                                            <img src="images/check.jpg" width="16" height="16" style="padding:13" alt="bullet" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <font size="4">Escalating prices and diminishing resources have resulted in a demand
                                                                for accurate information, control and accountability of fluid assets. </font>
                                                         <br />
                                                         <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:42px" valign="top">
                                                            <img src="images/check.jpg" width="16" height="16" style="padding:13" alt="bullet" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <font size="4">Trak Engineering is one of the foremost suppliers in the fuel management
                                                                industry. </font>
                                                         <br />
                                                         <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:42px"  valign="top">
                                                            <img src="images/check.jpg" width="16" height="16" hspace="13" alt="bullet" />
                                                        </td>
                                                        <td valign="top"  align="left">
                                                            <font size="4">In addition we provide products that add value to, and integrate with
                                                                our automated fuel management solutions. </font>
                                                         <br />
                                                         <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:42px"  valign="top">
                                                            <img src="images/check.jpg" width="16" height="16" hspace="13" alt="bullet" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <font size="4">Our customer base comprises of thousands of fuel islands and several
                                                                million vehicles; all operating successfully with Trak’s integrated solutions. </font>
                                                         <br />
                                                         <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:42px" valign="top">
                                                            <img src="images/check.jpg" width="16" height="16" hspace="13" alt="bullet" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <font size="4">One of Trak's strengths is that we excel in “outside-the-box” thinking.
                                                            </font>
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="width:42px"  valign="top">
                                                            <img src="images/check.jpg" width="16" height="16" hspace="13" alt="bullet" />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <font size="4">The willingness and flexibility to customize both the Fuel Sentry and
                                                                the Fuel Management System program is fundamental to our company.</font>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table id="TABLE1">
                            <tr>
                            <td align="right">
                                    <table style="text-align:right"; cellpadding="0" "vertical-align: top; width: 275px;
                                        border: solid 2px #006699; background-color: #ff00ff" id="TABLE2">
                                        <tr style="height:2px; text-align:right">
                                            <td colspan="3">  <img src="images/logincell2.jpg" width="310" height="100" alt="login"/>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td style="text-align:right; height:40px; width:100px">
                                                <asp:Label ID="lblDomainName" runat="server" Font-Bold="True" Font-Size="10pt" Text="Domain Name:">
                                                </asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:TextBox ID="txtDomainName" runat="server" Width="140px" TabIndex="1">
                                                </asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right; height:40px; width:100px">
                                                <asp:Label ID="lblUserName" runat="server" Font-Bold="True" Font-Size="10pt" Text="User Name:"></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:TextBox ID="txtUserName" runat="server" Width="140px" TabIndex="2"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator2" runat="server" Width="1%" Font-Size="Small" Font-Bold="True"
                                                    Font-Names="Arial" SetFocusOnError="True" Display="Dynamic" ControlToValidate="txtUserName"
                                                    ErrorMessage="Please enter User Name" ForeColor="DeepSkyBlue">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="text-align:right; height:40px; width:100px">
                                                <asp:Label ID="lblPassword" runat="server" Font-Bold="True" Font-Size="10pt" Text="Password:"></asp:Label>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="140px" TabIndex="2"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator3" runat="server" Width="1%" Font-Size="Small" Font-Bold="True"
                                                    Font-Names="Arial" SetFocusOnError="True" Display="Dynamic" ControlToValidate="txtPassword"
                                                    ErrorMessage="Please enter password" ForeColor="DeepSkyBlue">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="height:40px">
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center"> 
                                            <asp:Button ID="btnLogin" runat="server" Text="Log In" TabIndex="7" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <asp:Label ID="lblerror" runat="server" ForeColor="DeepSkyBlue" Font-Size="Small"
                                                    Font-Bold="True" Font-Names="Arial">
                                                </asp:Label>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                    Style="font-size: 10pt; font-family: Arial" ShowSummary="False" Height="36px"
                                                    ForeColor="DeepSkyBlue" />
                                                <input id="dtime" type="text" runat="server" visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width:393" rowspan="2">
                                    <p align="center" style="margin-bottom: 3px">
                                        <strong><font color="#FFFFFF">Contact Information</font></strong>
                                    </p>
                                    <p style="margin-top: 0; margin-bottom: 0; text-align:center">
                                        <strong><font size="2">Sales&nbsp; </font></strong>
                                    </p>
                                    <p style="margin-top: 0; margin-bottom: 0" align="center">
                                        <font size="2">Katherine Blyth&nbsp; </font><a href="mailto:kblyth@trakeng.com"><font
                                            size="2" color="#99CCFF">kblyth@trakeng.com</font></a>
                                    </p>
                                    <p style="margin-top: 0; margin-bottom: 0" align="center">
                                        <font size="2">Polly Cobb&nbsp; <a href="mailto:pcobb@trakeng.com"><font color="#99CCFF">
                                            pcobb@trakeng.com</font></a></font>
                                    </p>
                                    <p style="margin-top: 10px; margin-bottom: 0" align="center">
                                        <font size="2"><strong>Customer Service</strong></font>
                                    </p>
                                    <p style="margin-top: 0; margin-bottom: 0; text-align:center">
                                        <font size="2"><font color="#99CCFF"> </font><a href="mailto:support@trakeng.com">
                                         <font color="#99CCFF">support@trakeng.com</font></a></font>
                                    </p>
                                   <%-- <p style="margin-top: 0; margin-bottom: 0; text-align:center">
                                        <font size="2">Isiah Washington&nbsp;<font color="#99CCFF"> </font><a href="mailto:iwashington@trakeng.com">
                                            <font color="#99CCFF">iwashington@trakeng.com</font></a></font>
                                    </p>--%>
                                    <p style="margin-top: 0; margin-bottom: 0" align="center">
                                        &nbsp;
                                    </p>
                                    <p style="margin-top: 0; margin-bottom: 0" align="center">
                                        <font size="2">Phone (850)878-4585, Fax (850)656-8265</font>
                                    </p>
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
