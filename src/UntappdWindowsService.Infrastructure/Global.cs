namespace UntappdWindowsService.Infrastructure
{
    public static class Global
    {
        private static List<object> services = new();

        public static T GetService<T>() where T : class
        {
            return services.FirstOrDefault(item => item is T) as T;
        }

        public static void AddService<T>(T service)
        {
            services.Add(service);
        }
    }
}