using System;
using Microsoft.Practices.Unity;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.Core
{
    public static class Locator
    {
        #region Fields

        private static IUnityContainer _container;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        public static INavigationService NavigationService
        {
            get { return GetInstance<INavigationService>(); }
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        public static ILogger Logger
        {
            get { return GetInstance<ILogger>(); }
        }

        /// <summary>
        /// Gets the dialog service.
        /// </summary>
        public static IDialogService DialogService
        {
            get { return GetInstance<IDialogService>(); }
        }

        #endregion Members

        #region Methods

        /// <summary>
        /// Sets the container.
        /// </summary>
        /// <param name="container"></param>
        public static void SetContainer(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>()
        {
            return _container != null ? _container.Resolve<T>() : default(T);
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <returns></returns>
        public static object GetInstance(Type type)
        {
            return _container != null ? _container.Resolve(type) : null;
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static void RegisterType<TInterface, T>(LifetimeManager lifetimeManager = null) where T : TInterface
        {
            if (_container != null)
            {
                var manager = lifetimeManager != null ? lifetimeManager : new ContainerControlledLifetimeManager();
                _container.RegisterType<TInterface, T>(manager);
            }
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RegisterType<T>(LifetimeManager lifetimeManager = null)
        {
            if (_container != null)
            {
                var manager = lifetimeManager != null ? lifetimeManager : new ContainerControlledLifetimeManager();
                _container.RegisterType<T>(manager);
            }
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RegisterInstance<T>(T instance, LifetimeManager lifetimeManager = null)
        {
            if (_container != null)
            {
                var manager = lifetimeManager != null ? lifetimeManager : new ContainerControlledLifetimeManager();
                _container.RegisterInstance(instance, manager);
            }
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RegisterTypeForNavigation<T>(string name = "")
        {
            if (_container != null)
            {
                _container.RegisterTypeForNavigation<T>(name);
            }
        }

        #endregion Methods
    }
}