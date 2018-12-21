using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class ExtendedGridViewColumn : GridViewColumn
    {

        #region Hidden Columns

        #region CanUserHideColumns

        public static readonly DependencyProperty CanUserHideColumnProperty = DependencyProperty.Register(
            "CanUserHideColumn",
            typeof(bool),
            typeof(ExtendedGridViewColumn),
            new PropertyMetadata(true));

        public bool CanUserHideColumn
        {
            get => (bool)GetValue(CanUserHideColumnProperty);
            set => SetValue(CanUserHideColumnProperty, value);
        }

        #endregion

        #region Visibility

        /// <summary>
        ///     Dependency property for Visibility
        /// </summary>
        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register(
                "Visibility",
                typeof(Visibility),
                typeof(ExtendedGridViewColumn),
                new FrameworkPropertyMetadata(Visibility.Visible, OnVisibilityPropertyChanged));

        /// <summary>
        ///     The property which determines if the column is visible or not.
        /// </summary>
        public Visibility Visibility
        {
            get => (Visibility)GetValue(VisibilityProperty);
            set => SetValue(VisibilityProperty, value);
        }

        /// <summary>
        ///     Property changed callback for Visibility property
        /// </summary>
        private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            var column = d as ExtendedGridViewColumn;
            Visibility oldVisibility = (Visibility)eventArgs.OldValue;
            Visibility newVisibility = (Visibility)eventArgs.NewValue;

            if (oldVisibility != Visibility.Visible && newVisibility != Visibility.Visible)
            {
                return;
            }

            if (newVisibility == Visibility.Visible)
            {
                if (column != null) column.Width = column._internalWidth;
            }
            else
            {
                if (column != null)
                {
                    column._internalWidth = column.Width;
                    column.Width = 0;
                }
            }
        }

        /// <summary>
        ///     Helper IsVisible property
        /// </summary>
        internal bool IsVisible => Visibility == Visibility.Visible;

        private double _internalWidth;

        #endregion

        #region PropertyName

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            "PropertyName",
            typeof(string),
            typeof(ExtendedGridViewColumn),
            new PropertyMetadata(string.Empty));

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        #endregion

        #endregion
    }
}
