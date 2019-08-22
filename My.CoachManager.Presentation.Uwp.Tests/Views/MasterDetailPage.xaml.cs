using System;

using Microsoft.Toolkit.Uwp.UI.Controls;

using My.CoachManager.Presentation.Uwp.Tests.ViewModels;

using Windows.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Tests.Views
{
    public sealed partial class MasterDetailPage : Page
    {
        private MasterDetailViewModel ViewModel => DataContext as MasterDetailViewModel;

        public MasterDetailPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Workaround for issue on MasterDetail Control. Find More info at https://github.com/Microsoft/WindowsTemplateStudio/issues/2739.
            if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
            {
                ViewModel.SetDefaultSelection();
            }
        }
    }
}
