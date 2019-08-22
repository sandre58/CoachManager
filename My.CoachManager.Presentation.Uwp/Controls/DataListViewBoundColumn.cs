using Windows.UI.Xaml.Data;

namespace My.CoachManager.Presentation.Uwp.Controls
{
    public abstract class DataListViewBoundColumn : DataListViewColumn
    {
        /// <summary>Gets or sets the data binding for this column. </summary>
        public Binding Binding { get; set; }
    }
}
