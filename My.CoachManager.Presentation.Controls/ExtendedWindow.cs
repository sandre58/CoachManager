using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using My.CoachManager.Presentation.Controls.Flyouts;
using My.CoachManager.Presentation.Controls.Helpers;
using My.CoachManager.Presentation.Controls.Native;
using My.CoachManager.Presentation.Controls.Windows;

namespace My.CoachManager.Presentation.Controls
{
    /// <summary>
    /// Represents a Modern UI styled window.
    /// </summary>
    [TemplatePart(Name = PartIcon, Type = typeof(UIElement))]
    [TemplatePart(Name = PartTitleBar, Type = typeof(UIElement))]
    [TemplatePart(Name = PartWindowTitleBackground, Type = typeof(UIElement))]
    [TemplatePart(Name = PartWindowTitleThumb, Type = typeof(ExtendedThumb))]
    [TemplatePart(Name = PartFlyoutModalDragMoveThumb, Type = typeof(ExtendedThumb))]
    [TemplatePart(Name = PartLeftWindowCommands, Type = typeof(WindowCommands))]
    [TemplatePart(Name = PartRightWindowCommands, Type = typeof(WindowCommands))]
    [TemplatePart(Name = PartWindowButtonCommands, Type = typeof(WindowButtonCommands))]
    [TemplatePart(Name = PartOverlayBox, Type = typeof(Grid))]
    [TemplatePart(Name = PartFlyoutModal, Type = typeof(Rectangle))]
    public class ExtendedWindow
        : DpiAwareWindow
    {
        private const string PartIcon = "PART_Icon";
        private const string PartTitleBar = "PART_TitleBar";
        private const string PartWindowTitleBackground = "PART_WindowTitleBackground";
        private const string PartWindowTitleThumb = "PART_WindowTitleThumb";
        private const string PartFlyoutModalDragMoveThumb = "PART_FlyoutModalDragMoveThumb";
        private const string PartLeftWindowCommands = "PART_LeftWindowCommands";
        private const string PartRightWindowCommands = "PART_RightWindowCommands";
        private const string PartWindowButtonCommands = "PART_WindowButtonCommands";
        private const string PartOverlayBox = "PART_OverlayBox";
        private const string PartFlyoutModal = "PART_FlyoutModal";

        private FrameworkElement _icon;
        private UIElement _titleBar;
        private UIElement _titleBarBackground;
        private ExtendedThumb _windowTitleThumb;
        private ExtendedThumb _flyoutModalDragMoveThumb;
        private IInputElement _restoreFocus;
        internal ContentPresenter LeftWindowCommandsPresenter;
        internal ContentPresenter RightWindowCommandsPresenter;
        internal ContentPresenter WindowButtonCommandsPresenter;

        internal Grid OverlayBox;
        private Rectangle _flyoutModal;
        private Storyboard _overlayStoryboard;

        public static readonly RoutedEvent FlyoutsStatusChangedEvent = EventManager.RegisterRoutedEvent(
            "FlyoutsStatusChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ExtendedWindow));

        // Provide CLR accessors for the event
        public event RoutedEventHandler FlyoutsStatusChanged
        {
            add { AddHandler(FlyoutsStatusChangedEvent, value); }
            remove { RemoveHandler(FlyoutsStatusChangedEvent, value); }
        }

        public static readonly DependencyProperty ShowSystemMenuOnRightClickProperty = DependencyProperty.Register("ShowSystemMenuOnRightClick", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowIconOnTitleBarProperty = DependencyProperty.Register("ShowIconOnTitleBar", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true, OnShowIconOnTitleBarPropertyChangedCallback));
        public static readonly DependencyProperty ShowTitleOnTitleBarProperty = DependencyProperty.Register("ShowTitleOnTitleBar", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true, OnShowTitleOnTitleBarPropertyChangedCallback));
        public static readonly DependencyProperty ShowTitleBarProperty = DependencyProperty.Register("ShowTitleBar", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true, OnShowTitleBarPropertyChangedCallback, OnShowTitleBarCoerceValueCallback));
        public static readonly DependencyProperty IsTitleOverlayProperty = DependencyProperty.Register("IsTitleOverlay", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowMinButtonProperty = DependencyProperty.Register("ShowMinButton", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowMaxRestoreButtonProperty = DependencyProperty.Register("ShowMaxRestoreButton", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty IsMinButtonEnabledProperty = DependencyProperty.Register("IsMinButtonEnabled", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));
        public static readonly DependencyProperty IsMaxRestoreButtonEnabledProperty = DependencyProperty.Register("IsMaxRestoreButtonEnabled", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));
        public static readonly DependencyProperty IsCloseButtonEnabledProperty = DependencyProperty.Register("IsCloseButtonEnabled", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register("TitlebarHeight", typeof(int), typeof(ExtendedWindow), new PropertyMetadata(30, TitlebarHeightPropertyChangedCallback));
        public static readonly DependencyProperty TitleCharacterCasingProperty = DependencyProperty.Register("TitleCharacterCasing", typeof(CharacterCasing), typeof(ExtendedWindow), new FrameworkPropertyMetadata(CharacterCasing.Upper, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure), value => CharacterCasing.Normal <= (CharacterCasing)value && (CharacterCasing)value <= CharacterCasing.Upper);
        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register("TitleAlignment", typeof(HorizontalAlignment), typeof(ExtendedWindow), new PropertyMetadata(HorizontalAlignment.Stretch, PropertyChangedCallback));

        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(ExtendedWindow));
        public static readonly DependencyProperty IgnoreTaskbarOnMaximizeProperty = DependencyProperty.Register("IgnoreTaskbarOnMaximize", typeof(bool), typeof(ExtendedWindow), new PropertyMetadata(false));
        public static readonly DependencyProperty FlyoutsProperty = DependencyProperty.Register("Flyouts", typeof(FlyoutsControl), typeof(ExtendedWindow), new PropertyMetadata(null, UpdateLogicalChilds));

        public static readonly DependencyProperty WindowTitleBrushProperty = DependencyProperty.Register("WindowTitleBrush", typeof(Brush), typeof(ExtendedWindow), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty NonActiveWindowTitleBrushProperty = DependencyProperty.Register("NonActiveWindowTitleBrush", typeof(Brush), typeof(ExtendedWindow), new PropertyMetadata(Brushes.Gray));
        public static readonly DependencyProperty NonActiveBorderBrushProperty = DependencyProperty.Register("NonActiveBorderBrush", typeof(Brush), typeof(ExtendedWindow), new PropertyMetadata(Brushes.Gray));

        public static readonly DependencyProperty OverlayBrushProperty = DependencyProperty.Register("OverlayBrush", typeof(Brush), typeof(ExtendedWindow), new PropertyMetadata(new SolidColorBrush(Color.FromScRgb(255, 0, 0, 0)))); // BlackColorBrush
        public static readonly DependencyProperty OverlayOpacityProperty = DependencyProperty.Register("OverlayOpacity", typeof(double), typeof(ExtendedWindow), new PropertyMetadata(0.7d));

        public static readonly DependencyProperty IconTemplateProperty = DependencyProperty.Register("IconTemplate", typeof(DataTemplate), typeof(ExtendedWindow), new PropertyMetadata(null));
        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(ExtendedWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty LeftWindowCommandsProperty = DependencyProperty.Register("LeftWindowCommands", typeof(WindowCommands), typeof(ExtendedWindow), new PropertyMetadata(null, UpdateLogicalChilds));
        public static readonly DependencyProperty RightWindowCommandsProperty = DependencyProperty.Register("RightWindowCommands", typeof(WindowCommands), typeof(ExtendedWindow), new PropertyMetadata(null, UpdateLogicalChilds));
        public static readonly DependencyProperty WindowButtonCommandsProperty = DependencyProperty.Register("WindowButtonCommands", typeof(WindowButtonCommands), typeof(ExtendedWindow), new PropertyMetadata(null, UpdateLogicalChilds));

        public static readonly DependencyProperty LeftWindowCommandsOverlayBehaviorProperty = DependencyProperty.Register("LeftWindowCommandsOverlayBehavior", typeof(WindowCommandsOverlayBehavior), typeof(ExtendedWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Always));
        public static readonly DependencyProperty RightWindowCommandsOverlayBehaviorProperty = DependencyProperty.Register("RightWindowCommandsOverlayBehavior", typeof(WindowCommandsOverlayBehavior), typeof(ExtendedWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Always));
        public static readonly DependencyProperty WindowButtonCommandsOverlayBehaviorProperty = DependencyProperty.Register("WindowButtonCommandsOverlayBehavior", typeof(WindowCommandsOverlayBehavior), typeof(ExtendedWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Always));
        public static readonly DependencyProperty IconOverlayBehaviorProperty = DependencyProperty.Register("IconOverlayBehavior", typeof(WindowCommandsOverlayBehavior), typeof(ExtendedWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Never));
        public static readonly DependencyProperty StatusBarProperty = DependencyProperty.Register("StatusBar", typeof(object), typeof(ExtendedWindow));

        static ExtendedWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedWindow), new FrameworkPropertyMetadata(typeof(ExtendedWindow)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedWindow"/> class.
        /// </summary>
        public ExtendedWindow()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedWindow), new FrameworkPropertyMetadata(typeof(ExtendedWindow)));

            DataContextChanged += ExtendedWindow_DataContextChanged;
            Loaded += ExtendedWindow_Loaded;
        }

