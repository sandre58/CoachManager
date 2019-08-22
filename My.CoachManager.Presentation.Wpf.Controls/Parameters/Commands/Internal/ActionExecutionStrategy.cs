namespace My.CoachManager.Presentation.Wpf.Controls.Parameters.Commands.Internal
{
    /// <summary>
    /// executes a delegate
    /// </summary>
    internal class ActionExecutionStrategy : IExecutionStrategy
    {
        #region IExecutionStrategy Members

        /// <summary>
        /// Gets or sets the behaviour that we execute this strategy
        /// </summary>
        public CommandBehaviorBinding Behavior { get; set; }

        /// <summary>
        /// Executes an Action delegate
        /// </summary>
        /// <param name="parameter">The parameter to pass to the Action</param>
        public void Execute(object parameter)
        {
            Behavior.Action(parameter);
        }

        #endregion IExecutionStrategy Members
    }
}