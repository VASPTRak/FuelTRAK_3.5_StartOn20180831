// JScript File

   
    function KeyPressEvent_txtLast(e)
    {
    if(window.event.keyCode < 97 || window.event.keyCode > 122 )
        { 
          if(window.event.keyCode< 65 || window.event.keyCode > 90) 
          {
                window.event.keyCode=0;
          }
        }
   }
    
    function KeyPressEvent(e)
    {
        if (window.event.keyCode < 46 || window.event.keyCode > 58)
	    window.event.keyCode = 0;
    }
   
    function KeyUpEvent_txtKetexp(e)
    {
        var str = document.getElementById('txtKetexp').value;
        var d = new Date();        
        var curr_year = d.getFullYear();
        var curr_month = d.getMonth()+1;
        
        if(str.length == 2)
        {
             if(str > 12)
             {
                alert("Please enter valid month between 1 to 12");
             }
             else if(str < 1)
             {
                alert("Please enter valid month between 1 to 12");
             }
             else
             {
                document.getElementById('txtKetexp').value = str + "/";
             }
        }
        else if(str.length == 7)
        {
            var yr = str.split("/");
                     
            if(yr[1] < curr_year)
            {
                alert("Invalid expiry year");
            }
            //Check if month and year are less than current month.
            //Harshada
            //3 Apr 09
            else if(yr[0]<=curr_month && yr[1]<=curr_year)
            {
                alert("Invalid expiry month");
            }
            else
            {
                document.getElementById('txtFirstname').focus();
            }
        }
    }
    
    function KeyUpEvent_txtFirstname(e)
    {
        var str = document.getElementById('txtFirstname').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8) //back space
        {
        }
    }
    
    function KeyUpEvent_txtMI(e)
    {
        var str = document.getElementById('txtMI').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8) //back space
        {
        }
        else if(str.length == 1)
        {
            document.getElementById('txtCard').focus();
        }
    }
    
    function KeyUpEvent_txtLastName(e)
    {        var str = document.getElementById('txtLastName').value;    }
        
    function KeyUpEvent_txtCard(e)
    {
   
        var str = document.getElementById('txtCard').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8) //back space
        {
//            if(str.length == 0)           
//            {
//                 document.getElementById('txtFirstname').focus();
//            }
        }
        else if(str.length == 7)
        {
            document.getElementById('txtCardexp').focus();
        }

//alert("hi");

