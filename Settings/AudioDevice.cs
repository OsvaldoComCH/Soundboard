using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.Settings
{
    public class AudioDevice
    {
        public required string DeviceName { get; set; }

        public required int DeviceNumber { get; set; }
    }
}
