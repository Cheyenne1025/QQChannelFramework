namespace MyBot.Api.Types
{
    /// <summary>
    /// 子频道权限标识
    /// </summary>
    public enum ChannelPermissionType : int
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 子频道的查看权限
        /// </summary>
        Check = 1 << 0,
        /// <summary>
        /// 子频道的管理权限
        /// </summary>
        Manager = 1 << 1,
        /// <summary>
        /// 子频道的发言
        /// </summary>
        Message = 1 << 2,
    }
}

