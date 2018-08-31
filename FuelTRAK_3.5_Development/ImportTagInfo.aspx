<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportTagInfo.aspx.vb" Inherits="ImportTagInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
        <div>
            <table id="tblImportTXTNs" runat="server" align="center" class="FiveHundredPXTable">
                <tr>
                    <td class="MainHeader" colspan="2">
                        <asp:Label ID="lblHeader" runat="server" Text="Import Vehicle TagInfo from XML file"></asp:Label></td>
                </tr>
                <tr>
                    <td style="height: 15px">
                    </td>
                </tr>
                <tr>
                    <td align="center" class="VehicleTD" style="text-align: center">
                        <asp:FileUpload ID="fuTagInfo" runat="server" Style="width: 400px; height: 25px" />
                        <asp:RequiredFieldValidator ID="rfvTXTNupload" runat="server" ControlToValidate="fuTagInfo"
                            ErrorMessage="File Required!" Height="25px"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only .xml files are allowed."
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.xml)$"
                            ControlToValidate="fuTagInfo">*</asp:RegularExpressionValidator>
                        <asp:Button ID="btnSubmit" runat="Server" Height="35px" OnClientClick="DisplayDiv()"
                            Text="Upload" Width="100px" /></td>
                </tr>
                <tr>
                    <td id="TDButtons" runat="server" class="VehicleTD" style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td id="Td1" runat="server" class="VehicleTD" style="text-align: center">
                    </td>
                </tr>
                <tr>
                    <td id="TDImage" align="center" class="SearchTable" style="display: none; width: 181px;
                        height: 150px">
                        <asp:Image ID="impProcessing" runat="server" ImageUrl="~/images/processing1.gif" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
