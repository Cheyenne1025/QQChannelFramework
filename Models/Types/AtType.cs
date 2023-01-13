namespace MyBot.Models.Types
{
    /// <summary>
    /// @的类型
    /// </summary>
    public enum AtType : int
    {
        /// <summary>
        /// at特定人
        /// </summary>
        AT_EXPLICIT_USER = 1,
        /// <summary>
        /// at角色组所有人
        /// </summary>
        AT_ROLE_GROUP = 2,
        /// <summary>
        /// at频道所有人
        /// </summary>
        AT_GUILD = 3
    }
}
