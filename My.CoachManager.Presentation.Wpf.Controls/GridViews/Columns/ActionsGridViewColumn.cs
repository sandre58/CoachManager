using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Controls.GridViews.Columns
{
    public class ActionsGridViewColumn : ExtendedGridViewColumn
    {
        public ActionsGridViewColumn()
        {
            CellTemplate = (DataTemplate)Application.Current.FindResource("ActionsListViewColumnTemplate");
            HeaderTemplate = (DataTemplate)Application.Current.FindResource("SelectionListViewColumnHeaderTemplate");
            CanUserHideColumn = false;
            Width = 85;
        }
    }
}
