using System.Windows;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;

namespace My.CoachManager.Presentation.Prism.Core.Dialog
{
    public class Dialog : IDialog
    {
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
                if (Content is IDialogViewModel)
                {
                    var dialog = (IDialogViewModel)Content;
                    return dialog.DialogResult;
                }
                else
                {
                    var content = Content as FrameworkElement;
                    if (content != null)
                    {
                        var dialog = content.DataContext as IDialogViewModel;
                        if (dialog != null)
                        {
                            return dialog.DialogResult;
                        }
                    }
                }
                return DialogResult.None;
            }
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public IDialogViewModel Context
        {
            get
            {
                if (Content is IDialogViewModel)
                {
                    return (IDialogViewModel)Content;
                }

                var content = Content as FrameworkElement;
                if (content != null)
                {
                    return content.DataContext as IDialogViewModel;
                }
                return null;
            }
        }
    }
}