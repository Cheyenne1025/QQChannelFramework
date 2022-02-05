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
    public AnnounceApi GetAnnounceApi()
    {
        return new(apiBase);
    }
}

/// <summary>
/// 公告Api
/// </summary>
public class AnnounceApi
{
    private readonly ApiBase _apiBase;

    public AnnounceApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 创建子频道公告
    /// </summary>
    /// <param name="childChannel">子频道ID</param>
    /// <param name="messageId">消息ID</param>
    /// <returns>公告对象</returns>
    public async Task<Announces> CreateAsync(string childChannel, string messageId)
    {
        RawCreateAnnounceApi rawCreateAnnounceApi;

        var processedInfo = ApiFactory.Process(rawCreateAnnounceApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel }
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {"message_id",messageId }
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        Announces announces = new()
        {
            Guild = requestData["guild_id"].ToString(),
            ChildChannel = requestData["channel_id"].ToString(),
            MessageId = requestData["message_id"].ToString()
        };

        return announces;
    }

    /// <summary>
    /// 删除子频道公告
    /// </summary>
    /// <param name="childChannel">子频道ID</param>
    /// <param name="messageId">消息ID</param>
    /// <returns></returns>
    public async ValueTask DeleteAsync(string childChannel, string messageId)
    {
        RawDeleteAnnounceApi rawDeleteAnnounceApi;

        var processedInfo = ApiFactory.Process(rawDeleteAnnounceApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel },
            {ParamType.message_id,messageId }
        });

        await _apiBase.RequestAsync(processedInfo);
    }
}