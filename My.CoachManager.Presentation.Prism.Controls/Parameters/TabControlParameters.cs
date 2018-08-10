using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    using System.Windows;

    /// <summary>
    /// <see cref="TabControl"/> attached properties.
    /// </summary>
    public static class TabControlParameters
    {
        #region Dependency Properties

        public static readonly DependencyProperty IndicatorBrushProperty = DependencyProperty.RegisterAttached(
            "IndicatorBrush",
            typeof(SolidColorBrush),
            typeof(TabControlParameters));

        public static readonly DependencyProperty IndicatorThicknessProperty = DependencyProperty.RegisterAttached(
            "IndicatorThickness",
            typeof(int),
            typeof(TabControlParameters));

        public static readonly DependencyProperty HeaderStyleProperty = DependencyProperty.RegisterAttached(
            "HeaderStyle",
            typeof(Style),
            typeof(TabControlParameters));

        public static readonly DependencyProperty HeaderSizeProperty = DependencyProperty.RegisterAttached(
            "HeaderSize",
            typeof(GridLength),
            typeof(TabControlParameters));

        #endregion Dependency Properties

        #region Public Static Methods

        public static SolidColorBrush GetIndicatorBrush(TabControl tc)
        {
            return (SolidColorBrush)tc.GetValue(IndicatorBrushProperty);
        }

        public static void SetIndicatorBrush(TabControl dataGrid, SolidColorBrush value)
        {
            dataGrid.SetValue(IndicatorBrushProperty, value);
        }

        public static int GetIndicatorThickness(FrameworkElement tc)
        {
            return (int)tc.GetValue(IndicatorThicknessProperty);
        }

        public static void SetIndicatorThickness(FrameworkElement tc, int value)
        {
            tc.SetValue(IndicatorThicknessProperty, value);
        }

        public static Style GetHeaderStyle(TabItem tc)
        {
            return (Style)tc.GetValue(HeaderStyleProperty);
        }

        public static void SetHeaderStyle(TabItem tc, Style value)
        {
            tc.SetValue(HeaderStyleProperty, value);
        }

        public static GridLength GetHeaderSize(TabControl tc)
        {
            return (GridLength)tc.GetValue(HeaderSizeProperty);
        }

        public static void SetHeaderSize(TabControl tc, GridLength value)
        {
            tc.SetValue(HeaderSizeProperty, value);
        }

        #endregion Public Static Methods
    }
}