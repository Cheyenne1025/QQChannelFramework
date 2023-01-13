﻿namespace MyBot.Models.WsModels
{
    /// <summary>
    /// WebSocket鉴权信息
    /// </summary>
    public struct IdentifyData
    {
        public string token { get; set; }

        public int intents { get; set; }

        public int[] shard { get; set; }
    }
}

