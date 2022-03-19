using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{
    [Obsolete("2022年3月15日后废弃,请使用 精华消息API代替此功能")]
    public AnnounceApi GetAnnounceApi()
    {
        return new(apiBase);
    }
}

/// <summary>
/// 公告Api
/// </summary>
[Obsolete("2022年3月15日后废弃,请使用 精华消息API代替此功能")]
public class AnnounceApi
{
    private readonly ApiBase _apiBase;

    public AnnounceApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 创建频道公告
    /// </summary>
    /// <param name="guildId">频道ID</param>
    /// <param name="messageId">消息ID</param>
    /// <returns>公告对象</returns>
    [Obsolete("2022年3月15日后废弃,请使用 精华消息API代替此功能")]
    public async Task<Announces> CreateAsync(string guildId, string messageId)
    {
        RawCreateAnnounceApi rawCreateAnnounceApi;

        var processedInfo = ApiFactory.Process(rawCreateAnnounceApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id,guildId }
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {"message_id",messageId }
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);
 
        return requestData.ToObject<Announces>();
    }

    /// <summary>
    /// 删除子频道公告
    /// </summary>
    /// <param name="channelId">子频道ID</param>
    /// <param name="messageId">消息ID</param>
    /// <returns></returns>
    [Obsolete("2022年3月15日后废弃,请使用 精华消息API代替此功能")]
    public async ValueTask DeleteAsync(string channelId, string messageId)
    {
        RawDeleteAnnounceApi rawDeleteAnnounceApi;

        var processedInfo = ApiFactory.Process(rawDeleteAnnounceApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,channelId },
            {ParamType.message_id,messageId }
        });

        await _apiBase.RequestAsync(processedInfo);
    }
}