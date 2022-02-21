namespace ChannelModels.Types;

/// <summary>
/// 子频道类型
/// </summary>
public enum ChannelType : int
{
    /// <summary>
    /// 文字子频道
    /// </summary>
    Text,
    /// <summary>
    /// 私聊子频道
    /// </summary>
    [Obsolete("不可用", true)]
    Private,
    /// <summary>
    /// 语音子频道
    /// </summary>
    Audio,
    /// <summary>
    /// 多人私聊子频道
    /// </summary>
    [Obsolete("不可用", true)]
    MultiUserPrivate,
    /// <summary>
    /// 子频道分类
    /// </summary>
    Child,
    /// <summary>
    /// 直播子频道
    /// </summary>
    Live = 10005,
}