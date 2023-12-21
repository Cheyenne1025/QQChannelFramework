using Newtonsoft.Json;
namespace MyBot.Models.WsModels
{
    /// <summary>
    /// WebSocket接收的Guild数据对象
    /// </summary>
    public class WsGuild : Guild
    {
        /// <summary>
        /// 操作人的ID
        /// </summary>
        [JsonProperty("op_user_id")]
        public string OperationUserId { get; set; }
    }
}

