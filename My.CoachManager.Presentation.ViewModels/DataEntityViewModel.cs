using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Core.Commands;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(RoleMetadata))]
    public class DataEntityViewModel : EntityViewModel, IOrderableViewModel
    {
        public DataEntityViewModel()
        {
            MoveAboveCommand = new ParameterizedDelegateCommand<DataEntityViewModel>(MoveAbove, CanMoveAbove);
            MoveBelowCommand = new ParameterizedDelegateCommand<DataEntityViewModel>(MoveBelow, CanMoveBelow);
            MoveDownCommand = new DelegateCommand(MoveDown);
            MoveUpCommand = new DelegateCommand(MoveUp);
        }

        private string _label;
        public string Label { get { return _label; } set { SetEntityProperty(() => _label = value, value, Label); } }

        private string _description;
        public string Description { get { return _description; } set { SetEntityProperty(() => _description = value, value, Description); } }

        private string _code;
        public string Code { get { return _code; } set { SetEntityProperty(() => _code = value, value, Code); } }

        private int _order;
        public int Order { get { return _order; } set { SetEntityProperty(() => _order = value, value, Order); } }

        public ObservableCollection<DataEntityViewModel> OwnerList { get; set; }

        public ICommand MoveAboveCommand { get; private set; }
        public ICommand MoveBelowCommand { get; private set; }
        public ICommand MoveDownCommand { get; private set; }
        public ICommand MoveUpCommand { get; private set; }

        public event EventHandler OrderHasChanged;

        #region Methods

        /// <summary>
        /// Determines whether this instance can move above the specified data allocation item.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can move above the specified data; otherwise, <c>false</c>.
        /// </returns>
        private bool CanMoveAbove(DataEntityViewModel data)
        {
            return OwnerList != null && (this != data) &&
                data.CanMove(OwnerList.IndexOf(this) - 1);
        }

        /// <summary>
        /// Moves the specified data above this instance.
        /// </summary>
        /// <param name="target">The data.</param>
        private void MoveAbove(DataEntityViewModel target)
        {
            var position = OwnerList.IndexOf(this);
            var targetPosition = OwnerList.IndexOf(target);
            if (targetPosition > position)
            {
                target.Move(position);
            }
            else
            {
                target.Move(position - 1);
            }
        }

        /// <summary>
        /// Determines whether this instance can move below the specified data allocation item.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// <c>true</c> if this instance can move below the specified data allocation item; otherwise, <c>false</c>.
        /// </returns>
        private bool CanMoveBelow(DataEntityViewModel target)
        {
            return OwnerList != null && (this != target) &&
                target.CanMove(OwnerList.IndexOf(this) + 1);
        }

        /// <summary>
        /// Moves the specified data allocation item below this instance.
        /// </summary>
        /// <param name="target">The synonym item.</param>
        private void MoveBelow(DataEntityViewModel target)
        {
            if (OwnerList != null)
            {
                var position = OwnerList.IndexOf(this);
                var targetPosition = OwnerList.IndexOf(target);
                if (targetPosition > position)
                {
                    target.Move(position + 1);
                }
                else
                {
                    target.Move(position);
                }
            }
        }

        /// <summary>
        /// Determines whether this instance can move down.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can move down; otherwise, <c>false</c>.
        /// </returns>
        private bool CanMoveDown()
        {
            return OwnerList != null && CanMove(OwnerList.IndexOf(this) + 1);
        }

        /// <summary>
        /// Moves this instance down.
        /// </summary>
        private void MoveDown()
        {
            if (CanMoveDown())
            {
                Move(OwnerList.IndexOf(this) + 1);
            }
        }

        /// <summary>
        /// Determines whether this instance can move up.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can move up; otherwise, <c>false</c>.
        /// </returns>
        private bool CanMoveUp()
        {
            return OwnerList != null && CanMove(OwnerList.IndexOf(this) - 1);
        }

        /// <summary>
        /// Moves this instance up.
        /// </summary>
        private void MoveUp()
        {
            if (CanMoveUp())
            {
                Move(OwnerList.IndexOf(this) - 1);
            }
        }

        /// <summary>
        /// Determines whether this instance can move this instance to the specified Order.
        /// </summary>
        /// <param name="position">The Order.</param>
        /// <returns>
        /// <c>true</c> if this instance can move this instance to the specified Order; otherwise, <c>false</c>.
        /// </returns>
        private bool CanMove(int position)
        {
            return OwnerList != null && OwnerList.IndexOf(this) > -1 && position >= 0 && position < OwnerList.Count;
        }

        /// <summary>
        /// Moves this instance to the specified Order.
        /// </summary>
        /// <param name="position">The Order.</param>
        private void Move(int position)
        {
            var actualPosition = OwnerList.IndexOf(this);

            if (CanMove(position))
            {
                OwnerList.Move(actualPosition, position);
            }

            foreach (var data in OwnerList)
            {
                data.Order = OwnerList.IndexOf(data);
            }

            if (OrderHasChanged != null)
            {
                OrderHasChanged(this, EventArgs.Empty);
            }
        }

        #endregion Methods
    }
}