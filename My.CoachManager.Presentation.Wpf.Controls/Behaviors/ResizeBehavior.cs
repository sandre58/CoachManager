using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace My.CoachManager.Presentation.Wpf.Controls.Behaviors
{
    /// <summary>
    /// Behavior permettant redimensionner une <see cref="FrameworkElement"/> en fonction d'une valeur booléenne.
    /// </summary>
    public class ResizeBehavior : Behavior<FrameworkElement>
    {
        #region IsExpanded 

        /// <summary>
        /// Register the "IsExpanded" attached property and the "OnIsExpanded" callback 
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.RegisterAttached("IsExpanded", typeof(bool), typeof(ResizeBehavior),
                new FrameworkPropertyMetadata(OnIsExpandedChanged));

        /// <summary>
        /// Taille de la colonne lorsque la valeur <see cref="IsExpanded"/> vaut 1l
        /// </summary>
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);

            set => SetValue(IsExpandedProperty, value);
        }

        #endregion

        #region Duration

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            "Duration", typeof(TimeSpan), typeof(ResizeBehavior), new PropertyMetadata(TimeSpan.FromMilliseconds(70)));

        /// <summary>
        /// Taille de la colonne lorsque la valeur <see cref="Duration"/> vaut 1l
        /// </summary>
        public TimeSpan Duration
        {
            get => (TimeSpan)GetValue(DurationProperty);

            set => SetValue(DurationProperty, value);
        }

        #endregion

        #region CollapsedSize

        public static readonly DependencyProperty CollapsedSizeProperty = DependencyProperty.Register(
            "CollapsedSize", typeof(double?), typeof(ResizeBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Taille de la colonne lorsque la valeur <see cref="CollapsedSize"/> vaut 0
        /// </summary>
        public double? CollapsedSize
        {
            get => (double?)GetValue(CollapsedSizeProperty);

            set => SetValue(CollapsedSizeProperty, value);
        }

        #endregion

        #region ExpandedSize

        public static readonly DependencyProperty ExpandedSizeProperty = DependencyProperty.Register(
            "ExpandedSize", typeof(double?), typeof(ResizeBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Taille de la colonne lorsque la valeur <see cref="ExpandedSize"/> vaut 1l
        /// </summary>
        public double? ExpandedSize
        {
            get => (double?)GetValue(ExpandedSizeProperty);

            set => SetValue(ExpandedSizeProperty, value);
        }

        #endregion

        #region Methods

        private static void OnIsExpandedChanged(
            DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is ResizeBehavior toggleBehavior)
            {
                toggleBehavior.OnIsExpandedChanged(e);
            }
        }

        private void OnIsExpandedChanged(DependencyPropertyChangedEventArgs e)
        {
            var expandedWidth = ExpandedSize ?? 0;
            var collapsedWidth = CollapsedSize ?? 0;

            var anim = new DoubleAnimation
            {
                Duration = Duration,
                From = AssociatedObject.ActualWidth,
                To = (bool)e.NewValue ? expandedWidth  :collapsedWidth
            };
            AssociatedObject.BeginAnimation(FrameworkElement.WidthProperty, anim);

        }

        #endregion Methods
    }
}