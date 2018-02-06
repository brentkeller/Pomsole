using System;
using System.IO;
using Newtonsoft.Json;

namespace Pomsole.Core.IO
{
    public interface IFileManager
    {
        T Read<T>(string file);
        void Write<T>(T data, string file);
    }

    public class FileManager : IFileManager
    {

        public void Write<T>(T data, string file)
        {
            var serializer = new JsonSerializer();
            using (var writer = File.CreateText(file))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
                serializer.Serialize(jsonWriter, data);
        }

        public T Read<T>(string file)
        {
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(file))
            using (var jsonReader = new JsonTextReader(reader))
                return serializer.Deserialize<T>(jsonReader);
        }

    }
}
