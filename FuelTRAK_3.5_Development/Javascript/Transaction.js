// JScript File
     
    function KeyPressYear(e)
    {
        var str = document.getElementById('txtYear').value;
        
        if (window.event.keyCode < 48 || window.event.keyCode > 57)
        { window.event.keyCode = 0;}
        
        else if(str.length == 4)
        { window.event.keyCode = 0;}
    }
        
    function KeyUpEvent_txtOdometer(e)
    {
        var str = document.getElementById('txtOdometer').value;
        if(str.length == 6)
        {
             document.getElementById('txtHours').focus();
        }
    }
    
    function KeyUpEvent_txtHours(e)
    {
        var str = document.getElementById('txtHours').value;

        if(str.length == 6)
        {
             document.getElementById('txtTime').focus();
        }
    }

    function KeyUpEvent_txtCost(e)
    {
        var str = document.getElementById('txtCost').value;

        if(str.length == 6)
        {           document.getElementById('txtCost').value = str + ".";        }
    }

   
    function KeyPressEvent_txtQty(e)
    {
     
       if (window.event.keyCode != 46 && (window.event.keyCode < 48 || window.event.keyCode > 57))
        { window.event.keyCode = 0;}    
        var cnt;
        cnt=0;
        var str = document.getElementById('txtQty').value; 
          
         for (i=0;i<str.length;i++)
        {
            if(str.charAt(i) == ".")
            {    cnt = cnt + 1;                }
        }
       
        if(str.length == 5)
        {
        
            if(cnt == 0)
            {
                document.getElementById('txtQty').value = str + ".";
                cnt = cnt + 1;
            }
        }               
    }

    
    function KeyUpEvent_txtQty1(e)
    {
   
        var str = document.getElementById('txtQty').value;
        var price=document.getElementById('txtHideTankPrice').value;
        var count = 0;
        var valtext;
        valtext=document.getElementById('txtQty').value;
         for(var i=0;i < valtext.length;i++)
        {   if(valtext.substring(i,i+1) == '.')
            count++;
        }
         if (count > 1)
        {   count=0;
            for(var i=0;i < valtext.length;i++)
            {   
               if(valtext.substring(i,i+1) == '.')
                {   count++;
                    if(count > 1)
                    {                            
                        var pos=valtext.lastIndexOf('.');
                        valtext=valtext.substring(0,pos);   
                    }                                           
                }
            }
            
            document.getElementById('txtQty').value=valtext;            
        }    
         if (count == 1)              
          {
              var pos=document.getElementById('txtQty').value;
              var splittxt=pos.split('.');
              
              if (parseInt(splittxt[1]).toString().length > 3)
              { document.getElementById('txtQty').value=splittxt[0] + "." + splittxt[1].substring(0,3);               }
          }
        

         var str = document.getElementById('txtQty').value;
         
       if(price!='')
       {
            
          document.getElementById('txtCost').value = (parseFloat(str) * parseFloat(price)).toFixed(2);
            
        }
        if(document.getElementById('txtCost').value == "NaN")
        {
            document.getElementById('txtCost').value="";
        }
    }
    
    
    function Lost_Focus_Date1()
    {
        var str = document.getElementById('DateTextBox1').value;
        if (!(ValidateDate(str)))
             {
                 document.getElementById('DateTextBox1').focus();
             }
    }
    
      function Lost_Focus_Date2()
    {
        var str = document.getElementById('DateTextBox2').value;
        if (!(ValidateDate(str)))
             {
                 document.getElementById('DateTextBox2').focus();
             }
    }
    
    //to check the size of date
    function KeyPressEvent_sizeofdate(e)
    {
       if (window.event.keyCode < 46 || window.event.keyCode > 58)
	    
	    window.event.keyCode = 0;
	    
        var str = document.getElementById('txtDate').value;
        
        if(str.length==10)
        {
            window.event.keyCode = 0;
        }
    }
    
    var min,max
    
    function KeyUpEvent_txtVehicle(e,min,max)
    {
        var str = document.getElementById('txtVehicle').value;
        if(str < min)
        {
             alert ('Vehicle id:' + str + ' is not valid');
             alert ('Vehicles valid between range of ' + min + ' to ' + max);
        }
        else if(str > max)
        {
             alert ('Vehicle id:' + str + ' is not valid');
             alert ('Vehicles valid between range of ' + min + ' to ' + max);
        }
        else if(str.length == 2)
        {
             document.getElementById('txtOdometer').focus();
        }        
    }
     

    function HideControls(i)
    {
        if(i == -1)
        {
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            
            document.getElementById('txtDesc').value = "hidden";
            document.getElementById('txtLicNo').value = "hidden";
            document.getElementById('txtVechCrdNo').value = "hidden";
            document.getElementById('txtVechKeyNo').value = "hidden";
            document.getElementById('txtVechMake').value = "hidden";
            document.getElementById('txtVechModel').value = "hidden";
            document.getElementById('txtVINNo').value = "hidden";
            document.getElementById('txtxDept').value = "hidden";
            document.getElementById('txtYear').value = "hidden";
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
        }
        else if(i == 0)
        {
//            document.getElementById('txtDesc').value = "hidden";
//            document.getElementById('txtLicNo').value = "hidden";
//            document.getElementById('txtVechCrdNo').value = "hidden";
//            document.getElementById('txtVechKeyNo').value = "hidden";
//            document.getElementById('txtVechMake').value = "hidden";
//            document.getElementById('txtVechModel').value = "hidden";
//            document.getElementById('txtVINNo').value = "hidden";
//            document.getElementById('txtxDept').value = "hidden";
//            document.getElementById('txtYear').value = "hidden";
//            document.getElementById('txtDesc').style.visibility = "hidden";
//            document.getElementById('txtLicNo').style.visibility = "hidden";
//            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
//            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
//            document.getElementById('txtVechMake').style.visibility = "hidden";
//            document.getElementById('txtVechModel').style.visibility = "hidden";
//            document.getElementById('txtVINNo').style.visibility = "hidden";
//            document.getElementById('txtxDept').style.visibility = "hidden";
//            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('tblSearch').style.visibility = "hidden";
        }
        else if(i == 1)
        {
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
            document.getElementById('btnSearch').focus();
        }
        else if(i == 2)
        {
            document.getElementById('txtDesc').style.visibility = "visible";
            document.getElementById('txtDesc').focus();
            document.getElementById('txtDesc').value = "";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('btnDesc').disabled = true;
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        else if(i == 3)
        {
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "visible";
            document.getElementById('txtLicNo').focus();
            document.getElementById('txtLicNo').value = "";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = true;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        else if(i == 4)
        {
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        else if(i == 5)
        {
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        else if(i == 6)
        {
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        
        else if(i == 7)
        {
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "visible";
            document.getElementById('txtVechCrdNo').focus();
            document.getElementById('txtVechCrdNo').value = "";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = true;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        
        else if(i == 8)
        {
            ////document.getElementById('txtDate').style.visibility = "hidden";
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
//            document.getElementById('txtPersCrdNo').style.visibility = "hidden";
//            document.getElementById('txtPersKeyNo').style.visibility = "hidden";
            ////document.getElementById('txtSentry').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            //document.getElementById('txtVechID').style.visibility = "visible";
            //document.getElementById('txtVechID').focus();
            //document.getElementById('txtVechID').value = "";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            
            //document.getElementById('btnDate').disabled = false;
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
//            document.getElementById('btnPersCrdNo').disabled = false;
//            document.getElementById('btnPersKey').disabled = false;
            //document.getElementById('btnSentry').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            //document.getElementById('btnVechid').disabled = true;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        
        else if(i == 9)
        {
            ////document.getElementById('txtDate').style.visibility = "hidden";
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
//            document.getElementById('txtPersCrdNo').style.visibility = "hidden";
//            document.getElementById('txtPersKeyNo').style.visibility = "hidden";
            ////document.getElementById('txtSentry').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            ////document.getElementById('txtVechID').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "visible";
            document.getElementById('txtVechKeyNo').focus();
            document.getElementById('txtVechKeyNo').value = "";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            
            //document.getElementById('btnDate').disabled = false;
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
//            document.getElementById('btnPersCrdNo').disabled = false;
//            document.getElementById('btnPersKey').disabled = false;
            //document.getElementById('btnSentry').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            //document.getElementById('btnVechid').disabled = false;
            document.getElementById('btnVechKey').disabled = true;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
            document.getElementById('btnSearch').focus();
            document.getElementById('txtVechKeyNo').focus();
        }
        
        else if(i == 10)
        {
            ////document.getElementById('txtDate').style.visibility = "hidden";
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
//            document.getElementById('txtPersCrdNo').style.visibility = "hidden";
//            document.getElementById('txtPersKeyNo').style.visibility = "hidden";
            ////document.getElementById('txtSentry').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            ////document.getElementById('txtVechID').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "visible";
            document.getElementById('txtVechMake').focus();
            document.getElementById('txtVechMake').value = "";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            
            //document.getElementById('btnDate').disabled = false;
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
//            document.getElementById('btnPersCrdNo').disabled = false;
//            document.getElementById('btnPersKey').disabled = false;
            //document.getElementById('btnSentry').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            //document.getElementById('btnVechid').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = true;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        
        else if(i == 11)
        {
            ////document.getElementById('txtDate').style.visibility = "hidden";
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
//            document.getElementById('txtPersCrdNo').style.visibility = "hidden";
//            document.getElementById('txtPersKeyNo').style.visibility = "hidden";
            ////document.getElementById('txtSentry').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            ////document.getElementById('txtVechID').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "visible";
            document.getElementById('txtVechModel').focus();
            document.getElementById('txtVechModel').value = "";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            
            //document.getElementById('btnDate').disabled = false;
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
//            document.getElementById('btnPersCrdNo').disabled = false;
//            document.getElementById('btnPersKey').disabled = false;
            //document.getElementById('btnSentry').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            //document.getElementById('btnVechid').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = true;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        
        else if(i == 12)
        {
            ////document.getElementById('txtDate').style.visibility = "hidden";
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
//            document.getElementById('txtPersCrdNo').style.visibility = "hidden";
//            document.getElementById('txtPersKeyNo').style.visibility = "hidden";
            ////document.getElementById('txtSentry').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            ////document.getElementById('txtVechID').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "visible";
            document.getElementById('txtVINNo').focus();
            document.getElementById('txtVINNo').value = "";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "hidden";
            
            //document.getElementById('btnDate').disabled = false;
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
//            document.getElementById('btnPersCrdNo').disabled = false;
//            document.getElementById('btnPersKey').disabled = false;
            //document.getElementById('btnSentry').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            //document.getElementById('btnVechid').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = true;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = false;
        }
        
        else if(i == 13)
        {
            ////document.getElementById('txtDate').style.visibility = "hidden";
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
//            document.getElementById('txtPersCrdNo').style.visibility = "hidden";
//            document.getElementById('txtPersKeyNo').style.visibility = "hidden";
            ////document.getElementById('txtSentry').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            ////document.getElementById('txtVechID').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "visible";
            document.getElementById('txtxDept').focus();
            document.getElementById('txtxDept').value = "";
            document.getElementById('txtYear').style.visibility = "hidden";
            
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = true;
            document.getElementById('btnYear').disabled = false;
        }
         
        else if(i == 14)
        {
            document.getElementById('txtDesc').style.visibility = "hidden";
            document.getElementById('txtLicNo').style.visibility = "hidden";
            document.getElementById('txtVechCrdNo').style.visibility = "hidden";
            document.getElementById('txtVechKeyNo').style.visibility = "hidden";
            document.getElementById('txtVechMake').style.visibility = "hidden";
            document.getElementById('txtVechModel').style.visibility = "hidden";
            document.getElementById('txtVINNo').style.visibility = "hidden";
            document.getElementById('txtxDept').style.visibility = "hidden";
            document.getElementById('txtYear').style.visibility = "visible";
            document.getElementById('txtYear').value = "";
            document.getElementById('txtYear').focus();
            document.getElementById('btnDesc').disabled = false;
            document.getElementById('btnLicNo').disabled = false;
            document.getElementById('btnVechCrdNo').disabled = false;
            document.getElementById('btnVechKey').disabled = false;
            document.getElementById('btnVechMake').disabled = false;
            document.getElementById('btnVechModel').disabled = false;
            document.getElementById('btnVINNo').disabled = false;
            document.getElementById('btnDept').disabled = false;
            document.getElementById('btnYear').disabled = true;
        }
    }
    function SetValues(j)
    {
        document.getElementById('txtPersonnelID').value = "hidden";
        document.getElementById('txtLastname').value = "hidden";
        document.getElementById('txtAccountID').value = "hidden";
        document.getElementById('txtKeynumber').value = "hidden";
        document.getElementById('txtCardNumber').value = "hidden";
        document.getElementById('txtDepartment').value = "hidden";
        
        if(j == 1)
            document.getElementById('txtPersonnelID').value = "";
        else if(j == 2)
            document.getElementById('txtLastname').value = "";
        else if(j == 3)
            document.getElementById('txtAccountID').value = "";
        else if(j == 4)
            document.getElementById('txtKeynumber').value = "";
        else if(j == 5)
            document.getElementById('txtCardNumber').value = "";
        else if(j == 6)
            document.getElementById('txtDepartment').value = "";
    }
 //   Comented By Varun Moota
//    function ShowSearch()
//    {
//        document.getElementById('btnShowSearch').style.visibility = "hidden";
//        document.getElementById('btnNew').style.visibility = "hidden";
//        document.getElementById('btnShowSearch').style.visibility = "hidden";
//        document.getElementById('btnNew').style.visibility = "hidden";
//        document.getElementById('tblSearch').style.visibility = "visible";
//    }
    
    function Tbl()
    {
        //document.getElementById["tblContain1"].style.visibility="hidden";
        //document.getElementById["tblContain2"].style.visibility="hidden";
    }
    
    function NoRecord()
    {
    alert("No Record Found");
    }
  
  
    function ShowSearch()
    {
    document.getElementById('btnNew').style.visibility = "hidden";
    document.getElementById('btnShowSearch').style.visibility = "hidden";
    document.getElementById('tblSearch').style.visibility="visible";

    }