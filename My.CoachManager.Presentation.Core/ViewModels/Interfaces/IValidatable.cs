using System.ComponentModel;

namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
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