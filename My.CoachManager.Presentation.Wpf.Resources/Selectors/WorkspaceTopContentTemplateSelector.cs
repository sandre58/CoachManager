using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Resources.Selectors
{
    class WorkspaceTopContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IListViewModel listViewModel)
            {
                if (listViewModel.Filters != null)
                {
                    return ListFiltersTemplate;
                }

                return ListTemplate;
            } 
            return new DataTemplate();
        }

        public DataTemplate ListTemplate { get; set; }
        public DataTemplate ListFiltersTemplate { get; set; }


    }
}
