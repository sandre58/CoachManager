using System;
using System.Globalization;
using System.Reflection;

namespace My.CoachManager.Presentation.Wpf.Core.Ioc
{
/// <summary>
    /// Dialog type locator responsible for locating dialog types for specified view models based
    /// on a naming convention used in a multitude of articles and code samples regarding the MVVM
    /// pattern.
    /// <para/>
    /// The convention states that if the name of the view model is
    /// 'MyNamespace.ViewModels.MyDialogViewModel' then the name of the dialog is
    /// 'MyNamespace.Views.MyDialog'.
    /// </summary>
    public class ViewModelProvider : IViewModelProvider
    {
        #region Members

        /// <summary>
        ///     The ViewModel factory which defers to the SimpleIoc for creating instances of the ViewModel
        /// </summary>
        public static Func<Type, object> ViewModelFactory { get; set; } = Activator.CreateInstance;

        /// <summary>
        ///     Default view type to view model type resolver, assumes the view model is in same assembly as the view type, but in
        ///     the "ViewModels" namespace.
        /// </summary>
        public static Func<Type, Type> ViewTypeToViewModelTypeResolver { get; set; } = viewType =>
        {
            var viewName = viewType.FullName;
            if (viewName == null) return null;

            viewName = viewName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelInterfaceName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);
            return Type.GetType(viewModelInterfaceName);

        };

        #endregion

        #region Methods

        /// <summary>
        ///     Automatically looks up the vVewModel that corresponds to the current view using a naming convention strategy:
        ///     Swap the Views namespace for ViewModels in the full type name
        /// </summary>
        /// <param name="view">The dependency object, typically a view</param>
        /// <param name="setDataContextCallback">The call back to use to create the binding between the View and ViewModel</param>
        public static void AutoWireViewModelChanged(object view, Action<object, object> setDataContextCallback)
        {
            var viewModelType = ViewTypeToViewModelTypeResolver(view.GetType());
            if (viewModelType == null)
                return;

            var viewModel = ViewModelFactory(viewModelType);

            setDataContextCallback(view, viewModel);
        }

        #endregion

        /// <inheritdoc />
        public TView GetView<TView>()
        {
            return (TView) GetView(typeof(TView));
        }

        /// <inheritdoc />
        public object GetView(Type viewType)
        {
            return Activator.CreateInstance(viewType);
        }

        /// <inheritdoc />
        public TViewModel GetViewModel<TViewModel>()
        {
            return (TViewModel)GetViewModel(typeof(TViewModel));
        }

        /// <inheritdoc />
        public object GetViewModel(Type viewModelType)
        {
            return Activator.CreateInstance(viewModelType);
        }
    }
}
