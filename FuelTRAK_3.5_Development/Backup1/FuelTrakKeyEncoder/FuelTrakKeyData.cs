using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FuelTrakKeyEncoder
{
    public class FuelTrakKeyData
    {
        private FuelTrakKeyData() { }

        private FuelTrakKeyType keyType;
        public FuelTrakKeyType KeyType
        {
            get { return keyType; }
            set { keyType = value; }
        }

        private string keyId;
        public string KeyId
        {
            get { return keyId; }
            set { keyId = value; }
        }

        private string itemId;
        public string ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        private string expiration;
        public string Expiration
        {
            get { return expiration; }
            set { expiration = value; }
        }

        private string systemNumber;
        public string SystemNumber
        {
            get { return systemNumber; }
            set { systemNumber = value; }
        }

        private bool master;
        public bool IsMasterKey
        {
            get { return master; }
            set { master = value; }
        }
        private bool secondKeyReq;
        public bool IsSecondKeyReq
        {
            get { return secondKeyReq; }
            set { secondKeyReq = value; }
        }
        private bool mileageRequired;
        public bool IsMileageEntryRequired
        {
            get { return mileageRequired; }
            set { mileageRequired = value; }
        }

        private string mileage;
        public string Mileage
        {
            get { return mileage; }
            set { mileage = value; }
        }

        private string mileageWindow;
        public string MileageWindow
        {
            get { return mileageWindow; }
            set { mileageWindow = value; }
        }

        private bool secondKeyRequired;
        public bool IsSecondKeyRequired
        {
            get { return secondKeyRequired; }
            set { secondKeyRequired = value; }
        }

        private bool option;
        public bool Option
        {
            get { return option; }
            set { option = value; }
        }

        private string fuelLimit;
        public string FuelLimit
        {
            get { return fuelLimit; }
            set { fuelLimit = value; }
        }

        private string fuelTypes;
        public string FuelTypes
        {
            get { return fuelTypes; }
            set { fuelTypes = value; }
        }

        //public static bool TryParse(string keydata, out FuelTrakKeyData data)
        //{
        //    try
        //    {
        //        FuelTrakKeyData parsedData = Parse(keydata);
        //        data = parsedData;
        //        return true;
        //    }
        //    catch
        //    {
        //        data = null;
        //        return false;
        //    }
        //}

        public static FuelTrakKeyData Parse(string keydata, string mileageData, string mileageWindowData)
        {
            FuelTrakKeyData data = new FuelTrakKeyData();

            if (keydata.Substring(0, 1) != "+")
                throw new ArgumentException("keydata", "Data was not read successfully from the key.");

            switch (keydata.Substring(17, 1))
            {
                case "0":
                    data.keyType = FuelTrakKeyType.Vehicle;
                    break;
                case "1":
                    data.keyType = FuelTrakKeyType.Personnel;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("keydata", "Invalid key type");
            }
            data.keyId = keydata.Substring(2, 5);
            data.itemId = keydata.Substring(7, 10);
            data.expiration = "Never";
            data.systemNumber = keydata.Substring(21, 3);

            if (data.keyType == FuelTrakKeyType.Vehicle)
            {
                // RSmith 9/27/10 note: The original code for finding data.master is 
                // replaced below.
                //data.master = keydata.Substring(34, 1) == "1";
                data.secondKeyReq = keydata.Substring(18, 1) == "1";
                data.mileageRequired = keydata.Substring(19, 1) == "1";
                data.option = keydata.Substring(20, 1) == "1";
                data.fuelLimit = keydata.Substring(24, 3);
                data.master = keydata.Substring(31, 1) == "1";
                // Finished by RSmith 9/27/10
                // data.fuelTypes = "".PadRight(15, 'N');
                data.fuelTypes = string.Empty;
                for (int i = 0; i < 15; i++)
                {
                    data.fuelTypes += keydata.Substring(i + 32, 1) == "1" ? "Y" : "N";
                }
            }

            // data comes in as (e.g.) "\r\n8765 4321 \r\n\r\n+"
            // we clean that up to:  87654321 
            mileageData = mileageData.Replace("\r", "").Replace("\n", "").Replace("+", "").Replace(" ", "");
            mileageData = mileageData.Trim();
            // then rearrange to 21436587
            string strTemp = mileageData.Substring(6, 2) +
                mileageData.Substring(4, 2) +
                mileageData.Substring(2, 2) +
                mileageData.Substring(0, 2);

            Int32 intTemp = 0;
            try
            {
                intTemp = Int32.Parse(strTemp, System.Globalization.NumberStyles.HexNumber);
                data.mileage = intTemp.ToString();
            }
            catch
            {
                MessageBox.Show("Error converting Hex Mileage",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

            mileageWindowData = mileageWindowData.Replace("\r", "").Replace("\n", "").Replace("+", "");
            mileageWindowData = mileageWindowData.Trim();
            strTemp = mileageWindowData.Substring(2, 2) + 
                mileageWindowData.Substring(0, 2);

            try
            {
                intTemp = Int32.Parse(strTemp, System.Globalization.NumberStyles.HexNumber);
                data.mileageWindow = intTemp.ToString();
            }
            catch
            {
                MessageBox.Show("Error converting Hex Mileage Window",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            return data;
        }


        public static FuelTrakKeyData ParseFromVehicleInformation(FuelTrak.VehicleInformation info, string keyId)
        {
            FuelTrakKeyData data = new FuelTrakKeyData();
            data.KeyType = FuelTrakKeyType.Vehicle;
            data.ItemId = info.Id.ToString();
            data.SystemNumber = info.SystemId;
            data.KeyId = keyId;
            data.Expiration = "Never";
            data.FuelLimit = info.FuelLimit;
            data.IsMasterKey = info.IsMaster;
            data.IsMileageEntryRequired = info.MileageEntryRequired;
            data.IsSecondKeyReq = info.RequireSecondKey;
            data.Option = false;
            data.fuelTypes = info.FuelTypes;
            // Added by RSmith 9/27/10
            data.Mileage = info.Mileage;
            data.MileageWindow = info.MileageWindow;
            return data;
        }

        public static FuelTrakKeyData ParseFromPersonnelInformation(FuelTrak.PersonnelInformation info, string keyId)
        {
            FuelTrakKeyData data = new FuelTrakKeyData();
            data.KeyType = FuelTrakKeyType.Personnel;
            data.ItemId = info.Id.ToString();
            data.SystemNumber = info.SystemId;
            data.KeyId = keyId;
            data.Expiration = "Never";
            return data;
        }

        public string ToKeyDataString()
        {
            try
            {
                StringBuilder sb = new StringBuilder(46);

                sb.Append(keyId.Trim().PadLeft(5, '0'));
                sb.Append(itemId.Trim().PadRight(10));
                sb.Append(keyType == FuelTrakKeyType.Vehicle ? "0" : "1");
                sb.Append(secondKeyRequired ? "1" : "0");

                if (keyType == FuelTrakKeyType.Vehicle)
                {
                    sb.Append(mileageRequired ? "1" : "0");
                    sb.Append(option ? "1" : "0");
                    //Added by Jatin dated 24-Jan-2014
                    sb.Append(Convert.ToInt16(systemNumber).ToString().PadLeft(4, '0'));
                    //sb.Append(systemNumber.Trim().PadLeft(3, '0'));
                    //sb.Append(systemNumber.Trim().PadLeft(4, '0'));//Testing
                    sb.Append(fuelLimit.Trim().PadLeft(3, '0'));
                    sb.Append("0000");
                    sb.Append(master ? "1" : "0");
                    if (fuelTypes.Length != 15) throw new ArgumentException("Fuel types should only contain 15 characters");
                    foreach (char c in fuelTypes)
                    {
                        sb.Append(c == 'Y' ? "1" : "0");
                    }
                    sb.Append("0"); // empty space placeholder?
                }
                else
                {
                    
                    if(systemNumber.Length ==4)
                        sb.Append("0");
                    else
                        sb.Append("00");
                    //Added by Jatin dated 24-Jan-2014
                    sb.Append(Convert.ToInt16(systemNumber).ToString().PadLeft(4, '0'));
                    //sb.Append(systemNumber.Trim().PadLeft(4, '0'));
                    //sb.Append("0".PadLeft(24));
                    sb.Append("".PadLeft(24, '0'));//Changed by Varun , since they need to be 0's not blank spaces.
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ToKeyMileageString()
        {
            StringBuilder sb = new StringBuilder(8);

            // convert from mileage string to hex:  llmmhh00
            Int32 intMileage = Int32.Parse(mileage);

            string strHexTemp = intMileage.ToString("X").PadLeft(8, '0');  //"00abcdef" 
            sb.Append(strHexTemp.Substring(6, 2));
            sb.Append(strHexTemp.Substring(4, 2));
            sb.Append(strHexTemp.Substring(2, 2));
            sb.Append(strHexTemp.Substring(0, 2)); // now it's "efcdab00"
            return sb.ToString();
        }

        public string ToKeyMileageWindowString()
        {
            StringBuilder sb = new StringBuilder();
            // convert from mileage string to hex:  llmmhh00
            Int32 intMileageWindow = Int32.Parse(mileageWindow);

            string strHexTemp = intMileageWindow.ToString("X").PadLeft(4, '0');  //"00ab" 
            sb.Append(strHexTemp.Substring(2, 2));
            sb.Append(strHexTemp.Substring(0, 2)); 
            return sb.ToString();
        }
    }

    public enum FuelTrakKeyType { Vehicle, Personnel }
}
