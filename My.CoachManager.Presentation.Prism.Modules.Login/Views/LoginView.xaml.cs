using My.CoachManager.Presentation.Prism.Modules.Login.Core;

namespace My.CoachManager.Presentation.Prism.Modules.Login.Views
{
    /// <summary>
    /// Logique d'interaction pour DialogWindow.xaml
    /// </summary>
    public partial class LoginView : ILoginView
    {
        public LoginView(ILoginViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public ILoginViewModel Model
        {
            get { return DataContext as ILoginViewModel; }
            set { DataContext = value; }
        }
    }
}