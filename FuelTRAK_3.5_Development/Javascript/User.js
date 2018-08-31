// JScript File


        function ShowSearch()
        {
            //document.getElementById('btnNew').style.visibility = "hidden";
            document.getElementById('btnNew').style.visibility = "hidden";
            document.getElementById('btnsearch').style.visibility = "hidden";

        }
        
     function  ChkChange()
    {
         if(document.getElementById('CHKCOM').checked == true)
        {
         document.getElementById('Label4').innerHTML ="COM #:" ;
         //Commented By Varun
         // document.getElementById('t1').style.display='';
         
         //Added By Varun
         document.getElementById('txtIP').style.visibility = "visible";
         document.getElementById('lblIp').style.display="";
             
             
           
        }else
        {
             document.getElementById('Label4').innerHTML ="Phone # :" ;
            //Commented By Varun
             //document.getElementById('t1').style.display='none';
             
             //Added By Varun
             document.getElementById('txtIP').style.visibility = "hidden";             
             document.getElementById('lblIp').style.display="none";
         
         
             
          
        }
    }
     function KeyPress(e)
     {
        var str = document.getElementById('Txtstate').value;
        
        if (window.event.keyCode < 65 || window.event.keyCode > 122)
        { 
            
                window.event.keyCode = 0;
        }
      }
   