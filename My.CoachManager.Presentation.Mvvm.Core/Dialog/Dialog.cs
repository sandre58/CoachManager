using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.Dialog
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
            set => Content = (IDialogViewModel)value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public IDialogViewModel Content { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public DialogResult Result => Content?.DialogResult ?? DialogResult.None;
    }
}