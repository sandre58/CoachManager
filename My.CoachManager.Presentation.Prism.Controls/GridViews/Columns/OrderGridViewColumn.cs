using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls.GridViews.Columns
{
    public class OrderGridViewColumn : ExtendedGridViewColumn
    {
        public OrderGridViewColumn()
        {
            Header = " ";
            CellTemplate = (DataTemplate)Application.Current.FindResource("OrderListViewColumnTemplate");
            HeaderTemplate = (DataTemplate)Application.Current.FindResource("OrderListViewColumnHeaderTemplate");
            CanUserHideColumn = false;
            Width = 75;
        }
    }
}
