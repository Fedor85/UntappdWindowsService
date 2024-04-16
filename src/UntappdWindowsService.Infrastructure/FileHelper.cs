using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace UntappdWindowsService.Infrastructure
{
    public static class FileHelper
    {
        private static readonly JsonSerializer jsonSerializer = new();

        public static string GetFilePath(string filePath)
        {
            DirectoryInfo directory = new FileInfo(filePath).Directory;
            if (!directory.Exists)
                directory.Create();

            return filePath;
        }

        public static void SaveFile(string filePath, object saveObject)
        {
            using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            using BsonDataWriter writer = new BsonDataWriter(fileStream);
            jsonSerializer.Serialize(writer, saveObject);
        }

        public static T OpenFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return default;

            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using BsonDataReader bson = new BsonDataReader(stream);
            return jsonSerializer.Deserialize<T>(bson);
        }

        public static List<T> OpenFileToList<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return default;

            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using BsonDataReader bson = new BsonDataReader(stream);
            bson.ReadRootValueAsArray = true;
            return jsonSerializer.Deserialize<List<T>>(bson);
        }
    }
}