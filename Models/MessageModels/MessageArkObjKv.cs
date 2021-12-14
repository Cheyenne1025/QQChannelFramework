using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

public class MessageArkObjKv
{
    [JsonProperty("key")] 
    public string Key { get; set; }

    [JsonProperty("value")] 
    public string Value { get; set; }
}