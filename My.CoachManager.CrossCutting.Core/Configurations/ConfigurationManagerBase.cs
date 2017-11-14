using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using My.CoachManager.CrossCutting.Core.Helpers;

namespace My.CoachManager.CrossCutting.Core.Configurations
{
    /// <summary>
    /// Base Class For Configuration management.
    /// </summary>
    public abstract class ConfigurationManagerBase
    {
        #region ----- Constants-----

        /// <summary>
        /// The configuration tag name.
        /// </summary>
        private const string ConfigurationTagName = "configuration";

        /// <summary>
        /// The namespace for configuration file transformation.
        /// </summary>
        private const string ConfigTransformNamespace = "http://schemas.microsoft.com/XML-Document-Transform";

        #endregion ----- Constants-----

        #region ----- Fields -----

        /// <summary>
        /// The Container.
        /// </summary>
        private static readonly Dictionary<string, Configuration> Container = new Dictionary<string, Configuration>();

        #endregion ----- Fields -----

        #region ----- Constructors -----

        /// <summary>
        /// Initializes static members of the ConfigurationManagerBase class.
        /// </summary>
        static ConfigurationManagerBase()
        {
            Initialize();
        }

        #endregion ----- Constructors -----

        #region ----- Protected Methods -----

        /// <summary>
        /// Get an AppSetting Key from Global app.config store in the container.
        /// </summary>
        /// <typeparam name="T">Type of Value Key.</typeparam>
        /// <param name="appSettingKeyName">Key Name.</param>
        /// <returns>Generic Type.</returns>
        /// <exception cref="ArgumentException">Throw when an argument exception occurs.</exception>
        /// <exception cref="ConfigurationErrorsException">
        /// Throw when a configuration exception occurs.
        /// </exception>
        protected static T GetAppSettingKey<T>(string appSettingKeyName)
        {
            return GetAppSettingFromConfiguration<T>(appSettingKeyName);
        }

        /// <summary>
        /// Set an AppSetting Key.
        /// </summary>
        /// <typeparam name="T">Type of the Key.</typeparam>
        /// <param name="appSettingKeyName">Name of the Key.</param>
        /// <param name="value">Value to set.</param>
        protected static void SetAppSettingKey<T>(string appSettingKeyName, T value)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(appSettingKeyName))
            {
                SetAppSettingFromConfiguration(appSettingKeyName, value);
            }

