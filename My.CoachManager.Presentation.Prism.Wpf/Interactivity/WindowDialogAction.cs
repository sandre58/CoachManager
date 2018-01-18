using System;
using System.Windows;
using System.Windows.Interactivity;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Modules.Login.Core;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Wpf.Interactivity
{
    /// <summary>
    /// Shows a popup window in response to an <see cref="InteractionRequest{T}"/> being raised.
    /// </summary>
    public class WindowDialogAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Determines if the content should be shown in a modal window or not.
        /// </summary>
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register(
                "IsModal",
                typeof(bool),
                typeof(WindowDialogAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Determines if the content should be initially shown centered over the view that raised the interaction request or not.
        /// </summary>
        public static readonly DependencyProperty CenterOverAssociatedObjectProperty =
            DependencyProperty.Register(
                "CenterOverAssociatedObject",
                typeof(bool),
                typeof(WindowDialogAction),
                new PropertyMetadata(null));

        /// <summary>
        /// If set, applies this WindowStartupLocation to the child window.
        /// </summary>
        public static readonly DependencyProperty WindowStartupLocationProperty =
            DependencyProperty.Register(
                "WindowStartupLocation",
                typeof(WindowStartupLocation?),
                typeof(WindowDialogAction),
                new PropertyMetadata(null));

        /// <summary>
        /// If set, applies this Style to the child window.
        /// </summary>
        public static readonly DependencyProperty StyleProperty =
            DependencyProperty.Register(
                "Style",
                typeof(Style),
                typeof(WindowDialogAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets if the window will be modal or not.
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        /// <summary>
        /// Gets or sets if the window will be initially shown centered over the view that raised the interaction request or not.
        /// </summary>
        public bool CenterOverAssociatedObject
        {
            get { return (bool)GetValue(CenterOverAssociatedObjectProperty); }
            set { SetValue(CenterOverAssociatedObjectProperty, value); }
        }

        /// <summary>
        /// Gets or sets the startup location of the Window.
        /// </summary>
        public WindowStartupLocation? WindowStartupLocation
        {
            get { return (WindowStartupLocation?)GetValue(WindowStartupLocationProperty); }
            set { SetValue(WindowStartupLocationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Style of the Window.
        /// </summary>
        public Style Style
        {
            get { return (Style)GetValue(StyleProperty); }
            set { SetValue(StyleProperty, value); }
        }

        /// <summary>
        /// Displays the child window and collects results for <see cref="IInteractionRequest"/>.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            var wrapperWindow = GetWindow(args.Context as IDialog);

            // We invoke the callback when the interaction's window is closed.
            var callback = args.Callback;
            EventHandler handler = null;
            handler =
                (o, e) =>
                {
                    wrapperWindow.Closed -= handler;
                    wrapperWindow.Content = null;
                    if (callback != null) callback();
                };
            wrapperWindow.Closed += handler;

            if (CenterOverAssociatedObject && AssociatedObject != null)
            {
                // If we should center the popup over the parent window we subscribe to the SizeChanged event
                // so we can change its position after the dimensions are set.
                SizeChangedEventHandler sizeHandler = null;
                sizeHandler =
                    (o, e) =>
                    {
                        wrapperWindow.SizeChanged -= sizeHandler;

                        // If the parent window has been minimized, then the poition of the wrapperWindow is calculated to be off screen
                        // which makes it impossible to activate and bring into view.  So, we want to check to see if the parent window
                        // is minimized and automatically set the position of the wrapperWindow to be center screen.
                        var parentWindow = wrapperWindow.Owner;
                        if (parentWindow != null && parentWindow.WindowState == WindowState.Minimized)
                        {
                            wrapperWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                            return;
                        }

                        FrameworkElement view = AssociatedObject;

                        // Position is the top left position of the view from which the request was initiated.
                        // On multiple monitors, if the X or Y coordinate is negative it represent that the monitor from which
                        // the request was initiated is either on the left or above the PrimaryScreen
                        Point position = view.PointToScreen(new Point(0, 0));
                        PresentationSource source = PresentationSource.FromVisual(view);
                        if (source != null)
                            if (source.CompositionTarget != null)
                                position = source.CompositionTarget.TransformFromDevice.Transform(position);

                        // Find the middle of the calling view.
                        // Take the width and height of the view divided by 2 and add to the X and Y coordinates.
                        var middleOfView = new Point(position.X + (view.ActualWidth / 2),
                                                     position.Y + (view.ActualHeight / 2));

                        // Set the coordinates for the top left part of the wrapperWindow.
                        // Take the width of the wrapperWindow, divide it by 2 and substract it from
                        // the X coordinate of middleOfView. Do the same thing for the Y coordinate.
                        // If the wrapper window is wider or taller than the view, it will be behind the view.
                        wrapperWindow.Left = middleOfView.X - (wrapperWindow.ActualWidth / 2);
                        wrapperWindow.Top = middleOfView.Y - (wrapperWindow.ActualHeight / 2);
                    };
                wrapperWindow.SizeChanged += sizeHandler;
            }

            if (IsModal)
            {
                wrapperWindow.ShowDialog();
            }
            else
            {
                wrapperWindow.Show();
            }
        }

        /// <summary>
        /// Returns the window to display as part of the trigger action.
        /// </summary>
        /// <param name="dialog">The notification to be set as a DataContext in the window.</param>
        /// <returns></returns>
        protected virtual Window GetWindow(IDialog dialog)
        {
            var wrapperWindow = CreateWindow();

            if (wrapperWindow == null)
                throw new NullReferenceException("CreateWindow cannot return null");

            if (dialog != null)
            {
                wrapperWindow.DataContext = dialog;
                wrapperWindow.Content = PrepareContent(dialog);
                wrapperWindow.Title = dialog.Title;

                if (dialog.Context != null)
                {
                    dialog.Context.CloseRequest += (sender, e) => wrapperWindow.Close();
                }
            }

            try
            {
                if (AssociatedObject != null)
                    wrapperWindow.Owner = Window.GetWindow(AssociatedObject);
            }
            catch (Exception)
            {
                // ignored
            }

            // If the user provided a Style for a Window we set it as the window's style.
            if (Style != null)
                wrapperWindow.Style = Style;

            // If the user has provided a startup location for a Window we set it as the window's startup location.
            if (WindowStartupLocation.HasValue)
                wrapperWindow.WindowStartupLocation = WindowStartupLocation.Value;

            return wrapperWindow;
        }

        /// <summary>
        /// Creates a Window that is used when providing custom Window Content
        /// </summary>
        /// <returns>The Window</returns>
        protected virtual Window CreateWindow()
        {
            return new WindowDialog();
        }

        /// <summary>
        /// Creates a Window that is used when providing custom Window Content
        /// </summary>
        /// <returns>The Window</returns>
        protected virtual object PrepareContent(IDialog dialog)
        {
            object content = null;

            if (dialog.Context != null)
            {
                if (dialog.Context is IMessageViewModel)
                {
                    content = new MessageContent((IMessageViewModel)dialog.Context);
                }
                else if (dialog.Context is ILoginViewModel)
                {
                    var view = Locator.GetInstance<ILoginView>();
                    view.Model = (ILoginViewModel)dialog.Context;
                    content = view;
                }
                else
                {
                    content = dialog.Content;
                }
            }

            return content;
        }
    }
}