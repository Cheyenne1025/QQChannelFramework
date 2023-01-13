using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Api.Types;

namespace MyBot.Api;

sealed partial class QQChannelApi {
    public MuteApi GetMuteApi() {
        return new(apiBase);
    }
}

public class MuteApi
{
    readonly ApiBase _apiBase;

    public MuteApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    private async ValueTask MuteGuildAsync(string guildId, DateTime mute_end_timstamp = default,
        string mute_seconds = "")
    {
        RawGlobalMuteApi rawGlobalMuteApi;

        var processedInfo = ApiFactory.Process(rawGlobalMuteApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id, guildId}
        });

        await _apiBase
            .WithJsonContentData(new Dictionary<string, object>()
            {
                {
                    mute_end_timstamp == default ? "mute_seconds" : "mute_end_timestamp", mute_end_timstamp == default
                        ? mute_seconds
                        : new DateTimeOffset(mute_end_timstamp).ToUnixTimeSeconds().ToString()
                }
            })
            .RequestAsync(processedInfo);
    }

    /// <summary>
    /// 解除频道禁言
    /// </summary>
    /// <param name="guildId"></param>
    public async ValueTask UnMuteGuildAsync(string guildId)
    {
        RawGlobalMuteApi rawGlobalMuteApi;

        var processedInfo = ApiFactory.Process(rawGlobalMuteApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id, guildId}
        });

        await _apiBase
            .WithJsonContentData(new Dictionary<string, object>()
            {
                {"mute_seconds", "0"}, {"mute_end_timestamp", "0"}
            })
            .RequestAsync(processedInfo);
    }

    /// <summary>
    /// 禁言整个频道至指定日期时间
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="mute_end_timstamp">禁言结束时间</param>
    /// <returns></returns>
    public async ValueTask MuteGuildToAsync(string guildId, DateTime mute_end_timstamp)
    {
        await MuteGuildAsync(guildId, mute_end_timstamp: mute_end_timstamp);
    }

    /// <summary>
    /// 禁言整个频道指定的秒数
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="mute_seconds">禁言秒数</param>
    /// <returns></returns>
    public async ValueTask MuteGuildAsync(string guildId, int mute_seconds)
    {
        await MuteGuildAsync(guildId, mute_seconds: mute_seconds.ToString());
    }

    private async ValueTask MuteMemberAsync(string guildId, string userId, DateTime mute_end_timstamp = default,
        string mute_seconds = "")
    {
        RawMuteMemberApi rawMuteMemberApi;

        var processedInfo = ApiFactory.Process(rawMuteMemberApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id, guildId}, {ParamType.user_id, userId}
        });

        await _apiBase
            .WithJsonContentData(new Dictionary<string, object>()
            {
                {
                    mute_end_timstamp == default ? "mute_seconds" : "mute_end_timestamp", mute_end_timstamp == default
                        ? mute_seconds
                        : new DateTimeOffset(mute_end_timstamp).ToUnixTimeSeconds().ToString()
                }
            })
            .RequestAsync(processedInfo);
    }

    /// <summary>
    /// 解除成员禁言
    /// </summary>
    /// <param name="guildId"></param>
    /// <param name="userId"></param>
    public async ValueTask UnMuteMemberAsync(string guildId, string userId)
    {
        RawMuteMemberApi rawMuteMemberApi;

        var processedInfo = ApiFactory.Process(rawMuteMemberApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id, guildId}, {ParamType.user_id, userId}
        });

        await _apiBase
            .WithJsonContentData(new Dictionary<string, object>()
            {
                {"mute_seconds", "0"}, {"mute_end_timestamp", "0"}
            })
            .RequestAsync(processedInfo);
    }

    /// <summary>
    /// 禁言指定的成员至指定日期时间
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="userId">禁言用户ID</param>
    /// <param name="mute_end_timstamp">禁言结束时间</param>
    /// <returns></returns>
    public async ValueTask MuteMemberToAsync(string guildId, string userId, DateTime mute_end_timstamp)
    {
        await MuteMemberAsync(guildId, userId, mute_end_timstamp: mute_end_timstamp);
    }

    /// <summary>
    /// 禁言某个成员指定时间
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="userId">禁言用户ID</param>
    /// <param name="mute_seconds">禁言秒数</param>
    /// <returns></returns>
    public async ValueTask MuteMemberAsync(string guildId, string userId, int mute_seconds)
    {
        await MuteMemberAsync(guildId, userId, mute_seconds: mute_seconds.ToString());
    }

    private async ValueTask MuteMoreMember(string guildId, StringCollection userIds, DateTime mute_end_timstamp = default,
        string mute_seconds = "")
    {
        RawMuteMoreMemberApi rawMuteMoreMemberApi;

        var processedInfo = ApiFactory.Process(rawMuteMoreMemberApi, new Dictionary<ParamType, string>()
        {
            [ParamType.guild_id] = guildId
        });

        await _apiBase
            .WithJsonContentData(new Dictionary<string, object>()
            {
                [mute_end_timstamp == default ? "mute_seconds" : "mute_end_timestamp"] = mute_end_timstamp == default
                    ? mute_seconds
                    : new DateTimeOffset(mute_end_timstamp).ToUnixTimeSeconds().ToString()
                , ["user_ids"] = userIds
            }).RequestAsync(processedInfo)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// 批量禁言指定的成员至指定日期时间
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="userIds">禁言用户ID集合</param>
    /// <param name="mute_end_timstamp">禁言结束时间</param>
    /// <returns></returns>
    public async ValueTask MuteMemberToAsync(string guildId, StringCollection userIds, DateTime mute_end_timstamp)
    {
        await MuteMoreMember(guildId, userIds, mute_end_timstamp: mute_end_timstamp);
    }

    /// <summary>
    /// 批量禁言某些成员指定时间
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="userId">禁言用户ID集合</param>
    /// <param name="mute_seconds">禁言秒数</param>
    /// <returns></returns>
    public async ValueTask MuteMemberAsync(string guildId, StringCollection userIds, int mute_seconds)
    {
        await MuteMoreMember(guildId, userIds, mute_seconds: mute_seconds.ToString());
    }

    /// <summary>
    /// 批量解除成员禁言
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="userIds">禁言用户ID集合</param>
    public async ValueTask UnMuteMoreMemberAsync(string guildId, StringCollection userIds)
    {
        RawMuteMemberApi rawMuteMemberApi;

        var processedInfo = ApiFactory.Process(rawMuteMemberApi, new Dictionary<ParamType, string>()
        {
            [ParamType.guild_id] = guildId
        });

        await _apiBase
            .WithJsonContentData(new Dictionary<string, object>()
            {
                ["mute_seconds"] = "0", ["mute_end_timestamp"] = "0", ["user_ids"] = userIds
            })
            .RequestAsync(processedInfo);
    }
    
    
}