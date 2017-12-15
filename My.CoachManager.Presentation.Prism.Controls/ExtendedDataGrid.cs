using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Helpers;
using My.CoachManager.Presentation.Prism.Core.Filters;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class ExtendedDataGrid : DataGrid
    {
        #region Constructors

        static ExtendedDataGrid()
        {
            CommandManager.RegisterClassCommandBinding(
                typeof(ExtendedDataGrid),
                new CommandBinding(ApplicationCommands.Paste,
                    OnExecutedPaste,
                    OnCanExecutePaste));
        }

        public ExtendedDataGrid()
        {
        }

        #endregion Constructors

        #region Clipboard Paste

        private static void OnCanExecutePaste(object target, CanExecuteRoutedEventArgs args)
        {
            ((ExtendedDataGrid)target).OnCanExecutePaste(args);
        }

        /// <summary>
        /// This virtual method is called when ApplicationCommands.Paste command query its state.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnCanExecutePaste(CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
            args.Handled = true;
        }

        private static void OnExecutedPaste(object target, ExecutedRoutedEventArgs args)
        {
            ((ExtendedDataGrid)target).OnExecutedPaste(args);
        }

        /// <summary>
        /// This virtual method is called when ApplicationCommands.Paste command is executed.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnExecutedPaste(ExecutedRoutedEventArgs args)
        {
            // parse the clipboard data
            List<string[]> rowData = ClipboardHelper.ParseClipboardData();

            // call OnPastingCellClipboardContent for each cell
            int minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            int minColumnDisplayIndex = (SelectionUnit != DataGridSelectionUnit.FullRow)
                                            ? Columns.IndexOf(CurrentColumn)
                                            : 0;
            int maxColumnDisplayIndex = Columns.Count - 1;
            int rowDataIndex = 0;
            for (int i = minRowIndex; i <= maxRowIndex && rowDataIndex < rowData.Count; i++, rowDataIndex++)
            {
                int columnDataIndex = 0;
                for (int j = minColumnDisplayIndex;
                     j <= maxColumnDisplayIndex && columnDataIndex < rowData[rowDataIndex].Length;
                     j++, columnDataIndex++)
                {
                    DataGridColumn column = ColumnFromDisplayIndex(j);
                    column.OnPastingCellClipboardContent(Items[i], rowData[rowDataIndex][columnDataIndex]);
                }
            }
        }

        #endregion Clipboard Paste

        #region Filters

        private IEnumerable<IFilter> _filters;

        internal ICollectionView CollectionView
        {
            get
            {
                return CollectionViewSource.GetDefaultView(ItemsSource);
            }
        }

        ///// <summary>
        ///// Apply the filters
        ///// </summary>
        //private void ApplyFilters()
        //{
        //    // Get the view
        //    var view = CollectionView;
        //    if (view != null)
        //    {
        //        // Create a filter
        //        view.Filter = delegate (object item)
        //        {
        //            // Show the current object
        //            bool show = true;
        //            // Loop filters
        //            var filters = GetFilters().ToArray();
        //            foreach (var filter in filters)
        //            {
        //                if (filter != null)
        //                {
        //                    // Check if the current column contains a filter
        //                    var containsFilter = filter.IsMatch(item);

        //                    // Do the necessary things if the filter is not correct
        //                    if (!containsFilter)
        //                    {
        //                        show = false;
        //                        break;
        //                    }
        //                }
        //            }

        //            // Return if it's visible or not
        //            return show;
        //        };
        //    }
        //}

        ///// <summary>
        ///// Gets filter.
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerable<IFilter> GetFilters()
        //{
        //    foreach (var column in Columns)
        //    {
        //        var filter = column.GetFilter();
        //        if (filter != null) yield return filter;
        //    }

        #endregion Filters
    }
}