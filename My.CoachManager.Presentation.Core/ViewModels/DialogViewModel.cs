using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;
using Prism.Commands;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    public abstract class DialogViewModel : ScreenViewModel, IDialogViewModel
    {
        #region Fields

        /// <summary>
        /// Gets navigation parameters.
        /// </summary>
        private readonly IDictionary<string, object> _navigationParameters;

        #endregion

        #region Members

        public string Title { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// Gets or sets close command.
        /// </summary>
        public DelegateCommand<DialogResult?> CloseCommand { get; private set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="DialogViewModel"/>.
        /// </summary>
        public DialogViewModel()
        {
            _navigationParameters = new Dictionary<string, object>();
        }

        #endregion

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();
            DialogResult = DialogResult.None;
        }

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            CloseCommand = new DelegateCommand<DialogResult?>(Close, CanClose);
        }

        #endregion Initialisation

        #region Methods

        /// <summary>
        /// Can Close ?
        /// </summary>
        /// <param name="dialogResult"></param>
        /// <returns></returns>
        protected bool CanClose(DialogResult? dialogResult)
        {
            return true;
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public virtual void Close(DialogResult? dialogResult)
        {
            if (dialogResult != null)
            {
                DialogResult = dialogResult.Value;
            }
            
            OnCloseRequest(EventArgs.Empty);
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCloseRequest(EventArgs e)
        {
            CloseRequest?.Invoke(this, e);
        }

        /// <summary>
        /// Return a parameters;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetParameter<T>(string key, T defaultValue = default(T)) 
        {
            if (_navigationParameters.Any(x => x.Key == key) && _navigationParameters[key] is T)
            {
                Type t = typeof(T);
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    t = t.GetGenericArguments()[0];

                return (T)Convert.ChangeType(_navigationParameters[key], t);
            }

            return defaultValue;
        }

        /// <summary>
        /// Set parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetParameter<T>(string key, T value)
        {
            if (_navigationParameters.Any(x => x.Key == key))
            {
                _navigationParameters[key] = value;
            }
            else
            {
                _navigationParameters.Add(key, value);
            }
        }

        #endregion Methods

        #region Events

        /// <inheritdoc />
        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public event EventHandler CloseRequest;

        #endregion Events
    }
}