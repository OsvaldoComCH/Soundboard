using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.InputReader
{
    public class KeyboardInputHandler
    {
        public static event EventHandler<KeyEventArgs> OnKeyPress = new ((a, b) => { });

        public static void RaiseEvent(Keys key)
        {
            var args = new KeyEventArgs(key);
            OnKeyPress.Invoke(null, args);
        }
    }
}
