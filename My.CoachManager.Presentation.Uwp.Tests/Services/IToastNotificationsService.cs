using Windows.UI.Notifications;

namespace My.CoachManager.Presentation.Uwp.Tests.Services
{
    internal interface IToastNotificationsService
    {
        void ShowToastNotification(ToastNotification toastNotification);

        void ShowToastNotificationSample();
    }
}
