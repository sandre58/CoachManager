using System;

using My.CoachManager.Presentation.Uwp.Tests.ViewModels;

using Windows.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Tests.Views
{
    public sealed partial class BlankPage : Page
    {
        private BlankViewModel ViewModel => DataContext as BlankViewModel;

        public BlankPage()
        {
            InitializeComponent();
        }
    }
}
