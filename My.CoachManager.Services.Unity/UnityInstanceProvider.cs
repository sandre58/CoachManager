using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using Unity;

namespace My.CoachManager.Services.Unity
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        private readonly IUnityContainer _container;
        private readonly Type _serviceType;

        public UnityInstanceProvider(IUnityContainer container, Type serviceType)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _serviceType = serviceType;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return _container.Resolve(_serviceType);  // This is it, the one and only call to Unity in the entire solution!
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (instance is IDisposable disposable)
                disposable.Dispose();
        }

        #endregion IInstanceProvider Members
    }
}
