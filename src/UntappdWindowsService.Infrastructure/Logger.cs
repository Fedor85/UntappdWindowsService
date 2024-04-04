using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Infrastructure
{
    public class Logger(IConfigurationService configurationService) : ILogger
    {
        private readonly string filePath = configurationService.LopFilePath;

        private object locker = new();

        public void Log(string message)
        {
            lock (locker)
            {
                using StreamWriter writer = new StreamWriter(filePath, true);
                writer.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}]: {message}");
                writer.Flush();
            }
        }
    }
}