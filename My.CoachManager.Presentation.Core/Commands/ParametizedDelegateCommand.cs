using System;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Core.Commands
{
    /// <summary>
    /// Commande sans paramètres qui utilise deux fonctions pour son exécution et son test d'exécution
    /// et peut donc être créée dynamiquement sans avoir à implémenter manuellement <see cref="ICommand"/>.
    /// </summary>
    public sealed class ParameterizedDelegateCommand<TParameter> : ICommand
    {
        #region Fields

        private readonly Func<TParameter, bool> _canExecutePredicate;

        private readonly Action<TParameter> _executeAction;

        #endregion Fields

        #region Constructors and Destructors

        public ParameterizedDelegateCommand(Action<TParameter> execute, Func<TParameter, bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _executeAction = execute;
            _canExecutePredicate = canExecute;
        }

        #endregion Constructors and Destructors

        #region Public Events

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion Public Events

        #region Public Methods and Operators

        public bool CanExecute(object parameter)
        {
            return _canExecutePredicate == null || _canExecutePredicate((TParameter)parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeAction((TParameter)parameter);
            }
        }

        #endregion Public Methods and Operators
    }
}