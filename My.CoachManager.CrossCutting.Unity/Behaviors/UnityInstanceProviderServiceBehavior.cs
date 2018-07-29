﻿using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace My.CoachManager.CrossCutting.Unity.Behaviors
{
    public class UnityInstanceProviderServiceBehavior : IServiceBehavior
    {
        private readonly IUnityContainer _container;

        public UnityInstanceProviderServiceBehavior(IUnityContainer container)
        {
            _container = container;
        }

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var cdb in serviceHostBase.ChannelDispatchers)
            {
                if (!(cdb is ChannelDispatcher cd)) continue;
                foreach (EndpointDispatcher endpoint in cd.Endpoints)
                {
                    endpoint.DispatchRuntime.InstanceProvider = new UnityInstanceProvider(_container, serviceDescription.ServiceType);
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion IServiceBehavior Members
    }
}