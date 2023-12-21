using Newtonsoft.Json;
namespace MyBot.Tools.JsonConverters
{
    internal class EnumToStringConverter<TEnumType> : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((int)value).ToString());
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(TEnumType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Enum.Parse(typeof(TEnumType), (string)reader.Value);
        }
    }
}