//if (window.event.keyCode < 46 || window.event.keyCode > 58)
//	    window.event.keyCode = 0;





    }
    
    function KeyUpEvent_txtCardexp(e)
    {
    var str = document.getElementById('txtCardexp').value;
        var d = new Date();        
        var curr_year = d.getFullYear();
        var curr_month = d.getMonth()+1;
        if(window.event.keyCode == 8)
        {
//            if(str.length == 0)
//            {
//                 document.getElementById('txtKeyexp').focus();
//            }
        }    
        else if(str.length == 2)
        {
             if(str > 12)
             {
                alert("Please enter valid month between 1 to 12");
             }
             else if(str < 1)
             {
                alert("Please enter valid month between 1 to 12");
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
                alert("Invalid expiry year");
             }
             //Check if month and year are less than current month.
            //Harshada
            //3 Apr 09
            else if(yr[0]<=curr_month && yr[1]<=curr_year)
            {
                alert("Invalid expiry month");
            }
             else
             {
                document.getElementById('txtAccId').focus();
             }
        }
    
    }
    
    function check(VehId)
        {   
        
            
            
            var ac=confirm('Are you sure you want to permanently delete this record ?');
            document.form1.Hidtxt.value=ac;//
            document.form1.txtVehId.value=VehId;
            form1.submit(); 
        }
    
    function KeyUpEvent_txtAccId(e)
    {
        var str = document.getElementById('txtAccId').value;
        if(window.event.keyCode == 8) //back space
        {
//            if(str.length == 0)           
//            {
//                 document.getElementById('txtLastName').focus();
//            }
        }       
        else if(str.length == 20)
        {
             document.getElementById('txtMI').focus();
        }
    }
    
    function SetValuetoHiddenTexybox()
    {
            document.getElementById('txtPersonnelID').value = "hidden";
            document.getElementById('txtLastname').value = "hidden";
            document.getElementById('txtAccountID').value = "hidden";
            document.getElementById('txtKeynumber').value = "hidden";
            document.getElementById('txtCardNumber').value = "hidden";
            document.getElementById('txtDepartment').value = "hidden";
    }
    
      function HideControls(i)
    {
        if(i == 0)
        {//All 
            document.getElementById('btnShowSearch').value = "hidden";
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";

            document.getElementById('txtPersonnelID').style.visibility = "hidden";
            document.getElementById('txtLastname').style.visibility = "hidden";
            document.getElementById('txtAccountID').style.visibility = "hidden";
            document.getElementById('txtKeynumber').style.visibility = "hidden";
            document.getElementById('txtCardNumber').style.visibility = "hidden";
            document.getElementById('txtDepartment').style.visibility = "hidden";

            SetValuetoHiddenTexybox();

            document.getElementById('tblSearch').style.visibility = "hidden";
            document.getElementById('tblContain1').style.visibility="hidden";
        }
        else if(i == 1)
        {//Personnel 
            document.getElementById('btnShowSearch').value = "hidden";
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            SetValuetoHiddenTexybox();
            document.getElementById('txtPersonnelID').value = "";
            document.getElementById('txtPersonnelID').style.visibility = "visible";
            document.getElementById('txtPersonnelID').focus();
            document.getElementById('txtLastname').style.visibility = "hidden";
            document.getElementById('txtAccountID').style.visibility = "hidden";
            document.getElementById('txtKeynumber').style.visibility = "hidden";
            document.getElementById('txtCardNumber').style.visibility = "hidden";
            document.getElementById('txtDepartment').style.visibility = "hidden";

            document.getElementById('btnPersonnelID').disabled = true;
            document.getElementById('btnLastname').disabled = false;
            document.getElementById('btnAccountID').disabled = false;
            document.getElementById('btnKeynumber').disabled = false;
            document.getElementById('btnCardNumber').disabled = false;
            document.getElementById('btnDepartment').disabled = false;
        }
        else if(i == 2)
        {//Lastname
            document.getElementById('btnShowSearch').value = "hidden";
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            SetValuetoHiddenTexybox();

            document.getElementById('txtLastname').value = "";
            document.getElementById('txtPersonnelID').style.visibility = "hidden";
            document.getElementById('txtLastname').style.visibility = "visible";
            document.getElementById('txtLastname').focus();
            document.getElementById('txtAccountID').style.visibility = "hidden";
            document.getElementById('txtKeynumber').style.visibility = "hidden";
            document.getElementById('txtCardNumber').style.visibility = "hidden";
            document.getElementById('txtDepartment').style.visibility = "hidden";

            document.getElementById('btnPersonnelID').disabled = false;
            document.getElementById('btnLastname').disabled = true;
            document.getElementById('btnAccountID').disabled = false;
            document.getElementById('btnKeynumber').disabled = false;
            document.getElementById('btnCardNumber').disabled = false;
            document.getElementById('btnDepartment').disabled = false;
        }
        else if(i == 3)
        {//AccountID
            document.getElementById('btnShowSearch').value = "hidden";
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            SetValuetoHiddenTexybox();
            document.getElementById('txtAccountID').value = "";
            document.getElementById('txtPersonnelID').style.visibility = "hidden";
            document.getElementById('txtLastname').style.visibility = "hidden";
            document.getElementById('txtAccountID').style.visibility = "visible";
            document.getElementById('txtAccountID').focus();
            document.getElementById('txtKeynumber').style.visibility = "hidden";
            document.getElementById('txtCardNumber').style.visibility = "hidden";
            document.getElementById('txtDepartment').style.visibility = "hidden";

            document.getElementById('btnPersonnelID').disabled = false;
            document.getElementById('btnLastname').disabled = false;
            document.getElementById('btnAccountID').disabled = true;
            document.getElementById('btnKeynumber').disabled = false;
            document.getElementById('btnCardNumber').disabled = false;
            document.getElementById('btnDepartment').disabled = false;
        }
        else if(i == 4)
        {//Keynumber
            document.getElementById('btnShowSearch').value = "hidden";
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            SetValuetoHiddenTexybox();
            document.getElementById('txtKeynumber').value = "";
            document.getElementById('txtPersonnelID').style.visibility = "hidden";
            document.getElementById('txtLastname').style.visibility = "hidden";
            document.getElementById('txtAccountID').style.visibility = "hidden";
            document.getElementById('txtKeynumber').style.visibility = "visible";
            document.getElementById('txtKeynumber').focus();
            document.getElementById('txtCardNumber').style.visibility = "hidden";
            document.getElementById('txtDepartment').style.visibility = "hidden";
            document.getElementById('btnPersonnelID').disabled = false;
            document.getElementById('btnLastname').disabled = false;
            document.getElementById('btnAccountID').disabled = false;
            document.getElementById('btnKeynumber').disabled = true;
            document.getElementById('btnCardNumber').disabled = false;
            document.getElementById('btnDepartment').disabled = false;
        }
        else if(i == 5)
        {//CardNumber
            document.getElementById('btnShowSearch').value = "hidden";
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            
            SetValuetoHiddenTexybox();
            
            document.getElementById('txtCardNumber').value = "";
            document.getElementById('txtPersonnelID').style.visibility = "hidden";
            document.getElementById('txtLastname').style.visibility = "hidden";
            document.getElementById('txtAccountID').style.visibility = "hidden";
            document.getElementById('txtKeynumber').style.visibility = "hidden";
            document.getElementById('txtCardNumber').style.visibility = "visible";
            document.getElementById('txtCardNumber').focus();
            document.getElementById('txtDepartment').style.visibility = "hidden";
            
            document.getElementById('btnPersonnelID').disabled = false;
            document.getElementById('btnLastname').disabled = false;
            document.getElementById('btnAccountID').disabled = false;
            document.getElementById('btnKeynumber').disabled = false;
            document.getElementById('btnCardNumber').disabled = true;
            document.getElementById('btnDepartment').disabled = false;
        }
        else if(i == 6)
        {//Department
            document.getElementById('btnShowSearch').value = "hidden";
            document.getElementById('btnShowSearch').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            SetValuetoHiddenTexybox();
            document.getElementById('txtDepartment').value = "";
            document.getElementById('txtPersonnelID').style.visibility = "hidden";
            document.getElementById('txtLastname').style.visibility = "hidden";
            document.getElementById('txtAccountID').style.visibility = "hidden";
            document.getElementById('txtKeynumber').style.visibility = "hidden";
            document.getElementById('txtCardNumber').style.visibility = "hidden";
            document.getElementById('txtDepartment').style.visibility = "visible";
            document.getElementById('txtDepartment').focus();
            
            document.getElementById('btnPersonnelID').disabled = false;
            document.getElementById('btnLastname').disabled = false;
            document.getElementById('btnAccountID').disabled = false;
            document.getElementById('btnKeynumber').disabled = false;
            document.getElementById('btnCardNumber').disabled = false;
            document.getElementById('btnDepartment').disabled = true;
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
    
    function ShowSearch()
    {
        document.getElementById('btnShowSearch').value = "hidden";
        document.getElementById('btnShowSearch').style.visibility = "hidden";
        document.getElementById('btnNew').style.visibility = "hidden";
        document.getElementById('tblSearch').style.visibility = "visible";
        document.getElementById('tblContain1').style.visibility="visible";
        document.getElementById('txtPersonnelID').style.visibility = "visible";
        document.getElementById('txtLastname').style.visibility = "visible";
        document.getElementById('txtAccountID').style.visibility = "visible";
        document.getElementById('txtKeynumber').style.visibility = "visible";
        document.getElementById('txtCardNumber').style.visibility = "visible";
        document.getElementById('txtDepartment').style.visibility = "visible";
    }
    
    function Tbl()
    {
        document.getElementById('tblContain1').style.visibility="hidden";
    }
   