using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyBot.Models.MessageModels;

public class MessageArkKv
{
    [JsonProperty("key")] 
    public string Key { get; set; }

    [JsonProperty("value")] 
    public string Value { get; set; }

    [JsonProperty("obj")] 
    public List<MessageArkObj> Obj { get; set; }
}