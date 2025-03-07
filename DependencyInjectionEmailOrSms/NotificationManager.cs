namespace DependencyInjectionEmailOrSms
{
    public class NotificationManager
    {
        private readonly INotificationService _notificationService;

        public NotificationManager(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void SendNotification(string message)
        {
            _notificationService.SendNotification(message);
        }
    }
}