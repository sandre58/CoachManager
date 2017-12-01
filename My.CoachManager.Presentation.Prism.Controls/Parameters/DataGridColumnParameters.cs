using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// <see cref="DataGridColumn"/> attached properties
    /// </summary>
    public static class DataGridColumnParameters
    {
        #region Dependency Properties

        public static readonly DependencyProperty CanUserHideColumnProperty = DependencyProperty.RegisterAttached(
            "CanUserHideColumn",
            typeof(bool),
            typeof(DataGridColumnParameters),
            new PropertyMetadata(true));

        #endregion Dependency Properties

        #region Public Static Methods

        public static bool GetCanUserHideColumn(DataGridColumn dataGridColumn)
        {
            var value = dataGridColumn.GetValue(CanUserHideColumnProperty);
            return value != null && (bool)value;
        }

        public static void SetCanUserHideColumn(DataGridColumn dataGridColumn, bool value)
        {
            dataGridColumn.SetValue(CanUserHideColumnProperty, value);
        }

        #endregion Public Static Methods
    }
}