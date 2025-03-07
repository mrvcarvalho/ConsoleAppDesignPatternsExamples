using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionEmailOrSms
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Preparazione e configurazione di un contenitore di servizi EMAIL");
            // Configurazione del contenitore DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<INotificationService, EmailService>()
                .AddTransient<NotificationManager>()
                .BuildServiceProvider();

            // Usare NotificationManager con EmailService
            Console.WriteLine("Preparazione del messaggio 'Benvenuto al nostro servizio!'");
            var notificationManager = serviceProvider.GetService<NotificationManager>();
            Console.WriteLine(">> sendNotification(msg)");
            notificationManager?.SendNotification("Benvenuto al nostro servizio!");

            Console.WriteLine("\nRiconfigurazione del contenitore per l'utilizzo degli SMS");
            // Reconfigurando o contêiner para usar SMS
            serviceProvider = new ServiceCollection()
                .AddSingleton<INotificationService, SmsService>()
                .AddTransient<NotificationManager>()
                .BuildServiceProvider();

            // Uso do NotificationManager com SmsService
            Console.WriteLine("Preparazione del messaggio 'Offerta speciale per te!'");
            notificationManager = serviceProvider.GetService<NotificationManager>();
            Console.WriteLine(">> sendNotification(msg)");
            notificationManager?.SendNotification("Offerta speciale per te!");

            Console.WriteLine("\n");
        }
    }
}
