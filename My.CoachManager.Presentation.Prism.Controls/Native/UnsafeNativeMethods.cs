using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using My.CoachManager.Presentation.Prism.Controls.Windows;

namespace My.CoachManager.Presentation.Prism.Controls.Native
{
    internal class UnsafeNativeMethods
    {
        public const int S_OK = 0;
        public const int WM_DPICHANGED = 0x02E0;
        public const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        [DllImport("Shcore.dll")]
        public static extern int GetProcessDpiAwareness(IntPtr hprocess, out ProcessDpiAwareness value);

        [DllImport("Shcore.dll")]
        public static extern int SetProcessDpiAwareness(ProcessDpiAwareness value);

        [DllImport("user32.dll")]
        public static extern bool IsProcessDPIAware();

        [DllImport("user32.dll")]
        public static extern int SetProcessDPIAware();

        [DllImport("shcore.dll")]
        public static extern int GetDpiForMonitor(IntPtr hMonitor, int dpiType, ref uint xDpi, ref uint yDpi);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int flag);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms633528(v=vs.85).aspx</devdoc>
        [DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern bool IsWindow([In] [Optional] IntPtr hWnd);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms647985(v=vs.85).aspx</devdoc>
        [DllImport("user32")]
        internal static extern IntPtr GetSystemMenu([In] IntPtr hWnd, [In] bool bRevert);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms648003(v=vs.85).aspx</devdoc>
        [DllImport("user32")]
        internal static extern uint TrackPopupMenuEx([In] IntPtr hmenu, [In] uint fuFlags, [In] int x, [In] int y, [In] IntPtr hwnd, [In] [Optional] IntPtr lptpm);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644944(v=vs.85).aspx</devdoc>
        [DllImport("user32", EntryPoint = "PostMessage", SetLastError = true)]
        private static extern bool _PostMessage([In] [Optional] IntPtr hWnd, [In] uint Msg, [In] IntPtr wParam, [In] IntPtr lParam);

        internal static void PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam)
        {
            if (!_PostMessage(hWnd, Msg, wParam, lParam))
            {
                throw new Win32Exception();
            }
        }
    }
}