using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using My.CoachManager.Presentation.ViewModels.Mapping;

namespace My.CoachManager.Presentation.ViewModels.Core
{
    public abstract class DataListViewModel<TEntityViewModel, TEditViewModel> : ListViewModel<TEntityViewModel, TEditViewModel>
        where TEntityViewModel : DataEntityViewModel, new()
        where TEditViewModel : EditViewModel<TEntityViewModel>, IDialogViewModel
    {
        #region Public Methods and Operators

        protected void OnOrderHasChanged(object sender, EventArgs args)
        {
            Entities.Sort(a => a.Order);
            SaveOrders();
        }

        /// <summary>
        /// Save order.
        /// </summary>
        /// <returns></returns>
        protected virtual void SaveOrders()
        {
        }

        //protected virtual void SetEntities<TDto>(IListDto<TDto> dtos) where TDto : EntityDto
        //{
        //    Entities = new ObservableCollection<TEntityViewModel>(dtos.Results.ToViewModels<TEntityViewModel>());

        //    var list = new ObservableCollection<DataEntityViewModel>(Entities.Cast<TEntityViewModel>());
        //    Entities.ForEach(e =>
        //    {
        //        e.OwnerList = list;
        //        e.OrderHasChanged += OnOrderHasChanged;
        //    });

        //    Count = dtos.Total;
        //}

        private bool _canOrder;

        public bool CanOrder
        {
            get { return _canOrder; }
            set
            {
                if (value == _canOrder) return;
                _canOrder = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Public Methods and Operators
    }
}