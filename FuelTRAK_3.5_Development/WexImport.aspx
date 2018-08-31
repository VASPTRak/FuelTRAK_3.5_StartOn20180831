<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WexImport.aspx.vb" Inherits="WexImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Wex Import</title>
    <link type="text/css" href="Stylesheet/FuelTrak.css" rel="stylesheet" />
    
    <script type="text/javascript">
     myAlert("Message from FuelTRAK", "Some content");
    </script>
    
    <script type="text/javascript">
     function chksesion()
	  {
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	  }
    var validFilesTypes=["xls"];

    function ValidateFile()

    {
      document.getElementById("TDImage").style.display="";
      var file = document.getElementById("<%=fuTXTN.ClientID%>"); 
      var label = document.getElementById("<%=Label1.ClientID%>"); 
      var path = file.value;
      var ext=path.substring(path.lastIndexOf(".")+1,path.length).toLowerCase();
      var isValidFile = false;
      for (var i=0; i<validFilesTypes.length; i++)
      { 
        if (ext==validFilesTypes[i]) 
        {
            isValidFile=true;
            break;
        }
      }

      if (!isValidFile)

      {

        label.style.color="red";
        
        label.innerHTML="Invalid File. Please upload a File with" + 

         " extension:\n\n"+validFilesTypes.join(", ");

      }

      return isValidFile;

     }

    </script>
</head>
<body class="HomeFrame">
    <form id="form1" runat="server">
    <div align="left">
                <table align="center" id="tblImportTXTNs" runat="server" class="FiveHundredPXTable">
               <tr>
              <%-- <td style="border:solid 1px black;height:50px"> --%>
              <td colspan= "2" style="height:50px">
               </td>
               </tr>
                <tr>
                    <td colspan="2" class="MainHeader">
                        <asp:Label ID="lblHeader" runat="server" Text=" WEX Import"></asp:Label></td>
                </tr>
                <tr style="border:solid 1px black;height:20px"></tr>
                <tr>
                <td colspan="2" >
                <%--<asp:Label ID="lblWex" Text="Successfully Import Transactions from Excel file" runat="server" Font-Names="Arial" Font-Size="Medium" />--%>
                </td>
                </tr>
                <tr>
                    <td align="center" class="VehicleTD" style="text-align: center;">
                        <asp:FileUpload ID="fuTXTN" runat="server" Style="width: 400px; height: 25px" />
                        <asp:RequiredFieldValidator ID="rfvTXTNupload" runat="server" Height="25px" ControlToValidate="fuTXTN"
                            ErrorMessage="File Required!">
                        </asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td id="TDButtons" runat="server" class="VehicleTD" style="text-align: center;">
                        <input id="filepathVal" type="hidden" runat="server" style="width: 5px" />
                       &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSubmit" Text="Upload" runat="Server" OnClientClick="return ValidateFile()"
                            Width="100px" Height="32px" />
                    </td>
                </tr>
                <tr>
                    <td id="Td1" runat="server" class="VehicleTD" style="text-align: center">
                        <asp:CustomValidator ID="cvFileUpload" runat="server" ClientValidationFunction="ValidateFileUpload"
                            ErrorMessage="Please select a valid Transaction file."></asp:CustomValidator></td>
                </tr>
                <tr>
                <td>
                        <asp:Label Id="Label1" runat="server" />
                        </td>
                </tr>
                <tr>
                    <td id="TDImage" class="SearchTable" align="center" style="height: 150px; display: none;
                        width: 181px;">
                        <asp:Image ID="impProcessing" runat="server" ImageUrl="~/images/processing1.gif" />
                    </td>
                </tr>
                <tr>
                  <td id="TD2" class="SearchTable" align="center" colspan="4" style="height: 24px;display:none ">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/processing1.gif" />
                    </td>
                </tr>
            </table>
            
        </div>
    </form>
</body>
</html>
