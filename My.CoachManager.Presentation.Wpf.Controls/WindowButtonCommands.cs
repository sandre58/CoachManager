using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using My.CoachManager.Presentation.Wpf.Controls.Native;
using My.CoachManager.Presentation.Wpf.Controls.Windows;
using SystemCommands = My.CoachManager.Presentation.Wpf.Controls.Commands.SystemCommands;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    [TemplatePart(Name = "PART_Min", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Max", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Close", Type = typeof(Button))]
    public class WindowButtonCommands : ContentControl, INotifyPropertyChanged
    {
        public event ClosingWindowEventHandler ClosingWindow;

        public delegate void ClosingWindowEventHandler(object sender, ClosingWindowEventHandlerArgs args);

        public static readonly DependencyProperty MinimizeProperty =
            DependencyProperty.Register("Minimize", typeof(string), typeof(WindowButtonCommands),
                                        new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the minimize button tooltip.
        /// </summary>
        public string Minimize
        {
            get { return (string)GetValue(MinimizeProperty); }
            set { SetValue(MinimizeProperty, value); }
        }

        public static readonly DependencyProperty MaximizeProperty =
            DependencyProperty.Register("Maximize", typeof(string), typeof(WindowButtonCommands),
                                        new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the maximize button tooltip.
        /// </summary>
        public string Maximize
        {
            get { return (string)GetValue(MaximizeProperty); }
            set { SetValue(MaximizeProperty, value); }
        }

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.Register("Close", typeof(string), typeof(WindowButtonCommands),
                                        new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the close button tooltip.
        /// </summary>
        public string Close
        {
            get { return (string)GetValue(CloseProperty); }
            set { SetValue(CloseProperty, value); }
        }

        public static readonly DependencyProperty RestoreProperty =
            DependencyProperty.Register("Restore", typeof(string), typeof(WindowButtonCommands),
                                        new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the restore button tooltip.
        /// </summary>
        public string Restore
        {
            get { return (string)GetValue(RestoreProperty); }
            set { SetValue(RestoreProperty, value); }
        }

        public static readonly DependencyProperty ButtonsStyleProperty =
            DependencyProperty.Register("ButtonsStyle", typeof(Style), typeof(WindowButtonCommands),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the close button tooltip.
        /// </summary>
        public Style ButtonsStyle
        {
            get { return (Style)GetValue(ButtonsStyleProperty); }
            set { SetValue(ButtonsStyleProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonStyleProperty =
            DependencyProperty.Register("CloseButtonStyle", typeof(Style), typeof(WindowButtonCommands),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the close button tooltip.
        /// </summary>
        public Style CloseButtonStyle
        {
            get { return (Style)GetValue(CloseButtonStyleProperty); }
            set { SetValue(CloseButtonStyleProperty, value); }
        }

        private Button _min;
        private Button _max;
        private Button _close;
        private SafeLibraryHandle _user32;

        static WindowButtonCommands()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowButtonCommands), new FrameworkPropertyMetadata(typeof(WindowButtonCommands)));
        }

        public WindowButtonCommands()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                                        new Action(() =>
                                        {
                                            if (string.IsNullOrWhiteSpace(Minimize))
                                            {
                                                Minimize = GetCaption(900);
                                            }
                                            if (string.IsNullOrWhiteSpace(Maximize))
                                            {
                                                Maximize = GetCaption(901);
                                            }
                                            if (string.IsNullOrWhiteSpace(Close))
                                            {
                                                Close = GetCaption(905);
                                            }
                                            if (string.IsNullOrWhiteSpace(Restore))
                                            {
                                                Restore = GetCaption(903);
                                            }
                                        }));
        }

        private string GetCaption(int id)
        {
            if (_user32 == null)
            {
                _user32 = NativeMethods.LoadLibrary(Environment.SystemDirectory + "\\User32.dll");
            }

            var sb = new StringBuilder(256);
            NativeMethods.LoadString(_user32, (uint)id, sb, sb.Capacity);
            return sb.ToString().Replace("&", "");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _close = Template.FindName("PART_Close", this) as Button;
            if (_close != null)
            {
                _close.Click += CloseClick;
            }

            _max = Template.FindName("PART_Max", this) as Button;
            if (_max != null)
            {
                _max.Click += MaximizeClick;
            }

            _min = Template.FindName("PART_Min", this) as Button;
            if (_min != null)
            {
                _min.Click += MinimizeClick;
            }
        }

        protected void OnClosingWindow(ClosingWindowEventHandlerArgs args)
        {
            var handler = ClosingWindow;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            if (null == ParentWindow) return;
            SystemCommands.MinimizeWindow(ParentWindow);
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            if (null == ParentWindow) return;
            if (ParentWindow.WindowState == WindowState.Maximized)
            {
                SystemCommands.RestoreWindow(ParentWindow);
            }
            else
            {
                SystemCommands.MaximizeWindow(ParentWindow);
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            var closingWindowEventHandlerArgs = new ClosingWindowEventHandlerArgs();
            OnClosingWindow(closingWindowEventHandlerArgs);

            if (closingWindowEventHandlerArgs.Cancelled)
            {
                return;
            }

            if (null == ParentWindow) return;
            ParentWindow.Close();
        }

        private ExtendedWindow _parentWindow;

        public ExtendedWindow ParentWindow
        {
            get { return _parentWindow; }
            set
            {
                if (Equals(_parentWindow, value))
                {
                    return;
                }
                _parentWindow = value;
                RaisePropertyChanged("ParentWindow");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}