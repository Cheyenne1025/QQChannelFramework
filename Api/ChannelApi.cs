using System.Collections.Generic;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Api.Types;
using MyBot.Datas;
using MyBot.Models;
using MyBot.Models.Types;
using Newtonsoft.Json.Linq;
namespace MyBot.Api;

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
    /// <param name="channelId">子频道ID</param>
    /// <returns>子频道信息</returns>
    public async Task<Channel> GetInfoAsync(string channelId) {
        RawGetChannelApi rawGetChannelApi;

        var processedInfo = ApiFactory.Process(rawGetChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData.ToObject<Channel>();
    }

    /// <summary>
    /// 获取频道下的子频道列表
    /// </summary>
    /// <param name="guild_id">主频道Guild</param>
    /// <returns>子频道列表</returns>
    public async Task<List<Channel>> GetChannelsAsync(string guild_id) {
        RawGetChannelsApi rawGetChannelsApi;

        var processedInfo = ApiFactory.Process(rawGetChannelsApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id}
        });

        JArray requestDatas =
            JArray.Parse((await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false)).ToString());

        List<Channel> channels = new();

        foreach (var channelInfo in requestDatas) {
            channels.Add(channelInfo.ToObject<Channel>());
        }

        return channels;
    }

    /// <summary>
    /// 创建子频道 (私域可用)
    /// </summary>
    /// <param name="newChannel">新子频道对象，无需填写Id字段</param>
    /// <param name="privateUserIds">额外允许查看的用户列表</param>
    /// <returns>创建的子频道对象</returns>
    public async Task<Channel> CreateChannelAsync(string guildId, Channel newChannel) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawCreateChannelApi rawCreateChannelApi;

        var processedInfo = ApiFactory.Process(rawCreateChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId}
        });

        var requestData = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>() {
                {"name", newChannel.Name},
                {"type", (int) newChannel.Type},
                {"sub_type", (int) newChannel.SubType},
                {"position", newChannel.Position},
                {"parent_id", newChannel.ParentId},
                {"private_type", (int) newChannel.PrivateType},
                {"private_user_ids", newChannel.PrivateUserIds},
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

        RawUpdateChannelApi rawUpdateChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channel.Id}
        });

        var requestData = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>() {
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

        RawUpdateChannelApi rawUpdateChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>() {
                {"private_type", (int) privateType},
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
    public async Task<Channel> UpdateChannelNameAsync(string channelId, string name) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawUpdateChannelApi rawUpdateChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>() {
                {"name", name},
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
    public async Task<Channel> UpdateChannelPrivateTypeAsync(string channelId, ChannelPrivateType privateType, string[] privateUserIds) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawUpdateChannelApi rawUpdateChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>() {
                {"private_type", (int) privateType},
                {"private_user_ids", privateUserIds},
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
    public async Task<Channel> UpdateChannelSpeakPermissionAsync(string channelId, ChannelSpeakPermission speakPermission) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawUpdateChannelApi rawUpdateChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>() {
                {"speak_permission", (int) speakPermission}
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
    public async Task<Channel> UpdateChannelParentAndPositionAsync(string channelId, string parent, int position) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawUpdateChannelApi rawUpdateChannelApi;

        var processedInfo = ApiFactory.Process(rawUpdateChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        var requestData = await _apiBase
            .WithJsonContentData(new Dictionary<string, object>() {
                {"position", position},
                {"parent_id", parent}
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

        RawDeleteChannelApi rawDeleteChannelApi;

        var processedInfo = ApiFactory.Process(rawDeleteChannelApi, new Dictionary<ParamType, string>() {
            {ParamType.channel_id, channelId}
        });

        await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);
    }
}