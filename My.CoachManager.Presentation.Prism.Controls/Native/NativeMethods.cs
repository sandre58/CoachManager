using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32.SafeHandles;

namespace My.CoachManager.Presentation.Prism.Controls.Native
{
    internal class NativeMethods
    {
        /// <summary>
        /// Window message values, WM_*
        /// </summary>
        internal enum WM
        {
            NULL = 0x0000,
            CREATE = 0x0001,
            DESTROY = 0x0002,
            MOVE = 0x0003,
            SIZE = 0x0005,
            ACTIVATE = 0x0006,
            SETFOCUS = 0x0007,
            KILLFOCUS = 0x0008,
            ENABLE = 0x000A,
            SETREDRAW = 0x000B,
            SETTEXT = 0x000C,
            GETTEXT = 0x000D,
            GETTEXTLENGTH = 0x000E,
            PAINT = 0x000F,
            CLOSE = 0x0010,
            QUERYENDSESSION = 0x0011,
            QUIT = 0x0012,
            QUERYOPEN = 0x0013,
            ERASEBKGND = 0x0014,
            SYSCOLORCHANGE = 0x0015,
            SHOWWINDOW = 0x0018,
            CTLCOLOR = 0x0019,
            WININICHANGE = 0x001A,
            SETTINGCHANGE = 0x001A,
            ACTIVATEAPP = 0x001C,
            SETCURSOR = 0x0020,
            MOUSEACTIVATE = 0x0021,
            CHILDACTIVATE = 0x0022,
            QUEUESYNC = 0x0023,
            GETMINMAXINFO = 0x0024,

            WINDOWPOSCHANGING = 0x0046,
            WINDOWPOSCHANGED = 0x0047,

            CONTEXTMENU = 0x007B,
            STYLECHANGING = 0x007C,
            STYLECHANGED = 0x007D,
            DISPLAYCHANGE = 0x007E,
            GETICON = 0x007F,
            SETICON = 0x0080,
            NCCREATE = 0x0081,
            NCDESTROY = 0x0082,
            NCCALCSIZE = 0x0083,
            NCHITTEST = 0x0084,
            NCPAINT = 0x0085,
            NCACTIVATE = 0x0086,
            GETDLGCODE = 0x0087,
            SYNCPAINT = 0x0088,
            NCMOUSEMOVE = 0x00A0,
            NCLBUTTONDOWN = 0x00A1,
            NCLBUTTONUP = 0x00A2,
            NCLBUTTONDBLCLK = 0x00A3,
            NCRBUTTONDOWN = 0x00A4,
            NCRBUTTONUP = 0x00A5,
            NCRBUTTONDBLCLK = 0x00A6,
            NCMBUTTONDOWN = 0x00A7,
            NCMBUTTONUP = 0x00A8,
            NCMBUTTONDBLCLK = 0x00A9,

            SYSKEYDOWN = 0x0104,
            SYSKEYUP = 0x0105,
            SYSCHAR = 0x0106,
            SYSDEADCHAR = 0x0107,
            COMMAND = 0x0111,
            SYSCOMMAND = 0x0112,

            // These two messages aren't defined in winuser.h, but they are sent to windows
            // with captions. They appear to paint the window caption and frame.
            // Unfortunately if you override the standard non-client rendering as we do
            // with CustomFrameWindow, sometimes Windows (not deterministically
            // reproducibly but definitely frequently) will send these messages to the
            // window and paint the standard caption/title over the top of the custom one.
            // So we need to handle these messages in CustomFrameWindow to prevent this
            // from happening.
            NCUAHDRAWCAPTION = 0xAE,

            NCUAHDRAWFRAME = 0xAF,

            MOUSEMOVE = 0x0200,
            LBUTTONDOWN = 0x0201,
            LBUTTONUP = 0x0202,
            LBUTTONDBLCLK = 0x0203,
            RBUTTONDOWN = 0x0204,
            RBUTTONUP = 0x0205,
            RBUTTONDBLCLK = 0x0206,
            MBUTTONDOWN = 0x0207,
            MBUTTONUP = 0x0208,
            MBUTTONDBLCLK = 0x0209,
            MOUSEWHEEL = 0x020A,
            XBUTTONDOWN = 0x020B,
            XBUTTONUP = 0x020C,
            XBUTTONDBLCLK = 0x020D,
            MOUSEHWHEEL = 0x020E,
            PARENTNOTIFY = 0x0210,

