fixMozillaZIndex=true; //Fixes Z-Index problem  with Mozilla browsers but causes odd scrolling problem, toggle to see if it helps
_menuCloseDelay=500;
_menuOpenDelay=150;
_subOffsetTop=10;
_subOffsetLeft=-2;


with(menuStyle=new mm_style())
{
    bordercolor="#999999";
    borderstyle="solid";
    borderwidth=1;
    fontfamily="Verdana, Tahoma, Arial";
    fontsize="75%";
    fontstyle="normal";
    headerbgcolor="#ffd700" //"#ffffff";
    headercolor="#ffd700" //"#000000";
    offbgcolor="#eeeeee";
    offcolor="#000000";
    onbgcolor="#ddffdd";
    oncolor="#000099";
    outfilter="randomdissolve(duration=0.3)";
    overfilter="Fade(duration=0.2);Alpha(opacity=90);Shadow(color=#777777', Direction=135, Strength=3)";
    padding=4;
    pagebgcolor="#82B6D7";
    pagecolor="black";
    separatorcolor="#999999";
    separatorsize=1;
    subimage="images/arrow.gif";
    subimagepadding=2;
}

with(milonic=new menuname("Main Menu"))
{
    alwaysvisible=1;
    overflow="scroll";
    style=menuStyle;

    aI("url=Home.aspx;image=images/Home.gif;target=PageFrame");
    aI("url=Reports.aspx;image=images/Reports.gif;target=PageFrame");
    aI("showmenu=Transactions;subimage=images/Transactions.gif;target=PageFrame");
    aI("showmenu=Inventory;subimage=images/Inventory.gif;");
    aI("showmenu=Items;subimage=images/Item.gif;");
   // aI("showmenu=Setup;subimage=images/Setup.gif;");
    aI("url=LoginPage.aspx;image=images/Logout.gif;");
    aI("url=Help/TrakHelp.htm;image=images/Help.gif;target=NewPage");
    
}

with(milonic=new menuname("Setup"))
{
    style=menuStyle;
   // aI("showmenu=User;text=User");
    //aI("text=User\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0;url=user.aspx;target=PageFrame");
//    aI("text=Search / Edit;url=user.aspx;target=PageFrame");
//    aI("text=New;url=createuser.aspx;target=PageFrame");
    aI("text=Polling Setup;url=PollSetup.aspx;target=PageFrame");
    aI("text=Customer Information;url=StatusInfo.aspx;target=PageFrame");
    aI("showmenu=PollingQueue;text=Polling Queue;");
}

with(milonic=new menuname("PollingQueue"))
{    style=menuStyle;    
     aI("text=Search;url=PollingQueueSearch.aspx;target=PageFrame");
     aI("text=New;url=PollingQueue_New_Edit.aspx;target=PageFrame");
     }

with(milonic=new menuname("Items"))
{
    style=menuStyle;
  
    aI("showmenu=Vehicle;text=Vehicle;");
    aI("showmenu=Personnel;text=Personnel;");
    aI("showmenu=Department;text=Department;");
    aI("showmenu=Sentry;text=Sentry;");
    aI("showmenu=Product;text=Product;");
    aI("showmenu=Tank;text=Tank;");
    aI("showmenu=Tank_Monitor;text=Tank Monitor;");
    aI("text=Read Key;url=fueltrakencode:r");
    //aI("text=Import TagInfo;url=ImportTagInfo.aspx;target=PageFrame")
    //aI("text=Read Key;url=Key_Encoder.aspx?R=1;target=PageFrame");
}


