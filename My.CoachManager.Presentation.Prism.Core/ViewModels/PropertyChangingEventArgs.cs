using System;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public class PropertyChangingEventArgs<T> : EventArgs
    {
        #region Members

        /// <summary>
        /// Gets old value.
        /// </summary>
        public T OldValue { get; private set; }

        /// <summary>
        /// Gets new value.
        /// </summary>
        public T NewValue { get; private set; }

        /// <summary>
        /// Gets cancel value.
        /// </summary>
        public bool Cancel { get; set; }

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="PropertyChangingEventArgs{T}"/>.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public PropertyChangingEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion Constructor
    }
}