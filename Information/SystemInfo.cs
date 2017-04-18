using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;

namespace HappyStudio.UwpToolsLibrary.Information
{
    public static class SystemInfo
    {
        public static string DeviceType { get => AnalyticsInfo.VersionInfo.DeviceFamily; }

        public static string DeviceName
        {
            get
            {
                var deviceInfo = new EasClientDeviceInformation();
                return $"{deviceInfo.SystemManufacturer} {deviceInfo.SystemProductName}";
            }
        }

        public static double BuildVersion
        {
            get
            {
                ulong u = Convert.ToUInt64(AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
                string version = $"{u >> 16 & 0xFFFF}.{u & 0xFFFF}";
                return Double.Parse(version);
            }
        }
        
    }
}
