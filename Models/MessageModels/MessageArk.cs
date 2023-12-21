using System.Collections.Generic;
using Newtonsoft.Json;
namespace MyBot.Models.MessageModels;

public class MessageArk
{
    /// <summary>
    /// 模版ID
    /// </summary>
    [JsonProperty("template_id")] 
    public int TemplateId { get; set; }

    /// <summary>
    /// kv值列表
    /// </summary>
    [JsonProperty("kv")] 
    public List<MessageArkKv> Kv { get; set; }
}