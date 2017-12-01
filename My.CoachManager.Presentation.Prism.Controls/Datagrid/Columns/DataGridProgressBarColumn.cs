using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Controls.Datagrid.Columns
{
    /// <summary>
    /// The progress bar column for the <see cref="DataGrid" />
    /// </summary>
    public class DataGridProgressBarColumn : DataGridBoundColumn
    {
        #region Dependency Properties

        public static readonly DependencyProperty ToolTipTemplateProperty = DependencyProperty.Register(
            "ToolTipTemplate",
            typeof(DataTemplate),
            typeof(DataGridProgressBarColumn),
            new PropertyMetadata(null));

        #endregion Dependency Properties

        #region Fields

        private static Style _defaultEditingElementStyle;
        private static Style _defaultElementStyle;

        private BindingBase _largeChangeBinding;
        private BindingBase _maximumBinding;
        private BindingBase _minimumBinding;
        private BindingBase _smallChangeBinding;
        private BindingBase _stateChangeBinding;
        private BindingBase _tooltipBinding;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises static members of the <see cref="DataGridProgressBarColumn"/> class.
        /// </summary>
        static DataGridProgressBarColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(DataGridProgressBarColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(DataGridProgressBarColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DataGridProgressBarColumn"/> class.
        /// </summary>
        public DataGridProgressBarColumn()
        {
            EditingElementStyle = (Style)Application.Current.FindResource("DataGridProgressBarColumnEditingElementStyle");
            ElementStyle = (Style)Application.Current.FindResource("DataGridProgressBarColumnElementStyle");
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
                    Style style = new Style(typeof(MetroProgressBar));
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
                    Style style = new Style(typeof(Slider));
                    style.Seal();
                    _defaultElementStyle = style;
                }

                return _defaultElementStyle;
            }
        }

        #endregion Public Static Properties

        #region Public Properties

        public BindingBase LargeChange
        {
            get
            {
                return _largeChangeBinding;
            }

            set
            {
                _largeChangeBinding = value;
                NotifyPropertyChanged("LargeChange");
            }
        }

        public BindingBase Maximum
        {
            get
            {
                return _maximumBinding;
            }

            set
            {
                _maximumBinding = value;
                NotifyPropertyChanged("Maximum");
            }
        }

        public BindingBase Minimum
        {
            get
            {
                return _minimumBinding;
            }

            set
            {
                _minimumBinding = value;
                NotifyPropertyChanged("Minimum");
            }
        }

        public BindingBase SmallChange
        {
            get
            {
                return _smallChangeBinding;
            }

            set
            {
                _smallChangeBinding = value;
                NotifyPropertyChanged("SmallChange");
            }
        }

        public BindingBase State
        {
            get
            {
                return _stateChangeBinding;
            }

            set
            {
                _stateChangeBinding = value;
                NotifyPropertyChanged("State");
            }
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
                return _tooltipBinding;
            }

            set
            {
                _tooltipBinding = value;
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

        internal void ApplyBinding(DependencyObject target, DependencyProperty property)
        {
            ApplyBinding(Binding, target, property);
        }

        internal void ApplyBinding(BindingBase binding, DependencyObject target, DependencyProperty property)
        {
            if (binding != null)
            {
                BindingOperations.SetBinding(target, property, binding);
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
            Slider slider = new Slider()
            {
                DataContext = dataItem
            };
            ApplyStyle(true, false, slider);
            ApplyBinding(LargeChange, slider, RangeBase.LargeChangeProperty);
            ApplyBinding(Minimum, slider, RangeBase.MinimumProperty);
            ApplyBinding(Maximum, slider, RangeBase.MaximumProperty);
            ApplyBinding(SmallChange, slider, RangeBase.SmallChangeProperty);
            ApplyBinding(slider, RangeBase.ValueProperty);
            DataGridColumnToolTipHelper.SetToolTip(slider, ToolTip, ToolTipTemplate, dataItem);
            return slider;
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
            MetroProgressBar progressBar = new MetroProgressBar()
            {
                DataContext = dataItem
            };
            ApplyStyle(false, false, progressBar);
            ApplyBinding(LargeChange, progressBar, RangeBase.LargeChangeProperty);
            ApplyBinding(Minimum, progressBar, RangeBase.MinimumProperty);
            ApplyBinding(Maximum, progressBar, RangeBase.MaximumProperty);
            ApplyBinding(SmallChange, progressBar, RangeBase.SmallChangeProperty);
            ApplyBinding(progressBar, RangeBase.ValueProperty);
            DataGridColumnToolTipHelper.SetToolTip(cell, ToolTip, ToolTipTemplate, dataItem);
            return progressBar;
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
            Slider datePicker = editingElement as Slider;

            if (datePicker == null)
            {
                return null;
            }

            datePicker.Focus();
            return datePicker.Value;
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