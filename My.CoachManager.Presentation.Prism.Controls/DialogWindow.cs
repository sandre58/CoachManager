using System;
using System.Windows;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class DialogWindow : ExtendedWindow
    {
        /// <summary>
        /// The original main window.
        /// </summary>
        private Window _originalMainWindow;

        /// <summary>
        /// The restore main window.
        /// </summary>
        private bool _restoreMainWindow;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Prism.Wpf.Views.MessageDialog" /> class.
        /// </summary>
        public DialogWindow()
        {
            try
            {
                Owner = Application.Current.MainWindow;
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            Topmost = true;

            if (Application.Current == null || Equals(Application.Current.MainWindow, this))
            {
                return;
            }

            _originalMainWindow = Application.Current.MainWindow;
            _restoreMainWindow = true;
            Application.Current.MainWindow = this;

            if (DataContext == null) return;

            ((IDialogViewModel)DataContext).CloseRequest += OnCloseRequest;
        }

        private void OnCloseRequest(object o, EventArgs ev)
        {
            Close();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Deactivated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);

            Topmost = false;

            if (!_restoreMainWindow)
            {
                return;
            }

            Application.Current.MainWindow = _originalMainWindow;
            _originalMainWindow = null;
            _restoreMainWindow = false;

            ((IDialogViewModel)DataContext).CloseRequest -= OnCloseRequest;
        }
    }
}