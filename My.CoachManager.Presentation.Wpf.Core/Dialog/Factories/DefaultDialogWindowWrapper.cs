using System;
using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.Factories
{
    /// <summary>
    /// Class wrapping an instance of <see cref="Window"/> in <see cref="IDialogWindow"/>.
    /// </summary>
    /// <seealso cref="IDialogWindow" />
    public class DefaultDialogWindowWrapper : IDialogWindow
    {
        private readonly Window _window;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDialogWindowWrapper"/> class.
        /// </summary>
        /// <param name="window">The window.</param>
        public DefaultDialogWindowWrapper(Window window)
        {
            _window = window ?? throw new ArgumentNullException(nameof(window));
        }

        /// <inheritdoc />
        public object DataContext
        {
            get => _window.DataContext;
            set => _window.DataContext = value;
        }

        /// <inheritdoc />
        public string Title
        {
            get => _window.Title;
            set => _window.Title = value;
        }

        /// <inheritdoc />
        public bool? DialogResult
        {
            get => _window.DialogResult;
            set => _window.DialogResult = value;
        }

        /// <inheritdoc />
        public bool? ShowDialog()
        {
            return _window.ShowDialog();
        }

        /// <inheritdoc />
        public void Show()
        {
            _window.Show();
        }
    }
}
