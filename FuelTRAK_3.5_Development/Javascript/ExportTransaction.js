// JScript File

    //This function is used to allow only numeric data in text box.
     function KeyPressEvent(e)
    {
        //alert(window.event.keyCode);
        if (window.event.keyCode < 46 || window.event.keyCode > 58)
	    window.event.keyCode = 0;
    }
    
    //This function is used to add : automatically to time while entering time value    
    function KeyUpEvent_txtTime(e,source)
    {
    
        var str = document.getElementById(source).value;
       
        if(str.length == 2)
        {
             if(str.charAt(1) == ":")
             {
                document.getElementById(source).value = "0" + str.charAt(0) + ":";
             }
             else
             {
                document.getElementById(source).value = str + ":";
             }
        }
        if(str.length == 5)
        {
            if(!IsValidTime(str))
            {
                document.getElementById(source).focus();
            }
        }        
    }    
    
    //This function is used to add / automatically to date while entering date value
    
    function KeyUpEvent_txtDate(e,source)
    {
        var str = document.getElementById(source).value;
        
         if(str.length == 2)
        {
            if(str.charAt(1) == "/")
                document.getElementById(source).value = "0" + str.charAt(0) + "/";
             else
                document.getElementById(source).value = str + "/";
        }
        else if(str.length == 5)
        {
            if(str.charAt(4) == "/")
            {
                var str1 = str.substring(0,str.indexOf("/")+1);
                str1 = str1 + "0" + str.charAt(3) + "/";
                document.getElementById(source).value = str1;
                str = str1;
            }
            else
            {
                document.getElementById(source).value = str + "/";
            }
            
        }
        if(str.length == 10)
        {
           if(ValidateDate(str))
             {
                 document.getElementById(source).focus();
             }
         }   
    }
    
    //This function is used to check if the field is blank.Set focus back to the control if it is blank
    //Harshada Mutalik
    //17 Nov 07
    
    function isblank(source,message)
    {
        var str=document.getElementById(source).value;
       
        if(str=="")
        {
            alert(message);
            document.getElementById(source).focus();
        }
        else
        {
            
           return;
        }
    }
    
    //This function is used to compare values.
    //Harshada Mutalik
    //17 Nov 07
    
    function comparevalues(startdate,enddate,startdept,enddept)
    {
        var fromdate=document.getElementById(startdate).value;
        var todate=document.getElementById(enddate).value;
        var fromdept=document.getElementById(startdept).value;
        var todept=document.getElementById(enddept).value;
                  
       if(Date.parse(todate) < Date.parse (fromdate))
        {
            alert("End date should be greater than start date");
            document.getElementById(enddate).focus();
        }
        
        if(parseInt(todept) < parseInt(fromdept))
        {   
            alert("End department should be greater than start department");
            document.getElementById(enddept).focus();
        }
  
        return;
    }
    
        
   function chksesion()
   {
    	window.location.assign('LoginPage.aspx')
   }