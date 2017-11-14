namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface IOrderableViewModel : IEntityViewModel
    {
        #region Properties

        /// <summary>
        /// Get or Set the order.
        /// </summary>
        int Order { get; set; }

        #endregion Properties
    }
}