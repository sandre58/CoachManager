namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IModificable
    {
        /// <summary>
        /// Gets if the object is modified.
        /// </summary>
        bool IsModified { get; }

        /// <summary>
        /// Reset modified properties.
        /// </summary>
        /// <returns></returns>
        void ResetModified();
    }
}