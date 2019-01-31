using My.CoachManager.Presentation.Core.Dialog;

namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
{
    public interface IDialogViewModel : ICloseable, IScreenViewModel
    {
        /// <summary>
        /// Gets or sets title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        DialogResult DialogResult { get; set; }

        /// <summary>
        /// Return a parameters;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetParameter<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// Set parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void SetParameter<T>(string key, T value);
    }
}