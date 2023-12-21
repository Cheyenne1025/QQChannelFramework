using MyBot.Models.Types;
using Newtonsoft.Json;
namespace MyBot.Models.MessageModels;

/// <summary>
/// 表态对象
/// </summary>
public class ReactionTarget
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    [JsonConverter(typeof( ReactionTargetTypeConverter ))]
    public ReactionTargetType Type { get; set; }
}

class ReactionTargetTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new InvalidOperationException();
    }
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.ValueType == typeof( string ))
        {
            if ((string)reader.Value == "ReactionTargetType_MSG")
            {
                return ReactionTargetType.Message;
            }
            else if (((string)reader.Value)!.StartsWith("ReactionTargetType_P"))
            {
                return ReactionTargetType.Posts;
            }
            else if (((string)reader.Value)!.StartsWith("ReactionTargetType_C"))
            {
                return ReactionTargetType.Comments;
            }
            else if (((string)reader.Value)!.StartsWith("ReactionTargetType_R"))
            {
                return ReactionTargetType.Reply;
            }
            return ReactionTargetType.Message;
        }
        else
        {
            return (ReactionTargetType)(int)reader.Value!;
        }
    }
    public override bool CanConvert(Type objectType)
    {
        throw new InvalidOperationException();
    }
}
