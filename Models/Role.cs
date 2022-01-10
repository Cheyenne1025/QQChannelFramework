namespace QQChannelFramework.Models;

/// <summary>
/// 频道身份组对象
/// </summary>
public class Role
{
    /// <summary>
    /// 身份组ID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 颜色，ARGB的HEX十六进制颜色值转换后的十进制数值
    /// </summary>
    public uint Color { get; set; }

    /// <summary>
    /// 是否在成员列表中单独展示
    /// </summary>
    public bool Hoist { get; set; }

    /// <summary>
    /// 人数
    /// </summary>
    public uint Number { get; set; }

    /// <summary>
    /// 最大人数
    /// </summary>
    public uint MemberLimit { get; set; }
}

