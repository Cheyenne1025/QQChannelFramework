using System.Collections.Generic;
namespace MyBot.Models.Returns;

/// <summary>
/// 获取的频道身份组列表模型
/// </summary>
public class GetChannelRolesResult
{
    /// <summary>
    /// 频道ID
    /// </summary>
    public string GuildID { get; set; }

    /// <summary>
    /// 一组频道身份组对象
    /// </summary>
    public List<Role> Roles { get; set; }

    /// <summary>
    /// 默认分组上限
    /// </summary>
    public string RoleNumLimit { get; set; }
}