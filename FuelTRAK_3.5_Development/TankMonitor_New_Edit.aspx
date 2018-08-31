<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TankMonitor_New_Edit.aspx.vb"
    Inherits="TankMonitor_New_Edit" EnableViewState="false" ValidateRequest="false" MaintainScrollPositionOnPostback="true"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TankMonitor_Edit Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <script src="Javascript/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="Javascript/jquery-ui.js" type="text/javascript"></script>   
</head>
<script type="text/javascript" language="javascript"> 

 function buttonClicked() 
     {
     
        // $get("txtMessageIn").scrollIntoView("true");
          var textBox = $get("txtMessageIn");
          textBox.scrollTop = textBox.scrollHeight;
          
        //textBox.scrollTop = textBox.scrollHeight;
       //var textBox = document.getElementById('txtMessageIn');
     }
     
function DisplayAlert()
{
 <%DisplayParentData()%>
} 
 
function uncheck(check)
{
    var prim = document.getElementById("chkCR");
    var secn = document.getElementById("chkLF");
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

           

    function pageLoad(sender, args)
    { 
    // $('#txtMessageIn').focus(); 
    
        debugger;
        var cmnd;       
      
        $("#btnSend").click(function() {
           var chkId = '';

           
            var msgout = $('#txtMessageOut').val();           
                     if (msgout == '')           
                          {                   
                           alert("Please Enter Message Out");   
                          }                   
        
                    
        $('.chkNumber:checked').each(function() {
          chkId += $(this).val() + ",";
        });
        chkId =  chkId.slice(0,-1);
        //alert(chkId);        
        if(chkId == '1')
        {
        cmnd = 'n' ;
        //alert(cmnd);
        }
        if(chkId == '2')
        {
        cmnd = 'r' ;
       
        }

        $("#txtInterval").val(cmnd);
        if(msgout !== '') 
        { 
        //alert("Enter");
        $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "TankMonitor_New_Edit.aspx/GetAutoCompleteData",
        //data: "{'name':'" + $("#txtMessageOut").val()+ "'}",
        data: "{'name':'" + $("#txtMessageOut").val()+ "','asciicode':'" + chkId + "','currentTime':'" + $("#txtCrntTime").val() + "'}",       
        dataType: "json",
        success: function(data) {        
        //alert("Command send Successfully!"); 
         $('#btnSend').attr("disabled", true);    
         //$( '#btnSend' ).GetallData_1(); 
        document.getElementById("TDImage").style.display="";

        },
        error: function(result) {
        alert("Error");
    } 
 
    }); 
    } 

    });
    
    }   
      function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
	    
  
	    
    function CHKLBL()
        {
  
            if(document.getElementById('CHKCOm').checked == true)
        {
         
           document.getElementById('lblcom').innerHTML ="COM # :" ;
            var strSessionValue =   document.getElementById('HiddenCommVal').value
            document.getElementById('txtcom').value = strSessionValue;
        }else
        {
          document.getElementById('lblcom').innerHTML ="Phone #:" ;
           var strSessionValue =   document.getElementById('HiddenPhoneVal').value
                         
           document.getElementById('txtcom').value = strSessionValue;
        }
        }
     function KeyPressYear(e)
     {
        var str = document.getElementById('txtcode').value;
        
        if (window.event.keyCode < 48 || window.event.keyCode > 57)
        { window.event.keyCode = 0;}
      }
      function Chkctl()
      {
        if(document.getElementById('Rdom7').checked =true)
        {
            document.getElementById('txtcode').disabled=true;
            document.getElementById('txtWlen').disabled=true;
            document.getElementById('dropParity').disabled  =true;
            document.getElementById('RDOList').disabled  =true;
            document.getElementById('btnpoll').disabled  =true;            
           
        }
      }
      function ChkOCtl()
      {
           document.getElementById('txtcode').disabled=false;
            document.getElementById('txtWlen').disabled=false;
            document.getElementById('dropParity').disabled  =false;
            document.getElementById('RDOList').disabled  =false;
            document.getElementById('btnpoll').disabled  =false;            
            
      }
      function Run_Report(url)
        { window.open(url);        }
</script>

