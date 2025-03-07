
using System;
using System.Security.Cryptography;

namespace ConsoleAppDesignPatternsExamples
{

    public partial class SingletonDemo
    {
        protected SingletonDemo() { }

        public static async Task Main()
        {

            Console.WriteLine("Dimostrazione del pattern Singleton con operazione asincrona");
            Console.WriteLine("------------------------------------------------------------");

            var logTasks = new List<Task>();

            for (int x=1;x<=10;x++)
            {
                LogCaller auxCaller = new LogCaller();
                string strFill = $"-{auxCaller.Id}";

                logTasks.Add(Task.Run(async () =>
                {
                    await auxCaller.DoLogAsync("log" + strFill.Repeat(3));
                }));
            }

            await Task.WhenAll(logTasks);

            Logger loggerFinal = Logger.GetInstance();

            Console.WriteLine("\nLOG FINALE IN ORDINE PROGRESSIVO: ");
            Console.WriteLine("=======");

            var finalLog = await loggerFinal.GetLogFinalAsyncOrderById();
            Console.WriteLine(finalLog);

            Console.WriteLine("LOG FINALE IN ORDINE DI LOGGER[x:9]: ");
            Console.WriteLine("=======");

            finalLog = await loggerFinal.GetLogFinalAsyncOrderByLogger();
            Console.WriteLine(finalLog);
        }
    }
}




