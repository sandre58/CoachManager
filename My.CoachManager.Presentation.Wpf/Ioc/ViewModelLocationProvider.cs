using System;
using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core.Ioc;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Ioc
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
    public class ViewModelLocationProvider : IViewModelProvider
    {
        /// <inheritdoc />
        public TView GetView<TView>()
        {
            return (TView) GetView(typeof(TView));
        }

        /// <inheritdoc />
        public object GetView(Type viewType)
        {
            return ServiceLocator.Current.GetInstance(viewType);
        }

        /// <inheritdoc />
        public TViewModel GetViewModel<TViewModel>()
        {
            return (TViewModel)GetViewModel(typeof(TViewModel));
        }

        /// <inheritdoc />
        public object GetViewModel(Type viewModelType)
        {
            return ServiceLocator.Current.GetInstance(viewModelType, viewModelType.Name);
        }
    }
}
