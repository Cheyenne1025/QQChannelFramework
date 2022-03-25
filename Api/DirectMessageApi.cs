using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;
using QQChannelFramework.Models.MessageModels;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
    public DirectMessageApi GetDirectMessageApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 私聊Api
/// </summary>
public class DirectMessageApi {
    readonly ApiBase _apiBase;

    public DirectMessageApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 创建私信会话 
    /// </summary>
    /// <param name="userId">接收者 id</param>
    /// <param name="sourceGuildId">源频道 id</param>
    /// <returns></returns>
    public async Task<DirectMessageSession> CreateDirectMessageSessionAsync(string userId, string sourceGuildId) {
        RawCreateDirectMessageSessionApi raw;

        var textMessage = new {recipient_id = userId, source_guild_id = sourceGuildId};

        var requestData = await _apiBase.WithData(textMessage).RequestAsync(raw).ConfigureAwait(false);

        var dms = requestData.ToObject<DirectMessageSession>();

        return dms;
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param>
    /// <param name="content">内容</param>
    /// <param name="msgId">回复的消息ID</param>
    /// <returns></returns>
    public async Task<Message> SendTextMessageAsync(string guildId, string content, string msgId = "") {
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var textMessage = new {content = content, msg_id = msgId};

        var requestData = await _apiBase.WithData(textMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        var message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param>
    /// <param name="url">报备后的地址</param>
    /// <param name="msgId">回复的消息ID</param>
    /// <returns></returns>
    public async Task<Message> SendImageMessageAsync(string guildId, string url, string msgId = "") {
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var textMessage = new {image = url, msg_id = msgId};

        var requestData = await _apiBase.WithData(textMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        var message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送文字+图片消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param>
    /// <param name="content">内容</param>
    /// <param name="url">报备后的地址</param>
    /// <param name="msgId">回复的消息ID</param>
    /// <returns></returns>
    public async Task<Message> SendTextAndImageMessageAsync(string guildId, string content, string url,
        string msgId = "") {
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var textMessage = new {image = url, content = content, msg_id = msgId};

        var requestData = await _apiBase.WithData(textMessage).RequestAsync(processedInfo).ConfigureAwait(false);

        var message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送ark模版消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param>  
    /// <param name="arkTemplate">Ark</param>
    /// <returns></returns>
    public async Task<Message> SendTemplateMessageAsync(string guildId, JObject arkTemplate) {
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var requestData = await _apiBase.WithData(arkTemplate).RequestAsync(processedInfo).ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送Embed模版消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param> 
    /// <param name="msgId">回复的消息ID</param>
    /// <param name="embedTemplate">embed模版数据</param> 
    /// <returns></returns>
    public async Task<Message> SendEmbedMessageAsync(string guildId, JObject embedTemplate, string msgId = "") {
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var msg = new {msg_id = msgId, embed = embedTemplate};

        var requestData = await _apiBase.WithData(msg).RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData.ToObject<Message>();
    }

    /// <summary>
    /// 撤回消息
    /// </summary>
    /// <param name="channelId">消息所在的子频道Id</param>
    /// <param name="messageId">要撤回的消息ID</param>
    /// <param name="hideTip">隐藏撤回提示</param>
    /// <returns></returns>
    public async ValueTask RetractMessageAsync(string guildId, string messageId, bool hideTip = false) {
        RawDirectRetractMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId},
            {ParamType.message_id, messageId}
        });

        await _apiBase.WithData(new Dictionary<string, object> {{"hidetip", hideTip}}).RequestAsync(processedInfo)
            .ConfigureAwait(false);
    }
}