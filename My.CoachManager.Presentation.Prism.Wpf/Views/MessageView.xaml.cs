using My.CoachManager.Presentation.Prism.Wpf.ViewModels;

namespace My.CoachManager.Presentation.Prism.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour DialogWindow.xaml
    /// </summary>
    public partial class MessageView
    {
        public MessageView(IMessageViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}