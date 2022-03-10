using System.Collections.Generic;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models.MessageModels;

namespace QQChannelFramework.Api;

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
}