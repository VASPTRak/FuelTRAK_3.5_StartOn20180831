using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace FuelTrakKeyEncoder.Services
{
    public class SettingsService
    {
        public static event EventHandler UserSettingsChanged;

        private static object syncLock = new object();
        private static IUserSettings settings = GetUserSettingsFromSource();

        private static IUserSettings GetUserSettingsFromSource()
        {
            lock (syncLock)
            {
                string filepath = GetSettingsFilePath();
                if (!File.Exists(filepath)) return null;

                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        fs.Position = 0;
                        XmlSerializer formatter = new XmlSerializer(typeof(UserSettings));
                        UserSettings settings = (UserSettings)formatter.Deserialize(fs);
                        return settings;
                    }
                    catch (SerializationException)
                    {
                        // Attempt delete file since it is probably corrupt if deserialization fails
                        try { File.Delete(filepath); } catch (Exception) { }
                        return null;
                    }
                }
            }
        }

        public IUserSettings GetUserSettings()
        {
            lock (syncLock)
            {
                return settings;
            }
        }

        public void UpdateSettings(string newComPort, string fuelTrakUrl)
        {
            try
            {
                UserSettings newSettings = new UserSettings(newComPort, fuelTrakUrl);
                SaveUserSettings(newSettings);
            }
            catch (Exception ex)
            {
               
            }
        }

        private static void SaveUserSettings(UserSettings newSettings)
        {
            lock (syncLock)
            {
                string filepath = GetSettingsFilePath();
                using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(UserSettings));
                    formatter.Serialize(fs, newSettings);
                    fs.Flush();
                }
                settings = newSettings;
            }
            NotifySettingsChanged();
        }

        private static string GetSettingsFilePath()
        {
            string localDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dataFilePath = Path.Combine(localDataDirectory, "FuelTrakKeyEncoderSettings.data");

            return dataFilePath;
        }

        private static void NotifySettingsChanged()
        {
            EventHandler handler = UserSettingsChanged;
            if (handler != null)
                handler(null, EventArgs.Empty);
        }

        [Serializable]
        public struct UserSettings : IUserSettings
        {
            private string comPort;
            private string fuelTrakUrl;

            public UserSettings(string comPort, string fuelTrakUrl)
            {
                this.comPort = comPort;
                this.fuelTrakUrl = fuelTrakUrl;
            }

            public string ComPort { get { return comPort; } set { comPort = value; } }
            public string FuelTrakUrl { get { return fuelTrakUrl; } set { fuelTrakUrl = value; } }
        }
    }
}
