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
using QQChannelFramework.Models.MessageModels;

namespace QQChannelFramework.Api;

public enum ChatMessageType {
   Text = 0,
   TextImage = 1,
   Markdown = 2,
   Ark = 3,
   Embed = 4,
   Media = 7
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

   private async Task<ChatMessage> SendMessageAsync(string openId, string type, string content = null, ChatMessageType msgType = ChatMessageType.Text,
      MessageMarkdown markdown = null, MessageKeyboard keyboard = null, ChatMessageMedia media = null, JObject ark = null,
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
         msg_id = passiveMsgId,
         msg_seq = msgSeq
      };
      var requestData = await _apiBase.WithJsonContentData(m).RequestAsync(url, method).ConfigureAwait(false);

      var message = requestData.ToObject<ChatMessage>();
      return message;
   }

   /// <summary>
   /// 发送单聊消息
   /// </summary> 
   /// <returns></returns> 
   private async Task<ChatMessage> SendUserMessageAsync(string openId, string content = null, ChatMessageType msgType = ChatMessageType.Text,
      MessageMarkdown markdown = null, MessageKeyboard keyboard = null, ChatMessageMedia media = null, JObject ark = null,
      string passiveMsgId = null, int msgSeq = 1) {
      return await SendMessageAsync(openId, "users", content, msgType, markdown, keyboard, media, ark, passiveMsgId, msgSeq);
   }

   /// <summary>
   /// 发送群聊消息
   /// </summary> 
   /// <returns></returns> 
   private async Task<ChatMessage> SendGroupMessageAsync(string openId, string content = null, ChatMessageType msgType = ChatMessageType.Text,
      MessageMarkdown markdown = null, MessageKeyboard keyboard = null, ChatMessageMedia media = null, JObject ark = null,
      string passiveMsgId = null, int msgSeq = 1) {
      return await SendMessageAsync(openId, "groups", content, msgType, markdown, keyboard, media, ark, passiveMsgId, msgSeq);
   }
}
