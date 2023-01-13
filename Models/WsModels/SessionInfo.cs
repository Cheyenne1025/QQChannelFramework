namespace MyBot.Models.WsModels
{
    /// <summary>
    /// 会话信息
    /// </summary>
    public struct SessionInfo
    {
        public string Version { get; set; }

        public string SessionId { get; set; }

        public string BotId { get; set; }

        public string Name { get; set; }
    }
}

