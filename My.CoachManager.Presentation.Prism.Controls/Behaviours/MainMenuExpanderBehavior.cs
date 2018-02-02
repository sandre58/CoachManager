using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace My.CoachManager.Presentation.Prism.Controls.Behaviours
{
    /// <summary>
    /// Behavior that controls the expanders and the main menu in the application
    /// </summary>
    public class MainMenuExpanderBehavior : Behavior<Expander>
    {
        #region Static Fields

        public static readonly DependencyProperty IsMenuDisplayedProperty =
            DependencyProperty.Register(
                "IsMenuDisplayed",
                typeof(bool),
                typeof(MainMenuExpanderBehavior),
                new PropertyMetadata(default(bool), PropertyChangedCallback));

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Indicate if the main menu is displayed or not
        /// </summary>
        /// <remarks>Need a TwoWay binding</remarks>
        public bool IsMenuDisplayed
        {
            get
            {
                return (bool)GetValue(IsMenuDisplayedProperty);
            }

            set
            {
                SetValue(IsMenuDisplayedProperty, value);
            }
        }

        #endregion Public Properties

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Expanded += AssociatedObjectOnExpanded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Expanded -= AssociatedObjectOnExpanded;
            base.OnDetaching();
        }

        private static void PropertyChangedCallback(
            DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!((bool)dependencyPropertyChangedEventArgs.NewValue))
            {
                var behavior = dependencyObject as MainMenuExpanderBehavior;
                if (behavior != null && behavior.AssociatedObject != null)
                {
                    behavior.AssociatedObject.IsExpanded = false;
                }
            }
        }

        private void AssociatedObjectOnExpanded(object sender, RoutedEventArgs routedEventArgs)
        {
            // Display the main menu when the expander is expanded
            IsMenuDisplayed = true;
            AssociatedObject.BringIntoView();
        }

        #endregion Methods
    }
}