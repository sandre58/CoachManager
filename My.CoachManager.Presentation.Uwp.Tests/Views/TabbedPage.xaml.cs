using System;

using My.CoachManager.Presentation.Uwp.Tests.ViewModels;

using Windows.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Tests.Views
{
    public sealed partial class TabbedPage : Page
    {
        private TabbedViewModel ViewModel => DataContext as TabbedViewModel;

        public TabbedPage()
        {
            InitializeComponent();
        }
    }
}
