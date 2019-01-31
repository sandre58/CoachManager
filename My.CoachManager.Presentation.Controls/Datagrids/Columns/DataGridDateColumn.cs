using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using My.CoachManager.Presentation.Controls.Helpers;

namespace My.CoachManager.Presentation.Controls.Datagrids.Columns
{
    /// <summary>
    /// The date picker column for the <see cref="DataGrid" />
    /// </summary>
    public class DataGridDateColumn : DataGridBoundColumn
    {
        #region Dependency Properties

        public static readonly DependencyProperty FormatStringProperty = DependencyProperty.Register(
            "FormatString",
            typeof(string),
            typeof(DataGridDateColumn),
            new PropertyMetadata("dd MMM yyyy"));

        public static readonly DependencyProperty ToolTipTemplateProperty = DependencyProperty.Register(
            "ToolTipTemplate",
            typeof(DataTemplate),
            typeof(DataGridDateColumn),
            new PropertyMetadata(null));

        #endregion Dependency Properties

        #region Fields

        private static Style _defaultEditingElementStyle;
        private static Style _defaultElementStyle;

        private BindingBase _tooltip;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises static members of the <see cref="DataGridDateColumn"/> class.
        /// </summary>
        static DataGridDateColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(DataGridDateColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(DataGridDateColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DataGridDateColumn"/> class.
        /// </summary>
        public DataGridDateColumn()
        {
            EditingElementStyle = (Style)Application.Current.FindResource("DataGridDateColumnEditingElementStyle");
            ElementStyle = (Style)Application.Current.FindResource("DataGridDateColumnElementStyle");
        }

        #endregion Constructors

        #region Public Static Properties

        /// <summary>
        /// Gets the default editing element style.
        /// </summary>
        /// <value>
        /// The default editing element style.
        /// </value>
        public static Style DefaultEditingElementStyle
        {
            get
            {
                if (_defaultEditingElementStyle == null)
                {
                    Style style = new Style(typeof(DatePicker))
                    {
                        Setters = { new Setter(Control.BorderThicknessProperty, new Thickness(0.0)), new Setter(Control.PaddingProperty, new Thickness(0.0)) }
                    };
                    style.Seal();
                    _defaultEditingElementStyle = style;
                }

                return _defaultEditingElementStyle;
            }
        }

        /// <summary>
        /// Gets the default element style.
        /// </summary>
        /// <value>
        /// The default element style.
        /// </value>
        public static Style DefaultElementStyle
        {
            get
            {
                if (_defaultElementStyle == null)
                {
                    Style style = new Style(typeof(TextBlock))
                    {
                        Setters = { new Setter(FrameworkElement.MarginProperty, new Thickness(2.0, 0.0, 2.0, 0.0)) }
                    };
                    style.Seal();
                    _defaultElementStyle = style;
                }

                return _defaultElementStyle;
            }
        }

        #endregion Public Static Properties

        #region Public Properties

        /// <summary>
        /// Gets or sets the date format string.
        /// </summary>
        /// <value>
        /// The date format string.
        /// </value>
        public string FormatString
        {
            get { return (string)GetValue(FormatStringProperty); }
            set { SetValue(FormatStringProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tool tip binding.
        /// </summary>
        /// <value>
        /// The tool tip binding.
        /// </value>
        public BindingBase ToolTip
        {
            get
            {
                return _tooltip;
            }

            set
            {
                _tooltip = value;
                NotifyPropertyChanged("ToolTip");
            }
        }

        /// <summary>
        /// Gets or sets the tool tip data template.
        /// </summary>
        /// <value>
        /// The tool tip data template.
        /// </value>
        public DataTemplate ToolTipTemplate
        {
            get { return (DataTemplate)GetValue(ToolTipTemplateProperty); }
            set { SetValue(ToolTipTemplateProperty, value); }
        }

        #endregion Public Properties

        #region Internal Methods

        /// <summary>
        /// Applies the binding.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        internal void ApplyBinding(DependencyObject target, DependencyProperty property)
        {
            BindingBase binding = Binding;
            if (binding != null)
            {
                BindingBase clone = (BindingBase)XamlHelper.Clone(binding);
                clone.StringFormat = "{0:" + FormatString + "}";
                BindingOperations.SetBinding(target, property, clone);
            }
            else
            {
                BindingOperations.ClearBinding(target, property);
            }
        }

        /// <summary>
        /// Applies the style.
        /// </summary>
        /// <param name="isEditing">if set to <c>true</c> it is editing.</param>
        /// <param name="defaultToElementStyle">if set to <c>true</c> default to the element style.</param>
        /// <param name="element">The element.</param>
        internal void ApplyStyle(bool isEditing, bool defaultToElementStyle, FrameworkElement element)
        {
            Style style = PickStyle(isEditing, defaultToElementStyle);
            if (style != null)
            {
                element.Style = style;
            }
        }

        #endregion Internal Methods

        #region Protected Methods

        /// <summary>
        /// When overridden in a derived class, gets an editing element that is bound to the <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value of the column.
        /// </summary>
        /// <param name="cell">The cell that will contain the generated element.</param>
        /// <param name="dataItem">The data item that is represented by the row that contains the intended cell.</param>
        /// <returns>
        /// A new editing element that is bound to the <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value of the column.
        /// </returns>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var datePicker = new DatePicker();
            ApplyStyle(true, false, datePicker);
            ApplyBinding(datePicker, DatePicker.SelectedDateProperty);
            DataGridColumnToolTipHelper.SetToolTip(datePicker, ToolTip, ToolTipTemplate, dataItem);
            return datePicker;
        }

        /// <summary>
        /// When overridden in a derived class, gets a read-only element that is bound to the <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value of the column.
        /// </summary>
        /// <param name="cell">The cell that will contain the generated element.</param>
        /// <param name="dataItem">The data item that is represented by the row that contains the intended cell.</param>
        /// <returns>
        /// A new read-only element that is bound to the <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value of the column.
        /// </returns>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            TextBlock textBlock = new TextBlock();
            ApplyStyle(false, false, textBlock);
            ApplyBinding(textBlock, TextBlock.TextProperty);
            DataGridColumnToolTipHelper.SetToolTip(textBlock, ToolTip, ToolTipTemplate, dataItem);
            return textBlock;
        }

        /// <summary>
        /// When overridden in a derived class, sets cell content as needed for editing.
        /// </summary>
        /// <param name="editingElement">The element that the column displays for a cell in editing mode.</param>
        /// <param name="editingEventArgs">Information about the user gesture that is causing a cell to enter editing mode.</param>
        /// <returns>
        /// When returned by a derived class, the unedited cell value. This implementation returns null in all cases.
        /// </returns>
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            DatePicker datePicker = editingElement as DatePicker;

            if (datePicker == null)
            {
                return null;
            }

            datePicker.Focus();
            return datePicker.SelectedDate;
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Picks the style based on the specified settings.
        /// </summary>
        /// <param name="isEditing">if set to <c>true</c> it is editing.</param>
        /// <param name="defaultToElementStyle">if set to <c>true</c> default to element style.</param>
        /// <returns>The style with the specified settings.</returns>
        private Style PickStyle(bool isEditing, bool defaultToElementStyle)
        {
            Style elementStyle = isEditing ? EditingElementStyle : ElementStyle;

            if ((isEditing && defaultToElementStyle) && (elementStyle == null))
            {
                elementStyle = ElementStyle;
            }

            return elementStyle;
        }

        #endregion Private Methods
    }
}