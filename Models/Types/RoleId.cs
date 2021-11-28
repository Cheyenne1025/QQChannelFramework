using System;
namespace ChannelModels.Types
{
    /// <summary>
    /// 频道身份组ID
    /// </summary>
    public enum RoleId
    {
        /// <summary>
        /// 普通成员
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 管理员
        /// </summary>
        Admin,
        /// <summary>
        /// 机器人
        /// </summary>
        RoBot,
        /// <summary>
        /// 创建人
        /// </summary>
        GroupLeader,
        /// <summary>
        /// 子频道管理员
        /// </summary>
        ChildChannelAdmin = 5
    }
}

