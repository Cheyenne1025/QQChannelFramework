using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models.MessageModels;
using QQChannelFramework.Models.MessageModels.ReplyModels;

namespace QQChannelFramework.Api
{
    sealed partial class QQChannelApi
    {
        private static MessageApi _messageApi;

        public MessageApi GetMessageApi()
        {
            if (_messageApi is null)
            {
                _messageApi = new(apiBase);
            }

            return _messageApi;
        }
    }

    /// <summary>
    /// 消息Api
    /// </summary>
    public class MessageApi
    {
        readonly ApiBase _apiBase;

        public MessageApi(ApiBase apiBase)
        {
            _apiBase = apiBase;
        }

        /// <summary>
        /// 回复文字消息
        /// </summary>
        /// <param name="childChannelId">子频道ID</param>
        /// <param name="textMessage">消息内容</param>
        /// <returns>发送出去的消息</returns>
        public async Task<Message> ReplyTextMessage(string childChannelId,TextMessage textMessage)
        {
            if(textMessage.msg_id is null || textMessage.msg_id == "")
            {
                throw new Exception("当前回复的信息ID为空，会作为主动消息推送，如需主动推送消息请调用 SendTextMessage 方法");
            }

            RawSendMessageApi rawSendMessageApi;

            var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId }
            });

            var requestData = await _apiBase.WithData(textMessage).RequestAsync(processedInfo);

            Message message = new()
            {
                Id = requestData["id"].ToString(),
                ChildChannelId = requestData["channel_id"].ToString(),
                GuildId = requestData["guild_id"].ToString(),
                Time = DateTime.Parse(requestData["timestamp"].ToString()),
                MentionEveryone = bool.Parse(requestData["mention_everyone"].ToString()),
                Content = requestData["content"].ToString(),
                Author = new()
                {
                    Id = requestData["author"]["id"].ToString(),
                    IsBot = bool.Parse(requestData["author"]["bot"].ToString())
                }
            };

            return message;
        }

        /// <summary>
        /// 发送主动消息
        /// </summary>
        /// <param name="childChannelId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<Message> SendTextMessage(string childChannelId, string content)
        {
            RawSendMessageApi rawSendMessageApi;

            var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId }
            });

            TextMessage textMessage = new();
            textMessage.content = content;
            textMessage.msg_id = "";

            var requestData = await _apiBase.WithData(textMessage).RequestAsync(processedInfo);

            Message message = new()
            {
                Id = requestData["id"].ToString(),
                ChildChannelId = requestData["channel_id"].ToString(),
                GuildId = requestData["guild_id"].ToString(),
                Time = DateTime.Parse(requestData["timestamp"].ToString()),
                MentionEveryone = bool.Parse(requestData["mention_everyone"].ToString()),
                Content = requestData["content"].ToString(),
                Author = new()
                {
                    Id = requestData["author"]["id"].ToString(),
                    IsBot = bool.Parse(requestData["author"]["bot"].ToString())
                }
            };

            return message;
        }

        /// <summary>
        /// 发送模版消息
        /// </summary>
        /// <param name="childChannelId"></param>
        /// <param name="arkTemplate"></param>
        /// <returns></returns>
        public async Task<Message> SendTemplateMessage(string childChannelId,JObject arkTemplate)
        {
            RawSendMessageApi rawSendMessageApi;

            var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId }
            });

            var requestData = await _apiBase.WithData(arkTemplate).RequestAsync(processedInfo);

            Message message = new()
            {
                Id = requestData["id"].ToString(),
                ChildChannelId = requestData["channel_id"].ToString(),
                GuildId = requestData["guild_id"].ToString(),
                Time = DateTime.Parse(requestData["timestamp"].ToString()),
                MentionEveryone = bool.Parse(requestData["mention_everyone"].ToString()),
                Content = requestData["content"].ToString(),
                Author = new()
                {
                    Id = requestData["author"]["id"].ToString(),
                    IsBot = bool.Parse(requestData["author"]["bot"].ToString())
                }
            };

            return message;
        }

        /// <summary>
        /// 获取指定消息
        /// </summary>
        /// <param name="childChannelId">子频道ID</param>
        /// <param name="message_id">消息ID</param>
        /// <returns>获取的消息</returns>
        public async Task<Message> GetMessage(string childChannelId, string message_id)
        {
            RawGetMessageApi rawGetMessageApi;

            var processedInfo = ApiFactory.Process(rawGetMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId },
                {ParamType.message_id,message_id }
            });

            var requestData = await _apiBase.RequestAsync(processedInfo);

            Console.WriteLine($"获取的消息 -> {requestData}");

            return null;
        }
    }
}