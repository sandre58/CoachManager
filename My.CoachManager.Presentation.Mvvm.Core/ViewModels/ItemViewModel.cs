﻿using System.ComponentModel;
using My.CoachManager.Presentation.Mvvm.Core.Dialog;
using My.CoachManager.Presentation.Mvvm.Core.Manager;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels
{
    public abstract class ItemViewModel<TModel, TEditView> : ItemViewModel<TModel>
        where TModel : class, IEntityModel, IValidatable, IModifiable, INotifyPropertyChanged, new()
        where TEditView : IEditViewModel<TModel>
    {
        #region Members

        /// <summary>
        /// Gets or sets the edit command.
        /// </summary>
        public DelegateCommand EditCommand { get; set; }

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

            EditCommand = new DelegateCommand(Edit, CanEdit);
        }

        #endregion Initialization

        #region Edit

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void Edit()
        {
            if (Item == null) return;
            if (!CanEdit()) return;

            DialogManager.ShowEditDialog<TEditView>(Item.Id, dialog =>
            {
                OnEditCompleted(dialog.Result);
            });
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanEdit()
        {
            return Mode == ScreenMode.Read;
        }

        /// <summary>
        /// Called after the edit action.
        /// </summary>
        /// <param name="result">The dialog result.</param>
        protected virtual void OnEditCompleted(DialogResult result)
        {
            if (result == DialogResult.Ok) Refresh();
        }

        #endregion Edit

        #endregion Methods
    }

    public abstract class ItemViewModel<TModel> : NavigableWorkspaceViewModel, IItemViewModel<TModel>
        where TModel : class, IEntityModel, IValidatable, IModifiable, INotifyPropertyChanged, new()
    {
        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets item.
        /// </summary>
        [DoNotCheckEquality]
        IEntityModel IItemViewModel.Item
        {
            get => Item;
            set => Item = (TModel)value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get or set Item.
        /// </summary>
        [DoNotCheckEquality]
        public TModel Item { get; set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes data in constructor.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Item = new TModel();
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override void LoadDataCore()
        {
            var item = NavigationId > 0 ? LoadItemCore(NavigationId) : new TModel();
            Item = item;
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            Item?.ResetModified();
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected abstract TModel LoadItemCore(int id);

        #endregion Data

        #region PropertyChanged

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected virtual void OnItemChanged()
        {
            Item?.ResetModified();
        }

        #endregion PropertyChanged

        #endregion Methods
    }
}