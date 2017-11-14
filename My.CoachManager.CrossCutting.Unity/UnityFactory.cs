using System;
using System.Diagnostics.CodeAnalysis;

namespace My.CoachManager.CrossCutting.Unity
{
    /// <summary>
    /// Unity Factory.
    /// </summary>
    public static class UnityFactory
    {
        /// <summary>
        /// The unity container.
        /// </summary>
        private static readonly IocUnityContainer UnityContainer = new IocUnityContainer();

        #region ----- Constructors -----

        /// <summary>
        /// Initializes static members of the <see cref="UnityFactory"/> class.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "Declare only for protect instantiation of UnityFactory.")]
        static UnityFactory()
        {
        }

        #endregion ----- Constructors -----

        /// <summary>
        /// Resolve Unity Injection Dependency.
        /// </summary>
        /// <typeparam name="TService">The TService object.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The TService object resolved.</returns>
        public static TService Resolve<TService>(params ConstructorParameter[] parameters)
        {
            return UnityContainer.Resolve<TService>(parameters);
        }

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The TService object resolved.</returns>
        public static TService Resolve<TService>(string name, params ConstructorParameter[] parameters)
        {
            return UnityContainer.Resolve<TService>(name, parameters);
        }

        /// <summary>
        /// Resolve Unity Injection Dependency.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The TService object resolved.</returns>
        public static object Resolve(Type type, params ConstructorParameter[] parameters)
        {
            return UnityContainer.Resolve(type, parameters);
        }
    }
}