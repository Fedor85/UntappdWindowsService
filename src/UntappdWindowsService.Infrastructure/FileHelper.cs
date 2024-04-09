namespace UntappdWindowsService.Infrastructure
{
    public static class FileHelper
    {
        public static string GetFilePath(string filePath)
        {
            DirectoryInfo directory = new FileInfo(filePath).Directory;
            if (!directory.Exists)
                directory.Create();

            return filePath;
        }
    }
}