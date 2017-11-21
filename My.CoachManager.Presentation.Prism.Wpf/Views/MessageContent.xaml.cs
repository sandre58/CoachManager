using My.CoachManager.Presentation.Prism.Wpf.ViewModels;

namespace My.CoachManager.Presentation.Prism.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour DialogWindow.xaml
    /// </summary>
    public partial class MessageContent
    {
        public MessageContent(IMessageViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}