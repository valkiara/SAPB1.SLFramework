using System.Text.Json;
using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Converters
{
    public sealed class NumericEnumJsonConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Accept numbers (preferred)…
            if (reader.TokenType == JsonTokenType.Number)
            {
                var i = reader.GetInt32();
                return (TEnum)Enum.ToObject(typeof(TEnum), i);
            }

            // …and also numeric strings, just in case
            if (reader.TokenType == JsonTokenType.String &&
                int.TryParse(reader.GetString(), out var parsed))
            {
                return (TEnum)Enum.ToObject(typeof(TEnum), parsed);
            }

            throw new JsonException($"Expected numeric value for enum {typeof(TEnum).Name}.");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Convert.ToInt32(value));
        }
    }
}
