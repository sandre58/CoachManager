using System.Windows;
using My.CoachManager.Presentation.Prism.Controls.Parameters;

namespace My.CoachManager.Presentation.Prism.Controls.Datagrids.Columns
{
    /// <summary>
    /// The template column for the <see cref="DataGridOrderColumn"/>
    /// </summary>
    public class DataGridOrderColumn : System.Windows.Controls.DataGridTemplateColumn
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataGridOrderColumn"/> class.
        /// </summary>
        public DataGridOrderColumn()
        {
            Width = 60;
            CanUserReorder = false;
            CanUserResize = false;
            DataGridColumnParameters.SetCanUserHideColumn(this, false);
            CellStyle = (Style)Application.Current.FindResource("OrderDataGridOrderCellStyle");
            HeaderStyle = (Style)Application.Current.FindResource("OrderButtonDataGridColumnHeaderStyle");
        }

        #endregion Constructors
    }
}