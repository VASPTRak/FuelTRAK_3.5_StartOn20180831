<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jquery.aspx.cs" Inherits="jquery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>jQuery Hello World Alert box</title>
 

<%--<script type="text/javascript" src="jquery-1.4.2.js">--%>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
</head> 
 <script type="text/javascript" language="javascript">
$(document).ready(function(){
$("#cl").click(function(){
$( this ).slideUp();
alert("HELLO WORLD!");
});
});
</script>
<body>
<form id="form1" runat="server">
<font color="red">CLICK BELOW BUTTON TO SEE ALERT BOX</font>
<br>
<br>
<asp:Button id="cl" runat="server" Text="Click" OnClick="cl_Click"></asp:Button>
<asp:TextBox ID="txtTest" runat="server" /> 
</form>
</body>
</html>
 

