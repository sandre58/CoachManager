﻿using System;
using System.Runtime.InteropServices;
using My.CoachManager.Presentation.Prism.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Controls.Native
{
    internal class NativeMethods
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
    }
}