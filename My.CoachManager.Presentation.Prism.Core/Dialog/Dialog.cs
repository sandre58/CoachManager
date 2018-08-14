using System.Windows;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Dialog
{
    public class Dialog : IWorkspaceDialog
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the title. (Not used)
        /// </summary>
        public string Title { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        object INotification.Content
        {
            get => Content;
            set => Content = (FrameworkElement)value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public FrameworkElement Content { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public DialogResult Result
        {
            get
            {
                var dialog = Content?.DataContext as IDialogViewModel;

                return dialog?.DialogResult ?? DialogResult.None;
            }
        }

    }
}