using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using My.CoachManager.CrossCutting.Unity.Configurations;
using My.CoachManager.CrossCutting.Unity.Exceptions;
using My.CoachManager.CrossCutting.Unity.Resources;

namespace My.CoachManager.CrossCutting.Unity
{
    /// <summary>
    /// Implemented container in Microsoft Practices Unity.
    /// </summary>
    public sealed class IocUnityContainer : IDisposable
    {
        #region ----- Fields -----

        /// <summary>
        /// Unity container.
        /// </summary>
        private readonly IUnityContainer _container;

        #endregion ----- Fields -----

        #region ----- Constructors -----

        /// <summary>
        /// Initializes a new instance of the <see cref="IocUnityContainer"/> class.
        /// </summary>
        public IocUnityContainer()
        {
            // Create root container.
            _container = new UnityContainer();

            // Get unity configurations sections
            var configurations = UnityConfigurationManager.UnitySections;

            // Load All Container of all configurations.
            LoadContainersConfiguration(configurations);
        }

        #endregion ----- Constructors -----

        #region ----- Methods -----

        /// <summary>
        /// Resolve injection dependency.
        /// </summary>
        /// <typeparam name="TService">The service.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The good instance.</returns>
        public TService Resolve<TService>(params ConstructorParameter[] parameters)
        {
            var overrides = new ParameterOverrides();

            foreach (var param in parameters)
            {
                overrides.Add(param.Name, param.Parameter);
            }

            return _container.Resolve<TService>(overrides);
        }

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The good instance.</returns>
        public TService Resolve<TService>(string name, params ConstructorParameter[] parameters)
        {
            var overrides = new ParameterOverrides();

            foreach (var param in parameters)
            {
                overrides.Add(param.Name, param.Parameter);
            }

            return _container.Resolve<TService>(name, overrides);
        }

        /// <summary>
        /// Resolve injection dependency.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The good instance.</returns>
        public object Resolve(Type type, params ConstructorParameter[] parameters)
        {
            var overrides = new ParameterOverrides();

            foreach (var param in parameters)
            {
                overrides.Add(param.Name, param.Parameter);
            }

            return _container.Resolve(type, overrides);
        }

        #endregion ----- Methods -----

        #region ----- IDisposable -----

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> [disposing].</param>
        public void Dispose(Boolean disposing)
        {
            if (!disposing)
            {
                return;
            }

            // Dispose the unity container
            _container.Dispose();
        }

        #endregion ----- IDisposable -----

        #region ----- Private Methods -----

        /// <summary>
        /// Add all container section of all file in Current Container.
        /// </summary>
        /// <param name="configurations">Configurations to load.</param>
        private void LoadContainersConfiguration(IDictionary<string, UnityConfigurationSection> configurations)
        {
            // Add all Files of directory on configuration
            foreach (var configuration in configurations)
            {
                Trace.TraceInformation(string.Concat("Loading Unity File : ", configuration.Key));

                var configSection = configuration.Value;
                var containerNames = configSection.Containers.Select(x => x.Name);

                // Add all Container Section in unity container
                foreach (var name in containerNames)
                {
                    try
                    {
                        Trace.TraceInformation("Load Container {0} of file {1}", name, configuration.Key);

                        _container.LoadConfiguration(configSection, name);
                    }
                    catch (InvalidOperationException exception)
                    {
                        Trace.TraceError("Container {0} of file {1} is on Error, please check container configuration file : Error = {2}", name, configuration.Key, exception.Message);
                        throw;
                    }
                    catch (FileLoadException exception)
                    {
                        Trace.TraceError("Container {0} of file {1} is on Error, please check container configuration file : Error = {2}", name, configuration.Key, exception.Message);
                        throw new UnityException(string.Format(GlobalMessages.LoadConfigurationError, configuration.Key, name), exception);
                    }
                }
            }
        }

        #endregion ----- Private Methods -----
    }
}