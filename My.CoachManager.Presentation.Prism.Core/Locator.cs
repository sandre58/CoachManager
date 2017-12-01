﻿using Microsoft.Practices.Unity;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.Core
{
    public static class Locator
    {
        #region Fields

        private static IUnityContainer _container;

        #endregion Fields

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
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static void RegisterType<TInterface, T>() where T : TInterface
        {
            if (_container != null)
            {
                _container.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
            }
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RegisterType<T>()
        {
            if (_container != null)
            {
                _container.RegisterType<T>(new ContainerControlledLifetimeManager());
            }
        }

        /// <summary>
        /// Resolve an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RegisterInstance<T>(T instance)
        {
            if (_container != null)
            {
                _container.RegisterInstance(instance, new ContainerControlledLifetimeManager());
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