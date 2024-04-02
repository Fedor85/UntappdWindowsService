using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Infrastructure
{
    public class Logger: ILogger
    {
        private string filePath;

        private object locker = new();

        public Logger()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Untappd");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            

            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UntappdWindowsServiceLog.txt");
        }

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