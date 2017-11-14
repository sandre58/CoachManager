// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class WorkspaceDialog : ContentControl
    {
        //private bool _hideRequest;
        private bool _result = false;

        private bool _saveEnabled;

        public WorkspaceDialog()
        {
            Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register("OverlayBackground", typeof(SolidColorBrush), typeof(WorkspaceDialog));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public SolidColorBrush OverlayBackground
        {
            get { return (SolidColorBrush)GetValue(OverlayBackgroundProperty); }
            set { SetValue(OverlayBackgroundProperty, value); }
        }

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register("Owner", typeof(UIElement), typeof(WorkspaceDialog));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public UIElement Owner
        {
            get { return (UIElement)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        public bool ShowHandlerDialog()
        {
            Visibility = Visibility.Visible;

            if (Owner != null)
            {
                _saveEnabled = Owner.IsEnabled;
                Owner.IsEnabled = false;
            }

            //_hideRequest = false;
            //while (!_hideRequest)
            //{
            //    // HACK: Stop the thread if the application is about to close
            //    if (Dispatcher.HasShutdownStarted ||
            //        Dispatcher.HasShutdownFinished)
            //    {
            //        break;
            //    }

            //    // HACK: Simulate "DoEvents"
            //    Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
            //    Thread.Sleep(10);
            //}

            return _result;
        }

        public void HideHandlerDialog()
        {
            // _hideRequest = true;
            Visibility = Visibility.Hidden;
            if (Owner != null)
            {
                Owner.IsEnabled = _saveEnabled;
            }
        }
    }
}