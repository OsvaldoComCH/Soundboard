using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.InputReader
{
    public class KeyboardHookManager
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private static LowLevelKeyboardProc Proc = HookCallback;
        private static IntPtr HookID = IntPtr.Zero;

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        public static void StartHook()
        {
            HookID = SetHook(Proc);
        }

        public static void StopHook()
        {
            UnhookWindowsHookEx(HookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule?.ModuleName ?? ""), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                Keys vkCode = (Keys)Marshal.ReadInt32(lParam);
                if ((GetAsyncKeyState(Keys.ShiftKey) & 0x8000) != 0)
                {
                    vkCode |= Keys.Shift;
                }
                if ((GetAsyncKeyState(Keys.ControlKey) & 0x8000) != 0)
                {
                    vkCode |= Keys.ControlKey;
                }
                if ((GetAsyncKeyState(Keys.Menu) & 0x8000) != 0)
                {
                    vkCode |= Keys.Alt;
                }

                KeyboardInputHandler.RaiseEvent(vkCode);
            }
            return CallNextHookEx(HookID, nCode, wParam, lParam);
        }
    }
}
