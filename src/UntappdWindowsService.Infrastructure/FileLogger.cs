using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Infrastructure
{
    public class FileLogger(IConfigurationService configurationService) : ILogger
    {
        private readonly string logFile = FileHelper.GetFilePath(configurationService.LogFilePath);

        private int currentLevel = 0;

        private object locker = new();

        public void IncrementCurrentLevel(int level)
        {
            currentLevel += level;
        }

        public void DecrementCurrentLevel(int level)
        {
            currentLevel -= level;
            if (currentLevel < 0)
                currentLevel = 0;
        }

        public void Log(string message, int level = 0)
        {
            lock (locker)
            {
                using StreamWriter writer = new StreamWriter(logFile, true);
                writer.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}]:{GetLevel(level)}{message}");
                writer.Flush();
            }
        }

        private string GetLevel(int level)
        {
            int finalLevel = currentLevel + level;
            return finalLevel < 1 ? " " : $"{String.Concat(Enumerable.Repeat("-", finalLevel))}>";
        }
    }
}