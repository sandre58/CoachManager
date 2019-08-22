using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Controls.Parameters
{
    public static class NavigationParameters
    {
        #region NavigateTo

        public static string GetNavigateTo(NavigationViewItem item)
        {
            return (string)item.GetValue(NavigateToProperty);
        }

        public static void SetNavigateTo(NavigationViewItem item, string value)
        {
            item.SetValue(NavigateToProperty, value);
        }

        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(string), typeof(NavigationParameters), new PropertyMetadata(null));

        #endregion
    }
}
