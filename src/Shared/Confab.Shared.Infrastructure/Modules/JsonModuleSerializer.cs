using System;
using System.Text;
using System.Text.Json;

namespace Confab.Shared.Infrastructure.Modules
{
    internal sealed class JsonModuleSerializer : IModuleSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public T Deserialize<T>(byte[] obj) =>
                JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(obj),SerializerOptions);


        public object Deserialize(byte[] obj, Type type) =>
                JsonSerializer.Deserialize(Encoding.UTF8.GetString(obj), type, SerializerOptions);
        

        public byte[] Serialize<T>(T value) => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
    }
}
