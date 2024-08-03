using System.Net.Http;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Models.MessageModels;
using MyBot.Tools;
using Newtonsoft.Json.Linq;

namespace MyBot.Api;

/// <summary>
/// 提供单聊/群聊消息操作的消息类型。
/// </summary>
public enum ChatMessageType {
    /// <summary>
    /// 表示纯文本消息。
    /// </summary>
    Text = 0,

    /// <summary>
    /// 表示图文消息。
    /// </summary>
    TextImage = 1,

    /// <summary>
    /// 表示 Markdown 消息。
    /// </summary>
    Markdown = 2,

    /// <summary>
    /// 表示 ARK 模板消息。
    /// </summary>
    Ark = 3,

    /// <summary>
    /// 表示 Embed 内嵌消息。
    /// </summary>
    Embed = 4,

    /// <summary>
    /// 表示富媒体消息。
    /// </summary>
    Media = 7
}

/// <summary>
/// 表示聊天时使用富媒体消息的类型。
/// </summary>
public enum ChatMediaType {
    /// <summary>
    /// 表示图片消息。
    /// </summary>
    Image = 1,

    /// <summary>
    /// 表示视频消息。
    /// </summary>
    Video = 2,

    /// <summary>
    /// 表示语音消息。
    /// </summary>
    Voice = 3,

    /// <summary>
    /// 表示文件。文件的发送暂时不支持。
    /// </summary>
    File = 4
}

sealed partial class QQChannelApi {
    /// <summary>
    /// 获取 <see cref="ChatMessageApi"/> 对象的实例，以使用其单聊/群聊的消息收发功能。
    /// </summary>
    /// <returns>一个 <see cref="ChatMessageApi"/> 对象实例。</returns>
    public ChatMessageApi GetChatMessageApi() => new(apiBase);
}