            CAPTURECHANGED = 0x0215,
            POWERBROADCAST = 0x0218,
            DEVICECHANGE = 0x0219,

            ENTERSIZEMOVE = 0x0231,
            EXITSIZEMOVE = 0x0232,

            IME_SETCONTEXT = 0x0281,
            IME_NOTIFY = 0x0282,
            IME_CONTROL = 0x0283,
            IME_COMPOSITIONFULL = 0x0284,
            IME_SELECT = 0x0285,
            IME_CHAR = 0x0286,
            IME_REQUEST = 0x0288,
            IME_KEYDOWN = 0x0290,
            IME_KEYUP = 0x0291,

            NCMOUSELEAVE = 0x02A2,

            TABLET_DEFBASE = 0x02C0,
            //WM_TABLET_MAXOFFSET = 0x20,

            TABLET_ADDED = TABLET_DEFBASE + 8,
            TABLET_DELETED = TABLET_DEFBASE + 9,
            TABLET_FLICK = TABLET_DEFBASE + 11,
            TABLET_QUERYSYSTEMGESTURESTATUS = TABLET_DEFBASE + 12,

            CUT = 0x0300,
            COPY = 0x0301,
            PASTE = 0x0302,
            CLEAR = 0x0303,
            UNDO = 0x0304,
            RENDERFORMAT = 0x0305,
            RENDERALLFORMATS = 0x0306,
            DESTROYCLIPBOARD = 0x0307,
            DRAWCLIPBOARD = 0x0308,
            PAINTCLIPBOARD = 0x0309,
            VSCROLLCLIPBOARD = 0x030A,
            SIZECLIPBOARD = 0x030B,
            ASKCBFORMATNAME = 0x030C,
            CHANGECBCHAIN = 0x030D,
            HSCROLLCLIPBOARD = 0x030E,
            QUERYNEWPALETTE = 0x030F,
            PALETTEISCHANGING = 0x0310,
            PALETTECHANGED = 0x0311,
            HOTKEY = 0x0312,
            PRINT = 0x0317,
            PRINTCLIENT = 0x0318,
            APPCOMMAND = 0x0319,
            THEMECHANGED = 0x031A,

            DWMCOMPOSITIONCHANGED = 0x031E,
            DWMNCRENDERINGCHANGED = 0x031F,
            DWMCOLORIZATIONCOLORCHANGED = 0x0320,
            DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

            GETTITLEBARINFOEX = 0x033F,

            #region Windows 7

            DWMSENDICONICTHUMBNAIL = 0x0323,
            DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,

            #endregion Windows 7

            USER = 0x0400,

            // This is the hard-coded message value used by WinForms for Shell_NotifyIcon.
            // It's relatively safe to reuse.
            TRAYMOUSEMESSAGE = 0x800, //WM_USER + 1024

            APP = 0x8000,
        }

        internal enum SC
        {
            SIZE = 0xF000,
            MOVE = 0xF010,
            MOUSEMOVE = 0xF012,
            MINIMIZE = 0xF020,
            MAXIMIZE = 0xF030,
            NEXTWINDOW = 0xF040,
            PREVWINDOW = 0xF050,
            CLOSE = 0xF060,
            VSCROLL = 0xF070,
            HSCROLL = 0xF080,
            MOUSEMENU = 0xF090,
            KEYMENU = 0xF100,
            ARRANGE = 0xF110,
            RESTORE = 0xF120,
            TASKLIST = 0xF130,
            SCREENSAVE = 0xF140,
            HOTKEY = 0xF150,
            DEFAULT = 0xF160,
            MONITORPOWER = 0xF170,
            CONTEXTHELP = 0xF180,
            SEPARATOR = 0xF00F,

            /// <summary>
            /// SCF_ISSECURE
            /// </summary>
            F_ISSECURE = 0x00000001,

