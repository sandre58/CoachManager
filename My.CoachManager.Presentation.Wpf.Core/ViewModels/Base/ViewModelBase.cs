using CommonServiceLocator;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Core.Models;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using My.CoachManager.CrossCutting.Resources;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Base
{
    public abstract class ViewModelBase : ModelBase, IDisposable
    {
        #region Fields

        private IMessenger _messenger;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the Messenger.
        /// </summary>
        protected IMessenger Messenger => _messenger ?? (_messenger = ServiceLocator.Current.GetInstance<IMessenger>());

        /// <summary>
        /// Initialise.
        /// </summary>
        protected bool OnInitializing { get; set; }

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initialize a new instance of <see cref="ViewModelBase"/>.
        /// </summary>
        protected ViewModelBase()
        {
            InitializeInterval();
        }
        #endregion

        #region Initialisation

        /// <summary>
        /// Initialize all events, data, and commands.
        /// </summary>
        private void InitializeInterval()
        {
            OnInitializing = true;
            Initialize();
            OnInitializing = false;
        }

        /// <summary>
        /// Initialize all events, data, and commands.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Override method to include the cleaning logic (remove event handlers for ex.).
        /// </summary>
        protected virtual void Clean()
        {
        }

        #endregion Initialisation

        #region Exceptions Management

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExceptionOccured(Exception e)
        {
            LogManager.Fatal(e);
            DialogManager.ShowErrorDialog(MessageResources.GetDataError, MessageBoxButton.OK);
        }

        /// <summary>
        /// Call when error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBusinessExceptionOccured(BusinessException e)
        {
            NotificationManager.ShowError(e.Message);
        }

        #endregion Exceptions Management

        #region IDisposable implementation

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                Clean();
            }
        }

        /// <summary>
        /// Dispose Object and launch Clean Methods.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable implementation
    }
}