﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Microsoft.Practices.Unity.Configuration;

using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Services.Unity.Configurations;

using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace My.CoachManager.Services.Unity
{
    /// <inheritdoc />
    /// <summary>
    /// Implemented container in Microsoft Practices Unity.
    /// </summary>
    public sealed class IocUnityContainer : UnityContainerExtension
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IocUnityContainer"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public IocUnityContainer(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initial the container with this extension's functionality.
        /// </summary>
        /// <remarks>
        /// When overridden in a derived class, this method will modify the given <see cref="T:Microsoft.Practices.Unity.ExtensionContext" /> by adding
        /// strategies, policies, etc. to install it's functions into the container.
        /// </remarks>
        protected override void Initialize()
        {
            //// CrossCutting Layer
            //// Container.RegisterType<ICacheManager, MemoryCacheManager>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance(_logger, new ContainerControlledLifetimeManager());

            // Get unity configurations sections
            var configurations = UnityConfigurationManager.UnitySections;

            // Load All Container of all configurations.
            LoadContainersConfiguration(configurations);
        }

        #region ----- Methods -----

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

                        Container.LoadConfiguration(configSection, name);
                    }
                    catch (Exception exception)
                    {
                        Trace.TraceError("Container {0} of file {1} is on Error, please check container configuration file : Error = {2}", name, configuration.Key, exception.Message);
                        throw;
                    }
                }
            }
        }

        #endregion ----- Methods -----
    }
}
