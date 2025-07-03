using System.Text.Json;
using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Converters
{
    public class NumericEnumConverter<T> : JsonConverter<T>
    where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number
                && reader.TryGetInt32(out var i))
                return (T)Enum.ToObject(typeToConvert, i);
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Convert.ToInt32(value));
        }
    }
}
