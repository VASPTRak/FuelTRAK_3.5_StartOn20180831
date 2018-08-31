// JScript File
// Created on 3rd Octomber by Satish Harnol - TTPL
//This file contains code for TankInventory_New_Edit.aspx page controls

function KeyPressEvent_txt_nodecimal(e)
{
    if (window.event.keyCode < 48 || window.event.keyCode > 57){window.event.keyCode = 0;}
}

function KeuUpEvent(e)
{
    alert(document.getelementbyID("txt3").value)
}
function Check()
{
    window.open("TankInventory.aspx","PageFrame")
}

///Key events for R type visible textbox
    function KeyUpEvent_txt1R(e)
    {
        var str = document.getElementById('txt1').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
        }        
        else if(str.length == 5){document.getElementById('txt2').focus();}
    }
    
    function KeyUpEvent_txt2R(e)
    {
        var str = document.getElementById('txt2').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
        }       
        else if(str.length == 5)
        {document.getElementById('txt3').focus();        }
    }
    
    function KeyUpEvent_txt3R(e)
    {
        var str = document.getElementById('txt3').value;

        if(str.length == 1)
        {           document.getElementById('txt3').value = str + ".";        }
    }
    
    function KeyUpEvent_txt4R(e)
    {
        var str = document.getElementById('txt4').value;
        var timelimit = str.split(":",1);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txt3').focus();
            }
        }
        else if(str.length == 2)
        {
            document.getElementById('txt4').value = str + ":";
        }
        else if(str.length >= 5)
        {
            if(IsValidTime(str,4))
            {
                
            }
            else
            {
                document.getElementById('txt4').focus();
            }
        }       
    }
    
    function KeyUpEvent_txt5R(e)
    {
        var str = document.getElementById('txt5').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txt4').focus();
            }
        }
        else if(str.length == 2)
        {
            document.getElementById('txt5').value = str + "/";
        }
        else if(str.length == 5)
        {
            document.getElementById('txt5').value = str + "/";
        }
        else if(str.length == 10)
        {
             if(ValidateDate(str))
             {
                document.getElementById('txt6').focus();
             }
             else
             {
                document.getElementById('txt5').focus();
             }                     
        }
    }
    
    //Key events for S type visible textbox
    function KeyUpEvent_txt1S(e)
    {
        var str = document.getElementById('txt1').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
//            if(str.length == 0)
//            {
//               document.getElementById('DDLstTank').focus();
//            }
        }        
        else if(str.length == 5)
        {
             document.getElementById('txt2').focus();
        }
    }
    
    function KeyUpEvent_txt2S(e)
    {
        var str = document.getElementById('txt2').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txt1').focus();
            }
        }
         else if(str.length == 1)
        {
            document.getElementById('txt2').value = str + ".";
        }
        else if(str.length == 7)
        {
            // document.getElementById('txt3').focus();
        }
    }
    
    function KeyUpEvent_txt3S(e)
    {
        var str = document.getElementById('txt3').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txt2').focus();
            }
        }
        else if(str.length == 2)
        {
            document.getElementById('txt3').value = str + ":";
        }
        else if(str.length >= 5)
        {
            if(IsValidTime(str,3))
            {
                
            }
            else
            {
                document.getElementById('txt3').focus();
            }
        }
    }
    
    function KeyUpEvent_txt4S(e)
    {
        var str = document.getElementById('txt4').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txt3').focus();
            }
        }
        else if(str.length == 2)
        {
            document.getElementById('txt4').value = str + "/";
        }
        else if(str.length == 5)
        {
            document.getElementById('txt4').value = str + "/";
        }
        else if(str.length == 10)
        {
             
            if(ValidateDate(str))
             {
                
             }
             else
             {
                document.getElementById('txt4').focus();
             }             
        }
    }
    
    //Key events for D type visible textbox
    function KeyUpEvent_txt1D(e)
    {
        var str = document.getElementById('txt1').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
//            if(str.length == 0)
//            {
//               document.getElementById('DDLstTank').focus();
//            }
        }       
        else if(str.length == 5)
        {
             document.getElementById('txt2').focus();
        }
    }
    
    function KeyUpEvent_txt2D(e)
    {
        var str = document.getElementById('txt2').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txt1').focus();
            }
        }
         else if(str.length == 2)
        {
            document.getElementById('txt2').value = str + ":";
        }
        else if(str.length >= 5)
        {
            if(IsValidTime(str,2))
            {
                 ocument.getElementById('txt3').focus(); 
            }
            else
            {
                document.getElementById('txt2').focus();
            }
        }
    }
    
    function KeyUpEvent_txt3D(e)
    {
        var str = document.getElementById('txt3').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txt2').focus();
            }
        }
        else if(str.length == 2)
        {
            document.getElementById('txt3').value = str + "/";
        }
        else if(str.length == 5)
        {
            document.getElementById('txt3').value = str + "/";
        }
        else if(str.length == 10)
        {
             if(ValidateDate(str))
             {
                if(document.getElementById ('btnOk') != null)
                {
                    document.getElementById('btnOk').focus();
                }
             }
             else
             {
                document.getElementById('txt3').focus();
             }                 
        }
    }
    
        function ShowSearch()
        {
            document.getElementById('btnNew').style.visibility = "hidden";
            document.getElementById('btnSearchshow').style.visibility = "hidden";
            document.getElementById('tblSearch').style.visibility="visible";

        }
        
        function chksesion()
	    {
	        alert("Session expired, Please login again.");
		    window.location.assign('LoginPage.aspx')
	    }
