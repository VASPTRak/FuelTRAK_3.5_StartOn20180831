<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LeftMenu.ascx.vb" Inherits="UserControl_LeftMenu" %>
<table style="margin-top: 20px">
    <tr>
        <td align="left" style="width: 100px; text-align: left; vertical-align: top; height: 650px">
            <!-- <script type="text/javascript" language="javascript"> window.resizeTo(window.screen.availWidth, window.screen.availHeight);</script>-->

            <script type="text/javascript" src="Menu/milonic_src.js"></script>

            <script type="text/javascript" src="Javascript/Validation.js"></script>

            <script type="text/javascript">
            if(ns4)_d.write("<scr"+"ipt type=text/javascript src=Menu/mmenuns4.js><\/scr"+"ipt>");		
            else _d.write("<scr"+"ipt type=text/javascript src=Menu/mmenudom.js><\/scr"+"ipt>"); 
            </script>

            <%     If Session("User_name") Is Nothing Then%>
            <%  Response.Redirect("Loginpage.aspx")
            Else%>
            <%        If (Session("User_Level").ToString() = "1") Then%>

            <script type="text/javascript" src="Menu/menu_data.js"></script>

            <%        ElseIf (Session("User_Level").ToString() = "2") Then%>

            <script type="text/javascript" src="Menu/menu_data1.js"></script>

            <%        ElseIf (Session("User_Level").ToString() = "3") Then%>

            <script type="text/javascript" src="Menu/menu_data2.js"></script>

            <%        Else%>

            <script type="text/javascript" src="Menu/menu_data3.js"></script>

            <%        End If%>
            <%End If%>
        </td>
    </tr>
</table>
