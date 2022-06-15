using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinimalApi.Converters;
internal class DateOnlyConverter : JsonConverter<DateOnly>
{
    public const string serializationFormat = "dd-MM-yyyy";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        var dateOnly = DateOnly.ParseExact(value!, serializationFormat);
        return dateOnly;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(serializationFormat));
    }
}