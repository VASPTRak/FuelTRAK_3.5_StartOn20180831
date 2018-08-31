using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FuelTrakKeyEncoder.Services;
using Microsoft.Win32;

namespace FuelTrakKeyEncoder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                // Register protocol handler on app startup, this should really be
                // handled by the application installer.
                PerformInitialSetup();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SettingsService settingsService = new SettingsService();

                EncodeKeyForm form = new EncodeKeyForm(settingsService.GetUserSettings());

                // If a user runs the app by clicking one of the fueltrakencode links on the website
                // the link will be provided as a command line argument, this code parses the link text
                // to determins the action necessary. 
                if (args != null && args.Length > 0 && args[0].ToLower().StartsWith("fueltrakencode:"))
                {
                    string input = args[0].ToLower().Substring(15);
                    switch (input[0])
                    {
                        case 'v':
                            form.SetVehicleId(input.Substring(1));
                            break;
                        case 'p':
                            form.SetPersonnelId(input.Substring(1));
                            break;
                    }
                }

                //Added By Varun Moota to Un-Install the KeyEncoder. 04/05/2010
                // Check command line arguments
                foreach (string arg in args)
                {
                    try
                    {
                        // If /u=[ProductCode], then launch msiexec to uninstall program
                        if (arg.Split('=')[0].ToLower() == "/u")
                        {
                            string guid = arg.Split('=')[1];
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.System);

                            System.Diagnostics.Process.Start(path + "\\msiexec.exe", "/x " + guid);

                            Environment.Exit(0);
                        } // End if

                    }
                    catch
                    {
                        MessageBox.Show("ERROR! Invalid command line argument(s).", "ERROR", MessageBoxButtons.OK);
                    }
                } // End foreach

                Application.Run(form);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
             
        }

        private static void PerformInitialSetup()
        {
            // Create registry entries to setup fueltrakencode url protocol and registed current application as default handler
            RegistryKey protocolKey = Registry.ClassesRoot.OpenSubKey("fueltrakencode");            
            if (protocolKey == null)
            {
                protocolKey = Registry.ClassesRoot.CreateSubKey("fueltrakencode");
                protocolKey.SetValue(null, "URL:Fuel Trak Key Encode Protocol");
                protocolKey.SetValue("URL Protocol", "");

                protocolKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command").SetValue(null,  string.Format("\"{0}\" \"%1\"", System.Reflection.Assembly.GetExecutingAssembly().Location));
            }
            
        }
    }
}