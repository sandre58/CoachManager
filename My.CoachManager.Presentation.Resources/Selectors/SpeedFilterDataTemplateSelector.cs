using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Core.Models.Filters;

namespace My.CoachManager.Presentation.Resources.Selectors
{
    class SpeedFilterDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is StringFilter)
            {
                return StringFilterTemplate;
            }
            return null;
        }
        public DataTemplate StringFilterTemplate { get; set; }
        
    }
}
