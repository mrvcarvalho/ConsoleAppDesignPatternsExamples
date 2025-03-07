namespace DependencyInjectionEmailOrSms
{
    public interface INotificationService
    {
        void SendNotification(string message);
    }
}