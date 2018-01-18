using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using My.CoachManager.Presentation.Prism.Controls.Flyouts;
using My.CoachManager.Presentation.Prism.Controls.Helpers;
using My.CoachManager.Presentation.Prism.Controls.Windows;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// A sliding panel control that is hosted in a MetroWindow via a FlyoutsControl.
    /// <see cref="ExtendedWindow"/>
    /// <seealso cref="FlyoutsControl"/>
    /// </summary>
    [TemplatePart(Name = "PART_BackButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_BackHeaderText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_Root", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Header", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Content", Type = typeof(FrameworkElement))]
    public class Flyout : ContentControl
    {
        /// <summary>
        /// An event that is raised when IsOpen changes.
        /// </summary>
        public static readonly RoutedEvent IsOpenChangedEvent =
            EventManager.RegisterRoutedEvent("IsOpenChanged", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(Flyout));

        public event RoutedEventHandler IsOpenChanged
        {
            add { AddHandler(IsOpenChangedEvent, value); }
            remove { RemoveHandler(IsOpenChangedEvent, value); }
        }

        /// <summary>
        /// An event that is raised when the closing animation has finished.
        /// </summary>
        public static readonly RoutedEvent ClosingFinishedEvent =
            EventManager.RegisterRoutedEvent("ClosingFinished", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(Flyout));

        public event RoutedEventHandler ClosingFinished
        {
            add { AddHandler(ClosingFinishedEvent, value); }
            remove { RemoveHandler(ClosingFinishedEvent, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(Flyout), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(Position), typeof(Flyout), new PropertyMetadata(Position.Left, PositionChanged));
        public static readonly DependencyProperty IsPinnedProperty = DependencyProperty.Register("IsPinned", typeof(bool), typeof(Flyout), new PropertyMetadata(true));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(Flyout), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsOpenedChanged));
        public static readonly DependencyProperty AnimateOnPositionChangeProperty = DependencyProperty.Register("AnimateOnPositionChange", typeof(bool), typeof(Flyout), new PropertyMetadata(true));
        public static readonly DependencyProperty AnimateOpacityProperty = DependencyProperty.Register("AnimateOpacity", typeof(bool), typeof(Flyout), new FrameworkPropertyMetadata(false, AnimateOpacityChanged));
        public static readonly DependencyProperty IsModalProperty = DependencyProperty.Register("IsModal", typeof(bool), typeof(Flyout));
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(Flyout));

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.RegisterAttached("CloseCommand", typeof(ICommand), typeof(Flyout), new UIPropertyMetadata(null));
        public static readonly DependencyProperty CloseCommandParameterProperty = DependencyProperty.Register("CloseCommandParameter", typeof(object), typeof(Flyout), new PropertyMetadata(null));
        internal static readonly DependencyProperty InternalCloseCommandProperty = DependencyProperty.Register("InternalCloseCommand", typeof(ICommand), typeof(Flyout));

        public static readonly DependencyProperty ExternalCloseButtonProperty = DependencyProperty.Register("ExternalCloseButton", typeof(MouseButton), typeof(Flyout), new PropertyMetadata(MouseButton.Left));
        public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(Flyout), new FrameworkPropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty CloseButtonIsCancelProperty = DependencyProperty.Register("CloseButtonIsCancel", typeof(bool), typeof(Flyout), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty TitleVisibilityProperty = DependencyProperty.Register("TitleVisibility", typeof(Visibility), typeof(Flyout), new FrameworkPropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty AreAnimationsEnabledProperty = DependencyProperty.Register("AreAnimationsEnabled", typeof(bool), typeof(Flyout), new PropertyMetadata(true));
        public static readonly DependencyProperty FocusedElementProperty = DependencyProperty.Register("FocusedElement", typeof(FrameworkElement), typeof(Flyout), new UIPropertyMetadata(null));
        public static readonly DependencyProperty AllowFocusElementProperty = DependencyProperty.Register("AllowFocusElement", typeof(bool), typeof(Flyout), new PropertyMetadata(true));
        public static readonly DependencyProperty IsAutoCloseEnabledProperty = DependencyProperty.Register("IsAutoCloseEnabled", typeof(bool), typeof(Flyout), new FrameworkPropertyMetadata(false, IsAutoCloseEnabledChanged));
        public static readonly DependencyProperty AutoCloseIntervalProperty = DependencyProperty.Register("AutoCloseInterval", typeof(long), typeof(Flyout), new FrameworkPropertyMetadata(5000L, AutoCloseIntervalChanged));

        internal PropertyChangeNotifier IsOpenPropertyChangeNotifier { get; set; }
        internal PropertyChangeNotifier ThemePropertyChangeNotifier { get; set; }

        public bool AreAnimationsEnabled
        {
            get { return (bool)GetValue(AreAnimationsEnabledProperty); }
            set { SetValue(AreAnimationsEnabledProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the title is visible in this flyout.
        /// </summary>
        public Visibility TitleVisibility
        {
            get { return (Visibility)GetValue(TitleVisibilityProperty); }
            set { SetValue(TitleVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the close button is visible in this flyout.
        /// </summary>
        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets/sets if the close button is a cancel button in this flyout.
        /// </summary>
        public bool CloseButtonIsCancel
        {
            get { return (bool)GetValue(CloseButtonIsCancelProperty); }
            set { SetValue(CloseButtonIsCancelProperty, value); }
        }

        /// <summary>
        /// Gets/sets a command which will be executed if the close button was clicked.
        /// Note that this won't execute when <see cref="IsOpen"/> is set to <c>false</c>.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        /// <summary>
        /// Gets/sets the command parameter which will be passed by the CloseCommand.
        /// </summary>
        public object CloseCommandParameter
        {
            get { return GetValue(CloseCommandParameterProperty); }
            set { SetValue(CloseCommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets/sets a command which will be executed if the close button was clicked.
        /// </summary>
        internal ICommand InternalCloseCommand
        {
            get { return (ICommand)GetValue(InternalCloseCommandProperty); }
            set { SetValue(InternalCloseCommandProperty, value); }
        }

        /// <summary>
        /// A DataTemplate for the flyout's header.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Gets/sets whether this flyout is visible.
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// Gets/sets whether this flyout uses the open/close animation when changing the <see cref="Position"/> property. (default is true)
        /// </summary>
        public bool AnimateOnPositionChange
        {
            get { return (bool)GetValue(AnimateOnPositionChangeProperty); }
            set { SetValue(AnimateOnPositionChangeProperty, value); }
        }

        /// <summary>
        /// Gets/sets whether this flyout animates the opacity of the flyout when opening/closing.
        /// </summary>
        public bool AnimateOpacity
        {
            get { return (bool)GetValue(AnimateOpacityProperty); }
            set { SetValue(AnimateOpacityProperty, value); }
        }

        /// <summary>
        /// Gets/sets whether this flyout stays open when the user clicks outside of it.
        /// </summary>
        public bool IsPinned
        {
            get { return (bool)GetValue(IsPinnedProperty); }
            set { SetValue(IsPinnedProperty, value); }
        }

        /// <summary>
        /// Gets/sets the mouse button that closes the flyout on an external mouse click.
        /// </summary>
        public MouseButton ExternalCloseButton
        {
            get { return (MouseButton)GetValue(ExternalCloseButtonProperty); }
            set { SetValue(ExternalCloseButtonProperty, value); }
        }

        /// <summary>
        /// Gets/sets whether this flyout is modal.
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        /// <summary>
        /// Gets/sets this flyout's position in the FlyoutsControl/MetroWindow.
        /// </summary>
        public Position Position
        {
            get { return (Position)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// Gets/sets the flyout's header.
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the focused element.
        /// </summary>
        public FrameworkElement FocusedElement
        {
            get { return (FrameworkElement)GetValue(FocusedElementProperty); }
            set { SetValue(FocusedElementProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the flyout should auto close after AutoCloseInterval has passed.
        /// </summary>
        public bool IsAutoCloseEnabled
        {
            get { return (bool)GetValue(IsAutoCloseEnabledProperty); }
            set { SetValue(IsAutoCloseEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the time in milliseconds when the flyout should auto close.
        /// </summary>
        public long AutoCloseInterval
        {
            get { return (long)GetValue(AutoCloseIntervalProperty); }
            set { SetValue(AutoCloseIntervalProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the flyout should try focus an element.
        /// </summary>
        public bool AllowFocusElement
        {
            get { return (bool)GetValue(AllowFocusElementProperty); }
            set { SetValue(AllowFocusElementProperty, value); }
        }

        static Flyout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Flyout), new FrameworkPropertyMetadata(typeof(Flyout)));
        }

        public Flyout()
        {
            InternalCloseCommand = new CloseCommand(InternalCloseCommandCanExecute, InternalCloseCommandExecuteAction);
            Loaded += (sender, args) => UpdateFlyoutTheme();
            InitializeAutoCloseTimer();
        }

        private void InternalCloseCommandExecuteAction(object o)
        {
            var closeCommand = CloseCommand;
            // close the Flyout only if there is no command
            if (closeCommand == null)
            {
                IsOpen = false;
            }
            else
            {
                var closeCommandParameter = CloseCommandParameter ?? this;
                if (closeCommand.CanExecute(closeCommandParameter))
                {
                    // force the command handler to run
                    closeCommand.Execute(closeCommandParameter);
                }
            }
        }

        private bool InternalCloseCommandCanExecute(object o)
        {
            var closeCommand = CloseCommand;
            return closeCommand == null || closeCommand.CanExecute(CloseCommandParameter ?? this);
        }

        private void InitializeAutoCloseTimer()
        {
            StopAutoCloseTimer();

            _autoCloseTimer = new DispatcherTimer();
            _autoCloseTimer.Tick += AutoCloseTimerCallback;
            _autoCloseTimer.Interval = TimeSpan.FromMilliseconds(AutoCloseInterval);
        }

        private ExtendedWindow _parentWindow;

        private ExtendedWindow ParentWindow => _parentWindow ?? (_parentWindow = this.TryFindParent<ExtendedWindow>());

        private void UpdateFlyoutTheme()
        {
            var flyoutsControl = this.TryFindParent<FlyoutsControl>();

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                Visibility = flyoutsControl != null ? Visibility.Collapsed : Visibility.Visible;
            }

            var window = ParentWindow;
            if (window != null)
            {
                // we must certain to get the right foreground for window commands and buttons
                if (flyoutsControl != null && IsOpen)
                {
                    flyoutsControl.HandleFlyoutStatusChange(this, window);
                }
            }
        }

        private void UpdateOpacityChange()
        {
            if (_flyoutRoot == null || _fadeOutFrame == null || System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            if (!AnimateOpacity)
            {
                _fadeOutFrame.Value = 1;
                _flyoutRoot.Opacity = 1;
            }
            else
            {
                _fadeOutFrame.Value = 0;
                if (!IsOpen) _flyoutRoot.Opacity = 0;
            }
        }

        private static void IsOpenedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout)dependencyObject;

            Action openedChangedAction = () =>
            {
                if (e.NewValue != e.OldValue)
                {
                    if (flyout.AreAnimationsEnabled)
                    {
                        if ((bool)e.NewValue)
                        {
                            if (flyout._hideStoryboard != null)
                            {
                                // don't let the storyboard end it's completed event
                                // otherwise it could be hidden on start
                                flyout._hideStoryboard.Completed -= flyout.HideStoryboardCompleted;
                            }
                            flyout.Visibility = Visibility.Visible;
                            flyout.ApplyAnimation(flyout.Position, flyout.AnimateOpacity);
                            flyout.TryFocusElement();
                            if (flyout.IsAutoCloseEnabled)
                            {
                                flyout.StartAutoCloseTimer();
                            }
                        }
                        else
                        {
                            flyout.StopAutoCloseTimer();
                            if (flyout._hideStoryboard != null)
                            {
                                flyout._hideStoryboard.Completed += flyout.HideStoryboardCompleted;
                            }
                            else
                            {
                                flyout.Hide();
                            }
                        }
                        VisualStateManager.GoToState(flyout, (bool)e.NewValue == false ? "Hide" : "Show", true);
                    }
                    else
                    {
                        if ((bool)e.NewValue)
                        {
                            flyout.Visibility = Visibility.Visible;
                            flyout.TryFocusElement();
                            if (flyout.IsAutoCloseEnabled)
                            {
                                flyout.StartAutoCloseTimer();
                            }
                        }
                        else
                        {
                            flyout.StopAutoCloseTimer();
                            flyout.Hide();
                        }
                        VisualStateManager.GoToState(flyout, (bool)e.NewValue == false ? "HideDirect" : "ShowDirect", true);
                    }
                }

                flyout.RaiseEvent(new RoutedEventArgs(IsOpenChangedEvent));
            };

            flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, openedChangedAction);
        }

        private static void IsAutoCloseEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout)dependencyObject;

            Action autoCloseEnabledChangedAction = () =>
            {
                if (e.NewValue != e.OldValue)
                {
                    if ((bool)e.NewValue)
                    {
                        if (flyout.IsOpen)
                        {
                            flyout.StartAutoCloseTimer();
                        }
                    }
                    else
                    {
                        flyout.StopAutoCloseTimer();
                    }
                }
            };

            flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, autoCloseEnabledChangedAction);
        }

        private static void AutoCloseIntervalChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout)dependencyObject;

            Action autoCloseIntervalChangedAction = () =>
            {
                if (e.NewValue != e.OldValue)
                {
                    flyout.InitializeAutoCloseTimer();
                    if (flyout.IsAutoCloseEnabled && flyout.IsOpen)
                    {
                        flyout.StartAutoCloseTimer();
                    }
                }
            };

            flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, autoCloseIntervalChangedAction);
        }

        private void StartAutoCloseTimer()
        {
            //in case it is already running
            StopAutoCloseTimer();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                _autoCloseTimer.Start();
            }
        }

        private void StopAutoCloseTimer()
        {
            if ((_autoCloseTimer != null) && (_autoCloseTimer.IsEnabled))
            {
                _autoCloseTimer.Stop();
            }
        }

        private void AutoCloseTimerCallback(Object sender, EventArgs e)
        {
            StopAutoCloseTimer();

            //if the flyout is open and autoclose is still enabled then close the flyout
            if ((IsOpen) && (IsAutoCloseEnabled))
            {
                IsOpen = false;
            }
        }

        private void HideStoryboardCompleted(object sender, EventArgs e)
        {
            _hideStoryboard.Completed -= HideStoryboardCompleted;
            Hide();
        }

        private void Hide()
        {
            // hide the flyout, we should get better performance and prevent showing the flyout on any resizing events
            Visibility = Visibility.Hidden;
            RaiseEvent(new RoutedEventArgs(ClosingFinishedEvent));
        }

        private void TryFocusElement()
        {
            if (AllowFocusElement)
            {
                // first focus itself
                Focus();

                if (FocusedElement != null)
                {
                    FocusedElement.Focus();
                }
                else if (_flyoutContent == null || !_flyoutContent.MoveFocus(new TraversalRequest(FocusNavigationDirection.First)))
                {
                    _flyoutHeader?.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                }
            }
        }

        private static void AnimateOpacityChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout)dependencyObject;
            flyout.UpdateOpacityChange();
        }

        private static void PositionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout)dependencyObject;
            var wasOpen = flyout.IsOpen;
            if (wasOpen && flyout.AnimateOnPositionChange)
            {
                flyout.ApplyAnimation((Position)e.NewValue, flyout.AnimateOpacity);
                VisualStateManager.GoToState(flyout, "Hide", true);
            }
            else
            {
                flyout.ApplyAnimation((Position)e.NewValue, flyout.AnimateOpacity, false);
            }

            if (wasOpen && flyout.AnimateOnPositionChange)
            {
                flyout.ApplyAnimation((Position)e.NewValue, flyout.AnimateOpacity);
                VisualStateManager.GoToState(flyout, "Show", true);
            }
        }

        private DispatcherTimer _autoCloseTimer;
        private Grid _flyoutRoot;
        private Storyboard _hideStoryboard;
        private SplineDoubleKeyFrame _hideFrame;
        private SplineDoubleKeyFrame _hideFrameY;
        private SplineDoubleKeyFrame _showFrame;
        private SplineDoubleKeyFrame _showFrameY;
        private SplineDoubleKeyFrame _fadeOutFrame;
        private FrameworkElement _flyoutHeader;
        private FrameworkElement _flyoutContent;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _flyoutRoot = GetTemplateChild("PART_Root") as Grid;
            if (_flyoutRoot == null)
            {
                return;
            }

            _flyoutHeader = GetTemplateChild("PART_Header") as FrameworkElement;
            _flyoutHeader?.ApplyTemplate();
            _flyoutContent = GetTemplateChild("PART_Content") as FrameworkElement;

            var thumbContentControl = _flyoutHeader as IThumb;
            if (thumbContentControl != null)
            {
                thumbContentControl.PreviewMouseLeftButtonUp -= WindowTitleThumbOnPreviewMouseLeftButtonUp;

                var flyoutsControl = this.TryFindParent<FlyoutsControl>();
                if (flyoutsControl != null)
                {
                    thumbContentControl.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                }
            }

            _hideStoryboard = GetTemplateChild("HideStoryboard") as Storyboard;
            _hideFrame = GetTemplateChild("hideFrame") as SplineDoubleKeyFrame;
            _hideFrameY = GetTemplateChild("hideFrameY") as SplineDoubleKeyFrame;
            _showFrame = GetTemplateChild("showFrame") as SplineDoubleKeyFrame;
            _showFrameY = GetTemplateChild("showFrameY") as SplineDoubleKeyFrame;
            _fadeOutFrame = GetTemplateChild("fadeOutFrame") as SplineDoubleKeyFrame;

            if (_hideFrame == null || _showFrame == null || _hideFrameY == null || _showFrameY == null || _fadeOutFrame == null)
            {
                return;
            }

            ApplyAnimation(Position, AnimateOpacity);
        }

        protected internal void CleanUp(FlyoutsControl flyoutsControl)
        {
            var thumbContentControl = _flyoutHeader as IThumb;
            if (thumbContentControl != null)
            {
                thumbContentControl.PreviewMouseLeftButtonUp -= WindowTitleThumbOnPreviewMouseLeftButtonUp;
            }
            _parentWindow = null;
        }

        private void WindowTitleThumbOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var window = ParentWindow;
            if (window != null && Position != Position.Bottom)
            {
                ExtendedWindow.DoWindowTitleThumbOnPreviewMouseLeftButtonUp(window, e);
            }
        }

        internal void ApplyAnimation(Position position, bool animateOpacity, bool resetShowFrame = true)
        {
            if (_flyoutRoot == null || _hideFrame == null || _showFrame == null || _hideFrameY == null || _showFrameY == null || _fadeOutFrame == null)
                return;

            if (Position == Position.Left || Position == Position.Right)
                _showFrame.Value = 0;
            if (Position == Position.Top || Position == Position.Bottom)
                _showFrameY.Value = 0;

            // I mean, we don't need this anymore, because we use ActualWidth and ActualHeight of the flyoutRoot
            //this.flyoutRoot.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            if (!animateOpacity)
            {
                _fadeOutFrame.Value = 1;
                _flyoutRoot.Opacity = 1;
            }
            else
            {
                _fadeOutFrame.Value = 0;
                if (!IsOpen) _flyoutRoot.Opacity = 0;
            }

            switch (position)
            {
                default:
                    HorizontalAlignment = Margin.Right <= 0 ? (HorizontalContentAlignment != HorizontalAlignment.Stretch ? HorizontalAlignment.Left : HorizontalContentAlignment) : HorizontalAlignment.Stretch;//HorizontalAlignment.Left;
                    VerticalAlignment = VerticalAlignment.Stretch;
                    _hideFrame.Value = -_flyoutRoot.ActualWidth - Margin.Left;
                    if (resetShowFrame)
                        _flyoutRoot.RenderTransform = new TranslateTransform(-_flyoutRoot.ActualWidth, 0);
                    break;

                case Position.Right:
                    HorizontalAlignment = Margin.Left <= 0 ? (HorizontalContentAlignment != HorizontalAlignment.Stretch ? HorizontalAlignment.Right : HorizontalContentAlignment) : HorizontalAlignment.Stretch;//HorizontalAlignment.Right;
                    VerticalAlignment = VerticalAlignment.Stretch;
                    _hideFrame.Value = _flyoutRoot.ActualWidth + Margin.Right;
                    if (resetShowFrame)
                        _flyoutRoot.RenderTransform = new TranslateTransform(_flyoutRoot.ActualWidth, 0);
                    break;

                case Position.Top:
                    HorizontalAlignment = HorizontalAlignment.Stretch;
                    VerticalAlignment = Margin.Bottom <= 0 ? (VerticalContentAlignment != VerticalAlignment.Stretch ? VerticalAlignment.Top : VerticalContentAlignment) : VerticalAlignment.Stretch;//VerticalAlignment.Top;
                    _hideFrameY.Value = -_flyoutRoot.ActualHeight - 1 - Margin.Top;
                    if (resetShowFrame)
                        _flyoutRoot.RenderTransform = new TranslateTransform(0, -_flyoutRoot.ActualHeight - 1);
                    break;

                case Position.Bottom:
                    HorizontalAlignment = HorizontalAlignment.Stretch;
                    VerticalAlignment = Margin.Top <= 0 ? (VerticalContentAlignment != VerticalAlignment.Stretch ? VerticalAlignment.Bottom : VerticalContentAlignment) : VerticalAlignment.Stretch;//VerticalAlignment.Bottom;
                    _hideFrameY.Value = _flyoutRoot.ActualHeight + Margin.Bottom;
                    if (resetShowFrame)
                        _flyoutRoot.RenderTransform = new TranslateTransform(0, _flyoutRoot.ActualHeight);
                    break;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (!IsOpen) return; // no changes for invisible flyouts, ApplyAnimation is called now in visible changed event
            if (!sizeInfo.WidthChanged && !sizeInfo.HeightChanged) return;
            if (_flyoutRoot == null || _hideFrame == null || _showFrame == null || _hideFrameY == null || _showFrameY == null)
                return; // don't bother checking IsOpen and calling ApplyAnimation

            if (Position == Position.Left || Position == Position.Right)
                _showFrame.Value = 0;
            if (Position == Position.Top || Position == Position.Bottom)
                _showFrameY.Value = 0;

            switch (Position)
            {
                default:
                    _hideFrame.Value = -_flyoutRoot.ActualWidth - Margin.Left;
                    break;

                case Position.Right:
                    _hideFrame.Value = _flyoutRoot.ActualWidth + Margin.Right;
                    break;

                case Position.Top:
                    _hideFrameY.Value = -_flyoutRoot.ActualHeight - 1 - Margin.Top;
                    break;

                case Position.Bottom:
                    _hideFrameY.Value = _flyoutRoot.ActualHeight + Margin.Bottom;
                    break;
            }
        }
    }
}