using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBot.Api.Base;
using MyBot.Api.Raws;
using MyBot.Api.Types;
using MyBot.Datas;
using MyBot.Models;
using Newtonsoft.Json.Linq;
namespace MyBot.Api;

sealed partial class QQChannelApi {
    public MemberApi GetMemberApi() {
        return new(apiBase);
    }
}

/// <summary>
/// 成员Api
/// </summary>
public class MemberApi {
    private readonly ApiBase _apiBase;

    public MemberApi(ApiBase apiBase) {
        _apiBase = apiBase;
    }

    /// <summary>
    /// 获取某个成员信息
    /// </summary>
    /// <returns>成员信息/returns>
    public async Task<Member> GetInfoAsync(string guild_id, string user_id) {
        RawGetMemberInfoApi rawGetMemberInfoApi;

        var precessedInfo = ApiFactory.Process(rawGetMemberInfoApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id},
            {ParamType.user_id, user_id}
        });

        var requestData = await _apiBase.RequestAsync(precessedInfo).ConfigureAwait(false);
        return requestData.ToObject<Member>();
    }

    /// <summary>
    /// 获取频道内所有成员信息 (私域可用)
    /// </summary>
    /// <param name="guild_id">主频道GuildID</param>
    /// <param name="after">上一次回包中最大的用户ID， 如果是第一次请求填0，默认为0</param>
    /// <param name="limit">分页大小，1-400，默认是1</param>
    /// <returns>元组 (成员集合，成员数量)</returns>
    /// <exception cref="Exceptions.BotNotIsPrivateException"></exception>
    public async Task<List<Member>> GetMembersAsync(string guild_id, string after = "0", UInt32 limit = 1) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        if (limit > 400) {
            limit = 400;
        }

        RawGetChannelMembersApi rawGetChannelMembersApi;

        var processedInfo = ApiFactory.Process(rawGetChannelMembersApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guild_id}
        });

        var requestData = await _apiBase
            .WithQueryParam(new Dictionary<string, object>() {
                {"after", after},
                {"limit", limit}
            })
            .RequestAsync(processedInfo);

        JArray jArray = JArray.Parse(requestData.ToString());

        List<Member> members = new();

        foreach (var info in jArray) {
            members.Add(info.ToObject<Member>());
        }

        return members;
    }

    /// <summary>
    /// 删除指定频道成员 (私域可用)
    /// </summary>
    /// <param name="guildId"></param>
    /// <param name="userId"></param>
    /// <param name="addBlackList"></param>
    /// <param name="deleteHistoryMsgDays"></param>
    /// <returns></returns>
    public async Task<bool> DeleteMemberAsync(string guildId, string userId) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawDeleteChannelMemberApi rawDeleteChannelMemberApi;

        var processedInfo = ApiFactory.Process(rawDeleteChannelMemberApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId},
            {ParamType.user_id, userId}
        });

        var requestData = await _apiBase.RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData is null;
    }

    public async Task<bool> DeleteMemberAsync(string guildId, string userId, bool addBlackList,
        int deleteHistoryMsgDays) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        RawDeleteChannelMemberApi rawDeleteChannelMemberApi;

        var processedInfo = ApiFactory.Process(rawDeleteChannelMemberApi, new Dictionary<ParamType, string>() {
            {ParamType.guild_id, guildId},
            {ParamType.user_id, userId}
        });

        var requestData = await _apiBase.WithJsonContentData(
            new {
                add_blacklist = addBlackList,
                delete_history_msg_days = deleteHistoryMsgDays
            }
        ).RequestAsync(processedInfo).ConfigureAwait(false);

        return requestData is null;
    }

    /// <summary>
    /// 获取频道内所有成员信息 (私域可用)
    /// </summary>
    /// <param name="guild_id">主频道GuildID</param> 
    /// <returns>成员集合</returns>
    /// <exception cref="Exceptions.BotNotIsPrivateException"></exception>
    public async Task<List<Member>> GetAllMembersAsync(string guild_id) {
        if (CommonState.PrivateBot is false) {
            throw new Exceptions.BotNotIsPrivateException();
        }

        List<Member> ret = new List<Member>();

        string after = "0";

        while (true) {
            var batch = await GetMembersAsync(guild_id, after, 400);

            if (!batch.Any())
                break;

            ret.AddRange(batch);

            after = batch.Last().User.Id;
        }

        return ret;
    }
}