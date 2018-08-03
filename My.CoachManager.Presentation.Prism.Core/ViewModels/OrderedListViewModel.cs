using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.DragAndDrop;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class OrderedListViewModel<TEntityViewModel, TEditView> : ListViewModel<TEntityViewModel, TEditView>
        where TEntityViewModel : class, IOrderable, IEntityModel, IValidatable, IModifiable, new()
        where TEditView : FrameworkElement
    {
        #region Members

        /// <summary>
        /// Gets a value which indicates if the view is orderable.
        /// </summary>
        public bool CanOrder { get; set; }

        /// <summary>
        /// Gets or sets the active order command.
        /// </summary>
        public DelegateCommand ActivateOrderCommand { get; set; }

        /// <summary>
        /// Gets or sets the validate order command.
        /// </summary>
        public DelegateCommand ValidateOrderCommand { get; set; }

        /// <summary>
        /// Gets or sets the cancel order command.
        /// </summary>
        public DelegateCommand CancelOrderCommand { get; set; }

        /// <summary>
        /// Gets or sets the move above command.
        /// </summary>
        public DelegateCommand<DragAndDropEventArgs> MoveAboveCommand { get; set; }

        /// <summary>
        /// Gets or sets the move below command.
        /// </summary>
        public DelegateCommand<DragAndDropEventArgs> MoveBelowCommand { get; set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            ActivateOrderCommand = new DelegateCommand(ActivateOrder, CanActivateOrder);
            CancelOrderCommand = new DelegateCommand(CancelOrder, CanCancelOrder);
            ValidateOrderCommand = new DelegateCommand(ValidateOrder, CanValidateOrder);
            MoveAboveCommand = new DelegateCommand<DragAndDropEventArgs>(MoveAbove, CanMoveAbove);
            MoveBelowCommand = new DelegateCommand<DragAndDropEventArgs>(MoveBelow, CanMoveBelow);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            CanOrder = false;
        }

        #endregion Initialization

        #region Activate Order

        /// <summary>
        /// Can activate order.
        /// </summary>
        /// <returns></returns>
        private bool CanActivateOrder()
        {
            return !CanOrder;
        }

        /// <summary>
        /// Activate order.
        /// </summary>
        private void ActivateOrder()
        {
            Reorder();
            CanOrder = true;
            Mode = ScreenMode.Edition;
        }

        #endregion Activate Order

        #region Validate Order

        /// <summary>
        /// Validate orders.
        /// </summary>
        private void ValidateOrder()
        {
            try
            {
                ValidateOrderRequested();
                ValidateOrderCore();
                ValidateOrderCompleted();
            }
            catch (BusinessException e)
            {
                OnBusinessExceptionOccured(e);
            }
            catch (Exception e)
            {
                OnExceptionOccured(e);
            }
            finally
            {
                State = ScreenState.Ready;
                Mode = ScreenMode.Read;
                CanOrder = false;
            }
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected abstract void ValidateOrderCore();

        /// <summary>
        /// Call before loading data.
        /// </summary>
        protected virtual void ValidateOrderRequested()
        {
            State = ScreenState.Saving;
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected virtual void ValidateOrderCompleted()
        {
            NotificationManager.ShowSuccess(MessageResources.OrderSaved);
        }

        /// <summary>
        /// Can Validate Order.
        /// </summary>
        /// <returns></returns>
        private bool CanValidateOrder()
        {
            return CanOrder;
        }

        #endregion Validate Order

        #region Cancel Order

        /// <summary>
        /// Validate orders.
        /// </summary>
        protected virtual void CancelOrder()
        {
            RefreshDataAsync();
            State = ScreenState.Ready;
            Mode = ScreenMode.Read;
            CanOrder = false;
        }

        /// <summary>
        /// Can Cancel Order.
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanCancelOrder()
        {
            return CanOrder;
        }

        #endregion Cancel Order

        #region Move Below

        /// <summary>
        /// Move Below.
        /// </summary>
        protected virtual void MoveBelow(DragAndDropEventArgs e)
        {
            var source = e.Source as TEntityViewModel;
            var target = e.Target as TEntityViewModel;

            if (source != null && target != null)
                Move(source, target.Order);
        }

        /// <summary>
        /// Can move below.
        /// </summary>
        protected virtual bool CanMoveBelow(DragAndDropEventArgs e)
        {
            var source = e.Source as TEntityViewModel;
            var target = e.Target as TEntityViewModel;
            return CanOrder && source != null && target != null && CanMove(source, target.Order);
        }

        #endregion Move Below

        #region Move Above

        /// <summary>
        /// Move above.
        /// </summary>
        protected virtual void MoveAbove(DragAndDropEventArgs e)
        {
            var source = e.Source as TEntityViewModel;
            var target = e.Target as TEntityViewModel;

            if (source != null && target != null)
                Move(source, target.Order);
        }

        /// <summary>
        /// Can move above.
        /// </summary>
        protected virtual bool CanMoveAbove(DragAndDropEventArgs e)
        {
            var source = e.Source as TEntityViewModel;
            var target = e.Target as TEntityViewModel;
            return CanOrder && source != null && target != null && CanMove(source, target.Order);
        }

        #endregion Move Above

        #region Privates methods

        /// <summary>
        /// Moves this instance to the specified priority.
        /// </summary>
        /// <param name="source">The item source.</param>
        /// <param name="newOrder">The new order.</param>
        protected virtual void Move(IOrderable source, int newOrder)
        {
            var items = Items.OrderBy(x => x.Order).ToList();

            int minOrder = items.Min(x => x.Order);
            if (newOrder < minOrder)
            {
                newOrder = minOrder;
            }

            int maxPriority = items.Max(x => x.Order);
            if (newOrder > maxPriority)
            {
                newOrder = maxPriority;
            }

            if (newOrder <= source.Order)
            {
                // Move select item up - so shift siblings down
                items
                    .Where(x => x.Order >= newOrder && x.Order < source.Order)
                    .ToList()
                    .ForEach(x => x.Order = x.Order + 1);
            }
            else
            {
                // Move selected item down - so shift siblings up
                items
                    .Where(x => x.Order <= newOrder && x.Order > source.Order)
                    .ToList()
                    .ForEach(x => x.Order = x.Order - 1);
            }

            source.Order = newOrder;

            Reorder();
        }

        /// <summary>
        /// Can Move item.
        /// </summary>
        /// <param name="source">The item source.</param>
        /// <param name="newOrder">The new order.</param>
        /// <returns></returns>
        protected virtual bool CanMove(TEntityViewModel source, int newOrder)
        {
            var categories = Items.OrderBy(x => x.Order).ToList();

            int minOrder = categories.Min(x => x.Order);
            if (newOrder < minOrder)
            {
                newOrder = minOrder;
            }

            int maxOrder = categories.Max(x => x.Order);
            if (newOrder > maxOrder)
            {
                newOrder = maxOrder;
            }

            return newOrder >= categories.First().Order &&
                newOrder <= categories.Last().Order &&
                (newOrder <= source.Order && newOrder != source.Order ||
                newOrder > source.Order && newOrder != source.Order);
        }

        /// <summary>
        /// Reorder the items.
        /// </summary>
        protected virtual void Reorder()
        {
            Items = new ObservableCollection<TEntityViewModel>(Items.OrderBy(x => x.Order).ToList());
        }

        #endregion Privates methods

        #region PropertyChanged

        /// <summary>
        /// Call when CanOrder changed.
        /// </summary>
        protected virtual void OnCanOrderChanged()
        {
            AddCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            RefreshCommand.RaiseCanExecuteChanged();
            MoveAboveCommand.RaiseCanExecuteChanged();
            MoveBelowCommand.RaiseCanExecuteChanged();
            ActivateOrderCommand.RaiseCanExecuteChanged();
            CancelOrderCommand.RaiseCanExecuteChanged();
            ValidateOrderCommand.RaiseCanExecuteChanged();
        }

        #endregion PropertyChanged

        #endregion Methods
    }
}