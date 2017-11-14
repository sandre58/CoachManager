using System.ComponentModel;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IValidatable : INotifyDataErrorInfo
    {
        /// <summary>
        /// Check if the entity is valid.
        /// </summary>
        /// <returns></returns>
        bool IsValid(bool raisePropertyErrorChanged = true);
    }
}