        /// <summary>
        /// Gets/sets if the the system menu should popup on right click.
        /// </summary>
        public bool ShowSystemMenuOnRightClick
        {
            get { return (bool)GetValue(ShowSystemMenuOnRightClickProperty); }
            set { SetValue(ShowSystemMenuOnRightClickProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public object StatusBar
        {
            get { return GetValue(StatusBarProperty); }
            set { SetValue(StatusBarProperty, value); }
        }

        public WindowCommandsOverlayBehavior LeftWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(LeftWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(LeftWindowCommandsOverlayBehaviorProperty, value); }
        }

        public WindowCommandsOverlayBehavior RightWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(RightWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(RightWindowCommandsOverlayBehaviorProperty, value); }
        }

        public WindowCommandsOverlayBehavior WindowButtonCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(WindowButtonCommandsOverlayBehaviorProperty); }
            set { SetValue(WindowButtonCommandsOverlayBehaviorProperty, value); }
        }

        public WindowCommandsOverlayBehavior IconOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior)GetValue(IconOverlayBehaviorProperty); }
            set { SetValue(IconOverlayBehaviorProperty, value); }
        }

        /// <summary>
        /// Gets/sets the FlyoutsControl that hosts the window's flyouts.
        /// </summary>
        public FlyoutsControl Flyouts
        {
            get { return (FlyoutsControl)GetValue(FlyoutsProperty); }
            set { SetValue(FlyoutsProperty, value); }
        }

        /// <summary>
        /// Gets/sets the icon content template to show a custom icon.
        /// </summary>
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        /// <summary>
        /// Gets/sets the title content template to show a custom title.
        /// </summary>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        /// <summary>
        /// Gets/sets the left window commands that hosts the user commands.
        /// </summary>
        public WindowCommands LeftWindowCommands
        {
            get { return (WindowCommands)GetValue(LeftWindowCommandsProperty); }
            set { SetValue(LeftWindowCommandsProperty, value); }
        }

        /// <summary>
        /// Gets/sets the right window commands that hosts the user commands.
        /// </summary>
        public WindowCommands RightWindowCommands
        {
            get { return (WindowCommands)GetValue(RightWindowCommandsProperty); }
            set { SetValue(RightWindowCommandsProperty, value); }
        }

        /// <summary>
        /// Gets/sets the window button commands that hosts the min/max/close commands.
        /// </summary>
        public WindowButtonCommands WindowButtonCommands
        {
            get { return (WindowButtonCommands)GetValue(WindowButtonCommandsProperty); }
            set { SetValue(WindowButtonCommandsProperty, value); }
        }

        /// <summary>
        /// Gets/sets whether the window will ignore (and overlap) the taskbar when maximized.
        /// </summary>
        public bool IgnoreTaskbarOnMaximize
        {
            get { return (bool)GetValue(IgnoreTaskbarOnMaximizeProperty); }
            set { SetValue(IgnoreTaskbarOnMaximizeProperty, value); }
        }

        /// <summary>
        /// Gets/sets the brush used for the titlebar's foreground.
        /// </summary>
        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        public bool IsTitleOverlay
        {
            get { return (bool)GetValue(IsTitleOverlayProperty); }
            set { SetValue(IsTitleOverlayProperty, value); }
        }

        /// <summary>
        /// Get/sets whether the titlebar icon is visible or not.
        /// </summary>
        public bool ShowIconOnTitleBar
        {
            get { return (bool)GetValue(ShowIconOnTitleBarProperty); }
            set { SetValue(ShowIconOnTitleBarProperty, value); }
        }

        /// <summary>
        /// Get/sets whether the titlebar title is visible or not.
        /// </summary>
        public bool ShowTitleOnTitleBar
        {
            get { return (bool)GetValue(ShowTitleOnTitleBarProperty); }
            set { SetValue(ShowTitleOnTitleBarProperty, value); }
        }

        private static void OnShowIconOnTitleBarPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (ExtendedWindow)d;
            if (e.NewValue != e.OldValue)
            {
                window.SetVisibiltyForIcon();
            }
        }

        private static void OnShowTitleOnTitleBarPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (ExtendedWindow)d;
            if (e.NewValue != e.OldValue)
            {
                window.SetVisibiltyForTitle();
            }
        }

        /// <summary>
        /// Gets/sets whether the TitleBar is visible or not.
        /// </summary>
        public bool ShowTitleBar
        {
            get { return (bool)GetValue(ShowTitleBarProperty); }
            set { SetValue(ShowTitleBarProperty, value); }
        }

        private static void OnShowTitleBarPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (ExtendedWindow)d;
            if (e.NewValue != e.OldValue)
            {
                window.SetVisibiltyForAllTitleElements((bool)e.NewValue);
            }
        }

        private static object OnShowTitleBarCoerceValueCallback(DependencyObject d, object value)
        {
            return value;
        }

        /// <summary>
        /// Gets/sets if the minimize button is visible.
        /// </summary>
        public bool ShowMinButton
        {
            get { return (bool)GetValue(ShowMinButtonProperty); }
            set { SetValue(ShowMinButtonProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the Maximize/Restore button is visible.
        /// </summary>
        public bool ShowMaxRestoreButton
        {
            get { return (bool)GetValue(ShowMaxRestoreButtonProperty); }
            set { SetValue(ShowMaxRestoreButtonProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the close button is visible.
        /// </summary>
        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the min button is enabled.
        /// </summary>
        public bool IsMinButtonEnabled
        {
            get { return (bool)GetValue(IsMinButtonEnabledProperty); }
            set { SetValue(IsMinButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the max/restore button is enabled.
        /// </summary>
        public bool IsMaxRestoreButtonEnabled
        {
            get { return (bool)GetValue(IsMaxRestoreButtonEnabledProperty); }
            set { SetValue(IsMaxRestoreButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the close button is enabled.
        /// </summary>
        public bool IsCloseButtonEnabled
        {
            get { return (bool)GetValue(IsCloseButtonEnabledProperty); }
            set { SetValue(IsCloseButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets/sets the TitleBar's height.
        /// </summary>
        public int TitlebarHeight
        {
            get { return (int)GetValue(TitlebarHeightProperty); }
            set { SetValue(TitlebarHeightProperty, value); }
        }

        private static void TitlebarHeightPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var window = (ExtendedWindow)dependencyObject;
            if (e.NewValue != e.OldValue)
            {
                window.SetVisibiltyForAllTitleElements((int)e.NewValue > 0);
            }
        }

        private void SetVisibiltyForIcon()
        {
            if (_icon != null)
            {
                var isVisible = (IconOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar) && !ShowTitleBar)
                                || (ShowIconOnTitleBar && ShowTitleBar);
                var iconVisibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
                _icon.Visibility = iconVisibility;
            }
        }

        private void SetVisibiltyForTitle()
        {
            if (_titleBar != null)
            {
                var isVisible = ShowTitleOnTitleBar && ShowTitleBar;
                var titleVisibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
                _titleBar.Visibility = titleVisibility;
            }
        }

        private void SetVisibiltyForAllTitleElements(bool visible)
        {
            SetVisibiltyForTitle();
            SetVisibiltyForIcon();
            var newVisibility = visible && ShowTitleBar ? Visibility.Visible : Visibility.Collapsed;
            if (_titleBarBackground != null)
            {
                _titleBarBackground.Visibility = newVisibility;
            }
            if (LeftWindowCommandsPresenter != null)
            {
                LeftWindowCommandsPresenter.Visibility = LeftWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar) ?
                    Visibility.Visible : newVisibility;
            }
            if (RightWindowCommandsPresenter != null)
            {
                RightWindowCommandsPresenter.Visibility = RightWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar) ?
                    Visibility.Visible : newVisibility;
            }
            if (WindowButtonCommandsPresenter != null)
            {
                WindowButtonCommandsPresenter.Visibility = WindowButtonCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar) ?
                    Visibility.Visible : newVisibility;
            }

            SetWindowEvents();
        }

        /// <summary>
        /// Character casing of the title
        /// </summary>
        public CharacterCasing TitleCharacterCasing
        {
            get { return (CharacterCasing)GetValue(TitleCharacterCasingProperty); }
            set { SetValue(TitleCharacterCasingProperty, value); }
        }

        /// <summary>
        /// Gets/sets the title horizontal alignment.
        /// </summary>
        public HorizontalAlignment TitleAlignment
        {
            get { return (HorizontalAlignment)GetValue(TitleAlignmentProperty); }
            set { SetValue(TitleAlignmentProperty, value); }
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var window = dependencyObject as ExtendedWindow;
            if (window != null)
            {
                window.SizeChanged -= window.ExtendedWindow_SizeChanged;
                if (e.NewValue is HorizontalAlignment && (HorizontalAlignment)e.NewValue == HorizontalAlignment.Center)
                {
                    window.SizeChanged += window.ExtendedWindow_SizeChanged;
                }
            }
        }

        /// <summary>
        /// Gets/sets the brush used for the Window's title bar.
        /// </summary>
        public Brush WindowTitleBrush
        {
            get { return (Brush)GetValue(WindowTitleBrushProperty); }
            set { SetValue(WindowTitleBrushProperty, value); }
        }

        /// <summary>
        /// Gets/sets the brush used for the Window's non-active border.
        /// </summary>
        public Brush NonActiveBorderBrush
        {
            get { return (Brush)GetValue(NonActiveBorderBrushProperty); }
            set { SetValue(NonActiveBorderBrushProperty, value); }
        }

        /// <summary>
        /// Gets/sets the brush used for the Window's non-active title bar.
        /// </summary>
        public Brush NonActiveWindowTitleBrush
        {
            get { return (Brush)GetValue(NonActiveWindowTitleBrushProperty); }
            set { SetValue(NonActiveWindowTitleBrushProperty, value); }
        }

        /// <summary>
        /// Gets/sets the brush used for the dialog overlay.
        /// </summary>
        public Brush OverlayBrush
        {
            get { return (Brush)GetValue(OverlayBrushProperty); }
            set { SetValue(OverlayBrushProperty, value); }
        }

        /// <summary>
        /// Gets/sets the opacity used for the dialog overlay.
        /// </summary>
        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }

        /// <summary>
        /// Begins to show the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        public System.Threading.Tasks.Task ShowOverlayAsync()
        {
            if (OverlayBox == null) throw new InvalidOperationException("OverlayBox can not be founded in this MetroWindow's template. Are you calling this before the window has loaded?");

            var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();

            if (IsOverlayVisible() && _overlayStoryboard == null)
            {
                //No Task.FromResult in .NET 4.
                tcs.SetResult(null);
                return tcs.Task;
            }

            Dispatcher.VerifyAccess();

            OverlayBox.Visibility = Visibility.Visible;

            var sb = ((Storyboard)Template.Resources["OverlayFastSemiFadeIn"]).Clone();
            ((DoubleAnimation)sb.Children[0]).To = OverlayOpacity;

            EventHandler completionHandler = null;
            completionHandler = (sender, args) =>
            {
                sb.Completed -= completionHandler;

                if (Equals(_overlayStoryboard, sb))
                {
                    _overlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;

            OverlayBox.BeginStoryboard(sb);

            _overlayStoryboard = sb;

            return tcs.Task;
        }

        /// <summary>
        /// Begins to hide the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        public System.Threading.Tasks.Task HideOverlayAsync()
        {
            if (OverlayBox == null) throw new InvalidOperationException("OverlayBox can not be founded in this MetroWindow's template. Are you calling this before the window has loaded?");

            var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (OverlayBox.Visibility == Visibility.Visible && OverlayBox.Opacity == 0.0)
            {
                //No Task.FromResult in .NET 4.
                tcs.SetResult(null);
                return tcs.Task;
            }

            Dispatcher.VerifyAccess();

            var sb = ((Storyboard)Template.Resources["OverlayFastSemiFadeOut"]).Clone();
            ((DoubleAnimation)sb.Children[0]).To = 0d;

            EventHandler completionHandler = null;
            completionHandler = (sender, args) =>
            {
                sb.Completed -= completionHandler;

                if (Equals(_overlayStoryboard, sb))
                {
                    OverlayBox.Visibility = Visibility.Hidden;
                    _overlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;

            OverlayBox.BeginStoryboard(sb);

            _overlayStoryboard = sb;

            return tcs.Task;
        }

        public bool IsOverlayVisible()
        {
            if (OverlayBox == null) throw new InvalidOperationException("OverlayBox can not be founded in this MetroWindow's template. Are you calling this before the window has loaded?");

            return OverlayBox.Visibility == Visibility.Visible && OverlayBox.Opacity >= OverlayOpacity;
        }

        public void ShowOverlay()
        {
            OverlayBox.Visibility = Visibility.Visible;
            OverlayBox.SetCurrentValue(OpacityProperty, OverlayOpacity);
        }

        public void HideOverlay()
        {
            OverlayBox.SetCurrentValue(OpacityProperty, 0.0);
            OverlayBox.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Stores the given element, or the last focused element via FocusManager, for restoring the focus after closing a dialog.
        /// </summary>
        /// <param name="thisElement">The element which will be focused again.</param>
        public void StoreFocus(IInputElement thisElement = null)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _restoreFocus = thisElement ?? (_restoreFocus ?? FocusManager.GetFocusedElement(this));
            }));
        }

        internal void RestoreFocus()
        {
            if (_restoreFocus != null)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Keyboard.Focus(_restoreFocus);
                    _restoreFocus = null;
                }));
            }
        }

        /// <summary>
        /// Clears the stored element which would get the focus after closing a dialog.
        /// </summary>
        public void ResetStoredFocus()
        {
            _restoreFocus = null;
        }

        private void ExtendedWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // MahApps add these controls to the window with AddLogicalChild method.
            // This has the side effect that the DataContext doesn't update, so do this now here.
            if (LeftWindowCommands != null) LeftWindowCommands.DataContext = DataContext;
            if (RightWindowCommands != null) RightWindowCommands.DataContext = DataContext;
            if (WindowButtonCommands != null) WindowButtonCommands.DataContext = DataContext;
            if (Flyouts != null) Flyouts.DataContext = DataContext;
        }

        private void ExtendedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Flyouts == null)
            {
                Flyouts = new FlyoutsControl();
            }
        }

        private void ExtendedWindow_SizeChanged(object sender, RoutedEventArgs e)
        {
            // this all works only for centered title
            if (TitleAlignment != HorizontalAlignment.Center)
            {
                return;
            }

            // Half of this MetroWindow
            var halfDistance = ActualWidth / 2;
            // Distance between center and left/right
            var distanceToCenter = _titleBar.DesiredSize.Width / 2;
            // Distance between right edge from LeftWindowCommands to left window side
            var distanceFromLeft = _icon.ActualWidth + LeftWindowCommands.ActualWidth;
            // Distance between left edge from RightWindowCommands to right window side
            var distanceFromRight = WindowButtonCommands.ActualWidth + RightWindowCommands.ActualWidth;
            // Margin
            const double horizontalMargin = 5.0;

            var dLeft = distanceFromLeft + distanceToCenter + horizontalMargin;
            var dRight = distanceFromRight + distanceToCenter + horizontalMargin;
            if ((dLeft < halfDistance) && (dRight < halfDistance))
            {
                Grid.SetColumn(_titleBar, 0);
                Grid.SetColumnSpan(_titleBar, 5);
            }
            else
            {
                Grid.SetColumn(_titleBar, 2);
                Grid.SetColumnSpan(_titleBar, 1);
            }
        }

        private void FlyoutsPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (e.OriginalSource as DependencyObject);
            if (element != null)
            {
                // no preview if we just clicked these elements
                if (element.TryFindParent<Flyout>() != null
                    || Equals(element, OverlayBox)
                    || Equals(element.TryFindParent<ContentControl>(), _icon)
                    || element.TryFindParent<WindowCommands>() != null
                    || element.TryFindParent<WindowButtonCommands>() != null)
                {
                    return;
                }
            }

            if (Flyouts.OverrideExternalCloseButton == null)
            {
                foreach (var flyout in Flyouts.GetFlyouts().Where(x => x.IsOpen && x.ExternalCloseButton == e.ChangedButton && (!x.IsPinned || Flyouts.OverrideIsPinned)))
                {
                    flyout.IsOpen = false;
                }
            }
            else if (Flyouts.OverrideExternalCloseButton == e.ChangedButton)
            {
                foreach (var flyout in Flyouts.GetFlyouts().Where(x => x.IsOpen && (!x.IsPinned || Flyouts.OverrideIsPinned)))
                {
                    flyout.IsOpen = false;
                }
            }
        }

        private static void UpdateLogicalChilds(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var window = dependencyObject as ExtendedWindow;
            if (window == null)
            {
                return;
            }
            var oldChild = e.OldValue as FrameworkElement;
            if (oldChild != null)
            {
                window.RemoveLogicalChild(oldChild);
            }
            var newChild = e.NewValue as FrameworkElement;
            if (newChild != null)
            {
                window.AddLogicalChild(newChild);
                // Yes, that's crazy. But we must do this to enable all possible scenarios for setting DataContext
                // in a Window. Without set the DataContext at this point it can happen that e.g. a Flyout
                // doesn't get the same DataContext.
                // So now we can type
                //
                // this.InitializeComponent();
                // this.DataContext = new MainViewModel();
                //
                // or
                //
                // this.DataContext = new MainViewModel();
                // this.InitializeComponent();
                //
                newChild.DataContext = window.DataContext;
            }
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                // cheat, make a list with all logical content and return the enumerator
                ArrayList children = new ArrayList { Content };
                if (LeftWindowCommands != null)
                {
                    children.Add(LeftWindowCommands);
                }
                if (RightWindowCommands != null)
                {
                    children.Add(RightWindowCommands);
                }
                if (WindowButtonCommands != null)
                {
                    children.Add(WindowButtonCommands);
                }
                if (Flyouts != null)
                {
                    children.Add(Flyouts);
                }
                return children.GetEnumerator();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            LeftWindowCommandsPresenter = GetTemplateChild(PartLeftWindowCommands) as ContentPresenter;
            RightWindowCommandsPresenter = GetTemplateChild(PartRightWindowCommands) as ContentPresenter;
            WindowButtonCommandsPresenter = GetTemplateChild(PartWindowButtonCommands) as ContentPresenter;

            if (LeftWindowCommands == null)
                LeftWindowCommands = new WindowCommands();
            if (RightWindowCommands == null)
                RightWindowCommands = new WindowCommands();
            if (WindowButtonCommands == null)
                WindowButtonCommands = new WindowButtonCommands();

            LeftWindowCommands.ParentWindow = this;
            RightWindowCommands.ParentWindow = this;
            WindowButtonCommands.ParentWindow = this;

            OverlayBox = GetTemplateChild(PartOverlayBox) as Grid;
            _flyoutModal = (Rectangle)GetTemplateChild(PartFlyoutModal);
            if (_flyoutModal != null) _flyoutModal.PreviewMouseDown += FlyoutsPreviewMouseDown;
            PreviewMouseDown += FlyoutsPreviewMouseDown;

            _icon = GetTemplateChild(PartIcon) as FrameworkElement;
            _titleBar = GetTemplateChild(PartTitleBar) as UIElement;
            _titleBarBackground = GetTemplateChild(PartWindowTitleBackground) as UIElement;
            _windowTitleThumb = GetTemplateChild(PartWindowTitleThumb) as ExtendedThumb;
            _flyoutModalDragMoveThumb = GetTemplateChild(PartFlyoutModalDragMoveThumb) as ExtendedThumb;

            SetVisibiltyForAllTitleElements(TitlebarHeight > 0);
        }

        private void ClearWindowEvents()
        {
            // clear all event handlers first:
            if (_windowTitleThumb != null)
            {
                _windowTitleThumb.PreviewMouseLeftButtonUp -= WindowTitleThumbOnPreviewMouseLeftButtonUp;
                _windowTitleThumb.DragDelta -= WindowTitleThumbMoveOnDragDelta;
                _windowTitleThumb.MouseDoubleClick -= WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _windowTitleThumb.MouseRightButtonUp -= WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            var thumbContentControl = _titleBar as IThumb;
            if (thumbContentControl != null)
            {
                thumbContentControl.PreviewMouseLeftButtonUp -= WindowTitleThumbOnPreviewMouseLeftButtonUp;
                thumbContentControl.DragDelta -= WindowTitleThumbMoveOnDragDelta;
                thumbContentControl.MouseDoubleClick -= WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                thumbContentControl.MouseRightButtonUp -= WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            if (_flyoutModalDragMoveThumb != null)
            {
                _flyoutModalDragMoveThumb.PreviewMouseLeftButtonUp -= WindowTitleThumbOnPreviewMouseLeftButtonUp;
                _flyoutModalDragMoveThumb.DragDelta -= WindowTitleThumbMoveOnDragDelta;
                _flyoutModalDragMoveThumb.MouseDoubleClick -= WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _flyoutModalDragMoveThumb.MouseRightButtonUp -= WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            if (_icon != null)
            {
                _icon.MouseDown -= IconMouseDown;
            }
            SizeChanged -= ExtendedWindow_SizeChanged;
        }

        private void SetWindowEvents()
        {
            // clear all event handlers first
            ClearWindowEvents();

            // set mouse down/up for icon
            if (_icon != null && _icon.Visibility == Visibility.Visible)
            {
                _icon.MouseDown += IconMouseDown;
            }

            if (_windowTitleThumb != null)
            {
                _windowTitleThumb.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                _windowTitleThumb.DragDelta += WindowTitleThumbMoveOnDragDelta;
                _windowTitleThumb.MouseDoubleClick += WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _windowTitleThumb.MouseRightButtonUp += WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            var thumbContentControl = _titleBar as IThumb;
            if (thumbContentControl != null)
            {
                thumbContentControl.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                thumbContentControl.DragDelta += WindowTitleThumbMoveOnDragDelta;
                thumbContentControl.MouseDoubleClick += WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                thumbContentControl.MouseRightButtonUp += WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            if (_flyoutModalDragMoveThumb != null)
            {
                _flyoutModalDragMoveThumb.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                _flyoutModalDragMoveThumb.DragDelta += WindowTitleThumbMoveOnDragDelta;
                _flyoutModalDragMoveThumb.MouseDoubleClick += WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _flyoutModalDragMoveThumb.MouseRightButtonUp += WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }

            // handle size if we have a Grid for the title (e.g. clean window have a centered title)
            //if (titleBar != null && titleBar.GetType() == typeof(Grid))
            if (_titleBar != null && TitleAlignment == HorizontalAlignment.Center)
            {
                SizeChanged += ExtendedWindow_SizeChanged;
            }
        }

        private void IconMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    Close();
                }
                else
                {
                    ShowSystemMenuPhysicalCoordinates(this, PointToScreen(new Point(0, TitlebarHeight)));
                }
            }
        }

        private void WindowTitleThumbOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoWindowTitleThumbOnPreviewMouseLeftButtonUp(this, e);
        }

        private void WindowTitleThumbMoveOnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            DoWindowTitleThumbMoveOnDragDelta(sender as IThumb, this, dragDeltaEventArgs);
        }

        private void WindowTitleThumbChangeWindowStateOnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(this, mouseButtonEventArgs);
        }

        private void WindowTitleThumbSystemMenuOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(this, e);
        }

        internal static void DoWindowTitleThumbOnPreviewMouseLeftButtonUp(ExtendedWindow window, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.Source == mouseButtonEventArgs.OriginalSource)
            {
                Mouse.Capture(null);
            }
        }

        internal static void DoWindowTitleThumbMoveOnDragDelta(IThumb thumb, ExtendedWindow window, DragDeltaEventArgs dragDeltaEventArgs)
        {
            if (thumb == null)
            {
                throw new ArgumentNullException(nameof(thumb));
            }
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            // drag only if IsWindowDraggable is set to true
            if (
                !(Math.Abs(dragDeltaEventArgs.HorizontalChange) > 2) && !(Math.Abs(dragDeltaEventArgs.VerticalChange) > 2))
            {
                return;
            }

            // tage from DragMove internal code
            window.VerifyAccess();

            //var cursorPos = WinApiHelper.GetPhysicalCursorPos();

            // if the window is maximized dragging is only allowed on title bar (also if not visible)
            var windowIsMaximized = window.WindowState == WindowState.Maximized;
            var isMouseOnTitlebar = Mouse.GetPosition(thumb).Y <= window.TitlebarHeight && window.TitlebarHeight > 0;
            if (!isMouseOnTitlebar && windowIsMaximized)
            {
                return;
            }

            // for the touch usage
            UnsafeNativeMethods.ReleaseCapture();

            if (windowIsMaximized)
            {
                //var cursorXPos = cursorPos.x;
                EventHandler windowOnStateChanged = null;
                windowOnStateChanged = (sender, args) =>
                {
                    //window.Top = 2;
                    //window.Left = Math.Max(cursorXPos - window.RestoreBounds.Width / 2, 0);

                    window.StateChanged -= windowOnStateChanged;
                    if (window.WindowState == WindowState.Normal)
                    {
                        Mouse.Capture(thumb, CaptureMode.Element);
                    }
                };
                window.StateChanged += windowOnStateChanged;
            }

            //var criticalHandle = window.CriticalHandle;
            // DragMove works too
            window.DragMove();
            // instead this 2 lines
            //NativeMethods.SendMessage(criticalHandle, NativeMethods.WM.SYSCOMMAND, (IntPtr)NativeMethods.SC.MOUSEMOVE, IntPtr.Zero);
            //NativeMethods.SendMessage(criticalHandle, NativeMethods.WM.LBUTTONUP, IntPtr.Zero, IntPtr.Zero);
        }

        internal static void DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(ExtendedWindow window, MouseButtonEventArgs mouseButtonEventArgs)
        {
            // restore/maximize only with left button
            if (mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                // we can maximize or restore the window if the title bar height is set (also if title bar is hidden)
                var canResize = window.ResizeMode == ResizeMode.CanResizeWithGrip || window.ResizeMode == ResizeMode.CanResize;
                var mousePos = Mouse.GetPosition(window);
                var isMouseOnTitlebar = mousePos.Y <= window.TitlebarHeight && window.TitlebarHeight > 0;
                if (canResize && isMouseOnTitlebar)
                {
                    if (window.WindowState == WindowState.Normal)
                    {
                        SystemCommands.MaximizeWindow(window);
                    }
                    else
                    {
                        SystemCommands.RestoreWindow(window);
                    }
                    mouseButtonEventArgs.Handled = true;
                }
            }
        }

        internal static void DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(ExtendedWindow window, MouseButtonEventArgs e)
        {
            if (window.ShowSystemMenuOnRightClick)
            {
                // show menu only if mouse pos is on title bar or if we have a window with none style and no title bar
                var mousePos = e.GetPosition(window);
                if ((mousePos.Y <= window.TitlebarHeight && window.TitlebarHeight > 0) || (window.TitlebarHeight <= 0))
                {
                    ShowSystemMenuPhysicalCoordinates(window, window.PointToScreen(mousePos));
                }
            }
        }

        /// <summary>
        /// Gets the template child with the given name.
        /// </summary>
        /// <typeparam name="T">The interface type inheirted from DependencyObject.</typeparam>
        /// <param name="name">The name of the template child.</param>
        internal T GetPart<T>(string name) where T : class
        {
            return GetTemplateChild(name) as T;
        }

        /// <summary>
        /// Gets the template child with the given name.
        /// </summary>
        /// <param name="name">The name of the template child.</param>
        internal DependencyObject GetPart(string name)
        {
            return GetTemplateChild(name);
        }

        private static void ShowSystemMenuPhysicalCoordinates(Window window, Point physicalScreenLocation)
        {
            if (window == null) return;

            var hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero || !UnsafeNativeMethods.IsWindow(hwnd))
                return;

            var hmenu = UnsafeNativeMethods.GetSystemMenu(hwnd, false);

            var cmd = UnsafeNativeMethods.TrackPopupMenuEx(hmenu, NativeConstants.TPM_LEFTBUTTON | NativeConstants.TPM_RETURNCMD,
                (int)physicalScreenLocation.X, (int)physicalScreenLocation.Y, hwnd, IntPtr.Zero);
            if (0 != cmd)
                UnsafeNativeMethods.PostMessage(hwnd, NativeConstants.SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
        }

        internal void HandleFlyoutStatusChange(Flyout flyout, IEnumerable<Flyout> visibleFlyouts)
        {
            //checks a recently opened flyout's position.
            var enumerable = visibleFlyouts as IList<Flyout> ?? visibleFlyouts.ToList();
            if (flyout.Position == Position.Left || flyout.Position == Position.Right || flyout.Position == Position.Top)
            {
                //get it's zindex
                var zIndex = flyout.IsOpen ? Panel.GetZIndex(flyout) + 3 : enumerable.Count() + 2;

                // Note: ShowWindowCommandsOnTop is here for backwards compatibility reasons
                //if the the corresponding behavior has the right flag, set the window commands' and icon zIndex to a number that is higher than the flyout's.
                _icon.SetValue(Panel.ZIndexProperty, IconOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
                LeftWindowCommandsPresenter?.SetValue(Panel.ZIndexProperty, LeftWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
                RightWindowCommandsPresenter?.SetValue(Panel.ZIndexProperty, RightWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
                WindowButtonCommandsPresenter?.SetValue(Panel.ZIndexProperty, WindowButtonCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
            }

            if (_flyoutModal != null)
            {
                _flyoutModal.Visibility = enumerable.Any(x => x.IsModal) ? Visibility.Visible : Visibility.Hidden;
            }

            RaiseEvent(new FlyoutStatusChangedRoutedEventArgs(FlyoutsStatusChangedEvent, this) { ChangedFlyout = flyout });
        }

        public class FlyoutStatusChangedRoutedEventArgs : RoutedEventArgs
        {
            internal FlyoutStatusChangedRoutedEventArgs(RoutedEvent rEvent, object source) : base(rEvent, source)
            { }

            // ReSharper disable once UnusedAutoPropertyAccessor.Global
            public Flyout ChangedFlyout { get; internal set; }
        }
    }
}