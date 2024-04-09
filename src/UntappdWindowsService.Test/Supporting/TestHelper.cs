using UntappdWindowsService.Infrastructure;

namespace UntappdWindowsService.Test
{
    public static class TestHelper
    {
        public static string GetSolutionDirectory()
        {
            DirectoryInfo directory =new (Directory.GetCurrentDirectory());
            while (!directory.GetFiles("*.sln").Any())
                directory = directory.Parent;

            return directory.FullName;
        }

        public static void CreateTempFiles(string directoryPath, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                using StreamWriter writer = new StreamWriter(FileHelper.GetFilePath(Path.Combine(directoryPath,$"Temp{i}.txt")), true);
                writer.WriteLine($"Temp {i}");
                writer.Flush();
            }
        }
    }
}
