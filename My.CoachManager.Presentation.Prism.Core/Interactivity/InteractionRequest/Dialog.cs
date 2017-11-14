using System.Windows;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public class Dialog : IDialog
    {
        private DialogResult _result;

        /// <summary>
        /// Gets or sets the title. (Not used)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return Context != null ? Context.DialogResult : _result;
            }
            set
            {
                _result = value;
                if (Context != null) Context.DialogResult = value;
            }
        }

        /// <summary>
        /// Gets the context of the content.
        /// </summary>
        public IDialogViewModel Context
        {
            get
            {
                var content = Content as FrameworkElement;
                if (content != null) return content.DataContext as IDialogViewModel;
                return null;
            }
        }
    }
}