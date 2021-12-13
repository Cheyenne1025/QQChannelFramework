using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelModels.Types;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Datas;
using QQChannelFramework.Models;
using QQChannelFramework.Models.Types;

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
            SubType = Enum.Parse<ChildChannelSubType>(requestData["sub_type"].ToString()),
            Position = int.Parse(requestData["position"].ToString()),
            ParentId = ((JObject)requestData).ContainsKey("parent_id") ? requestData["parent_id"].ToString() : "",
            OwnerId = requestData["owner_id"].ToString()
        };

        return childChannel;
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

    /// <summary>
    /// 创建子频道 (私域可用)
    /// </summary>
    /// <param name="guild_id">主频道Guild</param>
    /// <param name="name">子频道名</param>
    /// <param name="type">子频道类型</param>
    /// <param name="position">子频道排序</param>
    /// <param name="parent_id">子频道所属分组ID</param>
    /// <returns>创建的子频道对象</returns>
    public async Task<ChildChannel> CreateChildChannelAsync(string guild_id, string name, ChildChannelType type, UInt32 position, UInt32 parent_id)
    {
        if (CommonState.PrivateBot is false)
        {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawCreateChildChannelApi rawCreateChildChannelApi;

        var processedInfo = ApiFactory.Process(rawCreateChildChannelApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id,guild_id }
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {"name",name },
                {"type",(int)type },
                {"position",position },
                {"parent_id",parent_id }
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

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

        return childChannel;
    }

    /// <summary>
    /// 更新子频道信息 (私域可用)
    /// </summary>
    /// <param name="childChannel_id">子频道ID</param>
    /// <param name="newName">子频道名</param>
    /// <param name="newType">子频道类型</param>
    /// <param name="newPosition">排序</param>
    /// <param name="parent_id">分组 id</param>
    /// <returns></returns>
    public async Task<ChildChannel> UpdateChildChannelInfoAsync(string childChannel_id, string newName, ChildChannelType newType, UInt32 newPosition, string parent_id)
    {
        if (CommonState.PrivateBot is false)
        {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawUpdateChildChannelApi rawUpdateChildChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChildChannelApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel_id }
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {"name",newName },
                {"type",(int)newType },
                {"position",newPosition },
                {"parent_id",parent_id }
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

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

        return childChannel;
    }

    /// <summary>
    /// 删除子频道 (私域可用)
    /// </summary>
    /// <param name="childChannel_id">要删除的子频道ID</param>
    /// <returns></returns>
    public async ValueTask DeleteChildChannelAsync(string childChannel_id)
    {
        if(CommonState.PrivateBot is false)
        {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawDeleteChildChannelApi rawDeleteChildChannelApi;

        var processedInfo = ApiFactory.Process(rawDeleteChildChannelApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel_id }
        });

        await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);
    }
}