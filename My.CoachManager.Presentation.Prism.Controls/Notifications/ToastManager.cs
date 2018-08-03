using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace My.CoachManager.Presentation.Prism.Controls.Notifications
{
    public class ToastManager : IToastManager
    {
        #region Fields

        private readonly Dispatcher _dispatcher;
        private static readonly List<ToastArea> Areas = new List<ToastArea>();
        private static Window _window;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of <see cref="ToastManager"/>
        /// </summary>
        /// <param name="dispatcher"></param>
        public ToastManager(Dispatcher dispatcher = null)
        {
            if (dispatcher == null)
            {
                dispatcher = Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;
            }

            _dispatcher = dispatcher;
        }

        #endregion

        #region IToastManager

        /// <inheritdoc />
        /// <summary>
        /// Show a toast notification.
        /// </summary>
        /// <param name="content">Notification content.</param>
        /// <param name="areaName">Area name.</param>
        /// <param name="expirationTime">Duration.</param>
        /// <param name="onClick">OnClick action.</param>
        /// <param name="onClose">OnClose Action.</param>
        public void Show(object content, string areaName = "", TimeSpan? expirationTime = null, Action onClick = null,
            Action onClose = null)
        {
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(
                    new Action(() => Show(content, areaName, expirationTime, onClick, onClose)));
                return;
            }

            if (expirationTime == null) expirationTime = TimeSpan.FromSeconds(5);

            if (areaName == string.Empty && _window == null)
            {
                var workArea = SystemParameters.WorkArea;

                _window = new Window
                {
                    WindowStyle = WindowStyle.None,
                    Background = Brushes.Transparent,
                    ShowInTaskbar = false,
                    AllowsTransparency = true,
                    Topmost = true,
                    ShowActivated = false,
                    Left = workArea.Left,
                    Top = workArea.Top,
                    Width = workArea.Width,
                    Height = workArea.Height,
                    Content = new ToastArea
                    {
                        Position = ToastPosition.BottomRight,
                        Margin = new Thickness(8)
                    }
                };

                _window.Show();
            }

            foreach (var area in Areas.Where(a => a.Name == areaName))
            {
                area.Show(content, (TimeSpan)expirationTime, onClick, onClose);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new area containing notification.
        /// </summary>
        /// <param name="area"></param>
        internal static void AddArea(ToastArea area)
        {
            Areas.Add(area);
        }

        #endregion

    }
}