using System;
using Newtonsoft.Json;

namespace QQChannelFramework.Tools.JsonConverters
{
    public class TimeConverter<T> : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Tools.ConvertHelper.GetChinaTicks((DateTime)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Tools.ConvertHelper.GetDateTime((string)reader.Value);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(T);
        }
    }
}