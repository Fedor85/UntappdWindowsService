using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Infrastructure
{
    public class FileLogger(IConfigurationService configurationService) : ILogger
    {
        private readonly string filePath = FileHelper.GetFilePath(configurationService.LogFilePath);

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