with(milonic=new menuname("Transactions"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=Transaction.aspx;target=PageFrame");
    aI("text=New;url=Transaction_New_Edit.aspx;target=PageFrame");
    aI("text=Export Transactions;url=ExportTransactions.aspx;target=PageFrame");
    aI("text=Import Transactions;url=TransactionsImport.aspx;target=PageFrame");
    aI("text=WEX Import;url=WexImport.aspx;target=PageFrame");
}

with(milonic=new menuname("Inventory"))
{
   style=menuStyle;
     aI("showmenu=Tank Inventory;text=Tank Inventory;");
     aI("showmenu=MeterReadings;text=MeterReadings;");
     aI("showmenu=Tank Reconciliation;text=Tank Reconiciliation;");
     aI("showmenu=Tank Charts;text=Tank Charts;");
}

with(milonic=new menuname("Tank Inventory"))
{    style=menuStyle;    aI("text=Search / Edit;url=TankInventory.aspx;target=PageFrame");
     style=menuStyle;    aI("text=New;url=InventoryPopup.aspx;target=PageFrame");
}

with(milonic=new menuname("Tank Reconciliation"))
{    style=menuStyle;    aI("text=Search / Edit;url=TankRecon.aspx;target=PageFrame");
    
}

with(milonic=new menuname("MeterReadings"))
{
    style=menuStyle;    aI("text=Search / Edit;url=MeterReadings.aspx;target=PageFrame");
    style=menuStyle;    aI("text=New;url=MeterReadings_New_Edit.aspx;target=PageFrame");

}
with(milonic=new menuname("Tank Charts"))
{    style=menuStyle;    aI("text=Search / Edit;url=TankCharts.aspx;target=PageFrame");
     style=menuStyle;    aI("text=New;url=TankCharts_New_Edit.aspx;target=PageFrame");
}



with(milonic=new menuname("Users"))
{
    style=menuStyle;
    aI("text=Vehicle;url=Vehicle.aspx;target=PageFrame");
    aI("text=Personnel;url=Personnel.aspx;target=PageFrame");
    aI("text=Department;url=Department.aspx;target=PageFrame");
    aI("text=Read Key;url=Key_Encoder.aspx;target=PageFrame");

}

with(milonic=new menuname("Vehicle"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=Vehicle.aspx;target=PageFrame");
    style=menuStyle;    aI("text=New;url=Vehicle_Edit.aspx;target=PageFrame");
     style=menuStyle;    aI("text=Upload New Vehicles;url=ImportTagInfo.aspx;target=PageFrame");
     style=menuStyle;   aI("text=Custom Vehicle Messages;url=VehCustMsgs.aspx;target=PageFrame");
     style=menuStyle;   aI("text=Instant Vehicle Messages;url=VehInstMsg.aspx;target=PageFrame");
     style=menuStyle;   aI("text=Import TRAK TAG Info;url=TrakTagInfo.aspx;target=PageFrame");
}

with(milonic=new menuname("Personnel"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=Personnel.aspx;target=PageFrame");
    style=menuStyle;    aI("text=New;url=Personnel_New_Edit.aspx;target=PageFrame");
}

with(milonic=new menuname("Department"))
{
    style=menuStyle;    aI("text=Search / Edit;url=Department.aspx;target=PageFrame");
      style=menuStyle;    aI("text=New;url=Department_New_Edit.aspx;target=PageFrame");
}

with(milonic=new menuname("Sentry"))
{    style=menuStyle;    aI("text=Search / Edit;url=Sentry.aspx;target=PageFrame");
     style=menuStyle;    aI("text=New;url=Sentry_New_Edit.aspx;target=PageFrame");
}

with(milonic=new menuname("Product"))
{    style=menuStyle;    aI("text=Edit;url=Product.aspx;target=PageFrame");
     
}

with(milonic=new menuname("Tank"))
{    style=menuStyle;    aI("text=Search / Edit;url=Tank.aspx;target=PageFrame");
     style=menuStyle;    aI("text=New;url=Tank_New_Edit.aspx;target=PageFrame");
}
with(milonic=new menuname("Tank_Monitor"))
{    style=menuStyle;    aI("text=Search / Edit;url=TankMonitor.aspx;target=PageFrame");
     style=menuStyle;    aI("text=New;url=TankMonitor_New_Edit.aspx;target=PageFrame");
}

drawMenus();

