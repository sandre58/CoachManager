using System;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;

    /// <summary>
    /// <see cref="GridView"/> attached properties.
    /// </summary>
    public static class GridViewParameters
    {
        #region Dependency Properties

        public static readonly DependencyProperty CanUserHideColumnsProperty = DependencyProperty.RegisterAttached(
            "CanUserHideColumns",
            typeof(bool),
            typeof(GridViewParameters),
            new PropertyMetadata(true));

        public static readonly DependencyProperty VisibleColumnsProperty = DependencyProperty.RegisterAttached(
            "VisibleColumns",
            typeof(IEnumerable<string>),
            typeof(GridViewParameters),
            new PropertyMetadata(null, OnVisibleColumnsPropertyChanged));

        public static readonly DependencyProperty IsDragAndDropEnabledProperty = DependencyProperty.RegisterAttached(
    "IsDragAndDropEnabled",
    typeof(bool),
    typeof(GridViewParameters));
        
        #endregion Dependency Properties

        #region Public Static Methods

        public static IEnumerable<string> GetVisibleColumns(GridView dataGrid)
        {
            return (IEnumerable<string>)dataGrid.GetValue(VisibleColumnsProperty);
        }

        public static void SetVisibleColumns(GridView dataGrid, IEnumerable<string> value)
        {
            dataGrid.SetValue(VisibleColumnsProperty, value);
        }

        /// <summary>
        /// Gets whether the user can hide columns.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <returns><c>true</c> if the user can hide columns, otherwise <c>false</c>.</returns>
        public static bool GetCanUserHideColumns(GridView dataGrid)
        {
            var value = dataGrid.GetValue(CanUserHideColumnsProperty);
            return value != null && (bool)value;
        }

        /// <summary>
        /// Sets whether the user can hide columns.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="value">if set to <c>true</c> the user can hide columns.</param>
        public static void SetCanUserHideColumns(GridView dataGrid, bool value)
        {
            dataGrid.SetValue(CanUserHideColumnsProperty, value);
        }

        /// <summary>
        /// Gets the deselection enabled property. If enabled, and the white space on the grid is clicked, all rows are deselected.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <returns><c>true</c> if deselecting all rows when white space is clicked, otherwise <c>false</c>.</returns>
        public static bool GetIsDragAndDropEnabled(GridView dataGrid)
        {
            var value = dataGrid.GetValue(IsDragAndDropEnabledProperty);
            return value != null && (bool)value;
        }

        /// <summary>
        /// Sets the deselection enabled property. If enabled, and the white space on the grid is clicked, all rows are deselected.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="value">if set to <c>true</c> deselect all rows when white space is clicked.</param>
        public static void SetIsDragAndDropEnabled(GridView dataGrid, bool value)
        {
            dataGrid.SetValue(IsDragAndDropEnabledProperty, value);
        }
        #endregion Public Static Methods

        #region Private Static Methods

        /// <summary>
        /// Called when the column settings property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnVisibleColumnsPropertyChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            GridView dataGrid = (GridView)dependencyObject;
            UpdateVisibleColumns(dataGrid);
        }
        
        /// <summary>
        /// Updates the columns from column settings.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        private static void UpdateVisibleColumns(GridView dataGrid)
        {
            IEnumerable<string> columns = GetVisibleColumns(dataGrid) != null ? GetVisibleColumns(dataGrid).ToArray() : new string[] { };
            foreach (var column in dataGrid.Columns)
            {
                if (column is ExtendedGridViewColumn extColumn)
                {
                    if (string.IsNullOrEmpty(AutomationProperties.GetName(column)) ||
                        columns.Any(x => string.Equals(x, AutomationProperties.GetName(column),
                            StringComparison.CurrentCultureIgnoreCase)) ||
                        !GridViewColumnParameters.GetCanUserHideColumn(column))
                    {
                        extColumn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        extColumn.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        #endregion Private Static Methods
    }
}