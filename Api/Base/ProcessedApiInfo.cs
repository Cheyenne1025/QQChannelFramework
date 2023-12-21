namespace MyBot.Api.Base;

/// <summary>
/// 已加工的Api信息
/// </summary>
public struct ProcessedApiInfo
{
    /// <summary>
    /// 源信息
    /// </summary>
    public IRawApiInfo RawInfo { get; set; }

    /// <summary>
    /// Api链接
    /// </summary>
    public string Url;
}