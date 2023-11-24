using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 群聊 / 单聊消息
/// </summary>
public class ChatMessageResp {
   [JsonProperty("id")]
   public string Id { get; set; }
   [JsonProperty("timestamp")]
   public int Timestamp { get; set; }
}

/// <summary>
/// 群聊 / 单聊消息媒体
/// </summary>
public class ChatMediaResp {
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

public class ChatMessageAuthor {
   [JsonProperty("user_openid")]
   public string UserOpenId { get; set; }
   [JsonProperty("member_openid")]
   public string MemberOpenId { get; set; }
}

public class ChatMessageAttachment {
   [JsonProperty("content_type")]
   public string ContentType { get; set; }
   [JsonProperty("filename")]
   public string Filename { get; set; }
   [JsonProperty("height")]
   public string Height { get; set; }
   [JsonProperty("width")]
   public string Width { get; set; }
   [JsonProperty("size")]
   public string Size { get; set; }
   [JsonProperty("url")]
   public string Url { get; set; }
}

public class ChatMessage {
   [JsonProperty("id")]
   public string Id { get; set; }
   [JsonProperty("author")]
   public ChatMessageAuthor Author { get; set; }
   [JsonProperty("content")]
   public string Content { get; set; }
   [JsonProperty("timestamp")]
   public DateTime Timestamp { get; set; }
   [JsonProperty("group_openid")]
   public string GroupOpenId { get; set; }
   [JsonProperty("attachments")]
   public List<ChatMessageAttachment> Attachments { get; set; }
}

public class ChatMessageGroupEvent {
   [JsonProperty("group_openid")]
   public string GroupOpenId { get; set; }
   [JsonProperty("op_member_openid")]
   public string OperateMemberId { get; set; }
   [JsonProperty("timestamp")]
   public long Timestamp { get; set; }
}

public class ChatMessageUserEvent {
   [JsonProperty("openid")]
   public string OpenId { get; set; }
   [JsonProperty("timestamp")]
   public long Timestamp { get; set; }
}
