namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// <see cref="GridViewColumn"/> attached properties
    /// </summary>
    public static class GridViewColumnParameters
    {
        #region CanUserHideColumn

        public static readonly DependencyProperty CanUserHideColumnProperty = DependencyProperty.RegisterAttached(
            "CanUserHideColumn",
            typeof(bool),
            typeof(GridViewColumnParameters),
            new PropertyMetadata(true));

        public static bool GetCanUserHideColumn(GridViewColumn dataGridColumn)
        {
            var value = dataGridColumn.GetValue(CanUserHideColumnProperty);
            return value != null && (bool)value;
        }

        public static void SetCanUserHideColumn(GridViewColumn dataGridColumn, bool value)
        {
            dataGridColumn.SetValue(CanUserHideColumnProperty, value);
        }

        #endregion CanUserHideColumn

    }
}