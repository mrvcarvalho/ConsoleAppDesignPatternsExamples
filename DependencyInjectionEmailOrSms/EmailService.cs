namespace DependencyInjectionEmailOrSms
{
    public class EmailService : INotificationService
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Invio di e-mail: {message}");
        }
    }
}