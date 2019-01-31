using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using My.CoachManager.Presentation.Controls.AdornedControls;

namespace My.CoachManager.Presentation.Controls
{
    /// <summary>
    /// A content control that allows an _adorner for the content to be defined in XAML.
    /// </summary>
    public class AdornedControl : System.Windows.Controls.ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty AdornedTemplatePartNameProperty = DependencyProperty.Register(
            "AdornedTemplatePartName",
            typeof(string),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty AdornerContentProperty = DependencyProperty.Register(
            "AdornerContent",
            typeof(FrameworkElement),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(OnAdornerContentPropertyChanged));

        public static readonly DependencyProperty AdornerOffsetXProperty = DependencyProperty.Register(
            "AdornerOffsetX",
            typeof(double),
            typeof(AdornedControl));

        public static readonly DependencyProperty AdornerOffsetYProperty = DependencyProperty.Register(
            "AdornerOffsetY",
            typeof(double),
            typeof(AdornedControl));

        public static readonly DependencyProperty CloseAdornerTimeOutProperty = DependencyProperty.Register(
            "CloseAdornerTimeOut",
            typeof(double),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(2.0, OnCloseAdornerTimeOutPropertyChanged));

        public static readonly DependencyProperty HorizontalAdornerPlacementProperty = DependencyProperty.Register(
            "HorizontalAdornerPlacement",
            typeof(AdornerPlacement),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(AdornerPlacement.Inside));

        public static readonly DependencyProperty FadeInTimeProperty = DependencyProperty.Register(
            "FadeInTime",
            typeof(double),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(0.25));

        public static readonly DependencyProperty FadeOutTimeProperty = DependencyProperty.Register(
            "FadeOutTime",
            typeof(double),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(1.0));

        public static readonly DependencyProperty IsAdornerVisibleProperty = DependencyProperty.Register(
            "IsAdornerVisible",
            typeof(bool),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(OnIsAdornerVisiblePropertyChanged));

        public static readonly DependencyProperty IsMouseOverShowEnabledProperty = DependencyProperty.Register(
            "IsMouseOverShowEnabled",
            typeof(bool),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(false, OnIsMouseOverShowEnabledPropertyChanged));

        public static readonly DependencyProperty VerticalAdornerPlacementProperty = DependencyProperty.Register(
            "VerticalAdornerPlacement",
            typeof(AdornerPlacement),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(AdornerPlacement.Inside));

        #endregion Dependency Properties

        #region Commands

        public static readonly RoutedCommand FadeInAdornerCommand = new RoutedCommand("FadeInAdorner", typeof(AdornedControl));
        public static readonly RoutedCommand FadeOutAdornerCommand = new RoutedCommand("FadeOutAdorner", typeof(AdornedControl));
        public static readonly RoutedCommand HideAdornerCommand = new RoutedCommand("HideAdorner", typeof(AdornedControl));
        public static readonly RoutedCommand ShowAdornerCommand = new RoutedCommand("ShowAdorner", typeof(AdornedControl));

        private static readonly CommandBinding ShowAdornerCommandBinding = new CommandBinding(ShowAdornerCommand, OnShowAdornerCommandExecuted);
        private static readonly CommandBinding FadeInAdornerCommandBinding = new CommandBinding(FadeInAdornerCommand, OnFadeInAdornerCommandExecuted);
        private static readonly CommandBinding HideAdornerCommandBinding = new CommandBinding(HideAdornerCommand, OnHideAdornerCommandExecuted);
        private static readonly CommandBinding FadeOutAdornerCommandBinding = new CommandBinding(FadeInAdornerCommand, OnFadeOutAdornerCommandExecuted);

        #endregion Commands

        #region Fields

        /// <summary>
        /// The actual _adorner create to contain our '_adorner UI content'.
        /// </summary>
        private FrameworkElementAdorner _adorner;

        /// <summary>
        /// Caches the _adorner layer.
        /// </summary>
        private AdornerLayer _adornerLayer;

        /// <summary>
        /// Specifies the current show/hide state of the _adorner.
        /// </summary>
        private AdornerShowState _adornerShowState = AdornerShowState.Hidden;

        /// <summary>
        /// This timer is used to fade out and close the _adorner.
        /// </summary>
        private readonly DispatcherTimer _closeAdornerTimer = new DispatcherTimer();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises static members of the <see cref="AdornedControl"/> class.
        /// </summary>
        static AdornedControl()
        {
            CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), ShowAdornerCommandBinding);
            CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), FadeOutAdornerCommandBinding);
            CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), HideAdornerCommandBinding);
            CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), FadeInAdornerCommandBinding);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AdornedControl"/> class.
        /// </summary>
        public AdornedControl()
        {
            Focusable = false; // By default don't want 'AdornedControl' to be focusable.

            DataContextChanged += OnAdornedControlDataContextChanged;

            _closeAdornerTimer.Tick += OnCloseAdornerTimerTick;
            _closeAdornerTimer.Interval = TimeSpan.FromSeconds(CloseAdornerTimeOut);
        }

        #endregion Constructors

        #region Private Enumerations

        /// <summary>
        /// Specifies the current show/hide state of the _adorner.
        /// </summary>
        private enum AdornerShowState
        {
            /// <summary>
            /// The _adorner is visible.
            /// </summary>
            Visible,

            /// <summary>
            /// The _adorner is hidden.
            /// </summary>
            Hidden,

            /// <summary>
            /// The _adorner is fading in.
            /// </summary>
            FadingIn,

            /// <summary>
            /// The _adorner is fading out.
            /// </summary>
            FadingOut,
        }

        #endregion Private Enumerations

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the adorned template part. When set to non-null it specifies the part name of a UI element
        /// in the visual tree of the AdornedControl content that is to be adorned. When this property is null it is the
        /// AdornerControl content that is adorned.
        /// </summary>
        /// <value>
        /// The name of the adorned template part.
        /// </value>
        public string AdornedTemplatePartName
        {
            get { return (string)GetValue(AdornedTemplatePartNameProperty); }
            set { SetValue(AdornedTemplatePartNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content of the _adorner.
        /// </summary>
        /// <value>
        /// The content of the _adorner.
        /// </value>
        public FrameworkElement AdornerContent
        {
            get { return (FrameworkElement)GetValue(AdornerContentProperty); }
            set { SetValue(AdornerContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the _adorner offset for the X-axis.
        /// </summary>
        /// <value>
        /// The _adorner offset for the X-axis.
        /// </value>
        public double AdornerOffsetX
        {
            get { return (double)GetValue(AdornerOffsetXProperty); }
            set { SetValue(AdornerOffsetXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the _adorner offset for the Y-axis.
        /// </summary>
        /// <value>
        /// The _adorner offset for the Y-axis.
        /// </value>
        public double AdornerOffsetY
        {
            get { return (double)GetValue(AdornerOffsetYProperty); }
            set { SetValue(AdornerOffsetYProperty, value); }
        }

        /// <summary>
        /// Gets or sets the close _adorner time out. Specifies the time (in seconds) after the mouse cursor moves away from the
        /// adorned control (or the _adorner) when the _adorner begins to fade out.
        /// </summary>
        /// <value>
        /// The close _adorner time out.
        /// </value>
        public double CloseAdornerTimeOut
        {
            get { return (double)GetValue(CloseAdornerTimeOutProperty); }
            set { SetValue(CloseAdornerTimeOutProperty, value); }
        }

        /// <summary>
        /// Gets or sets the fade in time in seconds.
        /// </summary>
        /// <value>
        /// The fade in time.
        /// </value>
        public double FadeInTime
        {
            get { return (double)GetValue(FadeInTimeProperty); }
            set { SetValue(FadeInTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the fade out time in seconds.
        /// </summary>
        /// <value>
        /// The fade out time.
        /// </value>
        public double FadeOutTime
        {
            get { return (double)GetValue(FadeOutTimeProperty); }
            set { SetValue(FadeOutTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the _adorner is visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if the _adorner is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdornerVisible
        {
            get { return (bool)GetValue(IsAdornerVisibleProperty); }
            set { SetValue(IsAdornerVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to fade in and out the _adorner when the mouse is hovered over the adorned control.
        /// </summary>
        /// <value>
        /// <c>true</c> if fade in and out is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsMouseOverShowEnabled
        {
            get { return (bool)GetValue(IsMouseOverShowEnabledProperty); }
            set { SetValue(IsMouseOverShowEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the horizontal placement of the _adorner relative to the adorned control.
        /// </summary>
        /// <value>
        /// The horizontal _adorner placement.
        /// </value>
        public AdornerPlacement HorizontalAdornerPlacement
        {
            get { return (AdornerPlacement)GetValue(HorizontalAdornerPlacementProperty); }
            set { SetValue(HorizontalAdornerPlacementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the vertical placement of the _adorner relative to the adorned control.
        /// </summary>
        /// <value>
        /// The vertical _adorner placement.
        /// </value>
        public AdornerPlacement VerticalAdornerPlacement
        {
            get { return (AdornerPlacement)GetValue(VerticalAdornerPlacementProperty); }
            set { SetValue(VerticalAdornerPlacementProperty, value); }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Fade the _adorner in and make it visible.
        /// </summary>
        public void FadeInAdorner()
        {
            if (_adornerShowState == AdornerShowState.Visible ||
                _adornerShowState == AdornerShowState.FadingIn)
            {
                // Already visible or fading in.
                return;
            }

            ShowAdorner();

            if (_adornerShowState != AdornerShowState.FadingOut)
            {
                _adorner.Opacity = 0.0;
            }

            DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(FadeInTime)));
            doubleAnimation.Completed += OnFadeInAnimationCompleted;
            doubleAnimation.Freeze();

            _adorner.BeginAnimation(OpacityProperty, doubleAnimation);

            _adornerShowState = AdornerShowState.FadingIn;
        }

        /// <summary>
        /// Fade the _adorner out and make it visible.
        /// </summary>
        public void FadeOutAdorner()
        {
            if (_adornerShowState == AdornerShowState.FadingOut)
            {
                // Already fading out.
                return;
            }

            if (_adornerShowState == AdornerShowState.Hidden)
            {
                // Adorner has already been hidden.
                return;
            }

            DoubleAnimation fadeOutAnimation = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(FadeOutTime)));
            fadeOutAnimation.Completed += FadeOutAnimationCompleted;
            fadeOutAnimation.Freeze();

            _adorner.BeginAnimation(OpacityProperty, fadeOutAnimation);

            _adornerShowState = AdornerShowState.FadingOut;
        }

        /// <summary>
        /// Hide the _adorner.
        /// </summary>
        public void HideAdorner()
        {
            IsAdornerVisible = false;
        }

        /// <summary>
        /// Show the _adorner.
        /// </summary>
        public void ShowAdorner()
        {
            IsAdornerVisible = true;
        }

        #endregion Public Methods

        #region Public Methods

        /// <summary>
        /// Called to build the visual tree.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ShowOrHideAdornerInternal();
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Called when the mouse cursor enters the area of the adorned control.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEnterLogic();
        }

        /// <summary>
        /// Called when the mouse cursor leaves the area of the adorned control.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            MouseLeaveLogic();
        }

        #endregion Protected Methods

        #region Private Static Methods

        /// <summary>
        /// Finds a child element in the visual tree that has the specified name.
        /// Returns null if no child with that name exists.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <param name="childName">Name of the child.</param>
        /// <returns>The child element with the specified name.</returns>
        private static FrameworkElement FindNamedChild(FrameworkElement rootElement, string childName)
        {
            int numChildren = VisualTreeHelper.GetChildrenCount(rootElement);
            for (int i = 0; i < numChildren; ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(rootElement, i);
                FrameworkElement childElement = child as FrameworkElement;
                if (childElement != null && childElement.Name == childName)
                {
                    return childElement;
                }

                FrameworkElement foundElement = FindNamedChild(childElement, childName);
                if (foundElement != null)
                {
                    return foundElement;
                }
            }

            return null;
        }

        /// <summary>
        /// Event raised when the value of AdornerContent has changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAdornerContentPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            AdornedControl c = (AdornedControl)dependencyObject;
            c.ShowOrHideAdornerInternal();

            FrameworkElement oldAdornerContent = (FrameworkElement)e.OldValue;
            if (oldAdornerContent != null)
            {
                oldAdornerContent.MouseEnter -= c.OnAdornerContentMouseEnter;
                oldAdornerContent.MouseLeave -= c.OnAdornerContentMouseLeave;
            }

            FrameworkElement newAdornerContent = (FrameworkElement)e.NewValue;
            if (newAdornerContent != null)
            {
                newAdornerContent.MouseEnter += c.OnAdornerContentMouseEnter;
                newAdornerContent.MouseLeave += c.OnAdornerContentMouseLeave;
            }
        }

        /// <summary>
        /// Event raised when the CloseAdornerTimeOut property has change.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnCloseAdornerTimeOutPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            AdornedControl c = (AdornedControl)dependencyObject;
            c._closeAdornerTimer.Interval = TimeSpan.FromSeconds(c.CloseAdornerTimeOut);
        }

        /// <summary>
        /// Event raised when the FadeIn command is executed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private static void OnFadeInAdornerCommandExecuted(object target, ExecutedRoutedEventArgs e)
        {
            AdornedControl c = (AdornedControl)target;
            c.FadeOutAdorner();
        }

        /// <summary>
        /// Event raised when the FadeOut command is executed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private static void OnFadeOutAdornerCommandExecuted(object target, ExecutedRoutedEventArgs e)
        {
            AdornedControl c = (AdornedControl)target;
            c.FadeOutAdorner();
        }

        /// <summary>
        /// Event raised when the Hide command is executed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private static void OnHideAdornerCommandExecuted(object target, ExecutedRoutedEventArgs e)
        {
            AdornedControl c = (AdornedControl)target;
            c.HideAdorner();
        }

        /// <summary>
        /// Event raised when the value of IsAdornerVisible has changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsAdornerVisiblePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            AdornedControl c = (AdornedControl)dependencyObject;
            c.ShowOrHideAdornerInternal();
        }

        /// <summary>
        /// Event raised when the IsMouseOverShowEnabled property has changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsMouseOverShowEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            AdornedControl c = (AdornedControl)dependencyObject;
            c._closeAdornerTimer.Stop();
            c.HideAdorner();
        }

        /// <summary>
        /// Event raised when the Show command is executed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private static void OnShowAdornerCommandExecuted(object target, ExecutedRoutedEventArgs e)
        {
            AdornedControl c = (AdornedControl)target;
            c.ShowAdorner();
        }

        #endregion Private Static Methods

        #region Private Methods

        /// <summary>
        /// Internal method to hide the _adorner.
        /// </summary>
        private void HideAdornerInternal()
        {
            if (_adornerLayer == null || _adorner == null)
            {
                // Not already adorned.
                return;
            }

            // Stop the timer that might be about to fade out the _adorner.
            _closeAdornerTimer.Stop();
            _adornerLayer.Remove(_adorner);
            _adorner.DisconnectChild();

            _adorner = null;
            _adornerLayer = null;

            // Ensure that the state of the adorned control reflects that the the _adorner is no longer.
            _adornerShowState = AdornerShowState.Hidden;
        }

        /// <summary>
        /// Shared mouse enter code.
        /// </summary>
        private void MouseEnterLogic()
        {
            if (!IsMouseOverShowEnabled)
            {
                return;
            }

            _closeAdornerTimer.Stop();

            FadeInAdorner();
        }

        /// <summary>
        /// Shared mouse leave code.
        /// </summary>
        private void MouseLeaveLogic()
        {
            if (!IsMouseOverShowEnabled)
            {
                return;
            }

            _closeAdornerTimer.Start();
        }

        /// <summary>
        /// Event raised when the DataContext of the adorned control changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnAdornedControlDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateAdornerDataContext();
        }

        /// <summary>
        /// Event raised when the mouse cursor enters the area of the _adorner.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void OnAdornerContentMouseEnter(object sender, MouseEventArgs e)
        {
            MouseEnterLogic();
        }

        /// <summary>
        /// Event raised when the mouse cursor leaves the area of the _adorner.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void OnAdornerContentMouseLeave(object sender, MouseEventArgs e)
        {
            MouseLeaveLogic();
        }

        /// <summary>
        /// Called when the close _adorner time-out has elapsed, the mouse has moved
        /// away from the adorned control and the _adorner and it is time to close the _adorner.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnCloseAdornerTimerTick(object sender, EventArgs e)
        {
            _closeAdornerTimer.Stop();

            FadeOutAdorner();
        }

        /// <summary>
        /// Event raised when the fade in animation has completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnFadeInAnimationCompleted(object sender, EventArgs e)
        {
            _adornerShowState = AdornerShowState.Visible;
        }

        /// <summary>
        /// Event raised when the fade-out animation has completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FadeOutAnimationCompleted(object sender, EventArgs e)
        {
            if (_adornerShowState == AdornerShowState.FadingOut)
            {
                // Still fading out, eg it wasn't aborted.
                HideAdorner();
            }
        }

        /// <summary>
        /// Internal method to show the _adorner.
        /// </summary>
        private void ShowAdornerInternal()
        {
            if (_adorner != null)
            {
                // Already adorned.
                return;
            }

            if (AdornerContent != null)
            {
                if (_adornerLayer == null)
                {
                    _adornerLayer = AdornerLayer.GetAdornerLayer(this);
                }

                if (_adornerLayer != null)
                {
                    FrameworkElement adornedControl = this; // The control to be adorned defaults to 'this'.

                    if (!string.IsNullOrEmpty(AdornedTemplatePartName))
                    {
                        // If 'AdornedTemplatePartName' is set to a valid string then search the visual-tree
                        // for a UI element that has the specified part name.  If we find it then use it as the
                        // adorned control, otherwise throw an exception.
                        adornedControl = FindNamedChild(this, AdornedTemplatePartName);
                        if (adornedControl == null)
                        {
                            throw new ApplicationException("Failed to find a FrameworkElement in the visual-tree with the part name '" + AdornedTemplatePartName + "'.");
                        }
                    }

                    _adorner = new FrameworkElementAdorner(
                        AdornerContent,
                        adornedControl,
                        HorizontalAdornerPlacement,
                        VerticalAdornerPlacement,
                        AdornerOffsetX,
                        AdornerOffsetY);
                    _adornerLayer.Add(_adorner);

                    UpdateAdornerDataContext();
                }
            }

            _adornerShowState = AdornerShowState.Visible;
        }

        /// <summary>
        /// Internal method to show or hide the _adorner based on the value of IsAdornerVisible.
        /// </summary>
        private void ShowOrHideAdornerInternal()
        {
            if (IsAdornerVisible)
            {
                ShowAdornerInternal();
            }
            else
            {
                HideAdornerInternal();
            }
        }

        /// <summary>
        /// Update the DataContext of the _adorner from the adorned control.
        /// </summary>
        private void UpdateAdornerDataContext()
        {
            if (AdornerContent != null)
            {
                AdornerContent.DataContext = DataContext;
            }
        }

        #endregion Private Methods
    }
}