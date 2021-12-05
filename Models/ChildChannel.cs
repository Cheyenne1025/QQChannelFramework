using ChannelModels.Types;
using QQChannelFramework.Models.Types;

namespace QQChannelFramework.Models;

/// <summary>
/// 子频道对象
/// </summary>
public class ChildChannel
{
    /// <summary>
    /// 子频道ID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 所处主频道ID
    /// </summary>
    public string GuildId { get; set; }

    /// <summary>
    /// 子频道名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 子频道类型
    /// </summary>
    public ChildChannelType Type { get; set; }

    /// <summary>
    /// 子类型
    /// </summary>
    public ChildChannelSubType SubType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// 分组ID
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    public string OwnerId { get; set; }
}