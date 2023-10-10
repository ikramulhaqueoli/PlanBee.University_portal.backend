using System.Text.Json;
using System.Text.Json.Serialization;

public class EnumToStringArrayConverter<TEnum> : JsonConverter<TEnum[]>
    where TEnum : struct, Enum
{
    public override TEnum[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            var enumList = new List<TEnum>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    // Parse the string value to the enum
                    if (Enum.TryParse(reader.GetString(), out TEnum enumValue))
                    {
                        enumList.Add(enumValue);
                    }
                    else
                    {
                        // Handle the case where parsing fails
                        throw new JsonException($"Unable to parse enum value: {reader.GetString()}");
                    }
                }
                else if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return enumList.ToArray();
                }
                else
                {
                    throw new JsonException($"Unexpected token type: {reader.TokenType}");
                }
            }
        }

        throw new JsonException("Expected StartArray token while deserializing.");
    }

    public override void Write(Utf8JsonWriter writer, TEnum[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var enumValue in value)
        {
            writer.WriteStringValue(enumValue.ToString());
        }
        writer.WriteEndArray();
    }
}
