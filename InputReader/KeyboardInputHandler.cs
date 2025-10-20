using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.InputReader
{
    public class KeyboardInputHandler
    {
        public event EventHandler<KeyEventArgs> OnKeyPress = new ((a, b) => { });

        public void RaiseEvent(Keys key, SysKeys sysKeys)
        {
            var args = new KeyEventArgs(key);
            OnKeyPress.Invoke(null, key)
        }
    }
}
