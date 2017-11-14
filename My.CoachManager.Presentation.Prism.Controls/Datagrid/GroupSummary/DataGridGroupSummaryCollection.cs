using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls.Datagrid.GroupSummary
{
    /// <summary>
    /// The collection of group summary items.
    /// </summary>
    public sealed class DataGridGroupSummaryCollection : FreezableCollection<DataGridGroupSummary>
    {
        /// <summary>
        /// The create instance core.
        /// </summary>
        /// <returns> The <see cref="Freezable"/> Freeze able Object </returns>
        protected override Freezable CreateInstanceCore()
        {
            return new DataGridGroupSummaryCollection();
        }
    }
}