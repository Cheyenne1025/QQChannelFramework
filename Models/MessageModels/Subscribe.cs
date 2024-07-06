using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyBot.Models.MessageModels;

public class SubscribeResult {
   [JsonProperty("op")]
   public int Operation { get; set; } 
   [JsonProperty("custom_template_id")]
   public string CustomTemplateId { get; set; }
   [JsonProperty("subscribe_id")]
   public string SubscribeId { get; set; }
   [JsonProperty("subscribe_ts")]
   public long SubscribeTimestamp { get; set; }
   [JsonProperty("template_id")]
   public long TemplateId { get; set; }
   [JsonProperty("update_ts")]
   public long UpdateTimestamp { get; set; } 
}

public class SubscribeMessageEvent {
   [JsonProperty("openid")]
   public string OpenId { get; set; }
   [JsonProperty("group_openid")]
   public string GroupOpenId { get; set; } 
   
   [JsonProperty("result")]
   public List<SubscribeResult> Result { get; set; } 
}