// JScript File
    
  
    function KeyUpEvent_txtHose(e)
    {
        var str = document.getElementById('txtHose').value;
        //alert(window.event.keyCode);
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('DDLstSentry').focus(); 
            }
        }
        else if(str.length == 1)
        {
             document.getElementById('txtReading').focus();
        }
    }
    
    function KeyUpEvent_Reading(e)
    {
        var str = document.getElementById('txtReading').value;
        if(window.event.keyCode == 8)
        {
            if(str.length == 0)
            {
               document.getElementById('txtHose').focus(); 
            }
        }
        else if(str.length == 8)
        {
            document.getElementById('txtReading').value = str + ".";
        }
        else if(str.length == 10)
        {
             document.getElementById('txtTime').focus();
        }
    }
    
  
        
    function HideControls(i)
    { if(i == 1)
        {   document.getElementById('btnNew').style.visibility = "hidden";
            document.getElementById('btnSearchshow').style.visibility = "hidden";
        }
        else if(i == 2)
        {   document.getElementById('tblSearch').style.visibility = "hidden"; }
        else if(i == 3)
        {   document.getElementById('btnNew').style.visibility = "hidden";
            document.getElementById('btnSearchshow').style.visibility = "hidden";

            document.getElementById('tblSearch').value="";
            document.getElementById('tblSearch').style.visibility = "visible";
        }
        else if(i == 4)
        {   document.getElementById('tblSearch').style.visibility = "visible";        }
    }
    
     function ShowControls()
     {
        document.getElementById('tblSearch').style.visibility = "hidden";
     }

    function EnableDisable(i)
    {
        if(i == 1)
        {
            document.getElementById('btnDate').disabled = false;            
            return true;
        }
        else if(i == 2)
        {
            document.getElementById('btnSentry').disabled = false;
            return true;
        }
        else if(i == 3)
        {
            return true;
        }
        return false;
    }
    function ShowSearch()
        {
        document.getElementById('btnNew').style.visibility = "hidden";
        document.getElementById('btnSearchshow').style.visibility = "hidden";
        document.getElementById('tblSearch').style.visibility="visible";

        }
        
       