namespace UntappdWindowsService.Domain.Models
{
    internal class ProcesseTempFiles
    {
        public long ProcesseId { get; set; }

        public string TempFilesPath { get; set; }

        public string ErrorMesssage { get; set; }

        public bool IsValid
        {
            get { return String.IsNullOrEmpty(ErrorMesssage); }
        }
    }
}
