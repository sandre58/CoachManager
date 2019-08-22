namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces
{
    public interface INavigableWorkspaceViewModel : IWorkspaceViewModel, INavigationAware, IRegionMemberLifetime
    {
        /// <summary>
        /// Return a parameters;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetParameter<T>(string key, T defaultValue = default(T));
    }
}