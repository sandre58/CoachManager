using System.ComponentModel;

namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
{
    public interface IOrderableViewModel : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Get or Set the order.
        /// </summary>
        int Order { get; set; }

        #endregion Properties
    }
}