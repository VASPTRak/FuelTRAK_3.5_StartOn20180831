// JScript File

function KeyPressYear(e)
     {
        var str = document.getElementById('txtSID').value;
        
        if (window.event.keyCode < 48 || window.event.keyCode > 57)
        { window.event.keyCode = 0;}
      }
       function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}
	function ShowControls()
    {
             document.getElementById('tblSearchsub').style.visibility = "visible";
             document.getElementById('btnSearch').style.visibility = "hidden";
             document.getElementById('btnNew').style.visibility = "hidden";
             document.getElementById('btnSearchsub').style.visibility = "visible";
         
    }
    
    
    function chksesion()
	{
	    alert("Session expired, Please login again.");
		window.location.assign('LoginPage.aspx')
	}