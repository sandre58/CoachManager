using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls.Datagrid.Columns
{
    /// <summary>
    /// The template column for the <see cref="DataGrid"/>
    /// </summary>
    public class DataGridTemplateColumn : System.Windows.Controls.DataGridTemplateColumn
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataGridTemplateColumn"/> class.
        /// </summary>
        public DataGridTemplateColumn()
        {
            CellStyle = (Style)Application.Current.FindResource("DataGridTemplateColumnCellStyle");
        }

        #endregion Constructors
    }
}