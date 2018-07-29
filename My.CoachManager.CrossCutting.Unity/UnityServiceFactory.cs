using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.CrossCutting.Logging.Supervision;

namespace My.CoachManager.CrossCutting.Unity
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
            _container.AddExtension(new IocUnityContainer(new Logger()));

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_container));
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(_container, serviceType, baseAddresses);
        }
    }
}