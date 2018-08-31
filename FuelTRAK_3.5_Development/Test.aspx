<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Test.aspx.vb" Inherits="Test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function pageLoad() {
        ShowPopup();
        setTimeout(HidePopup, 2000);
    }

    function ShowPopup() {
        $find('modalpopup').show();
        $get('Button1').click();
    }

    function HidePopup() {
        $find('modalpopup').hide();
        $get('btnCancel').click();
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
    
   <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>

<asp:button id="Button1" runat="server" text="Button" />

<cc1:modalpopupextender id="ModalPopupExtender1" runat="server" 
	cancelcontrolid="btnCancel" okcontrolid="btnOkay" 
	targetcontrolid="Button1" popupcontrolid="Panel1" 
	popupdraghandlecontrolid="PopupHeader" drag="true" 
	backgroundcssclass="ModalPopupBG">
</cc1:modalpopupextender>

<asp:panel id="Panel1" style="display: none" runat="server">
	<div class="HellowWorldPopup">
                <div class="PopupHeader" id="PopupHeader">Header</div>
                <div class="PopupBody">
                    <p>This is a simple modal dialog</p>
                </div>
                <div class="Controls">
                    <input id="btnOkay" type="button" value="Done" />
                    <input id="btnCancel" type="button" value="Cancel" />
		</div>
        </div>
</asp:panel>
    </form>
</body>
</html>
