// JScript File
function HideControls(i)
        {   if(i == 13)
            {   document.getElementById('btnSearchshow').style.visibility = "hidden";
                document.getElementById('btnNew').style.visibility = "hidden";
            }
           else if(i == 1)
            {   
                document.getElementById('tblSearch').style.visibility = "hidden";
            }
            else if(i == 2)
            {   document.getElementById('tblSearch').style.visibility = "hidden"; }
            else if(i == 3)
            {   document.getElementById('tblSearch').style.visibility = "visible";
                document.getElementById('btnSearch').focus();
                document.getElementById('btnSearchshow').style.visibility = "hidden";
                document.getElementById('btnNew').style.visibility = "hidden";
            }
            else if(i == 4)
            {   document.getElementById('tblSearch').style.visibility = "visible";
                document.getElementById('btnSearchshow').style.visibility = "hidden";
                document.getElementById('btnNew').style.visibility = "hidden";
            }
        }
        
         function DeleteMsg()
        {   
            alert("Record deleted Successfully");
            location.href="PollingQueueSearch.aspx"
        }
        
         function check(PollingId)
        {
            var ac=confirm('Are you sure you want to permanently delete this record ?');
            document.form1.Hidtxt.value=ac;
            document.form1.txtVehId.value=PollingId;
            form1.submit(); 
        }
        
        function ShowSearch()
        {
        document.getElementById('btnNew').style.visibility = "hidden";
        document.getElementById('btnSearchshow').style.visibility = "hidden";
        document.getElementById('tblSearch').style.visibility="visible";

        }