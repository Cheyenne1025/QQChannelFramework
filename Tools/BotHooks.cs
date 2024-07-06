using MyBot.Models.MessageModels;

namespace MyBot.Tools;

public delegate void OnBotSentGuildMessageDelegate(string channelId, Message msg);

public delegate void OnBotSentChatGroupMessageDelegate(string groupOpenId, ChatMessageResp msg);

public static class BotHooks {
    public static event OnBotSentGuildMessageDelegate OnBotSentGuildMessage;
    public static event OnBotSentChatGroupMessageDelegate OnBotSentChatGroupMessage;

    internal static void DispatchGuildMessage(string channelId, Message msg) {
        OnBotSentGuildMessage?.Invoke(channelId, msg);
    }

    internal static void DispatchChatGroupMessage(string groupOpenId, ChatMessageResp msg) {
        OnBotSentChatGroupMessage?.Invoke(groupOpenId, msg);
    }
}