using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Controls.Datagrids.Columns
{
    /// <summary>
    /// The hyperlink column for the <see cref="DataGrid" />
    /// </summary>
    public sealed class DataGridHyperlinkColumn : System.Windows.Controls.DataGridHyperlinkColumn
    {
        #region Dependency Properties

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(DataGridHyperlinkColumn),
            new PropertyMetadata(null));

        public static readonly DependencyProperty NavigateCommandProperty = DependencyProperty.Register(
            "NavigateCommand",
            typeof(ICommand),
            typeof(DataGridHyperlinkColumn),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToolTipTemplateProperty = DependencyProperty.Register(
            "ToolTipTemplate",
            typeof(DataTemplate),
            typeof(DataGridHyperlinkColumn),
            new PropertyMetadata(null));

        #endregion Dependency Properties

        private BindingBase _tooltip;

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataGridHyperlinkColumn"/> class.
        /// </summary>
        public DataGridHyperlinkColumn()
        {
            EditingElementStyle = (Style)Application.Current.FindResource("DataGridHyperlinkColumnEditingElementStyle");
            Style elementStyle = (Style)Application.Current.FindResource("DataGridHyperlinkColumnElementStyle");
            ElementStyle = new Style(typeof(TextBlock), elementStyle);
            ElementStyle.Setters.Add(
                new EventSetter(Hyperlink.ClickEvent, (RoutedEventHandler)OnHyperlinkClick));
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the navigate command.
        /// </summary>
        /// <value>
        /// The navigate command.
        /// </value>
        public ICommand NavigateCommand
        {
            get { return (ICommand)GetValue(NavigateCommandProperty); }
            set { SetValue(NavigateCommandProperty, value); }
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

        #region Protected Methods

        /// <summary>
        /// Gets an editable <see cref="T:System.Windows.Controls.TextBox" /> element that is bound to the column's <see cref="P:System.Windows.Controls.DataGridHyperlinkColumn.ContentBinding" /> property value.
        /// </summary>
        /// <param name="cell">The cell that will contain the generated element.</param>
        /// <param name="dataItem">The data item represented by the row that contains the intended cell.</param>
        /// <returns>
        /// A new text box control that is bound to the column's <see cref="P:System.Windows.Controls.DataGridHyperlinkColumn.ContentBinding" /> property value.
        /// </returns>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            TextBox textBox = (TextBox)base.GenerateEditingElement(cell, dataItem);
            DataGridColumnToolTipHelper.SetToolTip(textBox, ToolTip, ToolTipTemplate, dataItem);
            return textBox;
        }

        /// <summary>
        /// Gets a read-only <see cref="T:System.Windows.Documents.Hyperlink" /> element that is bound to the column's <see cref="P:System.Windows.Controls.DataGridHyperlinkColumn.ContentBinding" /> property value.
        /// </summary>
        /// <param name="cell">The cell that will contain the generated element.</param>
        /// <param name="dataItem">The data item represented by the row that contains the intended cell.</param>
        /// <returns>
        /// A new, read-only hyperlink element that is bound to the column's <see cref="P:System.Windows.Controls.DataGridHyperlinkColumn.ContentBinding" /> property value.
        /// </returns>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            TextBlock textBlock = (TextBlock)base.GenerateElement(cell, dataItem);
            DataGridColumnToolTipHelper.SetToolTip(
                textBlock,
                ToolTip,
                ToolTipTemplate,
                dataItem,
                "Inlines.FirstInline.NavigateUri");
            return textBlock;
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Called when the hyperlink is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)e.OriginalSource;

            if (Command != null)
            {
                if (Command.CanExecute(hyperlink.DataContext))
                {
                    Command.Execute(hyperlink.DataContext);
                }
            }

            if (NavigateCommand != null)
            {
                if (NavigateCommand.CanExecute(hyperlink.NavigateUri))
                {
                    NavigateCommand.Execute(hyperlink.NavigateUri);
                }
            }
        }

        #endregion Private Methods
    }
}