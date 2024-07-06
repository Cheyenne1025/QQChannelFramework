using System.Net.Http;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Models.MessageModels;
using MyBot.Tools;
using Newtonsoft.Json.Linq;

namespace MyBot.Api;

public enum ChatMessageType {
    Text = 0,
    TextImage = 1,
    Markdown = 2,
    Ark = 3,
    Embed = 4,
    Media = 7
}

public enum ChatMediaType {
    Image = 1,
    Video = 2,
    Voice = 3,
    File = 4
}

sealed partial class QQChannelApi {
    public ChatMessageApi GetChatMessageApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 群聊/单聊消息Api
/// </summary>
public class ChatMessageApi {
    readonly ApiBase _apiBase;

    public ChatMessageApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    private async Task<ChatMessageResp> SendMessageAsync(string openId, string type, string content = null,
        ChatMessageType msgType = ChatMessageType.Text,
        MessageMarkdown markdown = null, MessageKeyboard keyboard = null, ChatMessageMedia media = null, JObject ark = null, string subscribeId = null,
        string passiveMsgId = null, int msgSeq = 1) {
        var url = $"/v2/{type}/{openId}/messages";
        var method = HttpMethod.Post;

        var m = new {
            content = msgType == ChatMessageType.Media ? " " : content,
            msg_type = (int)msgType,
            markdown,
            keyboard,
            ark,
            media,
            subscribe_id = subscribeId,
            msg_id = passiveMsgId,
            msg_seq = msgSeq
        };
        var requestData = await _apiBase.WithJsonContentData(m).RequestAsync(url, method).ConfigureAwait(false);

        var message = requestData.ToObject<ChatMessageResp>();
        return message;
    }

    private async Task<ChatMediaResp> SendMediaAsync(string openId, string type, ChatMediaType mediaType = ChatMediaType.Image,
        string resourceUrl = null, bool send = false) {
        var url = $"/v2/{type}/{openId}/files";
        var method = HttpMethod.Post;

        var m = new {
            file_type = (int)mediaType,
            url = resourceUrl,
            srv_send_msg = send
        };
        var requestData = await _apiBase.WithJsonContentData(m).RequestAsync(url, method).ConfigureAwait(false);

        var message = requestData.ToObject<ChatMediaResp>();
        return message;
    }

    /// <summary>
    /// 发送单聊消息
    /// </summary> 
    /// <returns></returns> 
    public async Task<ChatMessageResp> SendUserMessageAsync(string openId, string content = null, ChatMessageType msgType = ChatMessageType.Text,
        MessageMarkdown markdown = null, MessageKeyboard keyboard = null, ChatMessageMedia media = null, JObject ark = null, string subscribeId = null,
        string passiveMsgId = null, int msgSeq = 1, (string PassiveId, int PassiveSeq)? passiveInfo = null) {
        if (passiveInfo.HasValue) {
            passiveMsgId = passiveInfo.Value.PassiveId;
            msgSeq = passiveInfo.Value.PassiveSeq;
        }

        return await SendMessageAsync(openId, "users", content, msgType, markdown, keyboard, media, ark, subscribeId, passiveMsgId, msgSeq);
    }

    /// <summary>
    /// 发送群聊消息
    /// </summary> 
    /// <returns></returns> 
    public async Task<ChatMessageResp> SendGroupMessageAsync(string openId, string content = null, ChatMessageType msgType = ChatMessageType.Text,
        MessageMarkdown markdown = null, MessageKeyboard keyboard = null, ChatMessageMedia media = null, JObject ark = null, string subscribeId = null,
        string passiveMsgId = null, int msgSeq = 1, (string PassiveId, int PassiveSeq)? passiveInfo = null) {
        if (passiveInfo.HasValue) {
            passiveMsgId = passiveInfo.Value.PassiveId;
            msgSeq = passiveInfo.Value.PassiveSeq;
        }

        var message = await SendMessageAsync(openId, "groups", content, msgType, markdown, keyboard, media, ark, subscribeId, passiveMsgId, msgSeq);
        BotHooks.DispatchChatGroupMessage(openId, message);
        return message;
    }

    /// <summary>
    /// 发送单聊媒体
    /// </summary> 
    /// <returns></returns> 
    public async Task<ChatMediaResp> SendUserMediaAsync(string openId, ChatMediaType mediaType = ChatMediaType.Image,
        string resourceUrl = null, bool send = false) {
        return await SendMediaAsync(openId, "users", mediaType, resourceUrl, send);
    }

    /// <summary>
    /// 发送群聊媒体
    /// </summary> 
    /// <returns></returns> 
    public async Task<ChatMediaResp> SendGroupMediaAsync(string openId, ChatMediaType mediaType = ChatMediaType.Image,
        string resourceUrl = null, bool send = false) {
        return await SendMediaAsync(openId, "groups", mediaType, resourceUrl, send);
    }
}