            ICON = MINIMIZE,
            ZOOM = MAXIMIZE,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StartupOutput
        {
            public IntPtr hook;
            public IntPtr unhook;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class StartupInput
        {
            public int GdiplusVersion = 1;
            public IntPtr DebugEventCallback;
            public bool SuppressBackgroundThread;
            public bool SuppressExternalCodecs;
        }

        /// <summary>
        /// GDI+ Status codes
        /// </summary>
        internal enum Status
        {
            Ok = 0,
            GenericError = 1,
            InvalidParameter = 2,
            OutOfMemory = 3,
            ObjectBusy = 4,
            InsufficientBuffer = 5,
            NotImplemented = 6,
            Win32Error = 7,
            WrongState = 8,
            Aborted = 9,
            FileNotFound = 10,
            ValueOverflow = 11,
            AccessDenied = 12,
            UnknownImageFormat = 13,
            FontFamilyNotFound = 14,
            FontStyleNotFound = 15,
            NotTrueTypeFont = 16,
            UnsupportedGdiplusVersion = 17,
            GdiplusNotInitialized = 18,
            PropertyNotFound = 19,
            PropertyNotSupported = 20,
            ProfileNotFound = 21,
        }

        /// <summary>
        /// GetDeviceCaps nIndex values.
        /// </summary>
        internal enum DeviceCap
        {
            /// <summary>Number of bits per pixel
            /// </summary>
            BITSPIXEL = 12,

            /// <summary>
            /// Number of planes
            /// </summary>
            PLANES = 14,

            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,

            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90,
        }

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms647486%28v=vs.85%29.aspx</devdoc>
        [DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true, EntryPoint = "LoadStringW", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        internal static extern int LoadString([In] [Optional] SafeLibraryHandle hInstance, [In] uint uID, [Out] StringBuilder lpBuffer, [In] int nBufferMax);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms633528(v=vs.85).aspx</devdoc>
        [DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern bool IsWindow([In] [Optional] IntPtr hWnd);

        // Depending on the message, callers may want to call GetLastError based on the return value.
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms647985(v=vs.85).aspx</devdoc>
        [DllImport("user32")]
        internal static extern IntPtr GetSystemMenu([In] IntPtr hWnd, [In] bool bRevert);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms648003(v=vs.85).aspx</devdoc>
        [DllImport("user32")]
        internal static extern uint TrackPopupMenuEx([In] IntPtr hmenu, [In] uint fuFlags, [In] int x, [In] int y, [In] IntPtr hwnd, [In] [Optional] IntPtr lptpm);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms684175%28v=vs.85%29.aspx</devdoc>
        [DllImport("kernel32", CharSet = CharSet.Unicode, ExactSpelling = true, EntryPoint = "LoadLibraryW", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        internal static extern SafeLibraryHandle LoadLibrary([In] [MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        /// <devdoc>http://msdn.microsoft.com/en-us/library/windows/desktop/ms683152%28v=vs.85%29.aspx</devdoc>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary([In] IntPtr hModule);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _PostMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static void PostMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam)
        {
            if (!_PostMessage(hWnd, Msg, wParam, lParam))
            {
                throw new Win32Exception();
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(SafeDC hdc, DeviceCap nIndex);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("kernel32.dll")]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClose(IntPtr handle);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("gdiplus.dll")]
        public static extern Status GdiplusStartup(out IntPtr token, StartupInput input, out StartupOutput output);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("gdiplus.dll")]
        public static extern Status GdiplusShutdown(IntPtr token);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void SafeRelease<T>(ref T comObject) where T : class
        {
            T t = comObject;
            comObject = default(T);
            if (null != t)
            {
                Marshal.ReleaseComObject(t);
            }
        }

        #region SafeHandles

        internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeFindHandle() : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                return FindClose(handle);
            }
        }

        internal sealed class SafeDC : SafeHandleZeroOrMinusOneIsInvalid
        {
            private static class NativeMethods
            {
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [DllImport("user32.dll")]
                public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [DllImport("user32.dll")]
                public static extern SafeDC GetDC(IntPtr hwnd);

                // Weird legacy function, documentation is unclear about how to use it...
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
                public static extern SafeDC CreateDC([MarshalAs(UnmanagedType.LPWStr)] string lpszDriver, [MarshalAs(UnmanagedType.LPWStr)] string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);

                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern SafeDC CreateCompatibleDC(IntPtr hdc);

                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [DllImport("gdi32.dll")]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DeleteDC(IntPtr hdc);
            }

            private IntPtr? _hwnd;
            private bool _created;

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public IntPtr Hwnd
            {
                set
                {
                    _hwnd = value;
                }
            }

            private SafeDC() : base(true)
            {
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                if (_created)
                {
                    return NativeMethods.DeleteDC(handle);
                }

                if (!_hwnd.HasValue || _hwnd.Value == IntPtr.Zero)
                {
                    return true;
                }

                return NativeMethods.ReleaseDC(_hwnd.Value, handle) == 1;
            }

            [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static SafeDC CreateDC(string deviceName)
            {
                SafeDC dc = null;
                try
                {
                    // Should this really be on the driver parameter?
                    dc = NativeMethods.CreateDC(deviceName, null, IntPtr.Zero, IntPtr.Zero);
                }
                finally
                {
                    if (dc != null)
                    {
                        dc._created = true;
                    }
                }

                if (dc.IsInvalid)
                {
                    dc.Dispose();
                    throw new SystemException("Unable to create a device context from the specified device information.");
                }

                return dc;
            }

            [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static SafeDC CreateCompatibleDC(SafeDC hdc)
            {
                SafeDC dc = null;
                try
                {
                    IntPtr hPtr = IntPtr.Zero;
                    if (hdc != null)
                    {
                        hPtr = hdc.handle;
                    }
                    dc = NativeMethods.CreateCompatibleDC(hPtr);
                    if (dc == null)
                    {
                        HRESULT.ThrowLastError();
                    }
                }
                finally
                {
                    if (dc != null)
                    {
                        dc._created = true;
                    }
                }

                if (dc.IsInvalid)
                {
                    dc.Dispose();
                    throw new SystemException("Unable to create a device context from the specified device information.");
                }

                return dc;
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static SafeDC GetDC(IntPtr hwnd)
            {
                SafeDC dc = null;
                try
                {
                    dc = NativeMethods.GetDC(hwnd);
                }
                finally
                {
                    if (dc != null)
                    {
                        dc.Hwnd = hwnd;
                    }
                }

                if (dc.IsInvalid)
                {
                    // GetDC does not set the last error...
                    HRESULT.E_FAIL.ThrowIfFailed();
                }

                return dc;
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static SafeDC GetDesktop()
            {
                return GetDC(IntPtr.Zero);
            }

            // In method 'SafeDC.WrapDC(IntPtr)', object '<>g__initLocal0' is not disposed along all exception paths.
            // Call System.IDisposable.Dispose on object '<>g__initLocal0' before all references to it are out of scope.
            // Sure...
            [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static SafeDC WrapDC(IntPtr hdc)
            {
                // This won't actually get released by the class, but it allows an IntPtr to be converted for signatures.
                return new SafeDC
                {
                    handle = hdc,
                    _created = false,
                    _hwnd = IntPtr.Zero,
                };
            }
        }

        internal sealed class SafeHBITMAP : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeHBITMAP() : base(true)
            {
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                return NativeMethods.DeleteObject(handle);
            }
        }

        internal sealed class SafeGdiplusStartupToken : SafeHandleZeroOrMinusOneIsInvalid
        {
            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            private SafeGdiplusStartupToken(IntPtr ptr) : base(true)
            {
                handle = ptr;
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                Status s = GdiplusShutdown(this.handle);
                return s == Status.Ok;
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
            public static SafeGdiplusStartupToken Startup()
            {
                IntPtr unsafeHandle;
                StartupOutput output;
                Status s = GdiplusStartup(out unsafeHandle, new StartupInput(), out output);
                if (s == Status.Ok)
                {
                    SafeGdiplusStartupToken safeHandle = new SafeGdiplusStartupToken(unsafeHandle);
                    return safeHandle;
                }
                throw new Exception("Unable to initialize GDI+");
            }
        }

        internal sealed class SafeConnectionPointCookie : SafeHandleZeroOrMinusOneIsInvalid
        {
            private IConnectionPoint _cp;
            // handle holds the cookie value.

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IConnectionPoint")]
            public SafeConnectionPointCookie(IConnectionPointContainer target, object sink, Guid eventId)
                : base(true)
            {
                handle = IntPtr.Zero;

                IConnectionPoint cp = null;
                try
                {
                    int dwCookie;
                    target.FindConnectionPoint(ref eventId, out cp);
                    cp.Advise(sink, out dwCookie);
                    if (dwCookie == 0)
                    {
                        throw new InvalidOperationException("IConnectionPoint::Advise returned an invalid cookie.");
                    }
                    handle = new IntPtr(dwCookie);
                    _cp = cp;
                    cp = null;
                }
                finally
                {
                    SafeRelease(ref cp);
                }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public void Disconnect()
            {
                ReleaseHandle();
            }

            [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                try
                {
                    if (!this.IsInvalid)
                    {
                        int dwCookie = handle.ToInt32();
                        handle = IntPtr.Zero;

                        try
                        {
                            _cp.Unadvise(dwCookie);
                        }
                        finally
                        {
                            SafeRelease(ref _cp);
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion SafeHandles
    }
}