using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Resources.Selectors
{
    class FiltersDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IPagingFiltersViewModel)
            {
                return PagingTemplate;
            }
            return new DataTemplate();
        }
        public DataTemplate PagingTemplate { get; set; }

    }
}
