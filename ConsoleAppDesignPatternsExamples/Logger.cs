using System.Collections.Concurrent;
using System.Text;

namespace ConsoleAppDesignPatternsExamples
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());
        private readonly ConcurrentDictionary<int, string> _logDictionary1 = new ConcurrentDictionary<int, string>();

        private int _progressivo = 0;

        private Logger() { }

        public static Logger GetInstance() => _instance.Value;

        public int NextProgressivo => System.Threading.Interlocked.Increment(ref _progressivo);

        public Task LogAsync(string message)
        {
            return Task.Run(() =>
            {
                int currentProgressivo = NextProgressivo;
                string logEntry1 = $"{currentProgressivo:D2} - [{DateTime.Now:dd/MM/yyyy HH:mm:ss.fff}] {message}";
                _logDictionary1.TryAdd(currentProgressivo, logEntry1);
            });
        }

        public async Task<string> GetLogFinalAsyncOrderById()
        {
            return await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                var orderedEntries = _logDictionary1.OrderBy(entry => entry.Key);
                foreach (var entry in orderedEntries)
                {
                    sb.AppendLine(entry.Value);
                }
                return sb.ToString();
            });
        }

        public async Task<string> GetLogFinalAsyncOrderByLogger()
        {
            return await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                var sortedDict = _logDictionary1.OrderBy(entry =>
                {
                    // Extrai a substring "Instance" do valor do dicionário
                    string value = entry.Value;
                    int startIndex = value.IndexOf("Instance [") + "Instance [".Length;
                    int endIndex = value.IndexOf("]", startIndex);
                    return value.Substring(startIndex, endIndex - startIndex);
                }).ToDictionary(entry => entry.Key, entry => entry.Value);

                foreach (var entry in sortedDict)
                {
                    sb.AppendLine(entry.Value);
                }
                return sb.ToString();
            });
        }
    }
}
