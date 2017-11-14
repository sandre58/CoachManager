using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using My.CoachManager.Presentation.Prism.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// Represents a Modern UI styled window.
    /// </summary>
    public class MetroWindow
        : DpiAwareWindow
    {
        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(SolidColorBrush), typeof(MetroWindow));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(SolidColorBrush), typeof(MetroWindow));

        /// <summary>
        /// Identifies the IsTitleVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTitleVisibleProperty = DependencyProperty.Register("IsTitleVisible", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the IsTitleVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTitleBarVisibleProperty = DependencyProperty.Register("IsTitleBarVisible", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the IsTitleVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsMinimizeButtonVisibleProperty = DependencyProperty.Register("IsMinimizeButtonVisible", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the IsTitleVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsMaximizeButtonVisibleProperty = DependencyProperty.Register("IsMaximizeButtonVisible", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the IsTitleVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsCloseButtonVisibleProperty = DependencyProperty.Register("IsCloseButtonVisible", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the IsTitleVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTitleOverlayProperty = DependencyProperty.Register("IsTitleOverlay", typeof(bool), typeof(MetroWindow), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the IconAlignment dependency property.
        /// </summary>
        public static readonly DependencyProperty IconAlignmentProperty = DependencyProperty.Register("IconAlignment", typeof(SideAlignement), typeof(MetroWindow));

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty StatusBarProperty = DependencyProperty.Register("StatusBar", typeof(object), typeof(MetroWindow));

        private Storyboard _backgroundAnimation;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroWindow"/> class.
        /// </summary>
        public MetroWindow()
        {
            DefaultStyleKey = typeof(MetroWindow);

            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // retrieve BackgroundAnimation storyboard
            var border = GetTemplateChild("WindowBorder") as Border;
            if (border != null)
            {
                _backgroundAnimation = border.Resources["BackgroundAnimation"] as Storyboard;

                if (_backgroundAnimation != null)
                {
                    _backgroundAnimation.Begin();
                }
            }
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public SolidColorBrush HeaderBackground
        {
            get { return (SolidColorBrush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public SolidColorBrush HeaderForeground
        {
            get { return (SolidColorBrush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public object StatusBar
        {
            get { return GetValue(StatusBarProperty); }
            set { SetValue(StatusBarProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window title is visible in the UI.
        /// </summary>
        public bool IsTitleVisible
        {
            get
            {
                var value = GetValue(IsTitleVisibleProperty);
                return value != null && (bool)value;
            }
            set { SetValue(IsTitleVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window title is visible in the UI.
        /// </summary>
        public bool IsTitleBarVisible
        {
            get
            {
                var value = GetValue(IsTitleBarVisibleProperty);
                return value != null && (bool)value;
            }
            set { SetValue(IsTitleBarVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window title is visible in the UI.
        /// </summary>
        public bool IsMinimizeButtonVisible
        {
            get
            {
                var value = GetValue(IsMinimizeButtonVisibleProperty);
                return value != null && (bool)value;
            }
            set { SetValue(IsMinimizeButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window title is visible in the UI.
        /// </summary>
        public bool IsMaximizeButtonVisible
        {
            get
            {
                var value = GetValue(IsMaximizeButtonVisibleProperty);
                return value != null && (bool)value;
            }
            set { SetValue(IsMaximizeButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window title is visible in the UI.
        /// </summary>
        public bool IsCloseButtonVisible
        {
            get
            {
                var value = GetValue(IsCloseButtonVisibleProperty);
                return value != null && (bool)value;
            }
            set { SetValue(IsCloseButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window title is visible in the UI.
        /// </summary>
        public bool IsTitleOverlay
        {
            get
            {
                var value = GetValue(IsTitleOverlayProperty);
                return value != null && (bool)value;
            }
            set { SetValue(IsTitleOverlayProperty, value); }
        }

        /// <summary>
        /// Gets or sets the path data for the logo displayed in the title area of the window.
        /// </summary>
        public SideAlignement IconAlignment
        {
            get
            {
                var value = GetValue(IconAlignmentProperty);
                if (value != null) return (SideAlignement)value;
                return SideAlignement.Left;
            }
            set { SetValue(IconAlignmentProperty, value); }
        }
    }
}