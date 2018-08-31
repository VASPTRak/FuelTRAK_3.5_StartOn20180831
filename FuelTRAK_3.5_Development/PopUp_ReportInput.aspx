<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopUp_ReportInput.aspx.vb" Inherits="PopUp_ReportInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
     <script language="javascript" type="text/javascript">
     function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}

    function loadpage2()
    {
        form1.submit();
    }
    
    </script>
</head>
<body topmargin=0>
    <form id="form1" runat="server">
    <div style="overflow:auto;height:350px">
        <table  align=center style="vertical-align:top" width=80%>
            <tr >
                <td>
                    <asp:Table ID="tblSearch_result_Header" runat="server" Width="100%"></asp:Table>
                    <asp:Table ID="tblSearchResult" runat="server" Width="100%"></asp:Table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
