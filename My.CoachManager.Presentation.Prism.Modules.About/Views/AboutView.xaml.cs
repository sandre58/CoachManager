using My.CoachManager.Presentation.Prism.Modules.About.Core;
using My.CoachManager.Presentation.Prism.Modules.About.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.About.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : IAboutView
    {
        public AboutView(IAboutViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}