namespace UntappdWindowsService.WCFService
{
    public class Program
    {
        static void Main(string[] args)
        {
            UntappdWindowsWCFService wcfService = new(null, null);
            wcfService.Initialize();
            wcfService.Run();
        }
    }
}