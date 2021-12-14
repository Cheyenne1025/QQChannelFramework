using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models.MessageModels;

namespace QQChannelFramework.Api;

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
    /// 发送文字消息
    /// </summary>
    /// <param name="childChannelId"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public async Task<Message> SendTextMessageAsync(string childChannelId, string content, string msg_id = "")
    {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId }
            });

        var textMessage = new { content = content, msg_id = msg_id };

        var requestData = await _apiBase.WithData(textMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送模版消息
    /// </summary>
    /// <param name="childChannelId"></param>
    /// <param name="arkTemplate"></param>
    /// <returns></returns>
    public async Task<Message> SendTemplateMessageAsync(string childChannelId, JObject arkTemplate)
    {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId }
            });

        var requestData = await _apiBase.WithData(arkTemplate).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="childChannelId">子频道ID</param>
    /// <param name="imageUrl">图片Url</param>
    /// <returns></returns>
    public async Task<Message> SendImageMessageAsync(string childChannelId, string imageUrl, string msg_id = "")
    {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId }
            });

        var imageMessage = new { image = imageUrl, msg_id = msg_id };

        var requestData = await _apiBase.WithData(imageMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送图片+文字消息
    /// </summary>
    /// <param name="childChannelId">子频道ID</param>
    /// <param name="imageUrl">图片Url</param>
    /// <param name="content">文字内容</param>
    /// <returns></returns>
    public async Task<Message> SendImageAndTextMessageAsync(string childChannelId, string imageUrl, string content, string msg_id = "")
    {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId }
            });

        var imageAndTextMessage = new { image = imageUrl, content = content, msg_id = msg_id };

        var requestData = await _apiBase.WithData(imageAndTextMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 获取指定消息
    /// </summary>
    /// <param name="childChannelId">子频道ID</param>
    /// <param name="message_id">消息ID</param>
    /// <returns>获取的消息</returns>
    public async Task<Message> GetMessageAsync(string childChannelId, string message_id)
    {
        RawGetMessageApi rawGetMessageApi;

        var processedInfo = ApiFactory.Process(rawGetMessageApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannelId },
                {ParamType.message_id,message_id }
            });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }
}