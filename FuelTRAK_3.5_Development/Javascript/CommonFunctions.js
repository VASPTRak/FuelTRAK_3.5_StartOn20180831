// JScript File

    function KeyPressEvent(e)
    {
    //alert(window.event.keyCode);
    if(window.event.keyCode < 97 || window.event.keyCode > 122 )
        { 
          if(window.event.keyCode< 65 || window.event.keyCode > 90) 
          {
                window.event.keyCode=0;
          }
        }
   }
   
   function KeyUpEvent_txtSurchage(e)
    {
    
        var str = document.getElementById('txtSurchage').value;
        if(str.length == 2)
        {
             document.getElementById('txtSurchage').value = str + "."
        }
        else if(str.length == 7)
        {
            document.getElementById('txtAccNo').focus();
        }
    }
    
    function Alrt()
    {
        alert("Record Updated Successfully");
    }
    function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}