using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Modules.Roster.Views;

namespace My.CoachManager.Presentation.Wpf
{
    /// <summary>
    /// The ViewLocationProvider class locates the view model for the view that has the AutoWireViewChanged attached property set to true.
    /// The view model will be located and injected into the view's DataContext. To locate the view model, two strategies are used: First the ViewLocationProvider
    /// will look to see if there is a view model factory registered for that view, if not it will try to infer the view model using a convention based approach.
    /// This class also provides methods for registering the view model factories,
    /// and also to override the default view model factory and the default view type to view model type resolver.
    /// </summary>

    // Documentation on using the MVVM pattern is at http://go.microsoft.com/fwlink/?LinkID=288814&clcid=0x409

    public static class ViewLocationProvider
    {
        public static SquadView SquadView => ServiceLocator.Current.GetInstance<SquadView>();

        /// <summary>
        /// A dictionary that contains all the registered factories for the views.
        /// </summary>
        static readonly Dictionary<string, Func<object>> Factories = new Dictionary<string, Func<object>>();

        /// <summary>
        /// A dictionary that contains all the registered View types for the views.
        /// </summary>
        static readonly Dictionary<string, Type> TypeFactories = new Dictionary<string, Type>();

        /// <summary>
        /// The default view model factory which provides the View type as a parameter.
        /// </summary>
        static Func<Type, object> _defaultViewFactory = Activator.CreateInstance;

        /// <summary>
        /// Viewfactory that provides the View instance and View type as parameters.
        /// </summary>
        static Func<object, Type, object> _defaultViewFactoryWithViewModelParameter;

        /// <summary>
        /// Default view type to view model type resolver, assumes the view model is in same assembly as the view type, but in the "Views" namespace.
        /// </summary>
        private static Func<Type, Type> _defaultViewModelTypeToViewTypeResolver =
            viewType =>
            {
                var viewModelName = viewType.FullName;
                if (viewModelName != null)
                {
                    viewModelName = viewModelName.Replace(".ViewModels.", ".Views.");
                    var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                    viewModelName = viewModelName.EndsWith("Model")
                        ? viewModelName.Substring(viewModelName.Length - 5) : viewModelName;
                    viewModelName = viewModelName.EndsWith("View") ? viewModelName : viewModelName + "Model";
                    var viewName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewModelName,
                        viewAssemblyName);
                    return Type.GetType(viewName);
                }

                return null;
            };

        /// <summary>
        /// Sets the default view model factory.
        /// </summary>
        /// <param name="viewFactory">The view model factory which provides the View type as a parameter.</param>
        public static void SetDefaultViewFactory(Func<Type, object> viewFactory)
        {
            _defaultViewFactory = viewFactory;
        }

        /// <summary>
        /// Sets the default view model factory.
        /// </summary>
        /// <param name="viewFactory">The view model factory that provides the View instance and View type as parameters.</param>
        public static void SetDefaultViewFactory(Func<object, Type, object> viewFactory)
        {
            _defaultViewFactoryWithViewModelParameter = viewFactory;
        }

        /// <summary>
        /// Sets the default view type to view model type resolver.
        /// </summary>
        /// <param name="viewModelTypeToViewTypeResolver">The view type to view model type resolver.</param>
        public static void SetDefaultViewModelTypeToViewTypeResolver(Func<Type, Type> viewModelTypeToViewTypeResolver)
        {
            _defaultViewModelTypeToViewTypeResolver = viewModelTypeToViewTypeResolver;
        }

        /// <summary>
        /// Automatically looks up the View that corresponds to the current view, using two strategies:
        /// It first looks to see if there is a mapping registered for that view, if not it will fallback to the convention based approach.
        /// </summary>
        /// <param name="viewModel">The dependency object, typically a view.</param>
        /// <param name="setDataContextCallback">The call back to use to create the binding between the View and View</param>
        public static void AutoWireViewChanged(object viewModel, Action<object, object> setDataContextCallback)
        {
            // Try mappings first
            object view = GetViewForViewModel(viewModel);

            // try to use View type
            if (view == null)
            {
                //check type mappings
                var viewType = GetViewTypeForViewModel(viewModel.GetType());

                // fallback to convention based
                if (viewType == null)
                    viewType = _defaultViewModelTypeToViewTypeResolver(viewModel.GetType());

                if (viewType == null)
                    return;

                view = _defaultViewFactoryWithViewModelParameter != null ? _defaultViewFactoryWithViewModelParameter(viewModel, viewType) : _defaultViewFactory(viewType);
            }


            setDataContextCallback(viewModel, view);
        }

        /// <summary>
        /// Gets the view model for the specified view.
        /// </summary>
        /// <param name="viewModel">The view that the view model wants.</param>
        /// <returns>The View that corresponds to the view passed as a parameter.</returns>
        public static object GetViewForViewModel(object viewModel)
        {
            var viewModelKey = viewModel.GetType().ToString();

            // Mapping of view models base on view type (or instance) goes here
            if (Factories.ContainsKey(viewModelKey))
                return Factories[viewModelKey]();

            return null;
        }

        /// <summary>
        /// Gets the View type for the specified view.
        /// </summary>
        /// <param name="viewModel">The View that the View wants.</param>
        /// <returns>The View type that corresponds to the View.</returns>
        public static Type GetViewTypeForViewModel(Type viewModel)
        {
            var viewModelKey = viewModel.ToString();

            if (TypeFactories.ContainsKey(viewModelKey))
                return TypeFactories[viewModelKey];

            return null;
        }

        /// <summary>
        /// Registers the View factory for the specified view type.
        /// </summary>
        /// <typeparam name="T">The View</typeparam>
        /// <param name="factory">The View factory.</param>
        public static void Register<T>(Func<object> factory)
        {
            Register(typeof(T).ToString(), factory);
        }

        /// <summary>
        /// Registers the View factory for the specified view type name.
        /// </summary>
        /// <param name="viewModelTypeName">The name of the view type.</param>
        /// <param name="factory">The View factory.</param>
        public static void Register(string viewModelTypeName, Func<object> factory)
        {
            Factories[viewModelTypeName] = factory;
        }

        /// <summary>
        /// Registers a View type for the specified view type.
        /// </summary>
        /// <typeparam name="T">The ViewModel</typeparam>
        /// <typeparam name="TV">The View</typeparam>
        public static void Register<T, TV>()
        {
            var viewModelType = typeof(T);
            var viewType = typeof(TV);

            Register(viewModelType.ToString(), viewType);
        }

        /// <summary>
        /// Registers a View type for the specified view.
        /// </summary>
        /// <param name="viewModelTypeName">The View type name</param>
        /// <param name="viewType">The View type</param>
        public static void Register(string viewModelTypeName, Type viewType)
        {
            TypeFactories[viewModelTypeName] = viewType;
        }
    }
}
