namespace DependencyInjectionEmailOrSms
{
    public class SmsService : INotificationService
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Invio di SMS: {message}");
        }
    }
}