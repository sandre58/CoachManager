using System;
using System.Windows;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.Commands
{
    public class DelegateKeyCommand : DelegateCommand, IKeyCommand
    {
        #region Members

        public string Header { get; set; }

        public object Icon => string.IsNullOrEmpty(IconName) ? null : Application.Current.FindResource(IconName);

        public string IconName { get; set; }

        public object Color => string.IsNullOrEmpty(ColorName) ? null : Application.Current.FindResource(ColorName);

        public string ColorName { get; set; }

        #endregion

        #region Constructors

        public DelegateKeyCommand(Action executeMethod) : base(executeMethod)
        {
        }

        public DelegateKeyCommand(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        #endregion
    }

    public class DelegateKeyCommand<T> : DelegateCommand<T>, IKeyCommand
    {
        #region Members

        public string Header { get; set; }

        public object Icon => string.IsNullOrEmpty(IconName) ? null : Application.Current.FindResource(IconName);

        public string IconName { get; set; }

        public object Color => string.IsNullOrEmpty(ColorName) ? null : Application.Current.FindResource(ColorName);

        public string ColorName { get; set; }

        #endregion

        #region Constructors

        public DelegateKeyCommand(Action<T> executeMethod) : base(executeMethod)
        {
        }

        public DelegateKeyCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        #endregion
    }
}
