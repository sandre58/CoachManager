using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using My.CoachManager.Presentation.Wpf.Controls.Helpers;
using My.CoachManager.Presentation.Wpf.Controls.Native;

namespace My.CoachManager.Presentation.Wpf.Controls.Commands
{
    internal static class SystemCommands
    {
        public static RoutedCommand CloseWindowCommand { get; private set; }
        public static RoutedCommand MaximizeWindowCommand { get; private set; }
        public static RoutedCommand MinimizeWindowCommand { get; private set; }
        public static RoutedCommand RestoreWindowCommand { get; private set; }
        public static RoutedCommand ShowSystemMenuCommand { get; private set; }

        static SystemCommands()
        {
            CloseWindowCommand = new RoutedCommand("CloseWindow", typeof(SystemCommands));
            MaximizeWindowCommand = new RoutedCommand("MaximizeWindow", typeof(SystemCommands));
            MinimizeWindowCommand = new RoutedCommand("MinimizeWindow", typeof(SystemCommands));
            RestoreWindowCommand = new RoutedCommand("RestoreWindow", typeof(SystemCommands));
            ShowSystemMenuCommand = new RoutedCommand("ShowSystemMenu", typeof(SystemCommands));
        }

        private static void _PostSystemCommand(Window window, NativeMethods.SC command)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero || !NativeMethods.IsWindow(hwnd))
            {
                return;
            }

            NativeMethods.PostMessage(hwnd, NativeMethods.WM.SYSCOMMAND, new IntPtr((int)command), IntPtr.Zero);
        }

        public static void CloseWindow(Window window)
        {
            _PostSystemCommand(window, NativeMethods.SC.CLOSE);
        }

        public static void MaximizeWindow(Window window)
        {
            _PostSystemCommand(window, NativeMethods.SC.MAXIMIZE);
        }

        public static void MinimizeWindow(Window window)
        {
            _PostSystemCommand(window, NativeMethods.SC.MINIMIZE);
        }

        public static void RestoreWindow(Window window)
        {
            _PostSystemCommand(window, NativeMethods.SC.RESTORE);
        }

        /// <summary>Display the system menu at a specified location.</summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="screenLocation">The location to display the system menu, in logical screen coordinates.</param>
        public static void ShowSystemMenu(Window window, Point screenLocation)
        {
            ShowSystemMenuPhysicalCoordinates(window, DpiHelper.LogicalPixelsToDevice(screenLocation));
        }

        internal static void ShowSystemMenuPhysicalCoordinates(Window window, Point physicalScreenLocation)
        {
            const uint TPM_RETURNCMD = 0x0100;
            const uint TPM_LEFTBUTTON = 0x0;

            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero || !NativeMethods.IsWindow(hwnd))
            {
                return;
            }

            IntPtr hmenu = NativeMethods.GetSystemMenu(hwnd, false);

            uint cmd = NativeMethods.TrackPopupMenuEx(hmenu, TPM_LEFTBUTTON | TPM_RETURNCMD, (int)physicalScreenLocation.X, (int)physicalScreenLocation.Y, hwnd, IntPtr.Zero);
            if (0 != cmd)
            {
                NativeMethods.PostMessage(hwnd, NativeMethods.WM.SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
            }
        }
    }
}