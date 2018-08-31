<%@ Page Language="VB" ValidateRequest="false" AutoEventWireup="false" CodeFile="Key_Encoder.aspx.vb" Inherits="Key_Encoder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Key Encoder</title>
    <link href="Stylesheet/FuelTrak.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
     
        function chksesion()
        {   
            window.location.assign('LoginPage.aspx')        
        }
        
        function BtnHide(cntButton)
        {
        if (cntButton==1)
        {
            document.getElementById('btnReadKey').style.visibility="hidden";
            document.getElementById('btnReadKey1').style.visibility="hidden";
            document.getElementById('btnWriteKey').style.visibility="visible";
        }
        else if (cntButton==2)
        {
            document.getElementById('btnReadKey').style.visibility="hidden";
            document.getElementById('btnReadKey1').style.visibility="visible";
            document.getElementById('btnWriteKey').style.visibility="hidden";
        }
      }
</script>

</head>
<body class="HomeFrame">
    <object id="PortComm" classid="CLSID:00B1F81B-9D80-43B6-B817-67E961BBD397" codebase="support/PortCommunication.CAB#version=1,0,0,1"
        width="1">
    </object>
    <form id="form1" runat="server">
        <div>
            <table align="center" class="ThreeHundredFiftyPXTable">
                <tr>
                    <td colspan="2" class="MainHeader" style="height:50px">
                        <asp:Label ID="Label8" runat="server" Text="Key Encoder"></asp:Label></td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px; width:150px">
                        <asp:Label ID="lbl10" Text="Key Type :" runat="server"></asp:Label></td>
                    <td style="text-align:left; height:32px; width:200px">
                    <asp:TextBox ID="txt10" runat="server" CssClass="TenCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl1" Text="lbl1" runat="server"></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt1" runat="server" CssClass="FiveCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl2" runat="server" Text="lbl2"></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt2" runat="server" CssClass="TenCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl3" runat="server" Text="lbl3" ></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt3" runat="server" CssClass="EightCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl4" runat="server" Text="lbl4"></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt4" runat="server" CssClass="ThreeCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl5" runat="server" Text="lbl5"></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt5" runat="server" CssClass="ThreeCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl6" runat="server" Text="lbl6"></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt6" runat="server"  CssClass="ThreeCharTxtBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl7" runat="server" Text="lbl7" ></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt7" runat="server" CssClass="ThreeCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl8" runat="server" Text="lbl8" ></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt8" runat="server" CssClass="ThreeCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="StdLableTxtLeft" style="height:32px">
                        <asp:Label ID="lbl9" runat="server" Text="lbl9"></asp:Label></td>
                    <td style="text-align:left; height:32px;">
                        <asp:TextBox ID="txt9" runat="server" CssClass="EightCharTxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="height:50px">
                        <input type="button" runat="server" id="btnWriteKey" onclick="Send();" value="Encode Key" style="width: 85px" />
                        <asp:Button ID="btnReadKey1" runat="server" OnClientClick="SendReadKey();" Text="Read Key" Width="85px" /></td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <input type="button" id="btnReadKey" onclick="SendReadKey()" value="Read Key" style="display: none; width: 85px" />
                        <input type="hidden" id="txtRKey" value="!1<R>" style="width: 1px" />
                        <input type="hidden" runat="server" id="txtGKey" />
                        <input type="hidden" runat="server" id="textResult" />
                    </td>
                </tr>
            </table>
        </div>
    </form>


    <script type="text/javascript" language="JavaScript">
        
        var objComport 
        var intPortID
        var alertTimerId
        var lngStatus
        var strError 
        var strData 
        var strPort  
        var strSetting  
        var LINE_BREAK = 1
        var LINE_DTR = 2
        var LINE_RTS = 3
        var strRecieveData
        
         //document.getElementById('downLink').style.visibility="hidden";
    
      function Send()
      {
          
          
	          try
	          {
	                objComport = new ActiveXObject("PortCommunication.PortComm");
                    //document.getElementById('textResult').value = "";
                    
                    strData = document.getElementById('txtGKey').value +"\r";
                    intPortID = <%=right(Session("COMM"),1)%>;
                    //alert(intPortID);
                    strPort = "COM" + intPortID;
                   
                    strSetting = "baud=" + 1200 + " parity=N data=8 stop=1";
                    //document.getElementById('textResponse').value="";
                    lngStatus = objComport.CommOpen(intPortID, strPort , strSetting);
                  
                                       
	                if (lngStatus != 0 )
		            {	
			             lngStatus = objComport.CommGetError(strError)
			             alert("COM Error: "+ strPort);
		            }    
	                // Set modem control lines.
    	            lngStatus = objComport.CommSetLine(intPortID, LINE_RTS, true)
    	            lngStatus = objComport.CommSetLine(intPortID, LINE_DTR, true)
            		
		             // Write data to serial port.
		            lngSize = strData.length;
		            lngStatus = objComport.CommWrite(intPortID, strData)
		            
                    objComport.WaitProg(1000);
                    
		             //Read maximum of 64 bytes from serial port.
	                lngStatus = objComport.CommRead(intPortID, strData, 64)
	               
	                strRecieveData = objComport.returnString()
	                //document.getElementById('textResponse').value = strRecieveData;
            		
		            if (lngStatus > 0 )
		            {  
		                var a =strRecieveData.substring(0,1);
	                //alert(a);
            		    if ( a == "-")
            		    {
            		        alert('Bad write !!!');
            		        document.getElementById('textResult').value = "Error"; 
            		        for(var i=1; i<=10; i++) {var txt = 'txt'+i; document.getElementById(txt).value ="";}
            		    }
            		    else if ( a == "+")
            		    {   
            		         
		                    alert('Key has been written successfully !!!');
		                    document.getElementById('textResult').value = "Successfully Read";  
		                    
		                    
		                    
		                    
		                                         
                         
		                }
		            }
		            else
		            {  
		                alert("Error to Read Data.");
		                document.getElementById('textResponse').value="";   
		                for(var i=1; i<=10; i++) {var txt = 'txt'+i; document.getElementById(txt).value ="";} 
		            }
                
	                // Reset modem control lines.
	                lngStatus = objComport.CommSetLine(intPortID, LINE_RTS, false)
    	            lngStatus = objComport.CommSetLine(intPortID, LINE_DTR, false)

    	            //Close communications.
	                objComport.CommClose(intPortID)
		        }
		        catch(err)
		        {
		            if(objComport == null)
                    {   
                   
                        alert("ActiveX component not installed.");
                        for(var i=1; i<=10; i++) {var txt = 'txt'+i; alert(txt);}
                    }
                    else
                    {   objComport.CommClose(intPortID)}
		        }
		  
      }
      function SendReadKey ()
      {
              try
	          {
	                objComport = new ActiveXObject("PortCommunication.PortComm");
	                
                    strData = document.getElementById('txtRKey').value +"\r";
                    
                    
                    intPortID = <%=right(session("COMM"),1)%>;
                    //alert(intPortID);
                    strPort = "COM" + intPortID;
                    
                    strSetting = "baud=" + 1200 + " parity=N data=8 stop=1";
                    lngStatus = objComport.CommOpen(intPortID, strPort , strSetting);
                    
	                if (lngStatus != 0 )
		            {	
			             lngStatus = objComport.CommGetError(strError)
			             alert("COM Error: "+ strPort);
			             for(var i=1; i<=10; i++) {var txt = 'txt'+i; document.getElementById(txt).value ="";}
		            }    
	                // Set modem control lines.
    	            lngStatus = objComport.CommSetLine(intPortID, LINE_RTS, true)
    	            lngStatus = objComport.CommSetLine(intPortID, LINE_DTR, true)
            		
		             // Write data to serial port.
		            lngSize = strData.length;
		            lngStatus = objComport.CommWrite(intPortID, strData)
            		
                    objComport.WaitProg(2000);
		            //Read maximum of 64 bytes from serial port.
		            //alert(strData);
	                lngStatus = objComport.CommRead(intPortID, strData, 64)
	              
	                strRecieveData = objComport.returnString()
	                
	                var a =strRecieveData.substring(0,1);
	                
            		if ( a != "-")
            		{
		                if (lngStatus > 0)
		                {  
		                    if (strRecieveData.substring(17,18)=="0")
		                    {   
		                   
		                      document.getElementById('txt10').value = 'Vehicle';
		                   
		                         
		                         
		                        document.getElementById('txt1').value=strRecieveData.substring(2,7);
		                        //Identity
		                        document.getElementById('txt2').value=strRecieveData.substring(7,17);
		                        document.getElementById('txt3').value="Never";
		                        
		                        document.getElementById('txt4').value=strRecieveData.substring(20,24);
		                        if (strRecieveData.substring(34,35)=="0")
		                        {document.getElementById('txt5').value="No";}
		                        else if (strRecieveData.substring(34,35)=="1"){document.getElementById('txt5').value="Yes";}
    		                   
		                        if (strRecieveData.substring(19,20)== "0")
		                        {
		                       
		                        document.getElementById('txt6').value="No";}
		                        else if (
		                        
		                        strRecieveData.substring(19,20)=="1")
		                        {
		                        
		                        document.getElementById('txt6').value="Yes";
		                        
		                        }
    		                    
		                        if (strRecieveData.substring(19,20)== "0")
		                        {document.getElementById('txt7').value="No";}
		                        else if (strRecieveData.substring(19,20)=="1"){document.getElementById('txt7').value="Yes";}

		                        document.getElementById('txt8').value=strRecieveData.substring(24,27);
		                        document.getElementById('txt10').value = 'Vehicle';
		                        
		                        
    		                    
		                        document.getElementById('txt5').style.display='';
		                        document.getElementById('txt6').style.display='';
		                        document.getElementById('txt7').style.display='';
		                        document.getElementById('txt8').style.display='';
		                        document.getElementById('txt9').style.display='';
		                        document.getElementById('lbl5').style.visibility = '';
		                        document.getElementById('lbl6').style.visibility = '';
		                        document.getElementById('lbl7').style.visibility = '';
		                        document.getElementById('lbl8').style.visibility = '';
		                        document.getElementById('lbl9').style.visibility = '';
		                    }
		                    else if (strRecieveData.substring(17,18)=="1")
		                    {
		                          
		                        document.getElementById('txt1').value=strRecieveData.substring(2,7);
		                        document.getElementById('txt2').value=strRecieveData.substring(7,17);
		                        document.getElementById('txt3').value="Never";
		                        document.getElementById('txt4').value=strRecieveData.substring(20,24);
		                        document.getElementById('txt5').style.display='none';
		                        document.getElementById('txt6').style.display='none';
		                        document.getElementById('txt7').style.display='none';
		                        document.getElementById('txt8').style.display='none';
		                        document.getElementById('txt9').style.display='none';
		                        document.getElementById('txt10').value = 'Personnel';
		                       
		                        document.getElementById('lbl5').style.visibility = 'hidden';
		                        document.getElementById('lbl6').style.visibility = 'hidden';
		                        document.getElementById('lbl7').style.visibility = 'hidden';
		                        document.getElementById('lbl8').style.visibility = 'hidden';
		                        document.getElementById('lbl9').style.visibility = 'hidden';
		                    }
		                }
		                else
		                {  
		                    alert("Error to Read Data.");
		                    for(var i=1; i<=10; i++) {var txt = 'txt'+i; document.getElementById(txt).value ="";}
		                }
		            }
                    else
	                {  
	                    alert("Error to Read Data.");
	                    for(var i=1; i<=10; i++) {var txt = 'txt'+i; document.getElementById(txt).value ="";}
	                }
	                // Reset modem control lines.
	                lngStatus = objComport.CommSetLine(intPortID, LINE_RTS, false)
    	            lngStatus = objComport.CommSetLine(intPortID, LINE_DTR, false)

    	            //Close communications.
	                objComport.CommClose(intPortID)
		        }
		        catch(err)
		        {
		        
    		     if(objComport == null)
                    {
                        alert("ActiveX component not installed.");
                        for(var i=1; i<=10; i++) {var txt = 'txt'+i; document.getElementById(txt).value ="";}
                    }
                    else
                    {   objComport.CommClose(intPortID) }
		        }
      }
      
      function pause(milis)
      {
          var d=new Date()
          var curdate=null;
          do{
             curdate=new Date();
            }while(curdate-d<milis)
      }
      
      function pagerefresh()
      {  document.getElementById('btnWriteKey').value="Refresh";   }
      
      
    </script>

</body>
</html>
