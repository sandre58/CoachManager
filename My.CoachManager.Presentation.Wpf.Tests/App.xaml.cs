using System.Windows;
using CommonServiceLocator;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Tests.Services;
using Prism.Events;
using Unity;
using Unity.ServiceLocation;

namespace My.CoachManager.Presentation.Wpf.Tests
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();

            container.RegisterType<IEventAggregator, EventAggregator>();
            container.RegisterInstance(typeof(ILogger), LoggerFactory.CreateLogger());
            container.RegisterType<IDialogService, DialogService>();
            container.RegisterType<INotificationService, NotificationService>();

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
