namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface ILabelableViewModel : IEntityViewModel
    {
        #region Properties

        string Label { get; set; }

        string Code { get; set; }

        #endregion Properties
    }
}