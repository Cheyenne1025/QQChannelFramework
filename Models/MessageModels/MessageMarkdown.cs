using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels; 

public class MessageMarkdownParams {
    [JsonProperty("key")]
    public string Key { get; set; }
    [JsonProperty("values")]
    public List<string> Values { get; set; }
}

public class MessageMarkdown {
    [JsonProperty("template_id")]
    public int TemplateId { get; set; }
    [JsonProperty("params")]
    public List<MessageMarkdownParams> Params { get; set; }
    [JsonProperty("content")]
    public string Content { get; set; } 
}