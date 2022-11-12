using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels; 

public class MessageMarkdownParams {
    [JsonProperty("key")]
    public string Key { get; set; }
    [JsonProperty("values")]
    public List<string> Values { get; set; }

    public MessageMarkdownParams() {
        
    }

    public MessageMarkdownParams(string key, string value) {
        Key = key;
        Values = new List<string>() {value};
    }
}

public class MessageMarkdown {
    [JsonProperty("template_id")]
    public int? TemplateId { get; set; }
    [JsonProperty("custom_template_id")]
    public string CustomTemplateId { get; set; }
    [JsonProperty("params")]
    public List<MessageMarkdownParams> Params { get; set; }
    [JsonProperty("content")]
    public string Content { get; set; } 
}

public class MessageKeyboard { 
    [JsonProperty("id")]
    public string Id { get; set; } 
} 
