using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

public class MessageArkObj
{
    /// <summary>
    /// ark objkv列表
    /// </summary>
    [JsonProperty("obj_kv")] 
    public List<MessageArkObjKv> ObjKv { get; set; }
}