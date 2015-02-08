using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Cloo;

namespace TestSolution.Cloo.Helpers
{
    public static class DeviceFinder
    {

        public static int GetComputePlatformsCount()
        {
            return ComputePlatform.Platforms.Count;
        }

        public static ReadOnlyCollection<ComputePlatform> GetComputePlatforms()
        {
            return ComputePlatform.Platforms;
        }

        public static List<string> GetComputePlatformsDescriptions()
        {
            var descriptions = new List<string>(GetComputePlatformsCount());
            var platforms = GetComputePlatforms();
            for (var i = 0; i < platforms.Count; i++)
            {
                var currentPlatform = platforms[i];
                var builder = new StringBuilder(100);
                builder
                    .Append("Name: ").Append(currentPlatform.Name).AppendLine()
                    .Append("Profile: ").Append(currentPlatform.Profile).AppendLine()
                    .Append("Version: ").Append(currentPlatform.Version).AppendLine()
                    .Append("Vendor: ").Append(currentPlatform.Vendor).AppendLine();                   
                descriptions.Add(builder.ToString());
            }
            return descriptions;
        }

        public static List<string> GetComputePlatformDevicesDescriptions(ComputePlatform computePlatform)
        {
            var descriptions = new List<string>(computePlatform.Devices.Count);
            var devices = computePlatform.Devices;
            for (var i = 0; i < devices.Count; i++)
            {
                var currentDevice = devices[i];
                var deviceDescription = GetComputeDeviceDescription(currentDevice);
                descriptions.Add(deviceDescription);
            }
            return descriptions;
        }

        public static string GetComputeDeviceDescription(ComputeDevice currentDevice)
        {
            var builder = new StringBuilder(150);
            builder
                .Append("Name: ").Append(currentDevice.Name).AppendLine()
                .Append("Profile: ").Append(currentDevice.Profile).AppendLine()
                .Append("Version: ").Append(currentDevice.Version).AppendLine()
                .Append("DriverVersion: ").Append(currentDevice.DriverVersion).AppendLine()
                .Append("MaxComputeUnits: ").Append(currentDevice.MaxComputeUnits).AppendLine()
                .Append("MaxSamplers: ").Append(currentDevice.MaxSamplers).AppendLine()
                .Append("Vendor: ").Append(currentDevice.Vendor).AppendLine()
                .Append("ErrorCorrectionSupport: ").Append(currentDevice.ErrorCorrectionSupport).AppendLine()
                .Append("Available: ").Append(currentDevice.Available).AppendLine();
            var deviceDescription = builder.ToString();
            return deviceDescription;
        }

        public static List<string> GetAllComputePlatformsDevicesDescriptions()
        {
            var platforms = GetComputePlatforms();
            var descriptions = new List<string>(platforms.Count);
            foreach (var computePlatform in platforms)
            {
                descriptions
                    .AddRange(GetComputePlatformDevicesDescriptions(computePlatform));
            }
            return descriptions;
        }
    }
}
