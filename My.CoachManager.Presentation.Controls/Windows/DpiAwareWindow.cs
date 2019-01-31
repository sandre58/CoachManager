﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.Win32;
using My.CoachManager.Presentation.Controls.Helpers;
using My.CoachManager.Presentation.Controls.Native;

namespace My.CoachManager.Presentation.Controls.Windows
{
    /// <summary>
    /// A window instance that is capable of per-monitor DPI awareness when supported.
    /// </summary>
    public abstract class DpiAwareWindow
        : Window
    {
        /// <summary>
        /// Occurs when the system or monitor DPI for this window has changed.
        /// </summary>
        public new event EventHandler DpiChanged;

        private HwndSource _source;
        private DpiInformation _dpiInfo;
        private readonly bool _isPerMonitorDpiAware;

        /// <summary>
        /// Initializes a new instance of the <see cref="DpiAwareWindow"/> class.
        /// </summary>
        public DpiAwareWindow()
        {
            SourceInitialized += OnSourceInitialized;

            // WM_DPICHANGED is not send when window is minimized, do listen to global display setting changes
            SystemEvents.DisplaySettingsChanged += OnSystemEventsDisplaySettingsChanged;

            // try to set per-monitor dpi awareness, before the window is displayed
            _isPerMonitorDpiAware = Helper.TrySetPerMonitorDpiAware();
        }

        /// <summary>
        /// Gets the DPI information for this window instance.
        /// </summary>
        /// <remarks>
        /// DPI information is available after a window handle has been created.
        /// </remarks>
        public DpiInformation DpiInformation
        {
            get { return _dpiInfo; }
        }

        /// <summary>
        /// Raises the System.Windows.Window.Closed event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // detach global event handlers
            SystemEvents.DisplaySettingsChanged -= OnSystemEventsDisplaySettingsChanged;
        }

        private void OnSystemEventsDisplaySettingsChanged(object sender, EventArgs e)
        {
            if (_source != null && WindowState == WindowState.Minimized)
            {
                RefreshMonitorDpi();
            }
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _source = (HwndSource)PresentationSource.FromVisual(this);

            // calculate the DPI used by WPF; this is the same as the system DPI
            if (_source != null)
            {
                if (_source.CompositionTarget != null)
                {
                    var matrix = _source.CompositionTarget.TransformToDevice;

                    _dpiInfo = new DpiInformation(96D * matrix.M11, 96D * matrix.M22);
                }
            }

            if (_isPerMonitorDpiAware)
            {
                if (_source != null) _source.AddHook(WndProc);

                RefreshMonitorDpi();
            }
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == UnsafeNativeMethods.WM_DPICHANGED)
            {
                // Marshal the value in the lParam into a Rect.
                var newDisplayRect = (RECT)Marshal.PtrToStructure(lParam, typeof(RECT));

                // Set the Window's position & size.
                if (_source.CompositionTarget != null)
                {
                    var matrix = _source.CompositionTarget.TransformFromDevice;
                    var ul = matrix.Transform(new Vector(newDisplayRect.left, newDisplayRect.top));
                    var hw = matrix.Transform(new Vector(newDisplayRect.right - newDisplayRect.left, newDisplayRect.bottom - newDisplayRect.top));
                    Left = ul.X;
                    Top = ul.Y;
                    UpdateWindowSize(hw.X, hw.Y);
                }

                // Remember the current DPI settings.
                var oldDpiX = _dpiInfo.MonitorDpiX;
                var oldDpiY = _dpiInfo.MonitorDpiY;

                // Get the new DPI settings from wParam
                var dpiX = (double)(wParam.ToInt32() >> 16);
                var dpiY = (double)(wParam.ToInt32() & 0x0000FFFF);

                if (oldDpiX != dpiX || oldDpiY != dpiY)
                {
                    _dpiInfo.UpdateMonitorDpi(dpiX, dpiY);

                    // update layout scale
                    UpdateLayoutTransform();

                    // raise DpiChanged event
                    OnDpiChanged(EventArgs.Empty);
                }

                handled = true;
            }
            return IntPtr.Zero;
        }

        private void UpdateLayoutTransform()
        {
            if (_isPerMonitorDpiAware)
            {
                var root = (FrameworkElement)GetVisualChild(0);
                if (root != null)
                {
                    if (_dpiInfo.ScaleX != 1 || _dpiInfo.ScaleY != 1)
                    {
                        root.LayoutTransform = new ScaleTransform(_dpiInfo.ScaleX, _dpiInfo.ScaleY);
                    }
                    else
                    {
                        root.LayoutTransform = null;
                    }
                }
            }
        }

        private void UpdateWindowSize(double width, double height)
        {
            // determine relative scalex and scaley
            var relScaleX = width / Width;
            var relScaleY = height / Height;

            if (relScaleX != 1 || relScaleY != 1)
            {
                // adjust window size constraints as well
                MinWidth *= relScaleX;
                MaxWidth *= relScaleX;
                MinHeight *= relScaleY;
                MaxHeight *= relScaleY;

                Width = width;
                Height = height;
            }
        }

        /// <summary>
        /// Refreshes the current monitor DPI settings and update the window size and layout scale accordingly.
        /// </summary>
        protected void RefreshMonitorDpi()
        {
            if (!_isPerMonitorDpiAware)
            {
                return;
            }

            // get the current DPI of the monitor of the window
            var monitor = UnsafeNativeMethods.MonitorFromWindow(_source.Handle, UnsafeNativeMethods.MONITOR_DEFAULTTONEAREST);

            uint xDpi = 96;
            uint yDpi = 96;
            if (UnsafeNativeMethods.GetDpiForMonitor(monitor, (int)MonitorDpiType.EffectiveDpi, ref xDpi, ref yDpi) != UnsafeNativeMethods.S_OK)
            {
                xDpi = 96;
                yDpi = 96;
            }
            // vector contains the change of the old to new DPI
            var dpiVector = _dpiInfo.UpdateMonitorDpi(xDpi, yDpi);

            // update Width and Height based on the current DPI of the monitor
            UpdateWindowSize(Width * dpiVector.X, Height * dpiVector.Y);

            // update graphics and text based on the current DPI of the monitor
            UpdateLayoutTransform();
        }

        /// <summary>
        /// Raises the <see cref="E:DpiChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnDpiChanged(EventArgs e)
        {
            var handler = DpiChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}