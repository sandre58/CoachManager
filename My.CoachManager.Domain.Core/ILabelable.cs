namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provides properties for an entity containing a label and a code.
    /// </summary>
    public interface ILabelable
    {
        string Label { get; set; }

        string Code { get; set; }
    }
}