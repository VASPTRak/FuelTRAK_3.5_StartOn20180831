// JScript File
/*******************************************************************
This is the javascript function that is invoked when the checkboxlist
is clicked.

Function:    disableListItems.
Inputs:        checkBoxListId - The id of the checkbox list.

            checkBoxIndex - The index of the checkbox to verify.
            i.e If the 4th checkbox is clicked and 
            you want the other checkboxes to be
            disabled the index would be 3.
            
            numOfItems - The number of checkboxes in the list.
Purpose:    Disables all the checkboxes when the checkbox to verify is
            checked.  The checkbox to verify is never disabled.
********************************************************************/
function disableListItems(checkBoxListId, checkBoxIndex, numOfItems)
{
    // Get the checkboxlist object.
    objCtrl = document.getElementById(checkBoxListId);
    
    // Does the checkboxlist not exist?
    if(objCtrl == null)    {        return;    }

    var i = 0;
    var objItem = null;
    // Get the checkbox to verify.
    var objItemChecked =       document.getElementById(checkBoxListId + '_' + checkBoxIndex);
    // Does the individual checkbox exist?
    if(objItemChecked == null)
    {        return;    }

    // Is the checkbox to verify checked?
    var isChecked = objItemChecked.checked;
    
    // Loop through the checkboxes in the list.
    for(i = 0; i < numOfItems; i++)
    {
        objItem = document.getElementById(checkBoxListId + '_' + i);
        if(objItem == null)
        {            continue;        }

        // If i does not equal the checkbox that is never to be disabled.
        if(i != checkBoxIndex)
        {
            // Disable/Enable the checkbox.
            objItem.disabled = isChecked;
            // Should the checkbox be disabled?
            if(isChecked)
            {    // Uncheck the checkbox.
                objItem.checked = false;
            }
        }
    }
}

 function DispPanel(varID)
        {
            var curleft  = 0;
             var curtop = 0;
             
             var myObj = document.getElementById('tbl1');
             var imgHeight = myObj.height;

             if (myObj.offsetParent) 
             { 
                 curleft = myObj.offsetLeft;
                 curtop = myObj.offsetTop;
                 
                 while (myObj = myObj.offsetParent) 
                 { 
                    curleft += myObj.offsetLeft;
                    curtop += myObj.offsetTop;
                 }             
             }
         
            if (varID==1)
            {
                document.getElementById ('SentryInputPanel').style.top=curtop+20 +'px';
                document.getElementById ('SentryInputPanel').style.left=curleft+75 +'px';                
            }
            else if (varID==2)
            {
                document.getElementById ('TMInputPanel').style.top=curtop+20+'px';
                document.getElementById ('TMInputPanel').style.left=curleft+75+'px';
            }
        } 
        function HidePanel(varID)
        {
            if (varID==1)
            {
                document.getElementById ('SentryInputPanel').style.top=50+'px';
                document.getElementById ('SentryInputPanel').style.left=-1000+'px';
            }
            else if (varID==2)
            {
                document.getElementById ('TMInputPanel').style.top=100+'px';
                document.getElementById ('TMInputPanel').style.left=-1000+'px';
            }
        }
        function AutoExport(varID)
        {
          if (varID==1)
          {
              document.getElementById ('panelAutoExport').disabled = false;
             document.getElementById ('panelAutoExport').style.top=50+'px';
             document.getElementById ('panelAutoExport').style.left=-1000+'px';
          }
          
        }
        
       function disableRadioItems(VarID)
       {
              var optList1= document.getElementById(VarID);
              var arrayOfRadioBtn = optList1.getElementsByTagName("input");
             
              if(arrayOfRadioBtn[2].checked == true)
              {
                document.getElementById('txtPollfromdt').disabled = false;
              }
              else
              {
                document.getElementById('txtPollfromdt').disabled = true;
              }              
       }
       
       function EnableAutoExport(varID) 
       {
         if (varID==1)
         {
           if(document.getElementById('chkAutoExport').checked==true )
           {         
             document.getElementById('txtAutoExportTime').disabled = false;
             document.getElementById('rdoUDFName').disabled = false;
             document.getElementById('rdoAutoIncrmnt').disabled = false ;
             document.getElementById('rdoUfixed').disabled = false ;
             document.getElementById('txtFixedfilename').disabled = false ;
             document.getElementById('txtExportFileExtn').disabled = false ;
             document.getElementById('txtExportFLocation').disabled = false ;
             document.getElementById('chkExecuteBatchProcess').disabled = false ;
             document.getElementById('txtBatchProcessFileName').disabled = false ;
             document.getElementById('txtBatchFileLocation').disabled = false ;
             document.getElementById('chkIncPrevExprtTxtn').disabled = false ;
             document.getElementById('chkIncZeroQtyTxtn').disabled = false ;
             document.getElementById('chkAppendtoPrevExprtFile').disabled = false ;            
          
           }
          else 
          {
                   
             document.getElementById('txtAutoExportTime').disabled = true;
             document.getElementById('rdoUDFName').disabled = true;
             document.getElementById('rdoAutoIncrmnt').disabled = true ;
             document.getElementById('rdoUfixed').disabled = true ;
             document.getElementById('txtFixedfilename').disabled = true ;
             document.getElementById('txtExportFileExtn').disabled = true ;
             document.getElementById('txtExportFLocation').disabled = true ;
             document.getElementById('chkExecuteBatchProcess').disabled = true ;
              document.getElementById('txtBatchProcessFileName').disabled = true ; 
             document.getElementById('txtBatchFileLocation').disabled = true ; 
             document.getElementById('chkIncPrevExprtTxtn').disabled = true ; 
             document.getElementById('chkIncZeroQtyTxtn').disabled = true ; 
             document.getElementById('chkAppendtoPrevExprtFile').disabled = true ; 
 
          }
        }
         
      }
        
       function EnableAutoPoll(varID)
       {
            if (varID==1)
            {
                if (document.getElementById('chkEnableAutoPoll').checked==true )
                {
                    document.getElementById('txtTime').disabled = false;
                    //document.getElementById('ChkListDays').disabled = false;
                    var chkList1= document.getElementById('ChkListDays');
                    var arrayOfCheckBoxes= chkList1.getElementsByTagName("input");
                    for(var i=0;i<arrayOfCheckBoxes.length-1;i++) 
                    {
                        arrayOfCheckBoxes[i].disabled = false
                    }
                    document.getElementById('btnSentryAutopoll').disabled = false;
                    document.getElementById('btnTMAutopoll').disabled = false;
                }
                else
                {
                    document.getElementById('txtTime').disabled = true;
                    //document.getElementById('ChkListDays').disabled = true;
                    var chkList1= document.getElementById('ChkListDays');
                    var arrayOfCheckBoxes= chkList1.getElementsByTagName("input");
                    for(var i=0;i<arrayOfCheckBoxes.length-1;i++) 
                    {
                        arrayOfCheckBoxes[i].disabled = true;
                    }
                    document.getElementById('btnSentryAutopoll').disabled = true;
                    document.getElementById('btnTMAutopoll').disabled = true;
                }
            }
            else
            {
                    //document.getElementById('chkEnableAutoPoll').checked=true
                    document.getElementById('txtTime').disabled = false;
                    document.getElementById('ChkListDays').disabled = false;
                    document.getElementById('btnSentryAutopoll').disabled = false;
                    document.getElementById('btnTMAutopoll').disabled = false; 
                    var chkList1= document.getElementById('chkPersonnelLst');
                    var arrayOfCheckBoxes= chkList1.getElementsByTagName("input");
                   
                    if(arrayOfCheckBoxes[0].checked == true)
                    {
                        arrayOfCheckBoxes[1].disabled = true;
                    }
                    
                     chkList1= document.getElementById('chkVehLst');
                     arrayOfCheckBoxes= chkList1.getElementsByTagName("input");
                   
                    if(arrayOfCheckBoxes[0].checked == true)
                    {
                        arrayOfCheckBoxes[1].disabled = true;
                    }
            }
       }
       
       
        function ChangeCheckBoxState(id, checkState)
        {
            var cb = document.getElementById(id);
            if (cb != null)
               cb.checked = checkState;
        }
        
        function ChangeAllCheckBoxStates(checkState)
        {
            // Toggles through all of the checkboxes defined in the CheckBoxIDs array
            // and updates their value to the checkState input parameter
            if (CheckBoxIDs != null)
            {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                   ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }
        }
        
        function ChangeHeaderAsNeeded()
        {
            // Whenever a checkbox in the GridView is toggled, we need to
            // check the Header checkbox if ALL of the GridView checkboxes are
            // checked, and uncheck it otherwise
            if (CheckBoxIDs != null)
            {
                // check to see if all other checkboxes are checked
                for (var i = 1; i < CheckBoxIDs.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked)
                    {
                        // Whoops, there is an unchecked checkbox, make sure
                        // that the header checkbox is unchecked
                        ChangeCheckBoxState(CheckBoxIDs[0], false);
                        return;
                    }
                }
                // If we reach here, ALL GridView checkboxes are checked
                ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }
        //**********************************************************************
        function ChangeCheckBoxStateTM(id, checkState)
        {
            var cb = document.getElementById(id);
            if (cb != null)
               cb.checked = checkState;
        }
        
        function ChangeAllCheckBoxStatesTM(checkState)
        {
            // Toggles through all of the checkboxes defined in the CheckBoxIDs array
            // and updates their value to the checkState input parameter
            if (CheckBoxIDsTM != null)
            {
                for (var i = 0; i < CheckBoxIDsTM.length; i++)
                   ChangeCheckBoxStateTM(CheckBoxIDsTM[i], checkState);
            }
        }
        
        function ChangeHeaderAsNeededTM()
        {
            // Whenever a checkbox in the GridView is toggled, we need to
            // check the Header checkbox if ALL of the GridView checkboxes are
            // checked, and uncheck it otherwise
            if (CheckBoxIDsTM != null)
            {
                // check to see if all other checkboxes are checked
                for (var i = 1; i < CheckBoxIDsTM.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDsTM[i]);
                    if (!cb.checked)
                    {
                        // Whoops, there is an unchecked checkbox, make sure
                        // that the header checkbox is unchecked
                        ChangeCheckBoxStateTM(CheckBoxIDsTM[0], false);
                        return;
                    }
                }
                // If we reach here, ALL GridView checkboxes are checked
                ChangeCheckBoxStateTM(CheckBoxIDsTM[0], true);
            }
        }
