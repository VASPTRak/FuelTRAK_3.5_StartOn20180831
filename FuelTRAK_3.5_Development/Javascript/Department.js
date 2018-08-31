// JScript File

 //Commented By Varun Moota to Handle Surcharge.
//    function KeyUpEvent_txtSurchage(e)
//    {
//        var str = document.getElementById('txtSurchage').value;
//        if(str.length == 1)
//        {
//             document.getElementById('txtSurchage').value = str + "."
//        }
//        else if(str.length == 6)
//        {
//            document.getElementById('txtAccNo').focus();
//        }
//    }
//    
    function Alrt()
    {
        alert("Record Updated Successfully");
    }
   
   function KeyPressEvent(e)
    {
    alert(window.event.keyCode);
    if(window.event.keyCode < 97 || window.event.keyCode > 122 )
        { 
          if(window.event.keyCode< 65 || window.event.keyCode > 90) 
          {
                window.event.keyCode=0;
          }
        }
   }
   
                    function chksesion()
                    {   window.location.assign('LoginPage.aspx')        }
                    


                   
                    
                    function HideControls(i)
                    {
                            if(i == -1)
                            {
                                document.getElementById('txtDepartmentName').value="hidden";
                                document.getElementById('txtDepartmentNumber').value="hidden";
                                document.getElementById('txtDepartmentName').style.visibility = "hidden";
                                document.getElementById('txtDepartmentNumber').style.visibility = "hidden";
                                document.getElementById('btnNew').style.visibility = "hidden";
                                document.getElementById('btnSearchshow').style.visibility = "hidden";
                            }
                            else if(i == 1)
                            {
                                document.getElementById('tblSearch').style.visibility = "hidden";
                                document.getElementById('txtDepartmentName').style.visibility = "hidden";
                                document.getElementById('txtDepartmentNumber').style.visibility = "hidden";
                            }
                            else if(i == 2){   document.getElementById('tblSearch').style.visibility = "hidden";}
                            
                            else if(i == 3)
                            {
                                document.getElementById('tblSearch').style.visibility = "visible";
                                document.getElementById('tblSearch').focus();
                                document.getElementById('txtDepartmentName').style.visibility = "hidden";
                                document.getElementById('txtDepartmentNumber').style.visibility = "hidden";
                                document.getElementById('btnNew').style.visibility = "hidden";
                                document.getElementById('btnSearchshow').style.visibility = "hidden";
                            }
                            else if(i == 4)
                            {
                                document.getElementById('txtDepartmentName').style.visibility = "hidden";
                                document.getElementById('txtDepartmentNumber').style.visibility = "hidden";
                                document.getElementById('btnNew').style.visibility = "hidden";
                                document.getElementById('btnSearchshow').style.visibility = "hidden";
                                document.getElementById('txtDepartmentName').value = "hidden";
                                document.getElementById('txtDepartmentNumber').value = "hidden";  
                            }
                    }

                    function ShowControls()
                    {         document.getElementById('tblSearch').style.visibility = "visible";
                              document.getElementById('btnSearchshow').style.visibility = "hidden";
                              //document.getElementById('btnNew').style.visibility = "hidden";
                              document.getElementById('txtDepartmentName').style.visibility = "visible";
                              document.getElementById('txtDepartmentNumber').style.visibility="visible";
        
                    }

                    function EnableDisable(i)
                    {
                            document.getElementById('txtDepartmentName').value = "";
                            document.getElementById('txtDepartmentNumber').value = "";   
                            if(i == 1)
                            {
                                document.getElementById('txtDepartmentNumber').value = "hidden";
                                document.getElementById('txtDepartmentName').style.visibility = "visible";
                                document.getElementById('txtDepartmentName').focus();
                                document.getElementById('txtDepartmentNumber').style.visibility = "hidden";            
                                document.getElementById('btnDeptName').disabled = true;
                                document.getElementById('btnDeptNo').disabled = false;            
                                return true;
                            }
                            else if(i == 2)
                            {
                                document.getElementById('txtDepartmentName').value = "hidden";
                                document.getElementById('txtDepartmentName').style.visibility = "hidden";
                                document.getElementById('txtDepartmentNumber').style.visibility = "visible";
                                document.getElementById('txtDepartmentNumber').focus();
                                document.getElementById('btnDeptName').disabled = false;
                                document.getElementById('btnDeptNo').disabled = true; 
                                return true;
                            }
                            else if(i == 3)
                            {
                                document.getElementById('txtDepartmentName').style.visibility = "hidden";
                                document.getElementById('txtDepartmentNumber').style.visibility = "hidden";
                                return true;
                            }
                            return false;
                    }