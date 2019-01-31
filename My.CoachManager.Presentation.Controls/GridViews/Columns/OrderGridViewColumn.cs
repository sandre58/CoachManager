using System.Windows;

namespace My.CoachManager.Presentation.Controls.GridViews.Columns
{
    public class OrderGridViewColumn : ExtendedGridViewColumn
    {
        public OrderGridViewColumn()
        {
            CellTemplate = (DataTemplate)Application.Current.FindResource("OrderListViewColumnTemplate");
            HeaderTemplate = (DataTemplate)Application.Current.FindResource("OrderListViewColumnHeaderTemplate");
            CanUserHideColumn = false;
            Width = 75;
        }
    }
}
