using System;
using My.CoachManager.Presentation.Wpf.Controls;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Dialog.Factories;
using My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox;
using My.CoachManager.Presentation.Wpf.Core.Ioc;

namespace My.CoachManager.Presentation.Wpf.Dialog
{
    /// <summary>
    /// Default framework dialog factory that will create instances of standard Windows dialogs.
    /// </summary>
    public class DialogFactory : DefaultDialogFactory
    {
        private readonly IViewModelProvider _viewModelProvider;
        public DialogFactory(IViewModelProvider viewModelProvider)
        {
            _viewModelProvider = viewModelProvider;
        }

        /// <inheritdoc />
        public override IDialogWindow Create(Type dialogType)
            {
                if (dialogType == null) throw new ArgumentNullException(nameof(dialogType));

                var instance = _viewModelProvider.GetView(dialogType);

            switch (instance)
                {
                    case IDialogWindow customDialog:
                        return customDialog;

                    default:
                        var dialogWindow = new DialogWindow
                        {
                            Content = instance
                        };
                        return new DefaultDialogWindowWrapper(dialogWindow);

                }
            }

            /// <inheritdoc />
        public override IMessageBox CreateMessageBox(MessageBoxSettings settings)
        {
            return new MessageBoxWrapper(settings);
        }
    }
}
