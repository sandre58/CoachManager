using My.CoachManager.Presentation.Wpf.Core.Services;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface IDialogViewModel : ICloseable
    {
        /// <summary>
        /// Gets or sets title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets the dialog result value, which is the value that is returned from the
        /// <see cref="IDialogService.ShowDialog"/> and <see cref="IDialogService.ShowDialog{T}"/>
        /// methods.
        /// </summary>
        bool? DialogResult { get; }

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
