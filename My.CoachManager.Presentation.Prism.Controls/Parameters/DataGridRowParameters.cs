using My.CoachManager.Presentation.Prism.Core.Interactivity;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using Helpers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// <see cref="DataGridRow"/> attached properties.
    /// </summary>
    public static class DataGridRowParameters
    {
        #region Dependency Properties

        public static readonly DependencyProperty IsDeselectionEnabledProperty = DependencyProperty.RegisterAttached(
            "IsDeselectionEnabled",
            typeof(bool),
            typeof(DataGridRowParameters),
            new PropertyMetadata(false, OnIsDeselectionEnabledChanged));

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.RegisterAttached(
            "IsReadOnly",
            typeof(bool),
            typeof(DataGridRowParameters),
            new PropertyMetadata(false, OnIsReadOnlyChanged));

        public static readonly DependencyProperty MoveAboveCommandProperty = DependencyProperty.RegisterAttached(
            "MoveAboveCommand",
            typeof(ICommand),
            typeof(DataGridRowParameters),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MoveBelowCommandProperty = DependencyProperty.RegisterAttached(
            "MoveBelowCommand",
            typeof(ICommand),
            typeof(DataGridRowParameters),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MoveDragContentTemplateProperty = DependencyProperty.RegisterAttached(
            "MoveDragContentTemplate",
            typeof(DataTemplate),
            typeof(DataGridRowParameters),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MoveDragFormatProperty = DependencyProperty.RegisterAttached(
            "MoveDragFormat",
            typeof(string),
            typeof(DataGridRowParameters),
            new PropertyMetadata(null));

        public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.RegisterAttached(
            "DoubleClickCommand",
            typeof(ICommand),
            typeof(DataGridRowParameters),
            new PropertyMetadata(null, OnMouseDoubleClickCommandChanged));

        public static readonly DependencyProperty KeyDownCommandProperty = DependencyProperty.RegisterAttached(
    "KeyDownCommand",
    typeof(ICommand),
    typeof(DataGridRowParameters),
    new PropertyMetadata(null, OnKeyDownCommandChanged));

        #endregion Dependency Properties

        #region Public Static Methods

        /// <summary>
        /// Gets the edit cells with a single click mode.
        /// By default a cell has to be double clicked to enter editing mode, this property changes this to be a single click.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <returns><c>true</c> if single click mode, otherwise <c>false</c>.</returns>
        public static ICommand GetDoubleClickCommand(DataGridRow dataGrid)
        {
            return (ICommand)dataGrid.GetValue(DoubleClickCommandProperty);
        }

        /// <summary>
        /// Sets the edit cells with a single click mode.
        /// By default a cell has to be double clicked to enter editing mode, this property changes this to be a single click.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="value">if set to <c>true</c> edit cells with a single click.</param>
        public static void SetDoubleClickCommand(DataGridRow dataGrid, ICommand value)
        {
            dataGrid.SetValue(DoubleClickCommandProperty, value);
        }

        /// <summary>
        /// Gets the edit cells with a single click mode.
        /// By default a cell has to be double clicked to enter editing mode, this property changes this to be a single click.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <returns><c>true</c> if single click mode, otherwise <c>false</c>.</returns>
        public static ICommand GetKeyDownCommand(DataGridRow dataGrid)
        {
            return (ICommand)dataGrid.GetValue(KeyDownCommandProperty);
        }

        /// <summary>
        /// Sets the edit cells with a single click mode.
        /// By default a cell has to be double clicked to enter editing mode, this property changes this to be a single click.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="value">if set to <c>true</c> edit cells with a single click.</param>
        public static void SetKeyDownCommand(DataGridRow dataGrid, ICommand value)
        {
            dataGrid.SetValue(KeyDownCommandProperty, value);
        }

        /// <summary>
        /// Gets the deselection enabled property. If enabled, and the row is clicked while selected, the row is deselected.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns><c>true</c> if deselecting row when selected and clicked, otherwise <c>false</c>.</returns>
        public static bool GetIsDeselectionEnabled(DataGridRow dataGridRow)
        {
            return (bool)dataGridRow.GetValue(IsDeselectionEnabledProperty);
        }

        /// <summary>
        /// Sets the deselection enabled property. If enabled, and the row is clicked while selected, the row is deselected.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="value">if set to <c>true</c> deselects the row when selected and clicked.</param>
        public static void SetIsDeselectionEnabled(DataGridRow dataGridRow, bool value)
        {
            dataGridRow.SetValue(IsDeselectionEnabledProperty, value);
        }

        /// <summary>
        /// Gets the is read only flag for the row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns><c>true</c> if the row is read only, otherwise <c>false</c>.</returns>
        public static bool GetIsReadOnly(DataGridRow dataGridRow)
        {
            return (bool)dataGridRow.GetValue(IsReadOnlyProperty);
        }

        /// <summary>
        /// Sets the is read only flag for the row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="value">if set to <c>true</c> the row is read only.</param>
        public static void SetIsReadOnly(DataGridRow dataGridRow, bool value)
        {
            dataGridRow.SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        /// Gets the command used to move another row above this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>The command to move a row above this instance.</returns>
        public static ICommand GetMoveAboveCommand(DataGridRow dataGridRow)
        {
            return (ICommand)dataGridRow.GetValue(MoveAboveCommandProperty);
        }

        /// <summary>
        /// Sets the command used to move another row above this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="command">The command to move a row above this instance.</param>
        public static void SetMoveAboveCommand(DataGridRow dataGridRow, ICommand command)
        {
            dataGridRow.SetValue(MoveAboveCommandProperty, command);
        }

        /// <summary>
        /// Gets the command used to move another row below this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>The command to move a row below this instance.</returns>
        public static ICommand GetMoveBelowCommand(DataGridRow dataGridRow)
        {
            return (ICommand)dataGridRow.GetValue(MoveBelowCommandProperty);
        }

        /// <summary>
        /// Sets the command used to move another row below this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="command">The command to move a row below this instance.</param>
        public static void SetMoveBelowCommand(DataGridRow dataGridRow, ICommand command)
        {
            dataGridRow.SetValue(MoveBelowCommandProperty, command);
        }

        /// <summary>
        /// Gets the content template when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>A data template used when this instance is being dragged above or below another row.</returns>
        public static DataTemplate GetMoveDragContentTemplate(DataGridRow dataGridRow)
        {
            return (DataTemplate)dataGridRow.GetValue(MoveDragContentTemplateProperty);
        }

        /// <summary>
        /// Sets the content template when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="value">A data template used when this instance is being dragged above or below another row.</param>
        public static void SetMoveDragContentTemplate(DataGridRow dataGridRow, DataTemplate value)
        {
            dataGridRow.SetValue(MoveDragContentTemplateProperty, value);
        }

        /// <summary>
        /// Gets the drag format when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>The drag format used when this instance is being dragged above or below another row.</returns>
        public static string GetMoveDragFormat(DataGridRow dataGridRow)
        {
            return (string)dataGridRow.GetValue(MoveDragFormatProperty);
        }

        /// <summary>
        /// Sets the drag format when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="format">The drag format used when this instance is being dragged above or below another row.</param>
        public static void SetMoveDragFormat(DataGridRow dataGridRow, string format)
        {
            dataGridRow.SetValue(MoveDragFormatProperty, format);
        }

        #endregion Public Static Methods

        #region Private Static Methods

        /// <summary>
        /// Called when the deselection enabled property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsDeselectionEnabledChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)dependencyObject;
            if (GetIsDeselectionEnabled(dataGridRow))
            {
                dataGridRow.MouseLeftButtonDown += OnDataGridMouseLeftButtonDown;
            }
            else
            {
                dataGridRow.MouseLeftButtonDown -= OnDataGridMouseLeftButtonDown;
            }
        }

        /// <summary>
        /// Called when the deselection enabled property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnMouseDoubleClickCommandChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)dependencyObject;
            if (GetDoubleClickCommand(dataGridRow) != null)
            {
                dataGridRow.PreviewMouseDoubleClick += OnDataGridPreviewMouseDoubleClick;
            }
            else
            {
                dataGridRow.PreviewMouseDoubleClick -= OnDataGridPreviewMouseDoubleClick;
            }
        }

        private static void OnDataGridPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = sender as DataGridRow;
            var command = GetDoubleClickCommand(dataGridRow);
            if (dataGridRow != null && command != null && command.CanExecute(dataGridRow.Item))
            {
                command.Execute(dataGridRow.Item);
            }
        }

        /// <summary>
        /// Called when the deselection enabled property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnKeyDownCommandChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var dataGridRow = (DataGridRow)dependencyObject;
            if (GetKeyDownCommand(dataGridRow) != null)
            {
                dataGridRow.PreviewKeyDown += OnDataGridPreviewKeyDown;
            }
            else
            {
                dataGridRow.PreviewKeyDown -= OnDataGridPreviewKeyDown;
            }
        }

        private static void OnDataGridPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var dataGridRow = sender as DataGridRow;
            var command = GetKeyDownCommand(dataGridRow);
            if (dataGridRow != null && command != null && command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        /// <summary>
        /// Called when the data grid mouse left button is down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private static void OnDataGridMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = sender as DataGridRow;
            if (dataGridRow != null && dataGridRow.IsSelected)
            {
                dataGridRow.IsSelected = false;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Called when the is read only flag is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsReadOnlyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)dependencyObject;
            DataGrid dataGrid = dataGridRow.FindVisualParent<DataGrid>();

            if (GetIsReadOnly(dataGridRow))
            {
                dataGrid.BeginningEdit += OnDataGridBeginningEdit;
            }
            else
            {
                dataGrid.BeginningEdit -= OnDataGridBeginningEdit;
            }
        }

        /// <summary>
        /// Called when the data grid begins editing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataGridBeginningEditEventArgs"/> instance containing the event data.</param>
        private static void OnDataGridBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (GetIsReadOnly(e.Row))
            {
                e.Cancel = true;
            }
        }

        #endregion Private Static Methods
    }
}