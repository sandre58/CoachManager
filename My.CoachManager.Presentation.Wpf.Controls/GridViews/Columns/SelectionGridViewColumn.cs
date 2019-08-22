using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Controls.GridViews.Columns
{
    public class SelectionGridViewColumn : ExtendedGridViewColumn
    {
        public SelectionGridViewColumn()
        {
            CellTemplate = (DataTemplate)Application.Current.FindResource("SelectionListViewColumnTemplate");
            HeaderTemplate = (DataTemplate)Application.Current.FindResource("SelectionListViewColumnHeaderTemplate");
            CanUserHideColumn = false;
            Width = 40;
        }
    }
}
