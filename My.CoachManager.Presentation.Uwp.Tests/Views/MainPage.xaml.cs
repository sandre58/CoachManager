using System;

using My.CoachManager.Presentation.Uwp.Tests.ViewModels;

using Windows.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Tests.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