<body class="HomeFrame">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout ="10000" runat="server">
        </asp:ScriptManager>
        <div>
            <table align="center" class="SixHundredPXTable">
                <tr>
                    <td colspan="3" class="MainHeader" style="height: 50px">
                        <asp:Label runat="server" ID="lblHeader" Text="Edit Tank Monitor Screen"></asp:Label></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px; width: 110px">
                        <asp:Label runat="server" ID="lblTM" Text="Tank Monitor #:"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 290">
                        <asp:TextBox runat="server" CssClass="ThreeCharTxtBox" ID="txtTm" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td rowspan="4" class="StdLableTxtLeft" style="vertical-align: top; width: 200px;
                        text-align: left">
                        <asp:Label runat="server" ID="lblsubHead" Text="Monitor Type" Font-Bold="True" Font-Underline="true"></asp:Label><br />
                        <asp:RadioButton ID="Rdom1" runat="server" Text="VR TLS" Checked="True" GroupName="MT" /><br />
                        <%--<asp:RadioButton ID="Rdom2" runat="server" Text="Emco  Wheaton" Enabled="False" GroupName="MT" /><br />
                        <asp:RadioButton ID="Rdom3" runat="server" Text="Andover" Enabled="False" GroupName="MT" /><br />--%>
                        <asp:RadioButton ID="Rdom4" runat="server" Text="Red Jacket" GroupName="MT" /><br />
                        <asp:RadioButton ID="Rdom5" runat="server" Text="VR 350" GroupName="MT" /><br />
                        <asp:RadioButton ID="Rdom6" runat="server" Text="Pneumercator" GroupName="MT" /><br />
                        <asp:RadioButton ID="Rdom7" runat="server" Text="Sentry Gold" GroupName="MT" />
                    </td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height: 32px">
                        <asp:Label runat="server" ID="lblName" Text="Name:"></asp:Label>
                    </td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox runat="server" ID="txtname" CssClass="TwentyFiveCharTxtBox" MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtname"
                            ErrorMessage="Please enter name of Tank Monitor." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft">
                        <asp:Label runat="server" ID="lblcom" Text="Phone No.:"></asp:Label>
                    </td>
                    <td style="text-align: left; height: 32px">
                        <asp:TextBox runat="server" ID="txtcom" CssClass="TwentyCharTxtBox" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 32px">
                    </td>
                    <td class="StdLableTxtLeft" style="text-align: left">
                        <input type="checkbox" id="CHKCOm" runat="server" value="T" onclick="CHKLBL()" />IP
                        Comm
                        <input id="HiddenPhoneVal" type="hidden" runat="server" />
                        <input id="HiddenCommVal" type="hidden" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                        <asp:ValidationSummary ID="VS1" runat="server" ShowMessageBox="True" ShowSummary="False"
                            Font-Names="arial" Font-Size="Small" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <table class="VehicleEditTableSub" style="width: 350px; height: 200px" id="TCommunication">
                            <tr>
                                <td colspan="5" class="TM_Header" style="height: 4px">
                                    <asp:Label runat="server" ID="lblsubHeadC" Text="Communications Setup"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="StdLableTxtLeft" style="width: 125px; height: 35px;">
                                    <asp:Label runat="server" ID="lblbud" Text="Baud rate"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 225px; height: 35px;">
                                    <asp:RadioButtonList ID="RDOList" runat="server" RepeatDirection="horizontal" RepeatLayout="Flow"
                                        Width="228px" Font-Size="Medium">
                                        <asp:ListItem>1200</asp:ListItem>
                                        <asp:ListItem>2400</asp:ListItem>
                                        <asp:ListItem>4800</asp:ListItem>
                                        <asp:ListItem>9600</asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft">
                                    <%-- <asp:RadioButton ID="rdo1" runat="server" Text="1200" GroupName="buad" /><br />
                                    <asp:RadioButton ID="rdo2" runat="server" Text="2400" Checked="True" GroupName="buad" /><br />
                                    <asp:RadioButton ID="rdo3" runat="server" Text="4800" GroupName="buad" /><br />
                                    <asp:RadioButton ID="Rdo4" runat="server" Text="9600" GroupName="buad" />--%>
                                    <asp:Label runat="server" ID="lblcode" Text="Security Code:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox runat="server" ID="txtcode" CssClass="SixCharTxtBox" MaxLength="6"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft">
                                    <asp:Label runat="server" ID="lblElen" Text="Word Length:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox runat="server" ID="txtWlen" CssClass="OneCharTxtBox" MaxLength="1">8</asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="StdLableTxtLeft">
                                    <asp:Label runat="server" ID="lblparity" Text="Parity:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="dropParity" runat="server" CssClass="EightCharTxtBox">
                                        <asp:ListItem Value="E">EVEN</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="N">NONE</asp:ListItem>
                                        <asp:ListItem Value="O">ODD</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="NewDept_ButtonTD" style="height: 50px">
                        <asp:Button ID="btnpoll" runat="server" Text="Poll Now" Width="100px" />
                        <asp:Button ID="btnlink" runat="server" Text="Direct Link" Width="100px" Enabled="True"
                            CausesValidation="False" OnClientClick="DisplayAlert();" />
                        <%--OnClientClick="popitup()"/>--%>
                        <asp:Button ID="btnok" runat="server" Text="OK" Width="100px" AccessKey="o" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" Width="100px" AccessKey="c"
                            CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <%--  <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnlink"
            PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader" Drag="true" DropShadow="true"
             OnOkScript="onOK()" OkControlID="btnClose"> <%--BackgroundCssClass="ModalPopupBG"
        </cc1:ModalPopupExtender>--%>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnlink"
            PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader" Drag="true" DropShadow="false"
            BackgroundCssClass="ModalPopupBG" OnOkScript="onOK()" OkControlID="btnClose">
            <%--OnCancelScript="onCancel()">  CancelControlID="btnClose"--%>
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div width="600px;">
                        <table width="100%">
                            <tr>
                                <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label Text="FuelTRAK Port Terminal" ID="lblTPT" runat="server" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="Larger" Font-Underline="True" ForeColor="Red" />
                                        
                                </td>
                                <td align="right">
                                <asp:Button ID="btnSave" runat="server" Text="Save"  CausesValidation="False" OnClick="btnSave_Click"  />
                                    <asp:Button ID="btnClose" runat="server" Text="X" CausesValidation="False" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                            <%--<tr> align="center"
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="2">
                                    <table width="90%">
                                        <tr>
                                            <td colspan="2" style="height: 80px;">
                                               <asp:TextBox ID="txtMessageIn" runat="server" Height="150px" Width="500px"   TextMode="MultiLine" AutoPostBack="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table style="width: 520px">
                                                    <tr>
                                                        <td style="width: 67px">
                                                            <asp:Label ID="lblSendMSG" Style="float: left;" Text="Send Msg:" runat="server" Font-Names="Arial"
                                                                Font-Size="Smaller" />
                                                        </td>
                                                        <td style="width: 305px">
                                                            <asp:TextBox ID="txtMessageOut" runat="server" Style="float: left;" Width="296px" />
                                                        </td>
                                                        <td colspan="2">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button Text="Send" runat="server" ID="btnSend" Width="64px" Font-Names="Arial"
                                                                            Font-Size="Smaller" Style="float: right;" /></td>
                                                                    <td>
                                                                        <asp:Button Text="Clear" runat="server" ID="btnClear" Width="64px" Font-Names="Arial"
                                                                            Font-Size="Smaller" CausesValidation="False" Style="float: left;" /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="TRImg" runat="server">
                                            <td style="width: 96px">
                                               <%-- <asp:TextBox ID="txtInterval" runat="server" Enabled="False" Visible="True" />--%>
                                                <input id="txtInterval" type="hidden" runat="server" />                                                
                                                </td>
                                            <td id="TDImage" align="center" style="height: 30px; width: 181px; display: none;">
                                                <%--class="SearchTable" --%>
                                                <asp:Image ID="impProcessing" runat="server" ImageUrl="~/images/processing1.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label Text="Communication Settings and Information" ID="lblCmmnPort" runat="server"
                                                    Style="float: left;" Font-Bold="True" Font-Names="Arial" Font-Size="Smaller" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%--<asp:CheckBoxList ID="chkCommandType" runat="server" RepeatDirection="Horizontal" tabindex="6" Font-Names="Arial" Font-Size="Smaller" >
                                <asp:ListItem Value ="0">Add Carriage Return</asp:ListItem>
                                <asp:ListItem Value ="1">Add Line Feed</asp:ListItem>
                                </asp:CheckBoxList>--%>
                                    <input type="checkbox" id="chkCR" onclick="uncheck(1);" class="chkNumber" value="1">
                                    <asp:Label Text="Add Carriage Return" ID="lblCR" runat="server" Font-Names="Arial"
                                        Font-Size="Smaller" />
                                    <input type="checkbox" id="chkLF" onclick="uncheck(2);" class="chkNumber" value="2">
                                    <asp:Label Text="Add Line Feed" ID="lblLF" runat="server" Font-Names="Arial" Font-Size="Smaller" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table style="width: 510px">
                                        <tr>
                                            <td style="width: 170px">
                                                <asp:Label Text="COM Port" ID="lblComPort" runat="Server" Font-Names="Arial" Font-Size="Smaller"
                                                    Style="float: left;" />
                                            </td>
                                            <td style="width: 170px">
                                                <asp:Label Text="IP Address" ID="lblIPaddress" runat="server" Font-Names="Arial"
                                                    Font-Size="Smaller" Style="float: left;" />
                                            </td>
                                            <td style="width: 170px">
                                                <asp:Label Text="Device Type" ID="lblDeviceType" runat="server" Font-Names="Arial"
                                                    Font-Size="Smaller" Style="float: left;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:TextBox ID="txtComPort" Width="100px" runat="server" ReadOnly="True" Enabled="False" />
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtIPAddress" Width="100px" runat="server" ReadOnly="True" Enabled="False" />
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDeviceType" Width="100px" runat="server" ReadOnly="True" Enabled="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">                                   
                                   <input id ="txtCrntTime" type="hidden" runat ="Server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </form>
</body>
</html>
