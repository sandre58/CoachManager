using System.Collections.Generic;
using My.CoachManager.Presentation.Mvvm.Core.DragAndDrop;
using My.CoachManager.Presentation.Mvvm.Core.Manager;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels
{
    public abstract class OrderableListViewModel<TEntityViewModel, TEditView, TItemView> : ListViewModel<TEntityViewModel, TEditView, TItemView>
        where TEntityViewModel : class, ISelectable, IOrderable, IEntityModel, IValidatable, IModifiable, new()
        where TEditView : IEditViewModel<TEntityViewModel>
        where TItemView : IItemViewModel<TEntityViewModel>
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
        protected override void Initialize()
        {
            base.Initialize();
            
            ActivateOrderCommand = new DelegateCommand(ActivateOrder, CanActivateOrder);
            CancelOrderCommand = new DelegateCommand(CancelOrder, CanCancelOrder);
            ValidateOrderCommand = new DelegateCommand(ValidateOrder, CanValidateOrder);
            MoveAboveCommand = new DelegateCommand<DragAndDropEventArgs>(MoveAbove, CanMoveAbove);
            MoveBelowCommand = new DelegateCommand<DragAndDropEventArgs>(MoveBelow, CanMoveBelow);

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
            SelectAll(false);
            CanOrder = true;
            Reorder();
            Mode = ScreenMode.Edition;
        }

        #endregion Activate Order

        #region Validate Order

        /// <summary>
        /// Can Validate Order.
        /// </summary>
        /// <returns></returns>
        private bool CanValidateOrder()
        {
            return CanOrder;
        }

        /// <summary>
        /// Validate orders.
        /// </summary>
        private void ValidateOrder()
        {
            CallWebService(ValidateOrderCore,
                () => NotificationManager.ShowSuccess(MessageResources.OrderSaved),
                null,
                () =>
                {
                    Mode = ScreenMode.Read;
                    CanOrder = false;
                },
                true);
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected abstract void ValidateOrderCore();

        #endregion Validate Order

        #region Cancel Order

        /// <summary>
        /// Validate orders.
        /// </summary>
        protected virtual void CancelOrder()
        {
            Refresh();
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
            if (e.Source is TEntityViewModel source && e.Target is TEntityViewModel target)
                Move(source, target.Order);
        }

        /// <summary>
        /// Can move above.
        /// </summary>
        protected virtual bool CanMoveAbove(DragAndDropEventArgs e)
        {
            return CanOrder && e.Source is TEntityViewModel source && e.Target is TEntityViewModel target && CanMove(source, target.Order);
        }

        #endregion Move Above

        #region Selection

        /// <inheritdoc />
        /// <summary>
        /// Can Select All ?
        /// </summary>
        /// <returns></returns>
        protected override bool CanSelectAll(bool? value)
        {
            return base.CanSelectAll(value) && !CanOrder;
        }

        /// <inheritdoc />
        /// <summary>
        /// Can select an item.
        /// </summary>
        /// <returns></returns>
        protected override bool CanSelectItem(TEntityViewModel item)
        {
            return base.CanSelectItem(item) && !CanOrder;
        }

        /// <inheritdoc />
        /// <summary>
        /// Can select items.
        /// </summary>
        /// <returns></returns>
        protected override bool CanSelectItems(IEnumerable<TEntityViewModel> items)
        {
            return base.CanSelectItems(items) && !CanOrder;
        }

        #endregion Selection

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
            Items = new ObservableItemsCollection<TEntityViewModel>(Items.OrderBy(x => x.Order).ToList());
        }

        #endregion Privates methods

        #region PropertyChanged

        /// <summary>
        /// Call when CanOrder changed.
        /// </summary>
        protected virtual void OnCanOrderChanged()
        {
            AddCommand.RaiseCanExecuteChanged();
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