using System.Windows;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour DialogWindow.xaml
    /// </summary>
    public partial class LoginDialog
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets or gets the <see cref="ILoginDialog"/> shown by this window./>
        /// </summary>
        public INotification Notification
        {
            get
            {
                return DataContext as ILoginDialog;
            }
            set
            {
                DataContext = value;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            OnClose(Core.Interactivity.DialogResult.Ok);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            OnClose(Core.Interactivity.DialogResult.Cancel);
        }

        private void OnClose(DialogResult result)
        {
            var dialog = Notification as IMessageDialog;
            if (dialog != null) dialog.Result = result;
            Close();
        }
    }
}