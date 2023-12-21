using Newtonsoft.Json;
namespace MyBot.Tools.JsonConverters
{
    internal class UnixSecondsToDateTimeConverter : JsonConverter<DateTime>
    {  
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer) {
            writer.WriteValue(new DateTimeOffset(value).ToUnixTimeSeconds().ToString());
        }
 
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            JsonSerializer serializer) {
            return reader.Value switch{
                string {Length: 1 or 10} strVal => DateTimeOffset.FromUnixTimeSeconds(long.Parse(strVal)).LocalDateTime,
                DateTime dateTimeVal => dateTimeVal,
                _ => throw new ArgumentOutOfRangeException(reader.Value?.ToString())
            };			 
        } 
    }
}