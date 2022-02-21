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

sealed partial class QQChannelApi {
    public ChannelApi GetChannelApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 子频道Api
/// </summary>
public class ChannelApi {
    readonly ApiBase _apiBase;

    public ChannelApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取子频道信息
    /// </summary>
    /// <param name="childChannelId">子频道ID</param>
    /// <returns>子频道信息</returns>
    public async Task<Channel> GetInfoAsync(string childChannelId) {
        RawGetChildChannelApi rawGetChildChannelApi;

        var processedInfo = ApiFactory.Process(rawGetChildChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, childChannelId}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData.ToObject<Channel>();
    }

    /// <summary>
    /// 获取频道下的子频道列表
    /// </summary>
    /// <param name="guild_id">主频道Guild</param>
    /// <returns>元组 (子频道列表,数量)</returns>
    public async Task<List<Channel>> GetChannelsAsync(string guild_id) {
        RawGetChildChannelsApi rawGetChildChannelsApi;

        var processedInfo = ApiFactory.Process(rawGetChildChannelsApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id}
        });

        JArray requestDatas =
            JArray.Parse((await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false)).ToString());

        List<Channel> childChannels = new();

        foreach (var channelInfo in requestDatas) {
            childChannels.Add(channelInfo.ToObject<Channel>());
        }

        return childChannels;
    }

    /// <summary>
    /// 创建子频道 (私域可用)
    /// </summary>
    /// <param name="newChannel">新子频道对象，无需填写Id字段</param>
    /// <param name="privateUserIds">额外允许查看的用户列表</param>
    /// <returns>创建的子频道对象</returns>
    public async Task<Channel> CreateChannelAsync(Channel newChannel, string[] privateUserIds = null) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawCreateChildChannelApi rawCreateChildChannelApi;

        var processedInfo = ApiFactory.Process(rawCreateChildChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, newChannel.GuildId}
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>() {
                {"name", newChannel.Name},
                {"type", (int) newChannel.Type},
                {"sub_type", (int) newChannel.SubType},
                {"position", newChannel.Position},
                {"parent_id", newChannel.ParentId},
                {"private_type", (int) newChannel.PrivateType},
                {"private_user_ids", privateUserIds},
                {"speak_permission", (int) newChannel.SpeakPermission},
                {"application_id", newChannel.ApplicationId}
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData.ToObject<Channel>();
    }

    /// <summary>
    /// 更新子频道信息 (私域可用)
    /// </summary>
    /// <param name="channel">子频道</param> 
    /// <returns></returns>
    public async Task<Channel> UpdateChannelInfoAsync(Channel channel) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawUpdateChildChannelApi rawUpdateChildChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChildChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channel.Id}
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>() {
                {"name", channel.Name},
                {"type", (int) channel.Type},
                {"position", channel.Position},
                {"parent_id", channel.ParentId},
                {"private_type", (int) channel.PrivateType},
                {"speak_permission", (int) channel.SpeakPermission}
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData.ToObject<Channel>();
    }

    /// <summary>
    /// 更新子频道信息 (私域可用)
    /// </summary>
    /// <param name="channel">子频道</param> 
    /// <returns></returns>
    public async Task<Channel> UpdateChannelPrivateTypeAsync(string channelId, ChannelPrivateType privateType) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawUpdateChildChannelApi rawUpdateChildChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChildChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>() {
                {"private_type", (int) privateType},
            })
            .RequestAsync(processedInfo)
            .ConfigureAwait(false);

        return requestData.ToObject<Channel>();
    }

    /// <summary>
    /// 删除子频道 (私域可用)
    /// </summary>
    /// <param name="channelId">要删除的子频道ID</param>
    /// <returns></returns>
    public async ValueTask DeleteChannelAsync(string channelId) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawDeleteChildChannelApi rawDeleteChildChannelApi;

        var processedInfo = ApiFactory.Process(rawDeleteChildChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);
    }
}