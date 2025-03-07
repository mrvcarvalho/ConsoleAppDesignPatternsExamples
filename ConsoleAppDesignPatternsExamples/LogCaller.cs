namespace ConsoleAppDesignPatternsExamples
{
    class LogCaller
    {
        private static int _nextId = 96; // ASCII: 97:a, 98:b, 99:c, 100:d, 101:e ...
        private static readonly Random _random = new();
        public char Id { get; }          // a, b, c, d, e ...

        public LogCaller()
        {
            Id = (char)Interlocked.Increment(ref _nextId);
        }

        // Nuovo metodo asincrono per simulare l'invio di 5 msg di log
        public Task DoLogAsync(string message)
        {
            return Task.Run(()=>
            {
                Logger logger = Logger.GetInstance();
                for (int c = 1; c <= 5; c++)
                {
                    string strLog = $"Instance [{Id}:{c}] logging {c}a msg:\"{message}\"";
                    Console.WriteLine(strLog);
                    logger.LogAsync(strLog);
                }

                // Simula una chiamata di rete 
                Task.Delay(_random.Next(0, 2001));
            }
            );
        }
    }
}
