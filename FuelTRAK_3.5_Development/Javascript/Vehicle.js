// JScript File

function Msg()
{
    alert(document.getElementById('txtPMValue').value);
}                                                                 

    function KeyUpEvent_txtKeyexp(e)
    {
        var str = document.getElementById('txtKeyexp').value;
        var d = new Date();
        
        var curr_year = d.getFullYear();
        
        //alert(window.event.keyCode);
        if(str.length == 2)
        {
             if(str > 12)
             {
                alert("Please enter valid month between 1 to 12");
             }
             else if(str < 1)
             {
                alert("Please enter valid month");
             }             
             else
             {
                document.getElementById('txtKeyexp').value = str + "/";
             }
        }
        else if(str.length == 7)
        {
             var yr = str.split("/");
                         
             if(yr[1] < curr_year)
             {
                alert("Invalid Expiration");
             }
             else
             {
                document.getElementById('txtCard').focus();
             }
        }
    }

    function KeyUpEvent_txtCard(e)
    {
        var str = document.getElementById('txtCard').value;
        
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
                 document.getElementById('txtKeyexp').focus();
            }
        } 
        //Commented By Varun   
//        else if(str.length == 8)
//        {
//             document.getElementById('txtCardexp').focus();
//        }
    }

    function KeyUpEvent_txtCardexp(e)
    {
        var str = document.getElementById('txtCardexp').value;
        var d = new Date();        
        var curr_year = d.getFullYear();
        
        //alert(window.event.keyCode);
        if(str.length == 2)
        {
             if(str > 12)
             {
                 alert("Please enter valid month between 1 to 12");
             }
             else if(str < 1)
             {
                alert("Please enter valid month");
             }             
             else
             {
                document.getElementById('txtCardexp').value = str + "/";
             }
        }
        else if(str.length == 7)
        {
             var yr = str.split("/");
                         
             if(yr[1] < curr_year)
             {
                alert("Invalid Expiration");
             }
             else
             {
                document.getElementById('txtCard').focus();
             }
        }

//////        if(window.event.keyCode == 8)
//////        {
////////            if(str.length == 0)
////////            {
////////                 document.getElementById('txtKeyexp').focus();
////////            }
//////        }    
//////        else if(str.length == 2)
//////        {
//////             if(str > 12)
//////             {
//////                alert("Please enter valid month between 1 to 12");
//////             }
//////             else if(str < 1)
//////             {
//////                alert("Please enter valid month between 1 to 12");
//////             }
//////             else
//////             {
//////                document.getElementById('txtCardexp').value = str + "/";
//////             }
//////        }
//////        else if(str.length == 7)
//////        {
//////             var yr = str.split("/");
//////                         
//////             if(yr[1] < curr_year)
//////             {
//////                alert("Invalid Expiration");
//////             }
//////             else
//////             {
//////                document.getElementById('txtYear').focus();
//////             }
//////        }
    }

    function KeyPressEvent_Year(e)
    {
      var str = document.getElementById('txtYear').value;
        //alert(window.event.keyCode);
        if (window.event.keyCode < 48 || window.event.keyCode > 57)
        {
//             if(str.length == 4)
//             {
                 window.event.keyCode = 0;
//             }
        }
        else if (window.event.keyCode == 13)
        {
            document.getelementbyid('btnsearch').focus();
        }
	     
      //  if(window.event.keyCode == 8)
      //  {
//            if(str.length == 0)
//            {
//                 document.getElementById('txtCardexp').focus();
//            }
      //  }    
        else if(str.length == 4)
        {
        window.event.keyCode = 0;
          
        }
    }
    
    function KeyUpEvent_txtYear(e)
    {
        var str = document.getElementById('txtYear').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
//            if(str.length == 0)
//            {
//                 document.getElementById('txtCardexp').focus();
//            }
        }    
        else if(str.length == 4)
        {
             document.getElementById('txtMake').focus();
        }
    }
    
    function KeyUpEvent_txtType(e)
    {
        var str = document.getElementById('txtType').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
//            if(str.length == 0)
//            {
//                 document.getElementById('txtYear').focus();
//            }
        }    
//////        else if(str.length == 3)
//////        {
//////             document.getElementById('txtVinNumber').focus();
//////        }
    }
    
    function KeyUpEvent_txtSubdepartment(e)
    {
        var str = document.getElementById('txtSubdepartment').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
//            if(str.length == 0)
//            {
//                 document.getElementById('DDLstDepartment').focus();
//            }
        } 
        //Commented By Varun  
