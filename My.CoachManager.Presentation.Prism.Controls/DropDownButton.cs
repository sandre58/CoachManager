using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace My.CoachManager.Presentation.Prism.Controls
{
    [TemplatePart(Name = "PART_Button", Type = typeof(Button)),
     TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    public class DropDownButton : ContentControl
    {
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
                                             typeof(RoutedEventHandler), typeof(DropDownButton));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(DropDownButton), new FrameworkPropertyMetadata(IsExpandedPropertyChangedCallback));

        private static void IsExpandedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DropDownButton dropDownButton = (DropDownButton)dependencyObject;
            dropDownButton.SetPopupPlacementTarget(dropDownButton._popup);
        }

        protected virtual void SetPopupPlacementTarget(Popup popup)
        {
            if (_clickButton != null)
            {
                popup.PlacementTarget = _clickButton;
            }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DropDownButton), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register("ButtonContent", typeof(object), typeof(DropDownButton));
        public static readonly DependencyProperty ArrowVisibilityProperty = DependencyProperty.Register("ArrowVisibility", typeof(Visibility), typeof(DropDownButton), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Indicates whether the Menu is visible.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the dimension of children stacking.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Gets/sets the button style.
        /// </summary>
        public object ButtonContent
        {
            get { return GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        /// <summary>
        /// Gets/sets the visibility of the button arrow icon.
        /// </summary>
        public Visibility ArrowVisibility
        {
            get { return (Visibility)GetValue(ArrowVisibilityProperty); }
            set { SetValue(ArrowVisibilityProperty, value); }
        }

        private Button _clickButton;
        private Popup _popup;

        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            IsExpanded = true;
            e.RoutedEvent = ClickEvent;
            RaiseEvent(e);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _clickButton = EnforceInstance<Button>("PART_Button");
            _popup = EnforceInstance<Popup>("PART_Popup");
            InitializeVisualElementsContainer();
        }

        //Get element from name. If it exist then element instance return, if not, new will be created
        private T EnforceInstance<T>(string partName) where T : FrameworkElement, new()
        {
            T element = GetTemplateChild(partName) as T ?? new T();
            return element;
        }

        private void InitializeVisualElementsContainer()
        {
            _clickButton.Click -= ButtonClick;
            _clickButton.Click += ButtonClick;
        }
    }
}