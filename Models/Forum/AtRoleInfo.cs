namespace MyBot.Models.Forum
{
    /// <summary>
    /// @身份组信息
    /// </summary>
    public class AtRoleInfo
    {
        /// <summary>
        /// 身份组ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("role_id")]
        public int RoleId { get; set; }

        /// <summary>
        /// 身分组名称
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 颜色值
        /// </summary>
        [Newtonsoft.Json.JsonProperty("color")]
        public int Color { get; set; }
    }
}
