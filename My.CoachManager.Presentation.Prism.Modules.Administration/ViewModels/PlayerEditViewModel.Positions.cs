using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public partial class PlayerEditViewModel {
        #region Properties

        /// <summary>
        /// Gets or sets positions.
        /// </summary>
        public IEnumerable<PositionModel> AllPositions { get; private set; }

        /// <summary>
        /// Get or Set Remove Phone Command.
        /// </summary>
        public DelegateCommand<PositionModel> RemovePositionCommand { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Raises when positions changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Position_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Item.Positions = AllPositions.Where(x => x.IsSelected)
                .Select(x => Item.Positions.Any(y => y.PositionId == x.Id) ? Item.Positions.First(y => y.PositionId == x.Id) : PlayerFactory.CreatePosition(x)).ToObservableCollection();
        }

        /// <summary>
        /// Remove a position.
        /// </summary>
        /// <param name="position"></param>
        protected void RemovePosition(PositionModel position)
        {
            AllPositions.Where(x => x.Equals(position)).ForEach(x => x.IsSelected = false);
        }

        /// <summary>
        /// Can remove a position.
        /// </summary>
        private bool CanRemovePosition(PositionModel position)
        {
            return position != null;
        }



        #endregion Methods
    }
}