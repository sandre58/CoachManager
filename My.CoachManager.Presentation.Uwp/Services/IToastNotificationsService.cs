using Windows.UI.Notifications;

namespace My.CoachManager.Presentation.Uwp.Services
{
    public interface IToastNotificationsService
    {
        void ShowToastNotification(ToastNotification toastNotification);

    }
}
