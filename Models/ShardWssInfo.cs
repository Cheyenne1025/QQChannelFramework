namespace QQChannelFramework.Models;

/// <summary>
/// 分片WSS接入点基础信息
/// </summary>
public class ShardWssInfo
{
    /// <summary>
    /// WebSocket 的连接地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 建议的 shard 数
    /// </summary>
    public int Shards { get; set; }
}