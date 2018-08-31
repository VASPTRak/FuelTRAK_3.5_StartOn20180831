<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sentry_New_Edit.aspx.vb"
    Inherits="Sentry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sentry_New_Edit Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <link href="Stylesheet/start/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }
        .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>

    <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	
//	function uncheck(check)
//{
//    var prim = document.getElementById("chkCR");
//    var secn = document.getElementById("chkLF");
//    if (prim.checked == true && secn.checked == true)
//    {
//        if(check == 1)
//        {
//            secn.checked = false;
////            checkRefresh();
//        }
//        else if(check == 2)
//        {
//            prim.checked = false;
////            checkRefresh();
//        }
//    }
// 
//}

  
     function KeyPress(e)
     {
        var str = document.getElementById('Txtstate').value;
        
        if (window.event.keyCode < 65 || window.event.keyCode > 122)
        { 
            
                window.event.keyCode = 0;
        }
      }
    </script>

    <!-- edit by omar - added script references to jquery and jquery-ui as well as 2 javascript methods -->

    <script type="text/javascript" src="Javascript/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="Javascript/jquery-ui-1.7.2.custom.min.js"></script>

    <script type="text/javascript">
    
        function beginPolling(key, type) {
            var title = '';
            if(type == 'commtest') { title = "Communication Test"; } else { title = "Polling Sentry"; }
            $('#log-messages').dialog({modal:true, resizable:true, draggable:true, width:"450", height:"300", title:title });
            $('#log-messages').append($('<p></p>'));
            $('#log-messages p:first-child').text("current status: queued");
            
            var spinner = $('<p><img alt="spinning circle" src="images/ajax-loader.gif" /></p>');
            $('#log-messages').append(spinner);
            getPollingUpdates(key, 0);
        }
        function  ChkChange(check)
         {
          
            if(document.getElementById('CHKCOM').checked == true && check == 2)
            {         
         
            document.getElementById('Label4').innerHTML ="IP Comm Port #:" ;
           //document.getElementById('t1').style.display='';           
         
            var strSessionValue =   document.getElementById('HiddenCommVal').value
             
             document.getElementById('TxtCom').value = strSessionValue;
             
           
            }
            else
            {
             document.getElementById('Label4').innerHTML ="Phone # :" ;
             //document.getElementById('t1').style.display='none';             
              var strSessionValue =   document.getElementById('HiddenPhoneVal').value               
                
                 document.getElementById('TxtCom').value = strSessionValue;               
          
             }
             
         var prim = document.getElementById('chkUseIPWebSocketConn');
         //alert(prim);
         var secn = document.getElementById('CHKCOM');
         
    if (prim.checked == true && secn.checked == true)
    {
        if(check == 1)
        {
            secn.checked = false;
//            checkRefresh();
        }
        else if(check == 2)
        {
            prim.checked = false;
//            checkRefresh();
        }
    }
        }
       function getPollingUpdates(key, lastid) {
        $.ajax({
            cache: false,
            url: 'PollingInfo.ashx?pollingKey=' + key,
            dataType: "json",
            success: function(data) { 
                $.each(data.messages, function(key, value) {
                    if(value.id > lastid) {
                        var m = $('<p></p>').text(value.text);
                        $('#log-messages').append(m);
                    }
                });
                $('#log-messages p:first-child').text("current status: " + data.status);
                if(data.status != 'completed') {
                    if(data.messages.length === 0) {
                        setTimeout(function() { getPollingUpdates(key, 0); }, 5000);
                    }
                    else {
                        setTimeout(function() { getPollingUpdates(key, data.messages[data.messages.length -1].id); }, 1);
                    }
                }
                else {
                    $('#log-messages p:nth-child(2)').hide();
                }
            },
            error: function(xhr,status,error) {
                alert(error);
            }            
        });            
       }  
       
        function TankSelectionValidation()
        {    
            var Tank1 = document.getElementById("ddlprobetank1").value;           
            if (Tank1 == 'Select Tank')           
            Tank1 = "1";            
            var Tank2 = document.getElementById("ddlprobetank2").value;
            if (Tank2 == 'Select Tank')
            Tank2 = "2";                       
            var Tank3 = document.getElementById("ddlprobetank3").value;  
            if (Tank3 == 'Select Tank')
            Tank3 = "3";                                    
            var Tank4 = document.getElementById("ddlprobetank4").value; 
             if (Tank4 == 'Select Tank')
            Tank4 = "4";               
            var Tank5 = document.getElementById("ddlprobetank5").value;
             if (Tank5 == 'Select Tank')
            Tank5 = "5";               
            var Tank6 = document.getElementById("ddlprobetank6").value;
             if (Tank6 == 'Select Tank')
            Tank6 = "6"; 
             var Tank7 = document.getElementById("ddlprobetank7").value;
             if (Tank7 == 'Select Tank')
            Tank7 = "7";  
             var Tank8 = document.getElementById("ddlprobetank8").value;
             if (Tank8 == 'Select Tank')
            Tank8 = "8";  
             var Tank9 = document.getElementById("ddlprobetank9").value;
             if (Tank9 == 'Select Tank')
            Tank9 = "9";   
             
            if(Tank1 == Tank2 || Tank3 == Tank1 || Tank3 == Tank2 || Tank4 == Tank1 || Tank4 == Tank2 || Tank4 == Tank3 || Tank5 == Tank1 || Tank5 == Tank2 || Tank5 == Tank3 || Tank5 == Tank4 || Tank6 == Tank1 || Tank6 == Tank2 || Tank6 == Tank3 || Tank6 == Tank4 || Tank6 == Tank5 || Tank7 == Tank1 || Tank7 == Tank2 || Tank7 == Tank3 || Tank7 == Tank4 || Tank7 == Tank5 || Tank7 == Tank6 || Tank8 == Tank1 || Tank8 == Tank2 || Tank8 == Tank3 || Tank8 == Tank4 || Tank8 == Tank5 || Tank8 == Tank6 || Tank8 == Tank7 || Tank9 == Tank1 || Tank9 == Tank2 || Tank9 == Tank3 || Tank9 == Tank4 || Tank9 == Tank5 || Tank9 == Tank6 || Tank9 == Tank7 || Tank9 == Tank8 )              
               {
                alert('Only one tank can be associate to one TLD at a time');
                return false;
               }
            else            
                return true;           
        
        } 
    </script>

    <style type="text/css">
        .style1
        {
            text-align: left;
            font-family: arial;
            font-size: small;
            width: 60px;
            height: 26px;
        }
        .style2
        {
            text-align: left;
            font-family: arial;
            font-size: small;
            width: 300px;
            height: 26px;
        }
        .style3
        {
            text-align: left;
            font-family: arial;
            font-size: small;
            width: 240px;
            height: 26px;
        }
    </style>
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table>
            <tr>
                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div class="modal">
                                <div class="center">
                                    <img src="images/Connecting.gif" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>
        <table align="center" class="EightHundredPXTable">
            <tr>
                <td colspan="4" class="MainHeader" style="height: 50px">
                    <asp:Label runat="server" ID="Label10" Text="Edit Sentry Information"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 30px; width: 404px">
                    <asp:Label runat="server" ID="lbl1" CssClass="StdLableTxtLeft" Text="Sentry #:"></asp:Label>
                </td>
                <td style="width: 220px; text-align: left">
                    <asp:TextBox runat="server" ID="txtSentry" CssClass="ThreeCharTxtBox" MaxLength="3"
                        TabIndex="1"></asp:TextBox>
                </td>
                <td class="StdLableTxtLeft" style="width: 554px">
                    <asp:Label ID="lblExpCode" runat="server" Text="Export Code:"></asp:Label>
                </td>
                <td style="width: 150px; text-align: left">
                    <asp:TextBox ID="txtExpCode" runat="server" CssClass="SevenCharTxtBox" MaxLength="7"
                        TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="width: 404px">
                    <asp:Label runat="server" ID="lbl2" CssClass="StdLableTxtLeft" Text="Name:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox runat="server" ID="TxtName" CssClass="TwentyFiveCharTxtBox" MaxLength="25"
                        TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtName"
                        Display="Dynamic" ErrorMessage="Enter Sentry Name">*</asp:RequiredFieldValidator>
                </td>
                <td class="StdLableTxtLeft" style="width: 554px">
                    <asp:Label ID="lblBT" runat="server" Text="Board Type:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlSentry" runat="server" Font-Names="arial" CssClass="TenCharacterField"
                        Font-Size="Small" TabIndex="7" Width="112px">
                        <asp:ListItem>Sentry5</asp:ListItem>
                        <asp:ListItem>Sentry6</asp:ListItem>
                        <asp:ListItem Selected="True">SentryGold</asp:ListItem>
                        <asp:ListItem>ArmRadio</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 30px; width: 404px;">
                    <asp:Label runat="server" ID="lbl3" CssClass="StdLableTxtLeft" Text="Address :"></asp:Label>
                </td>
                <td style="text-align: left" class="StdLableTxtLeft">
                    <asp:TextBox runat="server" ID="Txtaddress" CssClass="TwentyFiveCharTxtBox" MaxLength="25"
                        TabIndex="3"></asp:TextBox>
                </td>
                <td colspan="2" class="SentyChkBox" align="left" style="font-family: Arial; font-size: small;
                    font-weight: bold;">
                    <input type="checkbox" runat="server" id="chkUseIPWebSocketConn" onclick="ChkChange(1);"
                        class="chkNumber" value="1" />
                    <asp:Label runat="server" ID="lblIPWebSocket" Text="Use IP Web Socket Connection"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="StdLableTxtLeft" style="height: 30px; width: 404px;">
                    <asp:Label runat="server" ID="Label3" CssClass="StdLableTxtLeft" Text="State:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox runat="server" ID="Txtstate" MaxLength="2" TabIndex="4"></asp:TextBox>
                </td>
                <td class="SentyChkBox" align="left" style="font-family: Arial; font-size: small;
                    font-weight: bold; width: 800px">
                    <input type="checkbox" runat="server" id="CHKCOM" onclick="ChkChange(2)" class="chkNumber"
                        value="2" />
                    <asp:Label runat="server" ID="lblcom" Text="Use IP Comm Conn"></asp:Label>&nbsp;<label>-</label>
                    <asp:Label runat="server" ID="lblIp" Text="License:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox runat="server" ID="txtIP" MaxLength="15" CssClass="FifteenCharTxtBox"
                        TabIndex="8"></asp:TextBox>
                </td>
                <%--<td style="text-align: left">
                        <input name="txtIP" type="text" maxlength="15" tabindex="8" class="FifteenCharTxtBox" /></td>--%>
                <%--<td style="text-align: left">
                        <asp:TextBox runat="server" ID="txtIP" MaxLength="15" CssClass="FifteenCharTxtBox"
                            TabIndex="8"></asp:TextBox>
                    </td>--%>
            </tr>
            <tr>
                <td colspan="2" class="SentyChkBox" align="left" style="font-family: Arial; font-size: small;
                    font-weight: bold;">
                    <asp:CheckBox runat="server" ID="CHKCPR" Text="Send Custom Prompts/Replies" TabIndex="9" />
                </td>
                <td class="StdLableTxtLeft" valign="top" style="width: 554px">
                    <asp:Label runat="server" ID="Label4" CssClass="StdLableTxtLeft" Text="Phone #:"></asp:Label>
                </td>
                <td style="height: 32px; width: 385px; text-align: left">
                    <asp:TextBox runat="server" ID="TxtCom" MaxLength="25" TabIndex="5"></asp:TextBox>
                    <p style="margin-top: 0">
                        <small style="font-style: italic">(Blank for Local Sentry)</small></p>
                </td>
            </tr>
            <tr>
                <%--<td class="StdLableTxtLeft" valign="top">
                        <asp:Label runat="server" ID="Label4" CssClass="StdLableTxtLeft" Text="Phone #:"></asp:Label></td>
                    <td style="height: 32px; width: 385px; text-align: left">
                        <asp:TextBox runat="server" ID="TxtCom" MaxLength="25" TabIndex="5"></asp:TextBox>
                        <p style="margin-top: 0">
                            <small style="font-style: italic">(Blank for Local Sentry)</small></p>
                    </td>--%>
                <td colspan="2" align="left">
                    <%--   <table>
                    <tr>
                    <td class="StdLableTxtLeft" style="height: 30px;">
                        <asp:Label runat="server" ID="lblTMSelect" CssClass="StdLableTxtLeft" Text="Sentry Gold TM:"></asp:Label></td>
                    <td class="SentyChkBox" align="left">
                        <asp:DropDownList ID="ddlSentryTM" runat="server" Width="110px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:100px;"></td>
                    </tr>
                    </table>--%>
                </td>
                <td colspan="2" class="SentyChkBox" style="text-align: left" id="t1" runat="server">
                    <asp:Label runat="server" ID="lblBrate" Text="Baud Rate"></asp:Label>
                    <asp:RadioButtonList ID="rdoBuadRate" runat="server" RepeatDirection="horizontal"
                        Font-Names="arial" Font-Size="Small" Width="270px" TabIndex="10">
                        <asp:ListItem>1200</asp:ListItem>
                        <asp:ListItem Selected="True">2400</asp:ListItem>
                        <asp:ListItem>4800</asp:ListItem>
                        <asp:ListItem>9600</asp:ListItem>
                        <asp:ListItem>38400</asp:ListItem>
                        <asp:ListItem>57600</asp:ListItem>
                        <asp:ListItem>115200</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input id="HiddenPhoneVal" type="hidden" runat="server" />
                    <input id="HiddenCommVal" type="hidden" runat="server" />
                </td>
                <%--<td class="StdLableTxtLeft" style="height: 30px;">
                        <asp:Label runat="server" ID="lblTMSelect" CssClass="StdLableTxtLeft" Text="Sentry Gold TM:"></asp:Label></td>
                    <td class="SentyChkBox" align="left">
                        <asp:DropDownList ID="ddlSentryTM" runat="server" Width="110px">
                        </asp:DropDownList>
                    </td>--%>
            </tr>
            <tr>
                <td align="center" colspan="7">
                    <table align="Left" class="SentryFourHundredPXTable" cellpadding="5" cellspacing="0"
                        style="border-top: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black;
                        border-right: solid 1px black; background-color: #a5bbc5">
                        <tr>
                            <td class="style1" style="text-align: center">
                                <strong>
                                    <asp:Label runat="server" ID="lblHose" Text="Hose"></asp:Label></strong>
                            </td>
                            <td class="style2">
                                <strong>
                                    <asp:Label runat="server" ID="lbltank" Text="Tank"></asp:Label></strong>
                            </td>
                            <td class="style3">
                                <strong>
                                    <asp:Label runat="server" ID="lblpulser" Text="Pulser"></asp:Label></strong>
                            </td>
                            <%--<td class="Sentry_Lable_Center" style="width: 240px">
                                    <strong>
                                        <asp:Label runat="server" ID="lblStartTotRead" Text="Totalizer Reading" Visible="false"></asp:Label>
                                    </strong>
                                </td>
                                <td class="Sentry_Lable_Center" style="width: 210px">
                                    <strong>
                                        <asp:Label runat="server" ID="lblStartTotDT" Text="Date Time" Visible="false"></asp:Label></strong></td>--%>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label runat="server" ID="lblHeader" Text="Use {Drop arrow} to Select Tank and Pulser"
                                    Font-Italic="True" Font-Names="arial" Font-Size="X-Small" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="l1" Text="1"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank1" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser1" Width="130px">
                                </asp:DropDownList>
                            </td>
                            <%--<td class="Sentry_Border_Subright" >
                                    <asp:TextBox runat="server" ID="txtPump1StartTotRead" Width="100px" visible="false">
                                    </asp:TextBox></td>
                                <td class="Sentry_Border_Subright">
                                    <asp:TextBox runat="server" ID="txtPump1StartTotDT" Width="160px" visible="false">
                                    </asp:TextBox></td>
                                <td class="Sentry_Lable_Center" style="width: 130px">
                                    <strong>
                                        <asp:Button runat="server" ID="btnUpdateTot1" Text="UPDATE" visible="false" /></strong>
                                </td>--%>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label1" Text="2"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank2" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser2" Width="130px">
                                </asp:DropDownList>
                            </td>
                            <%-- <td class="Sentry_Border_Subright">
                                    <asp:TextBox runat="server" ID="txtPump2StartTotRead" Width="100px" Visible="false">
                                    </asp:TextBox></td>
                                <td class="Sentry_Border_Subright" visible="false">
                                    <asp:TextBox runat="server" ID="txtPump2StartTotDT" Width="160px" Visible="false">
                                    </asp:TextBox></td>
                                <td class="Sentry_Lable_Center" style="width: 130px">
                                    <strong>
                                        <asp:Button runat="server" ID="btnUpdateTot2" Text="UPDATE" Visible="false" /></strong>
                                </td>--%>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label2" Text="3"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank3" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser3" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label5" Text="4"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank4" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser4" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label6" Text="5"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank5" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser5" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label7" Text="6"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank6" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser6" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label8" Text="7"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank7" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser7" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label9" Text="8"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank8" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser8" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="lblTank9" Text="9"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank9" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser9" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="lblTank10" Text="10"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddltank10" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="Sentry_Border_Subright">
                                <asp:DropDownList runat="server" ID="ddlPulser10" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table align="left" class="SentryThreeHundredPXTable" cellpadding="5" cellspacing="0"
                        style="border-top: solid 1px black; border-bottom: solid 1px black; border-left: solid 1px black;
                        border-right: solid 1px black; background-color: #a5bbc5">
                        <tr>
                            <td class="Sentry_Lable_Center" style="width: 100px; text-align: center">
                                <strong>
                                    <asp:Label runat="server" ID="Label11" Text="Sentry Gold"></asp:Label></strong>
                            </td>
                            <td class="Sentry_Lable_Center" style="width: 200px">
                                <strong>
                                    <asp:Label runat="server" ID="Label12" Text="Tank"></asp:Label></strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <strong>
                                    <asp:Label runat="server" ID="Label14" Text="TLD"></asp:Label></strong>
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="Label13" Text="Use {Drop arrow} to Select Tank" Font-Italic="True"
                                    Font-Names="arial" Font-Size="X-Small" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label15" Text="1"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank1" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label16" Text="2"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank2" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label17" Text="3"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank3" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label18" Text="4"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank4" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label19" Text="5"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank5" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label20" Text="6"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank6" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label21" Text="7"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank7" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label22" Text="8"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank8" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Sentry_Label_Hose">
                                <asp:Label runat="server" ID="Label23" Text="9"></asp:Label>
                            </td>
                            <td class="Sentry_Tank_Dropdown">
                                <asp:DropDownList runat="server" ID="ddlprobetank9" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%-- <tr>
                                <td class="Sentry_Label_Hose" style="height:15px">
                                    </td>
                                <td class="Sentry_Tank_Dropdown">                            
                                    </td>
                            </tr>--%>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5" style="height: 26px">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Names="arial"
                        Font-Size="Small" ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" style="height: 26px">
                    <asp:Button ID="btnPollingLog" runat="server" CssClass="Sentry_New_Button" Text="Polling Log" />
                    <asp:Button ID="btnSentryDSA" runat="server" CssClass="Sentry_New_Button" Enabled="False"
                        Text="Sentry 6 DSA" />
                    <asp:Button runat="server" ID="btnComtst" Text="Comm Test" Width="100px" CssClass="Sentry_New_Button" />
                    <asp:Button runat="server" ID="btnSetup" Text="Setup" Width="100px" CssClass="Sentry_New_Button"
                        Enabled="False" />
                    <asp:Button runat="server" ID="btnpoll" Text="Poll" Width="100px" CssClass="Sentry_New_Button" />
                    <asp:Button runat="server" ID="btnok" Text="OK" Width="100px" CssClass="Sentry_New_Button"
                        AccessKey="o" OnClientClick="return TankSelectionValidation();" />
                    <asp:Button runat="server" ID="btncancel" Text="Cancel" Width="100px" CssClass="Sentry_New_Button"
                        AccessKey="c" CausesValidation="False" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <%--  <asp:Button runat="server" ID="btnDiagnostic" Text="Diagnostic" Width="100px" CssClass="Sentry_New_Button"
                                OnClick="btnDiagnostic_Click" Font-Bold="True" Font-Size="Medium" />--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </div>
    <div id='log-messages'>
    </div>
    </form>
</body>
</html>
