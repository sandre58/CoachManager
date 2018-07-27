namespace My.CoachManager.Presentation.Prism.Core.Models
{
    public interface IModifiable
    {
        /// <summary>
        /// Gets if the object is modified.
        /// </summary>
        bool IsModified { get; }
    }
}