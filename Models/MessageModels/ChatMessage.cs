using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 群聊 / 单聊消息
/// </summary>
public class ChatMessage {
   [JsonProperty("id")]
   public string Id { get; set; }
   [JsonProperty("timestamp")]
   public int Timestamp { get; set; }
}

/// <summary>
/// 群聊 / 单聊消息媒体
/// </summary>
public class ChatMedia {
   [JsonProperty("file_info")]
   public string FileInfo { get; set; }
   [JsonProperty("file_uuid")]
   public string FileId { get; set; }
   [JsonProperty("ttl")]
   public int TimeToLive { get; set; }
}

/// <summary>
/// 群聊 / 单聊消息媒体发送
/// </summary>
public class ChatMessageMedia {
   [JsonProperty("file_info")]
   public string FileInfo { get; set; }
}
