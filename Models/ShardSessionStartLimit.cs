namespace QQChannelFramework.Models;

/// <summary>
/// 分片Session限制信息
/// </summary>
public class ShardSessionStartLimit
{
    /// <summary>
    /// 每 24 小时可创建 Session 数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 目前还可以创建的 Session 数
    /// </summary>
    public int Remaining { get; set; }

    /// <summary>
    /// 重置计数的剩余时间(ms)
    /// </summary>
    public int ResetAfter { get; set; }

    /// <summary>
    /// 每 5s 可以创建的 Session 数
    /// </summary>
    public int MaxConcurrency { get; set; }
}