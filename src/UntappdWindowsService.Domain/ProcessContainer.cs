namespace UntappdWindowsService.Domain
{
    public class ProcessContainer(int id, string name)
    {
        public int Id { get; private set; } = id;

        public string Name { get; private set; } = name;

        public List<string> TempDirectories = new();
    }
}