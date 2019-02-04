﻿using System;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Core.Models;
using Prism.Events;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    public abstract class ViewModelBase : ModelBase, IDisposable
    {
        #region Fields

        private IEventAggregator _eventAggregator;
        private ILogger _logger;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the Prism event aggregator.
        /// </summary>
        protected IEventAggregator EventAggregator => _eventAggregator ?? (_eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>());

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        protected ILogger Logger => _logger ?? (_logger = ServiceLocator.Current.GetInstance<ILogger>());

        #endregion Members

        #region Initialisation

        /// <summary>
        /// Initialize all events, data, and commands.
        /// </summary>
        public virtual void Initialize()
        {
            InitializeEvent();
            InitializeCommand();
            InitializeData();
        }

        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected virtual void InitializeCommand()
        {
        }

        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected virtual void InitializeData()
        {
        }

        /// <summary>
        /// Launch on constructor for initialize all Event.
        /// </summary>
        protected virtual void InitializeEvent()
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
            Logger.Fatal(e);
            DialogManager.ShowErrorDialog(MessageResources.GetDataError, MessageDialogButtons.Ok);
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