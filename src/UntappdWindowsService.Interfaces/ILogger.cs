namespace UntappdWindowsService.Interfaces
{
    public interface ILogger
    {
        void IncrementCurrentLevel(int level = 1);
        
        void DecrementCurrentLevel(int level = 1);

        void Log(string message, int level = 0);
    }
}