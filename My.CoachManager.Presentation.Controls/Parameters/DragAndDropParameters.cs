using System.Windows.Input;

namespace My.CoachManager.Presentation.Controls.Parameters
{

    using System.Windows;

    /// <summary>
    /// <see cref="DragAndDropParameters"/> attached properties.
    /// </summary>
    public static class DragAndDropParameters
    {
        #region Dependency Properties

        public static readonly DependencyProperty IsDragAndDropEnabledProperty = DependencyProperty.RegisterAttached(
    "IsDragAndDropEnabled",
    typeof(bool),
    typeof(DragAndDropParameters));

        public static readonly DependencyProperty MoveAboveCommandProperty = DependencyProperty.RegisterAttached(
            "MoveAboveCommand",
            typeof(ICommand),
            typeof(DragAndDropParameters),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MoveBelowCommandProperty = DependencyProperty.RegisterAttached(
            "MoveBelowCommand",
            typeof(ICommand),
            typeof(DragAndDropParameters),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MoveDragContentTemplateProperty = DependencyProperty.RegisterAttached(
            "MoveDragContentTemplate",
            typeof(DataTemplate),
            typeof(DragAndDropParameters),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MoveDragFormatProperty = DependencyProperty.RegisterAttached(
            "MoveDragFormat",
            typeof(string),
            typeof(DragAndDropParameters),
            new PropertyMetadata(null));

        #endregion Dependency Properties

        #region Public Static Methods

        /// <summary>
        /// Gets the deselection enabled property. If enabled, and the white space on the grid is clicked, all rows are deselected.
        /// </summary>
        /// <param name="element">The data grid.</param>
        /// <returns><c>true</c> if deselecting all rows when white space is clicked, otherwise <c>false</c>.</returns>
        public static bool GetIsDragAndDropEnabled(DependencyObject element)
        {
            var value = element.GetValue(IsDragAndDropEnabledProperty);
            return value != null && (bool)value;
        }

        /// <summary>
        /// Sets the deselection enabled property. If enabled, and the white space on the grid is clicked, all rows are deselected.
        /// </summary>
        /// <param name="element">The data grid.</param>
        /// <param name="value">if set to <c>true</c> deselect all rows when white space is clicked.</param>
        public static void SetIsDragAndDropEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsDragAndDropEnabledProperty, value);
        }

        /// <summary>
        /// Gets the command used to move another row above this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>The command to move a row above this instance.</returns>
        public static ICommand GetMoveAboveCommand(DependencyObject dataGridRow)
        {
            return (ICommand)dataGridRow.GetValue(MoveAboveCommandProperty);
        }

        /// <summary>
        /// Sets the command used to move another row above this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="command">The command to move a row above this instance.</param>
        public static void SetMoveAboveCommand(DependencyObject dataGridRow, ICommand command)
        {
            dataGridRow.SetValue(MoveAboveCommandProperty, command);
        }

        /// <summary>
        /// Gets the command used to move another row below this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>The command to move a row below this instance.</returns>
        public static ICommand GetMoveBelowCommand(DependencyObject dataGridRow)
        {
            return (ICommand)dataGridRow.GetValue(MoveBelowCommandProperty);
        }

        /// <summary>
        /// Sets the command used to move another row below this one using drag and drop.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="command">The command to move a row below this instance.</param>
        public static void SetMoveBelowCommand(DependencyObject dataGridRow, ICommand command)
        {
            dataGridRow.SetValue(MoveBelowCommandProperty, command);
        }

        /// <summary>
        /// Gets the content template when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>A data template used when this instance is being dragged above or below another row.</returns>
        public static DataTemplate GetMoveDragContentTemplate(DependencyObject dataGridRow)
        {
            return (DataTemplate)dataGridRow.GetValue(MoveDragContentTemplateProperty);
        }

        /// <summary>
        /// Sets the content template when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="value">A data template used when this instance is being dragged above or below another row.</param>
        public static void SetMoveDragContentTemplate(DependencyObject dataGridRow, DataTemplate value)
        {
            dataGridRow.SetValue(MoveDragContentTemplateProperty, value);
        }

        /// <summary>
        /// Gets the drag format when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <returns>The drag format used when this instance is being dragged above or below another row.</returns>
        public static string GetMoveDragFormat(DependencyObject dataGridRow)
        {
            return (string)dataGridRow.GetValue(MoveDragFormatProperty);
        }

        /// <summary>
        /// Sets the drag format when this instance is being dragged above or below another row.
        /// </summary>
        /// <param name="dataGridRow">The data grid row.</param>
        /// <param name="format">The drag format used when this instance is being dragged above or below another row.</param>
        public static void SetMoveDragFormat(DependencyObject dataGridRow, string format)
        {
            dataGridRow.SetValue(MoveDragFormatProperty, format);
        }

        #endregion Public Static Methods

    }
}