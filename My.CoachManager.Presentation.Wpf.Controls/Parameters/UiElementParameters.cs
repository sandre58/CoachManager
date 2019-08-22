using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Wpf.Controls.Parameters
{
    /// <summary>
    /// <see cref="UIElement"/> attached properties.
    /// </summary>
    public static class UiElementParameters
    {
        #region IsBubblingMouseWheelEvents

        public static readonly DependencyProperty IsBubblingMouseWheelEventsProperty = DependencyProperty.RegisterAttached(
            "IsBubblingMouseWheelEvents",
            typeof(bool),
            typeof(UiElementParameters),
            new PropertyMetadata(false, OnIsBubblingMouseWheelEventsChanged));

        #region Public Static Methods

        /// <summary>
        /// Gets whether the <see cref="UIElement"/> is bubbling mouse wheel events to parent controls.
        /// Useful when you want to put a <see cref="ScrollViewer"/> around a control whose template also contains a <see cref="ScrollViewer"/>,
        /// this usually stops the scroll wheel from working.
        /// </summary>
        /// <param name="uiElement">The UI element.</param>
        /// <returns><c>true</c> if the events are being bubbled, otherwise <c>false</c>.</returns>
        public static bool GetIsBubblingMouseWheelEvents(UIElement uiElement)
        {
            return (bool)uiElement.GetValue(IsBubblingMouseWheelEventsProperty);
        }

        /// <summary>
        /// Sets whether the <see cref="UIElement" /> is bubbling mouse wheel events to parent controls.
        /// Useful when you want to put a <see cref="ScrollViewer" /> around a control whose template also contains a <see cref="ScrollViewer" />,
        /// this usually stops the scroll wheel from working.
        /// </summary>
        /// <param name="uiElement">The UI element.</param>
        /// <param name="value">if set to <c>true</c> <c>true</c> events are being bubbled.</param>
        public static void SetIsBubblingMouseWheelEvents(UIElement uiElement, bool value)
        {
            uiElement.SetValue(IsBubblingMouseWheelEventsProperty, value);
        }

        #endregion Public Static Methods

        #region Private Static Methods

        /// <summary>
        /// Called when the bubbling mouse wheel events property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsBubblingMouseWheelEventsChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            UIElement uiElement = (UIElement)dependencyObject;
            if (GetIsBubblingMouseWheelEvents(uiElement))
            {
                uiElement.AddHandler(UIElement.PreviewMouseWheelEvent, new MouseWheelEventHandler(OnPreviewMouseWheel));
            }
            else
            {
                uiElement.RemoveHandler(UIElement.PreviewMouseWheelEvent, new MouseWheelEventHandler(OnPreviewMouseWheel));
            }
        }

        /// <summary>
        /// Called when previewing mouse wheel events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseWheelEventArgs"/> instance containing the event data.</param>
        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            UIElement uiElement = (UIElement)sender;

            e.Handled = true;
            MouseWheelEventArgs e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            e2.RoutedEvent = UIElement.MouseWheelEvent;
            uiElement.RaiseEvent(e2);
        }

        #endregion Private Static Methods

        #endregion IsBubblingMouseWheelEvents

        #region InpuBindings

        public static readonly DependencyProperty InputBindingsProperty = DependencyProperty.RegisterAttached(
            "InputBindings",
            typeof(InputBindingCollection),
            typeof(UiElementParameters),
            new PropertyMetadata(new InputBindingCollection(), OnInputBindingsChanged));

        #region Public Static Methods

        /// <summary>
        /// Gets whether the <see cref="UIElement"/> is bubbling mouse wheel events to parent controls.
        /// Useful when you want to put a <see cref="ScrollViewer"/> around a control whose template also contains a <see cref="ScrollViewer"/>,
        /// this usually stops the scroll wheel from working.
        /// </summary>
        /// <param name="uiElement">The UI element.</param>
        /// <returns><c>true</c> if the events are being bubbled, otherwise <c>false</c>.</returns>
        public static InputBindingCollection GetInputBindings(UIElement uiElement)
        {
            return (InputBindingCollection)uiElement.GetValue(InputBindingsProperty);
        }

        /// <summary>
        /// Sets whether the <see cref="UIElement" /> is bubbling mouse wheel events to parent controls.
        /// Useful when you want to put a <see cref="ScrollViewer" /> around a control whose template also contains a <see cref="ScrollViewer" />,
        /// this usually stops the scroll wheel from working.
        /// </summary>
        /// <param name="uiElement">The UI element.</param>
        /// <param name="value">if set to <c>true</c> <c>true</c> events are being bubbled.</param>
        public static void SetInputBindings(UIElement uiElement, InputBindingCollection value)
        {
            uiElement.SetValue(InputBindingsProperty, value);
        }

        #endregion Public Static Methods

        #region Private Static Methods

        /// <summary>
        /// Called when the bubbling mouse wheel events property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnInputBindingsChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            if (!(dependencyObject is UIElement element)) return;
            //element.InputBindings.Clear();
            element.InputBindings.AddRange((InputBindingCollection)e.NewValue);
        }

        #endregion Private Static Methods

        #endregion InpuBindings
    }
}