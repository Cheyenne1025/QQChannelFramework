using System;
using Newtonsoft.Json;

namespace QQChannelFramework.Tools.JsonConverters
{
    internal class UnixSecondTimestampToDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Tools.ConvertHelper.GetChinaTicks((DateTime)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
			return  reader.Value switch{
                string strVal => ConvertHelper.GetDateTime(strVal),
                DateTime dateTimeVal => dateTimeVal,
                _ => throw new ArgumentOutOfRangeException()
            };			 
        }

        public override bool CanConvert(Type objectType) {
            return true;
        }
    }
}