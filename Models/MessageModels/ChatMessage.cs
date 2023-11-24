using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 群聊 / 单聊消息
/// </summary>
public class ChatMessage {
   public string Id { get; set; }
   public int Timestamp { get; set; }
}

/// <summary>
/// 群聊 / 单聊消息媒体
/// </summary>
public class ChatMessageMedia {
   [JsonProperty("file_info")]
   public string FileInfo { get; set; }
}
