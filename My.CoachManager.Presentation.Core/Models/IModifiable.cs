namespace My.CoachManager.Presentation.Core.Models
{
    public interface IModifiable
    {
        /// <summary>
        /// Reset IsModified value.
        /// </summary>
        void ResetModified();

        /// <summary>
        /// Gets if the object is modified.
        /// </summary>
        bool IsModified();
    }
}