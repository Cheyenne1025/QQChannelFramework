using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi {
    public AnnounceApi GetAnnounceApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 公告Api
/// </summary> 
public class AnnounceApi {
    private readonly ApiBase _apiBase;

    public AnnounceApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 创建频道公告（消息）
    /// </summary>
    /// <param name="guildId">频道ID</param>
    /// <param name="messageId">消息ID</param>
    /// <returns>公告对象</returns>
    public async Task<Announces> CreateAsync(string guildId, string messageId, string channelId) {
        RawCreateAnnounceApi rawCreateAnnounceApi;

        var processedInfo = ApiFactory.Process(rawCreateAnnounceApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var requestData = await _apiBase
            .WithContentData(new {
                message_id = messageId,
                channel_id = channelId
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData.ToObject<Announces>();
    }

    /// <summary>
    /// 创建频道公告（消息）
    /// </summary>
    /// <param name="guildId">频道ID</param> 
    /// <param name="announcesType">公告类别</param>
    /// <param name="recommendChannels">推荐子频道列表</param>
    /// <returns>公告对象</returns>
    public async Task<Announces> CreateAsync(string guildId, AnnounceType announcesType,
        RecommendChannel[] recommendChannels) {
        RawCreateAnnounceApi rawCreateAnnounceApi;

        var processedInfo = ApiFactory.Process(rawCreateAnnounceApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var requestData = await _apiBase
            .WithContentData(new {
                announces_type = announcesType,
                recommend_channels = recommendChannels
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData.ToObject<Announces>();
    }

    /// <summary>
    /// 删除频道公告
    /// </summary>
    /// <param name="guildId">频道ID</param>
    /// <param name="messageId">消息ID</param>
    /// <returns></returns> 
    public async ValueTask DeleteAsync(string guildId, string messageId = "all") {
        RawDeleteAnnounceApi rawDeleteAnnounceApi;

        var processedInfo = ApiFactory.Process(rawDeleteAnnounceApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId},
            {ParamType.message_id, messageId}
        });

        await _apiBase.RequestAsync(processedInfo);
    }
}