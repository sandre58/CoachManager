using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public partial class PlayerEditViewModel {
        #region Properties

        /// <summary>
        /// Gets or sets positions.
        /// </summary>
        public IEnumerable<PositionModel> AllPositions { get; private set; }

        /// <summary>
        /// Gets or sets positions.
        /// </summary>
        public ObservableCollection<PositionModel> SelectedPositions { get; set; }

        #endregion Properties

        #region Methods

        private void X_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Item.Positions = AllPositions.Where(x => x.IsSelected)
                .Select(x => Item.Positions.Any(y => y.PositionId == x.Id) ? Item.Positions.First(y => y.PositionId == x.Id) : PlayerFactory.CreatePosition(Item, x)).ToObservableCollection();
        }

        //private void X_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "IsSelected")
        //    {
        //        var position = (PositionModel)sender;
        //        if (position.IsSelected)
        //        {
        //            AddPosition(position);
        //        }
        //        else
        //        {
        //            RemovePosition(position.Id);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Add a position.
        ///// </summary>
        ///// <param name="position"></param>
        //protected void AddPosition(PositionModel position)
        //{
        //    if (Item.Positions.All(x => x.PositionId != position.Id))
        //        Item.Positions.Add(PlayerFactory.CreatePosition(Item, position));
        //}

        ///// <summary>
        ///// Remove a position.
        ///// </summary>
        ///// <param name="positionId"></param>
        //protected void RemovePosition(int positionId)
        //{
        //    if(Item.Positions.Any(x => x.PositionId == positionId))
        //    Item.Positions.Remove(Item.Positions.Single(x => x.PositionId == positionId));
        //}

        #endregion Methods
    }
}