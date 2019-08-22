using System;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Wpf.Controls.Windows
{
    internal class CloseCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _executeAction;

        public CloseCommand(Func<object, bool> canExecute, Action<object> executeAction)
        {
            _canExecute = canExecute;
            _executeAction = executeAction;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            _executeAction?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}