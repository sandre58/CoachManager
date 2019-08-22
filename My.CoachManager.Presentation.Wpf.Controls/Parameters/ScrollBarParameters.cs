using System.Windows;
using System.Windows.Controls.Primitives;

namespace My.CoachManager.Presentation.Wpf.Controls.Parameters
{
    /// <summary>
    /// <see cref="ScrollBar"/> attached properties.
    /// </summary>
    public static class ScrollBarParameters
    {
        #region Dependency Properties

        public static readonly DependencyProperty ButtonsVisibilityProperty = DependencyProperty.RegisterAttached(
            "ButtonsVisibility",
            typeof(Visibility),
            typeof(ScrollBarParameters),
            new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty BarWidthProperty = DependencyProperty.RegisterAttached(
            "BarWidth",
            typeof(double),
            typeof(ScrollBarParameters));

        #endregion Dependency Properties

        #region Public Static Methods

        public static Visibility GetButtonsVisibility(ScrollBar dataGrid)
        {
            var value = dataGrid.GetValue(ButtonsVisibilityProperty);
            if (value != null)
                return (Visibility)value;
            return default(Visibility);
        }

        public static void SetButtonsVisibility(ScrollBar dataGrid, Visibility value)
        {
            dataGrid.SetValue(ButtonsVisibilityProperty, value);
        }

        public static double GetBarWidth(ScrollBar dataGrid)
        {
            var value = dataGrid.GetValue(BarWidthProperty);
            if (value != null) return (double)value;
            return 0;
        }

        public static void SetBarWidth(ScrollBar dataGrid, double value)
        {
            dataGrid.SetValue(BarWidthProperty, value);
        }

        #endregion Public Static Methods
    }
}