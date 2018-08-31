// JScript File

    function KeyPressEvent(e)
    {
        //alert(window.event.keyCode);
        if (window.event.keyCode < 46 || window.event.keyCode > 58)
	    window.event.keyCode = 0;
    }
    
    function KeyPressEvent_RemoveLessThan(e)
    {
        //alert(window.event.keyCode);
        if (window.event.keyCode == 60 )
	    window.event.keyCode = 0;
    }
    
    function KeyPressEvent_notAllowDot(e)
    {
        //alert(window.event.keyCode);
        if ((window.event.keyCode >= 48 && window.event.keyCode <= 57) || window.event.keyCode == 8)
            {}
        else{window.event.keyCode=0;}
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
            if(isValidTime(str)== true)
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
    
   

     function Validate()
    {
        var str=document.getElementById('txtFieldPos').value;
        if (str == "")
        {
            alert("Please enter field position");
            document.getElementById('txtFieldPos').focus();
            return false;
        }
        else
        return;
    }
    
  

    function TrapKey(e,txtbox)
    {
        var str = document.getElementById(txtbox).value;
        var StrSplit;
        var blnFlg = false;
        StrSplit = str.split('.');
        if (StrSplit.length > 1) 
        {   blnFlg = true;        }
        if (blnFlg == false) 
        {    
            if ((window.event.keyCode >= 48 && window.event.keyCode <= 57) || window.event.keyCode == 8 || window.event.keyCode == 46 )
            {}
            else{window.event.keyCode=0;}
        }
        else
        {   if (!(window.event.keyCode >= 48 && window.event.keyCode <= 57) || (window.event.keyCode == 8) )
            {window.event.keyCode=0;}
        }
    }
    
    function AllowNumeric(txtbox)
    {
        if ((window.event.keyCode >= 48 && window.event.keyCode <= 57) || window.event.keyCode == 8)
            {}
        else{window.event.keyCode=0;}
    }
    
    function DotCommaNotAllow(txtbox)
    {
        if (window.event.keyCode == 46 || window.event.keyCode == 39)
            {window.event.keyCode=0;}
    }
    
    function isValidTime(value) {
   var hasMeridian = false;
   var re = /^\d{1,2}[:]\d{2}([:]\d{2})?( [aApP][mM]?)?$/;
   if (!re.test(value)) { return false; }
   if (value.toLowerCase().indexOf("p") != -1) { hasMeridian = true; }
   if (value.toLowerCase().indexOf("a") != -1) { hasMeridian = true; }
   var values = value.split(":");
   if ( (parseFloat(values[0]) < 0) || (parseFloat(values[0]) > 23) ) { return false; }
   if (hasMeridian) {
      if ( (parseFloat(values[0]) < 1) || (parseFloat(values[0]) > 12) ) { return false; }
   }
   if ( (parseFloat(values[1]) < 0) || (parseFloat(values[1]) > 59) ) { return false; }
   if (values.length > 2) {
      if ( (parseFloat(values[2]) < 0) || (parseFloat(values[2]) > 59) ) { return false; }
   }
   return true;
}