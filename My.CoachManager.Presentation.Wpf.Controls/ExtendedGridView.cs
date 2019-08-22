using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Wpf.Controls.GridViews.Columns;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public class ExtendedGridView : GridView
    {
        #region CanUserHideColumns

        public static readonly DependencyProperty CanUserHideColumnsProperty = DependencyProperty.Register(
            "CanUserHideColumns",
            typeof(bool),
            typeof(ExtendedGridView),
            new PropertyMetadata(true));

        public bool CanUserHideColumns
        {
            get => (bool)GetValue(CanUserHideColumnsProperty);
            set => SetValue(CanUserHideColumnsProperty, value);
        }

        #endregion

        #region VisibleColumns

        public static readonly DependencyProperty VisibleColumnsProperty = DependencyProperty.Register(
            "VisibleColumns",
            typeof(IEnumerable<string>),
            typeof(ExtendedGridView),
            new PropertyMetadata(OnVisibleColumnsChanged));

        public IEnumerable<string> VisibleColumns
        {
            get => (IEnumerable<string>)GetValue(VisibleColumnsProperty);
            set => SetValue(VisibleColumnsProperty, value);
        }

        private static void OnVisibleColumnsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var dataGrid = dependencyObject as ExtendedGridView;
            var columns = dataGrid?.VisibleColumns?.ToArray() ?? new string[] { };

            if (dataGrid != null)
            {
                foreach (var dataGridColumn in dataGrid.Columns)
                {
                    if (dataGridColumn is ExtendedGridViewColumn column)
                    {
                        if (string.IsNullOrEmpty(column.PropertyName) ||
                            columns.Any(x => string.Equals(x, column.PropertyName,
                                StringComparison.CurrentCultureIgnoreCase)) ||
                            !column.CanUserHideColumn)
                        {
                            column.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            column.Visibility = Visibility.Hidden;
                        }
                    }
                    
                }
            }
        }

        #endregion
    }
}