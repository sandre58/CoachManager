using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// <see cref="DataGridColumn"/> attached properties
    /// </summary>
    public static class DataGridColumnParameters
    {
        #region CanUserHideColumn

        public static readonly DependencyProperty CanUserHideColumnProperty = DependencyProperty.RegisterAttached(
            "CanUserHideColumn",
            typeof(bool),
            typeof(DataGridColumnParameters),
            new PropertyMetadata(true));

        public static bool GetCanUserHideColumn(DataGridColumn dataGridColumn)
        {
            var value = dataGridColumn.GetValue(CanUserHideColumnProperty);
            return value != null && (bool)value;
        }

        public static void SetCanUserHideColumn(DataGridColumn dataGridColumn, bool value)
        {
            dataGridColumn.SetValue(CanUserHideColumnProperty, value);
        }

        #endregion CanUserHideColumn

        #region Filters

        private static HashSet<string> GetValues(DataGrid datagrid, DataGridColumn dataGridColumn)
        {
            var values = new HashSet<string>();
            var view = CollectionViewSource.GetDefaultView(datagrid.ItemsSource);
            if (view != null)
            {
                foreach (var rowData in datagrid.ItemsSource)
                {
                    var propertyValue = rowData.GetType().GetProperty(dataGridColumn.SortMemberPath);
                    if (propertyValue != null)
                    {
                        var data = propertyValue.GetValue(rowData, null) == null ? null : Convert.ToString(propertyValue.GetValue(rowData, null));
                        if (!values.Contains(data))
                        {
                            values.Add(data);
                        }
                    }
                }
            }

            return values;
        }

        #endregion Filters
    }
}