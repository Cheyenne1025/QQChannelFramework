using System;
using Newtonsoft.Json;

namespace QQChannelFramework.Tools.JsonConverters
{
    internal class UnixMillisecondsToDateTimeConverter : JsonConverter<DateTime>
    {  
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer) {
            writer.WriteValue(new DateTimeOffset(value).ToUnixTimeMilliseconds().ToString());
        }
 
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            JsonSerializer serializer) {
            return reader.Value switch{
                string {Length: 1 or 13} strVal => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(strVal)).LocalDateTime,
                DateTime dateTimeVal => dateTimeVal,
                _ => throw new ArgumentOutOfRangeException(reader.Value?.ToString())
            };
        } 
    }
}