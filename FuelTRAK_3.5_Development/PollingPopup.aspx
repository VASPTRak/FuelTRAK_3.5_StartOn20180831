<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PollingPopup.aspx.vb" Inherits="PollingPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Polling Device Type</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function chksesion()
		{		    window.location.assign('LoginPage.aspx')		}
        function Close()
        {            self.close();        }
        function OpnWin1()
        {            opener.location.href = "TankInventory_New_Edit.aspx?R";        }
        function OpnWin(a)
        {            alert(a);            opener.location.href = a;        }
        function OpnNewWin(i)
        {   if (i == 1){opener.location.href = "PollingQueue_New_Edit.aspx?S";         self.close();}
            else if (i == 2) { opener.location.href = "PollingQueue_New_Edit.aspx?TM";  self.close();}
            else if (i == 3) { self.close() }
        }
    </script>

</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <center>
                <table class="InvPopTable">
                    <tr>
                        <td>
                            <input type="button" id="btnFuelDel" onclick="OpnNewWin(1)" value="Sentry" class="InvPopButton" />
                        </td>
                        <td>
                            <input type="button" id="btnTankSet" onclick="OpnNewWin(2)" value="Tank Monitor" class="InvPopButton" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                         <input type="button" id="btnCancel" onclick="OpnNewWin(3);" value="Cancel" class="InvPopButton" />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
