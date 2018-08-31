fixMozillaZIndex=true; //Fixes Z-Index problem  with Mozilla browsers but causes odd scrolling problem, toggle to see if it helps
_menuCloseDelay=500;
_menuOpenDelay=150;
_subOffsetTop=10;
_subOffsetLeft=-2;


with(menuStyle=new mm_style()){
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
onbgcolor="#ffd700";
oncolor="#000099";
outfilter="randomdissolve(duration=0.3)";
overfilter="Fade(duration=0.2);Alpha(opacity=90);Shadow(color=#777777', Direction=135, Strength=3)";
padding=4;
pagebgcolor="#82B6D7";
pagecolor="black";
separatorcolor="#999999";
separatorsize=1;
subimage="images/arrow.gif";
subimagepadding=1;
}

with(milonic=new menuname("Main Menu")){
//alwaysvisible=1;
//left=10;
//orientation="horizontal";
//style=menuStyle;
    alwaysvisible=1;
    overflow="scroll";
    style=menuStyle;

aI("url=Home.aspx;image=images/Home.gif;target=PageFrame");
aI("url=Reports.aspx;image=images/Reports.gif;target=PageFrame");
aI("url=LoginPage.aspx;image=images/Logout.gif;");
aI("url=Help/TrakHelp.htm;image=images/Help.gif;target=NewPage");
}

with(milonic=new menuname("Transactions"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=Transaction.aspx;target=PageFrame")
}

with(milonic=new menuname("Inventory"))
{
    style=menuStyle;
    aI("text=Tank Inventory;url=TankInventory.aspx;target=PageFrame");
    aI("text=Meter Readings;url=MeterReadings.aspx;target=PageFrame");
}

with(milonic=new menuname("TankInventory"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=TankInventory.aspx;target=PageFrame");
}

with(milonic=new menuname("MeterReadings"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=MeterReadings.aspx;target=PageFrame");
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
}

with(milonic=new menuname("Personnel"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=Personnel.aspx;target=PageFrame");
}

with(milonic=new menuname("Department"))
{
    style=menuStyle;
    aI("text=Search / Edit;url=Department.aspx;target=PageFrame");
}

drawMenus();

