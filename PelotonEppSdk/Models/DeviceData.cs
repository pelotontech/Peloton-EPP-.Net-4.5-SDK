namespace PelotonEppSdk.Models
{
    public class DeviceData
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string EncryptedCardData { get; set; }
    }

    // ReSharper disable InconsistentNaming
    internal class device_data
    {
        /// <summary>
        /// The manufacturer of the hardware device
        /// </summary>
        public string manufacturer { get; set; }

        /// <summary>
        /// The model of the hardware device
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// The serial number of the hardware device
        /// </summary>
        public string serial { get; set; }

        /// <summary>
        /// The encrypted data as provided from the device
        /// </summary>
        public string encrypted_data { get; set; }

        public static explicit operator DeviceData(device_data dd)
        {
            return new DeviceData
            {
                Manufacturer = dd.manufacturer,
                Model = dd.model,
                Serial = dd.serial,
                EncryptedCardData = dd.encrypted_data
            };
        }

        public static explicit operator device_data(DeviceData dd)
        {
            if (dd == null) return null;
            return new device_data
            {
                manufacturer = dd.Manufacturer,
                model = dd.Model,
                serial = dd.Serial,
                encrypted_data = dd.EncryptedCardData
            };
        }
    }
}
