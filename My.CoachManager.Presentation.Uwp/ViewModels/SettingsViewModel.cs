using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using My.CoachManager.Presentation.Uwp.Core.ViewModels;
using My.CoachManager.Presentation.Uwp.Extensions;
using My.CoachManager.Presentation.Uwp.Services;
using Prism.Commands;
using Prism.Windows.Navigation;

namespace My.CoachManager.Presentation.Uwp.ViewModels
{
    public class SettingsViewModel : ScreenViewModel
    {
        public ElementTheme ElementTheme { get; set; } = ThemeSelectorService.Theme;

        public string VersionDescription { get; set; }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                return _switchThemeCommand ?? (_switchThemeCommand = new DelegateCommand<object>(
                           async (param) =>
                           {
                               ElementTheme = (ElementTheme) param;
                               await ThemeSelectorService.SetThemeAsync((ElementTheme) param);
                           }));
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            VersionDescription = GetVersionDescription();
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        protected override Task LoadDataCoreAsync()
        {
            return Task.CompletedTask;
        }
    }
}
