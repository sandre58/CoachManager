using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace My.CoachManager.Presentation.Wpf.Controls.Behaviours
{
    /// <summary>
    /// Behavior that controls the expanders and the main menu in the application
    /// </summary>
    public class FocusBehavior : Behavior<FrameworkElement>
    {
        #region Static Fields

        public static readonly DependencyProperty FocusElementProperty =
            DependencyProperty.Register(
                "FocusElement",
                typeof(FrameworkElement),
                typeof(FocusBehavior),
                new PropertyMetadata(null));

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Indicate if the main menu is displayed or not
        /// </summary>
        /// <remarks>Need a TwoWay binding</remarks>
        public FrameworkElement FocusElement
        {
            get
            {
                return (FrameworkElement)GetValue(FocusElementProperty);
            }

            set
            {
                SetValue(FocusElementProperty, value);
            }
        }

        #endregion Public Properties

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
            base.OnDetaching();
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (FocusElement != null)
            {
                Keyboard.Focus(FocusElement);
                FocusElement.Focus();
            }
        }

        #endregion Methods
    }
}