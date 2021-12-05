using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelModels.Types;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{
    private static ChildChannelApi _childChannelApi;

    public ChildChannelApi GetChildChannelApi()
    {
        if (_childChannelApi is null)
        {
            _childChannelApi = new(apiBase);
        }

        return _childChannelApi;
    }
}

/// <summary>
/// 子频道Api
/// </summary>
public class ChildChannelApi
{
    readonly ApiBase _apiBase;

    public ChildChannelApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取子频道信息
    /// </summary>
    /// <param name="childChannel_id">子频道ID</param>
    /// <returns>子频道信息</returns>
    public async Task<ChildChannel> GetInfoAsync(string childChannel_id)
    {
        RawGetChildChannelApi rawGetChildChannelApi;

        var processedInfo = ApiFactory.Process(rawGetChildChannelApi, new Dictionary<ParamType, string>()
            {
                {ParamType.channel_id,childChannel_id }
            });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        ChildChannel childChannel = new()
        {
            Id = requestData["id"].ToString(),
            GuildId = requestData["guild_id"].ToString(),
            Name = requestData["name"].ToString(),
            Type = Enum.Parse<ChildChannelType>(requestData["type"].ToString()),
            Position = int.Parse(requestData["position"].ToString()),
            ParentId = requestData["parent_id"].ToString(),
            OwnerId = requestData["owner_id"].ToString()
        };

        return null;
    }

    /// <summary>
    /// 获取频道下的子频道列表
    /// </summary>
    /// <param name="guild_id">主频道Guild</param>
    /// <returns>元组 (子频道列表,数量)</returns>
    public async Task<(List<ChildChannel>, int)> GetChildChannelsAsync(string guild_id)
    {
        RawGetChildChannelsApi rawGetChildChannelsApi;

        var processedInfo = ApiFactory.Process(rawGetChildChannelsApi, new Dictionary<ParamType, string>()
            {
                {ParamType.guild_id,guild_id }
            });

        JArray requestDatas = JArray.Parse((await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false)).ToString());

        List<ChildChannel> childChannels = new();

        foreach (var channelInfo in requestDatas)
        {
            childChannels.Add(new()
            {
                Id = channelInfo["id"].ToString(),
                GuildId = channelInfo["guild_id"].ToString(),
                Name = channelInfo["name"].ToString(),
                Type = Enum.Parse<ChildChannelType>(channelInfo["type"].ToString()),
                Position = int.Parse(channelInfo["position"].ToString()),
                ParentId = channelInfo["parent_id"].ToString(),
                OwnerId = channelInfo["owner_id"].ToString()
            });
        }

        return (childChannels, childChannels.Count);
    }
}