//        else if(str.length == 6)
//        {
//             document.getElementById('txtFuellimit').focus();
//        }
    }
    
    function KeyPressEvent_txtFuellimit(e)
    {        
	   if (window.event.keyCode < 47 || window.event.keyCode > 57)
	    window.event.keyCode = 0;
    }

    function KeyUpEvent_txtFuellimit(e)
    {        
        var str = document.getElementById('txtFuellimit').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
        } 
    }
    
        
        
        
        function HideControls(i)
        {
        if(i == 0)
        {
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";

            document.getElementById('txtVehicleID').style.visibility = "visible";
            document.getElementById('txtLicenseNo').style.visibility = "visible";
            document.getElementById('txtMake').style.visibility = "visible";
            document.getElementById('txtModel').style.visibility = "visible";
            document.getElementById('txtKeyNo').style.visibility = "visible";
            document.getElementById('txtCardNo').style.visibility = "visible";
            document.getElementById('txtDepartment').style.visibility = "visible";
            document.getElementById('txtDescription').style.visibility = "visible";
            document.getElementById('txtVinNumber').style.visibility = "visible";
            document.getElementById('txtYear').style.visibility = "visible";

            document.getElementById('tblContain1').style.visibility="visible";
            
            document.getElementById('txtVehicleID').value = "";
            document.getElementById('txtLicenseNo').value = "";
            document.getElementById('txtMake').value = "";
            document.getElementById('txtModel').value = "";
            document.getElementById('txtKeyNo').value = "";
            document.getElementById('txtCardNo').value = "";
            document.getElementById('txtDepartment').value = "";
            document.getElementById('txtDescription').value = "";
            document.getElementById('txtVinNumber').value = "";
            document.getElementById('txtYear').value = "";
        }
        else if(i == 1)
        {
        document.getElementById('txtVehicleID').value = "";
        document.getElementById('txtVehicleID').style.visibility = "visible";
        document.getElementById('txtVehicleID').focus();
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = true;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 2)
        {
        document.getElementById('txtLicenseNo').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "visible";
        document.getElementById('txtLicenseNo').focus();
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = true;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 3)
        {
        document.getElementById('txtMake').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "visible";
        document.getElementById('txtMake').focus();
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = true;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 4)
        {
        document.getElementById('txtModel').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "visible";
        document.getElementById('txtModel').focus();
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = true;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 5)
        {
        document.getElementById('txtKeyNo').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "visible";
        document.getElementById('txtKeyNo').focus();
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = true;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }

        else if(i == 6)
        {
        document.getElementById('txtCardNo').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "visible";
        document.getElementById('txtCardNo').focus();
        document.getElementById('btnSearch').focus();
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled=true;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 7)
        {
        document.getElementById('txtDepartment').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "visible";
        document.getElementById('txtDepartment').focus();
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = true;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 8)
        {
        document.getElementById('txtDescription').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "visible";
        document.getElementById('txtDescription').focus();
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = true;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 9)
        {
        document.getElementById('txtVinNumber').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "visible";
        document.getElementById('txtVinNumber').focus();
        document.getElementById('txtYear').style.visibility = "hidden";

        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = true;
        document.getElementById('btnYear').disabled = false;
        }
        else if(i == 10)
        {
        document.getElementById('txtYear').value = "";
        document.getElementById('txtVehicleID').style.visibility = "hidden";
        document.getElementById('txtLicenseNo').style.visibility = "hidden";
        document.getElementById('txtMake').style.visibility = "hidden";
        document.getElementById('txtModel').style.visibility = "hidden";
        document.getElementById('txtKeyNo').style.visibility = "hidden";
        document.getElementById('txtCardNo').style.visibility = "hidden";
        document.getElementById('txtDepartment').style.visibility = "hidden";
        document.getElementById('txtDescription').style.visibility = "hidden";
        document.getElementById('txtVinNumber').style.visibility = "hidden";
        document.getElementById('txtYear').style.visibility = "visible";
        document.getElementById('txtYear').focus();


        document.getElementById('btnVehicleID').disabled = false;
        document.getElementById('btnLicenseNo').disabled = false;
        document.getElementById('btnMake').disabled = false;
        document.getElementById('btnModel').disabled = false;
        document.getElementById('btnKeyNo').disabled = false;
        document.getElementById('btnCardNo').disabled = false;
        document.getElementById('btnDepartment').disabled = false;
        document.getElementById('btnDescription').disabled = false;
        document.getElementById('btnVinNumber').disabled = false;
        document.getElementById('btnYear').disabled = true;
        }

        function SetValues(j)
        {
        document.getElementById('txtVehicleID').value = "hidden";
        document.getElementById('txtLicenseNo').value = "hidden";
        document.getElementById('txtMake').value = "hidden";
        document.getElementById('txtModel').value = "hidden";
        document.getElementById('txtKeyNo').value = "hidden";
        document.getElementById('txtCardNo').value = "hidden";
        document.getElementById('txtDepartment').value = "hidden";
        document.getElementById('txtDescription').value = "hidden";
        document.getElementById('txtVinNumber').value = "hidden";
        document.getElementById('txtYear').value = "hidden";

        if(j == 1)
        document.getElementById('txtVehicleID').value = "";
        else if(j == 2)
        document.getElementById('txtLicenseNo').value = "";
        else if(j == 3)
        document.getElementById('txtMake').value = "";
        else if(j == 4)
        document.getElementById('txtModel').value = "";
        else if(j == 5)
        document.getElementById('txtKeyNo').value = "";
        else if(j == 6)
        document.getElementById('txtCardNo').value = "";
        else if(j == 7)
        document.getElementById('txtDepartment').value = "";
        else if(j == 8)
        document.getElementById('txtDescription').value = "";
        else if(j == 9)
        document.getElementById('txtVinNumber').value = "";
        else if(j == 10)
        document.getElementById('txtYear').value = "";
        }
        }

        function Tbl()
        {        document.getElementById('tblContain1').style.visibility="hidden";    }
        
        
       function ShowSearch()
        {
        document.getElementById('btnNew').style.visibility = "hidden";
        document.getElementById('tblSearch').style.visibility = "visible";
        document.getElementById('btnShowSearch').style.visibility = "hidden";
        document.getElementById('tblContain1').style.visibility="visible";
        document.getElementById('txtVehicleID').value = "";
        document.getElementById('txtLicenseNo').value = "";
        document.getElementById('txtMake').value = "";
        document.getElementById('txtModel').value = "";
        document.getElementById('txtKeyNo').value = "";
        document.getElementById('txtCardNo').value = "";
        document.getElementById('txtDepartment').value = "";
        document.getElementById('txtDescription').value = "";
        document.getElementById('txtVinNumber').value = "";
        document.getElementById('txtYear').value = "";
        }  
	    
 //Commented By Varun Moota.
 
 function All(Id)
    {  

        var id = document.getElementById('txtVehicleID').value
        
       var r=confirm("Warning. Changing Vehicle ID may result in incorrect vehicle reporting.");
        if (r==true)
         {
           document.getElementById('txtMake').focus();
         }
       else
        {
        document.getElementById('txtVehicleID').value = id
     
        }
   

    }
    
     function getResult()
     {

        var selectedItemList = getListBoxSelections('<%= lstBox1.ClientID %>');
        var selectedItemArray = selectedItemList.split(',');
        for (var i=0; i<selectedItemArray.length; i++)
        {
          var selectedItem = selectedItemArray[i];
          alert('#' + i + ': ' + selectedItem);
          AddItems(selectedItem);

         }
     }
          
      function getListBoxSelections(listBoxId)
      {
        var listBoxRef = document.getElementById(listBoxId);
        var functionReturn = '';
          for (var i = 0; i < listBoxRef.options.length; i++)
         {
          if (listBoxRef.options[i].selected)
          {

            if (functionReturn.length > 0)
            functionReturn += ',';

            // If you want the text property use this:
            //functionReturn += listBoxRef.options[i].text;


            // If you want the value property use this:
            functionReturn += listBoxRef.options[i].value;

          }
        }

            return functionReturn;
      }

        function AddItems(txtValue) 
        {
            
            var mySel = document.getElementById('<%= lstBox2.ClientID %>'); // Listbox Name
            //var myDSel = document.getElementById('<%= lstBox1.ClientID %>'); // Listbox Name
            var myOption;

            myOption = document.createElement("Option");
            myOption.text = txtValue.toString();
            myOption.value = txtValue.toString();
            alert(myOption.value);
            mySel.add(myOption);
            //myDSel.remove(myOption);
        }
