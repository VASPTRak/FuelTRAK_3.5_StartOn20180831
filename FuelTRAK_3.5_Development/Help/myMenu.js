/******************************************************************************
* This file defines the tree menu with it's items and submenus.               *
******************************************************************************/

// User-defined tree menu data.

var treeMenu           = new TreeMenu();  // This is the main menu.
var treeMenuName       = "myMenu_1.0";    // Make this unique for each tree menu.
var treeMenuDays       = 7;               // Number of days to keep the cookie.
var treeMenuFrame      = "menuFrame";     // Name of the menu frame.
var treeMenuImgDir     = "graphics/"      // Path to graphics directory.
var treeMenuBackground = "";              // Background image for menu frame.
var treeMenuBgColor    = "#336699";       // Color for menu frame background.   
var treeMenuFgColor    = "white";       // Color for menu item text.
var treeMenuHiBg       = "#FDBF02";       // Color for selected item background.
var treeMenuHiFg       = "black";  // Color for selected item text.
var treeMenuFont       = 
      "MS Sans Serif,Arial,bold,Helvetica";    // Text font face.
var treeMenuFontSize   = 3;               // Text font size.
var treeMenuRoot       = "FuelTrak Menu";     // Text for the menu root.
var treeMenuFolders    = 1;               // Sets display of '+' and '-' icons.
var treeMenuAltText    = true;            // Use menu item text for icon image ALT text.

// Define the items for the top-level of the tree menu.


treeMenu.addItem(new TreeMenuItem("Introduction", "Intro.htm", "mainFrame","menu_link_ref.gif"));
treeMenu.addItem(new TreeMenuItem("Login","Login.htm","mainFrame","menu_link_ref.gif"));


treeMenu.addItem(new TreeMenuItem("Reports","","","menu_link_ref.gif"));
treeMenu.addItem(new TreeMenuItem("Transaction","","","menu_link_ref.gif"));
treeMenu.addItem(new TreeMenuItem("Inventory","","","menu_link_ref.gif"));
treeMenu.addItem(new TreeMenuItem("Item","","","menu_link_ref.gif"));
treeMenu.addItem(new TreeMenuItem("Setup","","","menu_link_ref.gif"));
treeMenu.addItem(new TreeMenuItem("Quit", "javascript:parent.window.close()", "mainFrame","menu_link_ref.gif"));

var asp = new TreeMenu();
asp.addItem(new TreeMenuItem("Transaction","TransactionRpt.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Site","SiteRpt.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Billing","BillingRpt.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Vehicle","VehicleRpt.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Personnel","PersonnelRpt.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Fuel Use","FuelUseRpt.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Inventory","InventoryRpt.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Miscellaneous","MiscRpt.htm","mainFrame","menu_link_ref.gif"));
treeMenu.items[2].makeSubmenu(asp);

var asp = new TreeMenu();
asp.addItem(new TreeMenuItem("Search/Edit, New","Transaction.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("New","TransactionNew.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Export Transaction","TransactionExport.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Translation Table","TranslationTable.htm","mainFrame","menu_link_ref.gif"));
treeMenu.items[3].makeSubmenu(asp);

var asp = new TreeMenu();
asp.addItem(new TreeMenuItem("Tank Inventory","TankInventory-2.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Meter Readings","MeterReading.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Tank Charts","TankCharts.htm","mainFrame","menu_link_ref.gif"));
treeMenu.items[4].makeSubmenu(asp);

var asp = new TreeMenu();
asp.addItem(new TreeMenuItem("Vehicle","Vehicle.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Personnel","Personnel.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Department","Department.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Sentry","Sentry.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Products","Products.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Tank","Tank.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("TankMonitor","TankMonitor.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Read Key","ReadKey.htm","mainFrame","menu_link_ref.gif"));
treeMenu.items[5].makeSubmenu(asp);

var asp = new TreeMenu();
asp.addItem(new TreeMenuItem("User, Search/Edit, New","CreateUser.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Polling Setup","PollingSetup.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Customer Information","Customer.htm","mainFrame","menu_link_ref.gif"));
asp.addItem(new TreeMenuItem("Polling Queue","PollingQueue.htm","mainFrame","menu_link_ref.gif"));
treeMenu.items[6].makeSubmenu(asp);