            ConfigurationManager.AppSettings.Set(appSettingKeyName, value.ToString());
        }

        /// <summary>
        /// Get Connection String from Global app.config store in the container.
        /// </summary>
        /// <param name="connectionStringName">Name of Connection String.</param>
        /// <returns>Connection String.</returns>
        protected static string GetConnectionString(string connectionStringName)
        {
            return GetConnectionStringFromConfiguration(connectionStringName);
        }

        /// <summary>
        /// Get ConfigSection from Global app.config store in the container.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration section.</typeparam>
        /// <param name="configSectionName">Name of configuration section.</param>
        /// <returns>Instance of Configuration Section.</returns>
        protected static TConfig GetConfigSection<TConfig>(string configSectionName)
            where TConfig : ConfigurationSection
        {
            return GetConfigSectionFromConfiguration<TConfig>(configSectionName);
        }

        /// <summary>
        /// Get ConfigSection from Global app.config store in the container.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration section.</typeparam>
        /// <param name="configSectionName">Name of configuration section.</param>
        /// <returns>Instance of Configuration Section.</returns>
        protected static IDictionary<string, TConfig> GetAllConfigSections<TConfig>(string configSectionName)
            where TConfig : ConfigurationSection
        {
            return GetAllConfigSectionFromConfiguration<TConfig>(configSectionName);
        }

        /// <summary>
        /// Loads the given configuration file and adds it to the container.
        /// </summary>
        /// <param name="key">The instance name used as a key for the dictionary.</param>
        /// <param name="fullPath">The full path of the configuration file.</param>
        protected static void LoadConfigurationFromFile(string key, string fullPath)
        {
            ParameterHelper.Requires<NullReferenceException>(
                !string.IsNullOrWhiteSpace(key),
                "Could not load the configuration with null Key.");
            ParameterHelper.Requires<NullReferenceException>(
                !string.IsNullOrWhiteSpace(fullPath),
                "Could not load the configuration with null full Path.");

            if (!Container.Keys.Contains(key))
            {
                var configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = fullPath };

                // Get the mapped configuration file.
                var mappedConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None, false);
                if (key != null) Container.Add(key, mappedConfig);

                Trace.TraceInformation("Configuration file '{0}' is loaded succesfully", fullPath);
            }
        }

        #endregion ----- Protected Methods -----

        #region ----- Private Methods -----

        /// <summary>
        /// Initializes the configurations container.
        /// </summary>
        protected static void Initialize()
        {
            // Get all *.config file from Base Directory and its sub directories
            string applicationPath = AppDomain.CurrentDomain.BaseDirectory;

            var configurationsFiles = Directory.EnumerateFiles(applicationPath, "*.config", SearchOption.AllDirectories);

            foreach (var configurationFilePath in configurationsFiles)
            {
                // check if file is configuration file
                var doc = XDocument.Load(configurationFilePath);
                if (doc.Root != null && doc.Root.Name.ToString().ToLower() != ConfigurationTagName)
                {
                    continue;
                }

                // If transform configuration file => debug mode => ignore
                if (doc.Root != null && doc.Root.GetPrefixOfNamespace(ConfigTransformNamespace) != null)
                {
                    continue;
                }

                // Load configuration from configuration file path
                LoadConfigurationFromFile(configurationFilePath.Substring(applicationPath.Length), configurationFilePath);
            }
        }

        /// <summary>
        /// Gets a value indicating whether a configuration container is good AppSetting
        /// configuration file for given key.
        /// </summary>
        /// <typeparam name="T">Type of the AppSetting key value.</typeparam>
        /// <param name="container">Configuration container to test.</param>
        /// <param name="appSettingKeyName">AppSetting key to search for.</param>
        /// <param name="tryParseValue">
        /// Value indicating whether to try parse value to check if configuration is valid.
        /// </param>
        /// <returns>
        /// Value indicating whether a configuration container is good AppSetting configuration file
        /// for given key.
        /// </returns>
        private static bool IsGoodAppSettingConfiguration<T>(KeyValuePair<string, Configuration> container, string appSettingKeyName, bool tryParseValue = true)
        {
            return IsGoodAppSettingConfiguration<T>(container, appSettingKeyName, false, tryParseValue);
        }

        /// <summary>
        /// Gets a value indicating whether a configuration container is good AppSetting
        /// configuration file for given key.
        /// </summary>
        /// <typeparam name="T">Type of the AppSetting key value.</typeparam>
        /// <param name="container">Configuration container to test.</param>
        /// <param name="appSettingKeyName">AppSetting key to search for.</param>
        /// <param name="canWrite">Value indicating whether AppSetting value is read only.</param>
        /// <param name="tryParseValue">
        /// Value indicating whether to try parse value to check if configuration is valid.
        /// </param>
        /// <returns>
        /// Value indicating whether a configuration container is good AppSetting configuration file
        /// for given key.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We just want to check if conversion is valid")]
        private static bool IsGoodAppSettingConfiguration<T>(KeyValuePair<string, Configuration> container, string appSettingKeyName, bool canWrite, bool tryParseValue)
        {
            if (!container.Value.AppSettings.Settings.AllKeys.Contains(appSettingKeyName))
            {
                return false;
            }

            // If canWrite => check if AppSetting is readonly
            if (canWrite && container.Value.AppSettings.Settings[appSettingKeyName].IsReadOnly())
            {
                return false;
            }

            // If we don't care of try parse value => return true
            if (!tryParseValue)
            {
                return true;
            }

            // Try parse value
            T result;
            try
            {
                result = (T)Convert.ChangeType(container.Value.AppSettings.Settings[appSettingKeyName].Value, typeof(T), CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                // Parsing value is not good => configuration file AppSetting is not good as well
                Trace.TraceError("Parsing value is not good => configuration file AppSetting is not good as well : {0}", exception.Message);
                return false;
            }

            return result != null;
        }

        /// <summary>
        /// Gets the first AppSetting value for given key in all configuration containers.
        /// </summary>
        /// <typeparam name="T">Type of the AppSetting key value.</typeparam>
        /// <param name="appSettingKeyName">AppSetting key to search for.</param>
        /// <returns>AppSetting value for the given key.</returns>
        private static T GetAppSettingFromConfiguration<T>(string appSettingKeyName)
        {
            ParameterHelper.Requires<NullReferenceException>(
                !string.IsNullOrWhiteSpace(appSettingKeyName),
                "Could not load the configuration with null appSettingKeyName.");

            // check if exists container containing the app setting key
            if (!Container.Any(container => IsGoodAppSettingConfiguration<T>(container, appSettingKeyName)))
            {
                Trace.TraceError("AppSetting key=\"{0}\" is not found", appSettingKeyName);
                throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture, "AppSetting key=\"{0}\" is not found", appSettingKeyName));
            }

            // get the app setting key value
            var configuration = Container.First(container => IsGoodAppSettingConfiguration<T>(container, appSettingKeyName)).Value;
            if (appSettingKeyName != null)
                return (T)Convert.ChangeType(configuration.AppSettings.Settings[appSettingKeyName].Value, typeof(T), CultureInfo.InvariantCulture);

            return (T)Convert.ChangeType("", typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Sets the first AppSetting value for given key in all configuration containers.
        /// </summary>
        /// <typeparam name="T">Type of the AppSetting key value.</typeparam>
        /// <param name="appSettingKeyName">AppSetting key to search for.</param>
        /// <param name="value">Value to set for AppSetting key.</param>
        private static void SetAppSettingFromConfiguration<T>(string appSettingKeyName, T value)
        {
            ParameterHelper.Requires<NullReferenceException>(
                !string.IsNullOrWhiteSpace(appSettingKeyName),
                "Could not load the configuration with null appSettingKeyName.");

            // Gets the first container defining
            bool existsExactConfiguration = Container.Any(container => IsGoodAppSettingConfiguration<T>(container, appSettingKeyName, true, true));
            bool existsConfiguration = existsExactConfiguration ? existsExactConfiguration : Container.Any(container => IsGoodAppSettingConfiguration<T>(container, appSettingKeyName, true, false));

            if (!existsConfiguration)
            {
                Trace.TraceError("AppSetting key=\"{0}\" is not found", appSettingKeyName);
                throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture, "AppSetting key=\"{0}\" is not found", appSettingKeyName));
            }

            var configuration = Container.First(container => IsGoodAppSettingConfiguration<T>(container, appSettingKeyName, true, existsExactConfiguration)).Value;
            if (appSettingKeyName != null)
                configuration.AppSettings.Settings[appSettingKeyName].Value = value.ToString();
            configuration.Save(ConfigurationSaveMode.Minimal);
        }

        /// <summary>
        /// Gets a value indicating whether a configuration container is good connection string
        /// configuration file for connection string name.
        /// </summary>
        /// <param name="container">Configuration container to test.</param>
        /// <param name="connectionStringName">Connection string name to search for.</param>
        /// <returns>
        /// Value indicating whether a configuration container is good connection string
        /// configuration file for given key.
        /// </returns>
        private static bool IsGoodConnectionStringConfiguration(KeyValuePair<string, Configuration> container, string connectionStringName)
        {
            return container.Value.ConnectionStrings.ConnectionStrings[connectionStringName] != null;
        }

        /// <summary>
        /// Reads the connection string.
        /// </summary>
        /// <param name="connectionStringName">The connection string name.</param>
        /// <returns>The connection string value.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Throw when a configuration exception occurs.
        /// </exception>
        private static string GetConnectionStringFromConfiguration(string connectionStringName)
        {
            ParameterHelper.Requires<NullReferenceException>(
                !string.IsNullOrWhiteSpace(connectionStringName),
                "Could not load the configuration with null connectionStringName.");

            if (!Container.Any(container => IsGoodConnectionStringConfiguration(container, connectionStringName)))
            {
                Trace.TraceError("Connection String key=\"{0}\" not found", connectionStringName);
                throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture, "Connection String key=\"{0}\" not found", connectionStringName));
            }

            var configuration = Container.First(container => IsGoodConnectionStringConfiguration(container, connectionStringName)).Value;

            return connectionStringName != null ? configuration.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString : "";
        }

        /// <summary>
        /// Gets a value indicating whether a configuration container is good configuration file for
        /// config section.
        /// </summary>
        /// <typeparam name="TConfig">Type of the configuration section.</typeparam>
        /// <param name="container">Configuration container to test.</param>
        /// <param name="configSectionName">Configuration section name to search for.</param>
        /// <param name="tryParseSection">Value indicating whether to try parse section.</param>
        /// <returns>
        /// Value indicating whether a configuration container is good configuration file for config section.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We just want to check if conversion is valid")]
        private static bool IsGoodSectionConfiguration<TConfig>(KeyValuePair<string, Configuration> container, string configSectionName, bool tryParseSection = true)
        {
            object section;
            try
            {
                section = container.Value.Sections.Get(configSectionName);
            }
            catch (Exception exception)
            {
                // Error in getting section
                Trace.TraceError("Error in getting section : {0}", exception.Message);
                return false;
            }

            if (section == null)
            {
                return false;
            }

            // If we don't care of try parse section => return true
            if (!tryParseSection)
            {
                return true;
            }

            // Try parse value
            TConfig result;
            try
            {
                result = (TConfig)Convert.ChangeType(section, typeof(TConfig), CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                // Parsing value is not good => configuration file section is not good as well
                Trace.TraceError("Unable to cast {0} into {1} : {2}", section.GetType().FullName, typeof(TConfig).FullName, exception.Message);
                return false;
            }

            return result != null;
        }

        /// <summary>
        /// Reads a specific section from the configuration files container.
        /// </summary>
        /// <typeparam name="TConfig">The return type of the read section.</typeparam>
        /// <param name="configSectionName">The config section name.</param>
        /// <returns>The specific section casted in the given type.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Throw when a configuration exception occurs.
        /// </exception>
        private static TConfig GetConfigSectionFromConfiguration<TConfig>(string configSectionName)
            where TConfig : ConfigurationSection
        {
            ParameterHelper.Requires<NullReferenceException>(
                !string.IsNullOrWhiteSpace(configSectionName),
                "Could not load the configuration with null configSectionName.");

            return GetAllConfigSectionFromConfiguration<TConfig>(configSectionName).First().Value;
        }

        /// <summary>
        /// Reads a specific section from the configuration files container.
        /// </summary>
        /// <typeparam name="TConfig">The return type of the read section.</typeparam>
        /// <param name="configSectionName">The config section name.</param>
        /// <returns>The specific section casted in the given type.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Throw when a configuration exception occurs.
        /// </exception>
        private static IDictionary<string, TConfig> GetAllConfigSectionFromConfiguration<TConfig>(string configSectionName)
            where TConfig : ConfigurationSection
        {
            ParameterHelper.Requires<NullReferenceException>(
                !string.IsNullOrWhiteSpace(configSectionName),
                "Could not load the configuration with null configSectionName.");

            var result = new Dictionary<string, TConfig>();

            var validConfigurationFile = Container.Where(container => IsGoodSectionConfiguration<TConfig>(container, configSectionName));

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var config in validConfigurationFile)
            {
                if (configSectionName != null)
                    result.Add(config.Key, (TConfig)Convert.ChangeType(config.Value.Sections.Get(configSectionName), typeof(TConfig), CultureInfo.InvariantCulture));
            }

            if (result.Count == 0)
            {
                Trace.TraceError("Config Section key=\"{0}\" not found", configSectionName);
                throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture, "Config Section key=\"{0}\" not found", configSectionName));
            }

            return result;
        }

        #endregion ----- Private Methods -----
    }
}