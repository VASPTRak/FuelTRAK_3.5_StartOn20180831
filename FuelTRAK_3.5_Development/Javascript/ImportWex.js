// JScript File

//Validate TXTN File
 function ValidateTXTNFileUpload(Source, args)
     {
     alert('test');
    
       var fuData = document.getElementById('<%= fuTXTN.ClientID %>');
       var FileUploadPath = fuData.value;
        alert('PATH:'+fuData.value);
       //document.form1.filepath.value  = fuData.value;
       document.getElementById("filepath").value  = fuData.value;
       if(FileUploadPath =='')
       {
        // There is no file selected
        args.IsValid = false;
       }
       else
       {
         var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
         var filename = FileUploadPath.replace(/^.*\\/, '');         
        
         if (filename.indexOf( "Transactions.xls" )>-1)
         {
            args.IsValid = true; // Valid file 
         }
         else
         {
         
            args.IsValid = false; // Not valid file 
//          document.getElementById("TDImage").style.display="none";
//          document.getElementById("TDButtons").style.display=""; 
         }
       }
     }
     
     //Validate Card File
function ValidateCardFileUpload(Source, args)
     {
       var fuData = document.getElementById('<%= fuTXTN.ClientID %>');
       var FileUploadPath = fuData.value;
     
       if(FileUploadPath =='')
       {
        // There is no file selected
        args.IsValid = false;
       }
       else
       {
         var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
         var filename = FileUploadPath.replace(/^.*\\/, '');         
        
         if (filename.indexOf( "T_ALL.txt" )>-1)
         {
         
          args.IsValid = true; // Valid file 
         }
         else
         {
          args.IsValid = false; // Not valid file 
          document.getElementById("TDImage").style.display="none";
          document.getElementById("TDButtons").style.display=""; 
         }
       }
     }
     
     //Validate Driver File
function ValidatePersFileUpload(Source, args)
     {
       var fuData = document.getElementById('<%= fuTXTN.ClientID %>');
       var FileUploadPath = fuData.value;
     
       if(FileUploadPath =='')
       {
        // There is no file selected
        args.IsValid = false;
       }
       else
       {
         var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
         var filename = FileUploadPath.replace(/^.*\\/, '');         
        
         if (filename.indexOf( "T_ALL.txt" )>-1)
         {
         
          args.IsValid = true; // Valid file 
         }
         else
         {
          args.IsValid = false; // Not valid file 
          document.getElementById("TDImage").style.display="none";
          document.getElementById("TDButtons").style.display=""; 
         }
       }
     }
     
     //Validate Lockout File
function ValidateLockoutFileUpload(Source, args)
     {
       var fuData = document.getElementById('<%= fuTXTN.ClientID %>');
       var FileUploadPath = fuData.value;
     
       if(FileUploadPath =='')
       {
        // There is no file selected
        args.IsValid = false;
       }
       else
       {
         var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
         var filename = FileUploadPath.replace(/^.*\\/, '');         
        
         if (filename.indexOf( "T_ALL.txt" )>-1)
         {
         
          args.IsValid = true; // Valid file 
         }
         else
         {
          args.IsValid = false; // Not valid file 
          document.getElementById("TDImage").style.display="none";
          document.getElementById("TDButtons").style.display=""; 
         }
       }
     }