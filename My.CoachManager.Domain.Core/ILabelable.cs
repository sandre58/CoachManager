namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface ILabelable
    {
        #region Properties

        string Label { get; set; }

        string Code { get; set; }

        #endregion Properties
    }
}