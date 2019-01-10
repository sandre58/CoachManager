using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls.GridViews.Columns
{
    public class ActionsGridViewColumn : ExtendedGridViewColumn
    {
        public ActionsGridViewColumn()
        {
            Header = " ";
            CellTemplate = (DataTemplate)Application.Current.FindResource("ActionsListViewColumnTemplate");
            HeaderTemplate = (DataTemplate)Application.Current.FindResource("SelectionListViewColumnHeaderTemplate");
            HeaderContainerStyle = (Style)Application.Current.FindResource("DisabledGridViewColumnHeaderStyle");
            CanUserHideColumn = false;
            Width = 85;
        }
    }
}
