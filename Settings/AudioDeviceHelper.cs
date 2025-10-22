using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.Settings
{
    public static class AudioDeviceHelper
    {
        public static IEnumerable<AudioDevice> GetOutputDevices()
        {
            for (int i = 0; i < WaveOut.DeviceCount; ++i)
            {
                yield return new AudioDevice()
                {
                    DeviceName = WaveOut.GetCapabilities(i).ProductName,
                    DeviceNumber = i
                };
            }
        }

        public static IEnumerable<AudioDevice> GetInputDevices()
        {
            for (int i = 0; i < WaveIn.DeviceCount; ++i)
            {
                yield return new AudioDevice()
                {
                    DeviceName = WaveIn.GetCapabilities(i).ProductName,
                    DeviceNumber = i
                };
            }
        }

        public static int GetOutputDeviceNumber(string deviceName)
        {
            return GetOutputDevices().FirstOrDefault(x => deviceName.StartsWith(x.DeviceName))?.DeviceNumber ?? -1;
        }

        public static int GetInputDeviceNumber(string deviceName)
        {
            return GetOutputDevices().FirstOrDefault(x => deviceName.StartsWith(x.DeviceName))?.DeviceNumber ?? -1;
        }
    }
}
