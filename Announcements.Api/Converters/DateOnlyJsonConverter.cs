using System.Globalization;
using Newtonsoft.Json;

namespace Announcements.WebApi.Converters
{
    //public class DateOnlyJsonConverter : JsonConverter<DateOnly?>
    //{
    //    public override DateOnly? ReadJson(JsonReader reader,
    //        Type objectType,
    //        DateOnly? existingValue,
    //        bool hasExistingValue,
    //        JsonSerializer serializer)
    //    {
    //        var value = reader.Value?.ToString();

    //        if (string.IsNullOrWhiteSpace(value))
    //            return null;

    //        return DateOnly.Parse(value, CultureInfo.InvariantCulture);
    //    }

    //    public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
    //    {
    //        if (!value.HasValue)
    //            writer.WriteNull();
    //        else
    //            writer.WriteValue(value.Value.ToString(FormatConst.DateTimeFormat, CultureInfo.InvariantCulture));
    //    }
    //}
}
