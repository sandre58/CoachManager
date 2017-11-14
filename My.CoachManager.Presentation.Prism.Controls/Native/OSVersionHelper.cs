﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace My.CoachManager.Presentation.Prism.Controls.Native
{
    // Taken from http://www.codeproject.com/Articles/707502/Version-Helper-API-for-NET
    // License: The Code Project Open License

    /// <summary>
    /// .NET wrapper for Version Helper functions.
    /// http://msdn.microsoft.com/library/windows/desktop/dn424972.aspx
    /// </summary>
    public static class OsVersionHelper
    {
        #region Supplementary data types

        /// <summary>
        /// Operating systems, the information which is stored within
        /// the class <seealso cref="OsVersionHelper"/>.
        /// </summary>
        public enum KnownOs
        {
            /// <summary>
            /// Windows XP.
            /// </summary>
            WindowsXp,

            /// <summary>
            /// Windows XP SP1.
            /// </summary>
            WindowsXpsp1,

            /// <summary>
            /// Windows XP SP2.
            /// </summary>
            WindowsXpsp2,

            /// <summary>
            /// Windows XP SP3.
            /// </summary>
            WindowsXpsp3,

            /// <summary>
            /// Windows Vista.
            /// </summary>
            WindowsVista,

            /// <summary>
            /// Windows Vista SP1.
            /// </summary>
            WindowsVistaSp1,

            /// <summary>
            /// Windows Vista SP2.
            /// </summary>
            WindowsVistaSp2,

            /// <summary>
            /// Windows 7.
            /// </summary>
            Windows7,

            /// <summary>
            /// Windows 7 SP1.
            /// </summary>
            Windows7Sp1,

            /// <summary>
            /// Windows 8.
            /// </summary>
            Windows8,

            /// <summary>
            /// Windows 8.1.
            /// </summary>
            Windows8Point1
        }

        /// <summary>
        /// Information about operating system.
        /// </summary>
        private sealed class OsEntry
        {
            #region Properties

            /// <summary>
            /// The major version number of the operating system.
            /// </summary>
            public uint MajorVersion { get; private set; }

            /// <summary>
            /// The minor version number of the operating system.
            /// </summary>
            public uint MinorVersion { get; private set; }

            /// <summary>
            /// The major version number of the latest Service Pack installed
            /// on the system. For example, for Service Pack 3, the major
            /// version number is 3. If no Service Pack has been installed,
            /// the value is zero.
            /// </summary>
            public ushort ServicePackMajor { get; private set; }

            /// <summary>
            /// Flag indicating if the running OS matches, or is greater
            /// than, the OS specified with this entry. Should be initialized
            /// with <see cref="VerifyVersionInfo"/> method.
            /// </summary>
            public bool? MatchesOrGreater { get; set; }

            #endregion Properties

            #region Constructor

            /// <summary>
            /// Creates a new entry of operating system.
            /// </summary>
            /// <param name="majorVersion">The major version number of the
            /// operating system.</param>
            /// <param name="minorVersion">The minor version number of the
            /// operating system.</param>
            /// <param name="servicePackMajor">The major version number of the
            /// latest Service Pack installed on the system. For example, for
            /// Service Pack 3, the major version number is 3. If no Service
            /// Pack has been installed, the value is zero.</param>
            public OsEntry(uint majorVersion, uint minorVersion,
                ushort servicePackMajor)
            {
                MajorVersion = majorVersion;
                MinorVersion = minorVersion;
                ServicePackMajor = servicePackMajor;
            }

            #endregion Constructor
        }

        #endregion Supplementary data types

        #region PInvoke data type declarations

        /// <summary>
        /// Wrapper for OSVERSIONINFOEX structure.
        /// http://msdn.microsoft.com/library/windows/desktop/ms724833.aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct OsVersionInfoEx
        {
            /// <summary>
            /// The size of this data structure, in bytes.
            /// </summary>
            public uint OSVersionInfoSize;

            /// <summary>
            /// The major version number of the operating system.
            /// </summary>
            public uint MajorVersion;

            /// <summary>
            /// The minor version number of the operating system.
            /// </summary>
            public uint MinorVersion;

            /// <summary>
            /// The build number of the operating system.
            /// </summary>
            public uint BuildNumber;

            /// <summary>
            /// The operating system platform.
            /// </summary>
            public uint PlatformId;

            /// <summary>
            /// A null-terminated string, such as "Service Pack 3", that
            /// indicates the latest Service Pack installed on the system. If
            /// no Service Pack has been installed, the string is empty.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string CSDVersion;

            /// <summary>
            /// The major version number of the latest Service Pack installed
            /// on the system. For example, for Service Pack 3, the major
            /// version number is 3. If no Service Pack has been installed,
            /// the value is zero.
            /// </summary>
            public ushort ServicePackMajor;

            /// <summary>
            /// The minor version number of the latest Service Pack installed
            /// on the system. For example, for Service Pack 3, the minor
            /// version number is 0.
            /// </summary>
            public ushort ServicePackMinor;

            /// <summary>
            /// A bit mask that identifies the product suites available on the
            /// system, e.g., flags indicating if the operating system is
            /// Datacenter Server or Windows XP Embedded.
            /// </summary>
            public ushort SuiteMask;

            /// <summary>
            /// Any additional information about the system, e.g., flags
            /// indicating if the operating system is a domain controller,
            /// a server or a workstation.
            /// </summary>
            public byte ProductType;

            /// <summary>
            /// Reserved for future use.
            /// </summary>
            public byte Reserved;
        }

        #endregion PInvoke data type declarations

        #region PInvoke function declarations

        /// <summary>
        /// <para>Wrapper for VerSetConditionMask function (
        /// http://msdn.microsoft.com/library/windows/desktop/ms725493.aspx).
        /// </para>
        /// <para>
        /// Sets the bits of a 64-bit value to indicate the comparison
        /// operator to use for a specified operating system version
        /// attribute. This method is used to build the dwlConditionMask
        /// parameter of the <see cref="VerifyVersionInfo"/> method.
        /// </para>
        /// </summary>
        /// <param name="dwlConditionMask">
        /// <para>A value to be passed as the dwlConditionMask parameter of
        /// the <see cref="VerifyVersionInfo"/> method. The function stores
        /// the comparison information in the bits of this variable.
        /// </para>
        /// <para>
        /// Before the first call to VerSetConditionMask, initialize this
        /// variable to zero. For subsequent calls, pass in the variable used
        /// in the previous call.
        /// </para>
        /// </param>
        /// <param name="dwTypeBitMask">A mask that indicates the member of
        /// the <see cref="OsVersionInfoEx"/> structure whose comparison
        /// operator is being set.</param>
        /// <param name="dwConditionMask">The operator to be used for the
        /// comparison.</param>
        /// <returns>Condition mask value.</returns>
        [DllImport("kernel32.dll")]
        private static extern ulong VerSetConditionMask(ulong dwlConditionMask,
           uint dwTypeBitMask, byte dwConditionMask);

        /// <summary>
        /// <para>
        /// Wrapper for VerifyVersionInfo function (
        /// http://msdn.microsoft.com/library/windows/desktop/ms725492.aspx).
        /// </para>
        /// <para>
        /// Compares a set of operating system version requirements to the
        /// corresponding values for the currently running version of the
        /// system.
        /// </para>
        /// </summary>
        /// <param name="lpVersionInfo">A pointer to an
        /// <see cref="OsVersionInfoEx"/> structure containing the operating
        /// system version requirements to compare.</param>
        /// <param name="dwTypeMask">A mask that indicates the members of the
        /// <see cref="OsVersionInfoEx"/> structure to be tested.</param>
        /// <param name="dwlConditionMask">The type of comparison to be used
        /// for each lpVersionInfo member being compared. Can be constructed
        /// with <see cref="VerSetConditionMask"/> method.</param>
        /// <returns>True if the current Windows OS satisfies the specified
        /// requirements; otherwise, false.</returns>
        [DllImport("kernel32.dll")]
        private static extern bool VerifyVersionInfo(
            [In] ref OsVersionInfoEx lpVersionInfo,
            uint dwTypeMask, ulong dwlConditionMask);

        #endregion PInvoke function declarations

        #region Local fields

        private static Dictionary<KnownOs, OsEntry> _osEntries;

        private static bool? _isServer = null;

        private static ulong? _versionOrGreaterMask;
        private static uint? _versionOrGreaterTypeMask;

        #endregion Local fields

        #region Constructor

        /// <summary>
        /// Initializes dictionary of operating systems.
        /// </summary>
        static OsVersionHelper()
        {
            _osEntries = new Dictionary<KnownOs, OsEntry>();
            _osEntries.Add(KnownOs.WindowsXp, new OsEntry(5, 1, 0));
            _osEntries.Add(KnownOs.WindowsXpsp1, new OsEntry(5, 1, 1));
            _osEntries.Add(KnownOs.WindowsXpsp2, new OsEntry(5, 1, 2));
            _osEntries.Add(KnownOs.WindowsXpsp3, new OsEntry(5, 1, 3));
            _osEntries.Add(KnownOs.WindowsVista, new OsEntry(6, 0, 0));
            _osEntries.Add(KnownOs.WindowsVistaSp1, new OsEntry(6, 0, 1));
            _osEntries.Add(KnownOs.WindowsVistaSp2, new OsEntry(6, 0, 2));
            _osEntries.Add(KnownOs.Windows7, new OsEntry(6, 1, 0));
            _osEntries.Add(KnownOs.Windows7Sp1, new OsEntry(6, 1, 1));
            _osEntries.Add(KnownOs.Windows8, new OsEntry(6, 2, 0));
            _osEntries.Add(KnownOs.Windows8Point1, new OsEntry(6, 3, 0));
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the provided version information. This method is useful in
        /// confirming a version of Windows Server that doesn't share a
        /// version number with a client release.
        /// </summary>
        /// <param name="majorVersion">The major OS version number.</param>
        /// <param name="minorVersion">The minor OS version number.</param>
        /// <param name="servicePackMajor">The major Service Pack version
        /// number.</param>
        /// <returns>True if the the running OS matches, or is greater
        /// than, the specified version information; otherwise, false.
        /// </returns>
        internal static bool IsWindowsVersionOrGreater(
            uint majorVersion, uint minorVersion, ushort servicePackMajor)
        {
            OsVersionInfoEx osvi = new OsVersionInfoEx();
            osvi.OSVersionInfoSize = (uint)Marshal.SizeOf(osvi);
            osvi.MajorVersion = majorVersion;
            osvi.MinorVersion = minorVersion;
            osvi.ServicePackMajor = servicePackMajor;

            // These constants initialized with corresponding definitions in
            // winnt.h (part of Windows SDK)
            const uint verMinorversion = 0x0000001;
            const uint verMajorversion = 0x0000002;
            const uint verServicepackmajor = 0x0000020;
            const byte verGreaterEqual = 3;

            if (!_versionOrGreaterMask.HasValue)
            {
                _versionOrGreaterMask = VerSetConditionMask(
                    VerSetConditionMask(
                        VerSetConditionMask(
                            0, verMajorversion, verGreaterEqual),
                        verMinorversion, verGreaterEqual),
                    verServicepackmajor, verGreaterEqual);
            }

            if (!_versionOrGreaterTypeMask.HasValue)
            {
                _versionOrGreaterTypeMask = verMajorversion |
                    verMinorversion | verServicepackmajor;
            }

            return VerifyVersionInfo(ref osvi, _versionOrGreaterTypeMask.Value,
                _versionOrGreaterMask.Value);
        }

        /// <summary>
        /// Indicates if the running OS version matches, or is greater than,
        /// the provided OS.
        /// </summary>
        /// <param name="os">OS to compare running OS to.</param>
        /// <returns>True if the the running OS matches, or is greater
        /// than, the specified OS; otherwise, false.</returns>
        public static bool IsWindowsVersionOrGreater(KnownOs os)
        {
            try
            {
                OsEntry osEntry = _osEntries[os];
                if (!osEntry.MatchesOrGreater.HasValue)
                {
                    osEntry.MatchesOrGreater = IsWindowsVersionOrGreater(
                        osEntry.MajorVersion, osEntry.MinorVersion,
                        osEntry.ServicePackMajor);
                }

                return osEntry.MatchesOrGreater.Value;
            }
            catch (KeyNotFoundException e)
            {
                throw new ArgumentException("Unknow OS", e);
            }
        }

        #endregion Public methods

        #region Public properties

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows XP version.
        /// </summary>
        public static bool IsWindowsXpOrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.WindowsXp); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows XP with Service Pack 1 (SP1) version.
        /// </summary>
        public static bool IsWindowsXpsp1OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.WindowsXpsp1); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows XP with Service Pack 2 (SP2) version.
        /// </summary>
        public static bool IsWindowsXpsp2OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.WindowsXpsp2); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows XP with Service Pack 3 (SP3) version.
        /// </summary>
        public static bool IsWindowsXpsp3OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.WindowsXpsp3); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows Vista version.
        /// </summary>
        public static bool IsWindowsVistaOrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.WindowsVista); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows Vista with Service Pack 1 (SP1) version.
        /// </summary>
        public static bool IsWindowsVistaSp1OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.WindowsVistaSp1); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows Vista with Service Pack 2 (SP2) version.
        /// </summary>
        public static bool IsWindowsVistaSp2OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.WindowsVistaSp2); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows 7 version.
        /// </summary>
        public static bool IsWindows7OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.Windows7); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows 7 with Service Pack 1 (SP1) version.
        /// </summary>
        public static bool IsWindows7Sp1OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.Windows7Sp1); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows 8 version.
        /// </summary>
        public static bool IsWindows8OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.Windows8); }
        }

        /// <summary>
        /// Indicates if the current OS version matches, or is greater than,
        /// the Windows 8.1 version.
        /// </summary>
        public static bool IsWindows8Point1OrGreater
        {
            get { return IsWindowsVersionOrGreater(KnownOs.Windows8Point1); }
        }

        /// <summary>
        /// Indicates if the current OS is a Windows Server release.
        /// </summary>
        public static bool IsWindowsServer
        {
            get
            {
                if (!_isServer.HasValue)
                {
                    // These constants initialized with corresponding
                    // definitions in winnt.h (part of Windows SDK)
                    const byte verNtWorkstation = 0x0000001;
                    const uint verProductType = 0x0000080;
                    const byte verEqual = 1;

                    OsVersionInfoEx osvi = new OsVersionInfoEx();
                    osvi.OSVersionInfoSize = (uint)Marshal.SizeOf(osvi);
                    osvi.ProductType = verNtWorkstation;
                    ulong dwlConditionMask = VerSetConditionMask(
                        0, verProductType, verEqual);

                    return !VerifyVersionInfo(
                        ref osvi, verProductType, dwlConditionMask);
                }

                return _isServer.Value;
            }
        }

        #endregion Public properties
    }
}