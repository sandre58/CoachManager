using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Prism.Core.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public class ListParametersViewModel : ModelBase
    {
        #region Members

        /// <summary>
        /// Gets or sets the columns to displayed.
        /// </summary>
        public ObservableCollection<string> DisplayedColumns { get; set; }

        /// <summary>
        /// Gets or sets the preset columns to displayed.
        /// </summary>
        public Dictionary<object, string[]> PresetColumns { get; set; }

        /// <summary>
        /// Command to change displayed columns.
        /// </summary>
        public DelegateCommand<object> ChangeDisplayedColumnsCommand { get; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ListParametersViewModel"/>.
        /// </summary>
        public ListParametersViewModel()
        {
            DisplayedColumns = new ObservableCollection<string>();
            PresetColumns = new Dictionary<object, string[]>();
            ChangeDisplayedColumnsCommand = new DelegateCommand<object>(ChangeDisplayedColumns);
        }

        #endregion Constructors

        #region Columns Management

        /// <summary>
        /// Changes displayed columns.
        /// </summary>
        protected void ChangeDisplayedColumns(object type)
        {
            if (type != null)
                DisplayedColumns = new ObservableCollection<string>(PresetColumns[type]);
        }

        /// <summary>
        /// Add a preset columns.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="columns"></param>
        protected void AddPresetColumns(object key, string[] columns)
        {
            PresetColumns?.Add(key, columns);
        }

        #endregion Columns Management
    }
}