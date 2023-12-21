using System.Collections.Generic;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Api.Types;
using MyBot.Models.MessageModels;
using MyBot.Models.Returns;
namespace MyBot.Api;

sealed partial class QQChannelApi {
    public ReactionApi GetReactionApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 精华消息Api
/// </summary>
public class ReactionApi {
    readonly ApiBase _apiBase;

    public ReactionApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 发表表情表态
    /// </summary> 
    /// <returns></returns>
    public async Task AddReactionAsync(string channelId, string messageId, Emoji emoji) {
        RawAddReactionApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId},
            {ParamType.message_id, messageId},
            {ParamType.type, emoji.Type.ToString()},
            {ParamType.id, emoji.Id}
        });

        await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);
    }
    
    /// <summary>
    /// 删除自己的表情表态 
    /// </summary> 
    /// <returns></returns>
    public async Task RemoveReactionAsync(string channelId, string messageId, Emoji emoji) {
        RawRemoveReactionApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId},
            {ParamType.message_id, messageId},
            {ParamType.type, emoji.Type.ToString()},
            {ParamType.id, emoji.Id}
        });

        await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);
    }

    /// <summary>
    /// 拉取对消息指定表情表态的用户列表
    /// </summary>
    /// <returns></returns>
    public async Task<GetReactionsResult> GetReactionsAsync(string channelId, string messageId, int type, string id, string cookie = "", int limit = 20) {
        RawGetReactionsApi raw;

        var processedInfo = ApiFactory.Process(raw, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId},
            {ParamType.message_id, messageId},
            {ParamType.type, type.ToString()},
            {ParamType.id, id}
        });

        var requestData = await _apiBase.WithQueryParam(new Dictionary<string, object> { { "cookie", cookie.ToLower() }, { "limit", limit } })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData.ToObject<GetReactionsResult>();
    }
}