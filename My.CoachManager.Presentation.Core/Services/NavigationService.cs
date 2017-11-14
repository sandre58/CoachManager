using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.Services
{
    /// <summary>
    /// The implementation of the contract <see cref="INavigationService{TWorkspaceViewModel}"/>.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class NavigationService<TWorkspaceViewModel> : INavigationService<TWorkspaceViewModel> where TWorkspaceViewModel : INavigatable
    {
        #region Fields

        private TWorkspaceViewModel _currentView;

        private readonly IList<TWorkspaceViewModel> _history;

        #endregion Fields

        #region Constructors

        public NavigationService()
        {
            _history = new List<TWorkspaceViewModel>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Create the viewModel.
        /// </summary>
        /// <param name="typeViewModel"></param>
        /// <returns></returns>
        protected virtual TWorkspaceViewModel CreateViewModel(Type typeViewModel)
        {
            return (TWorkspaceViewModel)Activator.CreateInstance(typeViewModel);
        }

        #endregion Methods

        #region INavigationService Implementation

        /// <summary>
        /// Go to view.
        /// </summary>
        public void NavigateToView<T>() where T : TWorkspaceViewModel
        {
            NavigateToView(typeof(T));
        }

        /// <summary>
        /// Go to view.
        /// </summary>
        public void NavigateToView(Type typeViewModel)
        {
            var view = CreateViewModel(typeViewModel);
            NavigateToView(view);
        }

        /// <summary>
        /// Go to view.
        /// </summary>
        public virtual void NavigateToView(TWorkspaceViewModel viewModel)
        {
            NavigateToView(viewModel, viewModel.CanNavigate);
        }

        /// <summary>
        /// Go to view.
        /// </summary>
        protected virtual void NavigateToView(TWorkspaceViewModel viewModel, bool addInHistory)
        {
            if (addInHistory)
                AddInHistory(viewModel);
            CurrentView = viewModel;
        }

        /// <summary>
        /// Go back.
        /// </summary>
        public void GoBack()
        {
            if (CanGoBack())
            {
                if (_history.Count > 1)
                {
                    _history.RemoveAt(_history.Count - 1);
                    NavigateToView(_history.Last(), false);
                }
            }
        }

        /// <summary>
        /// Can Back ?
        /// </summary>
        /// <returns></returns>
        public bool CanGoBack()
        {
            if (_history == null || CurrentView == null || _history.Count < 2) return false;

            var index = _history.IndexOf(CurrentView);

            return index >= 1;
        }

        /// <summary>
        /// Add in history.
        /// </summary>
        private void AddInHistory(TWorkspaceViewModel viewModel)
        {
            _history.Add(viewModel);
        }

        /// <summary>
        /// Get the current View.
        /// </summary>
        public TWorkspaceViewModel CurrentView
        {
            get { return _currentView; }
            private set
            {
                if (value.Equals(_currentView))
                    return;
                _currentView = value;
                OnNotifyViewChanged();
            }
        }

        #endregion INavigationService Implementation

        #region Event

        public virtual event EventHandler ViewChanged;

        /// <summary>
        /// Notify when the viewn change.
        /// </summary>
        private void NotifyViewChanged()
        {
            if (ViewChanged != null)
            {
                ViewChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Call when view change.
        /// </summary>
        protected virtual void OnNotifyViewChanged()
        {
            NotifyViewChanged();
        }

        #endregion Event
    }
}