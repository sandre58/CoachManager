using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace My.CoachManager.Presentation.Uwp.Controls
{
    public class ListViewItemControl : ContentControl
    {

        #region AppBarButtons

        public static readonly DependencyProperty AppBarButtonsProperty = DependencyProperty.Register("AppBarButtons", typeof(IList<AppBarButton>), typeof(ListViewItemControl), new PropertyMetadata(new List<AppBarButton>()));
        public IList<AppBarButton> AppBarButtons
        {
            get => (IList<AppBarButton>)GetValue(AppBarButtonsProperty);
            set => SetValue(AppBarButtonsProperty, value);
        }

        #endregion

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);

            // Only show hover buttons when the user is using mouse or pen.
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse || e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
            {
                VisualStateManager.GoToState(this, "HoverButtonsShown", true);
            }
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);

            VisualStateManager.GoToState(this, "HoverButtonsHidden", true);
        }

    }
}