/// <summary>
/// 群聊/单聊消息 API。
/// </summary>
/// <param name="_apiBase">用于底层收发消息的对象。</param>
public class ChatMessageApi(ApiBase _apiBase) {
    private async Task<ChatMessageResp> SendMessageAsync(
        string openId,
        string type,
        string content = null,
        ChatMessageType msgType = ChatMessageType.Text,
        MessageMarkdown markdown = null,
        MessageKeyboard keyboard = null,
        ChatMessageMedia media = null,
        JObject ark = null,
        string subscribeId = null,
        string passiveMsgId = null,
        int msgSeq = 1
    ) {
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

    private async Task<ChatMediaResp> SendMediaAsync(
        string openId,
        string type,
        ChatMediaType mediaType = ChatMediaType.Image,
        string resourceUrl = null,
        bool send = false
    ) {
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
    /// 在单聊中发送（主动或被动）消息。消息类型由 <paramref name="msgType"/> 传入。
    /// </summary>
    /// <param name="openId">
    /// 消息传入的对方的 ID——
    /// 对于单聊来说就是 <see cref="ChatMessageUserEvent.OpenId"/>，群聊就是 <see cref="ChatMessageGroupEvent.GroupOpenId"/>。
    /// 请参考<see href="https://bot.q.qq.com/wiki/develop/api-v2/server-inter/message/send-receive/send.html">此链接</see>了解。
    /// </param>
    /// <param name="content">表示发送的消息的文本内容。该参数配合 <paramref name="msgType"/> 为 <see cref="ChatMessageType.Text"/> 使用。</param>
    /// <param name="msgType">
    /// <para>表示消息的具体类型。请按照你所发送的内容选取对应的枚举值。</para>
    /// <para>需要注意的是，选取的枚举数值只能有一个，这意味着你发送的内容也必须是绑定的。</para>
    /// <para>
    /// 比如选取的 <paramref name="msgType"/> 为 <see cref="ChatMessageType.Text"/> 时，
    /// 只能发送文本消息，因此传参的时候，<paramref name="content"/> 不应为空；而其他数值此时都是用不到的；其他类型同理。
    /// </para>
    /// </param>
    /// <param name="markdown">
    /// 表示发送的消息的 Markdown 内容。该参数配合 <paramref name="msgType"/> 为 <see cref="ChatMessageType.Markdown"/> 使用。
    /// </param>
    /// <param name="keyboard">
    /// 表示按钮消息的具体内容。
    /// 该参数配合 <paramref name="msgType"/> 为 <see cref="ChatMessageType.Ark"/> 和 <see cref="ChatMessageType.Embed"/> 使用。
    /// </param>
    /// <param name="media">
    /// <para>表示媒体消息需要传入的对象。该参数配合 <paramref name="msgType"/> 为 <see cref="ChatMessageType.Media"/> 使用。</para>
    /// <para>
    /// 需要注意的是，发送消息如果是<b>图文消息</b>，需优先使用 <see cref="SendUserMediaAsync"/> 方法调用一次，
    /// 产生 <see cref="ChatMediaResp"/> 结果后，使用其中的 <see cref="ChatMediaResp.FileInfo"/> 结果数值，并调用此方法一次，
    /// 将消息类型 <paramref name="msgType"/> 改成 <see cref="ChatMessageType.Media"/>，并给 <paramref name="media"/>（此参数）
    /// 传入对应的 <c>FileInfo</c> 结果到 <see cref="ChatMessageMedia.FileInfo"/> 里，图文消息才能显示。
    /// </para>
    /// <para>
    /// 例如，如果是回复用户的消息（被动消息），则示例代码如下：
    /// <code><![CDATA[
    /// // 回应用户的消息时，需要发图片的话，此时是有“艾特 + 图片”的两重信息的，因此是图文消息类型。纯图片是发不出去的。
    /// // 这里需要优先建立图片的消息响应结果，并将 FileInfo 传入图文消息，一并发送出去才能达到效果。
    /// 
    /// // 1. 优先获得纯图片的响应结果。
    /// var response = await SendUserMediaAsync(
    ///     message.GroupOpenId,
    ///     ChatMediaType.Image,
    ///     "https://www.example.com/suppose-here-exists-an-image.jpg",
    ///     false // 传 false 是建议状态。用 true 会占用主动消息频次，非常不建议这么做。
    /// );
    /// 
    /// // 2. 然后发送图文消息。
    /// await SendUserMessageAsync(
    ///     message.GroupOpenId,
    ///     string.Empty, // 默认发送的消息为空字符串即可。
    ///     ChatMessageType.Media,
    ///     media: new() { FileInfo = response.FileInfo }, // 传入 FileInfo。
    ///     passiveMsgId: message.Id
    /// );
    /// ]]></code>
    /// </para>
    /// </param>
    /// <param name="ark">
    /// 表示 ARK 模板消息的具体 JSON 对象信息。
    /// 请参考<see href="https://bot.q.qq.com/wiki/develop/api-v2/server-inter/message/type/ark.html">此链接</see>了解处理规则。
    /// </param>
    /// <param name="subscribeId">订阅消息。</param>
    /// <param name="passiveMsgId">表示被动消息（非机器人主动发的消息，例如回应用户的消息，需要艾特的那种）的消息 ID。</param>
    /// <param name="msgSeq">
    /// 表示被动消息下的消息序列号。序列编号用于区分不同的被动消息状态，避免发送回应消息时，回应的消息是重复的，但消息内容不重复。
    /// 一般都是 1（不用变，就是单条回应消息）；如果需要区分，则需要给每一条消息设置不同的序列值，如 2、3、4 等，以这样方式区分它们。
    /// </param>
    /// <param name="passiveInfo">
    /// 被动消息的消息序列号信息对。可用该参数快捷创建 <paramref name="passiveMsgId"/> 和 <paramref name="msgSeq"/> 参数的内容。
    /// </param>
    /// <returns>一个异步函数依赖的任务对象，并返回 <see cref="ChatMessageResp"/> 对象表示发送消息后收到的响应结果。</returns>
    /// <seealso cref="SendUserMediaAsync"/>
    public async Task<ChatMessageResp> SendUserMessageAsync(
        string openId,
        string content = null,
        ChatMessageType msgType = ChatMessageType.Text,
        MessageMarkdown markdown = null,
        MessageKeyboard keyboard = null,
        ChatMessageMedia media = null,
        JObject ark = null,
        string subscribeId = null,
        string passiveMsgId = null,
        int msgSeq = 1,
        (string PassiveId, int PassiveSeq)? passiveInfo = null
    ) {
        if (passiveInfo is { PassiveId: var msgIdValue, PassiveSeq: var msgSeqValue }) {
            passiveMsgId = msgIdValue;
            msgSeq = msgSeqValue;
        }

        return await SendMessageAsync(openId, "users", content, msgType, markdown, keyboard, media, ark, subscribeId, passiveMsgId, msgSeq);
    }

    /// <summary>
    /// 在群聊中发送（主动或被动）消息。由 <paramref name="msgType"/> 区分消息类型。
    /// 参数调用上来说，大体和单聊消息方法 <see cref="SendUserMessageAsync"/> 一致。
    /// </summary>
    /// <inheritdoc cref="SendUserMessageAsync"/>
    /// <seealso cref="SendUserMessageAsync"/>
    public async Task<ChatMessageResp> SendGroupMessageAsync(
        string openId,
        string content = null,
        ChatMessageType msgType = ChatMessageType.Text,
        MessageMarkdown markdown = null,
        MessageKeyboard keyboard = null,
        ChatMessageMedia media = null,
        JObject ark = null,
        string subscribeId = null,
        string passiveMsgId = null,
        int msgSeq = 1,
        (string PassiveId, int PassiveSeq)? passiveInfo = null
    ) {
        if (passiveInfo is { PassiveId: var msgIdValue, PassiveSeq: var msgSeqValue }) {
            passiveMsgId = msgIdValue;
            msgSeq = msgSeqValue;
        }

        var message = await SendMessageAsync(openId, "groups", content, msgType, markdown, keyboard, media, ark, subscribeId, passiveMsgId, msgSeq);
        BotHooks.DispatchChatGroupMessage(openId, message);
        return message;
    }

    /// <summary>
    /// 为单聊发送富媒体消息。富媒体消息的消息内容（图片、视频、语音和文件，文件暂不支持）
    /// 都需要使用图床等在线方式传文件的访问链接到 <paramref name="resourceUrl"/> 才能使用。
    /// </summary>
    /// <param name="openId">表示单聊或群聊的 Open ID 信息。</param>
    /// <param name="mediaType">表示富媒体消息的消息类型（图片、视频、语音还是文件）。</param>
    /// <param name="resourceUrl">表示富媒体内容的具体链接。链接上的内容需要保持开放，腾讯服务器会下载此内容转存一份。</param>
    /// <param name="send">
    /// 表示是否直接把东西发往目标端。该参数一般是 <see langword="false"/>。为 <see langword="true"/> 时，直接会占用主动消息频次，强烈不建议。
    /// </param>
    /// <returns>
    /// <para>一个异步函数的任务包裹类型，并返回一个 <see cref="ChatMediaResp"/> 结果。</para>
    /// <para>
    /// 这个结果可用于图文消息之中，尤其是 <see cref="ChatMediaResp.FileInfo"/> 属性值。
    /// 详情请参考 <see cref="SendUserMessageAsync"/> 方法的 <c>media</c> 参数的使用方式。
    /// </para>
    /// </returns>
    public async Task<ChatMediaResp> SendUserMediaAsync(
        string openId,
        ChatMediaType mediaType = ChatMediaType.Image,
        string resourceUrl = null,
        bool send = false
    ) => await SendMediaAsync(openId, "users", mediaType, resourceUrl, send);

    /// <summary>
    /// 为群聊发送富媒体消息。富媒体消息的消息内容（图片、视频、语音和文件，文件暂不支持）
    /// 都需要使用图床等在线方式传文件的访问链接到 <paramref name="resourceUrl"/> 才能使用。
    /// 所有参数都和 <see cref="SendUserMediaAsync"/> 大体一致。
    /// </summary>
    /// <inheritdoc cref="SendUserMediaAsync"/>
    /// <seealso cref="SendUserMediaAsync"/>
    public async Task<ChatMediaResp> SendGroupMediaAsync(
        string openId,
        ChatMediaType mediaType = ChatMediaType.Image,
        string resourceUrl = null,
        bool send = false
    ) => await SendMediaAsync(openId, "groups", mediaType, resourceUrl, send);
}