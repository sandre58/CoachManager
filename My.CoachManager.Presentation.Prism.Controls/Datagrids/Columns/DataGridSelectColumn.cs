using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Prism.Controls.Parameters;

namespace My.CoachManager.Presentation.Prism.Controls.Datagrids.Columns
{
    /// <summary>
    /// The template column for the <see cref="DataGrid"/>
    /// </summary>
    public class DataGridSelectColumn : System.Windows.Controls.DataGridTemplateColumn
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataGridTemplateColumn"/> class.
        /// </summary>
        public DataGridSelectColumn()
        {
            MinWidth = 50;
            CanUserReorder = false;
            CanUserResize = false;
            DataGridColumnParameters.SetCanUserHideColumn(this, false);
            CellStyle = (Style)Application.Current.FindResource("SelectDataGridCellStyle");
            HeaderStyle = (Style)Application.Current.FindResource("SelectButtonDataGridColumnHeaderStyle");
        }

        #endregion Constructors
    }
}