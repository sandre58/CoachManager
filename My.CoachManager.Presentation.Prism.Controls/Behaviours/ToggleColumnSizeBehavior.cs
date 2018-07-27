using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace My.CoachManager.Presentation.Prism.Controls.Behaviours
{
    /// <summary>
    /// Behavior permettant redimensionner une <see cref="ColumnDefinition"/> en fonction d'une valeur booléenne.
    /// </summary>
    public class ToggleColumnSizeBehavior : Behavior<ColumnDefinition>
    {
        #region Static Fields

        public static readonly DependencyProperty CollapsedSizeProperty = DependencyProperty.Register(
            "CollapsedSize", typeof(double?), typeof(ToggleColumnSizeBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty ExpandedSizeProperty = DependencyProperty.Register(
            "ExpandedSize", typeof(double?), typeof(ToggleColumnSizeBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty ExpandedStrecthSizeProperty =
            DependencyProperty.Register(
                "ExpandedStrecthSize",
                typeof(bool),
                typeof(ToggleColumnSizeBehavior),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty ToggleValueProperty = DependencyProperty.Register(
            "ToggleValue",
            typeof(bool),
            typeof(ToggleColumnSizeBehavior),
            new PropertyMetadata(default(bool), ToggleValueChanged));

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Taille de la colonne lorsque la valeur <see cref="ToggleValue"/> vaut 0
        /// </summary>
        public double? CollapsedSize
        {
            get
            {
                return (double?)GetValue(CollapsedSizeProperty);
            }

            set
            {
                SetValue(CollapsedSizeProperty, value);
            }
        }

        /// <summary>
        /// Taille de la colonne lorsque la valeur <see cref="ToggleValue"/> vaut 1l
        /// </summary>
        public double? ExpandedSize
        {
            get
            {
                return (double?)GetValue(ExpandedSizeProperty);
            }

            set
            {
                SetValue(ExpandedSizeProperty, value);
            }
        }

        /// <summary>
        /// Indique
        /// </summary>
        public bool ExpandedStrecthSize
        {
            get
            {
                return (bool)GetValue(ExpandedStrecthSizeProperty);
            }

            set
            {
                SetValue(ExpandedStrecthSizeProperty, value);
            }
        }

        /// <summary>
        /// Booléen perrmettant de piloter la taille de la colonne
        /// </summary>
        public bool ToggleValue
        {
            get => (bool)GetValue(ToggleValueProperty);

            set => SetValue(ToggleValueProperty, value);
        }

        #endregion Public Properties

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            if (!ToggleValue)
            {
                OnSetToFalse();
            }
        }

        private static void ToggleValueChanged(
            DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is ToggleColumnSizeBehavior toggleBehavior))
            {
                return;
            }

            var booleanValue = (bool)dependencyPropertyChangedEventArgs.NewValue;
            if (booleanValue)
            {
                toggleBehavior.OnSetToTrue();
            }
            else
            {
                toggleBehavior.OnSetToFalse();
            }
        }

        private void OnSetToFalse()
        {
            if (CollapsedSize.HasValue)
            {
                AssociatedObject.Width = new GridLength(CollapsedSize.Value);
            }
            else
            {
                AssociatedObject.Width = new GridLength(0);
            }
        }

        private void OnSetToTrue()
        {
            if (AssociatedObject == null)
            {
                return;
            }

            if (ExpandedSize.HasValue)
            {
                AssociatedObject.Width = new GridLength(ExpandedSize.Value);
            }
            else if (ExpandedStrecthSize)
            {
                AssociatedObject.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                AssociatedObject.Width = GridLength.Auto;
            }
        }

        #endregion Methods
    }
}