// JScript File


		
        function Close()
        {
            self.close();
        }

        function OpnWin1()
        {
            opener.location.href = "TankInventory_New_Edit.aspx?R";
        }

        function OpnWin(a)
        {
            alert(a);
            opener.location.href = a;
        }

        function OpnNewWin(i)
        {
            if (i == 1)
            {   
                //Commented By Varun Moota.01/06/2010
                //opener.location.href = "TankInventory_New_Edit.aspx?R";
                //  self.close()
                
               //Added By Varun Moota to open New Window.01/06/2010
               window.location = "TankInventory_New_Edit.aspx?R"


            }
            else if (i == 2)
            {
                //Commented By Varun Moota.01/06/2010
                //opener.location.href = "TankInventory_New_Edit.aspx?S";
                //self.close()
                
                //Added By Varun Moota to open New Window.01/06/2010
               window.location = "TankInventory_New_Edit.aspx?S"
               
            }
            else if (i == 3)
            {
                 //Commented By Varun Moota.01/06/2010
                //opener.location.href = "TankInventory_New_Edit.aspx?D";
                //self.close()
                
               //Added By Varun Moota to open New Window.01/06/2010
               window.location = "TankInventory_New_Edit.aspx?D"
            }
            else if (i == 4)
            {
                self.close()
            }
        }
