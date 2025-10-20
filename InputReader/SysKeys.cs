using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.InputReader
{
    [Flags]
    public enum SysKeys
    {
        None = 0,
        Ctrl = 1,
        Shift = 2,
        Alt = 4,
        System = 8
    }
}
