using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{
    private static MuteApi _muteApi;

    public MuteApi GetMuteApi()
    {
        if (_muteApi is null)
        {
            _muteApi = new(apiBase);
        }

        return _muteApi;
    }
}


public class MuteApi
{
    readonly ApiBase _apiBase;

    public MuteApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }

    private async ValueTask MuteChannel(string guildId, DateTime mute_end_timstamp = default, string mute_seconds = "")
    {
        RawGlobalMuteApi rawGlobalMuteApi;

        var processedInfo = ApiFactory.Process(rawGlobalMuteApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id, guildId}
        });

        await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {mute_end_timstamp == default ? "mute_seconds":"mute_end_timstamp", mute_end_timstamp == default ? mute_seconds : new DateTimeOffset(mute_end_timstamp).ToUnixTimeSeconds()}
            })
            .RequestAsync(processedInfo);
    }

    /// <summary>
    /// 禁言整个频道至指定日期时间
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="mute_end_timstamp">禁言结束时间</param>
    /// <returns></returns>
    public async ValueTask MuteTo(string guildId, DateTime mute_end_timstamp)
    {
        await MuteChannel(guildId, mute_end_timstamp: mute_end_timstamp);
    }

    /// <summary>
    /// 禁言整个频道指定的秒数
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="mute_seconds">禁言秒数</param>
    /// <returns></returns>
    public async ValueTask MuteChannel(string guildId, int mute_seconds)
    {
        await MuteChannel(guildId, mute_seconds: mute_seconds.ToString());
    }

    private async ValueTask MuteMember(string guildId, string userId, DateTime mute_end_timstamp = default, string mute_seconds = "")
    {
        RawMuteMemberApi rawMuteMemberApi;

        var processedInfo = ApiFactory.Process(rawMuteMemberApi, new Dictionary<ParamType, string>()
        {
            {ParamType.guild_id, guildId },
            {ParamType.user_id, userId }
        });

        await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {mute_end_timstamp == default ? "mute_seconds": "mute_end_timestamp", mute_end_timstamp == default ? mute_seconds : new DateTimeOffset(mute_end_timstamp).ToUnixTimeSeconds()}
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
    public async ValueTask MuteMemberTo(string guildId, string userId, DateTime mute_end_timstamp)
    {
        await MuteMember(guildId, userId, mute_end_timstamp: mute_end_timstamp);
    }

    /// <summary>
    /// 禁言某个成员指定时间
    /// </summary>
    /// <param name="guildId">主频道ID</param>
    /// <param name="userId">禁言用户ID</param>
    /// <param name="mute_seconds">禁言秒数</param>
    /// <returns></returns>
    public async ValueTask MuteMember(string guildId, string userId, int mute_seconds)
    {
        await MuteMember(guildId, userId, mute_seconds: mute_seconds.ToString());
    }
}