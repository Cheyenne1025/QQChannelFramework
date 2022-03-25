using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models.MessageModels;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
    public MessageApi GetMessageApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 消息Api
/// </summary>
public class MessageApi {
    readonly ApiBase _apiBase;

    public MessageApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 发送消息
    /// </summary> 
    /// <returns></returns>
    public async Task<Message> SendMessageAsync(string channelId, string content = null, string image = null,
        JObject embed = null, JObject ark = null, string referenceMessageId = null, bool ignoreGetMessageError = false,
        string msgId = "") {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var m = new {
            content = content, embed = embed, ark = ark,
            message_reference = referenceMessageId == null
                ? null
                : new {message_id = referenceMessageId, ignore_get_message_error = ignoreGetMessageError},
            image = image, msg_id = msgId
        };

        var requestData = await _apiBase.WithData(m).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送文字消息
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public async Task<Message> SendTextMessageAsync(string channelId, string content, string msg_id = "") {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var textMessage = new {content = content, msg_id = msg_id};

        var requestData = await _apiBase.WithData(textMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送ark模版消息
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="arkTemplate"></param>
    /// <returns></returns>
    public async Task<Message> SendTemplateMessageAsync(string channelId, JObject arkTemplate) {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase.WithData(arkTemplate).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送Embed模版消息
    /// </summary>
    /// <param name="channelId">子频道ID</param>
    /// <param name="embedTemplate">embed模版数据</param>
    /// <param name="msg_id">要回复的消息ID (为空视为主动推送)</param>
    /// <returns></returns>
    public async Task<Message> SendEmbedMessageAsync(string channelId, JObject embedTemplate, string msg_id = "") {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var embedMessage = new {msg_id = msg_id, embed = embedTemplate};

        var requestData = await _apiBase.WithData(embedMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData.ToObject<Message>();
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="channelId">子频道ID</param>
    /// <param name="imageUrl">图片Url</param>
    /// <returns></returns>
    public async Task<Message> SendImageMessageAsync(string channelId, string imageUrl, string msg_id = "") {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var imageMessage = new {image = imageUrl, msg_id = msg_id};

        var requestData = await _apiBase.WithData(imageMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送图片+文字消息
    /// </summary>
    /// <param name="channelId">子频道ID</param>
    /// <param name="imageUrl">图片Url</param>
    /// <param name="content">文字内容</param>
    /// <returns></returns>
    public async Task<Message> SendImageAndTextMessageAsync(string channelId, string imageUrl, string content,
        string msg_id = "") {
        RawSendMessageApi rawSendMessageApi;

        var processedInfo = ApiFactory.Process(rawSendMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var imageAndTextMessage = new {image = imageUrl, content = content, msg_id = msg_id};

        var requestData =
            await _apiBase.WithData(imageAndTextMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 获取指定消息
    /// </summary>
    /// <param name="channelId">子频道ID</param>
    /// <param name="message_id">消息ID</param>
    /// <returns>获取的消息</returns>
    public async Task<Message> GetMessageAsync(string channelId, string message_id) {
        RawGetMessageApi rawGetMessageApi;

        var processedInfo = ApiFactory.Process(rawGetMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId},
            {ParamType.message_id, message_id}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 撤回消息
    /// </summary>
    /// <param name="channelId">消息所在的子频道Id</param>
    /// <param name="messageId">要撤回的消息ID</param>
    /// <param name="hideTip">隐藏撤回提示</param>
    /// <returns></returns>
    public async ValueTask RetractMessageAsync(string channelId, string messageId, bool hideTip = false) {
        RawRetractMessageApi rawRetractMessageApi;

        var processedInfo = ApiFactory.Process(rawRetractMessageApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId},
            {ParamType.message_id, messageId}
        });

        await _apiBase.WithData(new Dictionary<string, object> {{"hidetip", hideTip}}).RequestAsync(processedInfo).ConfigureAwait(false);
    }
}