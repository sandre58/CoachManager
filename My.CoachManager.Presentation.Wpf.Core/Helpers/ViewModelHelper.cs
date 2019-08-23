using System;
using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Core.Helpers
{
    public static  class ViewModelHelper
    {
        public static void ViewAndViewModelAction<T>(object view, Action<T> action) where T : class
        {
            if (view is T viewAsT)
                action(viewAsT);
            if (view is FrameworkElement element)
            {
                if (element.DataContext is T viewModelAsT)
                {
                    action(viewModelAsT);
                }
            }
        }

        public static T GetImplementerFromViewOrViewModel<T>(object view) where T : class
        {
            if (view is T viewAsT)
            {
                return viewAsT;
            }

            if (view is FrameworkElement element)
            {
                var vmAsT = element.DataContext as T;
                return vmAsT;
            }

            return null;
        }

    }
}
