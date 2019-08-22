using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Resources.Selectors
{
    class WorkspaceHeaderTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IEditViewModel)
            {
                return EditTemplate;
            }
            else if (item is ISelectItemsViewModel)
            {
                return SelectItemsTemplate;
            }
            return null;
        }

        public DataTemplate EditTemplate { get; set; }

        public DataTemplate SelectItemsTemplate { get; set; }


    }
}
