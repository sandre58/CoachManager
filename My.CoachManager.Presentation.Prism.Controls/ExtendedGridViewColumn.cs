using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class ExtendedGridViewColumn : GridViewColumn
    {
        #region Fields

        private double _visibleWidth;

        #endregion

        #region Visibility

        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register("Visibility", typeof(Visibility),
                typeof(ExtendedGridViewColumn),
                new FrameworkPropertyMetadata(Visibility.Visible,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnVisibilityPropertyChanged));

        public Visibility Visibility
        {
            get => (Visibility)GetValue(VisibilityProperty);
            set => SetValue(VisibilityProperty, value);
        }

        private static void OnVisibilityPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is ExtendedGridViewColumn column)
            {
                column.OnVisibilityChanged((Visibility)e.NewValue);
            }
        }

        private void OnVisibilityChanged(Visibility visibility)
        {
            if (visibility == Visibility.Visible)
            {
                Width = _visibleWidth;
            }
            else
            {
                _visibleWidth = Width;
                Width = 0.0;
            }
        }

        #endregion        
    }
}
