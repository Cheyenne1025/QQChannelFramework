using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        var requestData = await _apiBase.WithJsonContentData(textMessage).RequestAsync(raw).ConfigureAwait(false);

        var dms = requestData.ToObject<DirectMessageSession>();

        return dms;
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="guildId">私聊GuildID</param>
    /// <param name="content">文字</param>
    /// <param name="imageUrl">图片链接转存</param>
    /// <param name="imageFile">直接上传图片</param>
    /// <param name="embed">模板消息</param>
    /// <param name="ark">Ark消息</param>
    /// <param name="referenceMessageId">引用消息</param>
    /// <param name="passiveMsgId">被动消息回复ID</param>
    /// <param name="passiveEventId">被动消息事件ID</param>
    /// <param name="markdown">Markdown</param>
    /// <returns></returns>
    public async Task<Message> SendMessageAsync(
        string guildId,
        string content = null,
        string imageUrl = null,
        byte[] imageData = null,
        JObject embed = null,
        JObject ark = null,
        string referenceMessageId = null,
        MessageMarkdown markdown = null,
        MessageKeyboard keyboard = null,
        string passiveMsgId = null,
        string passiveEventId = null) {
        if (_apiBase._requestMode == RequestMode.SandBox) throw new InvalidOperationException("私信API无法在沙盒模式下使用");
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

       if (imageData == null) {
            var m = new {
                content, embed, ark,
                message_reference = referenceMessageId == null
                    ? null
                    : new {message_id = referenceMessageId, ignore_get_message_error = true},
                image = imageUrl, msg_id = passiveMsgId, event_id = passiveEventId,
                markdown, keyboard
            };
            var requestData =
                await _apiBase.WithJsonContentData(m).RequestAsync(processedInfo).ConfigureAwait(false);

            var message = requestData.ToObject<Message>();

            return message;
        } else {
            var form = new MultipartFormDataContent();
            form.Add(new StreamContent(new MemoryStream(imageData)), "file_image", "file_image"); 
            if (content != null)
                form.Add(new StringContent(content, Encoding.UTF8), "content");
            if (imageUrl != null)
                form.Add(new StringContent(imageUrl, Encoding.UTF8), "image"); 
            if (passiveMsgId != null)
                form.Add(new StringContent(passiveMsgId, Encoding.UTF8), "msg_id");
            if (passiveEventId != null)
                form.Add(new StringContent(passiveEventId, Encoding.UTF8), "event_id");

            var requestData =
                await _apiBase.WithMultipartContentData(form).RequestAsync(processedInfo).ConfigureAwait(false);

            var message = requestData.ToObject<Message>();

            return message;
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param>
    /// <param name="content">内容</param>
    /// <param name="passiveReference">回复的消息ID</param>
    /// <returns></returns>
    public async Task<Message> SendTextMessageAsync(string guildId, string content, string passiveReference = "") {
        if (_apiBase._requestMode == RequestMode.SandBox) throw new InvalidOperationException("私信API无法在沙盒模式下使用");
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var textMessage = new {content = content, msg_id = passiveReference};

        var requestData = await _apiBase.WithJsonContentData(textMessage).RequestAsync(processedInfo)
            .ConfigureAwait(false);

        var message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param>
    /// <param name="url">报备后的地址</param>
    /// <param name="passiveReference">回复的消息ID</param>
    /// <returns></returns>
    public async Task<Message> SendImageMessageAsync(string guildId, string url, string passiveReference = "") {
        if (_apiBase._requestMode == RequestMode.SandBox) throw new InvalidOperationException("私信API无法在沙盒模式下使用");
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var textMessage = new {image = url, msg_id = passiveReference};

        var requestData = await _apiBase.WithJsonContentData(textMessage).RequestAsync(processedInfo)
            .ConfigureAwait(false);

        var message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送文字+图片消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param>
    /// <param name="content">内容</param>
    /// <param name="url">报备后的地址</param>
    /// <param name="passiveReference">回复的消息ID</param>
    /// <returns></returns>
    public async Task<Message> SendImageAndTextMessageAsync(string guildId, string url, string content,
        string passiveReference = "") {
        if (_apiBase._requestMode == RequestMode.SandBox) throw new InvalidOperationException("私信API无法在沙盒模式下使用");
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var textMessage = new {image = url, content = content, msg_id = passiveReference};

        var requestData = await _apiBase.WithJsonContentData(textMessage).RequestAsync(processedInfo)
            .ConfigureAwait(false);

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
        if (_apiBase._requestMode == RequestMode.SandBox) throw new InvalidOperationException("私信API无法在沙盒模式下使用");
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var requestData = await _apiBase.WithJsonContentData(arkTemplate).RequestAsync(processedInfo)
            .ConfigureAwait(false);

        Message message = requestData.ToObject<Message>();

        return message;
    }

    /// <summary>
    /// 发送Embed模版消息
    /// </summary>
    /// <param name="guildId">在<b>创建私信会话时</b>以及<b>私信消息事件中</b>获取的<i>guild_id</i></param> 
    /// <param name="passiveReference">回复的消息ID</param>
    /// <param name="embedTemplate">embed模版数据</param> 
    /// <returns></returns>
    public async Task<Message> SendEmbedMessageAsync(string guildId, JObject embedTemplate,
        string passiveReference = "") {
        if (_apiBase._requestMode == RequestMode.SandBox) throw new InvalidOperationException("私信API无法在沙盒模式下使用");
        RawDirectSendMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var msg = new {msg_id = passiveReference, embed = embedTemplate};

        var requestData = await _apiBase.WithJsonContentData(msg).RequestAsync(processedInfo).ConfigureAwait(false);

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
        if (_apiBase._requestMode == RequestMode.SandBox) throw new InvalidOperationException("私信API无法在沙盒模式下使用");
        RawDirectRetractMessageApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId},
            {ParamType.message_id, messageId}
        });

        await _apiBase.WithQueryParam(new Dictionary<string, object> {{"hidetip", hideTip.ToString().ToLower()}})
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);
    }
}