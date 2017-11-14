using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using My.CoachManager.Presentation.Services;
using System;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Unity;

namespace My.CoachManager.Presentation.ViewModels.Core
{
    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class NavigatableViewModel : WorkspaceViewModel, INavigatable
    {
        #region Fields

        private bool _canNavigate;
        private bool _canGoBack;
        private readonly IDialogService<CustomDialogSettings> _dialogService;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>
        /// Initialise a new instance.
        /// </summary>
        protected NavigatableViewModel()
        {
            _dialogService = ServiceLocator.DialogService;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>
        /// Get Dialog Service.
        /// </summary>
        protected virtual IDialogService<CustomDialogSettings> DialogService
        {
            get
            {
                return _dialogService;
            }
        }

        /// <summary>
        /// Can Go Back ?
        /// </summary>
        public virtual bool CanGoBack
        {
            get
            {
                return _canGoBack;
            }

            set
            {
                if (_canGoBack == value)
                {
                    return;
                }

                _canGoBack = value;
                NotifyOfPropertyChange(() => CanGoBack);
            }
        }

        /// <summary>
        /// Can Navigate ?
        /// </summary>
        public virtual bool CanNavigate
        {
            get
            {
                return _canNavigate;
            }

            set
            {
                if (_canNavigate == value)
                {
                    return;
                }

                _canNavigate = value;
                NotifyOfPropertyChange(() => CanGoBack);
            }
        }

        #endregion Public Properties

        #region Methods

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFailureLoadingData(Exception e)
        {
            base.OnFailureLoadingData(e);
            DialogService.ShowError(e.Message);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBusinessFailureLoadingData(BusinessException e)
        {
            base.OnFailureLoadingData(e);
            DialogService.ShowErrorPopup(e.Message);
        }

        /// <summary>
        /// Initialise properties.
        /// </summary>
        protected override void Initialize()
        {
            CanGoBack = true;
            CanNavigate = true;
        }

        #endregion Methods
    }
}