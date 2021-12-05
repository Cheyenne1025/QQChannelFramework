namespace QQChannelFramework.Models;

/// <summary>
/// 主频道对象
/// </summary>
public class Guild
{
    /// <summary>
    /// 频道ID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 频道名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 频道头像Url
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 频道创建人用户ID
    /// </summary>
    public string OwnerId { get; set; }

    /// <summary>
    /// 当前人是否为创建人
    /// </summary>
    public bool Owner { get; set; }

    /// <summary>
    /// 当前成员数
    /// </summary>
    public int MemberCount { get; set; }

    /// <summary>
    /// 最大成员数
    /// </summary>
    public int MaxMembers { get; set; }

    /// <summary>
    /// 频道描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 加入时间
    /// </summary>
    public DateTime JoinedAt { get; set; }

    /// <summary>
    /// 游戏绑定公会区服ID，需要特殊申请并配置后才会返回
    /// </summary>
    public string UnionWorldId { get; set; }

    /// <summary>
    /// 游戏绑定公会/战队ID，需要特殊申请并配置后才会返回
    /// </summary>
    public string UnionOrgId { get; set; }
}