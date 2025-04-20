using System.Text.Json;
using System.Text.Json.Serialization;

namespace Steam.Converters;

// i let it public if someone wants to use it outside of the library
public class UnixDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out long seconds))
            return DateTimeOffset.FromUnixTimeSeconds(seconds);
        
        throw new JsonException("Invalid Unix timestamp format.");
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToUnixTimeSeconds());
    }
}