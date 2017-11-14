using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using System;
using System.Threading.Tasks;
using My.CoachManager.CrossCutting.Core.Exceptions;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens
{
    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class WorkspaceViewModel : ScreenViewModel, IWorkspaceViewModel
    {
        #region Fields

        private ViewModelState _state;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Etat du ViewModel
        /// </summary>
        /// <remarks>La modification de l'état du ViewModel peut entrainer l'affichage des écrans de chargement / sauvegarde</remarks>
        public ViewModelState State
        {
            get
            {
                return _state;
            }

            set
            {
                if (value == _state)
                {
                    return;
                }

                _state = value;
                NotifyOfPropertyChange(() => State);
            }
        }

        #endregion Public Properties

        #region Methods

        /// <summary>
        /// Load Data.
        /// </summary>
        private async void LoadData()
        {
            try
            {
                BeforeLoadData();
                await Task.Run(() =>
                {
                    try
                    {
                        LoadDataCore();
                    }
                    catch (BusinessException e)
                    {
                        OnBusinessFailureLoadingData(e);
                    }
                    catch (Exception e)
                    {
                        OnFailureLoadingData(e);
                    }
                });
                AfterLoadData();
            }
            catch (BusinessException e)
            {
                OnBusinessFailureLoadingData(e);
            }
            catch (Exception e)
            {
                OnFailureLoadingData(e);
            }
        }

        /// <summary>
        /// Load Data
        /// </summary>
        protected virtual void LoadDataCore()
        {
        }

        /// <summary>
        /// Call before loading data.
        /// </summary>
        protected virtual void BeforeLoadData()
        {
            State = ViewModelState.Loading;
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected virtual void AfterLoadData()
        {
            State = ViewModelState.Ready;
        }

        /// <summary>
        /// Permet de rafraichir l'ensemble du ViewModel
        /// </summary>
        public virtual void RefreshData()
        {
            LoadData();
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBusinessFailureLoadingData(BusinessException e)
        {
            State = ViewModelState.Ready;
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFailureLoadingData(Exception e)
        {
            State = ViewModelState.Ready;
            UnityFactory.Resolve<ILogger>().Error(e);
        }

        /// <summary>
        /// Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name="view"/>
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (State == ViewModelState.NotInitilized)
                LoadData();
        }

        /// <summary>
        /// Try close screen.
        /// </summary>
        /// <param name="dialogResult"></param>
        public override void TryClose(bool? dialogResult = null)
        {
            if (State == ViewModelState.Saving && TrackedEntities != null)
            {
                foreach (var trackedEntity in TrackedEntities)
                {
                    trackedEntity.ResetModified();
                }

                NotifyOfPropertyChange(() => IsModified);
            }

            State = ViewModelState.Ready;

            base.TryClose(dialogResult);
        }

        #endregion Methods
    }
}