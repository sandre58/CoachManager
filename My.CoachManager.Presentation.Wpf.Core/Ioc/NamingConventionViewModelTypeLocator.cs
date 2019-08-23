using System;
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
    public class NamingConventionDialogTypeLocator : IViewModelTypeLocator
    {
        /// <inheritdoc />
        public Type LocateView(Type viewModelType)
        {
            string viewName = GetViewName(viewModelType);

         var viewType = GetAssemblyFromType(viewModelType).GetType(viewName);
            if (viewType == null) throw new TypeLoadException(AppendInfoAboutDialogTypeLocators($"View with name '{viewName}' is missing."));

            return viewType;
        }

        /// <inheritdoc />
        public Type LocateViewModel(Type viewType)
        {
            string viewModelName = GetViewModelName(viewType);

            var viewModelType = GetAssemblyFromType(viewType).GetType(viewModelName);
            if (viewModelType == null) throw new TypeLoadException(AppendInfoAboutDialogTypeLocators($"View model with name '{viewModelName}' is missing."));

            return viewModelType;
        }

        private static string GetViewName(Type viewModelType)
        {
            if (viewModelType.FullName != null)
            {
                var viewName = viewModelType.FullName.Replace(".ViewModels.", ".Views.");

                if (viewName.EndsWith("ViewModel", StringComparison.Ordinal))
                {
                    return viewName.Substring(
                        0,
                        viewName.Length - "Model".Length);
                }
            }

            throw new TypeLoadException(AppendInfoAboutDialogTypeLocators($"View model of type '{viewModelType}' doesn't follow naming convention since it isn't suffixed with 'ViewModel'."));
        }

        private static string GetViewModelName(Type viewType)
        {
            if (viewType.FullName != null)
            {
                var viewModelName = viewType.FullName.Replace(".Views.", ".ViewModels.");

                if (viewModelName.EndsWith("View", StringComparison.Ordinal))
                {
                    return viewModelName + "Model";
                }
            }

            throw new TypeLoadException(AppendInfoAboutDialogTypeLocators($"View of type '{viewType}' doesn't follow naming convention since it isn't suffixed with 'View'."));
        }

        private static Assembly GetAssemblyFromType(Type type)
        {
            // Assembly is supported on all .NET versions
            return type.Assembly;
        }

        private static string AppendInfoAboutDialogTypeLocators(string errorMessage)
        {
            return
                errorMessage + Environment.NewLine +
                "If your project structure doesn't conform to the default convention of MVVM " +
                "Dialogs you can always define a new convention by implementing your own dialog " +
                "type locator. For more information on how to do that, please read the GitHub " +
                "wiki or ask the author.";
        }
    }
}
