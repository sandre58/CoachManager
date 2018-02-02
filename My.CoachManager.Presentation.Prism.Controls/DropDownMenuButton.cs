using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Prism.Controls
{
    [ContentProperty("ItemsSource")]
    [TemplatePart(Name = "PART_Button", Type = typeof(Button)),
     TemplatePart(Name = "PART_Menu", Type = typeof(ContextMenu))]
    public class DropDownMenuButton : ItemsControl
    {
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
                                             typeof(RoutedEventHandler), typeof(DropDownMenuButton));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(DropDownMenuButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(IsExpandedPropertyChangedCallback)));

        private static void IsExpandedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DropDownMenuButton dropDownButton = (DropDownMenuButton)dependencyObject;
            dropDownButton.SetContextMenuPlacementTarget(dropDownButton._menu);
        }

        protected virtual void SetContextMenuPlacementTarget(ContextMenu contextMenu)
        {
            if (_clickButton != null)
            {
                contextMenu.PlacementTarget = _clickButton;
            }
        }

        public static readonly DependencyProperty ExtraTagProperty = DependencyProperty.Register("ExtraTag", typeof(object), typeof(DropDownMenuButton));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DropDownMenuButton), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(DropDownMenuButton));
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(DropDownMenuButton));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(DropDownMenuButton));

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(DropDownMenuButton));

        /// <summary>
        /// The DependencyProperty for the ContentTemplate property.
        /// </summary>
        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(DropDownMenuButton), new FrameworkPropertyMetadata((DataTemplate)null));

        /// <summary>
        /// The DependencyProperty for the ContentTemplateSelector property.
        /// </summary>
        public static readonly DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register("ContentTemplateSelector", typeof(DataTemplateSelector), typeof(DropDownMenuButton), new FrameworkPropertyMetadata((DataTemplateSelector)null));

        /// <summary>
        /// The DependencyProperty for the ContentStringFormat property.
        /// </summary>
        public static readonly DependencyProperty ContentStringFormatProperty = DependencyProperty.Register("ContentStringFormat", typeof(string), typeof(DropDownMenuButton), new FrameworkPropertyMetadata((string)null));

        public static readonly DependencyProperty MenuStyleProperty = DependencyProperty.Register("MenuStyle", typeof(Style), typeof(DropDownMenuButton), new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ArrowVisibilityProperty = DependencyProperty.Register("ArrowVisibility", typeof(Visibility), typeof(DropDownMenuButton), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the Content of this control..
        /// </summary>
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// ContentTemplate is the template used to display the content of the control.
        /// </summary>
        [Bindable(true)]
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>
        /// ContentTemplateSelector allows to provide custom logic for choosing the template used to display the content of the control.
        /// </summary>
        /// <remarks>
        /// This property is ignored if <seealso cref="ContentTemplate"/> is set.
        /// </remarks>
        [Bindable(true)]
        public DataTemplateSelector ContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// ContentStringFormat is the format used to display the content of the control as a string
        /// </summary>
        /// <remarks>
        /// This property is ignored if <seealso cref="ContentTemplate"/> is set.
        /// </remarks>
        [Bindable(true)]
        public string ContentStringFormat
        {
            get { return (string)GetValue(ContentStringFormatProperty); }
            set { SetValue(ContentStringFormatProperty, value); }
        }

        /// <summary>
        /// Reflects the parameter to pass to the CommandProperty upon execution.
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the target element on which to fire the command.
        /// </summary>
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        /// <summary>
        /// Get or sets the Command property.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Indicates whether the Menu is visible.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// Gets or sets an extra tag.
        /// </summary>
        public object ExtraTag
        {
            get { return GetValue(ExtraTagProperty); }
            set { SetValue(ExtraTagProperty, value); }
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
        /// Gets/sets the menu style.
        /// </summary>
        public Style MenuStyle
        {
            get { return (Style)GetValue(MenuStyleProperty); }
            set { SetValue(MenuStyleProperty, value); }
        }

        /// <summary>
        /// Gets/sets the visibility of the button arrow icon.
        /// </summary>
        public Visibility ArrowVisibility
        {
            get { return (Visibility)GetValue(ArrowVisibilityProperty); }
            set { SetValue(ArrowVisibilityProperty, value); }
        }

        private System.Windows.Controls.Button _clickButton;
        private ContextMenu _menu;

        static DropDownMenuButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownMenuButton), new FrameworkPropertyMetadata(typeof(DropDownMenuButton)));
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
            _clickButton = EnforceInstance<System.Windows.Controls.Button>("PART_Button");
            _menu = EnforceInstance<ContextMenu>("PART_Menu");
            InitializeVisualElementsContainer();
            if (_menu != null && Items != null && ItemsSource == null)
            {
                foreach (var newItem in Items)
                {
                    TryRemoveVisualFromOldTree(newItem);
                    _menu.Items.Add(newItem);
                }
            }
        }

        private void TryRemoveVisualFromOldTree(object newItem)
        {
            var visual = newItem as Visual;
            if (visual != null)
            {
                var fe = LogicalTreeHelper.GetParent(visual) as FrameworkElement ?? VisualTreeHelper.GetParent(visual) as FrameworkElement;
                if (Equals(this, fe))
                {
                    RemoveLogicalChild(visual);
                    RemoveVisualChild(visual);
                }
            }
        }

        /// <summary>Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.Items" /> property changes.</summary>
        /// <param name="e">Information about the change.</param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (_menu == null || ItemsSource != null || _menu.ItemsSource != null)
            {
                return;
            }
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (var newItem in e.NewItems)
                        {
                            TryRemoveVisualFromOldTree(newItem);
                            _menu.Items.Add(newItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var oldItem in e.OldItems)
                        {
                            _menu.Items.Remove(oldItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldItems != null)
                    {
                        foreach (var oldItem in e.OldItems)
                        {
                            _menu.Items.Remove(oldItem);
                        }
                    }
                    if (e.NewItems != null)
                    {
                        foreach (var newItem in e.NewItems)
                        {
                            TryRemoveVisualFromOldTree(newItem);
                            _menu.Items.Add(newItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    if (Items != null)
                    {
                        _menu.Items.Clear();
                        foreach (var newItem in Items)
                        {
                            TryRemoveVisualFromOldTree(newItem);
                            _menu.Items.Add(newItem);
                        }
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //Get element from name. If it exist then element instance return, if not, new will be created
        private T EnforceInstance<T>(string partName) where T : FrameworkElement, new()
        {
            T element = GetTemplateChild(partName) as T ?? new T();
            return element;
        }

        private void InitializeVisualElementsContainer()
        {
            MouseRightButtonUp -= DropDownButtonMouseRightButtonUp;
            _clickButton.Click -= ButtonClick;
            MouseRightButtonUp += DropDownButtonMouseRightButtonUp;
            _clickButton.Click += ButtonClick;
        }

        private void DropDownButtonMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}