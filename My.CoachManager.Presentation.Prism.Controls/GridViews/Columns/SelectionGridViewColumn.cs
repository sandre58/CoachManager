using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls.GridViews.Columns
{
    public class SelectionGridViewColumn : ExtendedGridViewColumn
    {
        public SelectionGridViewColumn()
        {
            Header = " ";
            CellTemplate = (DataTemplate)Application.Current.FindResource("SelectionListViewColumnTemplate");
            HeaderTemplate = (DataTemplate)Application.Current.FindResource("SelectionListViewColumnHeaderTemplate");
            HeaderContainerStyle = (Style)Application.Current.FindResource("DisabledGridViewColumnHeaderStyle");
            CanUserHideColumn = false;
            Width = 40;
        }
    }
}
