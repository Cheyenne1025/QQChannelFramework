using Newtonsoft.Json;

namespace QQChannelFramework.Models;

/// <summary>
/// 分片WSS接入点基础信息
/// </summary>
public class ShardWssInfo {
   /// <summary>
   /// WebSocket 的连接地址
   /// </summary>
   [JsonProperty("url")]
   public string Url { get; set; }

   /// <summary>
   /// 建议的 shard 数
   /// </summary>
   [JsonProperty("shards")]
   public int Shards { get; set; }
   [JsonProperty("session_start_limit")]
   public ShardSessionStartLimit SessionStartLimit { get; set; }
}

/// <summary>
/// 分片Session限制信息
/// </summary>
public class ShardSessionStartLimit {
   /// <summary>
   /// 每 24 小时可创建 Session 数
   /// </summary>
   [JsonProperty("total")]
   public int Total { get; set; }

   /// <summary>
   /// 目前还可以创建的 Session 数
   /// </summary>
   [JsonProperty("remaining")]
   public int Remaining { get; set; }

   /// <summary>
   /// 重置计数的剩余时间(ms)
   /// </summary>
   [JsonProperty("reset_after")]
   public int ResetAfter { get; set; }

   /// <summary>
   /// 每 5s 可以创建的 Session 数
   /// </summary>
   [JsonProperty("max_concurrency")]
   public int MaxConcurrency { get; set; }
}