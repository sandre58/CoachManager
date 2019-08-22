using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Presentation.Mvvm.Core.Dialog;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base
{
    public abstract class DialogViewModel : DataViewModel, IDialogViewModel
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

        /// <inheritdoc />
        /// <summary>
        /// Initialise a new instance of <see cref="T:My.CoachManager.Presentation.Mvvm.Core.ViewModels.Base.DialogViewModel" />.
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
        protected override void Initialize()
        {
            base.Initialize();
            
            CloseCommand = new DelegateCommand<DialogResult?>(Close, CanClose);

            DialogResult = DialogResult.None;

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