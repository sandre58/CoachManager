using My.CoachManager.Presentation.Prism.Controls.Buttons;
using My.CoachManager.Presentation.Prism.Controls.CommandButtons;
using My.CoachManager.Presentation.Prism.Controls.Helpers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Prism.Controls
{
    [TemplatePart(Name = PopupName, Type = typeof(Popup))]
    public class DropDownButton : Button
    {
        private const string PopupName = "PART_Popup";

        private Popup _popup;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "We need to use static constructor for custom actions during dependency properties initialization")]
        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
            EventManager.RegisterClassHandler(typeof(DropDownButton), MenuItem.ClickEvent, new RoutedEventHandler(OnMenuItemClick), true);
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DropDownButton), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the dimension of children stacking.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        #region ShowArrow

        public static readonly DependencyProperty ShowArrowProperty = DependencyProperty.Register("ShowArrow", typeof(bool), typeof(DropDownButton), new UIPropertyMetadata(true));

        public bool ShowArrow
        {
            get
            {
                return (bool)GetValue(ShowArrowProperty);
            }
            set
            {
                SetValue(ShowArrowProperty, value);
            }
        }

        #endregion ShowArrow

        #region StayOpen

        public static readonly DependencyProperty StayOpenProperty = DependencyProperty.Register("StayOpen", typeof(bool), typeof(DropDownButton), new UIPropertyMetadata(false));

        public bool StayOpen
        {
            get
            {
                return (bool)GetValue(StayOpenProperty);
            }
            set
            {
                SetValue(StayOpenProperty, value);
            }
        }

        #endregion StayOpen

        #region PopupPlacement

        public static readonly DependencyProperty PopupPlacementProperty = DependencyProperty.Register("PopupPlacement", typeof(PlacementMode), typeof(DropDownButton), new UIPropertyMetadata(PlacementMode.Bottom));

        public PlacementMode PopupPlacement
        {
            get
            {
                return (PlacementMode)GetValue(PopupPlacementProperty);
            }
            set
            {
                SetValue(PopupPlacementProperty, value);
            }
        }

        #endregion PopupPlacement

        public static readonly DependencyProperty SubmenuProperty =
            DependencyProperty.Register("Submenu", typeof(Submenu), typeof(DropDownButton),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSubmenuChanged));

        [Bindable(true)]
        [Category("Content")]
        [Description("Popup menu that drop down.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Submenu Submenu
        {
            get { return (Submenu)GetValue(SubmenuProperty); }
            set { SetValue(SubmenuProperty, value); }
        }

        private static void OnSubmenuChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (DropDownButton)obj;
            instance.OnSubmenuChanged((Submenu)e.OldValue, (Submenu)e.NewValue);
        }

        protected virtual void OnSubmenuChanged(Submenu oldSubmenu, Submenu newSubmenu)
        {
            ApplyTemplate();
            HasSubmenu = newSubmenu != null;
        }

        private static readonly DependencyPropertyKey HasSubmenuPropertyKey =
            DependencyProperty.RegisterReadOnly("HasSubmenu", typeof(bool), typeof(DropDownButton),
                                                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None,
                                                                              OnHasSubmenuChanged));

        public static readonly DependencyProperty HasSubmenuProperty = HasSubmenuPropertyKey.DependencyProperty;

        [Bindable(false)]
        [Browsable(false)]
        public bool HasSubmenu
        {
            get { return (bool)GetValue(HasSubmenuProperty); }
            private set { SetValue(HasSubmenuPropertyKey, value); }
        }

        private static void OnHasSubmenuChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (DropDownButton)obj;
            instance.OnHasSubmenuChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnHasSubmenuChanged(bool oldHasSubmenu, bool newHasSubmenu)
        {
            var peer = UIElementAutomationPeer.FromElement(this) as DropDownButtonAutomationPeer;
            if (peer != null)
            {
                peer.RaiseExpandCollapseStatePropertyChangedEvent(
                    oldHasSubmenu ? IsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed : ExpandCollapseState.LeafNode,
                    newHasSubmenu ? IsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed : ExpandCollapseState.LeafNode);
            }
        }

        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(DropDownButton),
                                        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                      OnIsDropDownOpenChanged, CoerceIsDropDownOpen));

        [Bindable(true)]
        [Category("Appearance")]
        [Description("Indicates whether drop down is open.")]
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        private static void OnIsDropDownOpenChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var instance = (DropDownButton)obj;
            instance.OnIsDropDownOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsDropDownOpenChanged(bool oldIsDropDownOpen, bool newIsDropDownOpen)
        {
            var peer = UIElementAutomationPeer.FromElement(this) as DropDownButtonAutomationPeer;
            if (peer != null)
            {
                if (!HasSubmenu)
                {
                    peer.RaiseExpandCollapseStatePropertyChangedEvent(oldIsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed,
                                                                      newIsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed);
                }
            }

            switch (newIsDropDownOpen)
            {
                case true:
                    VisualStateManager.GoToState(this, "DropDown", true);
                    break;

                case false:
                    VisualStateManager.GoToState(this, "Normal", true);
                    break;
            }
        }

        private static object CoerceIsDropDownOpen(DependencyObject obj, object baseValue)
        {
            var instance = (DropDownButton)obj;
            return instance.CoerceIsDropDownOpen((bool)baseValue);
        }

        private object CoerceIsDropDownOpen(bool baseValue)
        {
            return HasSubmenu && baseValue;
        }

        [Category("Behavior")]
        [Description("Occurs when drop down is opened.")]
        public event EventHandler DropDownOpened;

        protected virtual void OnDropDownOpened(EventArgs e)
        {
            if (DropDownOpened != null)
            {
                DropDownOpened(this, e);
            }
        }

        private void OnDropDownOpened(object sender, EventArgs e)
        {
            if (HasSubmenu)
            {
                // HasSubmenu ensures that Submenu isn't null
                Contract.Assume(Submenu != null);
                Mouse.Capture((IInputElement)TreeHelper.FindTopLevelParent(Submenu), CaptureMode.SubTree);
                OnDropDownOpened(e);
            }
        }

        [Category("Behavior")]
        [Description("Occurs when drop down is closed.")]
        public event EventHandler DropDownClosed;

        protected virtual void OnDropDownClosed(EventArgs e)
        {
            if (DropDownClosed != null)
            {
                DropDownClosed(this, e);
            }
        }

        private void OnDropDownClosed(object sender, EventArgs e)
        {
            if (HasSubmenu)
            {
                Mouse.Capture(null);
                OnDropDownClosed(e);
            }
        }

        public static readonly DependencyProperty DropDownDirectionProperty =
            DependencyProperty.Register("DropDownDirection", typeof(DropDownDirection), typeof(DropDownButton),
                                        new FrameworkPropertyMetadata(DropDownDirection.Up, FrameworkPropertyMetadataOptions.None));

        [Category("Layout")]
        [Description("The direction of the drop down.")]
        public DropDownDirection DropDownDirection
        {
            get { return (DropDownDirection)GetValue(DropDownDirectionProperty); }
            set { SetValue(DropDownDirectionProperty, value); }
        }

        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(DropDownButton),
                                        new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.None));

        [Bindable(true)]
        [Category("Layout")]
        [Description("The maximum height constraint of the drop down.")]
        [TypeConverter(typeof(LengthConverter))]
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new DropDownButtonAutomationPeer(this);
        }

        [SecuritySafeCritical]
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyTemplateInternal();
        }

        [SecurityCritical]
        private void ApplyTemplateInternal()
        {
            if (Template != null)
            {
                if (_popup != null)
                {
                    _popup.Child = null;
                    if (Submenu != null)
                    {
                        var popupRootInstance = TreeHelper.FindTopLevelParent(Submenu);
                        if (popupRootInstance != null)
                        {
                            var presentationFramework = Assembly.GetAssembly(typeof(System.Windows.Window));
                            if (presentationFramework != null)
                            {
                                var popupRoot = presentationFramework.GetType("System.Windows.Controls.Primitives.PopupRoot");
                                if (popupRoot != null)
                                {
                                    var popupRootChild = popupRoot.GetProperty("Child",
                                                                               BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.NonPublic);
                                    if (popupRootChild != null)
                                    {
                                        popupRootChild.SetValue(popupRootInstance, null, null);
                                    }
                                }
                            }
                        }
                    }
                    _popup.Closed -= OnDropDownClosed;
                    _popup.Opened -= OnDropDownOpened;
                    _popup.CustomPopupPlacementCallback = null;
                }

                // Bug in Code Contracts static checker: Template is already checked to null
                Contract.Assume(Template != null);
                _popup = Template.FindName(PopupName, this) as Popup;
                if (_popup == null)
                {
                    Trace.TraceError(PopupName + " not found.");
                }
                else
                {
                    _popup.CustomPopupPlacementCallback = PlacePopup;
                    _popup.Opened += OnDropDownOpened;
                    _popup.Closed += OnDropDownClosed;
                    if (Submenu != null)
                    {
                        _popup.Child = Submenu;
                    }
                }
            }
        }

        protected override void OnClick()
        {
            IsDropDownOpen = !IsDropDownOpen;
            base.OnClick();
        }

        private static void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            var instance = sender as DropDownButton;
            if (instance != null && !instance.StayOpen)
            {
                instance.IsDropDownOpen = false;
            }
        }

        private CustomPopupPlacement[] PlacePopup(Size popupsize, Size targetsize, Point offset)
        {
            var window = System.Windows.Window.GetWindow(this);
            if (window != null)
            {
                var x = (targetsize.Width - Margin.Left + Margin.Right) / 2 - popupsize.Width / 2 + offset.X;
                var y = DropDownDirection == DropDownDirection.Up
                            ? -popupsize.Height - Margin.Top - Margin.Bottom - offset.Y
                            : ActualHeight + Margin.Top + Margin.Bottom + offset.Y;
                var position = new Point(x, y);

                var transformToAncestor = TransformToAncestor(window);

                if (transformToAncestor != null)
                {
                    var relativePosition = transformToAncestor.Transform(position);
                    var selfPosition = TranslatePoint(new Point(0, 0), window);

                    if (relativePosition.X < 0)
                    {
                        relativePosition.X = selfPosition.X;
                    }
                    if (relativePosition.X + popupsize.Width > window.Width)
                    {
                        relativePosition.X = selfPosition.X + ActualWidth - popupsize.Width;
                    }
                    if (DropDownDirection == DropDownDirection.Up && relativePosition.Y < 0)
                    {
                        relativePosition.Y = selfPosition.Y + ActualHeight + Margin.Top + Margin.Bottom;
                    }
                    if (DropDownDirection == DropDownDirection.Down && relativePosition.Y + popupsize.Height > window.Height)
                    {
                        relativePosition.Y = selfPosition.Y - popupsize.Height - Margin.Top - Margin.Bottom;
                    }

                    var transformToDescendant = window.TransformToDescendant(this);
                    position = transformToDescendant.Transform(relativePosition);
                }

                return new[] { new CustomPopupPlacement(position, PopupPrimaryAxis.None) };
            }
            return null;
        }
    }
}