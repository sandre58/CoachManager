using System;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Core.Commands
{
    /// <summary>
    /// Commande sans paramètres qui utilise deux fonctions pour son exécution et son test d'exécution
    /// et peut donc être créée dynamiquement sans avoir à implémenter manuellement <see cref="ICommand"/>.
    /// </summary>
    public sealed class DelegateCommand : ICommand
    {
        #region Fields

        private readonly Func<bool> _canExecutePredicate;

        private readonly Action _executeAction;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>
        /// Crée une nouvelle instance de <see cref="DelegateCommand"/> avec l'action d'exécution spécifiée,
        /// et un prédicat donné déterminant quand l'action peut être exécutée.
        /// </summary>
        /// <param name="execute">L'action à exécuter.</param>
        /// <param name="canExecute">Prédicat déterminant quand la commande peut être exécutée.</param>
        public DelegateCommand(Action execute, Func<bool> canExecute = null)
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

        /// <summary>
        /// Déclenché lorsque des changements interviennent qui peuvent affecter si la commande peut s'exécuter ou non.
        /// </summary>
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

        /// <summary>
        /// Méthode qui détermine si la commande peut être exécutée dans son état courant.
        /// </summary>
        /// <returns><c>true</c> si la commande peut être exécutée, sinon <c>false</c>.</returns>
        public bool CanExecute()
        {
            return _canExecutePredicate == null || _canExecutePredicate();
        }

        /// <summary>
        /// Méthode appelée pour l'exécution de la commande.
        /// </summary>
        public void Execute()
        {
            if (CanExecute())
            {
                _executeAction();
            }
        }

        #endregion Public Methods and Operators

        #region Explicit Interface Methods

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        #endregion Explicit Interface Methods
    }
}