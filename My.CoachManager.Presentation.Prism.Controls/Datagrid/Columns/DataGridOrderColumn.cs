using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Prism.Controls.Parameters;

namespace My.CoachManager.Presentation.Prism.Controls.Datagrid.Columns
{
    /// <summary>
    /// The template column for the <see cref="DataGrid"/>
    /// </summary>
    public class DataGridOrderColumn : System.Windows.Controls.DataGridTemplateColumn
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataGridTemplateColumn"/> class.
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