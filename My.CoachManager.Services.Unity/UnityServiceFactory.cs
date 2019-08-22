using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

using CommonServiceLocator;

using My.CoachManager.CrossCutting.Logging.Supervision;

using Unity;
using Unity.ServiceLocation;

namespace My.CoachManager.Services.Unity
{
    public class UnityServiceFactory : ServiceHostFactory
    {
        private readonly IUnityContainer _container;

        public UnityServiceFactory() : this(new UnityContainer())
        {
        }

        public UnityServiceFactory(IUnityContainer container)
        {
            _container = container;
            _container.AddExtension(new IocUnityContainer(LoggerFactory.CreateLogger("Services")));

            var locator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(_container, serviceType, baseAddresses);
        }
    }
}
