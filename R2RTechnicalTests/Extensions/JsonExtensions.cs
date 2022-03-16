using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace R2RTechnicalTests.Extensions
{
    public static class JsonExtensions
    {

        public static async Task<T> DeserializeJson<T>(this string filePath) where T : new()
        {
            try
            {

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                };

                using (Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (File.Exists(filePath) && stream.Length > 0)
                    {
                        T obj = await JsonSerializer.DeserializeAsync<T>(stream, options);
                        return obj;
                    }
                    else
                    {
                        T obj = new();
                        await JsonSerializer.SerializeAsync(stream, obj, options);
                        return obj;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }


        public static void CreateAJsonFile<T>(this string filepath, T t, JsonTypeInfo<T> info) where T : new()
        {
            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                using var createStream = File.Create(filepath);
                JsonSerializer.Serialize(createStream, t, info);
                createStream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}
